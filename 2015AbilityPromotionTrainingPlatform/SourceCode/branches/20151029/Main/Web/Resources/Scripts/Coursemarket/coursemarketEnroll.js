﻿//自适应高度
function SetWinHeight2(height) {
    iframeObj = $('#showlist')[0];
    if (navigator.userAgent.indexOf('MSIE') > 0) {
        height += 50;
    }

    if (iframeObj != null) {
        iframeObj.height = height;
    }
}

//批量报名
function classRegisterAll(_classid, People, LimitPeopleCnt) {
    if (check(People, LimitPeopleCnt)) {
        var _data = {
            ClassId: _classid,
            Members: $("#hidteachers").val(),
            MembersNames: $("#hidteachersname").val()
        };
        $.ajax({
            url: '/Market/Coursemarket/ClassRegisterAll',
            type: 'GET',
            data: _data,
            contentType: 'application/json; charset=utf-8',
            dataType: 'text',
            error: function (m) {
                var z = m;
            },
            success: function (data) {
                var result = $.parseJSON(data);
                if (result.Code == 0) {
                    Alert(result.Msg, 'window.location = \'/Market/Coursemarket/CoursemarketSingleList\'');
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

//验证时间和人数
function check(People, LimitPeopleCnt) {
    var myDate = new Date();
    var myStart = new Date($("#hidsignstart").val());
    var myEnd = new Date($("#hidsignend").val());
    var myplanStart = new Date($("#hidplansignstart").val());
    var myplanEnd = new Date($("#hidplansignend").val());

    var teachers = $("#hidteachers").val().split(',');

    var myPeople = Number(People);
    var myLimitPeopleCnt = Number(LimitPeopleCnt);
    if ($("#hidteachers").val() == "") {
        Alert("请选教师");
        return false;
    }
    if (myStart > myDate) {
        Alert("未到报名时间");
        return false;
    }
    if (myplanStart > myDate) {
        Alert("未到学期报名时间");
        return false;
    }
    if (myEnd < myDate) {
        Alert("已过报名时间");
        return false;
    }
    if (myplanEnd < myDate) {
        Alert("已过学期报名时间");
        return false;
    }
    if (myLimitPeopleCnt > 0) {
        if (myLimitPeopleCnt < myPeople + teachers.length-2) {
            Alert("报名人数超过最大人数限制");
            return false;
        }
    }
    return true;
}

$(function () {
    $(".chk").click(function () {
        if ($(this).prop("checked")) {
            var type = this.value;
            var url = $('#url').val();
            $('#showlist').attr('src', url + '&Type=' + type);
        }
    })
})
