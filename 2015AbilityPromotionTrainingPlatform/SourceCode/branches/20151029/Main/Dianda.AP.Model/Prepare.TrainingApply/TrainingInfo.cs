using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianda.AP.Model;

namespace Dianda.AP.Model.Prepare.TrainingApply
{
    public class TrainingInfo : Traning_Detail
    {
        public string OrganName { get; set; }

        public string SubjectName { get; set; }

        public int Trainers { get; set; }

        public string TeacherTitleName { get; set; }
    }
}
