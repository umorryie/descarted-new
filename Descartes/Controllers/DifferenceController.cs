using Descartes.DifferenceDeterminator;
using Descartes.Entities;
using Descartes.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceStack.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Descartes.Controllers
{
    [ApiController]
    [Route("v1/diff/")]
    public class DifferenceController : ControllerBase
    {
        private IDifferenceRepository _differenceRepository;

        public DifferenceController(IDifferenceRepository differenceRepository)
        {
            _differenceRepository = differenceRepository;
        }

        [HttpPut]
        [Route("{id}/left")]
        public IActionResult SaveLeftPartOfEquation([FromBody] RequestDifferenceInputHelper requestInput, int id)
        {
            if(requestInput.data == null)
            {
                return BadRequest();
            }

            _differenceRepository.SaveObject("left", requestInput, id);

            return Ok();
        }

        [HttpPut]
        [Route("{id}/right")]
        public IActionResult SaveRightPartOfEquation([FromBody] RequestDifferenceInputHelper requestInput, int id)
        {
            if (requestInput.data == null)
            {
                return BadRequest();
            }

            _differenceRepository.SaveObject("right", requestInput, id);

            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetDifferences(int id)
        {
            string differences = _differenceRepository.DetermineDifferences(id);

            if(differences == null)
            {
                return NotFound();
            }

            var objectDifferences = JsonConvert.DeserializeObject<DifferenceResponse>(differences);

            return Ok(objectDifferences);
        }

        [HttpGet]
        [Route("getAllDatabaseContent")]
        public IActionResult GetAllDatabaseContent()
        {
            try
            {
                var allDbContent = _differenceRepository.GetAllDatabaseContent();

                return Ok(allDbContent);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }
    }

    public class RequestDifferenceInputHelper
    {
        public string data { get; set; }
    }
}
