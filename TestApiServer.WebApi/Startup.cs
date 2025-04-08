using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using TestApiServer.Persistence;
using TestApiServer.WebApi.Middlewares;
using TestOnlineStore.WebApi.Configurations;
using TestOnlineStore.WebApi.Middleware;
namespace TestApiServer.WebApi
{
    public class Startup(IConfiguration congiguration, IWebHostEnvironment env )
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPersistence(congiguration);
            services.AddControllers();
            //services.AddSwaggerGen(x =>
            //{
            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    x.IncludeXmlComments(xmlPath);
            //});
            services.AddSwaggerGen();
            //add vor V1V2
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("X-Version"));
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AddApiVersionParametersWhenVersionNeutral = true;
            });
        }
        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    config.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });
            app.UseCustomExceptionHandler();
            app.UseRouting();

            app.UseHttpsRedirection();         

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
