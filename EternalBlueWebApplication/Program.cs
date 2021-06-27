using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EternalBlueWebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var builtConfig = config.Build();

                    config.AddAzureKeyVault(
                        builtConfig.GetValue<string>("AzureKeyVault:DNS"),
                        builtConfig.GetValue<string>("AzureKeyVault:ClientId"),
                        builtConfig.GetValue<string>("AzureKeyVault:ClientSecret"));
                })
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}