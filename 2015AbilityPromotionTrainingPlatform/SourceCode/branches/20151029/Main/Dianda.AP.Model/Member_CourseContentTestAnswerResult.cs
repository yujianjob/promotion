using System;

namespace Dianda.AP.Model
{
	public partial class Member_CourseContentTestAnswerResult
	{
		public int Id { get; set; }

		public string Verson { get; set; }

		public int UnitContent { get; set; }

		public int ClassId { get; set; }

		public double Score { get; set; }

		public int? QuestionCnt { get; set; }

		public int? RightAnswer { get; set; }

		public int? WrongAnswer { get; set; }

		public bool Result { get; set; }

		public int AccountId { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}

    public partial class QuizAnswerResultOther
    {
        /// <summary>
        /// Course_UnitContent.UnitContent
        /// </summary>
        public int UnitContent { get; set; }
        /// <summary>
        /// Course_UnitContent.Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户做题的结果集合
        /// </summary>
        public string Answer { get; set; }
    }
}
