﻿$(function () {
    $proto.init(function () {
        /* 页面加载后立即执行的代码写在这里 */
        //$('input.input-date').datepicker({});

        showQuesList();

        //题型下拉框索引更改事件
        $('#ddlQuesType').change(function () {
            //清空所有提示文本
            $('.input-valid').each(function () {
                $(this).css('display', 'none');
                var validFields = $(this).attr('validfor').split(' ');
                for (var i = 0; i < validFields.length; i++) {
                    $(this).css('display', 'none');
                    result = false; 
                }
            });

            //增加试题界面初始化
            $("#txtContent").val('');
            $("#txtContent1").val('');
            $("#txtCredit").val('');

            //根据题目类型显示相应HTML
            if ($('#ddlQuesType').val() == 3) {
                $("#txtContent1").val("shit");
                $("#Ques1").attr("style", "display:block;");
                $("#Ques2").attr("style", "display:none;");
                $("#Score").attr("style", "display:block;");
                $("#SelectQues").attr("style", "display:none;");
                $("#IsOrNoQues").attr("style", "display:block;");
                $("#QuesQues").attr("style", "display:none;");
            }
            else if ($('#ddlQuesType').val() == 4)
            {
                $("#txtContent").val("shit");
                $("#Ques1").attr("style", "display:none;");
                $("#Ques2").attr("style", "display:block;");
                $("#Score").attr("style", "display:block;");
                $("#SelectQues").attr("style", "display:none;");
                $("#IsOrNoQues").attr("style", "display:none;");
                $("#QuesQues").attr("style", "display:block;");
            }
            else {
                $("#txtContent1").val("shit");
                $("#Ques1").attr("style", "display:block;");
                $("#Ques2").attr("style", "display:none;");
                $("#Score").attr("style", "display:block;");
                $("#SelectQues").attr("style", "display:block;");
                $("#IsOrNoQues").attr("style", "display:none;");
                $("#QuesQues").attr("style", "display:none;");
            }

            //若选择的提醒为非判断题,则初始化答案文本
            $("#d_quesoptions").html(OptionHtml);
        });

        //新增按钮单击事件
        $('#btnAdd').click(function () {
            //验证必填表单
            var result = true;

            $('.input-valid').each(function () {
                $(this).css('display', 'none');
                var validFields = $(this).attr('validfor').split(' ');
                for (var i = 0; i < validFields.length; i++) {
                    var field = validFields[i];

                    if ($("#ddlQuesType").val() != 4) {
                        //验证必填
                        if (Trim($('#' + field).val()) == '' && $(this).attr('validtype') != 'number') {
                            $(this).css('display', 'inline-block');
                            result = false;
                            break;
                        }
                    }
                    else {
                        //验证必填
                        if (Trim($('#' + field).val()) == '' && $(this).attr('validtype') == 'Ques') {
                            $(this).css('display', 'inline-block');
                            result = false;
                            break;
                        }
                    }

                    //验证数字
                    if (Trim($('#' + field).val()) != '' && $(this).attr('validtype') == 'number' && !(/^[1-9]*[1-9][0-9]*$/).test($('#' + field).val())) {
                        $(this).css('display', 'inline-block');
                        result = false;
                        break;
                    }
                }
            });

            if (!result) {
                return;
            }

            var Question = "", Answer = "";
            if ($("#ddlQuesType").val() == 3) {
                Question = "[{Id:1,Content:'1'},{Id:2,Content:'0'}]";
                Answer = $("#radioRight").is(":checked") ? 1 : 2;
            }
            else if ($("#ddlQuesType").val() == 4)
            {
                Question = "";
            }
            else {
                var eleCon = $("#d_quesoptions").clone(true);
                var Options = eleCon.find("#Option");

                for (i = 0; i < Options.length; i++) {
                    var Option = Options[i];

                    if (Trim($(Option).find("#QtContent").val()) == '') {
                        Alert('请填写第' + (i + 1) + '项答案内容！');
                        $(Option).find("#QtContent").focus();
                        return false;
                    }

                    //问题答案内容
                    Question += "{";
                    Question += "Id:" + (i + 1) + ","
                    Question += "Content:'" + $(Option).find("#QtContent").val();
                    Question += "'},";

                    //问题答案
                    if ($(Option).find("#chkRight").is(":checked")) {
                        Answer += (i + 1) + ",";
                    }
                }

                if (Answer == "") {
                    Alert('请勾选复选框设置答案！');
                    return false;
                }
                else {
                    Answer = Answer.substr(0, Answer.length - 1);

                    //若是单选,则只能设置一个答案
                    if ($("#ddlQuesType").val() == 1) {
                        var Answers = Answer.split(',');
                        if (Answers.length > 1) {
                            Alert('单选题只能设置一个答案！');
                            return false;
                        }
                    }
                    else if ($("#ddlQuesType").val() == 2) {
                        var Answers = Answer.split(',');
                        if (Answers.length < 2) {
                            Alert('多选题至少设置两个答案！');
                            return false;
                        }
                    }
                }

                Question = Question.substr(0, Question.length - 1);
                Question = "[" + Question + "]";
            }

            $("#btnAdd").attr("disabled", "false");
            var obj = JSON.stringify({ TrainingId: TrainingId, QTtype: $("#ddlQuesType").val(), Content: ($("#ddlQuesType").val() == 4?HtmlEncode($("#txtContent1").val()):HtmlEncode($("#txtContent").val())), Question: Question, Answer: Answer, Credit: $("#txtCredit").val() });
            $.ajax({
                cache: false,
                async: false,
                type: 'post',
                contentType: 'application/json',
                url: '/CourseCreate/AddEaxmQues',
                data: obj,
                dataType: 'json',
                success: function (data) {
                    if ($('#ddlQuesType').val() == 4) {
                        $("#txtContent").val('shit');
                        $("#txtCredit").val('1');
                    }
                    else {
                        $("#txtContent").val('');
                        $("#txtCredit").val('');
                    }
                    
                    $("#d_quesoptions").html(OptionHtml);
                    Alert(data.Msg);

                    showQuesList();
                },
                error: function () {
                }
            });
            $("#btnAdd").removeAttr("disabled");
        });

        //保存按钮单击事件
        $('#btnEdit').click(function () {
            //验证必填表单
            var result = true;
            $('.input-valid').each(function () {
                $(this).css('display', 'none');
                var validFields = $(this).attr('validfor').split(' ');
                for (var i = 0; i < validFields.length; i++) {
                    var field = validFields[i];

                    //验证必填
                    if (Trim($('#' + field).val()) == '' && $(this).attr('validtype') != 'number') {
                        $(this).css('display', 'inline-block');
                        result = false;
                        break;
                    }

                    //验证数字
                    if (Trim($('#' + field).val()) != '' && $(this).attr('validtype') == 'number' && !(/^[1-9]*[1-9][0-9]*$/).test($('#' + field).val())) {
                        $(this).css('display', 'inline-block');
                        result = false;
                        break;
                    }
                }
            });

            if (!result) {
                return;
            }

            var Question = "", Answer = "";
            if ($("#ddlQuesType").val() == 3) {
                Question = "[{Id:1,Content:'1'},{Id:2,Content:'0'}]";
                Answer = $("#radioRight").is(":checked") ? 1 : 2;
            }
            else if ($("#ddlQuesType").val() == 4) {
                Question = "";
            }
            else {
                var eleCon = $("#d_quesoptions").clone(true);
                var Options = eleCon.find("#Option");

                for (i = 0; i < Options.length; i++) {
                    var Option = Options[i];

                    if (Trim($(Option).find("#QtContent").val()) == '') {
                        Alert('请填写第' + (i + 1) + '项答案内容！');
                        $(Option).find("#QtContent").focus();
                        return false;
                    }

                    //问题答案内容
                    Question += "{";
                    Question += "Id:" + (i + 1) + ","
                    Question += "Content:'" + $(Option).find("#QtContent").val();
                    Question += "'},";

                    //问题答案
                    if ($(Option).find("#chkRight").is(":checked")) {
                        Answer += (i + 1) + ",";
                    }
                }

                if (Answer == "") {
                    Alert('请勾选复选框设置答案！');
                    return false;
                }
                else {
                    Answer = Answer.substr(0, Answer.length - 1);

                    //若是单选,则只能设置一个答案
                    if ($("#ddlQuesType").val() == 1) {
                        var Answers = Answer.split(',');
                        if (Answers.length > 1) {
                            Alert('单选题只能设置一个答案！');
                            return false;
                        }
                    }
                }

                Question = Question.substr(0, Question.length - 1);
                Question = "[" + Question + "]";
            }

            $("#btnEdit").attr("disabled", "false");
            var obj = JSON.stringify({ Id: $("#TestId").val(), QTtype: $("#ddlQuesType").val(), Content: ($("#ddlQuesType").val() == 4 ? HtmlEncode($("#txtContent1").val()) : HtmlEncode($("#txtContent").val())), Question: Question, Answer: Answer, Credit: $("#txtCredit").val() });
            $.ajax({
                cache: false,
                async: false,
                type: 'post',
                contentType: 'application/json',
                url: '/CourseCreate/EditUnitTest',
                data: obj,
                dataType: 'json',
                success: function (data) {
                    Alert(data.Msg);
                    showQuesList();
                },
                error: function () {
                }
            });
            $("#btnEdit").removeAttr("disabled");
        });
    });
});

function showQuesList() {
    var quesModel = $("#quesModel div:eq(0)");

    var obj = JSON.stringify({ TrainingId: TrainingId });
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/CourseCreate/ExamQuesInfo',
        data: obj,
        dataType: 'json',
        success: function (data) {
            $("#queslist").html(Loading);

            if (!data || !data.Data || data.Data.length == 0) {
                $("#QuesCount").html(0);
                $("#queslist").html(noDataTip);
                return;
            }

            $("#QuesCount").html(data.Data.length);

            var htmls = "";

            for (var i = 0; i < data.Data.length; i++) {
                var eleCon = quesModel.clone(true);

                switch (data.Data[i]["QTtype"]) {
                    case 1:
                        {
                            eleCon.find("#Content").html(data.Data[i]["Content"] + '(单选题)');
                            break;
                        }
                    case 2:
                        {
                            eleCon.find("#Content").html(data.Data[i]["Content"] + '(多选题)');
                            break;
                        }
                    case 3:
                        {
                            eleCon.find("#Content").html(data.Data[i]["Content"] + '(判断题)');
                            break;
                        }
                    case 4:
                        {
                            eleCon.find("#Content").html(data.Data[i]["Content"] + '(问答题)');
                            break;
                        }
                }

                eleCon.find("#Content").attr("onclick", "EditQuesDetail(this," + data.Data[i]["Id"] + ");");

                htmls += $(eleCon)[0].outerHTML;
            }

            $("#queslist").html(htmls);
        },
        error: function () {
        }
    });
}

//追加答案项
function doAddOptions() {
    var s = '';
    s += '<div id="Option" class="option">';
    s += '<label class="chk" style="width:70px;">';
    s += '<input id ="chkRight" type="checkbox" name="a1" value="1" checked> 正确';
    s += '</label>';
    s += '<input id = "QtContent" type="text" class="form-control" value="" style="display:inline-block;width:450px;">';
    s += '<a href="#" class="glyphicon glyphicon-trash" onclick="$(this).parent().remove();"></a>';
    s += '</div>';

    $('#d_quesoptions').append(s);
}

//增加测试题目界面显示
function ShowQuesDetail(QuesType, obj) {
    //清空所有提示文本
    $('.input-valid').each(function () {
        $(this).css('display', 'none');
        var validFields = $(this).attr('validfor').split(' ');
        for (var i = 0; i < validFields.length; i++) {
            $(this).css('display', 'none');
            result = false;
        }
    });

    $("#ddlQuesType").val(QuesType);

    //增加试题界面初始化
    $("#txtContent").val('');
    $("#txtContent1").val('');
    $("#txtCredit").val('');
    $("#d_quesoptions").html(OptionHtml);

    //根据题目类型显示相应HTML
    if (QuesType == 3) {
        $("#txtContent1").val("shit");
        $("#Ques1").attr("style", "display:block;");
        $("#Ques2").attr("style", "display:none;");
        $("#Score").attr("style", "display:block;");
        $("#SelectQues").attr("style", "display:none;");
        $("#IsOrNoQues").attr("style", "display:block;");
        $("#QuesQues").attr("style", "display:none;");
    }
    else if (QuesType == 4)
    {
        $("#txtContent").val("shit");
        $("#Ques1").attr("style", "display:none;");
        $("#Ques2").attr("style", "display:block;");
        $("#Score").attr("style", "display:block;");
        $("#SelectQues").attr("style", "display:none;");
        $("#IsOrNoQues").attr("style", "display:none;");
        $("#QuesQues").attr("style", "display:block;");
    }
    else {
        $("#txtContent1").val("shit");
        $("#Ques1").attr("style", "display:block;");
        $("#Ques2").attr("style", "display:none;");
        $("#Score").attr("style", "display:block;");
        $("#SelectQues").attr("style", "display:block;");
        $("#IsOrNoQues").attr("style", "display:none;");
        $("#QuesQues").attr("style", "display:none;");
    }

    $("#btnAdd").attr("style", "width:100px;display:block;");
    $("#btnEdit").attr("style", "width:100px;display:none;");
    $("#btnDelete").attr("style", "width:100px;display:none;");

    $('#d_ques').show();
}

//编辑测试题目界面显示
function EditQuesDetail(obj, id) {
    $("#btnDelete").attr("style", "width:100px;display:block;");

    var obj = JSON.stringify({ id: id });
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/CourseCreate/ExamQuesModel',
        data: obj,
        dataType: 'json',
        success: function (data) {
            if (!data || !data.Data || data.Data.length == 0) {
                return;
            }

            $("#btnAdd").attr("style", "width:100px;display:none;");
            $("#btnEdit").attr("style", "width:100px;display:block;");

            $("#TestId").val(data.Data["Id"]);
            $("#ddlQuesType").val(data.Data["QTtype"]);
            $("#txtContent").val(HtmlDecode(data.Data["Content"]));
            $("#txtContent1").val(HtmlDecode(data.Data["Content"]));
            $("#txtCredit").val(data.Data["Credit"]);
            $("#ddlQuesType").val(data.Data["QTtype"]);

            switch (data.Data["QTtype"]) {
                case 1:
                    {
                        $("#txtContent1").val("shit");
                        $("#Ques1").attr("style", "display:block;");
                        $("#Ques2").attr("style", "display:none;");
                        $("#Score").attr("style", "display:block;");
                        $("#SelectQues").attr("style", "display:block;");
                        $("#IsOrNoQues").attr("style", "display:none;");
                        $("#QuesQues").attr("style", "display:none;");

                        $("#d_quesoptions").html(GetOptionListHtml(data.Data["Question"], data.Data["Answer"]));

                        break;
                    }
                case 2:
                    {
                        $("#txtContent1").val("shit");
                        $("#Ques1").attr("style", "display:block;");
                        $("#Ques2").attr("style", "display:none;");
                        $("#Score").attr("style", "display:block;");
                        $("#SelectQues").attr("style", "display:block;");
                        $("#IsOrNoQues").attr("style", "display:none;");
                        $("#QuesQues").attr("style", "display:none;");

                        $("#d_quesoptions").html(GetOptionListHtml(data.Data["Question"], data.Data["Answer"]));

                        break;
                    }
                case 3:
                    {
                        if (data.Data["Answer"] == 1) {
                            //$("#radioNoPrintScore").removeAttr("checked");
                            //$("#radioRight").attr("checked", "checked");
                            $("#radioRight").click();
                        }
                        else {
                            //$("#radioRight").removeAttr("checked");
                            //$("#radioNoPrintScore").attr("checked", "checked");
                            $("#radioNoPrintScore").click();
                        }

                        //显示答案列表
                        $("#txtContent1").val("shit");
                        $("#Ques1").attr("style", "display:block;");
                        $("#Ques2").attr("style", "display:none;");
                        $("#Score").attr("style", "display:block;");
                        $("#SelectQues").attr("style", "display:none;");
                        $("#IsOrNoQues").attr("style", "display:block;");
                        $("#QuesQues").attr("style", "display:none;");
                        break;
                    }
                case 4:
                    {
                        //显示答案列表
                        $("#txtContent").val("shit");
                        $("#Ques1").attr("style", "display:none;");
                        $("#Ques2").attr("style", "display:block;");
                        $("#Score").attr("style", "display:block;");
                        $("#SelectQues").attr("style", "display:none;");
                        $("#IsOrNoQues").attr("style", "display:none;");
                        $("#QuesQues").attr("style", "display:block;");
                        break;
                    }
            }

            $('#d_ques').show();
        },
        error: function () {
        }
    });
}

//获取答案列表Html
function GetOptionListHtml(Question, Answer) {
    //获取答案选项HTML模型
    var OptionModel = $("#OptionModel div:eq(0)");

    //显示答案列表
    var jsonArray = eval("(" + Question + ")");
    var Answers = Answer.split(',');
    var htmls = '<div id="d_quesoptions" class="form-group"><label class="control-label">选项</label>';
    for (i = 0; i < jsonArray.length; i++) {
        var eleCon = OptionModel.clone(true);

        for (var j in Answers) {
            if (Answers[j] == jsonArray[i].Id) {
                eleCon.find("#chkRight").attr("checked", "checked");
            }
        }
        eleCon.find("#QtContent").attr("value", jsonArray[i].Content);

        htmls += $(eleCon)[0].outerHTML;
    }
    htmls += '</div>';

    return htmls;
}

function DeleteUnitTest() {
    var returnVal = window.confirm("是否确认删除?", "提示");

    if (returnVal) {
        var obj = JSON.stringify({ Id: $("#TestId").val() });
        $.ajax({
            cache: false,
            async: false,
            type: 'post',
            contentType: 'application/json',
            url: '/CourseCreate/DeleteUnitTest',
            data: obj,
            dataType: 'json',
            success: function (data) {
                Alert(data.Msg);
                showQuesList();
            },
            error: function () {
            }
        });
    }
}

//设置指定课程结业考试试题版本号
function SetVerson() {
    if ($("#QuesCount").html() == "0") {
        Alert("请添加题目,以完成测试题目制作!");
        return;
    }

    var obj = JSON.stringify({ TrainingId: TrainingId });
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/CourseCreate/SetExamVerson',
        data: obj,
        dataType: 'json',
        success: function (data) {
            location.href = "/Course/CourseCreate/CourseDetail?TrainingId=" + UrlEncode(TrainingId);
        },
        error: function () {
        }
    });
}