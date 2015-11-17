function search() {
    var url = $('#url').val();
    var searchTitle = $('#searchTitle').val();
    if ($('#searchTitle').val() == "课程搜索")
    {
        searchTitle = "";
    }
    if (checkts($('#searchTitle').val())){
        window.location.href = url + "&searchTitle=" + searchTitle + "&organId=" + $('#organId').val();
    }
}

function checkts(search) {
    if (search.length > 0) {
        var reg = new RegExp('^[^@\/\\#$%&\^\*\<\>\']+$');
        if (!reg.test(search)) {
            Alert('搜索的内容不能输入特殊字符 ^ @ / \ # $ % & * < > \'');
            return false;
        }
    }
    return true;
}