var managertmp;
var obj;
var strUrl = "AnalysisResult_Lims.aspx";
var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var std_line_index = "";
var gridName = null;
var isFisrt = null;
$(document).ready(function () {
    $("#layout1").ligerLayout({ leftWidth: 200, allowLeftCollapse: false, height: '100%' });

    $("#navtab1").ligerTab({ contextmenu: false, onBeforeSelectTabItem: function (tabid) {
    },
        //在点击选项卡之后触发   点击其他的选项卡后，刷新该选项卡，防止CSS样式被串
        onAfterSelectTabItem: function (tabid) {
            var navtab = $("#navtab1").ligerGetTabManager();
            if (tabid == "home") {
                gridName = "0";
            }
            if (tabid == "tabitem1") {
                gridName = "1";
            }
            navtab.reload(navtab.getSelectedTabItemID());
            if (tabid != 'home') {
                isFisrt = false;
                objTwoGrid.set("url", strUrl + '?type=getTwoGridInfo');
            }
            else {
                isFisrt = true;
            }
        }
    });



    obj = $("#divAttr").ligerForm({ inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right', fields: [] });
    managertmp = $.ligerui.managers.divAttr;

    //获取所有属性类别及属性信息关联数据
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "?type=GetInfo&ResultID=" + request('ResultID'),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            //if (data.length > 0) {
            GetControlJson(data);
            initControlValue(data);
            //}
        }
    });

    $("#line_table").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 20,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: "100%",
        height: "100%",
        enabledSort: false,
        pageSizeOptions: [10, 15, 20],
        url: 'AnalysisResult_Lims.aspx?type=showLineTable',
        columns: [
                     { display: '工作曲线名称', name: 'TIME', align: 'left', width: 130, minWidth: 60 }
                    ],

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
            std_line_index = rowdata.TIME;

            //初始化FlashChart
            initchart("divGraph");
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });


});


function GetControlJson(data) {
    var strdata = { inputWidth: 160, labelWidth: 160, space: 60, Height: 9000, labelAlign: 'right', fields: [] };


    strdata.fields.push(GetJsonFieldData("仪器名称", "EQUIP_NAME", "true", false));
    strdata.fields.push(GetJsonFieldData("仪器编号", "EQUIP_NO", "false", false));
    strdata.fields.push(GetJsonFieldData("分析方法", "EQUIP_METHOD", "true", false));
    strdata.fields.push(GetJsonFieldData("检出限", "EQUIP_LOCK", "false", false));

    $("#divAttr").html("");

    $.ligerui.managers["divAttr"] = [];
    $.ligerui.managers["divAttr"] = managertmp;

    var obj1 = $("#divAttr").ligerForm(strdata);
    obj1._render();
}

function initControlValue(data) {
    $("#EQUIP_NAME").val(request('name'));
    $("#EQUIP_NO").val(request('code'));
    $("#EQUIP_METHOD").val(request('method'));
    $("#EQUIP_LOCK").val(request('lock'));

}

function GetJsonFieldData(displayName, conName, newLine, ifGroup) {
    var strdata1 = "";
    strdata1 += "{ display: '" + displayName + "',";
    strdata1 += "name:'" + conName + "',";
    strdata1 += "newline:" + newLine + ",";
    strdata1 += "type:'text'";
    if (ifGroup) {
        strdata1 += ",group: '仪器数据', groupicon: groupicon";
    }
    strdata1 += "}";

    var strJsonData = eval('(' + strdata1 + ')');

    return strJsonData;
}

function initchart(chart) {
    var params = { "wmode": "transparent" };
    swfobject.embedSWF("../../../../Controls/OpenFlashChart/open-flash-chart-SimplifiedChinese.swf", chart, "780", "400", "7.0.0", "", { 'save_image_message': '图片另存' }, params);
}

function open_flash_chart_data() {
    var strJsonData = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "?type=getinitchartdata&TIME=" + std_line_index,
        contentType: "application/json; charset=utf-8",
        dataType: "text",
        success: function (data, textStatus) {
            if (data.length > 0) {
                strJsonData = data;
            }
        }
    });
    return strJsonData;
}
