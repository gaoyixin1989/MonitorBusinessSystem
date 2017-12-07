var filetype = "";
var FillID = "";
var row_ID = "";
var strUrl = "FillAttachmentaspx.aspx";
$(document).ready(function () {
    objGrid = $("#Grid").ligerGrid({
        title: "填报附件",
        dataAction: 'server',
        usePager: true,
        pageSize: 5,
        pageSizeOptions: [5, 10],
        alternatingRow: true,
        checkbox: true,
        enabledEdit :false,
        width: '98%',
        height: "98%",
        url: strUrl + "?action=getInfo&filetype=" + getQueryString("filetype") + "&Save=" + getQueryString("Save") + "&IsInsert=" + getQueryString("IsInsert") + "&ID=" + getQueryString("ID"),
        columns: [
                { display: '报单ID', name: 'BUSINESS_ID', align: 'left', width: 50, minWidth: 50 },
                { display: '附件', name: 'UPLOAD_PATH', align: 'left', width: 200, minWidth: 300 },
                { display: '标题', name: 'ATTACH_NAME', minWidth: 120, width: 200 },
                { display: '文件类型', name: 'ATTACH_TYPE', minWidth: 60, width: 60 },
                { display: '大小', name: 'REMARKS', minWidth: 50, width: 60 },
                { display: '描述', name: 'DESCRIPTION', minWidth: 300, width: 300}],
        toolbar: { items: [{ text: '新增', click: SaveFWDate, icon: 'add' }, { line: true }, { text: '删除', click: deleteData, icon: 'delete'}] } ,
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }

    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //去除第一行第一列的复选框
    objGrid.toggleCol("BUSINESS_ID"); //隐藏填报单ID
    objGrid.toggleCol("UPLOAD_PATH"); //隐藏填报单ID
});

 function SaveFWDate() {
     if (getQueryString("filetype") != null && getQueryString("Save") != null) {
         var Save = getQueryString("Save");
         row_ID = getQueryString("ID");
         filetype = getQueryString("filetype");
         if (row_ID != "" && filetype != "") {
             $.ajax({
                 cache: false,
                 async: false, //设置是否为异步加载,此处必须
                 type: "POST",
                 url: strUrl + "?action=getInfo&Save=" + Save + "&filetype=" + filetype + "&IsInsert=true",
                 dataType: "json",
                 success: function (json) {
                     if (json != "" && json.result == "success") {
                         FillID = json.ID; //创建填报单的ID
                         upLoadFile();
                     }
                 },
                 error: function (msg) {
                     $.ligerDialog.warn('AJAX数据请求失败！');
                 }
             });
         }
     }
 }

 ///附件上传
 function upLoadFile() {
     $.ligerDialog.open({ title: '附件上传', width: 500, height: 250, isHidden: false,
         buttons: [
         { text: '上传', onclick: function (item, dialog) {
             dialog.frame.upLoadFile();
         }
         },
            { text: '关闭',
                onclick: function (item, dialog) { GetData(); dialog.close(); }
            }], url: '../../../OA/ATT/AttFileUpload.aspx?filetype=' + filetype + "&ID=" + FillID + "&ROW_ID=" + row_ID//row_ID为填报数据的第一行的ID
     });
    }
 //获取数据
 function GetData() {
     var ID = FillID;
     if (ID != "") {
         objGrid.set("url", strUrl + "?action=getInfo&ID=" + row_ID + "&IsInsert=false");
     }
 }
 //下载方法
 function aa() {
     if (objGrid.getSelectedRow() == null) {
         $.ligerDialog.warn('请选择一条记录进行办理');
         return;
     }
     if (objGrid.getSelectedRow().BUSINESS_ID > 0) {
         $("#hidSave").val(objGrid.getSelectedRow().BUSINESS_ID);//勾选的行ID
         $("#hidType").val(getQueryString("ID")); //row_ID为填报数据的第一行的ID
         $("#btnExcelOut").click();
     }
 }
 //删除
 function deleteData() {
     if (objGrid.getSelectedRow() == null) {
         $.ligerDialog.warn('请选择一条记录进行删除');
         return;
     }
     $.ligerDialog.confirm('确认删除已经选择的信息吗？', function (yes) {
         if (yes == true) {
             var strValue = objGrid.getSelectedRow().BUSINESS_ID;
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
         objGrid.set("url", strUrl + "?action=getInfo&ID=" + getQueryString("ID") + "&IsInsert=false");
     }
 }