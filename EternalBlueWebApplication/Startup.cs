using EternalBlueWebApplication.Configurations;
using EternalBlueWebApplication.Contracts;
using EternalBlueWebApplication.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EternalBlueWebApplication
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) =>
            _configuration = configuration;

        public void ConfigureServices(IServiceCollection services) =>
            services
                .AddSingleton<IPasswordService, PasswordService>()
                .AddSingleton(_configuration.GetSection(nameof(AzureSecretsName)).Get<AzureSecretsName>())
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