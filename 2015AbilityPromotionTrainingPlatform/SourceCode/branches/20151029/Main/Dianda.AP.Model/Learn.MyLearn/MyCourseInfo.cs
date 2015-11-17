﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.Model.Learn.MyLearn
{
    public class MyCourseInfo
    {
        public int Id { get; set; }

        public int StatusMember { get; set; }

        public int StatusClass { get; set; }

        public int Result { get; set; }

        public int ClassId { get; set; }

        public string ClassName { get; set; }

        public int TrainingId { get; set; }

        public string TrainingName { get; set; }

        public int AccountId { get; set; }

        public string Pic { get; set; }

        public int OrganId { get; set; }

        public string OrganName { get; set; }

        public int CurrentSchedule { get; set; }

        public int TotalSchedule { get; set; }

        public DateTime OpenClassFrom { get; set; }

        public DateTime OpenClassTo { get; set; }

        public int FieldId { get; set; }

        public string FieldName { get; set; }

        public double TotalTime { get; set; }
    }
}
