using APIstuff.DataAccess;
using APIstuff.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIstuff.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ApiDbContext context;

        public CourseController(ApiDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IList<Course>> GetAllStudents()
        {
            return await context.Courses
                .Include(e => e.Enrollments)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
