using System;

namespace Dianda.AP.Model
{
	public partial class Member_ClassContentNote
	{
		public int Id { get; set; }

		public int ClassId { get; set; }

		public int? TrainingId { get; set; }

		public int AccountId { get; set; }

		public int UnitContent { get; set; }

		public string Content { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

        public string CreateTime
        {
            get
            {
                return CreateDate.ToString("yyyy-MM-dd HH:mm");
            }
        }
	}
}
