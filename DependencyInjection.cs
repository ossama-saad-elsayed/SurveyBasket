using Mapster;
using MapsterMapper;
using SurveyBasket.Services;
using System.Reflection;
using FluentValidation.AspNetCore;
using FluentValidation;
using SurveyBasket.Persistence;
using Microsoft.EntityFrameworkCore;
using SurveyBasket.Entities;
using Microsoft.AspNetCore.Identity;
using SurveyBasket.Contracts.Authentication;
using SurveyBasket.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddOptions<JwtOptions>()
                .BindConfiguration(JwtOptions.SectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();
            var JwtSettingsOptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o => 
            {
                o.SaveToken=true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettingsOptions?.key!)),
                    ValidIssuer = JwtSettingsOptions?.Issuer,
                    ValidAudience = JwtSettingsOptions?.Audience
                };

            } 

            ); 


            var MappingConfig = TypeAdapterConfig.GlobalSettings;
            MappingConfig.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton<IMapper>(new Mapper(MappingConfig));
            services.AddScoped<IPollService, PollService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddSingleton<IJwtProvider, JwtProvider>();
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
