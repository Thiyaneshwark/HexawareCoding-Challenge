using LoanManagementSystem.Data;
using LoanManagementSystem.Interfaces;
using LoanManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace LoanManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

            // Register DbContext with SQL Server connection
            builder.Services.AddDbContext<LoanManagementContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register repository
            builder.Services.AddScoped<ILoanRepository, LoanRepository>();

            // Add Swagger services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Loan Management API V1");
                    c.RoutePrefix = string.Empty; // Swagger is served at the root URL
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
