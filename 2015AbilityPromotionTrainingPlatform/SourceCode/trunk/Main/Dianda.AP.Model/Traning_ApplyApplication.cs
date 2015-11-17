using System;

namespace Dianda.AP.Model
{
	public partial class Traning_ApplyApplication
	{
		public int Id { get; set; }

		public int TraningId { get; set; }

		public int Status { get; set; }

		public string Remark { get; set; }

		public int? Creater { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

		public int? OrganId { get; set; }

	}
}
