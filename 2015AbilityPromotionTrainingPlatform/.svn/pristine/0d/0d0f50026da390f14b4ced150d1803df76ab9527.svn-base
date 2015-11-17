$(function () {
    $proto.init(function () {
        /* 页面加载后立即执行的代码写在这里 */
        //$('input.input-date').datepicker({});

        if (Display == "True") {
            $("#chkDisplay").attr("checked", "checked");
        }

        $("#chkDisplay").click(function () {
            if ($("#chkDisplay").attr("checked")) {
                $("#chkDisplay").removeAttr("checked");
            }
            else {
                $("#chkDisplay").attr("checked", 'checked');
            }
        });

        $("#txtTitle").val(HtmlDecode(HtmlDecode(Title)));

        //实例化编辑器
        //建议使用工厂方法getEditor创建和引用编辑器实例，如果在某个闭包下引用该编辑器，直接调用UE.getEditor('editor')就能拿到相关的实例
        //UE.getEditor('preview');
        window.UEDITOR_HOME_URL = "/Resources/ueditor1_3_6-utf8-net/";
        var preview = UE.getEditor('areaContent', {
            toolbars: [['source', 'fullscreen', 'bold', 'italic', 'underline', 'FontFamily', 'FontSize', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyJustify', 'link', 'attachment', 'unlink', 'insertimage', 'insertvideo']],
            //toolbars: null,
            //catchRemoteImageEnable: false,
            textarea: 'editorValue',
            //textarea: 'introduction'
            //sourceEditor: "codemirror"
            maximumWords: 2000,
            focus: false,
            readonly: false,
            pasteplain: false
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

            if (preview.getPlainTxt() == '') {
                $('.reading-content .input-valid').css('display', 'inline-block');
                result = false;
            }

            if (preview.getPlainTxt() != '' && preview.getPlainTxt().length > 4000) {
                $('.reading-content .input-valid1').css('display', 'inline-block');
                result = false;
            }

            if (ParentId != 0) {
                if (IsSectionEditRename(ParentId, Trim(HtmlEncode($("#txtTitle").val())), Id)) {
                    Alert("提交失败,系统提示:节标题重复!");
                    result = false;
                }
            }
            else {
                if (IsChapterEditRename(TrainingId, Trim(HtmlEncode($("#txtTitle").val())), Id)) {
                    Alert("提交失败,系统提示:章标题重复!");
                    result = false;
                }
            }

            if (!result) {
                return;
            }

            $("#btnSubmit").attr("disabled", "false");
            var obj = JSON.stringify({ Id: Id, TrainingId: TrainingId, ParentId: ParentId, Title: HtmlEncode($("#txtTitle").val()), Sort: $("#txtSort").val(), Content: preview.getContent(), Display: $("#chkDisplay").attr("checked") == "checked" ? true : false });
            $.ajax({
                cache: false,
                type: 'post',
                contentType: 'application/json',
                url: '/CourseCreate/EditCourseUnit',
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