﻿//获取指定Id下章节最大顺序号
function GetChapterSectionMaxOrder(ParentId,TrainingId) {
    var obj = JSON.stringify({ ParentId: ParentId, TrainingId: TrainingId });
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/CourseCreate/GetChapterSectionMaxOrder',
        data: obj,
        dataType: 'json',
        success: function (data) {
            $("#txtSort").val(data.MaxOrder + 1);
        },
        error: function () {
        }
    });
}

//获取指定章节指定内容类型学习活动最大顺序号
function GetUnitContentMaxOrder(UnitId, UnitType) {
    var obj = JSON.stringify({ UnitId: UnitId, UnitType: UnitType });
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/CourseCreate/GetUnitContentMaxOrder',
        data: obj,
        dataType: 'json',
        success: function (data) {
            $("#txtSort").val(data.MaxOrder + 1);
        },
        error: function () {
        }
    });
}

//判断是否为非负整数
function isNotIntNum(num) {
    var re = /^[0-9]+$/;
    return re.test(num);
}

//判断是否为非负浮点数
function isNotFloatNum(num) {
    var re = /^\d+(\.\d+)?$/;
    return re.test(num);
}

function HtmlEncode(html) {
    var temp = document.createElement("div");
    (temp.textContent != null) ? (temp.textContent = html) : (temp.innerText = html);
    var output = temp.innerHTML;
    temp = null;
    return output;
}

function HtmlDecode(text) {
    var temp = document.createElement("div");
    temp.innerHTML = text;
    var output = temp.innerText || temp.textContent;
    temp = null;
    return output;
}

function Trim(str) {
    return str.replace(/(^\s*)|(\s*$)/g, "");
}

function IsChapterAddRename(TrainingId, tilte) {
    var IsChapterAddRename = false;
    var obj = JSON.stringify({ TrainingId: TrainingId, tilte: tilte });
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/CourseCreate/IsChapterAddRename',
        data: obj,
        dataType: 'json',
        success: function (data) {
            if (!data || !data.Data || data.Data.length == 0) {
                return;
            }

            IsChapterAddRename = data.Data;
        },
        error: function () {
        }
    });
    return IsChapterAddRename;
}

function IsChapterEditRename(TrainingId, tilte, CourseId) {
    var IsChapterEditRename = false;
    var obj = JSON.stringify({ TrainingId: TrainingId, tilte: tilte, CourseId: CourseId });
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/CourseCreate/IsChapterEditRename',
        data: obj,
        dataType: 'json',
        success: function (data) {
            if (!data || !data.Data || data.Data.length == 0) {
                return;
            }

            IsChapterEditRename = data.Data;
        },
        error: function () {
        }
    });
    return IsChapterEditRename;
}

function IsSectionAddRename(ParentId, tilte) {
    var IsSectionAddRename = false;
    var obj = JSON.stringify({ ParentId: ParentId, tilte: tilte});
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/CourseCreate/IsSectionAddRename',
        data: obj,
        dataType: 'json',
        success: function (data) {
            if (!data || !data.Data || data.Data.length == 0) {
                return;
            }

            IsSectionAddRename = data.Data;
        },
        error: function () {
        }
    });
    return IsSectionAddRename;
}

function IsSectionEditRename(ParentId, tilte, CourseId) {
    var IsSectionEditRename = false;
    var obj = JSON.stringify({ ParentId: ParentId, tilte: tilte, CourseId: CourseId });
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/CourseCreate/IsSectionEditRename',
        data: obj,
        dataType: 'json',
        success: function (data) {
            if (!data || !data.Data || data.Data.length == 0) {
                return;
            }

            IsSectionEditRename = data.Data;
        },
        error: function () {
        }
    });
    return IsSectionEditRename;
}

function IsActivityAddRename(UnitId,UnitType,tilte) {
    var IsActivityAddRename = false;
    var obj = JSON.stringify({ UnitId: UnitId,UnitType:UnitType,tilte:tilte });
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/CourseCreate/IsActivityAddRename',
        data: obj,
        dataType: 'json',
        success: function (data) {
            if (!data || !data.Data || data.Data.length == 0) {
                return;
            }

            IsActivityAddRename = data.Data;
        },
        error: function () {
        }
    });
    return IsActivityAddRename;
}

function IsActivityEditRename(UnitId, UnitType, tilte, UnitContent) {
    var IsActivityEditRename = false;
    var obj = JSON.stringify({ UnitId: UnitId, UnitType: UnitType, tilte: tilte, UnitContent: UnitContent});
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/CourseCreate/IsActivityEditRename',
        data: obj,
        dataType: 'json',
        success: function (data) {
            if (!data || !data.Data || data.Data.length == 0) {
                return;
            }

            IsActivityEditRename = data.Data;
        },
        error: function () {
        }
    });
    return IsActivityEditRename;
}

function UrlEncode(code) {
    var EnCode = "";
    var obj = JSON.stringify({ Code: code});
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/CourseCreate/EnCode',
        data: obj,
        dataType: 'json',
        success: function (data) {
            if (!data || !data.Data || data.Data.length == 0) {
                return;
            }

            EnCode = data.Data;
        },
        error: function () {
        }
    });

    return EnCode;
} 