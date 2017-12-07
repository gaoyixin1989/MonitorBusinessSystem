var grid;
$(function () {

    grid = $("#todoGrid").ligerGrid({
        width: '100%', height: '100%', usePager: true, rownumbers: true, allowHideColumn: false, enabledSort: false,
        url: 'HangUpWorkList.aspx?opt=getHangUpWorkList',
        columns: [

            { display: '主题', name: 'Title', align: 'left',
                render: function (row, index, value) {
                    return '<a href="javascript:;" onclick="viewWork(\'' + row.TitleUrl + '\')">' + value + '</a>'
                }
            },
            { display: '流程名称', name: 'FlowName', align: 'left' },
            { display: '当前节点', name: 'NodeName', align: 'left' },
            { display: '发起人', name: 'StarterName', align: 'left',
                render: function (row, index, value) {
                    return value + '(' + row.DeptName + ')';
                }
            },
            { display: '发起时间', name: 'RDT', align: 'left' },
            { display: '接收日期', name: 'SDTOfFlow', align: 'left' },
            { display: '完成期限', name: 'SDTOfNode', align: 'left' },
            { display: '节点状态', name: 'NodeState', render: function (row, index, value) {
                if (value == '正常') {
                    return value;
                } else {
                    return '<font color="red">' + value + '</font>';
                }
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
                dialog.close();
                grid.loadData(true); 
            }
        }]
    });
}
//撤销
function handleUnSend(url) {
    $.ligerDialog.confirm("您确定要撤销发送吗？", function (r) {
        if (r) {
            $.ligerDialog.open({
                title: '任务撤销', width: 400, height: 300, showMax: true, allowClose: false,
                content: '<iframe style="width:100%;height:100%" frameborder="0" src="' + url + '"></iframe>',
                buttons: [{
                    text: '关闭', onclick: function (item, dialog) {
                        grid.loadData(true);
                        dialog.close();
                    }
                }]
            });
        }
    });
}

//催办
function handlePress(url) {
    $.ligerDialog.open({
        title: '任务催办', width: 500, height: 400, showMax: true, allowClose: false,
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
    grid.setOptions({ url: 'HangUpWorkList.aspx?opt=getHangUpWorkList&' + $("#searchForm").serialize() });
    return false;
}