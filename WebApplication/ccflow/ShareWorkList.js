var grid,tab,grid2;
$(function () {
    tab = $("#tab").ligerTab({
        onAfterSelectTabItem: function (tabid) {
            if (tabid == 'unObtained' && (grid2 == null)) {
                grid2 = $("#grid2").ligerGrid({
                    width: '100%', height: '100%', usePager: true, rownumbers: true, allowHideColumn: false, enabledSort: false,
                    url: 'ShareWorkList.aspx?opt=getShareWorkList&isObtain=false',
                    columns: [
                            { display: '主题', name: 'Title', align: 'left',
                                render: function (row, index, value) {
                                    return '<a href="javascript:;" onclick="handleWork(\'' + row.Operation + '\')">' + value + '</a>'
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
                            { display: '完成期限', name: 'SDT', align: 'left' },
                            { display: '当前节点状态', name: 'NodeState',
                                render: function (row, index, value) {
                                    if (value == '正常') {
                                        return value;
                                    } else {
                                        return '<font color="red">' + value + '</font>';
                                    }
                                }
                            },
                            { display: '流程状态', name: 'WFStateText' }

                        ]
                });
            } else if (tabid == 'unObtained') {
                grid2.loadData(true);
            } else {
                grid.loadData(true);
            }
        }
    });
    grid = $("#todoGrid").ligerGrid({
        width: '100%', height: '100%', usePager: true, rownumbers: true, allowHideColumn: false, enabledSort: false,
        url: 'ShareWorkList.aspx?opt=getShareWorkList&isObtain=true',
        columns: [
            { display: '主题', name: 'Title', align: 'left',
                render: function (row, index, value) {
                    return '<a href="javascript:;" onclick="handleWork(\'' + row.Operation + '\')">' + value + '</a>'
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
            { display: '完成期限', name: 'SDT', align: 'left' },
            { display: '当前节点状态', name: 'NodeState',
                render: function (row, index, value) {
                    if (value == '正常') {
                        return value;
                    } else {
                        return '<font color="red">' + value + '</font>';
                    }
                }
            },
            { display: '流程状态', name: 'WFStateText' }

        ]
    });



});

    function handleWork(url) {
        var selectedTabId = tab.getSelectedTabItemID();
        if ('unObtained' == selectedTabId) {
            $.ligerDialog.confirm("您确定要申请下来该任务吗？", function (r) {
                if (r) {
                    var str = window.showModalDialog(url, '', 'dialogHeight: 50px; dialogWidth:50px; dialogTop: 100px; dialogLeft: 100px; center: no; help: no');
                    if (str == undefined ||str==null) {
                        grid2.loadData(true);
                        return;
                    }
                    
                }
            });
        } else {
            $.ligerDialog.confirm("您确定要放回共享池吗？", function (r) {
                if (r) {
                    var str = window.showModalDialog(url, '', 'dialogHeight: 50px; dialogWidth:50px; dialogTop: 100px; dialogLeft: 100px; center: no; help: no');
                    if (str == undefined || str == null) {
                        grid.loadData(true);
                        return;
                    }
                }
            });
        }
   
}



function search(form) {
    var params = {};
    var selectedTabId = tab.getSelectedTabItemID();
    if ('isObtained' == selectedTabId)
        grid.setOptions({ url: 'ShareWorkList.aspx?opt=getShareWorkList&isObtain=true&' + $("#searchForm").serialize() });
    else
        grid2.setOptions({ url: 'ShareWorkList.aspx?opt=getShareWorkList&isObtain=false&' + $("#searchForm").serialize() });
    return false;
}