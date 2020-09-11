using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SourceSystemConnectorContracts.Contract;
using SourceSystemConnectorLogic.Services;

namespace SourceSystemConnectorApi.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class SourceSystemsController : ControllerBase
    {
        private readonly ISourceSystemConnectorService _sourceSystemConnectorService;

        public SourceSystemsController(ISourceSystemConnectorService sourceSystemConnectorService)
        {
            _sourceSystemConnectorService = sourceSystemConnectorService;
        }

        [HttpGet]
        public async Task<IEnumerable<SourceSystemContract>> GetSourceSystems()
        {
            return await _sourceSystemConnectorService.GetSourceSystems();
        }
    }
}
