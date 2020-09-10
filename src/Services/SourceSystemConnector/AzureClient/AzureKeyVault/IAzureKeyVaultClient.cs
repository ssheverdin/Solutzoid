using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureClient.AzureKeyVault
{
    public interface IAzureKeyVaultClient
    {
        Task<string> GetSecretValue(string key);
        Task SetSecretValue(string key, string value);
    }
}
