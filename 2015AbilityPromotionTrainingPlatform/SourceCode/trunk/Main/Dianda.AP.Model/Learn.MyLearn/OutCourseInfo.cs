using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.Model.Learn.MyLearn
{
    public class OutCourseInfo
    {
        public int Id { get; set; }

        public int OutSideType { get; set; }

        public string OutSideLink { get; set; }

        public string OutSideContent { get; set; }

        public string OutCourseTitle { get; set; }

        public string OutCourseLink { get; set; }

        public bool DisplayEnterBtn { get; set; }
    }
}
