
using Microsoft.AspNetCore.Http.HttpResults;

namespace IDentAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            app.MapGet("/weatherforecast", (int number) =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    })
                    .ToArray();

                Console.WriteLine(number);

                return forecast;
            });

            app.MapPost("/PostTimeTable", (object TimeTable) => {
                Console.WriteLine(TimeTable);
            })
            .WithName("PostTimeTable")
            .WithOpenApi();

            app.MapGet("/GetTickets", (string dateTimeFrom, string dateTimeTo) =>
            {
                //выгрузка из бд заявок
                return Results.Ok("выгрузка из бд заявок");
            });

            //app.MapGet("/book", )

            app.Run();
        }
    }
}
