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
    public class StudentController : ControllerBase
    {
        private readonly ApiDbContext context;

        public StudentController(ApiDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IList<Student>> GetAllStudents()
        {
            var students =  await context.Students
                .AsNoTracking()
                .ToListAsync();

            return students;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<Student> GetStudent(int id)
        {
            return await context.Students
                .Include(s => s.Enrollments)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.StudentId == id);
        }
    }
}
