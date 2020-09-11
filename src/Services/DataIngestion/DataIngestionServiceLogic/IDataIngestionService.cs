using DataIngestionContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataIngestionServiceLogic
{
    public interface IDataIngestionService
    {
        IngestionResponce IngestData(IngestionRequest request);

    }
}
