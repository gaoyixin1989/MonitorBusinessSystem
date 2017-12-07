//Create By 胡方扬  2012-12-11
//委托书编制环节 自动生成监测点位信息

//说明:   该页面要引用 父页面 vContractInfor变量

var vContractMonitorItems = null;
var MonitorTypeId = "";
var isView = true, tabFirst = true;

$(document).ready(function () {
    var strdivImg = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="监测样品信息"/><span>监测样品信息</span>';
    $(strdivImg).appendTo(divImgItems);
    CreateDiv();
})
//根据委托书ID 获取监测类别
function CreateDiv() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../Contract/MethodHander.ashx?action=GetContractMonitorType&strContratId=" + task_id + "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total!=0) {
                vContractMonitorItems = data.Rows;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });



    if (vContractMonitorItems.length > 0) {
        //根据当前委托书的监测类别 动态生成监测类别
        var newDiv = '<div id="navtab1" position="center"  style = " width: 700px;height:300px; overflow:hidden; border:1px solid #A3C0E8; ">';
        for (var i = 0; i < vContractMonitorItems.length; i++) {
            /// <reference path="../ContractCheckTab.aspx" />

            if (i == 0) {
                MonitorTypeId = vContractMonitorItems[i].ID;
                newDiv += '<div id="div' + MonitorTypeId + '" title="' + vContractMonitorItems[i].MONITOR_TYPE_NAME + '" tabid="home" lselected="true" style="height:300px" >';
                newDiv += '<iframe frameborder="0" name="showmessage' + MonitorTypeId + '" src= "../../SinceSample/SampleCreate/ContractCheckTab_Since.aspx?strContratId=' + task_id + '&strMonitorType=' + MonitorTypeId + '&strCompanyIdFrim=' + vContractInfor[0].TESTED_COMPANY_ID + '&strProject=' + vContractInfor[0].PROJECT_NAME + '&strContractTypeId=' + vContractInfor[0].CONTRACT_TYPE + '&isView=' + isView + ' &strContractCode=' + vContractInfor[0].CONTRACT_CODE + '"></iframe>';
                newDiv += '</div>';
            }

            else {
                MonitorTypeId = vContractMonitorItems[i].ID;
                newDiv += '<div id="div' + MonitorTypeId + '" title="' + vContractMonitorItems[i].MONITOR_TYPE_NAME + '" style="height:300px" >';
                newDiv += '<iframe frameborder="0" name="showmessage' + MonitorTypeId + '" src= "../../SinceSample/SampleCreate/ContractCheckTab_Since.aspx?strContratId=' + task_id + '&strMonitorType=' + MonitorTypeId + '&strCompanyIdFrim=' + vContractInfor[0].TESTED_COMPANY_ID + '&strProject=' + vContractInfor[0].PROJECT_NAME + '&strContractTypeId=' + vContractInfor[0].CONTRACT_TYPE + '&isView=' + isView + ' &strContractCode=' + vContractInfor[0].CONTRACT_CODE + '"></iframe>';
                newDiv += '</div>';
            }
        }
        newDiv += '</div>';
        $(newDiv).appendTo(createDiv);
        $("#navtab1").ligerTab();


        $("#navtab1").ligerTab({
            //在点击选项卡之前触发
            onBeforeSelectTabItem: function (tabid) {
            },
            //在点击选项卡之后触发   点击其他的选项卡后，刷新该选项卡，防止CSS样式被串
            onAfterSelectTabItem: function (tabid) {
//                if (tabFirst) {
                    navtab = $("#navtab1").ligerGetTabManager();
                    navtab.reload(navtab.getSelectedTabItemID());
//                    tabFirst = false;
//                }
            }
        });
    }
}