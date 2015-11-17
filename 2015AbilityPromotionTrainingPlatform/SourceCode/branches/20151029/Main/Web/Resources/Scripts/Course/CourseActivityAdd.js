﻿$(function () {
    $proto.init(function () {
        /* 页面加载后立即执行的代码写在这里 */
        //$('input.input-date').datepicker({});

        //获取指定章的结业考试
        var obj = JSON.stringify({ TrainingId: TrainingId });
        $.ajax({
            cache: false,
            async: false,
            type: 'post',
            contentType: 'application/json',
            url: '/CourseCreate/ExamInfo',
            data: obj,
            dataType: 'json',
            success: function (data) {
                if (!data || !data.Data || data.Data.length == 0) {
                    $("#CourseActivityExam").attr("href", "CourseActivityExam?TrainingId=" + UrlEncode(TrainingId));
                    return;
                }

                $("#CourseActivityExam").attr("href", "CourseActivityExamEdit?TrainingId=" + UrlEncode(TrainingId) + "&UnitId=" + UrlEncode(data.Data[0]["Id"]));
            },
            error: function () {
            }
        });
    });
});