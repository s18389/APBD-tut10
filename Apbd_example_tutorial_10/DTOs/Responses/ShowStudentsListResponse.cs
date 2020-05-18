using Apbd_example_tutorial_10.Entities;
using Apbd_example_tutorial_10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apbd_example_tutorial_10.DTOs.Responses
{
    public class ShowStudentsListResponse
    {
        public List<GetStudentsResponse> listOfStudents { get; set; }
    }
}
