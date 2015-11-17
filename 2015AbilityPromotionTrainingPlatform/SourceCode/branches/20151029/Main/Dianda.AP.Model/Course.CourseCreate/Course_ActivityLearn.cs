using System;

namespace Dianda.AP.Model.Course.CourseCreate
{
    public partial class Course_ActivityLearn
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UnitType { get; set; }
        public int TimeLength { get; set; }
        public int TestCnt { get; set; }
        public bool Display { get; set; }
        public bool Status { get; set; }
        public bool FinalTestLimit { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime EndDate { get; set; }
	}
}
