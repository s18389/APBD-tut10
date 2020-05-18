using Apbd_example_tutorial_10.DTOs.Requests;
using Apbd_example_tutorial_10.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apbd_example_tutorial_10.Services
{
    public interface IStudentService
    {

        ShowStudentsListResponse ShowListOfStudent();
        AddStudentResponse AddStudent(AddStudentRequest request);
        ModifyStudentResponse ModifyStudent(ModifyStudentRequest request);
        DeleteStudentResponse DeleteStudent(DeleteStudentRequest request);
        EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);
        PromoteStudentResponse PromoteStudent(PromoteStudentRequest request);
    }
}
