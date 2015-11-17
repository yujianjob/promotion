﻿$("#close_Iframe").click(function () {
    var index = parent.layer.getFrameIndex(window.name);
    parent.layer.close(index);
});

$(function () {
    $("input[name=chkItem]").each(function () {

        var ss = SeleIsId($("#hidmembers").val(), "," + this.value + ",");
        if (ss != "-1") {
            $(this).prop("checked", true);
        }
    })


    $("input[name=chkItem]").click(function () {
        if ($(this).prop("checked")) {
            //Id
            var id = this.value;
            if ($("#hidmembers").val() == "" || $("#hidmembers").val() == ",")
            { var ids = "," + id + ","; }
            else {
                var ids = $("#hidmembers").val() + id + ",";
            }
            $("#hidmembers").val(ids);

            //name
            var name = $(this).parent().parent().find(".name").text();
            if ($("#hidmembernames").val() == "" || $("#hidmembernames").val() == ",")
            { var names = "," + name + ","; }
            else {
                var names = $("#hidmembernames").val() + name + ",";
            }
            $("#hidmembernames").val(names);

            //organ
            var organ = $(this).parent().parent().find(".organ").text();
            if ($("#hidmemberorgans").val() == "" || $("#hidmemberorgans").val() == ",")
            { var organs = "," + organ + ","; }
            else {
                var organs = $("#hidmemberorgans").val() + organ + ",";
            }
            $("#hidmemberorgans").val(organs);

            //mobile
            var mobile = $(this).parent().parent().find(".mobile").attr("title");
            if (typeof (mobile) == "undefined") { mobile = ""; }
            if ($("#hidmembermobiles").val() == "" || $("#hidmembermobiles").val() == ",")
            { var mobiles = "," + mobile + ","; }
            else {
                var mobiles = $("#hidmembermobiles").val() + mobile + ",";
            }
            $("#hidmembermobiles").val(mobiles);

            //email
            var email = $(this).parent().parent().find(".email").attr("title");
            if (typeof (email) == "undefined") { email = ""; }
            if ($("#hidmemberemails").val() == "" || $("#hidmemberemails").val() == ",")
            { var emails = "," + email + ","; }
            else {
                var emails = $("#hidmemberemails").val() + email + ",";
            }
            $("#hidmemberemails").val(emails);

            //pic
            var pic = $(this).parent().parent().find(".pic").attr("src");
            if (typeof (pic) == "undefined") { pic = ""; }
            if ($("#hidmemberpics").val() == "" || $("#hidmemberpics").val() == ",")
            { var pics = "," + pic + ","; }
            else {
                var pics = $("#hidmemberpics").val() + pic + ",";
            }
            $("#hidmemberpics").val(pics);


        } else {
            $("#hidmembers").val($("#hidmembers").val().replace("," + this.value + ",", ","));
            $("#hidmembernames").val($("#hidmembernames").val().replace("," + $(this).parent().parent().find(".name").text() + ",", ","));
            $("#hidmemberorgans").val($("#hidmemberorgans").val().replace("," + $(this).parent().parent().find(".organ").text() + ",", ","));
            $("#hidmembermobiles").val($("#hidmembermobiles").val().replace("," + $(this).parent().parent().find(".mobile").attr("title") + ",", ","));
            $("#hidmemberemails").val($("#hidmemberemails").val().replace("," + $(this).parent().parent().find(".email").attr("title") + ",", ","));
            $("#hidmemberpics").val($("#hidmemberpics").val().replace("," + $(this).parent().parent().find(".pic").attr("src") + ",", ","));
        }
    });
})
function SeleIsId(str1, str2) { //查询是否有
    var s = str1.indexOf(str2);
    return (s);
}

//学校--新增--教师搜索
function SearchMember() {
    var urlPath = $('#urlPath').val();
    var _d = '';
    _d += "&searchName=" + encodeURIComponent($('#searchName').val());
    var a = urlPath + _d;
    if (checkts($('#searchName').val())) {
        window.location = a;
    }
}

function checkts(search) {
    if (search.length > 0) {
        var reg = new RegExp('^[^@\/\\#$%&\^\*\<\>]+$');
        if (!reg.test(search)) {
            Alert('搜索的内容不能输入特殊字符 ^ @ / \ # $ % & * < >');
            return false;
        }
    }
    return true;
}

function checkMembers() {
    if ($("#hidmembers").val() == "") {
        Alert("请选教师");
        return false;
    }
    return true;
}

function saveMembers() {
    if (checkMembers()) {
        var ids = $("#hidmembers").val();
        var names = $("#hidmembernames").val();
        var organs = $("#hidmemberorgans").val();
        var mobiles = $("#hidmembermobiles").val();
        var emails = $("#hidmemberemails").val();
        var pics = $("#hidmemberpics").val();

        parent.createMembers($.trim(ids), $.trim(names), $.trim(organs), $.trim(mobiles), $.trim(emails), $.trim(pics));
    }
}


