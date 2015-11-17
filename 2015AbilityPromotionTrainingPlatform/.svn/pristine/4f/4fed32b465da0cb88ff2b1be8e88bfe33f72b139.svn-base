//提交审核
function saveVerify(IstuP) {


    var id = $("#mpId").val();
    var status = $("input[name='a13']:checked").val();
    var content = $("#verifycontent").val();

    if (check()) {
        $.ajax({
            type: 'get',
            url: '/PracticalCourse/MyVerify',
            data: {
                Status: status, Id: id, Content: content
            },
            contentType: 'application/json',
            dataType: "text",
            success: function (data) {
                var result = $.parseJSON(data);
                if (result.Code == 0) {
                    if (IstuP == 'p')
                    { Alert(result.Msg, 'window.location = \'/Practice/PracticalCourse/PracticalCourseList\''); }
                    else {
                        //Alert(result.Msg, 'window.location.reload()');
                        window.location.reload();
                    }
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
    if ($("input[name='status']:checked").val() == '3' && $.trim($("#verifycontent").val()) == '') {
        $("#verifycontent").next("span").text("请填审核内容");
        returnvalue = false;
        spanshow = false;
    }

    if (spanshow) {
        $("#verifycontent").next("span").text("");
    }
    return returnvalue;
});
