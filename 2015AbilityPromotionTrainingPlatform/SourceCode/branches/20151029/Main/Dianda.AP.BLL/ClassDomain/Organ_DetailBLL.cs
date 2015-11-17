using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.BLL
{
    public partial class Organ_DetailBLL
    {
        public List<Dianda.AP.Model.Organ_Detail> GetShiOrganDetailList()
        {
            return DataTableToList( this.dal.GetShiOrganDetailList());
        }
    }
}
