﻿
//初始加载 课程班级联动
function initVerifyPage() {
    $("#mpId").val("");

    $('#trainingId').change(function () {
        var trainingId = $(this).val();
        getClassListBytCourse(trainingId);
    });

    var trainingId = $("#trainingId").val();
    if (trainingId > 0) {
        getClassListBytCourse(trainingId);
    }
}

function chk(id) {
    //if ($('#' + id).attr('checked') == undefined || $('#' + id).attr('checked') == '') {
    //    $('#' + id).attr('checked', "checked");
    //}
    //else {
    //    $('#' + id).removeAttr('checked');
    //}
    if (id == 'r2') {
        document.getElementById('contt').style.display = "block";
        document.getElementById('content').style.display = "block";
    }
    else {
        document.getElementById('contt').style.display = "none";
        document.getElementById('content').style.display = "none";
    }
}

//根据课程获取班级
function getClassListBytCourse(_course) {
    var _data = {
        Course: _course
    };
    $.ajax({
        url: '/Coursemarket/GetClassListBytCourse',
        type: 'GET',
        data: _data,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (m) {
            var z = m;
        },
        success: function (a) {
            $('#myclassId option:gt(0)').remove();
            var classss = $("#hidclassid").val();
            if (a.length > 0) {
                for (var i = 0; i < a.length; i++) {
                    $("<option value='" + a[i].Id + "'>" + a[i].Title + "</option>").appendTo($("#myclassId"));
                }
                $('#myclassId').val(classss);
            }
        }
    });
}

//全选
function chooseall() {
    if (document.getElementById("chkItemAll").checked) {
        $("[name = chkItem]:checkbox").prop("checked", true);
    }
    else {
        $("[name = chkItem]:checkbox").prop("checked", false);
    }
}

//审核
function marketverify(id) {
    //$('#verify').modal({
    //    keyboard: false,
    //    backdrop: 'static'
    //});
    //$("#mpId").val(id);
    window.location = '/Market/Coursemarket/CoursemarketVerifyPage?Id=' + id;
}


//检查人数
function checkpeople(classids, maxpeoples,limitPeopleCnt, ctitles, status, nowstatus) {
    if (status == 2 || status == 4) {
        var myclassids = classids.substring(0, classids.length - 1).split(',');
        var mymaxpeoples = maxpeoples.substring(0, maxpeoples.length - 1).split(',');
        var mylimitPeopleCnt = limitPeopleCnt.substring(0, limitPeopleCnt.length - 1).split(',');
        var mystatus = nowstatus.substring(0, nowstatus.length - 1).split(',');
        var myctitles = ctitles.substring(0, ctitles.length - 1).split(',');
        for (var i = 0; i < myclassids.length; i++) {
            if (mystatus[i] == 3 || mystatus[i] == 5) {
                var count = 1;

                for (var j = 0; j < i; j++) {
                    if (mystatus[j] == 3 || mystatus[j] == 5) {
                        if (myclassids[i] = myclassids[j]) {
                            count++;
                        }
                    }
                }

                if (Number(mylimitPeopleCnt[i]) != -1 && Number(mymaxpeoples[i]) < count) {
                    Alert(myctitles[i] + "报名人数超出招收人数");
                    return false;
                }
            }
        }
    }
    return true;
}




//批量审核
function saveallVerify(status) {
    ids = "";
    classids = "";
    maxpeoples = "";
    limitPeopleCnt = "";
    ctitles = "";
    nowstatus = "";
    $("input[name=chkItem]").each(function () {
        if ($(this).prop("checked")) {
            ids += $(this).val() + ",";
            classids += $(this).parent().parent().find(".cid").val() + ",";
            maxpeoples += $(this).parent().parent().find(".maxpeople").val() + ",";
            limitPeopleCnt += $(this).parent().parent().find(".limitPeopleCnt").val() + ",";
            ctitles += $(this).parent().parent().find(".ctitle").text() + ",";
            nowstatus += $(this).parent().parent().find(".status").val() + ",";
        }
    });
    if ($.trim(ids).length <= 0) {
        Alert("请选择待处理的申请!")
    }
    else {
        if (checkpeople(classids, maxpeoples,limitPeopleCnt, ctitles, status, nowstatus)) {
            $.ajax({
                type: 'get',
                url: '/Market/Coursemarket/MyVerifyAll',
                data: {
                    Status: status, Ids: ids
                },
                contentType: 'application/json',
                dataType: "text",
                success: function (data) {
                    var result = $.parseJSON(data);
                    if (result.Code == 0) {
                        Alert(result.Msg, 'window.location = \'/Market/Coursemarket/CoursemarketVerify\'');
                    }
                    else {
                        Alert(result.Msg);
                    }
                },
                error: function () {
                    Alert('服务器错误！');
                }
            });
        }
    }
}

//搜索
function verifySearch() {
    var url = $('#url').val();
    var searchTitle = $('#searchTitle').val();
    if ($('#searchTitle').val() == "姓名、师训编号、身份证、课程名称搜索") {
        searchTitle = "";
    }
    if (checkts($('#searchTitle').val())) {
        window.location.href = url + "searchTitle=" + searchTitle + "&planId=" + $('#planId').val() + "&status=" + $('#status').val() + "&torganId=" + $('#torganId').val() + "&trainingId=" + $('#trainingId').val() + "&myclassId=" + $('#myclassId').val();
    }
}

function checkts(search) {
    if (search.length > 0) {
        var reg = new RegExp('^[^@\/\\#$%&\^\*\<\>\']+$');
        if (!reg.test(search)) {
            Alert('搜索的内容不能输入特殊字符 ^ @ / \ # $ % & * < > \'');
            return false;
        }
    }
    return true;
}