$(function () {
    $proto.init(function () {
        /* 页面加载后立即执行的代码写在这里 */
        //$('input.input-date').datepicker({});

        //页面加载获取章最大顺序号
        GetChapterSectionMaxOrder(0, TrainingId);

        //选择区/市
        $('.Type').click(function () {
            $('#TypeValue').val($(this).val());
        });

        //章Radio单击事件
        $("#radioChapter").click(function () {
            $("#LastUnit").attr("style", "display:none;");

            GetChapterSectionMaxOrder(0, TrainingId);
        });

        //节Radio单击事件
        $("#radioSection").click(function () {
            $("#LastUnit").attr("style", "display:block;");

            GetChapterSectionMaxOrder($("#ParentId").val(), TrainingId);
        });

        //上一单元索引更改事件
        $("#ParentId").change(function () {
            GetChapterSectionMaxOrder($("#ParentId").val(), TrainingId);
        });

        $("#chkDisplay").click(function () {
            if ("checked" == $("#chkDisplay").attr("checked")) {
                $("#chkDisplay").removeAttr("checked");
            }
            else {
                $("#chkDisplay").attr("checked", "checked");
            }
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

                        //验证非上级单元选项必填
                        if (field != "ParentId") {
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
                            if ($("#radioSection").is(":checked")) {
                                //验证上级单元必填
                                if ($('#' + field).val() == '-1') {
                                    $(this).css('display', 'inline-block');
                                    result = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            });

            if (preview.getContentTxt() == '') {
                $('.reading-content .input-valid').css('display', 'inline-block');
                result = false;
            }

            if (preview.getContentTxt() != '' && preview.getContentTxt().length > 4000) {
                $('.reading-content .input-valid1').css('display', 'inline-block');
                result = false;
            }

            if ($("#radioSection").is(":checked")) {
                if (IsSectionAddRename($("#ParentId").val(), Trim(HtmlEncode($("#txtTitle").val())))) {
                    Alert("提交失败,系统提示:节标题重复!");
                    result = false;
                }
            }
            else {
                if (IsChapterAddRename(TrainingId, Trim(HtmlEncode($("#txtTitle").val())))) {
                    Alert("提交失败,系统提示:章标题重复!");
                    result = false;
                }
            }

            if (!result)
            {
                return;
            }

            $("#btnSubmit").attr("disabled", "false");
            var obj = JSON.stringify({ TrainingId: TrainingId, ParentId: ($("#radioSection").is(":checked") ? $("#ParentId").val() : 0), Title: HtmlEncode($("#txtTitle").val()), Sort: $("#txtSort").val(), Content: preview.getContent(), Display: $("#chkDisplay").attr("checked") == "checked" ? true : false });
            $.ajax({
                cache: false,
                type: 'post',
                contentType: 'application/json',
                url: '/CourseCreate/AddCourseUnit',
                data: obj,
                dataType: 'json',
                success: function (data) {
                    var returnVal = window.confirm(data.Msg + '是否继续添加?', "提示");
                    if (returnVal) {                                                                                                                                                                                   
                        location.href = location.href;
                        GetChapterSectionMaxOrder(0, TrainingId);
                    }
                    else
                    {
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

