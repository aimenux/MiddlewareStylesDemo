using WebApi.Middlewares;

namespace WebApi
{
    public class Startup
    {
        public void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<FactoryStyleMiddleware>();
        }

        public void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.UseAuthorization();

            app.UseInlineStyleMiddleware();

            app.UseConventionStyleMiddleware();

            app.UseFactoryStyleMiddleware();

            app.MapControllers();
        }
    }
}
