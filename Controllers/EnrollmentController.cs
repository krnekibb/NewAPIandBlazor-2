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
    public class EnrollmentController : ControllerBase
    {
        private readonly ApiDbContext context;

        public EnrollmentController(ApiDbContext context)
        {
            this.context = context;
        }

        #region Enrollments
        [HttpGet]
        public async Task<IList<Enrollment>> GetAllEnrollments()
        {
            return await context.Enrollments
                .Include(s => s.Student)
                .Include(c => c.Course)
                .AsNoTracking()
                .ToListAsync();
        }
        #endregion

        #region All Enrollments for specific student
        [HttpGet]
        [Route("student/{id:int}")]
        public async Task<IList<Enrollment>> GetEnrollmentBySID(int id)
        {
            return await context.Enrollments
                .Include(s => s.Student)
                    .Where(sid => sid.StudentId == id)
                .Include(c => c.Course)
                .AsNoTracking()
                .ToListAsync(); 
        }
        #endregion

        #region All Enrollments for specific course
        [HttpGet]
        [Route("course/{id:int}")]
        public async Task<IList<Enrollment>> GetEnrollmentByCID(int id)
        {
            return await context.Enrollments
                .Include(c => c.Course)
                    .Where(cid => cid.CourseId == id)
                .Include(s => s.Student)
                .AsNoTracking()
                .ToListAsync();
        }
        #endregion

        #region Get specific student via Enrollment
        [HttpGet]
        [Route("specificstudent/{id:int}")]
        public async Task<Enrollment> GetStudentFromEnrollment(int id)
        {
            return await context.Enrollments
                .Include(s => s.Student)
                    .Where(sid => sid.StudentId == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
        #endregion

    }
}
