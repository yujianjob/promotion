using Dianda.AP.BLL;
using Dianda.AP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Code
{
    public class ScoreSetHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectId">单元ID</param>
        /// <param name="objectType">单元类型</param>
        /// <param name="classId">班级ID</param>
        /// <param name="planId">计划ID</param>
        /// <param name="accountId">成员ID</param>
        /// <param name="score">分值</param>
        public static void ScoreSet(int? objectId, int objectType, int classId, int planId, int accountId, double score, bool add = true)
        {
            dynamic objectBll;
            dynamic model;
            Member_ClassRegister memberModel;
            int total = 1;
            var member_classBll = new Member_ClassRegisterBLL();
            var member_class = member_classBll.GetList(" Delflag=0 and ClassId=" + classId + " and PlanId=" + planId + " and AccountId=" + accountId, "");
            if (member_class == null || member_class.Count == 0) return;
            memberModel = member_class.FirstOrDefault();

            var traningBll = new Traning_DetailBLL();
            var traning_detail = traningBll.GetModel(memberModel.TrainingId.Value, "");
            if (traning_detail == null) return;

            var courseBll = new Course_DetailBLL();
            var cousre_detail = courseBll.GetList(" Delflag=0 and TrainingId=" + traning_detail.Id, "");
            if (cousre_detail == null || cousre_detail.Count == 0) return;

            var unitContentBll = new Course_UnitContentBLL();
            var oldScore = 0.0;
            switch (objectType)
            {
                case 1://read

                    objectBll = new Member_ClassUnitContentScheduleBLL();
                    model = objectBll.GetList("Delflag=0 and ClassId=" + classId + " and AccountId=" + accountId + " and UnitContent=" + objectId, "");
                    if (model.Count > 0)
                    {
                        if (add)
                            model[0].score = 1;
                        else
                            model[0].score = 0;
                        objectBll.Update(model[0]);//
                    }
                    total = unitContentBll.GetUnitCountByClassAndUnitType(classId, "1,2");//阅读视频类的单元活动数
                    if (total > 1)
                    {
                        if (add)
                            memberModel.ReadingScore = (memberModel.ReadingScore.HasValue ? memberModel.ReadingScore.Value : 0)
                                + (cousre_detail[0].ReadingRate / (total)).ToDouble();
                        else
                        {
                            if (memberModel.ReadingScore.HasValue && memberModel.ReadingScore.Value.ToInt() > 0)
                                memberModel.ReadingScore = memberModel.ReadingScore.Value
                                    - (cousre_detail[0].ReadingRate / (total)).ToDouble();
                        }
                    }
                    else
                    {
                        if (add)
                            memberModel.ReadingScore = cousre_detail[0].ReadingRate;
                        else
                            memberModel.ReadingScore = 0.0;
                    }

                    if (memberModel.ReadingScore > cousre_detail[0].ReadingRate)
                    {
                        memberModel.ReadingScore = cousre_detail[0].ReadingRate;
                    }
                    if (memberModel.ReadingScore < 0)
                    {
                        memberModel.ReadingScore = 0;
                    }
                    member_classBll.Update(memberModel);//更新班级成员考试分数总值
                    break;
                case 2://discuss
                    objectBll = new Member_ClassUnitContentScheduleBLL();
                    model = objectBll.GetList("Delflag=0 and ClassId=" + classId + " and AccountId=" + accountId + " and UnitContent=" + objectId, "");
                    if (model.Count > 0)
                    {
                        if (add)
                            model[0].score = 1;
                        else
                            model[0].score = 0;
                        objectBll.Update(model[0]);//
                    }
                    total = unitContentBll.GetUnitCountByClassAndUnitType(classId, "3");//讨论类的单元活动数
                    if (total > 1)
                    {
                        if (add)
                            memberModel.DiscussScore = (memberModel.DiscussScore.HasValue ? memberModel.DiscussScore.Value : 0)
                                + (cousre_detail[0].DisscusRate / (total)).ToDouble();
                        else
                        {
                            if (memberModel.ReadingScore.HasValue && memberModel.ReadingScore.Value.ToInt() > 0)
                                memberModel.DiscussScore = memberModel.DiscussScore.Value
                                    - (cousre_detail[0].DisscusRate / (total)).ToDouble();
                        }
                    }
                    else
                    {
                        if (add)
                            memberModel.DiscussScore = cousre_detail[0].DisscusRate;
                        else
                            memberModel.DiscussScore = 0.0;
                    }

                    if (memberModel.DiscussScore > cousre_detail[0].DisscusRate)
                    {
                        memberModel.DiscussScore = cousre_detail[0].DisscusRate;
                    }
                    if (memberModel.DiscussScore < 0)
                    {
                        memberModel.DiscussScore = 0;
                    }
                    member_classBll.Update(memberModel);//更新班级成员考试分数总值
                    break;
                case 3://homework
                    #region

                    objectBll = new Course_UnitHomeWorkBLL();
                    model = objectBll.GetModel(objectId.Value, "");
                    model.Score = score;
                    model.ScoreCreater = SiteCache.Instance.ManagerId;
                    objectBll.Update(model);//更新结果表
                    total = unitContentBll.GetUnitCountByClassAndUnitType(classId, "4");//作业类的单元活动数
                    if (total > 1)
                    {
                        memberModel.HomeWorkScore = (memberModel.HomeWorkScore.HasValue ? memberModel.HomeWorkScore.Value : 0)
                           + (score * (cousre_detail[0].HomeWorkRate) / (total * 100)).ToDouble();
                    }
                    else
                    {
                        memberModel.HomeWorkScore = (score * (cousre_detail[0].HomeWorkRate) / (100)).ToDouble();
                    }

                    if (memberModel.HomeWorkScore > cousre_detail[0].HomeWorkRate)
                    {
                        memberModel.HomeWorkScore = cousre_detail[0].HomeWorkRate;
                    }
                    member_classBll.Update(memberModel);//更新班级成员作业分数总值
                    #endregion
                    break;
                case 4://question
                    #region

                    objectBll = new Member_ClassUnitContentScheduleBLL();
                    model = objectBll.GetModel(objectId.Value, "");
                    oldScore = model.score == null ? 0 : model.score;
                    model.score = score;
                    objectBll.Update(model);//更新班级成员测试分数总值
                    total = unitContentBll.GetUnitCountByClassAndUnitType(classId, "5");//测试类的单元活动数
                    if (add)//第一次打分
                    {
                        if (total > 1)
                        {
                            memberModel.TestingScore = (memberModel.TestingScore.HasValue ? memberModel.TestingScore.Value : 0)
                               + (score * (cousre_detail[0].QuestionRate) / (total * 100)).ToDouble();
                        }
                        else
                        {
                            memberModel.TestingScore = (score * (cousre_detail[0].QuestionRate) / (100)).ToDouble();
                        }
                    }
                    else//多次打分，重新计算
                    {
                        var contentList = DataTableToListHelper<Course_UnitContent>.ConvertToModel(unitContentBll.GetUnitByClassAndUnitType(classId, "5"));
                        if (contentList != null)
                        {
                            var scheduleBll = new Member_ClassUnitContentScheduleBLL();
                            double testscore = 0;
                            foreach (var item in contentList)
                            {
                                var scheduleModel = scheduleBll.GetList("Delflag=0 and ClassId=" + classId + " and AccountId=" + accountId + " AND UnitContent=" + item.Id, "");
                                if (scheduleModel != null && scheduleModel.Count > 0)
                                {
                                    if (scheduleModel.First().score.HasValue)
                                    {
                                        testscore += (scheduleModel.First().score.Value * (cousre_detail[0].QuestionRate) / (total * 100)).ToDouble();
                                    }
                                }
                            }
                            memberModel.TestingScore = testscore;
                        }
                    }
                    if (memberModel.TestingScore > cousre_detail[0].QuestionRate)
                    {
                        memberModel.TestingScore = cousre_detail[0].QuestionRate;
                    }
                    member_classBll.Update(memberModel);//更新班级成员测试分数总值
                    #endregion
                    break;
                case 5://examnation
                    #region

                    objectBll = new Member_ClassUnitContentScheduleBLL();
                    model = objectBll.GetModel(objectId.Value, "");
                    oldScore = model.score == null ? 0 : model.score;
                    model.score = score;
                    if (score >= 60)//考试大于等于60分算通过
                        memberModel.IsPass = true;
                    objectBll.Update(model);//更新班级成员考试分数总值
                    total = unitContentBll.GetUnitCountByClassAndUnitType(classId, "6");//考试类的单元活动数
                    if (total > 1)
                    {
                        memberModel.ExaminationScore = (memberModel.ExaminationScore.HasValue ? memberModel.ExaminationScore.Value : 0)
                            + (score * (cousre_detail[0].TestingRate) / (total * 100)).ToDouble();
                    }
                    else
                    {
                        memberModel.ExaminationScore = (score * (cousre_detail[0].TestingRate) / (100)).ToDouble();
                    }

                    if (memberModel.ExaminationScore > cousre_detail[0].TestingRate)
                    {
                        memberModel.ExaminationScore = cousre_detail[0].TestingRate;
                    }
                    member_classBll.Update(memberModel);//更新班级成员考试分数总值
                    #endregion
                    break;
                case 6://comment
                    #region
                    objectBll = new Member_ClassRegisterBLL();
                    model = objectBll.GetList(" Delflag=0 and ClassId=" + classId + " and PlanId=" + planId + " and AccountId=" + accountId, "")[0];
                    model.CommentScore = (score * (cousre_detail[0].CommentRate) / (total * 100)).ToDouble();
                    objectBll.Update(model);//更新班级成员评价分数总值
                    #endregion
                    break;
                default: break;
            }
        }


        /// <summary>
        /// 重新打分
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="planId"></param>
        /// <param name="accountId"></param>
        public static void resetScore(int classId, int planId)
        {
            var member_classBll = new Member_ClassRegisterBLL();

            var classDetail = new Class_DetailBLL().GetModel(classId);

            var traningBll = new Traning_DetailBLL();
            var traning_detail = traningBll.GetModel(classDetail.TraningId, "");
            if (traning_detail == null) return;

            var courseBll = new Course_DetailBLL();
            var cousre_detail = courseBll.GetList(" TrainingId=" + traning_detail.Id, "");
            if (cousre_detail == null || cousre_detail.Count == 0) return;

            var unitContentBll = new Course_UnitContentBLL();
            var memberList = member_classBll.GetList("Delflag=0 and ClassId=" + classId + " and PlanId=" + planId, "");
            foreach (var memberModel in memberList)
            {
                var accountId = memberModel.AccountId;
                int total = 1;

                IList<Course_UnitContent> contentList = new List<Course_UnitContent>();

                #region 
                //重新计算阅读分数

                total = unitContentBll.GetUnitCountByClassAndUnitType(classId, "1,2");
                contentList = DataTableToListHelper<Course_UnitContent>.ConvertToModel(unitContentBll.GetUnitByClassAndUnitType(classId, "1,2"));
                if (contentList != null)
                {
                    var scheduleBll = new Member_ClassUnitContentScheduleBLL();
                    int scoreCount = 0;
                    foreach (var item in contentList)
                    {
                        var scheduleModel = scheduleBll.GetList("Delflag=0 and ClassId=" + classId + " and AccountId=" + accountId + " and UnitContent=" + item.Id, "");
                        if (scheduleModel != null && scheduleModel.Count > 0)
                        {
                            if (scheduleModel.First().score.HasValue)//如果有值说明打过分
                            {
                                scoreCount++;
                            }
                        }
                    }
                    if (scoreCount == total)//全部阅读过，给满分
                    {
                        memberModel.ReadingScore = cousre_detail[0].ReadingRate;
                    }
                    else
                    {
                        memberModel.ReadingScore = cousre_detail[0].ReadingRate * scoreCount / total;//阅读数与总数折算
                    }
                }


                //重新计算讨论分数

                total = unitContentBll.GetUnitCountByClassAndUnitType(classId, "3");
                contentList = DataTableToListHelper<Course_UnitContent>.ConvertToModel(unitContentBll.GetUnitByClassAndUnitType(classId, "3"));
                if (contentList != null)
                {
                    var scheduleBll = new Member_ClassUnitContentScheduleBLL();
                    int scoreCount = 0;
                    foreach (var item in contentList)
                    {
                        var scheduleModel = scheduleBll.GetList("Delflag=0 and ClassId=" + classId + " and AccountId=" + accountId + " and UnitContent=" + item.Id, "");
                        if (scheduleModel != null && scheduleModel.Count > 0)
                        {
                            if (scheduleModel.First().score.HasValue)//如果有值说明打过分
                            {
                                scoreCount++;
                            }
                        }
                    }
                    if (scoreCount == total)//全部讨论过，给满分
                    {
                        memberModel.DiscussScore = cousre_detail[0].DisscusRate;
                    }
                    else
                    {
                        memberModel.DiscussScore = cousre_detail[0].DisscusRate * scoreCount / total;//讨论数与总数折算
                    }
                }

                //如有分值，重新计算作业分数

                total = unitContentBll.GetUnitCountByClassAndUnitType(classId, "4");
                contentList = DataTableToListHelper<Course_UnitContent>.ConvertToModel(unitContentBll.GetUnitByClassAndUnitType(classId, "4"));
                if (contentList != null)
                {
                    var homeworkBll = new Course_UnitHomeWorkBLL();
                    double homeworkscore = 0;
                    foreach (var item in contentList)
                    {
                        var scheduleModel = homeworkBll.GetList("Delflag=0 and ClassId=" + classId + " and AccountId=" + accountId + " and UnitContent=" + item.Id, "");
                        if (scheduleModel != null && scheduleModel.Count > 0)
                        {
                            if (scheduleModel.First().Score.HasValue)//如果有值说明打过分
                            {
                                homeworkscore += (scheduleModel.First().Score.Value * (cousre_detail[0].HomeWorkRate) / (total * 100)).ToDouble();//已打分数与总数折算
                            }
                        }
                    }

                    memberModel.HomeWorkScore = homeworkscore;
                }

                //重新计算测试分数

                total = unitContentBll.GetUnitCountByClassAndUnitType(classId, "5");
                contentList = DataTableToListHelper<Course_UnitContent>.ConvertToModel(unitContentBll.GetUnitByClassAndUnitType(classId, "5"));
                if (contentList != null)
                {
                    var scheduleBll = new Member_ClassUnitContentScheduleBLL();
                    double testscore = 0;
                    foreach (var item in contentList)
                    {
                        var scheduleModel = scheduleBll.GetList("Delflag=0 and ClassId=" + classId + " and AccountId=" + accountId + " and UnitContent=" + item.Id, "");
                        if (scheduleModel != null && scheduleModel.Count > 0)
                        {
                            if (scheduleModel.First().score.HasValue)
                            {
                                testscore += (scheduleModel.First().score.Value * (cousre_detail[0].QuestionRate) / (total * 100)).ToDouble();
                            }
                        }
                    }
                    memberModel.TestingScore = testscore;
                }

                //重新计算考试分数

                total = unitContentBll.GetUnitCountByClassAndUnitType(classId, "6");
                contentList = DataTableToListHelper<Course_UnitContent>.ConvertToModel(unitContentBll.GetUnitByClassAndUnitType(classId, "6"));
                if (contentList != null)
                {
                    var scheduleBll = new Member_ClassUnitContentScheduleBLL();
                    double examcore = 0;
                    foreach (var item in contentList)
                    {
                        var scheduleModel = scheduleBll.GetList("Delflag=0 and ClassId=" + classId + " and AccountId=" + accountId + " and UnitContent=" + item.Id, "");
                        if (scheduleModel != null && scheduleModel.Count > 0)
                        {
                            if (scheduleModel.First().score.HasValue)
                            {
                                examcore += (scheduleModel.First().score.Value * (cousre_detail[0].TestingRate) / (total * 100)).ToDouble();
                            }
                        }
                    }
                    memberModel.ExaminationScore = examcore;
                }
                #endregion

                member_classBll.Update(memberModel);
            }
        }
    }
}