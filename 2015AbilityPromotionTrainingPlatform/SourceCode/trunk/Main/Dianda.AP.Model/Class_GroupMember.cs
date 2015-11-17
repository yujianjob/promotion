using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.Model
{
    //Class_GroupMember
    public class Class_GroupMember
    {
        public Class_GroupMember()
        { }
        #region 属性
        /// <summary>
        /// GroupId
        /// </summary>		
        private int _groupid;
        public int GroupId
        {
            get { return _groupid; }
            set { _groupid = value; }
        }
        /// <summary>
        /// AccountId
        /// </summary>		
        private int _accountid;
        public int AccountId
        {
            get { return _accountid; }
            set { _accountid = value; }
        }
        /// <summary>
        /// Delflag
        /// </summary>		
        private bool _delflag;
        public bool Delflag
        {
            get { return _delflag; }
            set { _delflag = value; }
        }
        /// <summary>
        /// CreateDate
        /// </summary>		
        private DateTime _createdate;
        public DateTime CreateDate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }
        #endregion
    }
}