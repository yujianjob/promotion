﻿$(function () {
    $proto.init(function () {
        /* 页面加载后立即执行的代码写在这里 */
        //$('input.input-date').datepicker({});

        //当点击始终开放/截止,清空开放/截止天数文本框
        $("#radioAlreadyOpen").click(function () {
            $("#txtAfterOpenDayNum").val('');
        });

        $("#radioAlreadyEnd").click(function () {
            $("#txtAfterEndDayNum").val('');
        });

        if (OpenTime != 0) {
            //$("#radioAlreadyOpen").removeAttr("checked");
            //$("#radioPartialOpen").attr("checked", "checked");
            $("#radioPartialOpen").click();
            $("#txtAfterOpenDayNum").val(OpenTime);
        }

        if (EndTime != 0) {
            //$("#radioAlreadyEnd").removeAttr("checked");
            //$("#radioPartialEnd").attr("checked", "checked");
            $("#radioPartialEnd").click();
            $("#txtAfterEndDayNum").val(EndTime);
        }

        $("#txtTitle").val(HtmlDecode(HtmlDecode(Title)));
        $("#areaContent").val(HtmlDecode(HtmlDecode(Content)));

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
                        if (field != "txtAfterOpenDayNum" && field != "txtAfterEndDayNum") {

                            //验证必填
                            if (Trim($('#' + field).val()) == '' && $(this).attr('validtype') != 'number' && $(this).attr('validtype') != 'maxlength') {
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
                        }
                        else {
                            switch (field) {
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

            if (IsActivityEditRename(UnitId, 3, Trim(HtmlEncode($("#txtTitle").val())), Id)) {
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

            $("#btnSubmit").attr("disabled", "false");
            var obj = JSON.stringify({ Id: Id, UnitId: UnitId, Title: HtmlEncode($("#txtTitle").val()), Sort: $("#txtSort").val(), TimeLength: $("#txtTimeLength").val(), Content: HtmlEncode($("#areaContent").val()), OpenTime: ($("#radioPartialOpen").is(":checked") ? $("#txtAfterOpenDayNum").val() : 0), EndTime: ($("#radioPartialEnd").is(":checked") ? $("#txtAfterEndDayNum").val() : 0), UnitType: 3 });
            $.ajax({
                cache: false,
                type: 'post',
                contentType: 'application/json',
                url: '/CourseCreate/EditCourseAtivity',
                data: obj,
                dataType: 'json',
                success: function (data) {
                    Alert(data.Msg);
                },
                error: function () {
                }
            });
            $("#btnSubmit").removeAttr("disabled");
        });
    });
});