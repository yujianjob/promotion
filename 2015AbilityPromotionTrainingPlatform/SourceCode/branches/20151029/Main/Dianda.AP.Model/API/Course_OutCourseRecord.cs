﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.Model.API
{
    public class Course_OutCourseRecord
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AssessmentName { get; set; }

        public int ClassId { get; set; }

        public int? TrainingId { get; set; }

        public int AccountId { get; set; }

        public int DataType { get; set; }

        public double? learningTime { get; set; }

        public int? Role { get; set; }

        public bool? IsPassed { get; set; }

        public string Remark { get; set; }

        public double? Score { get; set; }

        public bool Status { get; set; }

        public bool Delflag { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
