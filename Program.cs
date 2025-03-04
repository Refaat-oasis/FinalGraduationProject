using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.Database;
using ThothSystemVersion1.Hubs;
using ThothSystemVersion1.MappingProfiles;

namespace ThothSystemVersion1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // add the business logic layer services
            builder.Services.AddScoped<AdminBusinessLogicLayer>();
            builder.Services.AddScoped<InventoryBussinesLogicLayer>();
            builder.Services.AddScoped<TechnicalBusinessLogicLayer>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddDbContext<ThothContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
         
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(10); 
                options.Cookie.HttpOnly = true; 
                options.Cookie.IsEssential = true; 
            });
            
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSignalR();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
           
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
            app.MapStaticAssets();

            app.MapHub<ProductHub>("/InventoryReorderPoint");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=employee}/{action=Loginpage}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
