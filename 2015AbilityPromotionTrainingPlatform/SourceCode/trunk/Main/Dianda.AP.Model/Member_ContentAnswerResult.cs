using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.Model
{
    //Member_ContentAnswerResult
    public class Member_ContentAnswerResult
    {
        public Member_ContentAnswerResult()
        { }
        #region 属性
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
        /// Verson
        /// </summary>		
        private string _verson;
        public string Verson
        {
            get { return _verson; }
            set { _verson = value; }
        }
        /// <summary>
        /// UnitContent
        /// </summary>		
        private int _unitcontent;
        public int UnitContent
        {
            get { return _unitcontent; }
            set { _unitcontent = value; }
        }
        /// <summary>
        /// ClassId
        /// </summary>		
        private int _classid;
        public int ClassId
        {
            get { return _classid; }
            set { _classid = value; }
        }
        /// <summary>
        /// Score
        /// </summary>		
        private decimal _score;
        public decimal Score
        {
            get { return _score; }
            set { _score = value; }
        }
        /// <summary>
        /// QuestionCnt
        /// </summary>		
        private int _questioncnt;
        public int QuestionCnt
        {
            get { return _questioncnt; }
            set { _questioncnt = value; }
        }
        /// <summary>
        /// RightAnswer
        /// </summary>		
        private int _rightanswer;
        public int RightAnswer
        {
            get { return _rightanswer; }
            set { _rightanswer = value; }
        }
        /// <summary>
        /// WrongAnswer
        /// </summary>		
        private int _wronganswer;
        public int WrongAnswer
        {
            get { return _wronganswer; }
            set { _wronganswer = value; }
        }
        /// <summary>
        /// Result
        /// </summary>		
        private bool _result;
        public bool Result
        {
            get { return _result; }
            set { _result = value; }
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