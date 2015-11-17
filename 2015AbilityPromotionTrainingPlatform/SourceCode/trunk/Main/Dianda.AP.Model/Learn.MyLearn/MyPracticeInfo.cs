﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.Model.Learn.MyLearn
{
    public class MyPracticeInfo : PracticalCourse_Detail
    {
        public string AccountName { get; set; }

        public double Credits { get; set; }

        public int Status { get; set; }

        public int MpcId { get; set; }

        public int Creater { get; set; }

        public string CategoryName { get; set; }

        public string SubjectName { get; set; }

        public string RoleName { get; set; }

        public string NationTitle { get; set; }

        public string NationContent { get; set; }
    }
}