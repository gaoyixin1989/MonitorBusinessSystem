var grid;
$(function () {
    
    grid = $("#todoGrid").ligerGrid({
        width: '100%', height: '100%', usePager: true, rownumbers: true, allowHideColumn: false, enabledSort: false,
        url: 'StartHistoryList.aspx?opt=getHisotryList&flowId='+flowId,
        columns: [
            { display: '任务主题', name: 'Title', align: 'left',width:300,
                render: function (row, index, value) {
                    return '<a href="javascript:;" onclick="viewWork(\'' + row.TitleUrl + '\')">' + value + '</a>'
                }
            },
            
            { display: '发起时间', name: 'RDT', align: 'left',width:120 },
            { display: '参与者', name: 'FlowEmps', align: 'left' },
            
            { display: '流程状态', name: 'WFStateText',width:80 }
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

function search(form) {
    var params = {};
    grid.setOptions({ url: 'StartHistoryList.aspx?opt=getHisotryList&flowId='+flowId+'&' + $("#searchForm").serialize() });
    return false;
}