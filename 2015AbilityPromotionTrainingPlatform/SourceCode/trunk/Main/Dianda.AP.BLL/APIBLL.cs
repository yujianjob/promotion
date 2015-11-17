﻿using Dianda.AP.DAL;
using Dianda.AP.Model;
using Dianda.AP.Model.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.BLL
{
    public class APIBLL
    {
        APIDAL dal = new APIDAL();

        #region 学校
        public bool AddOrgan(Organ_Detail model)
        {
            return dal.AddOrgan(model) > 0;
        }

        public bool UpdateOrgan(Organ_Detail model)
        {
            return dal.UpdateOrgan(model) > 0;
        }

        public bool DeleteOrgan(int outSourceId)
        {
            return dal.DeleteOrgan(outSourceId) > 0;
        }

        public Organ_Detail GetOrganByOutId(int outId)
        {
            return dal.GetOrganByOutId(outId);
        }
        #endregion 

        #region 教师
        public bool AddMemberAccount(Member_Account model)
        {
            return dal.AddMemberAccount(model) > 0;
        }

        public bool AddMemberBaseInfo(MemberBaseInfo model)
        {
            return dal.AddMemberBaseInfo(model) > 0;
        }

        public bool UpdateMemberAccount(Member_Account model)
        {
            return dal.UpdateMemberAccount(model) > 0;
        }

        public bool UpdateMemberBaseInfo(MemberBaseInfo model)
        {
            return dal.UpdateMemberBaseInfo(model) > 0;
        }

        public bool DeleteMemberAccount(int id)
        {
            return dal.DeleteMemberAccount(id) > 0;
        }

        public bool DeleteMemberBaseInfo(int accountId)
        {
            return dal.DeleteMemberBaseInfo(accountId) > 0;
        }

        public Member_Account GetMemberAccountByOutId(int outId)
        {
            return dal.GetMemberAccountByOutId(outId);
        }

        public Member_Account GetMemberAccountByUserName(string userName)
        {
            return dal.GetMemberAccountByUserName(userName);
        }

        public Member_BaseInfo GetMemberInfoByAccountId(int accountId)
        {
            return dal.GetMemberInfoByAccountId(accountId);
        }

        //学科
        public void UpdateSubject(int accountId, string source, string target)
        {
            dal.UpdateSubject(accountId, source, target);
        }
        public void DeleteSubject(int accountId)
        {
            dal.DeleteSubject(accountId);
        }

        //学段
        public void UpdateSection(int accountId, string source, string target)
        {
            dal.UpdateSection(accountId, source, target);
        }
        public void DeleteSection(int accountId)
        {
            dal.DeleteSection(accountId);
        }

        //年级
        public void UpdateGrade(int accountId, string source, string target)
        {
            dal.UpdateGrade(accountId, source, target);
        }
        public void DeleteGrade(int accountId)
        {
            dal.DeleteGrade(accountId);
        }

        //职务
        public void UpdateJob(int accountId, string source, string target)
        {
            dal.UpdateJob(accountId, source, target);
        }
        public void DeleteJob(int accountId)
        {
            dal.DeleteJob(accountId);
        }

        //职称
        public void UpdateRank(int accountId, string source, string target)
        {
            dal.UpdateRank(accountId, source, target);
        }
        public void DeleteRank(int accountId)
        {
            dal.DeleteRank(accountId);
        }

        //培训形式
        public void UpdateTrainingType(int accountId, string source, string target)
        {
            dal.UpdateTrainingType(accountId, source, target);
        }
        public void DeleteTrainingType(int accountId)
        {
            dal.DeleteTrainingType(accountId);
        }

        //权限
        public void UpdateGroup(int accountId, int quxian, int school, string source, string target)
        {
            dal.UpdateGroup(accountId, quxian, school, source, target);
        }
        public void DeleteGroup(int accountId)
        {
            dal.DeleteGroup(accountId);
        }

        //将外部编号转为内部Id
        public string ConvertKey(string table, string outKey)
        {
            return dal.ConvertKey(table, outKey);
        }

        //根据用户编号取得外键Id字符串
        public string GetKeyByAccountId(string table, int accountId)
        {
            return dal.GetKeyByAccountId(table, accountId);
        }
        #endregion
    }
}
