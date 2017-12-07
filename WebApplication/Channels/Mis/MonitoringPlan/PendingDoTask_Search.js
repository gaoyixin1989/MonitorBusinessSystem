var CONTRACT_TYPE = "";var maingrid = null;
$.extend({
    getUrlVars: function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    getUrlVar: function (name) {
        return $.getUrlVars()[name];
    }
});
$(document).ready(function () {
//    alert("fd");
    CONTRACT_TYPE = $.getUrlVar('CONTRACT_TYPE'); //委托类型
    maingrid = $("#maingrid1").ligerGrid({
        columns: [{ display: '任务单号', name: 'TICKET_NUM', width: 240, minWidth: 60}],
        width: '100%',
        height: '98%',
        pageSizeOptions: [5, 10, 15, 20, 25, 30],
        pageSize: 5,
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        rownumbers: true,
        alternatingRow: false,
        onRClickToSelect: true,
        url: 'PendingDoTask_Search.aspx?action=GetDocNo&CONTRACT_TYPE=' + CONTRACT_TYPE
    });
});