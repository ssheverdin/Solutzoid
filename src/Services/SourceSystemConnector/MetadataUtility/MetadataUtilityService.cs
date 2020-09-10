using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetadataUtility.Connectors;
using MetadataUtility.Connectors.SqlServerConnector;
using MetadataUtility.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MetadataUtility
{
    public class MetadataUtilityService : IMetadataUtilityService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MetadataUtilityService> _logger;
        private readonly Dictionary<string, IMetadataConnector> _metadataConnectors = new Dictionary<string, IMetadataConnector>();

        public MetadataUtilityService(IConfiguration configuration, ILogger<MetadataUtilityService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }


        public IMetadataConnector GetMetadataConnector(SourceSystemConnection sourceSystemConnection)
        {
            if (HasMetadataConnector(sourceSystemConnection.SourceSystem))
            {
                return _metadataConnectors[sourceSystemConnection.SourceSystem];
            }
            AddMetadataConnector(sourceSystemConnection);
            return _metadataConnectors[sourceSystemConnection.SourceSystem];
        }

        private void AddMetadataConnector(SourceSystemConnection connection)
        {
            if (!string.IsNullOrEmpty(connection.Connection))
            {
                switch (connection.Type)
                {
                    case "SqlServer":
                        _metadataConnectors.Add(connection.SourceSystem, new SqlServerMetadataConnector(connection));
                        break;
                    default:
                        break;
                }
            }
        }

        public async Task<List<DomainEntity>> GetDomainEntities(string sourceSystem)
        {
            if (HasMetadataConnector(sourceSystem))
            {
                return await _metadataConnectors[sourceSystem].GetEntities();
            }

            return null;
        }

        public async Task<List<DomainEntityAttribute>> GetEntityAttributes(string sourceSystem)
        {
            if (HasMetadataConnector(sourceSystem))
            {
                return await _metadataConnectors[sourceSystem].GetEntityAttributes();
            }

            return null;
        }

        public async Task<List<DomainEntityAttribute>> GetEntityAttributes(string sourceSystem, string entityName)
        {
            if (HasMetadataConnector(sourceSystem))
            {
                return await _metadataConnectors[sourceSystem].GetEntityAttributes(entityName);
            }

            return null;
        }

        public async Task<Dictionary<string, List<DomainEntityAttribute>>> GetEntityAttributes(string sourceSystem, List<string> entityNames)
        {
            if (HasMetadataConnector(sourceSystem))
            {
                return await _metadataConnectors[sourceSystem].GetEntityAttributes(entityNames);
            }

            return null;
        }

        public async Task<List<DomainEntityRelation>> GetEntityRelations(string sourceSystem)
        {
            if (HasMetadataConnector(sourceSystem))
            {
                return await _metadataConnectors[sourceSystem].GetEntityRelations();
            }

            return null;
        }

        public async Task<List<DomainEntityRelation>> GetEntityRelations(string sourceSystem, string entityName)
        {
            if (HasMetadataConnector(sourceSystem))
            {
                return await _metadataConnectors[sourceSystem].GetEntityRelations(entityName);
            }

            return null;
        }

        private bool HasMetadataConnector(string key)
        {
            return _metadataConnectors.ContainsKey(key);
        }

        private bool CanConnect(string key)
        {
            if (HasMetadataConnector(key))
            {
                return _metadataConnectors[key].CanConnect();
            }

            return false;
        }
    }
}
