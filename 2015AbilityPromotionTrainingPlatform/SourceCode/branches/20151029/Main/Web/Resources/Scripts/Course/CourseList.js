$(function () {
    $proto.init(function () {
        /* 页面加载后立即执行的代码写在这里 */

        if (index == "1") {
            $("#HomePage").css("color", "#BEBEBE").removeAttr("href");
            $("#OnPage").css("color", "#BEBEBE").removeAttr("href");
        }
        if (parseInt(count) < 10) {
            $("#NextPage").css("color", "#BEBEBE").removeAttr("href");
            $("#LastPage").css("color", "#BEBEBE").removeAttr("href");
        }
    });
});

function doHideLecture() {
    var e1 = $($d.elem($d.evt()));
    var e2 = e1.parent().parent().find('.lecture');
    if (e2.css('display') == 'block') {
        e1.html('<span class="glyphicon glyphicon-chevron-down"></span>&nbsp;显示 ' + e2.size() + ' 项内容');
        e2.hide();
    } else {
        e1.html('<span class="glyphicon glyphicon-chevron-up"></span>&nbsp;隐藏');
        e2.show();
    }
}