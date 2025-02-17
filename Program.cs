using Microsoft.EntityFrameworkCore;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.Database;

namespace ThothSystemVersion1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<AdminBusinessLogicLayer>();
            builder.Services.AddDbContext<ThothContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=employee}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
