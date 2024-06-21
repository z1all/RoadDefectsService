using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RoadDefectsService.Infrastructure.Identity.Configurations.DbSeed;
using RoadDefectsService.Infrastructure.Identity.Seeds.Models.Base;

namespace RoadDefectsService.Infrastructure.Identity.Seeds.Creators.Base
{
    public abstract class DbModelCreator<TModel, TCreate> 
        where TCreate : BaseCreateDTO<TModel>
    {
        private readonly ILogger<DbModelCreator<TModel, TCreate>> _logger;

        protected DbModelCreator(ILogger<DbModelCreator<TModel, TCreate>> logger, IOptions<DbSeedOptions> options)
        {
            _logger = logger;

            string jsonContent;
            using (StreamReader reader = new StreamReader(options.Value.Path))
            {
                jsonContent = reader.ReadToEnd();
            }

            TCreate? createModels = JsonConvert.DeserializeObject<TCreate>(jsonContent);
            Models = createModels?.Models ?? [];
        }

        protected List<TModel> Models { get; set; }
        protected abstract void CreateModel(TModel model);
        protected virtual void UpdateModel(TModel model) { }
        protected abstract bool CheckExistModel(TModel model);

        public void AddModels()
        {
            try
            {
                foreach (var model in Models)
                {
                    bool modelExist = CheckExistModel(model);
                    if (!modelExist)
                    {
                        CreateModel(model);
                    }
                    else
                    {
                        UpdateModel(model);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when adding models");
            }
        }
    }
}
