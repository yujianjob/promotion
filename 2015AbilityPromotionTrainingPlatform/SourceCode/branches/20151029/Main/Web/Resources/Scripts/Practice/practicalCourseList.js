﻿//学校--审核页面--初始加载
function initListPage() {
    $("#mpId").val("");
    $('#state').val($("input[data-name='state']").val());

    $('#searchTitle').val($("input[data-name='searchTitle']").val());
}


//学校--审核页面--搜索功能
function Search() {
    var urlPath = $('#urlPath').val();
    var _d = '';
    $('.parameter').map(function () {
        if (_d == '')
        { _d += this.name + "=" + encodeURIComponent(this.value); }
        else { _d += "&" + this.name + "=" + encodeURIComponent(this.value); }
    });
    var a = urlPath + "?" + _d;
    if (checkts($('#searchTitle').val())) {
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

//学校--审核--弹出审核
function verify(id) {
    //$('#verify').modal({
    //    keyboard: false,
    //    backdrop: 'static'
    //});
    $("#mpId").val(id);
    window.location = '/Practice/PracticalCourse/PracticalCourseVerifyPage?memberPCourseid=' + id;
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
        document.getElementById('verifycontent').style.display = "block";
    }
    else {
        document.getElementById('contt').style.display = "none";
        document.getElementById('verifycontent').style.display = "none";
    }
}