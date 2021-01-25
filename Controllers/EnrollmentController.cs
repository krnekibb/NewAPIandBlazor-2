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

        [HttpPost]
        public async Task<ActionResult<Enrollment>> CreateStudentEnrollment(Enrollment newEnrollment)
        {
            try
            {
                if (newEnrollment == null)
                {
                    return BadRequest();
                }

                var tempEnrollment = await context.Enrollments
                    .Where(e => e.StudentId == newEnrollment.StudentId)
                    .FirstOrDefaultAsync();

                if (tempEnrollment != null)
                {
                    ModelState.AddModelError("StudentId", "Student Id already in use!");
                    return BadRequest(ModelState);
                }

                var createStudentEnrollment = await context.Enrollments.AddAsync(newEnrollment);
                await context.SaveChangesAsync();
                return createStudentEnrollment.Entity;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error receiving data from database");
            }
        }

        //ne mores preko enrollment deletat Student ali pa Course rabis loceno
        [HttpDelete]
        public async Task<ActionResult<Enrollment>> DeleteEnrollment(int id)
        {
            try
            {
                var tempEnrollment = await context.Enrollments
                    .Include(s => s.Student)
                    //.Include(c => c.Course)
                    .FirstOrDefaultAsync(e => e.StudentId == id);

                if (tempEnrollment == null)
                {
                    return BadRequest(); 
                }

                context.Enrollments.Remove(tempEnrollment);
                await context.SaveChangesAsync();

                return tempEnrollment;
            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error receiving data from database");
            }

        } 
    }
}
