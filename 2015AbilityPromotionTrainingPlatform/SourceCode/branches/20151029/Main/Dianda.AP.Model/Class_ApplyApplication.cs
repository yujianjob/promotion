using System;

namespace Dianda.AP.Model
{
	public partial class Class_ApplyApplication
	{
		public int Id { get; set; }

		public int ClassId { get; set; }

		public int Status { get; set; }

		public string Remark { get; set; }

		public int? Creater { get; set; }

		public int? AccountId { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
