//Create By Castle(胡方扬) 2012-11-30


var vRItems = "", vRptItems = "", vContractMonitorItems = "";
var MonitorTypeId = "", strProject="";
var manager = null;
var tabFirst = true;
var strRpt_Way = ""; strRemarks1 = "", strRemarks2 = "", strRemarks3 = "", strRemarks4 = "", strContract_Date = "", strMonitor_Purpose = "",strRemarktData = "";
var strConsUrl = "";
$(document).ready(function () {

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../MethodHander.ashx?action=GetWebConfigValue&strKey=ConstUrl",
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                strConsUrl = data;
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../MethodHander.ashx?action=GetHidConstStatus",
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                if (data == "1") {
                    $("#thConst").attr("style", "display:none");
                }
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
    var strdivImg = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="核对委托书信息"/><span>核对委托书信息</span>';
    $(strdivImg).appendTo(divImgContract);
    $("#imgIcon").attr("src", "../../../../Images/Icons/money_yen.png");

    if (strContractCode != "") {
        //        var jsExport = "";
        //        if (isExport == "1") {
        //            jsExport += "  <a href='javascript:ExportContract();'>导出委托书</a>"
        //        }
        $("#Contract_Code").html(strContractCode);
    }
    else {
        $("#Contract_Code").attr("style", "color:Red;font-weight:bold")
        $("#Contract_Code").html('委托单号尚未生成');
    }
    $("#constDetail").html('0');
    GetConstractFeeCount();

    $("#btshowConst").bind("click", function () {
        if (strConsUrl != "") {
            strConsUrl += '?strContractId=' + strContratId + '&strContractCode=' + strContractCode + '&isView=' + isView
        }
        $.ligerDialog.open({ title: '监测费用明细', top: 40, width: 700, height: 550, buttons:
        [{ text: '确定', onclick: f_SaveConstData },
         { text: '返回', onclick: function (item, dialog) { GetConstractFeeCount(); dialog.close(); }
         }], url: strConsUrl
        });
    })
//    if (strContractCode != "") {
//        $("#Contract_Code").html(strContractCode);
//    }
//    else {
//        $("#Contract_Code").attr("style", "color:Red;font-weight:bold")
//        $("#Contract_Code").html('委托单号尚未生成');
//    }
//    $("#constDetail").html('0');
//    GetConstractFeeCount();

//    $("#btshowConst").bind("click", function () {
//        $.ligerDialog.open({ title: '监测费用明细', top: 40, width: 700, height: 550, buttons:
//        [{ text: '确定', onclick: f_SaveConstData },
//         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
//         }], url: '../ProgramInforDetail/ContractConstDetail.aspx?strContractId=' + strContratId + '&strContractCode=' + strContractCode + '&isView=' + isView
//        });
//    })


    function f_SaveConstData(item, dialog) {


        var fn = dialog.frame.GetChildInputValue || dialog.frame.window.GetChildInputValue;
        var strRequestdata = fn();
        SaveConstData(strRequestdata);
    }

    function SaveConstData(strRequestdata) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../MethodHander.ashx?action=UpdateConstractFeeCount&strContratId=" + strContratId + strRequestdata,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    $.ligerDialog.success('数据保存成功！');
                    GetConstractFeeCount();
                }
                else {
                    parent.$.ligerDialog.warn('数据保存失败！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
    //获取核对委托信息的 备注信息
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../MethodHander.ashx?action=GetDict&type=Contract_Remarks",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vRItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    //获取核对委托信息的 报告领取方式
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../MethodHander.ashx?action=GetDict&type=RPT_WAY",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vRptItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });


    $("#Rpt_Way").ligerComboBox({ data: vRptItems, width: 130, valueFieldID: 'Rpt_Way_OP', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false, initValue: SetRptboxInitValue() });
    manager = $("#Remarks").ligerComboBox({ data: vRItems, width: 500, valueFieldID: 'Remarks_OP', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isShowCheckBox: true, isMultiSelect: true, initValue: SetboxInitValue() });
    $("#Contract_Date").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime() });

    if (isAdd == false||!strType) {
        SetInputValue();
    }
    if (isAdd == true) {
        $("#Monitor_Purpose").val('排污状况监测');
    }

    function SetRptboxInitValue() {
        var strValue = "";
        if (isAdd == true) {
            strValue = vRptItems[0].DICT_CODE;
        }
        return strValue;
    }

    function SetboxInitValue() {
        var strValue = "";
        if (isAdd == true) {
            for (var i = 0; i < vRItems.length; i++) {
                strValue += vRItems[i].DICT_CODE + ";";
            }
            strValue = strValue.substring(0, strValue.length - 1);
        }
        else {
            strValue = strRemarktData;
        }
        return strValue;
    }

    //JS 获取当前时间
    function currentTime() {

        var d = new Date(), str = '';
        if (isAdd == true) {
            str += d.getFullYear() + '-';
            str += d.getMonth() + 1 + '-';
            str += d.getDate();
            //      str += d.getHours() + '时';
            //      str += d.getMinutes() + '分';
            //      str += d.getSeconds() + '秒';
        }
        else {
            str = strContract_Date;
        }
        return str;
    }

})

function GetConstractFeeCount() {
    // 获取监测费用总计
    if (strContratId != "") {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../MethodHander.ashx?action=GetConstractFeeCount&strContratId=" + strContratId + "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    vSumList = data.Rows;
                    $("#constDetail").html(vSumList[0].INCOME);
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
}
//根据委托书ID 获取监测类别
function CreateDiv() {
    strProject = $("#Project_Name").val();
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../MethodHander.ashx?action=GetContractMonitorType&strContratId=" + strContratId + "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
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
        var newDiv = '<div id="navtab1" position="center"  style = " width: 720px;height:300px; overflow:hidden; border:1px solid #A3C0E8; ">';
        for (var i = 0; i < vContractMonitorItems.length; i++) {

            if (i == 0) {
                MonitorTypeId = vContractMonitorItems[i].ID;
                newDiv += '<div id="div' + MonitorTypeId + '" title="' + vContractMonitorItems[i].MONITOR_TYPE_NAME + '" tabid="home" lselected="true" style="height:300px" >';
                newDiv += '<iframe frameborder="0" name="showmessage' + MonitorTypeId + '" src= "ContractCheckTab.aspx?strContratId=' + strContratId + '&strMonitorType=' + MonitorTypeId + '&strCompanyIdFrim=' + strCompanyIdFrim + '&strProject=' + strProject + '&strContractTypeId=' + strContratTypeId + '&isView='+ isView +' &strContractCode=' + strContractCode + '"></iframe>';
                newDiv += '</div>';
            }

            else {
                MonitorTypeId = vContractMonitorItems[i].ID;
                newDiv += '<div id="div' + MonitorTypeId + '" title="' + vContractMonitorItems[i].MONITOR_TYPE_NAME + '" style="height:300px" >';
                newDiv += '<iframe frameborder="0" name="showmessage' + MonitorTypeId + '" src= "ContractCheckTab.aspx?strContratId=' + strContratId + '&strMonitorType=' + MonitorTypeId + '&strCompanyIdFrim=' + strCompanyIdFrim + '&strProject=' + strProject + '&strContractTypeId=' + strContratTypeId + '&isView=' + isView +'&strContractCode=' + strContractCode + '"></iframe>';
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
                    navtab.reload(tabid);
//                    tabFirst = false;
//                }
            }
        });
        }
        GetConstractFeeCount();
}
function SetInputValue() {
    if (strMonitor_Purpose != "") {
        $("#Monitor_Purpose").val(strMonitor_Purpose);
    }
    $("#Rpt_Way").ligerGetComboBoxManager().setValue(strRpt_Way);


    if (isView == true) {
        $("#Monitor_Purpose").ligerTextBox({ disabled: true });
        $("#Project_Name").ligerTextBox({ disabled: true });
        $("#Rpt_Way").ligerGetComboBoxManager().setDisabled();
        $("#Remarks").ligerGetComboBoxManager().setDisabled();
        $("#Contract_Date").ligerGetComboBoxManager().setDisabled();
    }
}