﻿function saveVerify(id, status) {
    var content = encodeURIComponent($("#content").val());
    var id = $("#mpId").val();
    var status = $("input[name='a13']:checked").val();

    if (check()) {
        $.ajax({
            type: 'get',
            url: '/Market/Coursemarket/MyVerify',
            data: {
                Status: status, Id: id, Content: content
            },
            contentType: 'application/json',
            dataType: "text",
            success: function (data) {
                var result = $.parseJSON(data);
                if (result.Code == 0) {
                    //Alert(result.Msg, 'window.location.reload()');
                    window.location.reload();
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


$('#Check').submit(function () {
    var returnvalue = true;
    var spanshow = true;
    if ($("input[name='status']:checked").val() == undefined) {
        $("#checkshow").next("span").text("请选择审核结果");
        returnvalue = false;
    }
    else {
        $("#checkshow").next("span").text("");
    }
    if (($("input[name='status']:checked").val() == '3' || $("input[name='status']:checked").val() == '5') && $.trim($("#content").val()) == '') {
        $("#content").next("span").text("请填审核内容");
        returnvalue = false;
        spanshow = false;
    }
    if (($("input[name='status']:checked").val() == '3' || $("input[name='status']:checked").val() == '5') && $("#content").val().length > 500) {
        $("#content").next("span").text("审核内容不能超过500字符!");
        returnvalue = false;
        spanshow = false;
    } 
    if (spanshow)
    {
        $("#content").next("span").text("");
    }
    return returnvalue;
});