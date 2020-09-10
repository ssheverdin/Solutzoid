using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetadataUtility.Models;
using Microsoft.EntityFrameworkCore;

namespace MetadataUtility.Connectors.SqlServerConnector
{
    public class SqlServerMetadataConnector : MetadataConnector
    {
        public SqlServerMetadataConnector(SourceSystemConnection connection) : base(connection)
        {
        }

        public override bool CanConnect()
        {
            try
            {
                using (var sqlServerDatabase = new SqlServerContext(_connection.Connection))
                {
                    return sqlServerDatabase.Database.CanConnect();
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public override async Task<List<DomainEntity>> GetEntities()
        {
            using (SqlServerContext sqlServerDatabase = new SqlServerContext(_connection.Connection))
            {
                return await sqlServerDatabase.DomainEntities.FromSqlRaw(@"
                    SELECT
                    sysSchema.name as [Schema],
                    sysObj.name as [Name],
                    [Type] =
                    CASE
                        WHEN sysObj.type = 'U' THEN 'Table'
                        WHEN sysObj.type = 'V' THEN 'View'
                        ELSE NULL
                    END,
                    sysObj.create_date as CreatedDate,
                    sysObj.modify_date as LastUpdatedDate
                    FROM sys.objects as sysObj
                    INNER JOIN sys.schemas as sysSchema
                    ON sysSchema.schema_id = sysObj.schema_id
                    WHERE sysObj.type in ('U','V') AND sysSchema.name != 'sys'
                    ").AsNoTracking().ToListAsync();
            }
        }

        public override async Task<List<DomainEntityAttribute>> GetEntityAttributes()
        {
            using (var sqlServerDatabase = new SqlServerContext(_connection.Connection))
            {
                return await sqlServerDatabase.DomainEntityAttributes.FromSqlRaw(@"
                    SELECT 
                    [EntityName] = obj.name,
                    [Name] = col.name,
                    [Type] = ct.name,
                    Position = col.column_id,
                    [MaxLength] = col.max_length,
                    [Precision] = col.precision,
                    [IsNullable] = col.is_nullable,
                    [IsIdentity] = col.is_identity
                    FROM sys.columns as col
                    INNER JOIN sys.objects obj
                    ON obj.object_id = col.object_id
                    INNER JOIN sys.schemas as sch
                    ON sch.schema_id = obj.schema_id
                    LEFT JOIN sys.types as ct
                    ON ct.system_type_id = col.system_type_id AND ct.schema_id = 4 AND ct.user_type_id != 256
                    WHERE obj.type IN ('U','V') AND sch.name != 'sys' 
                    ").AsNoTracking().ToListAsync();
            }
        }

        public override async Task<List<DomainEntityAttribute>> GetEntityAttributes(string entityName)
        {
            return (await GetEntityAttributes()).Where(i => i.EntityName == entityName).ToList();
        }

        public override async Task<Dictionary<string, List<DomainEntityAttribute>>> GetEntityAttributes(List<string> entityNames)
        {
            var entities = await GetEntities();
            var attributes = await GetEntityAttributes();
            Dictionary<string, List<DomainEntityAttribute>> result = new Dictionary<string, List<DomainEntityAttribute>>();
            foreach (DomainEntity domainEntity in entities)
            {
                result.Add(domainEntity.Name, attributes.Where(i => i.EntityName == domainEntity.Name).ToList());
            }

            return result;
        }

        public override async Task<List<DomainEntityRelation>> GetEntityRelations()
        {
            using (var sqlServerDatabase = new SqlServerContext(_connection.Connection))
            {
                return await sqlServerDatabase.DomainEntityRelations.FromSqlRaw(@"
                     SELECT
                        fk.name as Name,
                        tp.name as EntityName,
                        cp.name as EntityAttributeName,
                        tr.name as RelationEntityName,
                        cr.name as RelationEntityAttributeName
                    FROM 
                        sys.foreign_keys fk
                    INNER JOIN 
                        sys.tables tp ON fk.parent_object_id = tp.object_id
                    INNER JOIN 
                        sys.tables tr ON fk.referenced_object_id = tr.object_id
                    INNER JOIN 
                        sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
                    INNER JOIN 
                        sys.columns cp ON fkc.parent_column_id = cp.column_id AND fkc.parent_object_id = cp.object_id
                    INNER JOIN 
                        sys.columns cr ON fkc.referenced_column_id = cr.column_id AND fkc.referenced_object_id = cr.object_id
                    ORDER BY
                        tp.name, cp.column_id
                    ").AsNoTracking().ToListAsync();
            }
        }

        public override async Task<List<DomainEntityRelation>> GetEntityRelations(string entityName)
        {
            return (await GetEntityRelations()).Where(i => i.EntityName == entityName).ToList();
        }


        private void SqlCommand(string sql, IEnumerable<object> parameters)
        {
            using (var sqlServerDatabase = (new SqlServerContext(_connection.Connection)))
            {
                sqlServerDatabase.Database.ExecuteSqlRaw(sql, parameters);
            }
        }
    }
}
