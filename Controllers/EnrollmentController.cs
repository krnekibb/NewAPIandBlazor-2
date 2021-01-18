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
    public class EnrollmentController : ControllerBase
    {
        private readonly ApiDbContext context;

        public EnrollmentController(ApiDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Enrollment>>> GetAllEnrollments()
        {
            try
            {
                var result = await context.Enrollments
                .Include(s => s.Student)
                .Include(c => c.Course)
                .AsNoTracking()
                .ToListAsync();

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

        [HttpGet]
        [Route("studentid/{id:int}")]
        public async Task<ActionResult<IList<Enrollment>>> GetEnrollmentBySID(int id)
        {
            try
            {
                var result = await context.Enrollments
                .Include(s => s.Student)
                    .Where(sid => sid.StudentId == id)
                .Include(c => c.Course)
                .AsNoTracking()
                .ToListAsync();

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
