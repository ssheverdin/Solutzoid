using System;
using System.Collections.Generic;
using System.Text;

namespace MetadataUtility.Models
{
    public class SourceSystemConnectorConnection : SourceSystemConnection
    {
        public bool HasMetadataConnector { get; set; }
        public bool CanConnect { get; set; } 
    }
}
