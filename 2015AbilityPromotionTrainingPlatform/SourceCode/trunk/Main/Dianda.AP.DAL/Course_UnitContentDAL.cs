using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dianda.AP.Model;
using Dianda.AP.Model.Course.CourseCreate;

namespace Dianda.AP.DAL
{
    public partial class Course_UnitContentDAL
    {
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Course_UnitContent model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into [dbo].[Course_UnitContent] ([Title],[Content],[UnitType],[TestCnt],[PrintScore],[TimeLimit],[PassLine],[FinalTestLimit],[UnitId],[TimeLength],[OpenTime],[EndTime],[Display],[Sort],[Delflag],[CreateDate],[ContentType])");
            sql.Append(" values (@Title,@Content,@UnitType,@TestCnt,@PrintScore,@TimeLimit,@PassLine,@FinalTestLimit,@UnitId,@TimeLength,@OpenTime,@EndTime,@Display,@Sort,@Delflag,@CreateDate,@ContentType)");
            sql.Append(" set @Id=@@IDENTITY");
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@Title", SqlDbType.VarChar, 200) { Value = model.Title },
				new SqlParameter("@Content", SqlDbType.VarChar, 8000) { Value = model.Content },
				new SqlParameter("@UnitType", SqlDbType.Int, 4) { Value = model.UnitType },
				new SqlParameter("@TestCnt", SqlDbType.Int, 4) { Value = model.TestCnt },
				new SqlParameter("@PrintScore", SqlDbType.Bit, 1) { Value = model.PrintScore },
				new SqlParameter("@TimeLimit", SqlDbType.Int, 4) { Value = model.TimeLimit },
				new SqlParameter("@PassLine", SqlDbType.Int, 4) { Value = model.PassLine },
				new SqlParameter("@FinalTestLimit", SqlDbType.Bit, 1) { Value = model.FinalTestLimit },
				new SqlParameter("@UnitId", SqlDbType.Int, 4) { Value = model.UnitId },
				new SqlParameter("@TimeLength", SqlDbType.Int, 4) { Value = model.TimeLength },
				new SqlParameter("@OpenTime", SqlDbType.Int, 4) { Value = model.OpenTime },
				new SqlParameter("@EndTime", SqlDbType.Int, 4) { Value = model.EndTime },
				new SqlParameter("@Display", SqlDbType.Bit, 1) { Value = model.Display },
				new SqlParameter("@Sort", SqlDbType.Int, 4) { Value = model.Sort },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
                new SqlParameter("@ContentType", SqlDbType.Int, 4) { Value = model.ContentType }
			};
            int result = Convert.ToInt32(MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams));
            model.Id = Convert.ToInt32(cmdParams[0].Value);
            return model.Id;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Course_UnitContent model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update [dbo].[Course_UnitContent] set ");
            sql.Append("[Title]=@Title,[Content]=@Content,[UnitType]=@UnitType,[TestCnt]=@TestCnt,[PrintScore]=@PrintScore,[TimeLimit]=@TimeLimit,[PassLine]=@PassLine,[FinalTestLimit]=@FinalTestLimit,[UnitId]=@UnitId,[TimeLength]=@TimeLength,[OpenTime]=@OpenTime,[EndTime]=@EndTime,[Display]=@Display,[Sort]=@Sort,[Delflag]=@Delflag,[CreateDate]=@CreateDate,[ContentType]=@ContentType");
            sql.Append(" where [Id]=@Id");
            SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@Title", SqlDbType.VarChar, 200) { Value = model.Title },
				new SqlParameter("@Content", SqlDbType.VarChar, 8000) { Value = model.Content },
				new SqlParameter("@UnitType", SqlDbType.Int, 4) { Value = model.UnitType },
				new SqlParameter("@TestCnt", SqlDbType.Int, 4) { Value = model.TestCnt },
				new SqlParameter("@PrintScore", SqlDbType.Bit, 1) { Value = model.PrintScore },
				new SqlParameter("@TimeLimit", SqlDbType.Int, 4) { Value = model.TimeLimit },
				new SqlParameter("@PassLine", SqlDbType.Int, 4) { Value = model.PassLine },
				new SqlParameter("@FinalTestLimit", SqlDbType.Bit, 1) { Value = model.FinalTestLimit },
				new SqlParameter("@UnitId", SqlDbType.Int, 4) { Value = model.UnitId },
				new SqlParameter("@TimeLength", SqlDbType.Int, 4) { Value = model.TimeLength },
				new SqlParameter("@OpenTime", SqlDbType.Int, 4) { Value = model.OpenTime },
				new SqlParameter("@EndTime", SqlDbType.Int, 4) { Value = model.EndTime },
				new SqlParameter("@Display", SqlDbType.Bit, 1) { Value = model.Display },
				new SqlParameter("@Sort", SqlDbType.Int, 4) { Value = model.Sort },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
                new SqlParameter("@ContentType", SqlDbType.Int, 4) { Value = model.ContentType }
			};
            return MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update [dbo].[Course_UnitContent] set ");
            sql.Append("Delflag = 1");
            sql.Append(" where [Id]=@Id");
            SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
            return MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
        }

        /// <summary>
        /// 取得一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public Course_UnitContent GetModel(int id, string where)
        {
            string sql = "select * from [dbo].[Course_UnitContent] where [Id]=@Id";
            if (!string.IsNullOrEmpty(where))
                sql += " and " + where;
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                if (reader.Read())
                {
                    Course_UnitContent model = new Course_UnitContent();
                    ConvertToModel(reader, model);
                    return model;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Course_UnitContent> GetList(string where, string orderBy)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from [dbo].[Course_UnitContent]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            if (!string.IsNullOrEmpty(orderBy))
                sql.Append(" order by " + orderBy);
            List<Course_UnitContent> list = new List<Course_UnitContent>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
            {
                while (reader.Read())
                {
                    Course_UnitContent model = new Course_UnitContent();
                    ConvertToModel(reader, model);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Course_UnitContent> GetListOther(string where, string orderBy)
        {
            List<Course_UnitContent> NewList = new List<Course_UnitContent>();

            StringBuilder sql = new StringBuilder();
            sql.Append(@"select Course_UnitContent.*,Course_UnitDetail.Id as DetailId,Course_UnitDetail.ParentId from Course_UnitContent inner join Course_UnitDetail on 
                    Course_UnitContent.UnitId = Course_UnitDetail.Id
                    and Course_UnitDetail.Delflag=0
                    and Course_UnitContent.Display=1 and Course_UnitContent.Delflag=0");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            if (!string.IsNullOrEmpty(orderBy))
                sql.Append(" order by " + orderBy);

            var Table_Course_UnitContent = MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];

            if (Table_Course_UnitContent.Rows.Count == 1)
            {
                NewList = this.GetCourse_UnitContentList(Table_Course_UnitContent);
            }
            else
            {
                var newDt = GetDataTable(Table_Course_UnitContent);
                NewList = this.GetCourse_UnitContentList(newDt);
            }

            return NewList;
        }

        /// <summary>
        /// DataTable 转换为 List<Course_UnitContent>
        /// </summary>
        /// <param name="dtUnitContent"></param>
        /// <returns></returns>
        private List<Course_UnitContent> GetCourse_UnitContentList(DataTable dtUnitContent)
        {
            var list = new List<Course_UnitContent>();

            if (dtUnitContent != null && dtUnitContent.Rows.Count > 0)
            {
                foreach (DataRow DrItem in dtUnitContent.Rows)
                {
                    var model = new Course_UnitContent();

                    if (DrItem["Id"] != DBNull.Value)
                        model.Id = Convert.ToInt32(DrItem["Id"]);
                    if (DrItem["Title"] != DBNull.Value)
                        model.Title = DrItem["Title"].ToString();
                    if (DrItem["Content"] != DBNull.Value)
                        model.Content = DrItem["Content"].ToString();
                    if (DrItem["UnitType"] != DBNull.Value)
                        model.UnitType = Convert.ToInt32(DrItem["UnitType"]);
                    if (DrItem["TestCnt"] != DBNull.Value)
                        model.TestCnt = Convert.ToInt32(DrItem["TestCnt"]);
                    if (DrItem["PrintScore"] != DBNull.Value)
                        model.PrintScore = Convert.ToBoolean(DrItem["PrintScore"]);
                    if (DrItem["TimeLimit"] != DBNull.Value)
                        model.TimeLimit = Convert.ToInt32(DrItem["TimeLimit"]);
                    if (DrItem["PassLine"] != DBNull.Value)
                        model.PassLine = Convert.ToInt32(DrItem["PassLine"]);
                    if (DrItem["FinalTestLimit"] != DBNull.Value)
                        model.FinalTestLimit = Convert.ToBoolean(DrItem["FinalTestLimit"]);
                    if (DrItem["UnitId"] != DBNull.Value)
                        model.UnitId = Convert.ToInt32(DrItem["UnitId"]);
                    if (DrItem["TimeLength"] != DBNull.Value)
                        model.TimeLength = Convert.ToInt32(DrItem["TimeLength"]);
                    if (DrItem["OpenTime"] != DBNull.Value)
                        model.OpenTime = Convert.ToInt32(DrItem["OpenTime"]);
                    if (DrItem["EndTime"] != DBNull.Value)
                        model.EndTime = Convert.ToInt32(DrItem["EndTime"]);
                    if (DrItem["Display"] != DBNull.Value)
                        model.Display = Convert.ToBoolean(DrItem["Display"]);
                    if (DrItem["Sort"] != DBNull.Value)
                        model.Sort = Convert.ToInt32(DrItem["Sort"]);
                    if (DrItem["Delflag"] != DBNull.Value)
                        model.Delflag = Convert.ToBoolean(DrItem["Delflag"]);
                    if (DrItem["CreateDate"] != DBNull.Value)
                        model.CreateDate = Convert.ToDateTime(DrItem["CreateDate"]);
                    if (DrItem["ContentType"] != DBNull.Value)
                        model.ContentType = Convert.ToInt32(DrItem["ContentType"]);

                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 重新按顺序组装Table
        /// </summary>
        /// <param name="dtUnitContent"></param>
        /// <returns></returns>
        private DataTable GetDataTable(DataTable dtUnitContent)
        {
            var NewDataTable = dtUnitContent.Clone();
            var ParentIdList = new List<int>();
            var ParentCount = 0;
            var ChildCount = 0;
            var ExamCount = 0;

            DataRow[] drParentArr = dtUnitContent.Select("ParentId = 0 AND UnitType < 6", "DetailId");
            ParentCount = drParentArr.Length;
            DataRow[] drChildArr = dtUnitContent.Select("ParentId > 0 AND UnitType < 6", "DetailId");
            ChildCount = drChildArr.Length;
            DataRow[] drExamArr = dtUnitContent.Select("UnitType = 6", "DetailId");
            ExamCount = drExamArr.Length;

            //获取所有ParentId=0的Id集合
            if (drParentArr != null && drParentArr.Length > 0)
            {
                foreach (var ParentItem in drParentArr)
                {
                    var iDetailId = Convert.ToInt32(ParentItem["DetailId"]);

                    if (!ParentIdList.Contains(iDetailId))
                    {
                        ParentIdList.Add(iDetailId);
                    }
                }
            }

            //加载ParentId=0的数据
            foreach (var item in drParentArr)
            {
                NewDataTable.ImportRow(item);
            }

            if (ParentIdList.Count > 0)
            {
                foreach (var iId in ParentIdList)
                {
                    //查询章下是否有节,有节,获取节下的活动   没有节直接获取活动
                    DataRow[] drChapterArr = dtUnitContent.Select("ParentId = " + iId, "Id");

                    if (drChapterArr.Length > 0)
                    {
                        foreach (var item in drChapterArr)
                        {
                            var iActiveId = Convert.ToInt32(item["DetailId"]);
                            DataRow[] drActiveArr = dtUnitContent.Select("ParentId = " + iActiveId, "Id");
                            if (drActiveArr.Length > 0)
                            {
                                foreach (var ActiveItem in drActiveArr)
                                {
                                    NewDataTable.ImportRow(ActiveItem);
                                }
                            }
                            else
                            {
                                NewDataTable.ImportRow(item);
                            }
                        }
                    }
                    else
                    {
                        foreach (var item in drChapterArr)
                        {
                            NewDataTable.ImportRow(item);
                        }
                    }

                }
            }

            //添加结业考试
            if (drExamArr != null && drExamArr.Length > 0)
            {
                foreach (var item in drExamArr)
                {
                    NewDataTable.ImportRow(item);
                }
            }

            return NewDataTable;
        }

        /// <summary>
        /// 获取分页数据集
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public List<Course_UnitContent> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
        {
            if (string.IsNullOrEmpty(orderBy))
                throw new ArgumentNullException();
            StringBuilder sb = new StringBuilder();
            sb.Append(@"select count(1) from [dbo].[Course_UnitContent]");
            if (!string.IsNullOrEmpty(where))
                sb.Append(" where " + where);
            recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from (select *,ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Course_UnitContent]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            sql.Append(") as T where [RowNum] between " + start + " and " + end);
            List<Course_UnitContent> list = new List<Course_UnitContent>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
            {
                while (reader.Read())
                {
                    Course_UnitContent model = new Course_UnitContent();
                    ConvertToModel(reader, model);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取分页数据集
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public List<Course_UnitContent> GetListOther(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
        {
            if (string.IsNullOrEmpty(orderBy))
                throw new ArgumentNullException();
            StringBuilder sb = new StringBuilder();
            sb.Append(@"select count(1) from Course_UnitContent inner join Course_UnitDetail on 
                    Course_UnitContent.UnitId = Course_UnitDetail.Id
                    and Course_UnitDetail.Display=1 and Course_UnitDetail.Delflag=0
                    and Course_UnitContent.Display=1 and Course_UnitContent.Delflag=0");
            if (!string.IsNullOrEmpty(where))
                sb.Append(" where " + where);
            recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"select * from (select Course_UnitContent.*,ROW_NUMBER() over (order by Course_UnitContent.{0}) as [RowNum] from Course_UnitContent inner join Course_UnitDetail on 
                    Course_UnitContent.UnitId = Course_UnitDetail.Id
                    and Course_UnitDetail.Display=1 and Course_UnitDetail.Delflag=0
                    and Course_UnitContent.Display=1 and Course_UnitContent.Delflag=0", orderBy);
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            sql.Append(") as T where [RowNum] between " + start + " and " + end);
            List<Course_UnitContent> list = new List<Course_UnitContent>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
            {
                while (reader.Read())
                {
                    Course_UnitContent model = new Course_UnitContent();
                    ConvertToModel(reader, model);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public DataTable GetTable(string where, string orderBy)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select [Id],[Title],[Content],[UnitType],[TestCnt],[PrintScore],[TimeLimit],[PassLine],[FinalTestLimit],[UnitId],[TimeLength],[OpenTime],[EndTime],[Display],[Sort],[Delflag],[CreateDate],[ContentType] from [dbo].[Course_UnitContent]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            if (!string.IsNullOrEmpty(orderBy))
                sql.Append(" order by " + orderBy);
            return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
        }

        /// <summary>
        /// 获取分页DataTable
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataTable GetTable(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
        {
            if (string.IsNullOrEmpty(orderBy))
                throw new ArgumentNullException();
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(1) from [dbo].[Course_UnitContent]");
            if (!string.IsNullOrEmpty(where))
                sb.Append(" where " + where);
            recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from (select [Id],[Title],[Content],[UnitType],[TestCnt],[PrintScore],[TimeLimit],[PassLine],[FinalTestLimit],[UnitId],[TimeLength],[OpenTime],[EndTime],[Display],[Sort],[Delflag],[CreateDate],[ContentType],ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Course_UnitContent]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            sql.Append(") as T where [RowNum] between " + start + " and " + end);
            return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
        }

        private void ConvertToModel(IDataReader reader, Course_UnitContent model)
        {
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);
            if (reader["Title"] != DBNull.Value)
                model.Title = reader["Title"].ToString();
            if (reader["Content"] != DBNull.Value)
                model.Content = reader["Content"].ToString();
            if (reader["UnitType"] != DBNull.Value)
                model.UnitType = Convert.ToInt32(reader["UnitType"]);
            if (reader["TestCnt"] != DBNull.Value)
                model.TestCnt = Convert.ToInt32(reader["TestCnt"]);
            if (reader["PrintScore"] != DBNull.Value)
                model.PrintScore = Convert.ToBoolean(reader["PrintScore"]);
            if (reader["TimeLimit"] != DBNull.Value)
                model.TimeLimit = Convert.ToInt32(reader["TimeLimit"]);
            if (reader["PassLine"] != DBNull.Value)
                model.PassLine = Convert.ToInt32(reader["PassLine"]);
            if (reader["FinalTestLimit"] != DBNull.Value)
                model.FinalTestLimit = Convert.ToBoolean(reader["FinalTestLimit"]);
            if (reader["UnitId"] != DBNull.Value)
                model.UnitId = Convert.ToInt32(reader["UnitId"]);
            if (reader["TimeLength"] != DBNull.Value)
                model.TimeLength = Convert.ToInt32(reader["TimeLength"]);
            if (reader["OpenTime"] != DBNull.Value)
                model.OpenTime = Convert.ToInt32(reader["OpenTime"]);
            if (reader["EndTime"] != DBNull.Value)
                model.EndTime = Convert.ToInt32(reader["EndTime"]);
            if (reader["Display"] != DBNull.Value)
                model.Display = Convert.ToBoolean(reader["Display"]);
            if (reader["Sort"] != DBNull.Value)
                model.Sort = Convert.ToInt32(reader["Sort"]);
            if (reader["Delflag"] != DBNull.Value)
                model.Delflag = Convert.ToBoolean(reader["Delflag"]);
            if (reader["CreateDate"] != DBNull.Value)
                model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
            if (reader["ContentType"] != DBNull.Value)
                model.ContentType = Convert.ToInt32(reader["ContentType"]);
        }

        /// <summary>
        /// 获取指定章节指定内容类型学习活动最大顺序号
        /// </summary>
        /// <param name="UnitId"></param>
        /// <param name="UnitType"></param>
        /// <returns></returns>
        public int GetUnitContentMaxOrder(int UnitId, int UnitType)
        {
            string sql = "select Max(Sort) as MaxSort from [dbo].[Course_UnitContent] where UnitId=@UnitId and UnitType=@UnitType and Delflag=0";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@UnitId", SqlDbType.Int, 4) { Value = UnitId },
                new SqlParameter("@UnitType", SqlDbType.Int, 4) { Value = UnitType }
            };

            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                if (reader.Read())
                {
                    if (reader["MaxSort"].ToString() != "")
                    {
                        return Convert.ToInt32(reader["MaxSort"]);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 获取指定章节的活动信息(课程详细)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Course_ActivityInfo> GetActivityInfo(int id)
        {
            string sql = @"select distinct A.id as id,title,Sort,content,unittype,timelength,display,B.Status as status  
                           from [dbo].[Course_UnitContent] as A left join dbo.[Member_ClassUnitContentSchedule] as B 
                           on B.UnitContent = A.Id and B.Delflag = 0    
                           where unitid=@Id and A.Delflag=0 order by Sort";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
            };

            List<Course_ActivityInfo> list = new List<Course_ActivityInfo>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                while (reader.Read())
                {
                    Course_ActivityInfo model = new Course_ActivityInfo();
                    model.Id = Convert.ToInt32(reader["id"]);
                    model.Title = reader["title"].ToString();
                    model.Content = reader["content"].ToString();
                    model.UnitType = Convert.ToInt32(reader["unittype"]);
                    model.TimeLength = Convert.ToInt32(reader["timelength"]);
                    model.Display = Convert.ToBoolean(reader["display"]);

                    if (!string.IsNullOrEmpty(reader["Status"].ToString()))
                    {
                        model.Status = Convert.ToBoolean(reader["Status"]);
                    }

                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取指定章节的活动信息(课程预览和学习)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Course_ActivityInfo> GetLearnActivityInfo(int id)
        {
            string sql = @"select id,title,content,unittype,timelength,display from [dbo].[Course_UnitContent]     
                            where unitid=@Id and Display = 1 and Delflag=0 order by Sort";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
            };

            List<Course_ActivityInfo> list = new List<Course_ActivityInfo>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                while (reader.Read())
                {
                    Course_ActivityInfo model = new Course_ActivityInfo();
                    model.Id = Convert.ToInt32(reader["id"]);
                    model.Title = reader["title"].ToString();
                    model.Content = reader["content"].ToString();
                    model.UnitType = Convert.ToInt32(reader["unittype"]);
                    model.TimeLength = Convert.ToInt32(reader["timelength"]);
                    model.Display = Convert.ToBoolean(reader["display"]);

                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取指定章节的学习活动信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ClassId"></param>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        public List<Course_ActivityLearn> GetActivityLearn(int id, int ClassId, int AccountId)
        {
            string sql = @"select y.id,title,content,unittype,timelength,display,OpenDate,EndDate,Status,count(mcr.Id)as mcrcount from (
                            select A.id as id,A.title as title,A.content as content,unittype,timelength,A.display as display,(case when OpenTime=0 then 0 else dateadd(DAY,OpenTime,OpenClassFrom) end) as OpenDate,(case when EndTime=0 then 0 else dateadd(DAY,EndTime,OpenClassFrom) end) as EndDate,D.Status as Status  ,a.Sort,D.AccountId 
                            from [dbo].[Course_UnitContent] as A  
                                 left join Course_UnitDetail as B on B.Id = A.UnitId and B.Delflag =0  
                                 left join Class_Detail C on C.TraningId= B.TrainingId and C.Delflag = 0 and C.Id=@ClassId
                                 left join Member_ClassUnitContentSchedule as D on D.UnitContent = A.Id and D.Delflag =0 and D.AccountId=@AccountId  
                            where unitid=@Id and A.Delflag=0 and A.Display = 1 
                            ) y left join 
                            Member_ClassContentTimeRecord mcr on y.id=mcr.UnitContent where mcr.AccountId=y.AccountId 
                            group by y.id,title,content,unittype,timelength,display,OpenDate,EndDate,Status,Sort
                            order by y.Sort";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id },
                new SqlParameter("@ClassId", SqlDbType.Int, 4) { Value = ClassId },
                new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = AccountId }
            };

            List<Course_ActivityLearn> list = new List<Course_ActivityLearn>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                while (reader.Read())
                {
                    Course_ActivityLearn model = new Course_ActivityLearn();
                    model.Id = Convert.ToInt32(reader["id"]);
                    model.Title = reader["title"].ToString();
                    model.Content = reader["content"].ToString();
                    model.UnitType = Convert.ToInt32(reader["unittype"]);
                    model.TimeLength = Convert.ToInt32(reader["timelength"]);
                    model.Display = Convert.ToBoolean(reader["display"]);
                    if (!string.IsNullOrEmpty(reader["Status"].ToString()))
                    {
                        model.Status = Convert.ToBoolean(reader["Status"]);
                    }
                    if (!string.IsNullOrEmpty(reader["OpenDate"].ToString()))
                    {
                        if (Convert.ToDateTime(reader["OpenDate"]).Year > 1900)
                        {
                            model.OpenDate = Convert.ToDateTime(reader["OpenDate"]);
                        }
                        else
                        {
                            model.OpenDate = DateTime.Now;
                        }
                    }
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        if (Convert.ToDateTime(reader["EndDate"]).Year > 1900)
                        {
                            model.EndDate = Convert.ToDateTime(reader["EndDate"]);
                        }
                        else
                        {
                            model.EndDate = DateTime.Now.AddYears(100);
                        }
                    }
                    if (!string.IsNullOrEmpty(reader["mcrcount"].ToString()))
                    {
                        model.Mcrcount = Convert.ToInt32(reader["mcrcount"]);
                    }
                    
                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取指定课程ID的结业考试(课程详细)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Course_ActivityInfo> GetExamInfo(int id)
        {
            string sql = @"select C.id as id,C.title as title,C.TimeLength as TimeLength,C.TestCnt as TestCnt,C.display as display,D.Status as Status   
                            from dbo.[Traning_Detail] as A,[dbo].[Course_UnitDetail] as B,[dbo].[Course_UnitContent] as C 
                            left join dbo.Member_ClassUnitContentSchedule D on D.UnitContent = C.Id and D.Delflag = 0 
                            where C.unitid = B.id and B.trainingid = A.id and C.Delflag = 0 and B.Delflag = 0 and A.Delflag = 0 
                            and A.Id =@Id and C.UnitType = 6 order by C.id";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
            };

            List<Course_ActivityInfo> list = new List<Course_ActivityInfo>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                while (reader.Read())
                {
                    Course_ActivityInfo model = new Course_ActivityInfo();
                    model.Id = Convert.ToInt32(reader["id"]);
                    model.Title = reader["title"].ToString();
                    model.TimeLength = Convert.ToInt32(reader["TimeLength"]);
                    model.TestCnt = Convert.ToInt32(reader["TestCnt"]);
                    model.Display = Convert.ToBoolean(reader["display"]);
                    if (!string.IsNullOrEmpty(reader["Status"].ToString()))
                    {
                        model.Status = Convert.ToBoolean(reader["Status"]);
                    }
                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取指定课程ID的结业考试(课程预览和学习)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Course_ActivityInfo> GetExamLearnInfo(int id)
        {
            string sql = @"select distinct C.id as id,C.title as title,C.TimeLength as TimeLength,C.TestCnt as TestCnt,C.display as display   
                           from dbo.[Traning_Detail] as A,[dbo].[Course_UnitDetail] as B,[dbo].[Course_UnitContent] as C 
                           where C.unitid = B.id and B.trainingid = A.id and C.Display = 1 and C.Delflag = 0 and B.Delflag = 0 and A.Delflag = 0 
                           and A.Id = @Id and C.UnitType = 6 order by C.id";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
            };

            List<Course_ActivityInfo> list = new List<Course_ActivityInfo>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                while (reader.Read())
                {
                    Course_ActivityInfo model = new Course_ActivityInfo();
                    model.Id = Convert.ToInt32(reader["id"]);
                    model.Title = reader["title"].ToString();
                    model.TimeLength = Convert.ToInt32(reader["TimeLength"]);
                    model.TestCnt = Convert.ToInt32(reader["TestCnt"]);
                    model.Display = Convert.ToBoolean(reader["display"]);
                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取指定课程ID的学习结业考试
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ClassId"></param>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        public List<Course_ActivityLearn> GetLearnExamInfo(int id, int ClassId, int AccountId)
        {
            string sql = @"select distinct C.id as id,C.title as title,C.TimeLength as TimeLength,C.TestCnt as TestCnt,C.display as display,C.FinalTestLimit as FinalTestLimit,(case when OpenTime=0 then 0 else dateadd(DAY,OpenTime,OpenClassFrom) end) as OpenDate,(case when EndTime=0 then 0 else dateadd(DAY,EndTime,OpenClassFrom) end) as EndDate,D.Status as Status  
                            from dbo.[Traning_Detail] as A 
                                 left join [dbo].[Course_UnitDetail] as B on B.trainingid = A.id and B.Delflag = 0  
                                 left join [dbo].[Course_UnitContent] as C on C.unitid = B.id and C.Delflag = 0 and C.Delflag = 0 
                                 left join Member_ClassUnitContentSchedule as D on D.UnitContent = C.Id and D.Delflag =0 and D.AccountId=@AccountId
                                 left join Class_Detail F on F.traningid = B.trainingid and F.Delflag = 0 and F.Id=@ClassId 
                            where A.Delflag = 0 and A.Id=@Id and C.UnitType=6 order by C.id";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id },
                new SqlParameter("@ClassId", SqlDbType.Int, 4) { Value = ClassId },
                new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = AccountId }
            };

            List<Course_ActivityLearn> list = new List<Course_ActivityLearn>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                while (reader.Read())
                {
                    Course_ActivityLearn model = new Course_ActivityLearn();
                    model.Id = Convert.ToInt32(reader["id"]);
                    model.Title = reader["title"].ToString();
                    model.TimeLength = Convert.ToInt32(reader["TimeLength"]);
                    model.TestCnt = Convert.ToInt32(reader["TestCnt"]);
                    model.Display = Convert.ToBoolean(reader["display"]);
                    model.FinalTestLimit = Convert.ToBoolean(reader["FinalTestLimit"]);
                    if (!string.IsNullOrEmpty(reader["Status"].ToString()))
                    {
                        model.Status = Convert.ToBoolean(reader["Status"]);
                    }
                    if (!string.IsNullOrEmpty(reader["OpenDate"].ToString()))
                    {
                        if (Convert.ToDateTime(reader["OpenDate"]).Year > 1900)
                        {
                            model.OpenDate = Convert.ToDateTime(reader["OpenDate"]);
                        }
                        else
                        {
                            model.OpenDate = DateTime.Now;
                        }
                    }
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        if (Convert.ToDateTime(reader["EndDate"]).Year > 1900)
                        {
                            model.EndDate = Convert.ToDateTime(reader["EndDate"]);
                        }
                        else
                        {
                            model.EndDate = DateTime.Now.AddYears(100);
                        }
                    }
                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 更新结业考试可测试次数(-1)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateTestCnt(int id)
        {
            string sql = @"Update [dbo].[Course_UnitContent] set TestCnt=TestCnt-1 where Id=@Id";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
            };

            List<Course_ActivityInfo> list = new List<Course_ActivityInfo>();
            int result = Convert.ToInt32(MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams));

            return result > 0;
        }

        /// <summary>
        /// 是否学完所有课程
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ClassId"></param>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        public bool IsAllLearn(int TrainingId, int ClassId, int AccountId)
        {
            bool IsAllLearn = true;

            string sql = @"select D.Status as Status   
                            from [dbo].[Course_UnitContent] as A  
                                 left join Course_UnitDetail as B on B.Id = A.UnitId and B.Delflag =0  
                                 left join Class_Detail C on C.TraningId= B.TrainingId and C.Delflag = 0 and C.Id=@ClassId 
                                 left join Member_ClassUnitContentSchedule as D on D.UnitContent = A.Id and D.Delflag =0 and D.AccountId=@AccountId  
                            where B.TrainingId =@TrainingId and A.UnitType != 6 and A.Delflag=0 and A.Display = 1 order by A.Sort";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@TrainingId", SqlDbType.Int, 4) { Value = TrainingId },
                new SqlParameter("@ClassId", SqlDbType.Int, 4) { Value = ClassId },
                new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = AccountId }
            };

            List<Course_ActivityLearn> list = new List<Course_ActivityLearn>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(reader["Status"].ToString()))
                    {
                        if (!Convert.ToBoolean(reader["Status"]))
                        {
                            IsAllLearn = false;
                            break;
                        }
                    }
                    else
                    {
                        IsAllLearn = false;
                        break;
                    }
                }
            }

            return IsAllLearn;
        }

        /// <summary>
        /// 判断同一课程下新增章名称是否重复
        /// </summary>
        /// <param name="TrainingId"></param>
        /// <param name="tilte"></param>
        /// <returns></returns>
        public bool IsChapterAddRename(int TrainingId, string tilte)
        {
            bool IsChapterAddRename = false;

            string sql = @"select Title from dbo.Course_UnitDetail where Delflag = 0 and ParentId = 0 and TrainingId =@TrainingId";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@TrainingId", SqlDbType.Int, 4) { Value = TrainingId }
            };

            List<Course_ActivityLearn> list = new List<Course_ActivityLearn>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(reader["Title"].ToString()))
                    {
                        if (String.Equals(tilte, reader["Title"].ToString()))
                        {
                            IsChapterAddRename = true;
                            break;
                        }
                    }
                }
            }

            return IsChapterAddRename;
        }

        /// <summary>
        /// 判断同一课程下编辑章名称是否重复
        /// </summary>
        /// <param name="TrainingId"></param>
        /// <param name="tilte"></param>
        /// <param name="CourseId"></param>
        /// <returns></returns>
        public bool IsChapterEditRename(int TrainingId, string tilte, int CourseId)
        {
            bool IsChapterEditRename = false;

            string sql = @"select Title from dbo.Course_UnitDetail where Delflag = 0 and ParentId = 0 and TrainingId =@TrainingId and Id !=@CourseId";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@TrainingId", SqlDbType.Int, 4) { Value = TrainingId },
                new SqlParameter("@CourseId", SqlDbType.Int, 4) { Value = CourseId }
            };

            List<Course_ActivityLearn> list = new List<Course_ActivityLearn>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(reader["Title"].ToString()))
                    {
                        if (String.Equals(tilte, reader["Title"].ToString()))
                        {
                            IsChapterEditRename = true;
                            break;
                        }
                    }
                }
            }

            return IsChapterEditRename;
        }

        /// <summary>
        /// 判断同一章下增加节名称是否重复
        /// </summary>
        /// <param name="ParentId"></param>
        /// <param name="tilte"></param>
        /// <returns></returns>
        public bool IsSectionAddRename(int ParentId, string tilte)
        {
            bool IsSectionAddRename = false;

            string sql = @"select Title from dbo.Course_UnitDetail where Delflag = 0 and ParentId=@ParentId";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@ParentId", SqlDbType.Int, 4) { Value = ParentId }
            };

            List<Course_ActivityLearn> list = new List<Course_ActivityLearn>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(reader["Title"].ToString()))
                    {
                        if (String.Equals(tilte, reader["Title"].ToString()))
                        {
                            IsSectionAddRename = true;
                            break;
                        }
                    }
                }
            }

            return IsSectionAddRename;
        }

        /// <summary>
        /// 判断同一章下编辑节名称是否重复
        /// </summary>
        /// <param name="ParentId"></param>
        /// <param name="tilte"></param>
        /// <param name="CourseId"></param>
        /// <returns></returns>
        public bool IsSectionEditRename(int ParentId, string tilte, int CourseId)
        {
            bool IsSectionEditRename = false;

            string sql = @"select Title from dbo.Course_UnitDetail where Delflag = 0 and ParentId =@ParentId and Id !=@CourseId";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@ParentId", SqlDbType.Int, 4) { Value = ParentId },
                new SqlParameter("@CourseId", SqlDbType.Int, 4) { Value = CourseId }
            };

            List<Course_ActivityLearn> list = new List<Course_ActivityLearn>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(reader["Title"].ToString()))
                    {
                        if (String.Equals(tilte, reader["Title"].ToString()))
                        {
                            IsSectionEditRename = true;
                            break;
                        }
                    }
                }
            }

            return IsSectionEditRename;
        }

        /// <summary>
        /// 判断同一章节下同一类型新增活动名称是否重复
        /// </summary>
        /// <param name="UnitId"></param>
        /// <param name="UnitType"></param>
        /// <param name="tilte"></param>
        /// <returns></returns>
        public bool IsActivityAddRename(int UnitId, int UnitType, string tilte)
        {
            bool IsActivityAddRename = false;

            string sql = @"select title from dbo.Course_UnitContent where UnitId =@UnitId and UnitType =@UnitType and Delflag = 0";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@UnitId", SqlDbType.Int, 4) { Value = UnitId },
                new SqlParameter("@UnitType", SqlDbType.Int, 4) { Value = UnitType }
            };

            List<Course_ActivityLearn> list = new List<Course_ActivityLearn>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(reader["Title"].ToString()))
                    {
                        if (String.Equals(tilte, reader["Title"].ToString()))
                        {
                            IsActivityAddRename = true;
                            break;
                        }
                    }
                }
            }

            return IsActivityAddRename;
        }

        /// <summary>
        /// 判断同一章节下同一类型编辑活动名称是否重复
        /// </summary>
        /// <param name="UnitId"></param>
        /// <param name="UnitType"></param>
        /// <param name="tilte"></param>
        /// <param name="UnitContent"></param>
        /// <returns></returns>
        public bool IsActivityEditRename(int UnitId, int UnitType, string tilte, int UnitContent)
        {
            bool IsActivityEditRename = false;

            string sql = @"select title from dbo.Course_UnitContent where UnitId =@UnitId and UnitType =@UnitType and Delflag = 0 and Id !=@UnitContent";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@UnitId", SqlDbType.Int, 4) { Value = UnitId },
                new SqlParameter("@UnitType", SqlDbType.Int, 4) { Value = UnitType },
                new SqlParameter("@UnitContent", SqlDbType.Int, 4) { Value = UnitContent }
            };

            List<Course_ActivityLearn> list = new List<Course_ActivityLearn>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(reader["Title"].ToString()))
                    {
                        if (String.Equals(tilte, reader["Title"].ToString()))
                        {
                            IsActivityEditRename = true;
                            break;
                        }
                    }
                }
            }

            return IsActivityEditRename;
        }
    }
}