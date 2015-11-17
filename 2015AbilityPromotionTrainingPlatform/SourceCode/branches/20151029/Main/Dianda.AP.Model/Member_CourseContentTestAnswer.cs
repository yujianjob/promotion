using System;

namespace Dianda.AP.Model
{
	public partial class Member_CourseContentTestAnswer
	{
		public int Id { get; set; }

		public int AnswerResult { get; set; }

		public int Question { get; set; }

		public string Answer { get; set; }

		public double Credit { get; set; }

		public bool Result { get; set; }

		public int AccountId { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}

    public partial class Member_CourseContentTestAnswerOther
    {
        /// <summary>
        /// Class_TraningCommentQuestion Id
        /// 题目Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Course_UnitContent Id
        /// 标题Id
        /// </summary>
        public int UnitContentId { get; set; }

        /// <summary>
        /// 用户做题的结果集合
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// 当前题选中项的分值集合
        /// </summary>
        public string Credits { get; set; }

        /// <summary>
        /// 题目版本号
        /// </summary>
        public string Verson { get; set; }

        /// <summary>
        /// 当前的班级Id
        /// </summary>
        public int ClassId { get; set; }

        /// <summary>
        /// 课程Id
        /// Class_Detail.TraningId
        /// </summary>
        public int TrainingId { get; set; }
    }
}
