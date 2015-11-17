using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dianda.AP.Model
{
	public partial class Class_Notice
	{
		public int Id { get; set; }

        [DisplayName("�༶ID")]
        [Required(ErrorMessage = "�༶ID����Ϊ��")]
		public int ClassId { get; set; }

        [DisplayName("����")]
        [Required(ErrorMessage = "���ⲻ��Ϊ��")]
        [StringLength(30, ErrorMessage = "�����������ܳ���30")]
		public string Title { get; set; }

        [DisplayName("����")]
        [Required(ErrorMessage = "���ݲ���Ϊ��")]
        [StringLength(4000, ErrorMessage = "�����������ܳ���4000")]
		public string Content { get; set; }

        [DisplayName("������")]
        [Required(ErrorMessage = "�����˲���Ϊ��")]
		public int? Creater { get; set; }

        [DisplayName("�Ƿ���ʾ")]
		public bool Display { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

        public string OrganTitle { get; set; }
	}
}
