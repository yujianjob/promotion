
////学校/教师--新增/修改--初始加载
//function initAddPage() {
//    $('#createtraingCategory').change(function () {
//        var tCategory = $(this).val();
//        gettTopicListBytCategory(tCategory);
//    });

//    $('#createtraingCategory').val($("input[data-name='traingCategory']").val());
//    var tCategory = $("input[data-name='traingCategory']").val();
//    if (tCategory > 0) {
//        gettTopicListBytCategory(tCategory);
//    }

//    $('#createtraingTopic').change(function () {
//        var tTopic = $(this).val();
//        gettRoleListBytTopic(tTopic);
//    });

//    $('#createtraingTopic').val($("input[data-name='traingTopic']").val());
//    var tTopic = $("input[data-name='traingTopic']").val();
//    if (tTopic > 0) {
//        gettRoleListBytTopic(tTopic);
//    }


//    //上传
//    $('#AttachUpload').UploadFile({
//        multi: true,
//        queueSizeLimit: 3,
//        fileTypeExts: '*.zip; *.rar; *.7z; *.pdf; *.doc; *.txt; *.xls',
//        uploader: '/Entrance/Login/UploadAttach',
//        onUploadSuccess: function (file, data, response) {
//            if (response) {
//                var myData = $.parseJSON(data);
//                if (myData.Result) {
//                    $('#AttachNameList').append('<p>' + file.name + ' <a href="javascript:void(0)" onclick="RemoveAttach(this);" fileid="0" src="' + myData.Msg + '" filename="' + file.name + '">删除</a></p>');
//                    $('#AttachPathList').val($('#AttachPathList').val() + '0,' + file.name + ',' + myData.Msg + '\r\n');
//                } else {
//                    Alert(myData.Msg);
//                }
//            }
//            else {
//                Alert('服务器没有响应！');
//            }
//        }
//    });
//}

////上传功能移除
//function RemoveAttach(e) {
//    var path = $(e).attr('src');
//    $('#AttachPathList').val($('#AttachPathList').val().replace(path + ',', ''));
//    $(e).parent().remove();
//}

////学校/教师--根据小类获取主题
//function gettTopicListBytCategory(_tCategory) {
//    var _data = {
//        tCategory: _tCategory
//    };
//    $.ajax({
//        url: '/PracticalCourse/GetTopicByCategory',
//        type: 'GET',
//        data: _data,
//        contentType: 'application/json; charset=utf-8',
//        dataType: 'json',
//        error: function (m) {
//            var z = m;
//        },
//        success: function (a) {
//            if (a.length > 0) {
//                $('#createtraingTopic option:gt(0)').remove();

//                for (var i = 0; i < a.length; i++) {
//                    $("<option value='" + a[i].Id + "'>" + a[i].Title + "</option>").appendTo($("select[name=createtraingTopic]"));
//                }
//                if (typeof ($("input[data-name='traingTopic']").val()) == "undefined" || $("input[data-name='traingTopic']").val()=="")
//                { $('#createtraingTopic').val(-1); }
//                else {
//                    $('#createtraingTopic').val($("input[data-name='traingTopic']").val());
//                }

//            }
//        }
//    });
//}

////学校/教师--根据主题获取角色
//function gettRoleListBytTopic(_tTopic) {
//    var _data = {
//        tTopic: _tTopic
//    };
//    $.ajax({
//        url: '/PracticalCourse/GetRoleByTopic',
//        type: 'GET',
//        data: _data,
//        contentType: 'application/json; charset=utf-8',
//        dataType: 'json',
//        error: function (m) {
//            var z = m;
//        },
//        success: function (a) {
//            if (a.length > 0) {
//                $('.createroleList option:gt(0)').remove();
//                for (var i = 0; i < a.length; i++) {
//                    $("<option value='" + a[i].Id + "'>" + a[i].Title + "</option>").appendTo($("select[name=createroleList]"));
//                }
//                if (typeof ($("input[data-name='traingRoles']").val()) == "undefined" || $("input[data-name='traingRoles']").val() == "")
//                { $('#createroleList').val(-1); }
//                else {
//                    $('#createroleList').val($("input[data-name='traingRoles']").val());
//                }
//            }
//        }
//    });
//}

////学校--新增--弹跳出选择教师
////function addmembers(OrganId) {
////    //$.layer({
////    //    type: 2,
////    //    title: "添加教师",
////    //    maxmin: true,
////    //    shadeClose: true, //开启点击遮罩关闭层
////    //    area: ['960px', '600px'],
////    //    offset: ['100px', ''],
////    //    iframe: { src: '../PracticalCourse/ChooseMembers?OrganId=' + OrganId }
////    //});
////    $('#chooseMembers').modal({
////        keyboard: false,
////        backdrop: 'static'
////    });
////}




////学校--新增--选择教师到主页面
//function createMembers(ids, names, organs, mobiles, emails, pics) {
//    var index = layer.getFrameIndex(window.name);
//    layer.close(index);
//    var cids = ids.substr(1, ids.length - 2).split(",");
//    var cnames = names.substr(1, names.length - 2).split(",");
//    var corgans = organs.substr(1, organs.length - 2).split(",");
//    var cmobiles = mobiles.substr(1, mobiles.length - 2).split(",");
//    var cemails = emails.substr(1, emails.length - 2).split(",");
//    var cpics = pics.substr(1, pics.length - 2).split(",");


//    var addhtml = $("#teachers").html();
//    for (var i = 0; i < cids.length; i++) {
//        addhtml += "<div class='i'>";
//        addhtml += "<span class='name'>" + cnames[i] + "</span>";
//        addhtml += "<span class='title'>(讲师)</span>";
//        addhtml += "<div class='unit trim organ'>" + corgans[i] + "</div>";
//        addhtml += "<span title=" + cmobiles[i] + " class='glyphicon glyphicon-earphone mobile'></span>";
//        addhtml += "<span title=" + cemails[i] + " class='glyphicon glyphicon-envelope email'></span>";
//        addhtml += "<div class='thumb'>";
//        addhtml += "<img src=" + cpics[i] + "class='pic'>"
//        addhtml += "</div>";
//        addhtml += "<div class='int'>";
//        addhtml += "<input type='hidden' name='chkItem' value='" + cids[i] + "' onclick='deletes(this)' >";
//        //addhtml += "<input type='button'   onclick='deletes(this)' >";
//        addhtml += "<img src='/Resources/Images/deletes.ico'  onclick='deletes(this)'>";
//        addhtml += "</div>";
//        addhtml += "<div class=ddlrole>";
//        addhtml += $("#createroleList").clone().html();
//        addhtml += "</div>";
//        addhtml += "</div>";

//    }
//    $("#teachers").html(addhtml);
//}

////学校--新增--删除已选择的教师
//function deletes(t) {
//    t.parentElement.parentElement.remove();
//}


////学校/教师--创建课程实践验证
//function check(type, title, traingField, createtraingCategory, createtraingTopic, content, members, roles) {
//    if (title == null || title == "") {
//        alert("实践名称不能为空");
//        return false;
//    }
//    if (traingField == null || traingField == "") {
//        alert("课程大类不能为空");
//        return false;
//    }
//    if (createtraingCategory == null || createtraingCategory == -1) {
//        alert("课程小类不能为空");
//        return false;
//    }
//    if (createtraingTopic == null || createtraingTopic == -1) {
//        alert("课程主题不能为空");
//        return false;
//    }
//    if (content.length>8000) {
//        alert("实践内容不能超过8000字符");
//        return false;
//    }
//    if (type == "school") {
//        if (members == null || members == "") {
//            alert("参与教师未选择");
//            return false;
//        }
//        if (roles == "-1," || roles.substring(0,roles.length-3).indexOf('-1,') >= 0)
//        {
//            alert("有教师未选择角色");
//            return false;
//        }
//    }
//    else {
//        if (roles == null || roles == -1) {
//            alert("角色选择不能为空");
//            return false;
//        }
//    }
//    return true;
//}

////学校--批量创建课程实践（方法）
//function btnSubmit() {
//    var title = $('#title').val();
//    var traingField = $('#traingField').val();
//    var createtraingCategory = $('#createtraingCategory').val();
//    var createtraingTopic = $('#createtraingTopic').val();
//    var content = $('#content').val();
//    var planId = 1;
//    var members = "";
//    $("input[name=chkItem]").each(function () {
//        members += $(this).val() + ",";
//    });
//    var roles = "";
//    $("select[name=createroleList]").each(function () {
//        roles += $(this).val() + ",";
//    });
//    if (check("school", title, traingField, createtraingCategory, createtraingTopic, content, members, roles)) {
//        $.ajax({
//            type: 'get',
//            url: '/PracticalCourse/CreatePracticalCourse',
//            data: {
//                Title: title, TraingField: traingField, CreatetraingCategory: createtraingCategory, CreatetraingTopic: createtraingTopic, Content: content,  Members: members, Roles: roles
//            },
//            contentType: 'application/json',
//            dataType: "text",
//            success: function (data) {
//                var result = $.parseJSON(data);
//                if (result.Code == 0) {
//                    alert(result.Msg);
//                }
//                else {
//                    alert(result.Msg);
//                }
//                window.location = "/Practice/PracticalCourse/PracticalCourseList";
//            },
//            error: function () {
//                alert('服务器错误！');
//            }
//        });
//    }
//}

////教师--创建课程实践（方法）
//function btnSubmitSingle() {
//    var title = $('#title').val();
//    var traingField = $('#traingField').val();
//    var createtraingCategory = $('#createtraingCategory').val();
//    var createtraingTopic = $('#createtraingTopic').val();
//    var content = $('#content').val();
//    var planId = 1;
//    var roles = $('#createroleList').val();
//    var members = "";
//    if (check("teacher", title, traingField, createtraingCategory, createtraingTopic, content, members, roles)) {
//        $.ajax({
//            type: 'get',
//            url: '/PracticalCourse/CreatePracticalCourseSingle',
//            data: {
//                Title: title, TraingField: traingField, CreatetraingCategory: createtraingCategory, CreatetraingTopic: createtraingTopic, Content: content,  Roles: roles
//            },
//            contentType: 'application/json',
//            dataType: "text",
//            success: function (data) {
//                var result = $.parseJSON(data);
//                if (result.Code == 0) {
//                    alert(result.Msg);
//                }
//                else {
//                    alert(result.Msg);
//                }
//                window.location = "/Practice/PracticalCourse/PracticalCourseList";
//            },
//            error: function () {
//                alert('服务器错误！');
//            }
//        });
//    }
//}


////学校--审核页面--初始加载
//function initListPage() {

//    $('#state').val($("input[data-name='state']").val());

//    $('#searchTitle').val($("input[data-name='searchTitle']").val());
//}


////学校--审核页面--搜索功能
//function Search() {
//    var urlPath = $('#urlPath').val();
//    var _d = '';
//    $('.parameter').map(function () {
//        if (_d == '')
//        { _d += this.name + "=" + encodeURIComponent(this.value); }
//        else { _d += "&" + this.name + "=" + encodeURIComponent(this.value); }
//    });
//    var a = urlPath + "?" + _d;
//    window.location = a;
//}


////学校--审核--弹出审核
//function verify(id) {
//    $.layer({
//        type: 2,
//        title: "审核",
//        maxmin: true,
//        shadeClose: true, //开启点击遮罩关闭层
//        area: ['700px', '500px'],
//        offset: ['100px', ''],
//        iframe: { src: '../PracticalCourse/Verify?id=' + id }
//    });
//}


