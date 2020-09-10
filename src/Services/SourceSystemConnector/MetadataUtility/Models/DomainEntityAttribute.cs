using System;
using System.Collections.Generic;
using System.Text;

namespace MetadataUtility.Models
{
    public class DomainEntityAttribute
    {
        public string EntityName { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Position { get; set; }
        public short MaxLength { get; set; }
        public byte Precision { get; set; }
        public bool IsNullable { get; set; }
        public bool IsIdentity { get; set; }
    }
}
