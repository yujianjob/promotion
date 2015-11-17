var isIE = (navigator.appVersion.indexOf("MSIE") != -1) ? true : false;
var isWin = (navigator.appVersion.toLowerCase().indexOf("win") != -1) ? true : false;
var isOpera = (navigator.userAgent.indexOf("Opera") != -1) ? true : false;

var requiredMajorVersion = 10;
// Minor version of Flash required
var requiredMinorVersion = 0;
// Minor version of Flash required
var requiredRevision = 12;

var width = 715;
var height = 489;



function ControlVersion() {
    var version;
    var axo;
    var e;
    try {
        axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.7");
        version = axo.GetVariable("$version");
    } catch (e) {
    }
    if (!version) {
        try {
            axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.6");
            version = "WIN 6,0,21,0";
            axo.AllowScriptAccess = "always";
            version = axo.GetVariable("$version");
        } catch (e) {
        }
    }
    if (!version) {
        try {
            axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.3");
            version = axo.GetVariable("$version");
        } catch (e) {
        }
    }
    if (!version) {
        try {
            axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.3");
            version = "WIN 3,0,18,0";
        } catch (e) {
        }
    }
    if (!version) {
        try {
            axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash");
            version = "WIN 2,0,0,11";
        } catch (e) {
            version = -1;
        }
    }
    return version;
}
function GetSwfVer() {
    var flashVer = -1;
    if (navigator.plugins != null && navigator.plugins.length > 0) {
        if (navigator.plugins["Shockwave Flash 2.0"] || navigator.plugins["Shockwave Flash"]) {
            var swVer2 = navigator.plugins["Shockwave Flash 2.0"] ? " 2.0" : "";
            var flashDescription = navigator.plugins["Shockwave Flash" + swVer2].description;
            var descArray = flashDescription.split(" ");
            var tempArrayMajor = descArray[2].split(".");
            var versionMajor = tempArrayMajor[0];
            var versionMinor = tempArrayMajor[1];
            var versionRevision = descArray[3];
            if (versionRevision == "") {
                versionRevision = descArray[4];
            }
            if (versionRevision[0] == "d") {
                versionRevision = versionRevision.substring(1);
            } else if (versionRevision[0] == "r") {
                versionRevision = versionRevision.substring(1);
                if (versionRevision.indexOf("d") > 0) {
                    versionRevision = versionRevision.substring(0, versionRevision.indexOf("d"));
                }
            }
            var flashVer = versionMajor + "." + versionMinor + "." + versionRevision;
        }
    }
    else if (navigator.userAgent.toLowerCase().indexOf("webtv/2.6") != -1) flashVer = 4;
    else if (navigator.userAgent.toLowerCase().indexOf("webtv/2.5") != -1) flashVer = 3;
    else if (navigator.userAgent.toLowerCase().indexOf("webtv") != -1) flashVer = 2;
    else if (isIE && isWin && !isOpera) {
        flashVer = ControlVersion();
    }
    return flashVer;
}
function DetectFlashVer(reqMajorVer, reqMinorVer, reqRevision) {
    versionStr = GetSwfVer();
    if (versionStr == -1) {
        return false;
    } else if (versionStr != 0) {
        if (isIE && isWin && !isOpera) {
            tempArray = versionStr.split(" ");
            tempString = tempArray[1];
            versionArray = tempString.split(",");
        } else {
            versionArray = versionStr.split(".");
        }
        var versionMajor = versionArray[0];
        var versionMinor = versionArray[1];
        var versionRevision = versionArray[2];
        if (versionMajor > parseFloat(reqMajorVer)) {
            return true;
        } else if (versionMajor == parseFloat(reqMajorVer)) {
            if (versionMinor > parseFloat(reqMinorVer))
                return true;
            else if (versionMinor == parseFloat(reqMinorVer)) {
                if (versionRevision >= parseFloat(reqRevision))
                    return true;
            }
        }
        return false;
    }
}

function AC_AddExtension(src, ext) {
    if (src.indexOf('?') != -1)
        return src.replace(/\?/, ext + '?');
    else
        return src + ext;
}

function AC_Generateobj(objAttrs, params, embedAttrs) {
    var str = '';
    if (isIE && isWin && !isOpera) {
        str += '<object ';
        for (var i in objAttrs) {
            str += i + '="' + objAttrs[i] + '" ';
        }
        str += '>';
        for (var i in params) {
            str += '<param name="' + i + '" value="' + params[i] + '" /> ';
        }
        str += '</object>';
    }
    else {
        str += '<embed ';
        for (var i in embedAttrs) {
            str += i + '="' + embedAttrs[i] + '" ';
        }
        str += '> </embed>';
    }
    return str;
}

function AC_FL_RunContent() {
    var ret =
      AC_GetArgs
      (arguments, ".swf", "movie", "clsid:d27cdb6e-ae6d-11cf-96b8-444553540000"
       , "application/x-shockwave-flash"
      );
    return AC_Generateobj(ret.objAttrs, ret.params, ret.embedAttrs);
}

function AC_SW_RunContent() {
    var ret =
      AC_GetArgs
      (arguments, ".dcr", "src", "clsid:166B1BCA-3F9C-11CF-8075-444553540000"
       , null
      );
    AC_Generateobj(ret.objAttrs, ret.params, ret.embedAttrs);
}

function AC_GetArgs(args, ext, srcParamName, classid, mimeType) {
    var ret = new Object();
    ret.embedAttrs = new Object();
    ret.params = new Object();
    ret.objAttrs = new Object();
    for (var i = 0; i < args.length; i = i + 2) {
        var currArg = args[i].toLowerCase();
        switch (currArg) {
            case "classid":
                break;
            case "pluginspage":
                ret.embedAttrs[args[i]] = args[i + 1];
                break;
            case "src":
            case "movie":
                args[i + 1] = AC_AddExtension(args[i + 1], ext);
                ret.embedAttrs["src"] = args[i + 1];
                ret.params[srcParamName] = args[i + 1];
                break;
            case "onafterupdate":
            case "onbeforeupdate":
            case "onblur":
            case "oncellchange":
            case "onclick":
            case "ondblclick":
            case "ondrag":
            case "ondragend":
            case "ondragenter":
            case "ondragleave":
            case "ondragover":
            case "ondrop":
            case "onfinish":
            case "onfocus":
            case "onhelp":
            case "onmousedown":
            case "onmouseup":
            case "onmouseover":
            case "onmousemove":
            case "onmouseout":
            case "onkeypress":
            case "onkeydown":
            case "onkeyup":
            case "onload":
            case "onlosecapture":
            case "onpropertychange":
            case "onreadystatechange":
            case "onrowsdelete":
            case "onrowenter":
            case "onrowexit":
            case "onrowsinserted":
            case "onstart":
            case "onscroll":
            case "onbeforeeditfocus":
            case "onactivate":
            case "onbeforedeactivate":
            case "ondeactivate":
            case "type":
            case "codebase":
            case "id":
                ret.objAttrs[args[i]] = args[i + 1];
                ret.embedAttrs[args[i]] = args[i + 1];
                break;
            case "width":
            case "height":
            case "align":
            case "vspace":
            case "hspace":
            case "class":
            case "title":
            case "accesskey":
            case "name":
            case "tabindex":
                ret.embedAttrs[args[i]] = ret.objAttrs[args[i]] = args[i + 1];
                break;
            default:
                ret.embedAttrs[args[i]] = ret.params[args[i]] = args[i + 1];
        }
    }
    ret.objAttrs["classid"] = classid;
    if (mimeType) ret.embedAttrs["type"] = mimeType;
    return ret;
}

function showplayer(divid, w, h, vars, swffile) {
    width = w;
    height = h;
    document.getElementById(divid).innerHTML = AC_FL_RunContent(
        'codebase', 'http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0',
        'width', w,
        'height', h,
        'src', "flash/" + swffile,
        'flashvars', vars,
        'quality', 'high',
        'pluginspage', 'http://www.macromedia.com/go/getflashplayer',
        'align', 'middle',
        'play', 'true',
        'loop', 'true',
        'scale', 'showall',
        'wmode', 'opaque',
        'devicefont', 'false',
        'id', divid + "_swf",
        'bgcolor', '#ffffff',
        'name', divid + "_swf",
        'menu', 'true',
        'allowFullScreen', 'true',
        'allowScriptAccess', 'always',
        'movie', "flash/" + swffile,
        'salign', ''
        );
}

function showlinkvideo(divid, w, h, linkurl) {
    document.getElementById(divid).innerHTML = "<object width='" + w + "' height='" + h + "'><param name='movie' value='" + linkurl + "'></param><param name='allowFullScreen' value='true'></param><param name='allowscriptaccess' value='always'></param><param name='wmode' value='opaque'></param><embed src='" + linkurl + "' type='application/x-shockwave-flash' allowscriptaccess='always' allowfullscreen='true' wmode='opaque' width='420' height='363'></embed></object>";
}

function thisMovie(movieName) {
    if (navigator.appName.indexOf("Microsoft") != -1) {
        return window[movieName];
    }
    else {
        return document[movieName];
    }
}

function broadcastEvent(_event, cname) {
    if (thisMovie(cname)) {
        thisMovie(cname).sendToFlash(_event);
    }
}

function broadcastEventToList(_event) {
    thisMovie("photoview_swf").sendToFlashListPage(_event);
}

function broadcastEventToPhoto(_event, cname) {
    thisMovie("photoview_swf").sendToFlashPhotoPage(_event);
}

var _firefox_flash_wheel = function (event) {
    var app = window.document[swfname];
    if (app) {
        var o = {
            x: event.screenX, y: event.screenY,
            delta: -event.detail,
            ctrlKey: event.ctrlKey, altKey: event.altKey,
            shiftKey: event.shiftKey
        }
        app.handleWheel(o);
    }
    event.preventDefault();
    event.stopPropagation();
}
var _chrome_flash_wheel = function (event) {
    var app = window.document[swfname];
    if (app) {
        var o = {
            x: event.screenX, y: event.screenY,
            delta: -event.wheelDelta,
            ctrlKey: event.ctrlKey, altKey: event.altKey,
            shiftKey: event.shiftKey
        }

        app.handleWheel(o);
    }
    event.preventDefault();
    event.stopPropagation();
}

function unenablePageScroll() {
    if (window.addEventListener) {
        window.addEventListener('DOMMouseScroll', _firefox_flash_wheel, false);
        window.addEventListener('mousewheel', _chrome_flash_wheel, false);
    }
    else {
        window.onmousewheel = document.onmousewheel = function (event) {
            var event = window.event;
            var app = window.document[swfname];
            if (app) {
                var o = {
                    x: event.screenX, y: event.screenY,
                    delta: -event.wheelDelta,
                    ctrlKey: event.ctrlKey, altKey: event.altKey,
                    shiftKey: event.shiftKey
                }
                app.handleWheel(o);
            }
            return false;
        };
    }
}

function enablePageScroll() {
    if (window.addEventListener) {
        window.removeEventListener('DOMMouseScroll', _firefox_flash_wheel, false);
        window.removeEventListener('mousewheel', _chrome_flash_wheel, false);
    }
    else {
        window.onmousewheel = document.onmousewheel = null;
    }
}

function turnToWide() {
    thisMovie("ovideoplayerDiv_swf").width = 970;
    $("#videoShow").css({ "width": "970px" });
    $("#ovideoplayerDiv").css({ "width": "970px" });
    $("#ovideoplayerDiv").css({ "height": "500px" });
}
function turnToNormal() {

    //thisMovie("ovideoplayerDiv_swf").width = 715;
    //$("#videoShow").css({ "width": "715px" });
    //$("#ovideoplayerDiv").css({ "width": "715px" });
    //$("#ovideoplayerDiv").css({ "height": "489px" });

    thisMovie("ovideoplayerDiv_swf").width = width;
    $("#videoShow").css({ "width": width+"px" });
    $("#ovideoplayerDiv").css({ "width": width+"px" });
    $("#ovideoplayerDiv").css({ "height": height+"px" });

}

function turnOff() {
    if ($("#turn_div").get(0)) {
        $("#turn_div").get(0).style.display = '';
    } else {
        var coverDiv = document.createElement('div');
        document.body.appendChild(coverDiv);
        coverDiv.id = 'turn_div';
        with (coverDiv.style) {
            position = 'absolute';
            background = '#000';
            left = '0px';
            top = '0px';
            zoom = 1;
            var bodySize = getBodySize();
            width = bodySize[0] + 'px'
            height = bodySize[1] + 'px';
            zIndex = 98;
            if ($.browser.msie) {
                filter = "Alpha(Opacity=90)";
            } else {
                opacity = 0.9;
            }
        }
    }
}
function turnOn() {
    $("#turn_div").get(0).style.display = 'none';
}
