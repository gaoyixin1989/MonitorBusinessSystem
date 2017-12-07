//功能描述：导入导出通用JS
//创建人：钟杰华
//创建日期：2013-05-08

var urlIE = "../../Scripts/ImportExport.ashx";
$(document).ready(function () {
    var html = "";

    //导入表单
    html += "<div id=\"dataImportDiv\" style=\"display: none\">";
    html += "    <form id=\"dataImportForm\" method=\"post\" enctype=\"multipart/form-data\">";
    html += "    <div style=\"margin: 10px\">";
    html += "        <div style=\"height: 35px; margin-left: 24px\">";
    html += "           <div style=\"float: left\">";
    html += "                规则：";
    html += "            </div>";
    html += "            <div style=\"float: left\">";
    html += "                <select id=\"ddlImportConfig\" name=\"ddlImportConfig\">";
    html += "                </select>";
    html += "            </div>";
    html += "        </div>";
    html += "        <div style=\"height: 35px;\">";
    html += "            数据文件：";
    html += "            <input type=\"file\" id=\"importFile\" name=\"importFile\" style=\"width: 300px\" />";
    html += "        </div>";
    html += "    </div>";
    html += "    <input type=\"hidden\" id=\"action\" name=\"action\" value=\"DataImport\" />";
    html += "    </form>";
    html += "</div>";

    //导出表单
    html += "<div id=\"dataExportDiv\" style=\"display: none\">";
    html += "    <form id=\"dataExportForm\" method=\"post\" enctype=\"multipart/form-data\">";
    html += "    <div style=\"margin: 10px\">";
    html += "        <div style=\"height: 35px; margin-left: 24px\">";
    html += "            <div style=\"float: left\">";
    html += "                规则：";
    html += "            </div>";
    html += "            <div style=\"float: left\">";
    html += "                <select id=\"ddlExportConfig\" name=\"ddlExportConfig\">";
    html += "                </select>";
    html += "            </div>";
    html += "        </div>";
    html += "    </div>";
    html += "    <input type=\"hidden\" id=\"action\" name=\"action\" value=\"DataExport\" />";
    html += "    <input type=\"hidden\" id=\"hidMonthToExport\" name=\"hidMonthToExport\" />";
    html += "    <input type=\"hidden\" id=\"hidYearToExport\" name=\"hidYearToExport\" />";
    html += "    <input type=\"hidden\" id=\"hidDayToExport\" name=\"hidDayToExport\" />";
    html += "    <input type=\"hidden\" id=\"hidSeasonToExport\" name=\"hidSeasonToExport\" />";
    html += "    <input type=\"hidden\" id=\"hidPointIdToExport\" name=\"hidPointIdToExport\" />";
    html += "    <input type=\"hidden\" id=\"hidHourExport\" name=\"hidHourExport\" />";
    html += "    <input type=\"hidden\" id=\"hidMinuteToExport\" name=\"hidMinuteToExport\" />";
    html += "    <input type=\"hidden\" id=\"hidSecondToExport\" name=\"hidSecondToExport\" />";
    html += "    </form>";
    html += "</div>";

    //把表单表入页面中
    $("body").append(html);

    //构建数据导入表单
    $("#ddlImportConfig").ligerComboBox({
        resize: false,
        textField: "CONFIG_NAME",
        valueField: "ID",
        valueFieldID: "hidImportConfig",
        width: 300
    });

    //构建数据导出表单
    $("#ddlExportConfig").ligerComboBox({
        resize: false,
        textField: "CONFIG_NAME",
        valueField: "ID",
        valueFieldID: "hidExportConfig",
        width: 300
    });
});

//弹出数据导入窗口
var dataImportDialog = null;
function showDataImport(envType,getDataFun) {
    if (dataImportDialog) {
        dataImportDialog.show();
    } else {
        getEnvConfig("ddlImportConfig", envType);

        dataImportDialog = $.ligerDialog.open({
            target: $("#dataImportDiv"),
            width: 650, height: 200, top: 90, title: "数据导入",
            buttons: [
                  { text: '确定', onclick: function () { dataImport(getDataFun); dataImportDialog.hide(); } },
                  { text: '取消', onclick: function () { dataImportDialog.hide(); } }
                  ]
        });
    }
}

//弹出数据导出窗口
var dataExportDialog = null;
function showDataExport(envType) {
    if (dataExportDialog) {
        dataExportDialog.show();
    } else {
        getEnvConfig("ddlExportConfig", envType);

        dataExportDialog = $.ligerDialog.open({
            target: $("#dataExportDiv"),
            width: 650, height: 200, top: 90, title: "数据导出",
            buttons: [
                  { text: '确定', onclick: function () { dataExport(); dataExportDialog.hide(); } },
                  { text: '取消', onclick: function () { dataExportDialog.hide(); } }
                  ]
        });
    }
}

//数据导入
function dataImport(getDataFun) {
    $("#dataImportForm").ajaxSubmit({
        url: urlIE,
        type: "post",
        dataType: "json",
        async: true,
        cache: false,
        beforeSend: function () {
            $.ligerDialog.waitting('正在处理中,请稍候...');
        },
        error: function () { $.ligerDialog.error("数据导入出错"); },
        success: function (json) {
            $.ligerDialog.closeWaitting();

            if (json.result == "success") {
                $.ligerDialog.success('导入成功');
                getDataFun();
            } else {
                $.ligerDialog.warn('导入失败');
            }
        }
    });
}

//数据导出
function dataExport() {
    $("#dataExportForm").ajaxSubmit({
        url: urlIE,
        type: "post",
        dataType: "json",
        async: true,
        cache: false,
        beforeSend: function () {
            $.ligerDialog.waitting('正在处理中,请稍候...');
        },
        error: function () { $.ligerDialog.error("数据导出出错"); },
        success: function (json) {
            $.ligerDialog.closeWaitting();

            if (json.result != "") {
                window.open("../ExcelUpload/" + json.result);
            } else {
                $.ligerDialog.warn('导入出败');
            }
        }
    });
}

//获取环境质量类别
function getEnvConfig(objName, envType) {
    $.ajax({
        url: urlIE,
        data: "action=GetEnvConfig&envType=" + envType,
        type: "post",
        dataType: "json",
        async: true,
        cache: false,
        error: function () { $.ligerDialog.error("获取环境质量类别错误"); },
        success: function (json) {
            $.ligerui.get(objName).setData(json);
        }
    });
}