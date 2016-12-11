using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using RecordParse.Shared;
using RecordParse.Shared.Interfaces;
using RecordParse.Shared.Model;

namespace RecordParse.API.Controllers
{
    [RoutePrefix("records")]
    public class RecordsController : ApiController
    {
        private readonly IPersonService _personService;

        public RecordsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        [Route("gender")]
        public IHttpActionResult GetByGenderAndLastName()
        {
            return Ok(_personService.GetByDateGender());
        }

        [HttpGet]
        [Route("birthdate")]
        public IHttpActionResult GetByBirtdate()
        {
            return Ok(_personService.GetByBirthdate());
        }

        [HttpGet]
        [Route("name")]
        public IHttpActionResult GetByName()
        {
            return Ok(_personService.GetByName());
        }
    }
}