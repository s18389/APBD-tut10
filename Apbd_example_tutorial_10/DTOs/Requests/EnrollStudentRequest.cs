﻿using Apbd_example_tutorial_10.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apbd_example_tutorial_10.DTOs.Requests
{
    public class EnrollStudentRequest
    {
        public Enrollment enrollment { get; set; }
        public string studentIndex { get;  set; }
    }
}
