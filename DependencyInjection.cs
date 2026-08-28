using Mapster;
using MapsterMapper;
using SurveyBasket.Services;
using System.Reflection;
using FluentValidation.AspNetCore;
using FluentValidation;

namespace SurveyBasket
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies (this IServiceCollection services)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            var MappingConfig = TypeAdapterConfig.GlobalSettings;
            MappingConfig.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton<IMapper>(new Mapper(MappingConfig));
            services.AddScoped<IPollService, PollService>();
            services.AddFluentValidationAutoValidation().
                AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
