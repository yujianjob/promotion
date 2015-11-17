using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianda.AP.Model;
using Dianda.AP.DAL;
using System.Data;

namespace Dianda.AP.BLL
{
    public class PlanExemptionBLL
    {
        PlanExemptionDAL dal = new PlanExemptionDAL();

        public List<PlanExemption> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, where, orderBy, out recordCount);
        }

        public DataTable GetOrgan(int ParentId)
        {
            return dal.GetOrgan(ParentId);
        }

        public int GetAccount(string RealName,string TeacherNo,string OrganTitle)
        {
            return dal.GetAccount(RealName,TeacherNo,OrganTitle);
        }

        public int AddExem(List<PlanExemption> list)
        {
            return dal.AddExem(list);
        }

        public bool DelExem(int id)
        {
            return dal.DelExem(id);
        }

        public PlanExemption GetModel(int id)
        {
            return dal.GetModel(id);
        }

        public bool ExemEdit(PlanExemption model)
        {
            return dal.ExemEdit(model);
        }

        public int GetTeacherNo(string TeacherNo)
        {
            return dal.GetTeacherNo(TeacherNo);
        }
        public DataTable GetOrganqu(int OrganId)
        {
            return dal.GetOrganqu(OrganId);
        }
        public bool GetSchool(string SchoolName, int OrganId)
        {
            return dal.GetSchool(SchoolName, OrganId);
        }
        public bool GetSchoolTo(string SchoolName, int OrganId)
        {
            return dal.GetSchoolTo(SchoolName, OrganId);
        }
    }
}
