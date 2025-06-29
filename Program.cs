using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.Hubs;
using ThothSystemVersion1.Utilities;

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
            builder.Services.AddScoped<CostBusinessLogicLayer>();
            builder.Services.AddScoped<AccountingBusinessLogicLayer>();

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

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ThothContext>();
                DataBaseAdminAddition.Initialize(context);
            }

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
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
