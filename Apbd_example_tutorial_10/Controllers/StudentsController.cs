using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Apbd_example_tutorial_10.DTOs.Requests;
using Apbd_example_tutorial_10.Entities;
using Apbd_example_tutorial_10.Models;
using Apbd_example_tutorial_10.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apbd_example_tutorial_10.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IStudentService _serviceStudent;

        public StudentsController(IStudentService service)
        {
            _serviceStudent = service;
        }

        [HttpGet("list")]
        public IActionResult GetStudents(ShowStudentsListRequest request)
        {
            var response = _serviceStudent.ShowListOfStudent();
            return Ok(response.listOfStudents);
        }

        [HttpGet("addStudent")]
        public IActionResult AddStudents(AddStudentRequest request)
        {
            Student newStudent = new Student();
            newStudent.IndexNumber = "2";
            newStudent.FirstName = "Marek";
            newStudent.LastName = "Unkknow";
            newStudent.BirthDate = new DateTime(2000, 1, 1);
            newStudent.IdEnrollment = 1;
            request.student = newStudent;

            var response = _serviceStudent.AddStudent(request);
            return Ok(response);
        }

        [HttpGet("modify")]
        public IActionResult ModifyStudents(ModifyStudentRequest request)
        {
            Student modifyStudent = new Student();
            modifyStudent.IndexNumber = "2";
            modifyStudent.FirstName = "Marek";
            modifyStudent.LastName = "UNKNOWMODIFIED";
            modifyStudent.BirthDate = new DateTime(2000, 1, 1);
            modifyStudent.IdEnrollment = 1;
            request.student = modifyStudent;

            var response = _serviceStudent.ModifyStudent(request);
            return Ok(response.message);
        }

        [HttpGet("delete")]
        public IActionResult DeleteStudents(DeleteStudentRequest request)
        {
            request.indexNumber = "2";

            var response = _serviceStudent.DeleteStudent(request);
            return Ok(response.message);
        }

        [HttpGet("enroll")]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        { 
            Enrollment enroll = new Enrollment();
            enroll.IdStudy = 1;
            enroll.Semester = 1;
            enroll.StartDate = new DateTime(2021, 1, 1);
            request.enrollment = enroll;
            request.studentIndex = "1";
            
            var response = _serviceStudent.EnrollStudent(request);
            return Ok(response.message);
        }

        [HttpGet("promote")]
        public IActionResult PromoteStudent(PromoteStudentRequest request)
        {
            string indexNumberToPromote = "1";
            request.indexNumberToPromote = indexNumberToPromote;

            var response = _serviceStudent.PromoteStudent(request);
            return Ok(response.message);
        }

    }
}