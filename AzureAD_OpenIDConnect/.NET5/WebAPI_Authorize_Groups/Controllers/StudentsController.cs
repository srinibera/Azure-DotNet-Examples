using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Authorize_Groups.Models;

namespace WebAPI_Authorize_Groups.Controllers
{
    [Authorize(Policy = "GroupDataReader")]
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private IMemoryCache _cache;

        public StudentsController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        [HttpGet]
        public List<Student> List()
        {
            var userclaims = this.User.Claims;
            var studentList = (List<Student>)_cache.Get(nameof(Student));
            if (studentList == null)
            {
                studentList = new List<Student>();
                studentList.Add(new Student() { Id = 1, Name="student1",Location="India" });
            }
            return studentList;
        }
        
        
        [Authorize(Policy = "GroupDataWriter")]
        [HttpPost]
        public List<Student> Create([FromBody]Student student)
        {
            var studentList = (List<Student>)_cache.Get(nameof(Student));
            if (studentList != null)
            {
                studentList.Add(student);
            }
            else
            {
                studentList = new List<Student>();
                studentList.Add(student);                
            }

            _cache.Set(nameof(Student), studentList);
            return studentList;
        }
    }
}
