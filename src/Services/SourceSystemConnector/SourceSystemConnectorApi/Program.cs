using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SourceSystemConnectorApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((context,cfg) => {
                    var buildConfig = cfg.Build();
                    var valtName = buildConfig["AzureKeyVaultUrl"];
                    var keyVaultClient = new KeyVaultClient(async (authority, resource, scope) => {
                        var credentials = new DefaultAzureCredential(false);
                        var token = credentials.GetToken(new Azure.Core.TokenRequestContext(new[] { "https://vault.azure.net/.default" }));
                        return token.Token;
                    });
                    cfg.AddAzureKeyVault(valtName, keyVaultClient, new DefaultKeyVaultSecretManager());
                });
    }
}
