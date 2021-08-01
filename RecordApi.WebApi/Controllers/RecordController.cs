using System;
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
    [Route("api/records")]
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
        [ProducesResponseType(typeof(IEnumerable<RecordDto>[]), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        public IEnumerable<RecordDto> Get([SwaggerParameter("Last Name ", Required = false)]
            string name = "")
        {
            if (name == null || name.Equals(string.Empty))
            {
                
                return _fileProcessor.Records.Select(r=> new RecordDto
                {
                    LastName  = r.LastName,
                    FirstName = r.FirstName,
                    Email = r.Email,
                    FavoriteColor = r.FavoriteColor,
                    DateOfBirth = r.DateOfBirth.ToString("d")
                });
            }
            else
            {
                return  new List<RecordDto>
                {

                    _fileProcessor.Records.Select(selector: r=> new RecordDto
                    {
                        LastName  = r.LastName,
                        FirstName = r.FirstName,
                        Email = r.Email,
                        FavoriteColor = r.FavoriteColor,
                        DateOfBirth = r.DateOfBirth.ToString("d")
                    }).FirstOrDefault(r => string.Equals(r.LastName, name, StringComparison.InvariantCultureIgnoreCase))
                };

               
            }
        }

        [HttpGet]
        [Route("color")]
        [SwaggerOperation(OperationId = "get-records-color", Summary = "Get All Records, Sorted By Color.")]
        [ProducesResponseType(typeof(IEnumerable<RecordDto>[]), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        public IEnumerable<RecordDto> GetByDob()
        {
            return _fileProcessor.Records.Select(selector: r => new RecordDto
            {
                LastName = r.LastName,
                FirstName = r.FirstName,
                Email = r.Email,
                FavoriteColor = r.FavoriteColor,
                DateOfBirth = r.DateOfBirth.ToString("d")
            }).OrderBy(r => r.FavoriteColor);
        }

        [HttpGet]
        [Route("birthdate")]
        [SwaggerOperation(OperationId = "get-records-dob", Summary = "Get All Records, Sorted By DOB.")]
        [ProducesResponseType(typeof(IEnumerable<RecordDto>[]), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        public IEnumerable<RecordDto> GetByColor()
        {
            var records = _fileProcessor.Records.OrderBy(r => r.DateOfBirth);
            
           return  records.Select(selector: r => new RecordDto
            {
                LastName = r.LastName,
                FirstName = r.FirstName,
                Email = r.Email,
                FavoriteColor = r.FavoriteColor,
                DateOfBirth = r.DateOfBirth.ToString("d")
            });
        }

        [HttpGet]
        [Route("name")]
        [SwaggerOperation(OperationId = "get-records-name", Summary = "Get All Records, Sorted By Last NAme.")]
        [ProducesResponseType(typeof(IEnumerable<RecordDto>[]), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        public IEnumerable<RecordDto> GetByName()
        {
            return _fileProcessor.Records.Select(selector: r => new RecordDto
            {
                LastName = r.LastName,
                FirstName = r.FirstName,
                Email = r.Email,
                FavoriteColor = r.FavoriteColor,
                DateOfBirth = r.DateOfBirth.ToString("d")
            }).OrderBy(r => r.LastName);
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(RecordDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(OperationId = "add-record", Summary = "Add a new Record")]
        public ActionResult<IRecord> Add([FromBody][Required(ErrorMessage = "Record is required.")] RecordDto record, [FromQuery]char delimiter = '*')
        {
            if (!DateTimeOffset.TryParse(record.DateOfBirth, out var dob)) return BadRequest(record);
            
            var ret = _fileProcessor.AddRecord(new Record
            {
                LastName = record.LastName,
                FirstName = record.FirstName,
                Email = record.Email,
                FavoriteColor = record.FavoriteColor,
                DateOfBirth = dob
            }, delimiter);
            
            return Created($"records?name={ret.LastName.ToLowerInvariant()}", ret);


        }
    }
}