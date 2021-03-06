﻿$(function () {
    $proto.init(function () {
        /* 页面加载后立即执行的代码写在这里 */

        var SectionModel = $("#SectionModel");
        var ChapterModel = $("#ChapterModel div:eq(0)");
        var TestMode = $("#TestMode");
       
        var obj = JSON.stringify({ TrainingId: TrainingId, ParentId: 0 });
        $.ajax({
            cache: false,
            async: false,
            type: 'post',
            contentType: 'application/json',
            url: '/CourseCreate/LearnChapterSectionInfo',
            data: obj,
            dataType: 'json',
            success: function (data) {
                $("#CourseInfo").html(Loading);

                if (!data || !data.Data || data.Data.length == 0) {
                    $("#CourseInfo").html(noDataTip);
                    return;
                }

                var htmls = "";

                for (var i = 0; i < data.Data.length; i++) {
                    var eleCon = ChapterModel.clone(true);

                    eleCon.find("#ChapTitle").html(data.Data[i]["Title"]);
                    eleCon.find("#TotalTimeLength").html(GetTotalTimeLength(data.Data[i]["Id"]));
                    eleCon.find("#ChapContent").attr("style", "background-color:transparent;overflow:hidden;font-size:16px;border:none;word-break:break-all; word-wrap:break-word;");
                    eleCon.find("#ChapContent").html(data.Data[i]["Content"]);
                    if (GetChapterSectionActivity(data.Data[i]["Id"]) != "") {
                        eleCon.find("#ChapterActivity").html(GetChapterSectionActivity(data.Data[i]["Id"]));
                    }
                    else {
                        eleCon.find("#ChapLecture").remove();
                    }
                    if (GetSectionContent(data.Data[i]["Id"]) != "") {
                        eleCon.find("#SectionContent").html(GetSectionContent(data.Data[i]["Id"]));
                    }
                    else {
                        eleCon.find("#SecLecture").remove();
                    }

                    htmls += $(eleCon)[0].outerHTML;
                }

                //增加结业考试HTML
                htmls += GetExamContent(TrainingId);

                $("#CourseInfo").html(htmls);
            },
            error: function () {
            }
        });
    });
});

//睡眠函数
function sleep(numberMillis) {
    var now = new Date();
    var exitTime = now.getTime() + numberMillis;
    while (true) {
        now = new Date();
        if (now.getTime() > exitTime)
            return;
    }
}

//获取指定章下面节HTML
function GetSectionContent(ParentID) {
    var htmls = "";
    var SectionModel = $("#SectionModel div:eq(0)");

    var obj = JSON.stringify({ TrainingId: TrainingId, ParentID: ParentID });
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/CourseCreate/LearnChapterSectionInfo',
        data: obj,
        dataType: 'json',
        success: function (data) {
            if (!data || !data.Data || data.Data.length == 0) {
                return;
            }

            for (var i = 0; i < data.Data.length; i++) {
                var eleCon = SectionModel.clone(true);

                eleCon.find("#SecTitle").html(data.Data[i]["Title"]);
                eleCon.find("#SectionActivity").html(GetChapterSectionActivity(data.Data[i]["Id"]));

                htmls += $(eleCon)[0].outerHTML;
            }
        },
        error: function () {
        }
    });

    return htmls;
}

//获取指定章节总时长
function GetTotalTimeLength(TimeLengthId) {
    var TotalTimeLength;
    var obj = JSON.stringify({ TimeLengthId: TimeLengthId });
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/CourseCreate/LearnTotalTimeLength',
        data: obj,
        dataType: 'json',
        success: function (data) {
            if (!data || !data.Data || data.Data.length == 0) {
                TotalTimeLength = 0;
            }

            TotalTimeLength = data.Data;
        },
        error: function () {
        }
    });

    return TotalTimeLength;
}

//获取章节单元活动HTML
function GetChapterSectionActivity(UnitID) {
    var htmls = "";
    var ActivityModel = $("#ActivityModel div:eq(0)");

    var obj = JSON.stringify({ UnitID: UnitID, ClassId: ClassId });
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/CourseCreate/ActivityLearn',
        data: obj,
        dataType: 'json',
        success: function (data) {
            if (!data || !data.Data || data.Data.length == 0) {
                return;
            }

            for (var i = 0; i < data.Data.length; i++) {
                if (data.Data[i]["UnitType"] != 6) {
                    var eleCon = ActivityModel.clone(true);
                    if (data.Data[i]["Status"]) {
                        eleCon.find("#Complete").addClass("complete");
                    }

                    eleCon.find("#AcTitle").html(data.Data[i]["Title"]);
                    eleCon.find("#AcTitle").attr("style", "width:700px;white-space:nowrap;display:inline-block;overflow:hidden;text-overflow:ellipsis;");
                    eleCon.find("#AcTitle").attr("title", data.Data[i]["Title"]);
                    eleCon.find("#MCount").html("以学习" + data.Data[i]["Mcrcount"] + "次");
                    switch (data.Data[i]["UnitType"]) {
                        case 1:
                            {
                                if (new Date(new Date().toDateString()) < new Date(Date.parse(data_string(data.Data[i]["OpenDate"]))) || new Date(new Date().toDateString()) > new Date(Date.parse(data_string(data.Data[i]["EndDate"])))) {
                                    eleCon.find("#AcTitle").attr("href", "javascript:void(0);");

                                    if (new Date(new Date().toDateString()) < new Date(Date.parse(data_string(data.Data[i]["OpenDate"])))) {
                                        eleCon.find("#AcTitle").attr("onclick", "Alert('此活动未开放!');");
                                    }
                                    else if (new Date(new Date().toDateString()) > new Date(Date.parse(data_string(data.Data[i]["EndDate"])))) {
                                        eleCon.find("#AcTitle").attr("onclick", "Alert('此活动已截止!');");
                                    }

                                }
                                else {
                                    eleCon.find("#AcTitle").attr("href", "/Learn/LearnOnLine/LearnOnLineReadingView?TrainingId=" + UrlEncode(TrainingId) + "&UnitContent=" + UrlEncode(data.Data[i]["Id"]) + "&ClassId=" + UrlEncode(ClassId));
                                }
                                eleCon.find("#AcType").html("阅读");
                                break;
                            }
                        case 2:
                            {
                                if (new Date(new Date().toDateString()) < new Date(Date.parse(data_string(data.Data[i]["OpenDate"]))) || new Date(new Date().toDateString()) > new Date(Date.parse(data_string(data.Data[i]["EndDate"])))) {
                                    eleCon.find("#AcTitle").attr("href", "javascript:void(0);");
                                    if (new Date(new Date().toDateString()) < new Date(Date.parse(data_string(data.Data[i]["OpenDate"])))) {
                                        eleCon.find("#AcTitle").attr("onclick", "Alert('此活动未开放!');");
                                    }
                                    else if (new Date(new Date().toDateString()) > new Date(Date.parse(data_string(data.Data[i]["EndDate"])))) {
                                        eleCon.find("#AcTitle").attr("onclick", "Alert('此活动已截止!');");
                                    }
                                }
                                else {
                                    eleCon.find("#AcTitle").attr("href", "/Learn/LearnOnLine/LearnOnLineVideoView?TrainingId=" + UrlEncode(TrainingId) + "&UnitContent=" + UrlEncode(data.Data[i]["Id"]) + "&ClassId=" + UrlEncode(ClassId));
                                }
                                eleCon.find("#AcType").html("<span class='glyphicon glyphicon-facetime-video'>");
                                break;
                            }
                        case 3:
                            {
                                if (new Date(new Date().toDateString()) < new Date(Date.parse(data_string(data.Data[i]["OpenDate"]))) || new Date(new Date().toDateString()) > new Date(Date.parse(data_string(data.Data[i]["EndDate"])))) {
                                    eleCon.find("#AcTitle").attr("href", "javascript:void(0);");
                                    if (new Date(new Date().toDateString()) < new Date(Date.parse(data_string(data.Data[i]["OpenDate"])))) {
                                        eleCon.find("#AcTitle").attr("onclick", "Alert('此活动未开放!');");
                                    }
                                    else if (new Date(new Date().toDateString()) > new Date(Date.parse(data_string(data.Data[i]["EndDate"])))) {
                                        eleCon.find("#AcTitle").attr("onclick", "Alert('此活动已截止!');");
                                    }
                                }
                                else {
                                    eleCon.find("#AcTitle").attr("href", "/Learn/LearnOnLine/LearnOnLineDiscussView?TrainingId=" + UrlEncode(TrainingId) + "&UnitContent=" + UrlEncode(data.Data[i]["Id"]) + "&ClassId=" + UrlEncode(ClassId));
                                }
                                eleCon.find("#AcType").html("讨论");
                                break;
                            }
                        case 4:
                            {
                                if (new Date(new Date().toDateString()) < new Date(Date.parse(data_string(data.Data[i]["OpenDate"]))) || new Date(new Date().toDateString()) > new Date(Date.parse(data_string(data.Data[i]["EndDate"])))) {
                                    eleCon.find("#AcTitle").attr("href", "javascript:void(0);");
                                    if (new Date(new Date().toDateString()) < new Date(Date.parse(data_string(data.Data[i]["OpenDate"])))) {
                                        eleCon.find("#AcTitle").attr("onclick", "Alert('此活动未开放!');");
                                    }
                                    else if (new Date(new Date().toDateString()) > new Date(Date.parse(data_string(data.Data[i]["EndDate"])))) {
                                        eleCon.find("#AcTitle").attr("onclick", "Alert('此活动已截止!');");
                                    }
                                }
                                else {
                                    eleCon.find("#AcTitle").attr("href", "/Learn/LearnOnLine/LearnOnLineTaskView?TrainingId=" + UrlEncode(TrainingId) + "&UnitContent=" + UrlEncode(data.Data[i]["Id"]) + "&ClassId=" + UrlEncode(ClassId));
                                }
                                eleCon.find("#AcType").html("作业");
                                break;
                            }
                        case 5:
                            {
                                if (new Date(new Date().toDateString()) < new Date(Date.parse(data_string(data.Data[i]["OpenDate"]))) || new Date(new Date().toDateString()) > new Date(Date.parse(data_string(data.Data[i]["EndDate"])))) {
                                    eleCon.find("#AcTitle").attr("href", "javascript:void(0);");
                                    if (new Date(new Date().toDateString()) < new Date(Date.parse(data_string(data.Data[i]["OpenDate"])))) {
                                        eleCon.find("#AcTitle").attr("onclick", "Alert('此活动未开放!');");
                                    }
                                    else if (new Date(new Date().toDateString()) > new Date(Date.parse(data_string(data.Data[i]["EndDate"])))) {
                                        eleCon.find("#AcTitle").attr("onclick", "Alert('此活动已截止!');");
                                    }
                                }
                                else {
                                    eleCon.find("#AcTitle").attr("href", "/Learn/LearnOnLine/LearnOnLineQuizView?TrainingId=" + UrlEncode(TrainingId) + "&UnitContent=" + UrlEncode(data.Data[i]["Id"]) + "&ClassId=" + UrlEncode(ClassId));
                                }
                                eleCon.find("#AcType").html("测试");
                                break;
                            }
                        default:
                            {
                                eleCon.find("#AcType").html("-");
                                break;
                            }
                    }
                    eleCon.find("#AcTimeLength").html(data.Data[i]["TimeLength"]);

                    htmls += $(eleCon)[0].outerHTML;
                }
            }
        },
        error: function () {
        }
    });
    return htmls;
}

function data_string(str) {
    var d = eval('new ' + str.substr(1, str.length - 2));
    var ar_date = [d.getFullYear(), d.getMonth() + 1, d.getDate()];
    for (var i = 0; i < ar_date.length; i++) ar_date[i] = dFormat(ar_date[i]);
    return ar_date.join('/');

    function dFormat(i) { return i < 10 ? "0" + i.toString() : i; }
}

//获取指定章的结业考试
function GetExamContent(TrainingId) {
    var htmls = "";
    var ExamModel = $("#ExamModel div:eq(0)");

    var obj = JSON.stringify({ TrainingId: TrainingId, ClassId: ClassId });
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/CourseCreate/LearnExamInfo',
        data: obj,
        dataType: 'json',
        success: function (data) {
            if (!data || !data.Data || data.Data.length == 0) {
                return;
            }

            var eleCon = ExamModel.clone(true);
            if (data.Data[0]["Status"]) {
                eleCon.find("#Complete").addClass("complete");
            }
            eleCon.find("#ExamTitle1").html(data.Data[0]["Title"]);
            eleCon.find("#ExamTitle2").html(data.Data[0]["Title"]);

            if (new Date(new Date().toDateString()) < new Date(Date.parse(data_string(data.Data[0]["OpenDate"]))) || new Date(new Date().toDateString()) > new Date(Date.parse(data_string(data.Data[0]["EndDate"])))) {
                //结业考试未开放和未截止相应处理
                eleCon.find("#ExamTitle2").attr("href", "javascript:void(0);");
                if (new Date(new Date().toDateString()) < new Date(Date.parse(data_string(data.Data[0]["OpenDate"])))) {
                    eleCon.find("#ExamTitle2").attr("onclick", "Alert('此结业考试未开放!');");
                }
                else if (new Date(new Date().toDateString()) > new Date(Date.parse(data_string(data.Data[0]["EndDate"])))) {
                    eleCon.find("#ExamTitle2").attr("onclick", "Alert('此结业考试已截止!');");
                }
            }
            else {
                //是否学完课程活动相应处理                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
                if (data.Data[0]["FinalTestLimit"]) {
                    if (!IsAllLearn(TrainingId, ClassId)) {
                        eleCon.find("#ExamTitle2").attr("onclick", "Alert('请学习完本课程所有内容!');");
                    }
                    else {
                        eleCon.find("#ExamTitle2").attr("onclick", "ExamClick(" + TrainingId + "," + data.Data[0]["Id"] + "," + ClassId + ");");
                    }
                }
                else {
                    eleCon.find("#ExamTitle2").attr("onclick", "ExamClick(" + TrainingId + "," + data.Data[0]["Id"] + "," + ClassId + ");");
                }
            }

            eleCon.find("#ExamTimeLength").html(data.Data[0]["TimeLength"]);
            eleCon.find("#ExamQuesCnt").html(ExamQuesCnt);

            htmls += $(eleCon)[0].outerHTML;
        },
        error: function () {
        }
    });

    return htmls;
}

//获取是否学完所有课程
function IsAllLearn(TrainingId, ClassId) {
    var IsAllLearn;
    var obj = JSON.stringify({ TrainingId: TrainingId, ClassId: ClassId });
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/CourseCreate/IsAllLearn',
        data: obj,
        dataType: 'json',
        success: function (data) {
            if (!data || !data.Data || data.Data.length == 0) {
                return;
            }

            IsAllLearn = data.Data;
        },
        error: function () {
        }
    });
    return IsAllLearn;
}

//结业考试单击事件
function ExamClick(TrainingId, UnitContent, ClassId) {
    var Score = $("#Score").val();
    if (Score > 0) {
        Alert("讲师或辅导员已打分,不能再次测试");
    }
    else {
        var ExamAlert = "进入结业考试界面,请在规定时间内完成答题,否则会影响考试分数,如已测试,再次测试会覆盖原先分数,是否确认进入？";
        $("#btnConfirm").attr("onclick", "location.href='/Learn/LearnOnLine/LearnOnLineExamView?TrainingId=" + UrlEncode(TrainingId) + "&UnitContent=" + UrlEncode(UnitContent) + "&ClassId=" + UrlEncode(ClassId) + "'");

        $('#modal-edit-status .modal-title').text(ExamAlert);
        $('#modal-edit-status').modal({
            keyboard: false,
            backdrop: 'static'
        });
    }
}

function doHideLecture() {
    var eo = $d.elem($d.evt());
    if (eo.tagName.toLowerCase() != 'a') {
        eo = eo.parentNode;
    }
    var e1 = $(eo);
    var e2 = e1.parent().parent().find('.lecture');
    if (e2.css('display') == 'block') {
        e1.html('<span class="glyphicon glyphicon-chevron-down"></span>&nbsp;显示 ' + e2.size() + ' 项内容');
        e2.hide();
    } else {
        e1.html('<span class="glyphicon glyphicon-chevron-up"></span>&nbsp;隐藏');
        e2.show();
    }
}