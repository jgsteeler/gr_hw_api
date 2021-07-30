using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecordApi.Shared.Model;
using RecordApi.Shared.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RecordApi.WebApi.Controllers
{
    [ApiController]
    [Route("records")]
    public class RecordController : ControllerBase
    {
        private readonly IFileProcessor _fileProcessor;

        private readonly ILogger<RecordController> _logger;

        public RecordController(ILogger<RecordController> logger, IFileProcessor fileProcessor)
        {
            _logger = logger;
            _fileProcessor = fileProcessor;
        }

        [HttpGet]
        [SwaggerOperation(OperationId = "get-records", Summary = "Get All Records.")]
        [ProducesResponseType(typeof(IEnumerable<IRecord>[]), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        public IEnumerable<IRecord> Get([SwaggerParameter("Last Name ", Required = false)]
            string name = "")
        {
            if (name == null || name.Equals(string.Empty))
                return _fileProcessor.Records.ToArray();
            else
                return new List<IRecord> {_fileProcessor.Records.FirstOrDefault(r => r.LastName.ToUpperInvariant() == name.ToUpperInvariant())};
        }

        [HttpGet]
        [Route("color")]
        [SwaggerOperation(OperationId = "get-records-color", Summary = "Get All Records, Sorted By Color.")]
        [ProducesResponseType(typeof(IEnumerable<IRecord>[]), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        public IEnumerable<IRecord> GetByDob()
        {
            return _fileProcessor.Records.OrderBy(r => r.FavoriteColor).ToArray();
        }

        [HttpGet]
        [Route("birthdate")]
        [SwaggerOperation(OperationId = "get-records-dob", Summary = "Get All Records, Sorted By DOB.")]
        [ProducesResponseType(typeof(IEnumerable<IRecord>[]), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        public IEnumerable<IRecord> GetByColor()
        {
            return _fileProcessor.Records.OrderBy(r => r.DateOfBirth).ToArray();
        }

        [HttpGet]
        [Route("name")]
        [SwaggerOperation(OperationId = "get-records-name", Summary = "Get All Records, Sorted By Last NAme.")]
        [ProducesResponseType(typeof(IEnumerable<IRecord>[]), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        public IEnumerable<IRecord> GetByName()
        {
            return _fileProcessor.Records.OrderBy(r => r.LastName).ToArray();
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(IRecord), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(OperationId = "add-record", Summary = "Add a new Record")]
        public ActionResult<IRecord> Add([FromBody][Required(ErrorMessage = "Record is required.")] Record record, [FromQuery]char delimiter = ',')
        {
           

            var ret = _fileProcessor.AddRecord(record, delimiter);
            return Created($"records?name={ret.LastName.ToLowerInvariant()}", ret);

        }
    }
}