using System;

namespace Dianda.AP.Model
{
	public partial class Member_ClassRegister
	{
		public int Id { get; set; }

		public int ClassId { get; set; }

		public int PlanId { get; set; }

		public int AccountId { get; set; }

		public int Status { get; set; }

		public string ApplyRemark { get; set; }

		public Guid BatchCode { get; set; }

		public int? ManagerId { get; set; }

		public bool IsPass { get; set; }

		public int CurrentSchedule { get; set; }

		public int TotalSchedule { get; set; }

		public double? ReadingScore { get; set; }

		public double? DiscussScore { get; set; }

		public double? HomeWorkScore { get; set; }

		public double? TestingScore { get; set; }

		public double? ExaminationScore { get; set; }

		public double? CommentScore { get; set; }

		public double? TotalScore { get; set; }

		public int? Result { get; set; }

		public int? ResultCreater { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

		public int? TrainingId { get; set; }

        public double? OtherScore { get; set; }
	}


    public partial class ClassRegisterManage : Member_ClassRegister
    {
        public string RealName { get; set; }

        public string Nickname { get; set; }

        public string UserName { get; set; }

        public string TeacherNo { get; set; }

        public string TTitle { get; set; }

        public string CTitle { get; set; }

        public DateTime SignUpStartTime { get; set; }

        public DateTime SignUpEndTime { get; set; }

        public DateTime OpenClassFrom { get; set; }

        public DateTime OpenClassTo { get; set; }

        public int CId { get; set; }

        public int People { get; set; }

        public int LimitPeopleCnt { get; set; }
        
    }


}