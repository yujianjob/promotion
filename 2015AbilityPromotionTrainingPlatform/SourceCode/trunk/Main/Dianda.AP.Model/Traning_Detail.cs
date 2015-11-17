using System;
using System.ComponentModel.DataAnnotations;

namespace Dianda.AP.Model
{
	public partial class Traning_Detail
	{
		public int Id { get; set; }

		public string Title { get; set; }

        [StringLength(4000)]
		public string Content { get; set; }

		public string Pic { get; set; }

		public int Creater { get; set; }

		public int? OrganId { get; set; }

		public int TraingField { get; set; }

		public int TraingCategory { get; set; }

		public int? TraingTopic { get; set; }

		public string TraningObject { get; set; }

		public string Subject { get; set; }

		public string StudyLevel { get; set; }

		public double? TotalTime { get; set; }

		public int? TrainingForm { get; set; }

		public int? TeacherTitle { get; set; }

		public string TeacherName { get; set; }

		public string TeacherFrom { get; set; }

		public string TeacherPic { get; set; }

        [StringLength(4000)]
		public string Outline { get; set; }

		public int PartitionId { get; set; }

		public int OutSideType { get; set; }

		public string OutSideLink { get; set; }

		public int Range { get; set; }

		public int Status { get; set; }

		public string ApplyRemark { get; set; }

		public bool Display { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

		public int? ParentOrganId { get; set; }

        [StringLength(4000)]
		public string OutSideContent { get; set; }

        public bool CanEdit { get; set; }

        public string NationalCoursId { get; set; }
	}
}
