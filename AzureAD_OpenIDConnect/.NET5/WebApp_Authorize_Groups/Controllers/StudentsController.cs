using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Authorize_Groups.Models;

namespace WebApp_Authorize_Groups.Controllers
{
    [Authorize(Policy = "GroupDataReader")]    
    [Route("[controller]")]
    public class StudentsController : Controller
    {
        private IMemoryCache _cache;
        private StudentService _studentService;

        public StudentsController(IMemoryCache memoryCache, StudentService studentService)
        {
            _cache = memoryCache;
            _studentService = studentService;
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var list = await _studentService.GetStudentsAsync();
            return await Task.Run(()=>View(list));
        }
                     
        [Authorize(Policy = "GroupDataWriter")]
        [HttpPost]
        public async Task<IActionResult> Update(Student student)
        {
            await _studentService.PostStudentAsync(student);
            var list = await _studentService.GetStudentsAsync();
            return await Task.Run(()=>View("GetStudents", list));
        }
    }
}
