using CoreIntecWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreIntecWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            Console.WriteLine("Hello World!");

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSqlServer<PeopleContext>(builder.Configuration.GetConnectionString("DefaultConnection"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=People}/{action=Index}/{id?}");

            app.Run();
        }
    }
}