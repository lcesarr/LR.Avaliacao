using LR.Avaliacao.Api.Filters;
using LR.Avaliacao.Ioc.ResolveDependecia;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace LR.Avaliacao.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(opcoes =>
            {
                opcoes.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllers();
            services.AddScoped<DefaultExceptionFilterAttribute>();
            services.AddAutoMapper(new Assembly[]
            {
                Assembly.Load("LR.Avaliacao.Api"),
                Assembly.Load("LR.Avaliacao.Application"),
                Assembly.Load("LR.Avaliacao.Domain"),
                Assembly.Load("LR.Avaliacao.Infrastructure"),
                Assembly.Load("LR.Avaliacao.IoC"),
                Assembly.Load("LR.Avaliacao.Util")
            });
            ResolveDependencias.AddResolverDependencies(services);
            services.AddHealthChecks();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "LR.Avaliacao",
                    Description = "API - LR.Avaliacao",
                    Version = "v1"
                });

                var apiPath = Path.Combine(AppContext.BaseDirectory, "LR.Avaliacao.Api.xml");
                var applicationPath = Path.Combine(AppContext.BaseDirectory, "LR.Avaliacao.Api.xml");

                c.IncludeXmlComments(apiPath);
                c.IncludeXmlComments(applicationPath);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePathBase("/LR.Avaliacao");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/LR.Avaliacao/swagger/v1/swagger.json", "API LR.Avaliacao");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(option =>
            {
                option.AllowAnyOrigin();
                option.AllowAnyMethod();
                option.AllowAnyHeader();
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
