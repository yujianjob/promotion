//function search() {
//    var url = $('#url').val();
//    window.location.href = url + "&searchTitle=" + $('#searchTitle').val() + "&organId=" + $('#organId').val();
//}

//function change(id, type) {
//    $('#showlist').attr("src", "CoursemarketTCList?Id=" + id + "&Type=" + type);
//}

//function SetWinHeight2(height) {
//    iframeObj = $('#showlist')[0];
//    if (navigator.userAgent.indexOf('MSIE') > 0) {
//        height += 50;
//    }

//    if (iframeObj != null) {
//        iframeObj.height = height;
//    }
//}

//function classRegister(_classid, SignUpStartTime, SignUpEndTime, People, LimitPeopleCnt) {
//    if (check(SignUpStartTime, SignUpEndTime, People, LimitPeopleCnt)) {
//        var _data = {
//            ClassId: _classid
//        };
//        $.ajax({
//            url: '/Market/Coursemarket/ClassRegister',
//            type: 'GET',
//            data: _data,
//            contentType: 'application/json; charset=utf-8',
//            dataType: 'text',
//            error: function (m) {
//                var z = m;
//            },
//            success: function (data) {
//                var result = $.parseJSON(data);
//                if (result.Code == 0) {
//                    alert(result.Msg);
//                }
//                else {
//                    alert(result.Msg);
//                }
//                window.location = "/Market/Coursemarket/CoursemarketSingleList";
//            },
//            error: function () {
//                alert('服务器错误！');
//            }
//        });
//    }
//}


//function check(SignUpStartTime, SignUpEndTime, People, LimitPeopleCnt) {
//    var myDate = new Date();
//    var myStart = new Date(SignUpStartTime);
//    var myEnd = new Date(SignUpEndTime);
//    var myPeople = Number(People);
//    var myLimitPeopleCnt = Number(LimitPeopleCnt);
//    if (myStart > myDate) {
//        alert("未到报名时间");
//        return false;
//    }
//    if (myEnd < myDate) {
//        alert("已过报名时间");
//        return false;
//    }
//    if (myLimitPeopleCnt <= myPeople) {
//        alert("报名人数已满");
//        return false;
//    }
//    return true;
//}

//function classRegisterAll(_classid, SignUpStartTime, SignUpEndTime, People, LimitPeopleCnt) {
//    if (check(SignUpStartTime, SignUpEndTime, People, LimitPeopleCnt)) {
//        var _data = {
//            ClassId: _classid,
//            Members: $(window.frames["showlist"].document).find("#hidteachers").val()
//            //$("#hidteachers").val()
//        };
//        $.ajax({
//            url: '/Market/Coursemarket/ClassRegisterAll',
//            type: 'GET',
//            data: _data,
//            contentType: 'application/json; charset=utf-8',
//            dataType: 'text',
//            error: function (m) {
//                var z = m;
//            },
//            success: function (data) {
//                var result = $.parseJSON(data);
//                if (result.Code == 0) {
//                    alert(result.Msg);
//                }
//                else {
//                    alert(result.Msg);
//                }
//                window.location = "/Market/Coursemarket/CoursemarketSingleList";
//            },
//            error: function () {
//                alert('服务器错误！');
//            }
//        });
//    }
//}

//function chooseall() {
//    if (document.getElementById("chkItemAll").checked) {
//        $("[name = chkItem]:checkbox").prop("checked", true);
//    }
//    else {
//        $("[name = chkItem]:checkbox").prop("checked", false);
//    }
//}

//教师列表--选择教师



////提交验证存在教师
//function checkMembers() {
//    if ($("#hidteachers").val() == "") {
//        alert("请选教师");
//        return false;
//    }
//    return true;
//}




//学校--审核--弹出审核
//function marketverify(id) {
//    $.layer({
//        type: 2,
//        title: "审核",
//        maxmin: true,
//        shadeClose: true, //开启点击遮罩关闭层
//        area: ['700px', '500px'],
//        offset: ['100px', ''],
//        iframe: { src: '../Coursemarket/Verify?id=' + id }
//    });
//}



//function checkpeople(classids, maxpeoples, ctitles, status) {
//    if (status == 2 || status == 4) {
//        var myclassids = classids.substring(0, classids.length - 1).split(',');
//        var maxpeoples = maxpeoples.substring(0, maxpeoples.length - 1).split(',');
//        for (var i = 0; i < myclassids.length; i++) {
//            var count = 1;
//            for (var j = 0; j < i; j++) {
//                if (myclassids[i] = myclassids[j]) {
//                    count++;
//                }
//            }
//            if (Number(maxpeoples[i]) < count) {
//                alert(ctitles[i] + "报名人数超出招收人数");
//                return false;
//            }
//        }
//    }
//    return true;
//}

//function saveallVerify(status) {
//    ids = "";
//    classids = "";
//    maxpeoples = "";
//    ctitles = "";
//    $("input[name=chkItem]").each(function () {
//        if ($(this).prop("checked")) {
//            ids += $(this).val() + ",";
//            classids += $(this).parent().parent().find(".cid").val()+",";
//            maxpeoples += $(this).parent().parent().find(".maxpeople").val() + ",";
//            ctitles += $(this).parent().parent().find(".ctitle").text() + ",";
//        }
//    });
//    if ($.trim(ids).length <= 0) {
//        alert("请选择审核通过的申请!")
//    }
//    else {
//        if (checkpeople(classids, maxpeoples, ctitles, status)) {
//            $.ajax({
//                type: 'get',
//                url: '/Market/Coursemarket/MyVerifyAll',
//                data: {
//                    Status: status, Ids: ids
//                },
//                contentType: 'application/json',
//                dataType: "text",
//                success: function (data) {
//                    var result = $.parseJSON(data);
//                    if (result.Code == 0) {
//                        alert(result.Msg);
//                    }
//                    else {
//                        alert(result.Msg);
//                    }
//                    window.parent.location.reload();
//                },
//                error: function () {
//                    alert('服务器错误！');
//                }
//            });
//        }
//    }
//}


//function saveVerify(id, status) {
//    var content = $("#content").val();

//    $.ajax({
//        type: 'get',
//        url: '/Market/Coursemarket/MyVerify',
//        data: {
//            Status: status, Id: id, Content: content
//        },
//        contentType: 'application/json',
//        dataType: "text",
//        success: function (data) {
//            var result = $.parseJSON(data);
//            if (result.Code == 0) {
//                alert(result.Msg);
//            }
//            else {
//                alert(result.Msg);
//            }
//            window.parent.location.reload();
//        },
//        error: function () {
//            alert('服务器错误！');
//        }
//    });
//}

//$("#close_Iframe").click(function () {
//    var index = parent.layer.getFrameIndex(window.name);
//    parent.layer.close(index);
//});


//function verifySearch()
//{
//    var url = $('#url').val();
//    window.location.href = url + "searchTitle=" + $('#searchTitle').val() + "&planId=" + $('#planId').val() + "&status=" + $('#status').val() + "&trainingId=" + $('#trainingId').val() + "&classId=" + $('#classId').val();
//}