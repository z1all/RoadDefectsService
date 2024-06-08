using Microsoft.EntityFrameworkCore;
using RoadDefectsService.Core.Application.DTOs.UserService;
using RoadDefectsService.Core.Application.Enums;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Contexts;

namespace RoadDefectsService.Infrastructure.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<List<CustomUser>> GetAllByFilterAsync(UserFilterDTO userFilter, bool showOperators)
        {
            return await ApplyFilter(userFilter, showOperators)
                .Skip((userFilter.Page - 1) * userFilter.Size)
                .Take(userFilter.Size)
                .ToListAsync();
        }

        public async Task<int> CountByFilterAsync(UserFilterDTO userFilter, bool showOperators)
        {
            return await ApplyFilter(userFilter, showOperators)
                .CountAsync();
        }

        private IQueryable<CustomUser> ApplyFilter(UserFilterDTO userFilter, bool showOperators)
        {
            var users = _dbContext.Users.AsQueryable();

            if (userFilter.UserFullName is not null)
            {
                users = users.Where(user => user.FullName.ToLower().Contains(userFilter.UserFullName.ToLower()));
            }

            if (showOperators)
            {
                users = userFilter.UserRole switch
                {
                    RoleFilter.Operator => users.Where(user => user.HighestRole == Role.Operator || user.HighestRole == Role.Admin),
                    RoleFilter.RoadInspector => users.Where(user => user.HighestRole == Role.RoadInspector),
                    _ => users
                };
            }
            else
            {
                users = users.Where(user => user.HighestRole == Role.RoadInspector);
            }

            return users;
        }
    }
}
