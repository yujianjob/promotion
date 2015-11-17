
//学校/教师--新增/修改--初始加载
function initAddPage() {
    $('#createtraingCategory').change(function () {
        $('#createroleList').val(-1);
        $('#createtraingNation').val(-1);
        var tCategory = $(this).val();
        gettTopicListBytCategory(tCategory);
    });

    $('#createtraingCategory').val($("input[data-name='traingCategory']").val());
    var tCategory = $("input[data-name='traingCategory']").val();
    if (tCategory > 0) {
        gettTopicListBytCategory(tCategory);
    }

    $('#createtraingTopic').change(function () {
        var tTopic = $(this).val();
        gettRoleListBytTopic(tTopic);
    });

    $('#createtraingTopic').val($("input[data-name='traingTopic']").val());
    var tTopic = $("input[data-name='traingTopic']").val();
    if (tTopic > 0) {
        gettRoleListBytTopic(tTopic);
    }


    //上传
    $('#AttachUpload').UploadFile({
        multi: true,
        queueSizeLimit: 10,
        fileTypeExts: '*.zip; *.rar; *.7z; *.pdf; *.doc; *.txt; *.xls',
        uploader: '/Entrance/Login/UploadAttach',
        onUploadSuccess: function (file, data, response) {
            if (response) {
                var myData = $.parseJSON(data);
                if (myData.Result) {
                    $('#AttachNameList').append('<p style="float: left;width: 100%;"><a style="float:left;" target="_blank" href="' + myData.Msg + '">' + file.name + '</a> <a style="margin-left:5px;float:left;" href="javascript:void(0)" onclick="RemoveAttach(this);" fileid="0" src="' + myData.Msg + '" filename="' + file.name + '">删除</a></p>');
                    //$('#AttachNameList').append('<p>' + file.name + ' <a style="margin-left:5px;float:left;" href="javascript:void(0)" onclick="RemoveAttach(this);" fileid="0" src="' + myData.Msg + '" filename="' + file.name + '">删除</a></p>');
                    $('#AttachPathList').val($('#AttachPathList').val() + '0|' + file.name + '|' + myData.Msg + ':');
                } else {
                    Alert(myData.Msg);
                }
            }
            else {
                Alert('服务器没有响应！');
            }
        }
    });
}

//上传功能移除
function RemoveAttach(e) {
    var path = $(e).attr('src');
    var fileName = $(e).attr('filename');
    var fileId = $(e).attr('fileId');
    $('#AttachPathList').val($('#AttachPathList').val().replace(fileId + '|' + fileName + '|' + path + ':', ''));
    $(e).parent().remove();
}

//学校/教师--根据小类获取主题和国家标准
function gettTopicListBytCategory(_tCategory) {

    var _data = {
        tCategory: _tCategory
    };
    $.ajax({
        url: '/PracticalCourse/GetTopicByCategory',
        type: 'GET',
        data: _data,
        contentType: 'application/json; charset=utf-8',
        dataType: 'text',
        error: function (m) {
            var z = m;
        },
        success: function (a) {
            var myData = $.parseJSON(a);
            var mytopic = myData.topic;
            var mynation = myData.nation;
            if (mytopic.length > 0) {
                $('#createtraingTopic option:gt(0)').remove();

                for (var i = 0; i < mytopic.length; i++) {
                    $("<option value='" + mytopic[i].Id + "'>" + mytopic[i].Title + "</option>").appendTo($("select[name=createtraingTopic]"));
                }
                if (typeof ($("input[data-name='traingTopic']").val()) == "undefined" || $("input[data-name='traingTopic']").val() == "")
                { $('#createtraingTopic').val(-1); }
                else {
                    $('#createtraingTopic').val($("input[data-name='traingTopic']").val());
                }
            }
            $('#createtraingNation option:gt(0)').remove();
            if (mynation.length > 0) {
                $('#createtraingNation option:gt(0)').remove();

                for (var i = 0; i < mynation.length; i++) {
                    $("<option value='" + mynation[i].Id +"'>" + mynation[i].Title +"("+mynation[i].Content+")"+ "</option>").appendTo($("select[name=createtraingNation]"));
                }
                if (typeof ($("input[data-name='traingNation']").val()) == "undefined" || $("input[data-name='traingNation']").val() == "")
                { $('#createtraingNation').val(-1); }
                else {
                    $('#createtraingNation').val($("input[data-name='traingNation']").val());
                }
            }
        }
    });
}

//学校/教师--根据主题获取角色
function gettRoleListBytTopic(_tTopic) {
    var _data = {
        tTopic: _tTopic
    };
    $.ajax({
        url: '/PracticalCourse/GetRoleByTopic',
        type: 'GET',
        data: _data,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (m) {
            var z = m;
        },
        success: function (a) {
            if (a.length > 0) {
                $('.createroleList option:gt(0)').remove();
                for (var i = 0; i < a.length; i++) {
                    $("<option value='" + a[i].Id + "'>" + a[i].Title + "</option>").appendTo($("select[name=createroleList]"));
                }
                if (typeof ($("input[data-name='traingRoles']").val()) == "undefined" || $("input[data-name='traingRoles']").val() == "")
                { $('#createroleList').val(-1); }
                else {
                    $('#createroleList').val($("input[data-name='traingRoles']").val());
                }
            }
        }
    });
}


//学校/教师--创建课程实践验证
function check(type, title, traingField, createtraingCategory, createtraingTopic, content, members, roles) {
    var returnvalue=true;
    if (title == null || $.trim(title) == "") {
        //alert("实践名称不能为空");
        $("#title").next("span").text("实践名称不能为空");
        returnvalue = false;
    } else { $("#title").next("span").text(""); }
    if (traingField == null || traingField == "") {
        //alert("课程大类不能为空");
        $("#traingField").next("span").text("课程大类不能为空");
        returnvalue = false;
    } else { $("#traingField").next("span").text(""); }
    if (createtraingCategory == null || createtraingCategory == -1) {
        //alert("课程小类不能为空");
        $("#createtraingCategory").next("span").text("课程小类不能为空");
        returnvalue = false;
    } else { $("#createtraingCategory").next("span").text(""); }
    if (createtraingTopic == null || createtraingTopic == -1) {
        //alert("课程主题不能为空");
        $("#createtraingTopic").next("span").text("课程主题不能为空");
        returnvalue = false;
    } else { $("#createtraingTopic").next("span").text(""); }
    //if (content.length > 8000) {
    //    //alert("实践内容不能超过8000字符");
    //    $("#content").next("span").text("实践内容不能超过8000字符");
    //    returnvalue = false;
    //} else { $("#content").next("span").text(""); }
    if (type == "school") {
        if (members == null || members == "") {
            //alert("参与教师未选择");
            $("#teachers").next("span").text("参与教师未选择");
            returnvalue = false;
        } else { $("#teachers").next("span").text(""); }
        if (roles == "-1," || roles.substring(0, roles.length - 3).indexOf('-1,') >= 0) {
            //alert("有教师未选择角色");
            $("#teachers").next("span").text("有教师未选择角色");
            returnvalue = false;
        } else { $("#teachers").next("span").text(""); }
    }
    else {
        if (roles == null || roles == -1) {
            //alert("角色选择不能为空");
            $("#createroleList").next("span").text("角色选择不能为空");
            returnvalue = false;
        } else { $("#createroleList").next("span").text(""); }
    }
    return returnvalue;
}






