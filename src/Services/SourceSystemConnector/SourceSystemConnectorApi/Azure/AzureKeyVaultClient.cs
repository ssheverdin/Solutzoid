using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SourceSystemConnectorApi.Azure
{
    public class AzureKeyVaultClient : IAzureKeyVaultClient
    {
        private readonly IConfiguration _configuration;

        public AzureKeyVaultClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SecretClient GetSecretClient()
        {
            var creds = new DefaultAzureCredential();
            var secretClient = new SecretClient(new Uri(_configuration["AzureKeyVaultUrl"]), creds);
            return secretClient;
        }

        public async Task<string> GetSecretValue(string key)
        {
            var secret = await GetSecretClient().GetSecretAsync("key");
            return secret.Value.Value;
        }

        public async Task SetSecretValue(string key, string value)
        {
            var secret = new KeyVaultSecret("key", "value");
            await GetSecretClient().SetSecretAsync(secret);
        }
    }
}
