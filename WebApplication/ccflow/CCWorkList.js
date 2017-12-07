var grid;
var mode;
var FK_Node;
$(function () {

    mode = getUrlRequestParam('mode');
    FK_Node = getUrlRequestParam('FK_Node');
    FK_Node = (FK_Node == null || FK_Node == undefined) ? '' : FK_Node;

    if (mode != 'Statistics') {
        $('#tdCCTo').hide();
    }

    $("#ddlType").ligerComboBox({
        width: "130",
        data: [
                    { text: '未读', id: 'read' },
                    { text: '已读', id: 'unread' }
                ],
        valueFieldID: 'hidType',
        initValue: ''
    });

    grid = $("#todoGrid").ligerGrid({
        width: '100%', height: '100%', usePager: true, rownumbers: true, allowHideColumn: false, enabledSort: false,
        url: 'CCWorkList.aspx?opt=getCCWorkList&mode=' + mode + '&FK_Node=' + FK_Node,
        columns: [
            { display: '主题', name: 'Title', align: 'left',
                render: function (row, index, value) {
                    return '<a href="javascript:;" onclick="viewWork(\'' + row.TitleUrl + '\')">' + value + '</a>'
                }
            },
            { display: '流程名称', name: 'FlowName', align: 'left' },
            { display: '抄送节点', name: 'NodeName', align: 'left' },
            { display: '抄送人', name: 'CCToName', align: 'left' },
            { display: '抄送时间', name: 'RDT', align: 'left' },
            { display: '查看时间', name: 'CDT', align: 'left' },
            { display: '是否已读', name: 'StaText', align: 'left',
                render: function (row, index, value) {
                    return '<font color="' + (value == '已读' ? 'green' : 'red') + '">' + value + '</font>'
                }
            }
        ]
    });
});

function viewWork(url) {
    $.ligerDialog.open({
        title: '任务查看', width: $("body").width() - 20, height: $("body").height() - 50, showMax: true, allowClose: false,
        content: '<iframe style="width:100%;height:100%" frameborder="0" src="' + url + '"></iframe>',
        buttons: [{
            text: '关闭', onclick: function (item, dialog) {
                grid.loadData(true);
                dialog.close();

            }
        }]
    });
}

function search(form) {

    var params = {};
    grid.setOptions({ url: 'CCWorkList.aspx?opt=getCCWorkList&mode=' + mode + '&FK_Node=' + FK_Node + '&' + $("#searchForm").serialize() });
    return false;
}

function getUrlRequestParam(paramName) {
    var args = new Object();
    var query = location.search.substring(1);

    var pairs = query.split("&"); // Break at ampersand 
    for (var i = 0; i < pairs.length; i++) {
        var pos = pairs[i].indexOf('=');
        if (pos == -1) continue;
        var argname = pairs[i].substring(0, pos);
        var value = pairs[i].substring(pos + 1);
        value = decodeURIComponent(value);
        args[argname] = value;
    }
    return args[paramName];
}