var grid;
$(function () {

    grid = $("#todoGrid").ligerGrid({
        width: '100%', height: '100%', usePager: true, rownumbers: true, allowHideColumn: false, enabledSort: false,
        url: 'RuningWorkList.aspx?opt=getRuningWorkList',
        columns: [
            { display: '操作', name: 'WorkID', align: 'center',width:80,
                render: function (row, index, value) {
                    var html = '<a href="javascript:;" onclick="handleUnSend(\'' + row.UnSendUrl + '\')">撤销</a>';
                    html += '&nbsp;<a href="javascript:;" onclick="handlePress(\'' + row.PressUrl + '\')">催办</a>';
                    return html;
                }
            },
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
            { display: '待办人', name: 'TodoEmps', align: 'left' },

            { display: '流程状态', name: 'WFStateText' }

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
    grid.setOptions({ url: 'RuningWorkList.aspx?opt=getRuningWorkList&' + $("#searchForm").serialize() });
    return false;
}