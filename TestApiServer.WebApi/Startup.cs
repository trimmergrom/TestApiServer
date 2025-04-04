using System.Reflection;
using TestApiServer.Persistence;
using TestApiServer.WebApi.Middlewares;
using TestOnlineStore.WebApi.Middleware;
namespace TestApiServer.WebApi
{
    public class Startup(IConfiguration congiguration, IWebHostEnvironment env )
    {
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddPersistence(congiguration);
            services.AddControllers();
            services.AddSwaggerGen();            
            
        }
        public void Configure(IApplicationBuilder app)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI();
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
