using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Todo.Domain.Application.Handlers;
using Todo.Domain.Infra.Contexts;
using Todo.Domain.Infra.Repositories;
using Todo.Domain.Repositories;

namespace Todo.Domain.Api.Extensions
{
    public static class BuilderConfigurationExtension
    {
        public static IServiceCollection LoadConfigSwagger(this IServiceCollection service)
        {
            service.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIContagem", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
                        "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
                        "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return service;
        }

        public static IServiceCollection LoadConfigDataContext(this IServiceCollection service, ConfigurationManager configuration)
        {
            service.AddDbContext<DataContext>(options =>
                options.UseInMemoryDatabase("Database")
            );

            return service;
        }

        public static IServiceCollection LoadDependecyInjections(this IServiceCollection service)
        {
            service.AddScoped<ITodoRepository, TodoRepository>();
            service.AddScoped<TodoHandler, TodoHandler>();
            return service;
        }

    }
}
