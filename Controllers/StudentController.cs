using APIstuff.DataAccess;
using APIstuff.Models;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<IList<Student>>> GetAllStudents()
        {
            try
            {
                var students = await context.Students
                .AsNoTracking()
                .ToListAsync();

                if (students == null)
                {
                    return NotFound();
                }
                return Ok(students);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error receiving data from database");
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                var result = await context.Students
                    .Include(s => s.Enrollments)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.StudentId == id);

                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error receiving data from database");
            }
        }
    }
}