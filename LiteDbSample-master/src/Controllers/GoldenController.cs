using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDbSample.LiteDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LiteDbSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GoldenController : ControllerBase
    {
        private readonly ILogger<GoldenController> _logger;
        private readonly ILiteDbGoldenService _forecastDbService;

        public GoldenController(ILogger<GoldenController> logger, ILiteDbGoldenService forecastDbService)
        {
            _forecastDbService = forecastDbService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Golden> Get()
        {
            return _forecastDbService.FindAll();
        }

        [HttpGet("{id}", Name = "FindOne")]
        public ActionResult<Golden> Get(int id)
        {
            var result = _forecastDbService.FindOne(id);
            if (result != default)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpPost]
        public ActionResult<Golden> Insert(Golden dto)
        {
            var id = _forecastDbService.Insert(dto);
            if (id != default)
                return CreatedAtRoute("FindOne", new { id = id }, dto);
            else
                return BadRequest();
        }

        [HttpPut]
        public ActionResult<Golden> Update(Golden dto)
        {
            var result = _forecastDbService.Update(dto);
            if (result)
                return NoContent();
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult<Golden> Delete(int id)
        {
            var result = _forecastDbService.Delete(id);
            if (result > 0)
                return NoContent();
            else
                return NotFound();
        }
    }
}
