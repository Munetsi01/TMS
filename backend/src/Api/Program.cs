
using Api.Extensions;
using Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration)
                                                .Enrich.FromLogContext().CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            //identity
            var connectionString = builder.Configuration.GetConnectionString("PostgresqlDBConnectionString") ?? throw new InvalidOperationException("Connection string 'PostgresqlDBConnectionString' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(connectionString));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //register application services here
            builder.Services.AddTokenProvider();
            builder.Services.AddRepositories();
            builder.Services.AddHandlers();

            //register automapper
            builder.Services.AddAutoMapper(configAction => configAction.AddProfile(typeof(ApplicationMappingProfile)));

            var app = builder.Build();

            //creates the database and the tables using the added migrations
            using (var serviceScope = app.Services.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
