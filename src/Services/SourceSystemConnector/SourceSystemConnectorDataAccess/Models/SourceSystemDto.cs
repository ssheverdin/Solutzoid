using DataUnitOfWork.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SourceSystemConnectorDataAccess.Models
{
    [Table("SourceSystem")]
    public class SourceSystemDto : EntityBase
    {
        public string Name { get; set; }
        public string ConnectionName { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool? SyncEnabled { get; set; }
        public DateTime? InitialSyncDate { get; set; }
        public DateTime? LastSyncDate { get; set; }
    }
}
