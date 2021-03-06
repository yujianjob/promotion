﻿$(function () {
    $proto.init(function () {
        /* 页面加载后立即执行的代码写在这里 */
        var ChapterModel = $("#ChapterModel div:eq(0)");

        var obj = JSON.stringify({ TrainingId: TrainingId, ParentId: 0 });
        $.ajax({
            cache: false,
            async: false,
            type: 'post',
            contentType: 'application/json',
            url: '/LearnOnLine/ChapterSectionInfo',
            data: obj,
            dataType: 'json',
            success: function (data) {
                $("#notesContent").html(Loading);

                if (!data || !data.Data || data.Data.length == 0) {
                    $("#notesContent").html(noDataTip);
                    return;
                }
                
                var htmls = "";
                for (var i = 0; i < data.Data.length; i++) {
                    var eleCon = ChapterModel.clone(true);
                    
                    eleCon.find("#ChapTitle").html(data.Data[i]["Title"]);
                    eleCon.find("#ExportNote").attr("href", "/Learn/LearnOnLine/ExportNotesById?Id=" + data.Data[i]["Id"]);

                    //根据章显示状态显示文本
                    eleCon.find("#NoteContent").html(GetNotesContent(data.Data[i]["Id"]));

                    htmls += $(eleCon)[0].outerHTML;
                }

                $("#notesContent").html(htmls);
            },
            error: function () {
            }
        });
    });
});

//获取指定章下面笔记HTML
function GetNotesContent(Id) {
    var htmls = "";
    var NoteModel = $("#NoteModel div:eq(0)");

    var obj = JSON.stringify({ Id: Id });
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/LearnOnLine/NotesInfo',
        data: obj,
        dataType: 'json',
        success: function (data) {
            if (!data || !data.Data || data.Data.length == 0) {
                return;
            }

            for (var i = 0; i < data.Data.length; i++) {
                var eleCon = NoteModel.clone(true);

                eleCon.find("#content").attr("style", "word-break:break-all; word-wrap:break-word;");
                eleCon.find("#content").html(HtmlEncode(data.Data[i]["Content"]));
                eleCon.find("#createtime").html(data.Data[i]["CreateTime"]);
                eleCon.find("#NoteDelete").attr("onclick", "NoteDelete(" + data.Data[i]["Id"] + ");");
                eleCon.find("#NoteEdit").attr("onclick", "doOperation(" + data.Data[i]["Id"] + ");");

                htmls += $(eleCon)[0].outerHTML;
            }
        },
        error: function () {
        }
    });

    return htmls;
}

//编辑弹出框
function doOperation(id) {
    $("#NoteTypeTitle").html("笔记编辑");

    var obj = JSON.stringify({ Id: id });
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/LearnOnLine/NotesInfoByID',
        data: obj,
        dataType: 'json',
        success: function (data) {
            if (!data || !data.Data || data.Data.length == 0) {
                return;
            }

            $("#NoteId").val(data.Data["Id"]);
            $("#ClassId").val(data.Data["ClassId"]);
            $("#TrainingId").val(data.Data["TrainingId"]);
            $("#AccountId").val(data.Data["AccountId"]);
            $("#UnitContent").val(data.Data["UnitContent"]);
            $("#txtContent").val(data.Data["Content"]);
        },
        error: function () {
        }
    });

    $("#NoteTypeModal").modal({
        keyboard: false,
        backdrop: 'static'
    });
}

//新增编辑提交按钮单击事件
function doSubmit() {
    //验证必填表单
    var result = true;
    $('.input-valid').each(function () {
        $(this).css('display', 'none');
        var validFields = $(this).attr('validfor').split(' ');
        for (var i = 0; i < validFields.length; i++) {
            var field = validFields[i];
            //验证必填
            if (Trim($('#' + field).val()) == '' && $(this).attr('validtype') != 'maxlength') {
                $(this).css('display', 'inline-block');
                result = false;
                break;
            }

            //验证最大长度
            if ($(this).attr('validtype') == 'maxlength' && $('#' + field).val().length > 300) {
                $(this).css('display', 'inline-block');
                result = false;
                break;
            }
        }
    });

    if (!result) {
        return;
    }

    var obj = JSON.stringify({ Id: $("#NoteId").val(), Content: $("#txtContent").val(), ClassId: $("#ClassId").val(), TrainingId: $("#TrainingId").val(), AccountId: $("#AccountId").val(), UnitContent: $("#UnitContent").val() });
    $.ajax({
        cache: false,
        type: 'post',
        contentType: 'application/json',
        url: '/LearnOnLine/EditContentNote',
        data: obj,
        dataType: 'json',
        success: function (data) {
            location.href = location.href;
        },
        error: function () {
        }
    });
}

//笔记本删除按钮单击事件
function NoteDelete(id) {
    var returnVal = window.confirm("是否确认删除?", "提示");

    if (returnVal) {
        var obj = JSON.stringify({ id: id });
        $.ajax({
            cache: false,
            type: 'post',
            contentType: 'application/json',
            url: '/LearnOnLine/DelContentNote',
            data: obj,
            dataType: 'json',
            success: function (data) {
                location.href = location.href;
            },
            error: function () {
            }
        });
    }
}

function Trim(str) {
    return str.replace(/(^\s*)|(\s*$)/g, "");
}