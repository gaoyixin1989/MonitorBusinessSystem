$(function () {
    $("#flowGrid").ligerGrid({
        width: '100%', height: '100%', usePager: false, rownumbers: true, allowHideColumn: false, enabledSort: false,
        url: 'FlowList.aspx?opt=getFlowList',
        groupColumnName: 'FK_FlowSortText', groupColumnDisplay: '类别:',
        columns: [
            { display: '流程类别', name: 'FK_FlowSortText', align: 'left', hide: true },
            { display: '流程名称', name: 'Name', align: 'left',
                render: function (row, index, value) {
                    return '<a href="javascript:;" onclick="openStartFlow(\'' + value + '\',\'' + row.NameUrl + '\')">' + value + '</a>';
                }
            },
            { display: '流程图', name: 'FlowPic', align: 'center',width:50,
                render: function (row, index, value) {
                    return '<a href="javascript:;" onclick="viewFlowPicture(\'' + row.Name + '\',\'' + value + '\')">查看</a>';
                }
            },
            { display: '历史发起', name: 'No', align: 'center', width: 50,
                render: function (row, index, value) {
                    return '<a href="javascript:;;" onclick="viewFlowStartHistory(\''+row.Name+'\',\''+row.No+'\')">查看</a>';
                }
            },
            { display: '描述', name: 'Note', align: 'left' }
        ]
    });
});
    //工作发起
    function openStartFlow(name,url) {
//        $.ligerDialog.open({
//            title: name + '发起', width: $("body").width() - 20, height: $("body").height() - 50, showMax: true,
//            content: '<iframe style="width:100%;height:100%" frameborder="0" src="'+url+'"></iframe>'
//        });
        // by yinchengyi 2015-9-18 新建任务嵌入tab，有效利用空间
        top.f_addTab("newFlow", "新建任务", url); 
    }
    //流程图查看
    function viewFlowPicture(name,pic) {
        $.ligerDialog.open({
            title: name + '流程图', width: $("body").width() - 20, height: $("body").height() - 50, showMax: true,
             content:'<img src="'+pic+'">'
        });
     }

     function viewFlowStartHistory(name, flowId) {
         parent.f_addTab('FlowHistory' + flowId, name+'---发起历史', '../ccflow/StartHistoryList.aspx?flowId='+flowId);
     }