using System;

namespace Dianda.AP.Model
{
	public partial class Class_AddressList
	{
		public int Id { get; set; }

		public int ClassId { get; set; }

		public int AccountId { get; set; }

		public string Tel { get; set; }

		public string Email { get; set; }

		public bool Display { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
