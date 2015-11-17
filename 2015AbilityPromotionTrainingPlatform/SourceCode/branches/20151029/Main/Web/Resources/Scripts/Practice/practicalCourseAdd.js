﻿
//学校/教师--新增/修改--初始加载
//学校--新增--弹跳出选择教师
function addmembers(OrganId) {
    $('#chooseMembers').modal({
        keyboard: false,
        backdrop: 'static'
    });

    $('#iframe-teacherlsit').attr('src', '../PracticalCourse/ChooseMembers?OrganId=' + OrganId);
}

//学校--新增--选择教师到主页面
function createMembers(ids, names, organs, mobiles, emails, pics) {
    //var index = layer.getFrameIndex(window.name);
    //layer.close(index);
    $('#chooseMembers').modal('hide');
    var ids = $("#hidmembers").val();
    var names = $("#hidmembernames").val();
    var organs = $("#hidmemberorgans").val();
    var mobiles = $("#hidmembermobiles").val();
    var emails = $("#hidmemberemails").val();
    var pics = $("#hidmemberpics").val();

    var cids = ids.substr(1, ids.length - 2).split(",");
    var cnames = names.substr(1, names.length - 2).split(",");
    var corgans = organs.substr(1, organs.length - 2).split(",");
    var cmobiles = mobiles.substr(1, mobiles.length - 2).split(",");
    var cemails = emails.substr(1, emails.length - 2).split(",");
    var cpics = pics.substr(1, pics.length - 2).split(",");

    $("#teachers").html("");
    var addhtml = $("#teachers").html();
    for (var i = 0; i < cids.length; i++) {
        addhtml += "<div class='i'>";
        addhtml += "<span class='name'>" + cnames[i] + "</span>";
        addhtml += "<span class='title'>(讲师)</span>";
        addhtml += "<div class='unit trim organ'>" + corgans[i] + "</div>";
        addhtml += "<span title=" + cmobiles[i] + " class='glyphicon glyphicon-earphone mobile'></span>";
        addhtml += "<span title=" + cemails[i] + " class='glyphicon glyphicon-envelope email'></span>";
        addhtml += "<div class='thumb'>";
        addhtml += "<img src=" + cpics[i] + " class='pic' style='width:100%;height:100%;'>"
        addhtml += "</div>";
        addhtml += "<div class='int'>";
        addhtml += "<input type='hidden' name='chkItem' value='" + cids[i] + "' onclick='deletes(this)' >";
        //addhtml += "<input type='button'   onclick='deletes(this)' >";
        addhtml += "<img src='/Resources/Images/deletes.ico'  onclick='deletes(this)'>";
        addhtml += "</div>";
        addhtml += "<div class=ddlrole>";
        addhtml += $("#createroleList").clone().html();
        addhtml += "</div>";
        addhtml += "</div>";

    }
    $("#teachers").html(addhtml);
}

//学校--新增--删除已选择的教师
function deletes(t) {
    t.parentElement.parentElement.remove();
}

//学校--批量创建课程实践（方法）
$('#FormAdd').submit(function () {
    var title = $('#title').val();
    var traingField = $('#traingField').val();
    var createtraingCategory = $('#createtraingCategory').val();
    var createtraingTopic = $('#createtraingTopic').val();
    var content = $('#content').val();
    var nationalCoursId = $('#createtraingNation').val();
    var members = "";
    $("input[name=chkItem]").each(function () {
        members += $(this).val() + ",";
    });
    $("#Members").val(members);
    var roles = "";
    $("select[name=createroleList]").each(function () {
        roles += $(this).val() + ",";
    });
    $("#Roles").val(roles);
    var attachPathList = $("#AttachPathList").val();
    return check("school", title, traingField, createtraingCategory, createtraingTopic, content, members, roles);

});