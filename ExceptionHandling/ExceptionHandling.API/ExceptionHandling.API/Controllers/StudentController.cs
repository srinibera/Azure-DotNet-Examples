using Microsoft.AspNetCore.Mvc;
using System;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExceptionHandling.API.Filters;
using ExceptionHandling.API.Models;
using ExceptionHandling.API.ExceptionHandlers;
using ExceptionHandling.API.Commands.Students;

namespace ExceptionHandling.API.Controllers
{

    [ApplicationExceptionFilter]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IMediator _mediator = null;
        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [UnifyResponseFilter]
        public Task<IActionResult> GetStudents()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudent createStudent)
        {
            var result=await _mediator.Send(createStudent);
            return Ok(result);
        }
    }
}
