using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureClient.AzureKeyVault
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
            try
            {
                var secret = await GetSecretClient().GetSecretAsync(key);
                return secret.Value.Value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;

        }

        public async Task SetSecretValue(string key, string value)
        {
            var secret = new KeyVaultSecret(key, value);
            await GetSecretClient().SetSecretAsync(secret);
        }
    }
}
