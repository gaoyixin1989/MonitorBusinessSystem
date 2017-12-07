//多附件上传
var objGrid = "";
var Doc_ID = "";
var strUrl = "AttMoreFileUpLoad.aspx";
$(document).ready(function () {
    objGrid = $("#Grid").ligerGrid({
        title: "附件上传",
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
        toolbar: { items: [{ text: '新增', click: SaveFWDate, icon: 'add' }, { line: true }, { text: '删除', click: deleteData, icon: 'delete'}] },
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
//新增
function SaveFWDate() {
    upLoadFile();
}
 //删除
function deleteData() {
    if (objGrid.getSelectedRow() == null) {
        $.ligerDialog.warn('请选择一条记录进行删除');
        return;
    }
    $.ligerDialog.confirm('确认删除已经选择的信息吗？', function (yes) {
        if (yes == true) {
            var strValue = objGrid.getSelectedRow().ID;
            $.ajax({
                cache: false,
                type: "POST",
                url: strUrl + "/deleteDataInfo",
                data: "{'strValue':'" + strValue + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.d == "1") {
                        DeleteAfterDate();
                        $.ligerDialog.success('删除数据成功')
                    }
                    else {
                        $.ligerDialog.warn('删除数据失败');
                    }
                }
            });
        }
    });
}
function DeleteAfterDate() {
    if (getQueryString("ID") != null) {
        objGrid.set("url", strUrl + "?action=getInfo&ID=" + getQueryString("ID") + "&filetype=" + getQueryString("filetype") + "&IsInsert=false");
    }
}
///附件上传
function upLoadFile() {
    Doc_ID = getQueryString("id"); //获取ID
    var strID = Doc_ID;
    if (objGrid.rows.length > 0) {
        strID += "_" + (parseInt(objGrid.rows[objGrid.rows.length - 1].BUSINESS_ID.split('_')[1]) + 1);
        //strID += "_" + ((parseInt(((objGrid.rows[objGrid.rows.length - 1].BUSINESS_ID) + 1).substr(10, 1))) + 1);
    }
    else {
        strID += "_1";
    }
    var filetype = getQueryString("filetype"); //类型
    $.ligerDialog.open({ title: '附件上传', width: 500, height: 250, isHidden: false,
        buttons: [
         { text: '上传', onclick: function (item, dialog) {
             dialog.frame.upLoadFile();
             GetData(filetype);
             dialog.close();
         }
         },
            { text: '关闭',
                onclick: function (item, dialog) { GetData(filetype); dialog.close(); }
            }], url: 'AttFileUpload.aspx?ID=' + strID + '&filetype=' + filetype
    });
}
//获取数据
function GetData(type) {
    var ID = Doc_ID;
    //alert(type);
    if (ID != "") {
        objGrid.set("url", strUrl + "?action=getInfo&ID=" + ID + "&IsInsert=false&filetype=" + type);
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