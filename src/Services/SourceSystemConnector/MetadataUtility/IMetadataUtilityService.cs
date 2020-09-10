using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MetadataUtility.Connectors;
using MetadataUtility.Models;

namespace MetadataUtility
{
    public interface IMetadataUtilityService
    {
        IMetadataConnector GetMetadataConnector(SourceSystemConnection sourceSystemConnection);
        Task<List<DomainEntity>> GetDomainEntities(string sourceSystem);

        Task<List<DomainEntityAttribute>> GetEntityAttributes(string sourceSystem);
        Task<List<DomainEntityAttribute>> GetEntityAttributes(string sourceSystem, string entityName);
        Task<Dictionary<string, List<DomainEntityAttribute>>> GetEntityAttributes(string sourceSystem, List<string> entityNames);
        Task<List<DomainEntityRelation>> GetEntityRelations(string sourceSystem);
        Task<List<DomainEntityRelation>> GetEntityRelations(string sourceSystem, string entityName);
    }
}
