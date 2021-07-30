using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecordApi.Shared.Model;
using RecordApi.Shared.Services;

namespace RecordApi.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecordController : ControllerBase
    {
       
        private readonly ILogger<RecordController> _logger;
        private readonly IFileProcessor _fileProcessor;

        public RecordController(ILogger<RecordController> logger, IFileProcessor fileProcessor)
        {
            _logger = logger;
            _fileProcessor = fileProcessor;
        }

        [HttpGet]
        public IEnumerable<IRecord> Get()
        {
            return _fileProcessor.Records.ToArray();
        }
    }
}
