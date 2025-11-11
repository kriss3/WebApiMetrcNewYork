
using WebApiMetrcNewYork.App.Client;
using WebApiMetrcNewYork.App.Models;
using WebApiMetrcNewYork.App.Services;

namespace WebApiMetrcNewYork.App;

public class Program
{
    public static void Main()
    {
        var builder = WebApplication.CreateBuilder();

		builder.Services.Configure<MetrcOptions>(
            builder.Configuration.GetSection(MetrcOptions.SectionName));
		builder.Services.AddTransient<MetrcAuthHandler>();

		var allowedOrigins = builder.Configuration
            .GetSection("Cors:AllowedOrigins").Get<string[]>()
            	?? ["http://localhost:3000", "https://localhost:3000"];

		builder.Services.AddCors(o => o.AddPolicy("Frontend", p => 
        p	.WithOrigins(allowedOrigins)
        	.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()));


		// Named client; BaseAddress is optional since I have absolute URLs.
		builder.Services.AddHttpClient("MetrcNY", (sp, http) =>
		{
			http.DefaultRequestHeaders.Accept.ParseAdd("application/json");
		}).AddHttpMessageHandler<MetrcAuthHandler>();

		builder.Services.AddScoped<IMetrcHttp, MetrcHttp>();
		builder.Services.AddScoped<IMetrcDeliveriesService, MetrcDeliveriesService>();

		builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        var app = builder.Build();


        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
