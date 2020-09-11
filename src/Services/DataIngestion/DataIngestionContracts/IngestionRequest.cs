using System;
using System.Collections.Generic;

namespace DataIngestionContracts
{
    public class IngestionRequest
    {
        public string SourceSystem { get; set; }
        public List<string> SourceEntities { get; set; }
    }
}
