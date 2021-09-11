using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppInsights.Logging.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private readonly ILogger<StudentsController> _logger;
        
        public StudentsController(ILogger<StudentsController> logger)
        {
            _logger = logger;            
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetStudent(int id)
        {
            //Azure portal, run query on logs and select "traces" for found all the logs based on severity
            _logger.LogWarning($"studnet {id} not found");            
            _logger.LogError($"No database implementation");            
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetStudents()
        {            
            //Un handled exception can tracked on application insights
            throw new ApplicationException("Error in getting student record.");
        }

        [HttpGet]
        [Route("Results")]
        public IActionResult GetResults([FromServices] TelemetryClient telemetryClient)
        {            
            //Custom tracking
            telemetryClient.TrackMetric("Score", 70);
            return Ok();
        }
    }
}
