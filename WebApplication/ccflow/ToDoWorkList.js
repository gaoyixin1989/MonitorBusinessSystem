var grid;
$(function () {

    var FK_Node = $.getUrlVar('FK_Node');

    FK_Node = (FK_Node == null || FK_Node == undefined) ? '' : FK_Node;

    grid = $("#todoGrid").ligerGrid({
        width: '100%', height: '100%', usePager: true, rownumbers: true, allowHideColumn: false, enabledSort: false,
        url: 'ToDoWorkList.aspx?opt=getToDoWorkList&FK_Node=' + FK_Node,
        columns: [
            { display: '任务主题', name: 'Title', align: 'left',
                render: function (row, index, value) {
                    return '<a href="javascript:;" onclick="handleWork(\'' + row.TitleUrl + '\')">' + value + '</a>'
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
            { display: '接收时间', name: 'ADT', align: 'left' },
            //{ display: '要求开始时间', name: 'BusinessStartTime', align: 'center' },
            { display: '要求完成期限', name: 'SDT', align: 'center',
                render: function (row, index, value) {
                    if (value != '') {
                        return value.split(' ')[0];
                    }
                    else {
                        return value;
                    }
                }
            },
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
    // by yinchengyi 2015-7-14 改为在tab页面中显示 以有效利用空间
    //by yinchengyi 2015-7-20 由固定的tabID改成自动生成tabID。因固定的tabID存在缓存问题，导致代办列表任务下发后，必须重新登录才能继续处理其他任务，否则报告“当前的工作已经被处理，或者您没有执行此工作的权限”
    top.f_addTab("任务办理", "任务办理", url);

    //    $.ligerDialog.open({
    //        title: '任务处理', width: $("body").width() - 20, height: $("body").height() - 50, showMax: true, allowClose: false,
    //        content: '<iframe style="width:100%;height:100%" frameborder="0" src="' + url + '"></iframe>',
    //        buttons: [{
    //            text: '关闭', onclick: function (item, dialog) {
    //                dialog.close();
    //                grid.loadData(true); 
    //            }
    //        }]
    //    });
}

function search(form) {

    var FK_Node = $.getUrlVar('FK_Node');

    var params = {};
    grid.setOptions({ url: 'ToDoWorkList.aspx?opt=getToDoWorkList&FK_Node=' + FK_Node + '&' + $("#searchForm").serialize() });
    return false;
}


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