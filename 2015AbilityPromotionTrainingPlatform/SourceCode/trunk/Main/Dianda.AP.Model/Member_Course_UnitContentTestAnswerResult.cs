using System;

namespace Dianda.AP.Model
{
	public partial class Member_Course_UnitContentTestAnswerResult
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
}
