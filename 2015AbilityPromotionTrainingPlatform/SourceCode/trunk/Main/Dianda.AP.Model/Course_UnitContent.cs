using System;

namespace Dianda.AP.Model
{
	public partial class Course_UnitContent
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

		public int UnitType { get; set; }

		public int? TestCnt { get; set; }

		public bool? PrintScore { get; set; }

		public int? TimeLimit { get; set; }

		public int? PassLine { get; set; }

		public bool? FinalTestLimit { get; set; }

		public int? UnitId { get; set; }

		public int? TimeLength { get; set; }

		public int? OpenTime { get; set; }

		public int? EndTime { get; set; }

		public bool Display { get; set; }

		public int Sort { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

        public int ContentType { get; set; }
	}
}
