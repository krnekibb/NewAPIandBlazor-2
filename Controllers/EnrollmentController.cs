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
        [Route("allEnrollmentsStudentId/{id:int}")]
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

        [HttpGet]
        [Route("studentId/{id:int}")]
        public async Task<ActionResult<Enrollment>> GetSingleEnrollmentBySID(int id)
        {
            try
            {
                var result = await context.Enrollments
                .Include(s => s.Student)
                    .Where(sid => sid.StudentId == id)
                .Include(c => c.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync();

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

        [HttpPost]
        public async Task<ActionResult<Enrollment>> CreateEnrollment(Enrollment newEnrollment)
        {
            try
            {
                if (newEnrollment == null)
                {
                    return BadRequest();
                }

                var tempEnrollment = await context.Enrollments
                    .FirstOrDefaultAsync(s => s.StudentId == newEnrollment.StudentId);

                if (tempEnrollment != null)
                {
                    ModelState.AddModelError("studentId", "Student ID already in use");
                    return BadRequest(ModelState);
                }

                var createdEnrollment = await context.Enrollments.AddAsync(newEnrollment);
                await context.SaveChangesAsync();

                return createdEnrollment.Entity;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }
    }
}
