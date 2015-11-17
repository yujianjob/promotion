using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.Model
{
    public class AccountEditModel
    {
        [DisplayName("用户编号")]
        public int AccountId { get; set; }

        [DisplayName("用户名")]
        public string UserName { get; set; }

        [DisplayName("用户名")]
        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(10, ErrorMessage = "用户名字数不能超过10")]
        public string RealName { get; set; }

        [DisplayName("师训编号")]
        [Required(ErrorMessage = "师训编号不能为空")]
        [StringLength(20, ErrorMessage = "师训编号字数不能超过20")]
        public string TeacherNo { get; set; }

        public string Pic { get; set; }

        [DisplayName("邮箱地址")]
        [Required(ErrorMessage = "邮箱地址不能为空")]
        public string Email { get; set; }

        [DisplayName("性别")]
        [Required(ErrorMessage = "请选择性别")]
        public int Sex { get; set; }

        [DisplayName("出生日期")]
        [Required(ErrorMessage = "出生日期不能为空")]
        public DateTime Birthday { get; set; }

        [DisplayName("身份证号")]
        [Required(ErrorMessage = "身份证号不能为空")]
        public string CredentialsNumber { get; set; }

        [DisplayName("手机号码")]
        [Required(ErrorMessage = "手机号码不能为空")]
        public string Mobile { get; set; }

        [DisplayName("民族")]
        [Required(ErrorMessage = "请选择民族")]
        public int Nation { get; set; }

        [DisplayName("政治面貌")]
        [Required(ErrorMessage = "请选择政治面貌")]
        public int PoliticalStatus { get; set; }

        [DisplayName("区县")]
        [Required(ErrorMessage = "请选择区县")]
        public string Region { get; set; }

        [DisplayName("学段")]
        [Required(ErrorMessage = "学段不能为空")]
        public string StudySection { get; set; }

        [DisplayName("学校")]
        [Required(ErrorMessage = "请选择学校")]
        public string Organ { get; set; }

        [DisplayName("职务")]
        [Required(ErrorMessage = "请选择职务")]
        public string Job { get; set; }

        [DisplayName("职称")]
        [Required(ErrorMessage = "请选择职称")]
        public string WorkRank { get; set; }

        [DisplayName("任教日期")]
        [Required(ErrorMessage = "任教日期不能为空")]
        public DateTime TeachDate { get; set; }

        [DisplayName("任教学段")]
        [Required(ErrorMessage = "任教学段不能为空")]
        public string TeachStudySection { get; set; }

        [DisplayName("任教学科")]
        [Required(ErrorMessage = "任教学科不能为空")]
        public string TeachSubject { get; set; }

        [DisplayName("任教年级")]
        [Required(ErrorMessage = "任教年级不能为空")]
        public string TeachGrade { get; set; }

        [DisplayName("培训类型")]
        [Required(ErrorMessage = "请选择培训类型")]
        public string TraningType { get; set; }

        [DisplayName("培训对象")]
        [Required(ErrorMessage = "请选择培训对象")]
        public string TraningObject { get; set; }

        [DisplayName("学历")]
        [Required(ErrorMessage = "请选择学历")]
        public int EduLevel { get; set; }

        [DisplayName("学位")]
        [Required(ErrorMessage = "请选择学位")]
        public int EduDegree { get; set; }

        [DisplayName("专业")]
        [Required(ErrorMessage = "请选择专业")]
        public int EduMajor { get; set; }

        [DisplayName("专业-其他")]
        [Required(ErrorMessage = "专业-其他不能为空")]
        public string EduMajorOhter { get; set; }

        [DisplayName("毕业院校")]
        [Required(ErrorMessage = "毕业院校不能为空")]
        public string GraduateSchool { get; set; }

        [DisplayName("毕业时间")]
        [Required(ErrorMessage = "毕业时间不能为空")]
        public DateTime GraduateTime { get; set; }
    }


    public class AccountEdit {
        [DisplayName("用户编号")]
        public int AccountId { get; set; }

        [DisplayName("用户名")]
        public string UserName { get; set; }

        [DisplayName("用户名")]
        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(10, ErrorMessage = "用户名字数不能超过10")]
        public string RealName { get; set; }

        [DisplayName("师训编号")]
        [Required(ErrorMessage = "师训编号不能为空")]
        [StringLength(20, ErrorMessage = "师训编号字数不能超过20")]
        public string TeacherNo { get; set; }

        public string Pic { get; set; }

        [DisplayName("邮箱地址")]
        [Required(ErrorMessage = "邮箱地址不能为空")]
        public string Email { get; set; }

        [DisplayName("性别")]
        [Required(ErrorMessage = "请选择性别")]
        public int Sex { get; set; }

        [DisplayName("出生日期")]
        [Required(ErrorMessage = "出生日期不能为空")]
        public DateTime Birthday { get; set; }

        [DisplayName("身份证号")]
        [Required(ErrorMessage = "身份证号不能为空")]
        public string CredentialsNumber { get; set; }

        [DisplayName("手机号码")]
        [Required(ErrorMessage = "手机号码不能为空")]
        public string Mobile { get; set; }

        [DisplayName("民族")]
        [Required(ErrorMessage = "请选择民族")]
        public int Nation { get; set; }

        [DisplayName("政治面貌")]
        [Required(ErrorMessage = "请选择政治面貌")]
        public int PoliticalStatus { get; set; }

        [DisplayName("区县")]
        [Required(ErrorMessage = "请选择区县")]
        public int Region { get; set; }

        [DisplayName("学段")]
        [Required(ErrorMessage = "学段不能为空")]
        public string StudySection { get; set; }

        [DisplayName("学校")]
        [Required(ErrorMessage = "请选择学校")]
        public int Organ { get; set; }

        [DisplayName("职务")]
        [Required(ErrorMessage = "请选择职务")]
        public int Job { get; set; }

        [DisplayName("职称")]
        [Required(ErrorMessage = "请选择职称")]
        public int WorkRank { get; set; }

        [DisplayName("任教日期")]
        [Required(ErrorMessage = "任教日期不能为空")]
        public DateTime TeachDate { get; set; }

        [DisplayName("任教学段")]
        [Required(ErrorMessage = "任教学段不能为空")]
        public string TeachStudySection { get; set; }

        [DisplayName("任教学科")]
        [Required(ErrorMessage = "任教学科不能为空")]
        public string TeachSubject { get; set; }

        [DisplayName("任教年级")]
        [Required(ErrorMessage = "任教年级不能为空")]
        public string TeachGrade { get; set; }

        [DisplayName("培训类型")]
        [Required(ErrorMessage = "请选择培训类型")]
        public int TraningType { get; set; }

        [DisplayName("培训对象")]
        [Required(ErrorMessage = "请选择培训对象")]
        public int TraningObject { get; set; }

        [DisplayName("学历")]
        [Required(ErrorMessage = "请选择学历")]
        public int EduLevel { get; set; }

        [DisplayName("学位")]
        [Required(ErrorMessage = "请选择学位")]
        public int EduDegree { get; set; }

        [DisplayName("专业")]
        [Required(ErrorMessage = "请选择专业")]
        public int EduMajor { get; set; }

        [DisplayName("专业-其他")]
        [Required(ErrorMessage = "专业-其他不能为空")]
        public string EduMajorOhter { get; set; }

        [DisplayName("毕业院校")]
        [Required(ErrorMessage = "毕业院校不能为空")]
        public string GraduateSchool { get; set; }

        [DisplayName("毕业时间")]
        [Required(ErrorMessage = "毕业时间不能为空")]
        public DateTime GraduateTime { get; set; }
    }
}
