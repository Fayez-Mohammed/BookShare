//using Book_Sphere.Data;
//using Book_Sphere.Interfaces;
//using Book_Sphere.Models;
//using Book_Sphere.Repository;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//namespace Book_Sphere
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // Add services to the container.
//            builder.Services.AddControllersWithViews();
//            builder.Services.AddSession();
//            builder.Services.AddDbContext<ApplicationDbContext>(options =>
//            {
//                options.UseSqlServer(builder.Configuration.GetConnectionString("C1"));
//            });
//            builder.Services.AddScoped<ICatecory, CategoryRepo>();
//            builder.Services.AddScoped<IBook, BookRepo>();
//            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
//            {
//                options.Password.RequiredLength = 4;
//                options.Password.RequireDigit = false;
//            }).AddEntityFrameworkStores<ApplicationDbContext>();
//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            if (!app.Environment.IsDevelopment())
//            {
//                app.UseExceptionHandler("/Home/Error");
//                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
//            }

//            app.UseHttpsRedirection();
//            app.UseRouting();

//            app.UseAuthorization();

//            app.MapStaticAssets();
//            app.MapControllerRoute(
//                name: "default",
//                pattern: "{controller=Home}/{action=Index}/{id?}")
//                .WithStaticAssets();

//            app.Run();
//        }
//    }
//}


using Book_Sphere.Data;
using Book_Sphere.Interfaces;
using Book_Sphere.Models;
using Book_Sphere.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Book_Sphere
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("C1")));

            builder.Services.AddScoped<ICatecory, CategoryRepo>();
            builder.Services.AddScoped<IBook, BookRepo>();


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            var app = builder.Build();

            // Environment-specific error handling
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Detailed errors in development
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); // Friendly error page in production
                app.UseHsts(); // Enforce HTTPS
            }

            // Common middleware
            app.UseHttpsRedirection();
            app.UseStaticFiles(); // Make sure your CSS/JS works
            app.UseRouting();
            app.UseAuthentication(); // Required for Identity
            app.UseAuthorization();
            app.UseSession();

            // Routing
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
