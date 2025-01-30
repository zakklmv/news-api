using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using News.Api.Application.Stories.v1;
using News.Api.Infrastructure.Services.HackerNews.v0;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(
            x =>
            {
                x.EnableAnnotations();
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "News API", Version = "v1" });
            });
        builder.Services.AddApiVersioning();

        builder.Services.AddScoped<IHackerNewsProvider, HackerNewsProvider>();
        builder.Services.AddScoped<IHackerNewsProvider, HackerNewsProvider>();

        builder.Services.AddHttpClient<IHackerNewsHttpClient, HackerNewsHttpClient>();

        builder.Services.AddScoped<IValidator<GetBestStoriesQuery>, GetBestStoriesQueryValidator>();
        builder.Services.AddFluentValidationAutoValidation();

        var configuration = builder.Configuration;
        builder.Services.AddOptions<HackerNewsConfig>()
            .Bind(
                configuration
                    .GetSection(nameof(HackerNewsConfig)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });


        builder.Services.AddMemoryCache();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        var app = builder.Build();

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
