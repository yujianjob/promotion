using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dianda.AP.DAL;
using Dianda.AP.Model;

namespace Dianda.AP.BLL
{
    public class UserBLL
    {
        UserDAL dal = new UserDAL();
        public List<UserModel> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, where, orderBy, out recordCount);
        }
        public bool UserAdd(string UserName, string PassWord, string ConfirmPass, string NickName, string Email, int Status, int OrganId, int PartitionId, int UserGroup)
        {
            return dal.UserAdd(UserName, PassWord, ConfirmPass, NickName, Email, Status, OrganId, PartitionId, UserGroup);
        }
        public string GetModel(int id)
        {
            return dal.GetModel(id);
        }
        public bool UserEdit(int id, string UserName, string NickName, string Email, int Status)
        {
            return dal.UserEdit(id, UserName, NickName, Email, Status);
        }
        public bool UpdPass(int id, string PassWord)
        {
            return dal.UpdPass(id, PassWord);
        }
        public bool UpdUserStatus(int id, string UpdString)
        {
            return dal.UpdUserStatus(id, UpdString);
        }
        public string GetPassWord(int AccountId)
        {
            return dal.GetPassWord(AccountId);
        }
        public int CountUser(string UserName,int id)
        {
            return dal.CountUser(UserName, id);
        }

        public bool UpdGroup(int AccountId, int PartitionId, int OrganId, int GroupId)
        {
            return dal.UpdGroup(AccountId, PartitionId, OrganId, GroupId);
        }
        public int GetGroup(int AccountId, int PartitionId)
        {
            return dal.GetGroup(AccountId, PartitionId);
        }
        public bool UserAddXJ(string UserName, string PassWord, string ConfirmPass, string NickName, string Email, int Status, int OrganId)
        {
            return dal.UserAddXJ(UserName, PassWord, ConfirmPass, NickName, Email, Status, OrganId);
        }
    }
}
