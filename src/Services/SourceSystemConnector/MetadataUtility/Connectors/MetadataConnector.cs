using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MetadataUtility.Models;

namespace MetadataUtility.Connectors
{
    public abstract class MetadataConnector : IMetadataConnector
    {
        protected readonly SourceSystemConnection _connection;

        protected MetadataConnector(SourceSystemConnection connection)
        {
            _connection = connection;
        }

        public abstract bool CanConnect();
        public abstract Task<List<DomainEntity>> GetEntities();
        public abstract Task<List<DomainEntityAttribute>> GetEntityAttributes();
        public abstract Task<List<DomainEntityAttribute>> GetEntityAttributes(string entityName);
        public abstract Task<Dictionary<string, List<DomainEntityAttribute>>> GetEntityAttributes(List<string> entityNames);
        public abstract Task<List<DomainEntityRelation>> GetEntityRelations();
        public abstract Task<List<DomainEntityRelation>> GetEntityRelations(string entityName);
    }
}
