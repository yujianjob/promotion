$(function () {
    $proto.init(function () {
        /* 页面加载后立即执行的代码写在这里 */
        //$('input.input-date').datepicker({});

        //上一单元索引更改事件
        $("#UnitId").change(function () {
            GetUnitContentMaxOrder($("#UnitId").val(), 5);
        });

        //当点击始终开放/截止,清空开放/截止天数文本框
        $("#radioAlreadyOpen").click(function () {
            $("#txtAfterOpenDayNum").val('');
        });

        $("#radioAlreadyEnd").click(function () {
            $("#txtAfterEndDayNum").val('');
        });

        //提交按钮单击事件
        $('#btnSubmit').click(function () {
            //验证必填表单
            var result = true;
            $('.input-valid').each(function () {
                $(this).css('display', 'none');
                if (typeof ($(this).attr('validfor')) != 'undefined') {
                    var validFields = $(this).attr('validfor').split(' ');
                    for (var i = 0; i < validFields.length; i++) {
                        var field = validFields[i];

                        if (field != "comparedate") {
                            //验证非上级单元选项必填
                            if (field != "UnitId" && field != "txtAfterOpenDayNum" && field != "txtAfterEndDayNum" && field != "txtTestCnt") {
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
                                    case "txtTestCnt":
                                        {
                                            if ($("#radioLimit").is(":checked")) {
                                                //验证可测试次数必填
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
                }
            });

            //验证截止日期必须不小于开放日期
            if ($("#radioPartialOpen").is(":checked") && $("#radioPartialEnd").is(":checked")) {
                if (parseInt($("#txtAfterEndDayNum").val()) < parseInt($("#txtAfterOpenDayNum").val())) {
                    $("#DateCompare").css('display', 'inline-block');
                    result = false;
                }
            }

            if (preview.getContentTxt() == '') {
                $('.reading-content .input-valid').css('display', 'inline-block');
                result = false;
            }

            if (preview.getContentTxt() != '' && preview.getContentTxt().length > 4000) {
                $('.reading-content .input-valid1').css('display', 'inline-block');
                result = false;
            }

            if (IsActivityAddRename($("#UnitId").val(), 5, Trim(HtmlEncode($("#txtTitle").val())))) {
                Alert("提交失败,系统提示:活动标题重复!");
                result = false;
            }

            if (!result) {
                return;
            }

            if ($("#txtTestCnt").val() != "") {
                $("#radioNoLimit").removeAttr("checked");
                $("#radioLimit").attr("checked", "checked");
            }

            if ($("#txtAfterOpenDayNum").val() != "")
            {
                $("#radioAlreadyOpen").removeAttr("checked");
                $("#radioPartialOpen").attr("checked", "checked");
            }

            if ($("#txtAfterEndDayNum").val() != "") {
                $("#radioAlreadyEnd").removeAttr("checked");
                $("#radioPartialEnd").attr("checked", "checked");
            }

            $("#btnSubmit").attr("disabled", "false");
            var obj = JSON.stringify({ UnitId: $("#UnitId").val(), Title: HtmlEncode($("#txtTitle").val()), Sort: $("#txtSort").val(), TimeLength: $("#txtTimeLength").val(), Content: preview.getContent(), TestCnt: ($("#radioLimit").is(":checked") ? $("#txtTestCnt").val() : -1), PrintScore: ($("#radioPrintScore").is(":checked") ? 1 : 0), OpenTime: ($("#radioPartialOpen").is(":checked") ? $("#txtAfterOpenDayNum").val() : 0), EndTime: ($("#radioPartialEnd").is(":checked") ? $("#txtAfterEndDayNum").val() : 0), UnitType: 5 });
            $.ajax({
                cache: false,
                type: 'post',
                contentType: 'application/json',
                url: '/CourseCreate/AddCourseAtivity',
                data: obj,
                dataType: 'json',
                success: function (data) {
                    location.href = "/Course/CourseCreate/CourseActivityQuizQues?TrainingId=" + UrlEncode(TrainingId) + "&UnitContent=" + UrlEncode(data.UnitContent);
                },
                error: function () {
                }
            });
            $("#btnSubmit").removeAttr("disabled");
        });
    });
});