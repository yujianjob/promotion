using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dianda.AP.Model
{
    public class UserModel
    {
        private int _UserId;

        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private string _PassWord;

        public string PassWord
        {
            get { return _PassWord; }
            set { _PassWord = value; }
        }

        private string _NickName;

        public string NickName
        {
            get { return _NickName; }
            set { _NickName = value; }
        }

        private int _Status;

        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private string _OrganTitle;

        public string OrganTitle
        {
            get { return _OrganTitle; }
            set { _OrganTitle = value; }
        }
    }
}