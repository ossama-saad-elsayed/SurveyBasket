using Mapster;
using MapsterMapper;
using SurveyBasket.Services;
using System.Reflection;
using FluentValidation.AspNetCore;
using FluentValidation;
using SurveyBasket.Persistence;
using Microsoft.EntityFrameworkCore;

namespace SurveyBasket
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            var connectionstring = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(optionsAction => optionsAction.UseSqlServer(connectionstring));



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
