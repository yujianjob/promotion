using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.Model
{
    //Class_Detail
    public class Class_Detail
    {
        public Class_Detail()
        { }
        #region ����
        /// <summary>
        /// Id
        /// </summary>		
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// Title
        /// </summary>		
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// TraningId
        /// </summary>		
        private int _traningid;
        public int TraningId
        {
            get { return _traningid; }
            set { _traningid = value; }
        }
        /// <summary>
        /// PlanId
        /// </summary>		
        private int _planid;
        public int PlanId
        {
            get { return _planid; }
            set { _planid = value; }
        }
        /// <summary>
        /// SignUpStartTime
        /// </summary>		
        private DateTime _signupstarttime;
        public DateTime SignUpStartTime
        {
            get { return _signupstarttime; }
            set { _signupstarttime = value; }
        }
        /// <summary>
        /// SignUpEndTime
        /// </summary>		
        private DateTime _signupendtime;
        public DateTime SignUpEndTime
        {
            get { return _signupendtime; }
            set { _signupendtime = value; }
        }
        /// <summary>
        /// OpenClassFrom
        /// </summary>		
        private DateTime _openclassfrom;
        public DateTime OpenClassFrom
        {
            get { return _openclassfrom; }
            set { _openclassfrom = value; }
        }
        /// <summary>
        /// OpenClassTo
        /// </summary>		
        private DateTime _openclassto;
        public DateTime OpenClassTo
        {
            get { return _openclassto; }
            set { _openclassto = value; }
        }
        /// <summary>
        /// ClassForm
        /// </summary>		
        private int _classform;
        public int ClassForm
        {
            get { return _classform; }
            set { _classform = value; }
        }
        /// <summary>
        /// People
        /// </summary>		
        private int _people;
        public int People
        {
            get { return _people; }
            set { _people = value; }
        }
        /// <summary>
        /// LimitPeopleCnt
        /// </summary>		
        private int _limitpeoplecnt;
        public int LimitPeopleCnt
        {
            get { return _limitpeoplecnt; }
            set { _limitpeoplecnt = value; }
        }
        /// <summary>
        /// Address
        /// </summary>		
        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        /// <summary>
        /// Content
        /// </summary>		
        private string _content;
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
        /// <summary>
        /// ManagerId
        /// </summary>		
        private int _managerid;
        public int ManagerId
        {
            get { return _managerid; }
            set { _managerid = value; }
        }
        /// <summary>
        /// OrganId
        /// </summary>		
        private int _organid;
        public int OrganId
        {
            get { return _organid; }
            set { _organid = value; }
        }
        /// <summary>
        /// Subject
        /// </summary>		
        private bool _subject;
        public bool Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }
        /// <summary>
        /// StudyLevel
        /// </summary>		
        private bool _studylevel;
        public bool StudyLevel
        {
            get { return _studylevel; }
            set { _studylevel = value; }
        }
        /// <summary>
        /// TeachGrade
        /// </summary>		
        private bool _teachgrade;
        public bool TeachGrade
        {
            get { return _teachgrade; }
            set { _teachgrade = value; }
        }
        /// <summary>
        /// TeachRank
        /// </summary>		
        private bool _teachrank;
        public bool TeachRank
        {
            get { return _teachrank; }
            set { _teachrank = value; }
        }
        /// <summary>
        /// OrganRange
        /// </summary>		
        private string _organrange;
        public string OrganRange
        {
            get { return _organrange; }
            set { _organrange = value; }
        }
        /// <summary>
        /// Instructor
        /// </summary>		
        private int _instructor;
        public int Instructor
        {
            get { return _instructor; }
            set { _instructor = value; }
        }
        /// <summary>
        /// Status
        /// </summary>		
        private int _status;
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// ApplyRemark
        /// </summary>		
        private string _applyremark;
        public string ApplyRemark
        {
            get { return _applyremark; }
            set { _applyremark = value; }
        }
        /// <summary>
        /// Display
        /// </summary>		
        private bool _display;
        public bool Display
        {
            get { return _display; }
            set { _display = value; }
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
        /// <summary>
        /// PartitionId
        /// </summary>		
        private int _partitionid;
        public int PartitionId
        {
            get { return _partitionid; }
            set { _partitionid = value; }
        }
        #endregion
    }
}