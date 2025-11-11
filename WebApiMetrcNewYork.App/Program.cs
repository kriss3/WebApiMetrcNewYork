
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
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

		builder.Services.AddTransient<MetrcAuthHandler>();


		// Named client; BaseAddress is optional since I have absolute URLs.
		builder.Services.AddHttpClient("MetrcNY", (sp, http) =>
		{
			var opts = sp.GetRequiredService<IOptions<MetrcOptions>>().Value;
			http.BaseAddress = new Uri(opts.BaseUrl);

			// Accept header is already set by MetrcAuthHandler in your code.
			// If you ever move it out of the handler, uncomment this:
			// http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}).AddHttpMessageHandler<MetrcAuthHandler>();

		//builder.Services.AddScoped<IMetrcDeliveriesService, MetrcDeliveriesService>();
		// 4) DI: client abstraction + services
		builder.Services.AddScoped<IMetrcHttp, MetrcHttp>();
		builder.Services.AddScoped<IMetrcPackagesService, MetrcPackagesService>();
		builder.Services.AddScoped<IMetrcDeliveriesSandboxService, MetrcDeliveriesSandboxService>();

		builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        var app = builder.Build();


        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

		app.UseCors("Frontend");
		app.MapControllers();

        app.Run();
    }
}
