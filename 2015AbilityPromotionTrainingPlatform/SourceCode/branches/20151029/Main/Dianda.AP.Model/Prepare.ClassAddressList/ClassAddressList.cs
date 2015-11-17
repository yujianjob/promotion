using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.Model.Prepare.ClassAddressList
{
    public class AddressList
    {
        public string RealName { get; set; }

        //会员类型:1.讲师 2.辅导员 3.学员
        public int MemberType { get; set; }

        public string GraduateSchool { get; set; }

        public string Title { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string Pic { get; set; }
    }
}
