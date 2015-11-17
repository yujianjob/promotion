using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXW.Models
{
    [Serializable]
    public class MgrInfoModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RealName { get; set; }
        public int Status { get; set; }
        public int GroupID { get; set; }
        public int Level { get; set; }
        public int RegionID { get; set; }

        public string LogIP { get; set; }

        public string Password { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public bool DelFlag { get; set; }
        public DateTime Createdate { get; set; }
    }
}
