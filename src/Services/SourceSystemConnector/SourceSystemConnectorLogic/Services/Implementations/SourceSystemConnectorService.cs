using DataUnitOfWork;
using SourceSystemConnectorContracts.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AzureClient.AzureKeyVault;
using MetadataUtility;
using MetadataUtility.Models;
using SourceSystemConnectorDataAccess.Models;

namespace SourceSystemConnectorLogic.Services.Implementations
{
    public class SourceSystemConnectorService : ISourceSystemConnectorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAzureKeyVaultClient _azureKeyVaultClient;
        private readonly IMetadataUtilityService _metadataUtility;

        public SourceSystemConnectorService(IUnitOfWork unitOfWork, IAzureKeyVaultClient azureKeyVaultClient, IMetadataUtilityService metadataUtility)
        {
            _unitOfWork = unitOfWork;
            _azureKeyVaultClient = azureKeyVaultClient;
            _metadataUtility = metadataUtility;
        }

        public async Task<IEnumerable<SourceSystemContract>> GetSourceSystems()
        {
            var savedSourceSystems = await _unitOfWork.GetRepository<SourceSystemDto>().GetAllAsync();
            var result = new List<SourceSystemContract>();
            foreach (var savedSourceSystem in savedSourceSystems)
            {
                var metadataConnector = _metadataUtility.GetMetadataConnector(new SourceSystemConnection()
                {
                    SourceSystem =  savedSourceSystem.Name,
                    Connection = await _azureKeyVaultClient.GetSecretValue(savedSourceSystem.ConnectionName),
                    Type = savedSourceSystem.Type
                });

                result.Add(new SourceSystemContract()
                {
                    Name = savedSourceSystem.Name,
                    Description = savedSourceSystem.Description,
                    Type = savedSourceSystem.Type,
                    HasMetadataConnector = metadataConnector != null,
                    CanConnect = metadataConnector?.CanConnect() ?? false,
                    HasConnection = !string.IsNullOrEmpty(savedSourceSystem.ConnectionName),
                    HasMetadata = savedSourceSystem.InitialSyncDate.HasValue,
                    HasSync = savedSourceSystem.SyncEnabled == true,

                });
            }

            return result;
        }
    }
}
