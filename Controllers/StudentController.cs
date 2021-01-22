﻿using APIstuff.DataAccess;
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
                        .ThenInclude(e => e.Course)
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

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent (Student newStudent)
        {
            try
            {
                if (newStudent == null)
                {
                    return BadRequest();
                }

                var tempStudent = await context.Students
                    .FirstOrDefaultAsync(s => s.StudentId == newStudent.StudentId);

                if (tempStudent != null)
                {
                    ModelState.AddModelError("studentId", "Student ID already in use");
                    return BadRequest(ModelState);
                }

                var createdStudent = await context.Students.AddAsync(newStudent);
                await context.SaveChangesAsync();

                return createdStudent.Entity;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        } 
    }
}