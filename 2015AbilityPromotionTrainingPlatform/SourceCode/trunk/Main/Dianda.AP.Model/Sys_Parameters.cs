using System;

namespace Dianda.AP.Model
{
	public partial class Sys_Parameters
	{
		public int id { get; set; }

		public string Range { get; set; }

		public string ParameterKey { get; set; }

		public string ParameterValue { get; set; }

		public string Remark { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
