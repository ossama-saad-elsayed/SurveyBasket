
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using SurveyBasket.Services;
using System.Data;
using System.Reflection;
using SharpGrip.FluentValidation.AutoValidation.Mvc;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
namespace SurveyBasket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDependencies();
            // Add services to the container.

          
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
