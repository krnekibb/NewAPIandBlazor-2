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

        [HttpGet]
        public async Task<IList<Enrollment>> GetAllEnrollments()
        {
            return await context.Enrollments
                .Include(s => s.Student)
                .Include(c => c.Course)
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<Enrollment> GetEnrollment(int id)
        {
            return await context.Enrollments
                .Include(s => s.Student)
                .Include(c => c.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EnrollmentId == id);
        }
    }
}
