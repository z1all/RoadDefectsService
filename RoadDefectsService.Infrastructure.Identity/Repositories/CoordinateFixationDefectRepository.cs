using Microsoft.EntityFrameworkCore;
using RoadDefectsService.Core.Application.DTOs.MetricsService;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain;
using RoadDefectsService.Infrastructure.Identity.Contexts;

namespace RoadDefectsService.Infrastructure.Identity.Repositories
{
    public class CoordinateFixationDefectRepository : ICoordinateFixationDefectRepository
    {
        private readonly AppDbContext _appDbContext;

        public CoordinateFixationDefectRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<List<CoordinateFixationDefect>> GetAllByFilterAsync(CoordinatesFilter coordinatesFilter)
        {
            DateTime beginDateOnly = ConvertToDateTimeUtc(coordinatesFilter.BeginDateOnly);
            DateTime endDateOnly = ConvertToDateTimeUtc(coordinatesFilter.EndDateOnly);

            return _appDbContext.FixationDefects
                .Where(fixationDefect => beginDateOnly < fixationDefect.RecordedDateTime && fixationDefect.RecordedDateTime < endDateOnly &&
                                         (coordinatesFilter.ShowNotEliminated ? true : fixationDefect.IsEliminated == true))
                .Select(fixationDefect => new CoordinateFixationDefect()
                {
                    FixationDefectId = fixationDefect.Id,
                    FixationDateTime = fixationDefect.RecordedDateTime,
                    IsEliminated = fixationDefect.IsEliminated,
                    X = fixationDefect.CoordinatesX,
                    Y = fixationDefect.CoordinatesY
                })
                .ToListAsync();
        }

        public static DateTime ConvertToDateTimeUtc(DateOnly date)
        {
            return DateTime.SpecifyKind(date.ToDateTime(TimeOnly.MinValue).Date, DateTimeKind.Utc);
        }
    }
}
