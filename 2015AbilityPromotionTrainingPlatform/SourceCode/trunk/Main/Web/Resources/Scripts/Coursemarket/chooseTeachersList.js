
//自适应Iframe高度
$(document).ready(function () {
    parent.SetWinHeight2($("#CTeacherList").innerHeight());
});


//加载选择过的教师  保存教师Id
$(function () {

    $("input[name=chkItem]").each(function () {

        var ss = SeleIsId($("#hidteachers", parent.document).val(), "," + this.value + ",");
        if (ss != "-1") {
            $(this).prop("checked", true);
        }
    })


    $("input[name=chkItem]").click(function () {

        if ($(this).prop("checked")) {
            var type1 = this.value;

            if ($("#hidteachers", parent.document).val() == "" || $("#hidteachers", parent.document).val() == ",")
            { var type2 = "," + type1 + ","; }
            else {
                var type2 = $("#hidteachers", parent.document).val() + type1 + ",";
            }
            $("#hidteachers", parent.document).val(type2);

            var type3 = $(this).parent().parent().find(".name").text();
            if ($("#hidteachersname", parent.document).val() == "" || $("#hidteachersname", parent.document).val() == ",")
            { var type4 = "," + type3 + ","; }
            else {
                var type4 = $("#hidteachersname", parent.document).val() + type3 + ",";
            }
            $("#hidteachersname", parent.document).val(type4);
            
        } else {
            $("#hidteachers", parent.document).val($("#hidteachers", parent.document).val().replace("," + this.value + ",", ","));
            $("#hidteachersname", parent.document).val($("#hidteachersname", parent.document).val().replace("," + $(this).parent().parent().find(".name").text() + ",", ","));
        }
    });
})



//教师列表--查询是否点过此教师
function SeleIsId(str1, str2) {
    var s = str1.toString().indexOf(str2.toString());
    return (s);
}


//提交验证存在教师
function checkMembers() {
    if ($("#hidteachers").val() == "") {
        //alert("请选教师");
        $("#chooseTeachers").next("span").text("请选教师");
        return false;
    }
    return true;
}


$(".i").mouseover(function () {
    $(this).css("background-color", "#EEEEEE");
})

$(".i").mouseout(function () {
    $(this).css("background-color", "#ffffff");
})

