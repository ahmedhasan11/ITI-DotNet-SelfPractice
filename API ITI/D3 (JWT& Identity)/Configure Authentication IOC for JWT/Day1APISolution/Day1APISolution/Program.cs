
using Day1APISolution.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ITIContext>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme= JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters= new TokenValidationParameters() 
                {
                    ValidateIssuer = true,
				    ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
				    ValidateAudience = true,
                    ValidAudience= builder.Configuration["JwtOptions:Audience"],
                    IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SecretKey"]))
                };
            }) ;





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
            //app.UseAuthorization -->is here by default but looking for cookie
			app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
