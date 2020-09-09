using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SourceSystemConnectorApi.Azure
{
    public interface IAzureKeyVaultClient
    {
        Task<string> GetSecretValue(string key);
        Task SetSecretValue(string key, string value);
    }
}
