using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.Model
{
    public class PlanExemption
    {
        public int Id { get; set; }

        public int PlanId { get; set; }

        public int Status { get; set; }

        public string Remark { get; set; }

        public double? Credits { get; set; }

        public int? PEType { get; set; }

        public int? Creater { get; set; }

        public int? AccountId { get; set; }

        public bool Delflag { get; set; }

        public DateTime CreateDate { get; set; }

        public string TeacherNo { get; set; }

        public string RealName { get; set; }

        public int Organid { get; set; }

        public string OrganTitle { get; set; }
    }
}
