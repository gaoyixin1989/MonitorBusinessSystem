var strSampleId = "", strSampleCode = "", strSubSampleNum = "", strActionDate = "", strSubSampleNum = 0;
var strSubSampleId="";
var strView = "", editStatus = true;
var objGrid = null,objToolbar=null;
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

//监测点位信息
$(document).ready(function () {
    strSampleId = $.getUrlVar('strSampleId');
    strSampleCode = $.getUrlVar('strSampleCode');
    strView = $.getUrlVar('strView');
    if (!strView) {
        $("#layout1").ligerLayout({ topHeight: 30, leftWidth: "100%", allowLeftCollapse: false, allowRightCollapse: false, height: "98%" });

        $("#txtNum").val("1");
        $("#txtDate").ligerDateEditor({ showTime: true, initValue: TogetDate(new Date()), width: 150, onChangeDate: function (value) {
            strActionDate = $("#txtDate").val();
        }
        });

        objToolbar = { items: [
                { text: '快捷生成', click: quicklyData, icon: 'database_wrench' },
                { line: true },
                { text: '增加', click: createData, icon: 'add' },
                { line: true },
                { text: '修改', click: updateData, icon: 'modify' },
                { line: true },
                { text: '删除', click: deleteData, icon: 'delete' }
                ]
        };
    }
    else {
        $("#layout1").ligerLayout({ topHeight: "1%", leftWidth: "100%", allowLeftCollapse: false, allowRightCollapse: false, height: "98%" });
        $("#txtNum").remove();
        $("#tdNum").html("");
        $("#tdDate").html("");
        $("#txtDate").remove();
        objToolbar = null;
    }


    objGrid = $("#gridItems").ligerGrid({
        columns: [
                    { display: '子样编号', name: 'SUBSAMPLE_NAME', align: 'left', width: 150, minWidth: 60, type: "text" },
                     { display: '采样日期', name: 'ACTIONDATE', type: 'date', width: 120, render: function (items) {
                         if (items.ACTIONDATE != "") {
                             return TogetDate(new Date(Date.parse(items.ACTIONDATE)))
                         }
                     }
                     }
                ],
        dataAction: 'server',
        usePager: true,
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        height: '98%',
        pageSizeOptions: [5, 10, 15, 20],
        url: 'SubSample.aspx?action=GetSubSampleList&strSampleId=' + strSampleId,
        toolbar: objToolbar,
        alternatingRow: false,
        checkbox: true,
        rownumbers: true,
        whenRClickToSelect: true
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
    $("#pageloading").hide();
    function UpdateSubSample(strSubSampleId, strActionDate, strIndex) {
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "SubSample.aspx?action=UpdateSubSample&strSubSampleId=" + strSubSampleId + "&strActionDate=" + strActionDate,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    objGrid.cancelEdit(strIndex);
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax请求数据失败！');
            }
        });
    }
    function InsertSubSample() {
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "SubSample.aspx?action=InsertSubSample&strSampleId=" + strSampleId + "&strSampleCode=" + strSampleCode + "&strActionDate=" + strActionDate + "&strSubSampleNum=" + strSubSampleNum,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data == true) {
                    objGrid.loadData();
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax请求数据失败！');
            }
        });
    }
    function quicklyData() {
        strActionDate = $("#txtDate").val();
        strSubSampleNum = $("#txtNum").val();
        InsertSubSample();
    }
    //增加数据
    function createData() {

        $.ligerDialog.open({ title: '子样增加', top: 10, width: 600, height: 300, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'SubSampleEdit.aspx?strSampleId=' + strSampleId
        });
    }
    //修改数据
    function updateData() {
        var selectedRow = objGrid.getSelectedRow();
        if (selectedRow == null) {
            $.ligerDialog.warn('请选择一条记录'); return;
        }
        else {
            $.ligerDialog.open({ title: '子样编辑', top: 10, width: 600, height: 300, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'SubSampleEdit.aspx?strSubSampleId=' + selectedRow.ID + '&strSampleId=' + strSampleId + '&strSampleCode=' + selectedRow.SUBSAMPLE_NAME + '&strActionDate=' + selectedRow.ACTIONDATE
            });
        }
    }

    //删除数据
    function deleteData() {
        var selectedRow = objGrid.getSelectedRow();
        if (selectedRow == null) {
            $.ligerDialog.warn('请选择一条记录'); return;
        }
        else {
            $.ligerDialog.confirm("确认删除点位信息吗？", function (yes) {
                if (yes == true) {
                    $.ajax({
                        cache: false,
                        type: "POST",
                        url: "SubSample.aspx?action=DeleteData&strSubSampleId=" + selectedRow.ID,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (data == true) {
                                objGrid.loadData();
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
    }
    function f_SaveDate(item, dialog) {
        var fn = dialog.frame.gerRequestStr || dialog.frame.window.gerRequestStr;
        var data = fn();

        $.ajax({
            cache: false,
            type: "POST",
            url: "SubSample.aspx?action=SaveData" + data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data == true) {
                    $.ligerDialog.success('数据保存成功');
                    dialog.close();
                    objGrid.loadData();
                }
                else {
                    $.ligerDialog.warn('数据保存失败');
                }
            }
        });
    }
    function TogetDate(date) {
        var strD = "";
        var thisYear = date.getYear();
        var thisHour = date.getHours();
        if (thisHour < 10) thisHour = "0" + thisHour;
        var thisMinu = date.getMinutes();
        if (thisMinu < 10) thisMinu = "0" + thisMinu;
        thisYear = (thisYear < 1900) ? (1900 + thisYear) : thisYear;
        var thisMonth = date.getMonth() + 1;
        //如果月份长度是一位则前面补0    
        if (thisMonth < 10) thisMonth = "0" + thisMonth;
        var thisDay = date.getDate();
        //如果天的长度是一位则前面补0    
        if (thisDay < 10) thisDay = "0" + thisDay;
        {

            strD = thisYear + "-" + thisMonth + "-" + thisDay + " " + thisHour + ":" + thisMinu;
        }
        return strD;
    }
})