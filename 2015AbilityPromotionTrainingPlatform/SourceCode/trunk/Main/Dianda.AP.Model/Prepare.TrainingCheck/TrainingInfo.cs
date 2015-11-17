using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.Model.Prepare.TrainingCheck
{
    public class TrainingInfo : Traning_Detail
    {
        public string OrganName { get; set; }

        public string SubjectName { get; set; }

        public int Trainers { get; set; }

        public string TeacherTitleName { get; set; }
    }
}
