﻿using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace RoadDefectsService.Presentation.Web.Configurations.Swagger
{
    public class SwaggerGenOptionsConfigure : IConfigureOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            SwaggerControllerOrder<BaseController> swaggerControllerOrder = new SwaggerControllerOrder<BaseController>(Assembly.GetEntryAssembly());

            options.OrderActionsBy((apiDesc) => $"{swaggerControllerOrder.SortKey(apiDesc.ActionDescriptor.RouteValues["controller"])}");

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n" +
                                "Enter 'Your token in the text input below.\r\n\r\n" +
                                "Example: \"eyJhbGciOiJIUzI1NiJ9.eyJwYXlsb2FkIjoi0JLQvtC_0YDQvtGBLCDQl9CQ0KfQldCcPz8_In0.lyOs-Vq66shvnDET9eAQ_9pjhxhwkqf8B_9hhOuq8Yc\"",
            });

            options.OperationFilter<SwaggerAuthOperationFilter>();

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        }
    }
}
