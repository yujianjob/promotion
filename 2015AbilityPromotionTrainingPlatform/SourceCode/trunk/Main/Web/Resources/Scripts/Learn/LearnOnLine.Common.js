function UrlEncode(code) {
    var EnCode = "";
    var obj = JSON.stringify({ Code: code });
    $.ajax({
        cache: false,
        async: false,
        type: 'post',
        contentType: 'application/json',
        url: '/LearnOnLine/EnCode',
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

function Trim(str) {
    return str.replace(/(^\s*)|(\s*$)/g, "");
}

/**********************************************保存时文本处理  开始**********************************************/
//保存时转译
function HtmlEnCode(html) {
    var temp = document.createElement("div");
    (temp.textContent != undefined) ? (temp.textContent = html) : (temp.innerText = html);
    var output = temp.innerHTML;
    temp = null;
    for (var i = 0; i < output.length; i++) {
        output = output.replace('\'', '&apos;');//单引号
    }
    return output;
}

//显示时转译
function HtmlDecode(text) {
    var temp = document.createElement("div");
    temp.innerHTML = text;
    var output = temp.innerText || temp.textContent;
    temp = null;
    
    return output;
}
/**********************************************保存时文本处理  结束**********************************************/