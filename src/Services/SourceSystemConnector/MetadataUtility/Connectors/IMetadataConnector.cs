using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MetadataUtility.Models;

namespace MetadataUtility.Connectors
{
    public interface IMetadataConnector
    {
        bool CanConnect();
        Task<List<DomainEntity>> GetEntities();
        Task<List<DomainEntityAttribute>> GetEntityAttributes();
        Task<List<DomainEntityAttribute>> GetEntityAttributes(string entityName);
        Task<Dictionary<string, List<DomainEntityAttribute>>> GetEntityAttributes(List<string> entityNames);
        Task<List<DomainEntityRelation>> GetEntityRelations();
        Task<List<DomainEntityRelation>> GetEntityRelations(string entityName);
    }
}
