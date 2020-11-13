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
        public HttpException saveLeft([FromBody] RequestDifferenceInputHelper requestInput, int id)
        {
            if(requestInput == null)
            {
                throw new HttpException(404, "Not Found");
            }

            _differenceRepository.saveObject("left", requestInput, id);

            return new HttpException(401, "Created");
        }

        [HttpPut]
        [Route("{id}/right")]
        public HttpException saveRight([FromBody] RequestDifferenceInputHelper requestInput, int id)
        {
            if (requestInput == null)
            {
                throw new HttpException(404, "Not Found");
            }

            _differenceRepository.saveObject("right", requestInput, id);

            return new HttpException(401, "Created");
        }

        [HttpGet]
        [Route("{id}")]
        public object getDifferences(int id)
        {
            string differences = _differenceRepository.determineDifferences(id);

            if(differences == null)
            {
                throw new HttpException(404, "Not Found");
            }

            return JsonConvert.DeserializeObject<DifferenceResponse>(differences);
        }

        [HttpGet]
        [Route("getAllDatabaseContent")]
        public List<DifferenceObject> getAllDatabaseContent()
        {
            return _differenceRepository.getAllDatabaseContent();
        }
    }

    public class RequestDifferenceInputHelper
    {
        public string data { get; set; }
    }
}
