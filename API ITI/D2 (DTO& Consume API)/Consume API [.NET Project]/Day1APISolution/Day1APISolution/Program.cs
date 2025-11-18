
using Day1APISolution.Models;
using Microsoft.EntityFrameworkCore;

namespace Day1APISolution
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ITIContext>(options => 
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
                });
            builder.Services.AddCors(options =>
            {
                //do it when the consumer is web , because the browser who has cors policy
                options.AddPolicy("MyPolicy", policy =>
                {
                    // policy.WithOrigins();
                    //policy.AllowAnyOrigin()
                    //policy.AllowAnyOrigin().WithMethods();
                    //policy.AllowAnyOrigin().AllowAnyMethod()
                    //policy.AllowAnyOrigin().AllowAnyMethod().WithHeaders();
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

				});
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
			app.UseStaticFiles();
			app.UseCors("MyPolicy");
			app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
