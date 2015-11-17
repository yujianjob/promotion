﻿$(function () {
    $proto.init(function () {
        /* 页面加载后立即执行的代码写在这里 */
        //$('input.input-date').datepicker({});

        //上一单元索引更改事件
        $("#UnitId").change(function () {
            GetUnitContentMaxOrder($("#UnitId").val(), 2);
        });

        //当点击始终开放/截止,清空开放/截止天数文本框
        $("#radioAlreadyOpen").click(function () {
            $("#txtAfterOpenDayNum").val('');
        });

        $("#radioAlreadyEnd").click(function () {
            $("#txtAfterEndDayNum").val('');
        });

        $('#AttachUpload').UploadFile({
            multi: true,
            fileSizeLimit: '500MB',
            queueSizeLimit: 1,
            fileTypeExts: '*.mp4;',
            uploader: '/Entrance/Login/VideoUploadAttach',
            onUploadSuccess: function (file, data, response) {
                if (response) {
                    var myData = $.parseJSON(data);
                    if (myData.Result) {
                        $("#txtVideoLink").val(myData.Msg);
                    } else {
                        $("#txtVideoLink").val(myData.Msg);
                    }
                }
                else {
                    Alert('服务器没有响应！');
                }
            }
        });

        $("#radioMp4").click(function () {
            $("#radioMp4").attr("checked", "checked");
            $("#radioFlash").removeAttr("checked");
            $("#radioMp3").removeAttr("checked");

            $('#AttachUpload').UploadFile({
                multi: true,
                fileSizeLimit: '500MB',
                queueSizeLimit: 1,
                fileTypeExts: '*.mp4;',
                uploader: '/Entrance/Login/VideoUploadAttach',
                onUploadSuccess: function (file, data, response) {
                    if (response) {
                        var myData = $.parseJSON(data);
                        if (myData.Result) {
                            $("#txtVideoLink").val(myData.Msg);
                        } else {
                            $("#txtVideoLink").val(myData.Msg);
                        }
                    }
                    else {
                        Alert('服务器没有响应！');
                    }
                }
            });
        });

        $("#radioFlash").click(function () {
            $("#radioMp4").removeAttr("checked");
            $("#radioFlash").attr("checked", "checked");
            $("#radioMp3").removeAttr("checked");

            $('#AttachUpload').UploadFile({
                multi: true,
                fileSizeLimit: '500MB',
                queueSizeLimit: 1,
                fileTypeExts: '*.swf;',
                uploader: '/Entrance/Login/VideoUploadAttach',
                onUploadSuccess: function (file, data, response) {
                    if (response) {
                        var myData = $.parseJSON(data);
                        if (myData.Result) {
                            $("#txtVideoLink").val(myData.Msg);
                        } else {
                            $("#txtVideoLink").val(myData.Msg);
                        }
                    }
                    else {
                        Alert('服务器没有响应！');
                    }
                }
            });
        });

        $("#radioMp3").click(function () {
            $("#radioMp4").removeAttr("checked");
            $("#radioFlash").removeAttr("checked");
            $("#radioMp3").attr("checked", "checked");

            $('#AttachUpload').UploadFile({
                multi: true,
                fileSizeLimit: '500MB',
                queueSizeLimit: 1,
                fileTypeExts: '*.mp3;',
                uploader: '/Entrance/Login/VideoUploadAttach',
                onUploadSuccess: function (file, data, response) {
                    if (response) {
                        var myData = $.parseJSON(data);
                        if (myData.Result) {
                            $("#txtVideoLink").val(myData.Msg);
                        } else {
                            $("#txtVideoLink").val(myData.Msg);
                        }
                    }
                    else {
                        Alert('服务器没有响应！');
                    }
                }
            });
        });

        //提交按钮单击事件
        $('#btnSubmit').click(function () {
            //验证必填表单
            var result = true;
            $('.input-valid').each(function () {
                $(this).css('display', 'none');
                var validFields = $(this).attr('validfor').split(' ');
                for (var i = 0; i < validFields.length; i++) {
                    var field = validFields[i];

                    if (field != "comparedate") {
                        //验证非上级单元选项必填
                        if (field != "UnitId" && field != "txtAfterOpenDayNum" && field != "txtAfterEndDayNum") {
                            //验证必填
                            if (Trim($('#' + field).val()) == '' && $(this).attr('validtype') != 'number' && $(this).attr('validtype') != 'maxlength' && $(this).attr('validtype') != 'format') {
                                $(this).css('display', 'inline-block');
                                result = false;
                                break;
                            }

                            //验证数字
                            if (Trim($('#' + field).val()) != '' && $(this).attr('validtype') == 'number' && !(/^[1-9]*[1-9][0-9]*$/).test($('#' + field).val())) {
                                $(this).css('display', 'inline-block');
                                result = false;
                                break;
                            }

                            //验证最大长度
                            if ($(this).attr('validtype') == 'maxlength' && $('#' + field).val().length > 4000) {
                                $(this).css('display', 'inline-block');
                                result = false;
                                break;
                            }

                            //验证视频格式
                            if (Trim($('#' + field).val()) != '' && $(this).attr('validtype') == 'format' && !isValidateFile($('#' + field).val())) {
                                $(this).css('display', 'inline-block');
                                result = false;
                                break;
                            }
                        }
                        else {
                            switch (field) {
                                case "UnitId":
                                    {
                                        //验证上级单元必填
                                        if ($('#' + field).val() == '-1') {
                                            $(this).css('display', 'inline-block');
                                            result = false;
                                            break;
                                        }
                                        break;
                                    }
                                case "txtAfterOpenDayNum":
                                    {
                                        if ($("#radioPartialOpen").is(":checked")) {
                                            //验证开放日期必填
                                            if (Trim($('#' + field).val()) == '' && $(this).attr('validtype') != 'number') {
                                                $(this).css('display', 'inline-block');
                                                result = false;
                                                break;
                                            }
                                        }

                                        //验证数字
                                        if (Trim($('#' + field).val()) != '' && $(this).attr('validtype') == 'number' && !(/^[1-9]*[1-9][0-9]*$/).test($('#' + field).val())) {
                                            $(this).css('display', 'inline-block');
                                            result = false;
                                            break;
                                        }
                                        break;
                                    }
                                case "txtAfterEndDayNum":
                                    {
                                        if ($("#radioPartialEnd").is(":checked")) {
                                            //验证截止日期必填
                                            if (Trim($('#' + field).val()) == '' && $(this).attr('validtype') != 'number') {
                                                $(this).css('display', 'inline-block');
                                                result = false;
                                                break;
                                            }
                                        }

                                        //验证数字
                                        if (Trim($('#' + field).val()) != '' && $(this).attr('validtype') == 'number' && !(/^[1-9]*[1-9][0-9]*$/).test($('#' + field).val())) {
                                            $(this).css('display', 'inline-block');
                                            result = false;
                                            break;
                                        }
                                        break;
                                    }
                            }
                        }
                    }
                }
            });

            //验证截止日期必须不小于开放日期
            if ($("#radioPartialOpen").is(":checked") && $("#radioPartialEnd").is(":checked")) {
                if (parseInt($("#txtAfterEndDayNum").val()) < parseInt($("#txtAfterOpenDayNum").val())) {
                    $("#DateCompare").css('display', 'inline-block');
                    result = false;
                }
            }

            if (IsActivityAddRename($("#UnitId").val(), 2, Trim(HtmlEncode($("#txtTitle").val())))) {
                Alert("提交失败,系统提示:活动标题重复!");
                result = false;
            }

            if (!result) {
                return;
            }

            if ($("#txtAfterOpenDayNum").val() != "") {
                $("#radioAlreadyOpen").removeAttr("checked");
                $("#radioPartialOpen").attr("checked", "checked");
            }

            if ($("#txtAfterEndDayNum").val() != "") {
                $("#radioAlreadyEnd").removeAttr("checked");
                $("#radioPartialEnd").attr("checked", "checked");
            }

            var ContentType;
            if ($("#radioMp4").is(":checked")) {
                ContentType = 1;
            }
            else {
                if ($("#radioFlash").is(":checked")) {
                    ContentType = 2;
                }
                else {
                    if ($("#radioMp3").is(":checked")) {
                        ContentType = 3;
                    }
                }
            }

            $("#btnSubmit").attr("disabled", "false");
            var obj = JSON.stringify({ UnitId: $("#UnitId").val(), Title: HtmlEncode($("#txtTitle").val()), Sort: $("#txtSort").val(), TimeLength: $("#txtTimeLength").val(),ContentType:ContentType,Content: HtmlEncode($('#txtVideoLink').val()), OpenTime: ($("#radioPartialOpen").is(":checked") ? $("#txtAfterOpenDayNum").val() : 0), EndTime: ($("#radioPartialEnd").is(":checked") ? $("#txtAfterEndDayNum").val() : 0), UnitType: 2 });
            $.ajax({
                cache: false,
                type: 'post',
                contentType: 'application/json',
                url: '/CourseCreate/AddCourseAtivity',
                data: obj,
                dataType: 'json',
                success: function (data) {
                    var returnVal = window.confirm(data.Msg + '是否继续添加?', "提示");
                    if (returnVal) {
                        location.href = "/Course/CourseCreate/CourseActivityAdd?TrainingId=" + UrlEncode(TrainingId);
                        GetChapterSectionMaxOrder(0);
                    }
                    else {
                        location.href = "/Course/CourseCreate/CourseDetail?TrainingId=" + UrlEncode(TrainingId);
                    }
                },
                error: function () {
                }
            });
            $("#btnSubmit").removeAttr("disabled");
        });
    });
});

//验证mp4格式视频文件
function isValidateFile(value) {
    var filename = "";
    var extend = "";

    if (-1 != value.lastIndexOf(".")) { 
        filename = value.substring(0, value.lastIndexOf("."));
        extend = value.substring(value.lastIndexOf(".") + 1);
    }
    extend = extend.toUpperCase();
    if (filename == "" || extend == "") {
        return false;
    } else {
        if ($("#radioMp4").is(":checked")) {
            $("#format").html("此项内容不符合mp4文件格式(1.后缀是否为.mp4或MP4 2.文件名是否为空)");
            if (!(extend == "MP4")) {
                return false;
            }
        }
        else {
            if ($("#radioFlash").is(":checked")) {
                $("#format").html("此项内容不符合Flash文件格式(1.后缀是否为.swf或SWF 2.文件名是否为空)");
                if (!(extend == "SWF")) {
                    return false;
                }
            }
            else {
                if ($("#radioMp3").is(":checked"))
                {
                    $("#format").html("此项内容不符合mp3文件格式(1.后缀是否为.mp3或MP3 2.文件名是否为空)");
                    if (!(extend == "MP3")) {
                        return false;
                    }
                }
            }
        }
    }

    return true;
}