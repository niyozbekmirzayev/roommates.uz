using Microsoft.EntityFrameworkCore;
using Roommates.Data;
using Roommates.Data.IRepositories;
using Roommates.Data.Repositories;

namespace Roommates.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();
            builder.Services.AddScoped<IRoommateRepository, RoommateRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<ILocationRepository, LocationRepository>();
            builder.Services.AddScoped<IFileRepository, FileRepository>();
            builder.Services.AddScoped<PrepDatabase>();

            builder.Services.AddDbContext<RoommatesDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("Postgre")));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<PrepDatabase>();
            }

            // Configure the HTTP request pipeline.
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
}