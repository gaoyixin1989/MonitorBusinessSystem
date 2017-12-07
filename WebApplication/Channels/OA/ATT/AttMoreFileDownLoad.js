//多方案下载
var objGrid = "";
var Doc_ID = "";
var strUrl = "AttMoreFileDownLoad.aspx";
$(document).ready(function () {
    objGrid = $("#Grid").ligerGrid({
        title: "方案下载",
        dataAction: 'server',
        usePager: false,
        pageSize: 5,
        pageSizeOptions: [5, 10],
        alternatingRow: true,
        checkbox: true,
        enabledEdit: false,
        width: '98%',
        height: "98%",
        url: strUrl + "?action=add&ID=" + getQueryString("ID") + "&filetype=" + getQueryString("filetype"),
        columns: [
               { display: 'ID', name: 'ID', align: 'left', width: 50, minWidth: 50 },
                { display: 'BUS_ID', name: 'BUSINESS_ID', align: 'left', width: 50, minWidth: 50 },
                { display: '附件', name: 'UPLOAD_PATH', align: 'left', width: 200, minWidth: 300 },
                { display: '标题', name: 'ATTACH_NAME', align: 'left', minWidth: 120, width: 200 },
                { display: '文件类型', name: 'ATTACH_TYPE', minWidth: 60, width: 60 },
                { display: '大小', name: 'REMARKS', minWidth: 50, width: 60 },
                { display: '描述', name: 'DESCRIPTION', minWidth: 300, width: 300}],
                onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //去除第一行第一列的复选框
    objGrid.toggleCol("ID"); //隐藏填报单ID
    objGrid.toggleCol("BUSINESS_ID"); //隐藏填报单ID
    objGrid.toggleCol("UPLOAD_PATH"); //隐藏填报单ID
});

//获取数据
function GetData() {
    var ID = Doc_ID;
    if (ID != "") {
        objGrid.set("url", strUrl + "?action=getInfo&ID=" + ID + "&filetype=" + getQueryString("filetype") + "&IsInsert=false");
    }
}
//下载方法
function aa() {
    if (objGrid.getSelectedRow() == null) {
        $.ligerDialog.warn('请选择一条记录进行办理');
        return;
    }
    if (objGrid.getSelectedRow().BUSINESS_ID.length > 0) {
        $("#hidSave").val(objGrid.getSelectedRow().ID); //勾选的行ID
        $("#hidType").val(getQueryString("ID")); //row_ID为填报数据的第一行的ID
        $("#btnExcelOut").click();
    }
}