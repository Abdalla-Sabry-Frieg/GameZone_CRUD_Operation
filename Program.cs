using GameZone.Data;
using Microsoft.EntityFrameworkCore;

namespace GameZone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //Add ConnectionString to database
            var ConnectionString = builder.Configuration.GetConnectionString("DefultConnection") ??
                throw new InvalidOperationException("No found the connection string");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(ConnectionString));

            builder.Services.AddControllersWithViews();

            // Add Services Interface

            builder.Services.AddScoped<ICategoriesService,CategoriesService>();
            builder.Services.AddScoped<IDevicesServices,DevicesServices>();
            builder.Services.AddScoped<IGamesServices,GamesServices>();


            
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
          
          //  app.MapControllers();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=home}/{action=Index}/{id?}");
           
            app.Run();
        }
    }
}
