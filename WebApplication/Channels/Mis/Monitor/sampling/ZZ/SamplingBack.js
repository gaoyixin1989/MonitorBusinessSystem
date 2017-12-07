// Create by 熊卫华 2013.09.10  "采样任务退回功能"功能

var objOneGrid = null;
var strUrl = "SamplingBack.aspx";
var strOneGridTitle = "采样任务退回列表";

$(document).ready(function () {
    objOneGrid = $("#oneGrid").ligerGrid({
        title: strOneGridTitle,
        dataAction: 'server',
        usePager: true,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '99%',
        pageSizeOptions: [5, 10, 15, 20],
        height: '99%',
        enabledSort: false,
        url: strUrl + '?type=getOneGridInfo',
        columns: [
                { display: '项目名称', name: 'PROJECT_NAME', align: 'left', width: 360, render: function (items) {
                    var strNewValue = ReturnSubValue(items.CONTRACT_TYPE, items.PLAN_NUM);
                    if (strNewValue != "") {
                        return items.PROJECT_NAME + " (" + strNewValue + ")";
                    }
                    return items.PROJECT_NAME;
                }
                },
                { display: '任务下达日期', name: 'PLANDATE', width: 120, minWidth: 60, align: 'left', render: function (items) {
                    if (items.PLAN_YEAR != "") {
                        return items.PLAN_YEAR + '年' + items.PLAN_MONTH + '月' + items.PLAN_DAY + '日';
                    }
                }
                },
                 { display: '要求完成日期', name: 'ASKING_DATE', width: 120, minWidth: 60, align: 'left', render: function (items) {
                     var strDate = "";
                     var v = items.ASKING_DATE;
                     if (v != "") {
                         var contractData = new Date(Date.parse(v.replace(/-/g, '/')))
                         strDate += contractData.getFullYear() + "年";
                         strDate += (contractData.getMonth() + 1) + "月";
                         strDate += contractData.getDate() + "日";
                         return strDate;
                     }
                     return items.ASKING_DATE;
                 }
                 },
                { display: '委托单号', name: 'CONTRACT_CODE', width: 200, minWidth: 60 },
                { display: '任务单号', name: 'TICKET_NUM', width: 200, minWidth: 60 },
                { display: '已选监测类型', name: 'TYPES', width: 160, minWidth: 60, align: 'left', render: function (items) {
                    var strValue = GetContractMonitorType(items.CONTRACT_ID, items.ID);
                    if (strValue.length > 20) {
                        return "<a title=" + strValue + ">" + strValue.substring(0, 20) + "......</a>"
                    }
                    return strValue;
                }
                },
                { display: '受检单位', name: 'COMPANY_NAME', align: 'left', width: 180, minWidth: 60 },
                { display: '所属区域', name: 'AREA_NAME', width: 150, minWidth: 60 }
                ],
        toolbar: { items: [
                { text: '退回', click: GoToBack, icon: 'add'}]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
    //退回监测监测项目 
    function GoToBack() {
        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('退回之前请先选择一条任务信息');
            return;
        }
        $.ligerDialog.confirm('您确认退回该监测任务吗？', function (yes) {
            if (yes == true) {
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: strUrl + "/GoToBack",
                    data: "{'strTaskId':'" + objOneGrid.getSelectedRow().TASK_ID + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        strValue = data.d;
                        if (strValue == "1") {
                            objOneGrid.loadData();
                            $.ligerDialog.success('监测任务退回成功')
                        }
                        else {
                            $.ligerDialog.warn('监测任务退回失败');
                        }
                    }
                });
            }
        });
    }
});
function ReturnSubValue(strType, strPlanNum) {
    var strValue = "";
    //清远 11 表示 省控  13表示年度委托监测
    if (strType == "11" || strType == "13") {
        strValue = "第" + "<a style='color:Red;font-weight:Bold'>" + strPlanNum + "</a>" + "次";
    }
    return strValue;
}
function GetContractMonitorType(strtask_id, strplan_id) {
    vMonitorArrList = null;
    strMonitorName = "";
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: '../../../MonitoringPlan/MonitoringPlan.ashx?action=GetPointMonitorInfor&task_id=' + strtask_id + '&strPlanId=' + strplan_id + '&strIfPlan=1',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != 0) {
                vMonitorArrList = data.Rows;
                for (var i = 0; i < vMonitorArrList.length; i++) {
                    strMonitorName += vMonitorArrList[i].MONITOR_TYPE_NAME + ";";
                }
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
    if (strMonitorName != "") {
        strMonitorName = strMonitorName.substring(0, strMonitorName.length - 1);
    }
    return strMonitorName;
}