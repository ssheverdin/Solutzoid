using SourceSystemConnectorContracts.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SourceSystemConnectorLogic.Services
{
    public interface ISourceSystemConnectorService
    {
        Task<IEnumerable<SourceSystemContract>> GetSourceSystems();
    }
}
