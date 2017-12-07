var grid;
$(function () {
    
    grid = $("#todoGrid").ligerGrid({
        width: '100%', height: '100%', usePager: true, rownumbers: true, allowHideColumn: false, enabledSort: false,
        url: 'CompleteWorkList.aspx?opt=getCompleteWorkList',
        columns: [
            { display: '任务主题', name: 'Title', align: 'left',
                render: function (row, index, value) {
                    return '<a href="javascript:;" onclick="viewWork(\'' + row.TitleUrl + '\')">' + value + '</a>'
                }
            },
            { display: '流程名称', name: 'FlowName', align: 'left' },
            { display: '完成节点', name: 'NodeName', align: 'left' },
            { display: '发起人', name: 'StarterName', align: 'left',
                render: function (row, index, value) {
                    return value + '(' + row.DeptName + ')';
                }
            },
            { display: '发起时间', name: 'RDT', align: 'left' },
            { display: '完成时间', name: 'FlowEnderRDT', align: 'left' },
            { display: '参与者', name: 'FlowEmps' }
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
                
            }
        }]
    });
}

function search(form) {
    var params = {};
    grid.setOptions({ url: 'CompleteWorkList.aspx?opt=getCompleteWorkList&' + $("#searchForm").serialize() });
    //grid.loadData(true); 
    return false;
}