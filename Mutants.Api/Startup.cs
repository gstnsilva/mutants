using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Mutants.Core.Filters;
using Mutants.Core.Infrastructure;
using Mutants.Models;
using Mutants.ResourceAccess;
using Mutants.Services;
using Newtonsoft.Json;

namespace Mutants.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Info>(Configuration.GetSection("Info"));
            services.Configure<DnaEvaluationSettings>(Configuration.GetSection("DnaEvaluationSettings"));
            services.Configure<MutantsDbSettings>(Configuration.GetSection(nameof(MutantsDbSettings)));
            
            services.AddSingleton<HttpRequestHelper>(new HttpRequestHelper());
            services.AddSingleton<MutantsDbSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MutantsDbSettings>>().Value);
            services.AddSingleton<MutantsDbContext>();

            services.AddScoped<IDnaRepository, DnaRepository>();
            services.AddScoped<IDnaSequenceEvaluator, DnaSequenceEvaluator>();
            services.AddScoped<IDnaSequenceService, DnaSequenceService>();
            
            services.AddControllers(options =>
            {
                options.CacheProfiles.Add("Static", new CacheProfile { Duration = 86400 });
                options.CacheProfiles.Add("Collection", new CacheProfile { Duration = 60 });
                options.CacheProfiles.Add("Resource", new CacheProfile { Duration = 180 });

                options.Filters.Add<JsonExceptionFilter>();
                options.Filters.Add<LinkRewritingFilter>();
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
            });

            services
                .AddRouting(options => options.LowercaseUrls = true);
                
            services.AddResponseCaching();
            
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Mutants API";
                    document.Info.Description = "We are the future, Charles, not them! They no longer matter!";
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseAuthorization();

            app.UseResponseCaching();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
