$(function () {
    $proto.init(function () {
        /* 页面加载后立即执行的代码写在这里 */

        var SectionModel = $("#SectionModel");
        var ChapterModel = $("#ChapterModel div:eq(0)");
        var TestMode = $("#TestMode");

        var obj = JSON.stringify({ TrainingId:TrainingId,ParentId: 0 });
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

    var obj = JSON.stringify({ UnitID: UnitID });
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/CourseCreate/LearnActivityInfo',
        data: obj,
        dataType: 'json',
        success: function (data) {
            if (!data || !data.Data || data.Data.length == 0) {
                return;
            }

            for (var i = 0; i < data.Data.length; i++) {
                if (data.Data[i]["UnitType"] != 6) {
                    var eleCon = ActivityModel.clone(true);

                    eleCon.find("#AcTitle").html(data.Data[i]["Title"]);
                    eleCon.find("#AcTitle").attr("style", "width:800px;white-space:nowrap;display:inline-block;overflow:hidden;text-overflow:ellipsis;");
                    eleCon.find("#AcTitle").attr("title", data.Data[i]["Title"]);

                    switch (data.Data[i]["UnitType"]) {
                        case 1:
                            {
                                eleCon.find("#AcTitle").attr("href", "/Learn/LearnOnLinePreview/LearnOnLineReadingPreviewView?TrainingId=" + UrlEncode(TrainingId) + "&UnitContent=" + UrlEncode(data.Data[i]["Id"]));
                                eleCon.find("#AcType").html("阅读");
                                break;
                            }
                        case 2:
                            {
                                eleCon.find("#AcTitle").attr("href", "/Learn/LearnOnLinePreview/LearnOnLineVideoPreviewView?TrainingId=" + UrlEncode(TrainingId) + "&UnitContent=" + UrlEncode(data.Data[i]["Id"]));
                                eleCon.find("#AcType").html("<span class='glyphicon glyphicon-facetime-video'>");
                                break;
                            }
                        case 3:
                            {
                                eleCon.find("#AcTitle").attr("href", "/Learn/LearnOnLinePreview/LearnOnLineDiscussPreviewView?TrainingId=" + UrlEncode(TrainingId) + "&UnitContent=" + UrlEncode(data.Data[i]["Id"]));
                                eleCon.find("#AcType").html("讨论");
                                break;
                            }
                        case 4:
                            {
                                eleCon.find("#AcTitle").attr("href", "/Learn/LearnOnLinePreview/LearnOnLineTaskPreviewView?TrainingId=" + UrlEncode(TrainingId) + "&UnitContent=" + UrlEncode(data.Data[i]["Id"]));
                                eleCon.find("#AcType").html("作业");
                                break;
                            }
                        case 5:
                            {
                                eleCon.find("#AcTitle").attr("href", "/Learn/LearnOnLinePreview/LearnOnLineQuizPreviewView?TrainingId=" + UrlEncode(TrainingId) + "&UnitContent=" + UrlEncode(data.Data[i]["Id"]));
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

//获取指定章的结业考试
function GetExamContent(TrainingId) {
    var htmls = "";
    var ExamModel = $("#ExamModel div:eq(0)");

    var obj = JSON.stringify({ TrainingId: TrainingId });
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/CourseCreate/ExamLearnInfo',
        data: obj,
        dataType: 'json',
        success: function (data) {
            if (!data || !data.Data || data.Data.length == 0) {
                return;
            }

            var eleCon = ExamModel.clone(true);
            eleCon.find("#ExamTitle1").html(data.Data[0]["Title"]);
            eleCon.find("#ExamTitle2").html(data.Data[0]["Title"]);
            eleCon.find("#ExamTitle2").attr("href", "/Learn/LearnOnLinePreview/LearnOnLineExamPreviewView?TrainingId=" + UrlEncode(TrainingId) + "&UnitContent=" + UrlEncode(data.Data[0]["Id"]));
            eleCon.find("#ExamTimeLength").html(data.Data[0]["TimeLength"]);
            eleCon.find("#ExamQuesCnt").html(ExamQuesCnt);

            htmls += $(eleCon)[0].outerHTML;
        },
        error: function () {
        }
    });

    return htmls;
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