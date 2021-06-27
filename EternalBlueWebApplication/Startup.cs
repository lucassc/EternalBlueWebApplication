using EternalBlueWebApplication.Contracts;
using EternalBlueWebApplication.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EternalBlueWebApplication
{
    public class Startup
    {
        public Startup()
        {
        }

        public void ConfigureServices(IServiceCollection services) =>
            services
                .AddSingleton<IEternalBlueService, EternalBlueService>()
                .AddControllersWithViews();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app
                    .UseExceptionHandler("/Home/Error")
                    .UseHsts();
            }

            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=EternalBlue}/{action=Index}/{id?}"));
        }
    }
}