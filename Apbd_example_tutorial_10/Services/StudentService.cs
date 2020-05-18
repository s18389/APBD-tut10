using Apbd_example_tutorial_10.DTOs.Requests;
using Apbd_example_tutorial_10.DTOs.Responses;
using Apbd_example_tutorial_10.Entities;
using Apbd_example_tutorial_10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apbd_example_tutorial_10.Services
{
    public class StudentService : ControllerBase, IStudentService
    {

        public ShowStudentsListResponse ShowListOfStudent()
        {
            var response = new ShowStudentsListResponse();
            StudentContext studentContext = new StudentContext();

            response.listOfStudents = studentContext.Student.Include(s => s.IdEnrollmentNavigation).ThenInclude(e => e.IdStudyNavigation).Select(s => new GetStudentsResponse
            {
                IndexNumber = s.IndexNumber,
                FirstName = s.FirstName,
                LastName = s.LastName,
                BirthDate = s.BirthDate.ToShortDateString(),
                Semester = s.IdEnrollmentNavigation.Semester,
                Studies = s.IdEnrollmentNavigation.IdStudyNavigation.Name
            }).ToList();

            return response;
        }

        public AddStudentResponse AddStudent(AddStudentRequest request)
        {
            var response = new AddStudentResponse();

            using (var studentContext = new StudentContext())
            {
                studentContext.Student.Add(request.student);
                try
                {
                    studentContext.SaveChanges();
                    response.message = "INSERT SUCCESSFUL";
                }
                catch (Exception)
                {
                    response.message = "INSERT FAILED";
                }
            }
            return response;
        }

        public ModifyStudentResponse ModifyStudent(ModifyStudentRequest request)
        {
            var response = new ModifyStudentResponse();

            using (var studentContext = new StudentContext())
            {
                var entity = studentContext.Student.FirstOrDefault(item => item.IndexNumber == request.student.IndexNumber);
                if (entity != null)
                {
                    entity.FirstName = request.student.FirstName;
                    entity.LastName = request.student.LastName;
                    entity.BirthDate = request.student.BirthDate;
                    entity.IdEnrollment = request.student.IdEnrollment;

                    try
                    {
                        studentContext.SaveChanges();
                        response.message = "UPDATE SUCCESSFULL";
                    }
                    catch (Exception)
                    {
                        response.message = "UPDATE FAILED";
                    }
                }
                else
                {
                    response.message = "There is no studnet with this index!";
                }
            }
            return response;
        }

        public DeleteStudentResponse DeleteStudent(DeleteStudentRequest request)
        {
            var response = new DeleteStudentResponse();

            using (var studentContext = new StudentContext())
            {
                var studentToDelete = studentContext.Student.SingleOrDefault(student => student.IndexNumber.Equals(request.indexNumber));
                if (studentToDelete != null)
                {
                    try
                    {
                        studentContext.Student.Remove(studentToDelete);
                        studentContext.SaveChanges();
                        response.message = "Student " + request.indexNumber + " deleted successful";
                    }
                    catch (Exception)
                    {
                        response.message = "Student delete FAILED";
                    }
                }
                else
                {
                    response.message = "There is no such a student to delete!";
                }
            }
            return response;
        }

        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {
            var response = new EnrollStudentResponse();

            using (var studentContext = new StudentContext())
            {
                var entityStudent = studentContext.Student.FirstOrDefault(item => item.IndexNumber == request.studentIndex);
                var entityStudies = studentContext.Studies.FirstOrDefault(item => item.IdStudy == request.enrollment.IdStudy);
                if (entityStudent != null && entityStudies != null)
                {
                    int newID = studentContext.Enrollment.Max(e => e.IdEnrollment) + 1;
                    var idStudy = entityStudies.IdStudy;

                    request.enrollment.IdEnrollment = newID;

                    studentContext.Enrollment.Add(request.enrollment);
                    try
                    {
                        ModifyStudentRequest requestModifyStudent = new ModifyStudentRequest();
                        Student stunetToModify = new Student();
                        stunetToModify.IndexNumber = request.studentIndex;
                        stunetToModify.IdEnrollment = newID;
                        requestModifyStudent.student = stunetToModify;
                        var responseStudent = ModifyStudent(requestModifyStudent);
                        studentContext.SaveChanges();
                        response.message = "ENROLL SUCCESSFUL";
                    }
                    catch (Exception)
                    {
                        response.message = "ENROLL FAILED";
                    }
                }
                else
                {
                    response.message = "There is no studnet with this index!";
                }
            }

            return response;
        }

        public PromoteStudentResponse PromoteStudent(PromoteStudentRequest request)
        {
            var response = new PromoteStudentResponse();

            using (var studentContext = new StudentContext())
            {
                var entityStudent = studentContext.Student.FirstOrDefault(item => item.IndexNumber == request.indexNumberToPromote);
                var entityEnroll = studentContext.Enrollment.FirstOrDefault(item => item.IdEnrollment == entityStudent.IdEnrollment);

                if (entityStudent != null && entityEnroll != null)
                {
                    entityEnroll.Semester += 1;
                    try
                    {
                        studentContext.SaveChanges();
                        response.message = "Successful promote student";
                    }
                    catch (Exception)
                    {
                        response.message = "promote student failed";
                    }
                 
                }
                else
                {
                    response.message = "Cannot promote student that is not exist in enroll table!";
                }
            }
            return response;
        }
    }
}
