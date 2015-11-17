﻿//教师--创建课程实践（方法）


$('#FormAdd').submit(function () {
    var title = $('#title').val();
    var traingField = $('#traingField').val();
    var createtraingCategory = $('#createtraingCategory').val();
    var createtraingTopic = $('#createtraingTopic').val();
    var content = encodeURIComponent($('#content').val());
    var nationalCoursId = $('#createtraingNation').val();
    var role = $('#createroleList').val();
    var members = "";
    var attachPathList = $("#AttachPathList").val();
    return check("teacher", title, traingField, createtraingCategory, createtraingTopic, content, members, role);
});



function btnSubmitSingle(id,memberPCourseid, type) {
    var title = $('#title').val();
    var traingField = $('#traingField').val();
    var createtraingCategory = $('#createtraingCategory').val();
    var createtraingTopic = $('#createtraingTopic').val();
    var content = encodeURIComponent($('#content').val());
    alert(content.length+1);
    var nationalCoursId = $('#createtraingNation').val();
    var role = $('#createroleList').val();
    var members = "";
    var attachPathList = $("#AttachPathList").val();
    if (check("teacher", title, traingField, createtraingCategory, createtraingTopic, members, role)) {
        if (type == "insert") {
            $.ajax({
                type: 'get',
                url: '/PracticalCourse/CreatePracticalCourseSingle',
                data: {
                    Title: encodeURIComponent(title), TraingField: traingField, CreatetraingCategory: createtraingCategory, CreatetraingTopic: createtraingTopic, Content: content, Role: role, AttachPathList: attachPathList, NationalCoursId: nationalCoursId
                },
                contentType: 'application/json',
                dataType: "text",
                success: function (data) {
                    var result = $.parseJSON(data);
                    if (result.Code == 0) {
                        Alert(result.Msg, 'window.location = \'/Learn/MyPractice/List\'');
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
        else {
            $.ajax({
                type: 'get',
                url: '/PracticalCourse/UpdatePracticalCourseSingle',
                data: {
                    Id: id, MemberPCourseid: memberPCourseid, Title: encodeURIComponent(title), TraingField: traingField, CreatetraingCategory: createtraingCategory, CreatetraingTopic: createtraingTopic, Content: content, Role: role, AttachPathList: attachPathList
                },
                contentType: 'application/json',
                dataType: "text",
                success: function (data) {
                    var result = $.parseJSON(data);
                    if (result.Code == 0) {
                        Alert(result.Msg, 'window.location = \'/Learn/MyPractice/List\'');
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
