using API.DTO;
using API.Validators;
using Core.Interfaces;
using FluentValidation;
using Infraestructure.UnitOfWork;

namespace API.Extensions;
public static class ApplicationServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()   //WithOrigins("https://dominio.com")
                    .AllowAnyMethod()          //WithMethods("GET","POST")
                    .AllowAnyHeader());        //WithHeaders("accept","content-type")
            });

    public static void AddAplicacionServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void RegisterValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<ProductoAddUpdateDto>, ProductoValidator>();

    }

    public static void RegisterValidatorsMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ValidationMiddleware<ProductoAddUpdateDto>>();
    }

}
