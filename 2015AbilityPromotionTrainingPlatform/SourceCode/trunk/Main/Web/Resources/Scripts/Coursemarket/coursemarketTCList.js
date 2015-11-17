$(document).ready(function () {
    parent.SetWinHeight2($("#TCList").innerHeight());
});

function changeurl(id, usertype) {
    if (usertype == 7)
        parent.window.location.href = "CoursemarketSingleEnroll?id=" + id;
    else if (usertype == 2 || usertype == 3 || usertype == 4)
        parent.window.location.href = "CoursemarketEnroll?id=" + id;
}