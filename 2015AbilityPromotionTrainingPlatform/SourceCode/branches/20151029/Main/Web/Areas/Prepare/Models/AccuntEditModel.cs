using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.Prepare.Models
{
    public class AccuntEditModel
    {
        private int _AccountId;
        /// <summary>
        /// 用户编号
        /// </summary>
        public int AccountId
        {
            get { return _AccountId; }
            set { _AccountId = value; }
        }

        private string _UserName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private string _TeacherNo;
        /// <summary>
        /// 师训编号
        /// </summary>
        public string TeacherNo
        {
            get { return _TeacherNo; }
            set { _TeacherNo = value; }
        }

        private string _Email;
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private int _Sex;
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex
        {
            get { return _Sex; }
            set { _Sex = value; }
        }

        private DateTime _Birthday;
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday
        {
            get { return _Birthday; }
            set { _Birthday = value; }
        }

        private string _CredentialsNumber;
        /// <summary>
        /// 身份证号
        /// </summary>
        public string CredentialsNumber
        {
            get { return _CredentialsNumber; }
            set { _CredentialsNumber = value; }
        }

        private string _Mobile;
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }

        private int _Nation;
        /// <summary>
        /// 民族
        /// </summary>
        public int Nation
        {
            get { return _Nation; }
            set { _Nation = value; }
        }

        private int _PoliticalStatus;
        /// <summary>
        /// 政治面貌
        /// </summary>
        public int PoliticalStatus
        {
            get { return _PoliticalStatus; }
            set { _PoliticalStatus = value; }
        }
    }
}