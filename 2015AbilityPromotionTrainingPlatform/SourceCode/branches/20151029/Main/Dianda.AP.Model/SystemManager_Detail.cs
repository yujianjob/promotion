using System;

namespace Dianda.AP.Model
{
	public partial class SystemManager_Detail
	{
		public int Id { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public string Email { get; set; }

		public string RealName { get; set; }

		public string Mobile { get; set; }

		public string Address { get; set; }

		public int Status { get; set; }

		public int OrganId { get; set; }

		public int Level { get; set; }

		public int GroupId { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
