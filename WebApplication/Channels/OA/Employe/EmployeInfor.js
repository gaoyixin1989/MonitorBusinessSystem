/// 站务管理--人员档案管理
/// 创建时间：2013-01-07
/// 创建人：胡方扬
///-------------------------------------------------------------------------------------
///定义变量
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var EmployeItems = null, EmployeStatusItems = null, PostDutyItems = null, EmployeTypeItems = null, EmployeLeveItems = null, PoliticalStatusItems = null, DegreeStatusItems = null, NationItems = null,
SkillGradeItems = null, DeptItems = null, DutyItems = null, SexItems = null, OrangnizationItems = null, ProfessionPostItems = null;
var maingrid = null, maingrid1 = null, maingrid2 = null, maingrid3 = null, maingrid4 = null, maingrid5 = null, maingrid6 = null, objJsonTool1 = null;
var strEmployeID = "", strEmployeQualID = "", strEmployeWorkHistoryID = "", strEmployeWorkResultID = "", strAttID = "", strEmployeExamID = "", strEmployeTrainID = "";
var boolresult = false;
///-------------------------------------------------------------------------------------

///加载主处理函数
$(document).ready(function () {
    var tab;
    //窗口改变时的处理函数
    function f_heightChanged(options) {
        if (tab)
            tab.addHeight(options.diff);
    }

    var topHeight = $(window).height() / 2;
    var gridHeight = $(window).height() / 2;

    $("#layout1").ligerLayout({ topHeight: topHeight, leftWidth: "100%", allowLeftCollapse: false, allowRightCollapse: false, height: "100%", onHeightChanged: f_heightChanged });
    tab = $("#navtab1").ligerTab({ contextmenu: false, onAfterSelectTabItem: function (tabid) {
        var navtab = $("#navtab1").ligerGetTabManager();
        SelectEmployeDetail();
    }
    });
    // ===================Begin 初始化加载下拉列表数据================
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "EmployeHander.ashx?action=GetDict&type=EmployeStatus",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                EmployeStatusItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
    //岗位
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "EmployeHander.ashx?action=GetDict&type=PostDuty",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                PostDutyItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
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
        url: "EmployeHander.ashx?action=GetDict&type=dept",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                DeptItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });





    // ===================End 初始化加载下拉列表数据================

    // ===================Begin 构造ligerUIGrid================
    objJsonTool1 = { items: [
                { id: 'add', text: '增加', click: createData, icon: 'add' },
                { line: true },
                { id: 'modify', text: '修改', click: updateData, icon: 'modify' },
                { line: true },
                { id: 'del', text: '删除', click: f_deleteData, icon: 'delete' },
                { line: true },
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' },
                { line: true },
                { id: 'showEx', text: '导出', click: showExcelData, icon: 'modify' }
                ]
    };
    window['g1'] = maingrid = $("#divEmployeInfo").ligerGrid({
        columns: [
            { display: '员工编号', name: 'EMPLOYE_CODE', align: 'left', width: 160, minWidth: 60 },
            { display: '员工姓名', name: 'EMPLOYE_NAME', width: 100, minWidth: 60 },
            { display: '所在部门', name: 'DEPART', width: 100, minWidth: 60, render: function (items) {
                for (var i = 0; i < DeptItems.length; i++) {
                    if (DeptItems[i].DICT_CODE == items.DEPART) {
                        return DeptItems[i].DICT_TEXT;
                    }
                }
                return items.DEPART;
            }
            },
            { display: '所在岗位', name: 'POSITION', width: 100, minWidth: 60, render: function (items) {
                for (var i = 0; i < PostDutyItems.length; i++) {
                    if (PostDutyItems[i].DICT_CODE == items.POSITION) {
                        return PostDutyItems[i].DICT_TEXT;
                    }
                }
                return items.POSITION;
            }
            },
            { display: '在职状态', name: 'POST_STATUS', width: 100, minWidth: 60, render: function (items) {
                for (var i = 0; i < EmployeStatusItems.length; i++) {
                    if (EmployeStatusItems[i].DICT_CODE == items.POST_STATUS) {
                        return EmployeStatusItems[i].DICT_TEXT;
                    }
                }
                return items.POST_STATUS;
            }
            }
            ],
        //        title: '人员档案列表',
        width: '100%', height: gridHeight,
        pageSizeOptions: [5, 10, 20],
        url: "EmployeHander.ashx?action=GetEmployeInfor",
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        rownumbers: true,
        toolbar: objJsonTool1,
        whenRClickToSelect: true,
        onCheckRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            strEmployeID = rowindex.ID;
            SelectEmployeDetail();
        },
        onAfterShowData: AfterShowData,
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }

    });
    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll
    
    
    function AfterShowData(data) {
        if (data.Total != "0") {
            maingrid.select(data.Rows[0]);
            strEmployeID = data.Rows[0].ID;
            SelectEmployeDetail();
        }
    }
    //增加数据
    function createData() {
        strEmployeID = "";
        $.ligerDialog.open({ title: '员工档案信息增加', top: 0, width: 700, height: 550, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'EmployeInforEdit.aspx'
        });
    }

    function showExcelData() {
        /*
        $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "EmployeInfor.aspx?action=ShowExcelData",
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
        if (data != "") {
        maingrid.loadData();
        SelectEmployeDetail();
        $.ligerDialog.success('数据操作成功！'); return;
        } else {
        $.ligerDialog.warn('数据操作失败！'); return;
        }
        },
        error: function () {
        $.ligerDialog.warn('Ajax加载数据失败！');
        }
        });
        */
        $("#btnImport").click();
    }

    //修改数据
    function updateData() {
        var row = maingrid.getSelectedRow();

        if (row == null) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        else {
            strEmployeID = row.ID;
            $.ligerDialog.open({ title: '员工档案信息修改', top: 0, width: 700, height: 550, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'EmployeInforEdit.aspx?strEmployeID=' + strEmployeID
            });
        }
    }
    //删除员工档案事件
    function f_deleteData() {
        var row = maingrid.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        else {
            strEmployeID = row.ID
            $.ligerDialog.confirm('您确定要删除该员工档案信息吗？', function (yes) {
                if (yes) {
                    DelEmployeData();
                }
                else
                { return; }
            })
        }
    }
    //删除员工档案
    function DelEmployeData() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "EmployeHander.ashx?action=DelEmployeData&strEmployeID=" + strEmployeID,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    maingrid.loadData();
                    SelectEmployeDetail();
                    $.ligerDialog.success('数据操作成功！'); return;
                } else {
                    $.ligerDialog.warn('数据操作失败！'); return;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });

    }



    //保存数据
    function f_SaveDate(item, dialog) {
        var fn = dialog.frame.GetIsExiststr || dialog.frame.window.GetIsExiststr;
        var strRequestdata = fn();
        if (strRequestdata != "") {
            f_booExistDate(strRequestdata);
        }
        if (boolresult) {
            var fnSave = dialog.frame.GetBaseInfoStr || dialog.frame.window.GetBaseInfoStr;
            var strReques = fnSave();
            SaveEmployeInfor(strReques);
            dialog.close();
        }
    }


    //判断是否存在该点位信息
    function f_booExistDate(strRequestParme) {
        boolresult = false;
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "EmployeHander.ashx?action=IsExistEmployeInfor" + strRequestParme,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    $.ligerDialog.warn('已存在相同编号的员工！');
                    boolresult = false;
                }
                else {
                    boolresult = true;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });
    }

    //保存不存在的数据

    function SaveEmployeInfor(strReques) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "EmployeHander.ashx?action=SaveEmployeInfor" + strReques + "",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    $.ligerDialog.success('数据保存成功！');
                    maingrid.loadData();
                    return;
                }
                else {
                    $.ligerDialog.warn('数据保存失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });
    }
    //===================Begin 员工档案检索=============================
    //设置grid 的弹出查询对话框
    var detailWinSrh = null;
    function showDetailSrh() {
        if (detailWinSrh) {
            detailWinSrh.show();
        }
        else {
            //创建表单结构

            var mainform = $("#SrhForm");
            mainform.ligerForm({
                inputWidth: 170, labelWidth: 90, space: 40,
                fields: [
                     { display: "员工编号", name: "SEA_EMPLOYE_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "员工姓名", name: "SEA_EMPLOYE_NAME", newline: false, type: "text" },
                     { display: "所在部门", name: "SEA_DEPT", newline: true, type: "select", comboboxName: "SEA_DEPT_BOX", options: { valueFieldID: "SEA_DEPT_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: DeptItems, render: function (item) {
                         for (var i = 0; i < DeptItems.length; i++) {
                             if (DeptItems[i]['DICT_CODE'] == item.DEPART)
                                 return DeptItems[i]['DICT_TEXT'];
                         }
                         return item.DEPART;
                     }
                     }
                     },
                    { display: "所在岗位", name: "SEA_POSITION", newline: false, type: "select", comboboxName: "SEA_POSITION_BOX", options: { valueFieldID: "SEA_POSITION_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: PostDutyItems }, render: function (item) {
                        for (var i = 0; i < PostDutyItems.length; i++) {
                            if (PostDutyItems[i]['DICT_CODE'] == item.POSITION)
                                return PostDutyItems[i]['DICT_TEXT'];
                        }
                        return item.POSITION;
                    }
                    },
                { display: "在职状态", name: "SEA_POST_STATUS", newline: true, type: "select", comboboxName: "SEA_POST_STATUS_BOX", options: { valueFieldID: "SEA_POST_STATUS_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: EmployeStatusItems }, render: function (item) {
                    for (var i = 0; i < EmployeStatusItems.length; i++) {
                        if (EmployeStatusItems[i]['DICT_CODE'] == item.POST_STATUS)
                            return EmployeStatusItems[i]['DICT_TEXT'];
                    }
                    return item.POST_STATUS;
                }
                }
                    ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 660, height: 200, top: 90, title: "员工档案查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '返回', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }

        function search() {
            var SEA_EMPLOYE_CODE = encodeURI($("#SEA_EMPLOYE_CODE").val());
            var SEA_EMPLOYE_NAME = encodeURI($("#SEA_EMPLOYE_NAME").val());
            var SEA_DEPT_OP = $("#SEA_DEPT_OP").val();
            var SEA_POSITION_OP = $("#SEA_POSITION_OP").val();
            var SEA_POST_STATUS_OP = $("#SEA_POST_STATUS_OP").val();

            $("#hdSrh").val(SEA_EMPLOYE_CODE + "," + SEA_EMPLOYE_NAME + "," + SEA_DEPT_OP + "," + SEA_POSITION_OP + "," + SEA_POST_STATUS_OP);

            maingrid.set('url', "EmployeHander.ashx?action=GetEmployeInfor&srhEmployeCode=" + SEA_EMPLOYE_CODE + "&srhEmployeName=" + SEA_EMPLOYE_NAME + "&srhDepart=" + SEA_DEPT_OP + "&srhPostion=" + SEA_POSITION_OP + "&srhPostStatus=" + SEA_POST_STATUS_OP);
        }
    }

    function clearSearchDialogValue() {
        $("#SEA_EMPLOYE_CODE").val("");
        $("#SEA_EMPLOYE_NAME").val("");
        $("#SEA_DEPT_BOX").ligerGetComboBoxManager().setValue("");
        $("#SEA_POSITION_BOX").ligerGetComboBoxManager().setValue("");
        $("#SEA_POST_STATUS_BOX").ligerGetComboBoxManager().setValue("");
    }

    //===================End 员工档案检索==========================
    // ===================Begin 构造ligerUIGrid 员工证书明细================
    function SelectEmployeDetail() {
        CreateGrid1();
        CreateGrid2();
        CreateGrid3();
        CreateGrid4();
        CreateGrid5();
        CreateGrid6();
    }

    //#region 员工证书
    function CreateGrid1() {
        window['grid1'] = maingrid1 = $("#maingrid1").ligerGrid({
            columns: [
            { display: '证书名称', name: 'CERTITICATENAME', align: 'left', width: 160, minWidth: 60 },
            { display: '证书编号', name: 'CERTITICATECODE', width: 140, minWidth: 60 },
            { display: '发证时间', name: 'ISSUINDATE', width: 100, minWidth: 60 },
            { display: '有效期限', name: 'ACTIVEDATE', width: 100, minWidth: 60 },
            { display: '发证单位', name: 'ISSUINGAUTHO', width: 200, minWidth: 60 },
            { display: '附件信息', name: 'ATTFILE', width: 200, minWidth: 60 },
             { display: '上传时间', name: 'UPLOAD_DATE', width: 100, minWidth: 60 }
            ],
            width: '100%', height: '100%',
            pageSizeOptions: [5, 10],
            url: "EmployeHander.ashx?action=GetEmployeQualDetail&strFileType=EmployeQual&strEmployeID=" + strEmployeID,
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 5,
            alternatingRow: false,
            checkbox: true,
            rownumbers: true,
            toolbar: { items: [
                { id: 'add', text: '增加', click: createDataGrid1, icon: 'add' },
                { line: true },
                { id: 'modify', text: '修改', click: updateDataGrid1, icon: 'modify' },
                { line: true },
                 { id: 'fileup', text: '附件上传', click: upLoadFileGrid1, icon: 'fileup' },
                { line: true },
                { id: 'filedown', text: '附件下载', click: downLoadFileGrid1, icon: 'filedown' },
                { line: true },
                { id: 'del', text: '删除', click: f_deleteDataGrid1, icon: 'delete' }
                ]
            },
            whenRClickToSelect: true,
            onCheckRow: function (data, rowindex, rowobj) {
                for (var rowid in this.records)
                    this.unselect(rowid);
                this.select(rowindex);
                strEmployeQualID = rowindex.ID;
            },
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }

        });
        $("#pageloading").hide();
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll
    }

    function createDataGrid1() {
        strEmployeQualID = "";
        $.ligerDialog.open({ title: '证书信息增加', top: 0, width: 700, height: 300, buttons:
        [{ text: '确定', onclick: f_SaveDateGrid1 },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'EmployeQualEdit.aspx?strEmployeID=' + strEmployeID
        });
    }
    function updateDataGrid1() {
        var row = maingrid1.getSelectedRow();

        if (row == null) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        else {
            strEmployeQualID = row.ID;
            $.ligerDialog.open({ title: '证书信息修改', top: 0, width: 700, height: 300, buttons:
        [{ text: '确定', onclick: f_SaveDateGrid1 },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'EmployeQualEdit.aspx?strEmployeID=' + strEmployeID + '&strEmployeQualID=' + strEmployeQualID
            });
        }
    }
    function f_deleteDataGrid1() {

        var row = maingrid1.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        else {
            strEmployeQualID = row.ID
            $.ligerDialog.confirm('您确定要删除该员工档案证书信息吗？', function (yes) {
                if (yes) {
                    DelEmployeQualData();
                }
                else
                { return; }
            })
        }
    }

    //删除员工档案证书
    function DelEmployeQualData() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "EmployeHander.ashx?action=DelEmployeQualData&strEmployeQualID=" + strEmployeQualID,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    $.ligerDialog.success('数据操作成功！');
                    maingrid1.loadData();
                    return;
                } else {
                    $.ligerDialog.warn('数据操作失败！'); return;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });
    }
    //保存数据
    function f_SaveDateGrid1(item, dialog) {

        var fnSave = dialog.frame.GetBaseInfoStr || dialog.frame.window.GetBaseInfoStr;
        var strReques = fnSave();
        if (strReques != "") {
            SaveEmployeQualDetail(strReques);
            dialog.close();
        }
    }

    function SaveEmployeQualDetail(strReques) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "EmployeHander.ashx?action=SaveEmployeQualDetail" + strReques + "",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    $.ligerDialog.success('数据保存成功！');
                    maingrid1.loadData();
                    return;
                }
                else {
                    $.ligerDialog.warn('数据保存失败！'); return;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！'); return
            }
        });
    }

    //#endregion
    // ===================End 构造ligerUIGrid 员工证书明细================



    // ===================Begin 构造ligerUIGrid 员工工作经历明细================
    //#region 员工工作经历
    function CreateGrid2() {

        window['grid2'] = maingrid2 = $("#maingrid2").ligerGrid({
            columns: [
            { display: '所在单位', name: 'WORKCOMPANY', align: 'left', width: 160, minWidth: 60 },
            { display: '所在岗位', name: 'POSITION', width: 140, minWidth: 60 },
            { display: '开始时间', name: 'WORKBEGINDATE', width: 100, minWidth: 60 },
            { display: '截止时间', name: 'WORKENDDATE', width: 100, minWidth: 60 }
            ],
            width: '100%', height: '100%',
            pageSizeOptions: [5, 10],
            url: "EmployeHander.ashx?action=GetEmployeWorkHistoryDetail&strEmployeID=" + strEmployeID,
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 5,
            alternatingRow: false,
            checkbox: true,
            rownumbers: true,
            toolbar: { items: [
                { id: 'add', text: '增加', click: createDataGrid2, icon: 'add' },
                { line: true },
                { id: 'modify', text: '修改', click: updateDataGrid2, icon: 'modify' },
                { line: true },
                { id: 'del', text: '删除', click: f_deleteDataGrid2, icon: 'delete' }
                ]
            },
            whenRClickToSelect: true,
            onCheckRow: function (data, rowindex, rowobj) {
                for (var rowid in this.records)
                    this.unselect(rowid);
                this.select(rowindex);
                strEmployeWorkHistoryID = rowindex.ID;
            },
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }

        });
        $("#pageloading").hide();
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll
    }

    //新增工作经历信息
    function createDataGrid2() {
        strEmployeWorkHistoryID = "";
        $.ligerDialog.open({ title: '工作经历增加', top: 0, width: 700, height: 300, buttons:
        [{ text: '确定', onclick: f_SaveDateGrid2 },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'EmployeWorkHistoryEdit.aspx?strEmployeID=' + strEmployeID
        });
    }
    //更新工作经历信息
    function updateDataGrid2() {
        var row = maingrid2.getSelectedRow();

        if (row == null) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        else {
            strEmployeWorkHistoryID = row.ID;
            $.ligerDialog.open({ title: '工作经历修改', top: 0, width: 700, height: 300, buttons:
        [{ text: '确定', onclick: f_SaveDateGrid2 },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'EmployeWorkHistoryEdit.aspx?strEmployeID=' + strEmployeID + '&strEmployeWorkHistoryID=' + strEmployeWorkHistoryID
            });
        }
    }
    //删除工作经历信息
    function f_deleteDataGrid2() {

        var row = maingrid2.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        else {
            strEmployeWorkHistoryID = row.ID;
            $.ligerDialog.confirm('您确定要删除该员工档案工作履历信息吗？', function (yes) {
                if (yes) {
                    DelEmployeWorkHistoryData();
                }
                else
                { return; }
            })
        }
    }

    //删除工作经历事件
    function DelEmployeWorkHistoryData() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "EmployeHander.ashx?action=DelEmployeWorkHistoryData&strEmployeWorkHistoryID=" + strEmployeWorkHistoryID,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    maingrid2.loadData();
                    $.ligerDialog.success('数据操作成功！');
                    return;
                } else {
                    $.ligerDialog.warn('数据操作失败！'); return;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });
    }
    //保存数据
    function f_SaveDateGrid2(item, dialog) {

        var fnSave = dialog.frame.GetBaseInfoStr || dialog.frame.window.GetBaseInfoStr;
        var strReques = fnSave();
        if (strReques != "") {
            SaveEmployeWorkHistoryDetail(strReques);
            dialog.close();
        }
    }


    function SaveEmployeWorkHistoryDetail(strReques) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "EmployeHander.ashx?action=SaveEmployeWorkHistoryDetail" + strReques + "",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    $.ligerDialog.success('数据保存成功！');
                    maingrid2.loadData();
                    return;
                }
                else {
                    $.ligerDialog.warn('数据保存失败！'); return;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！'); return
            }
        });
    }
    //#endregion
    // ===================End 构造ligerUIGrid 员工工作经历明细================


    // ===================Begin 构造ligerUIGrid 员工业绩成果明细================
    //#region 员工业绩成果
    function CreateGrid3() {

        window['grid3'] = maingrid3 = $("#maingrid3").ligerGrid({
            columns: [
            { display: '业绩成果', name: 'WORKRESULT', align: 'left', width: 340, minWidth: 60 },
            { display: '附件信息', name: 'ATTFILE', width: 200, minWidth: 60 },
             { display: '上传时间', name: 'UPLOAD_DATE', width: 100, minWidth: 60 }
            ],
            width: '100%', height: '100%',
            pageSizeOptions: [5, 10],
            url: "EmployeHander.ashx?action=GetEmployeWorkResultDetail&strFileType=EmployeWorkResult&strEmployeID=" + strEmployeID + "&strEmployeWorkResultType=1",
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 5,
            alternatingRow: false,
            checkbox: true,
            rownumbers: true,
            toolbar: { items: [
                { id: 'add', text: '增加', click: createDataGrid3, icon: 'add' },
                { line: true },
                { id: 'modify', text: '修改', click: updateDataGrid3, icon: 'modify' },
                { line: true },
                { id: 'fileup', text: '附件上传', click: upLoadFileGrid3, icon: 'fileup' },
                { line: true },
                { id: 'filedown', text: '附件下载', click: downLoadFileGrid3, icon: 'filedown' },
                { line: true },
                { id: 'del', text: '删除', click: f_deleteDataGrid3, icon: 'delete' }
                ]
            },
            whenRClickToSelect: true,
            onCheckRow: function (data, rowindex, rowobj) {
                for (var rowid in this.records)
                    this.unselect(rowid);
                this.select(rowindex);
                strEmployeWorkResultID = rowindex.ID;
            },
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }

        });
        $("#pageloading").hide();
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll
    }

    //新增工作经历信息
    function createDataGrid3() {
        strEmployeWorkResultID = "";
        $.ligerDialog.open({ title: '业绩成果增加', top: 0, width: 700, height: 300, buttons:
        [{ text: '确定', onclick: f_SaveDateGrid3 },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'EmployeWorkResultEidt.aspx?strEmployeID=' + strEmployeID + '&strEmployeWorkResultType=1'
        });
    }
    //更新工作经历信息
    function updateDataGrid3() {
        var row = maingrid3.getSelectedRow();

        if (row == null) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        else {
            strEmployeWorkResultID = row.ID;
            $.ligerDialog.open({ title: '业绩成果修改', top: 0, width: 700, height: 300, buttons:
        [{ text: '确定', onclick: f_SaveDateGrid3 },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'EmployeWorkResultEidt.aspx?strEmployeID=' + strEmployeID + '&strEmployeWorkResultID=' + strEmployeWorkResultID + '&strEmployeWorkResultType=1'
            });
        }
    }
    //删除工作经历信息
    function f_deleteDataGrid3() {

        var row = maingrid3.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        else {
            strEmployeWorkResultID = row.ID;
            $.ligerDialog.confirm('您确定要删除该员工档案业绩成果信息吗？', function (yes) {
                if (yes) {
                    DelEmployeWorkResultData();
                }
                else
                { return; }
            })
        }
    }

    //删除业绩成果事件
    function DelEmployeWorkResultData() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "EmployeHander.ashx?action=DelEmployeWorkResultData&strEmployeWorkResultID=" + strEmployeWorkResultID,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    $.ligerDialog.success('数据操作成功！');
                    maingrid3.loadData();
                    return;
                } else {
                    $.ligerDialog.warn('数据操作失败！'); return;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });
    }
    //保存数据
    function f_SaveDateGrid3(item, dialog) {

        var fnSave = dialog.frame.GetBaseInfoStr || dialog.frame.window.GetBaseInfoStr;
        var strReques = fnSave();
        if (strReques != "") {
            SaveEmployeWorkResultDetail(strReques);
            dialog.close();
        }
    }


    function SaveEmployeWorkResultDetail(strReques) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "EmployeHander.ashx?action=SaveEmployeWorkResultDetail" + strReques + "",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    $.ligerDialog.success('数据保存成功！');
                    maingrid3.loadData();
                    return;
                }
                else {
                    $.ligerDialog.warn('数据保存失败！'); return;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！'); return
            }
        });
    }

    //#endregion
    // ===================End 构造ligerUIGrid 员工业绩成果明细================



    // ===================Begin 构造ligerUIGrid 员工工作事故明细================
    //#region 工作事故
    function CreateGrid4() {

        window['grid4'] = maingrid4 = $("#maingrid4").ligerGrid({
            columns: [
            { display: '发生时间', name: 'ACCIDENTHAPPENDATE', align: 'left', width: 100, minWidth: 60 },
            { display: '工作事故', name: 'ACCIDENTS', align: 'left', width: 340, minWidth: 60 }
            ],
            width: '100%', height: '100%',
            pageSizeOptions: [5, 10],
            url: "EmployeHander.ashx?action=GetEmployeWorkResultDetail&strEmployeID=" + strEmployeID + "&strEmployeWorkResultType=2",
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 5,
            alternatingRow: false,
            checkbox: true,
            rownumbers: true,
            toolbar: { items: [
                { id: 'add', text: '增加', click: createDataGrid4, icon: 'add' },
                { line: true },
                { id: 'modify', text: '修改', click: updateDataGrid4, icon: 'modify' },
                { line: true },
                { id: 'del', text: '删除', click: f_deleteDataGrid4, icon: 'delete' }
                ]
            },
            whenRClickToSelect: true,
            onCheckRow: function (data, rowindex, rowobj) {
                for (var rowid in this.records)
                    this.unselect(rowid);
                this.select(rowindex);
                strEmployeWorkResultID = rowindex.ID;
            },
            rownumber: true,
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }

        });
        $("#pageloading").hide();
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll
    }

    //新增工作经历信息
    function createDataGrid4() {
        strEmployeWorkResultID = "";
        $.ligerDialog.open({ title: '工作事故增加', top: 0, width: 700, height: 300, buttons:
        [{ text: '确定', onclick: f_SaveDateGrid4 },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'EmployeWorkResultfaultEidt.aspx?strEmployeID=' + strEmployeID + '&strEmployeWorkResultType=1'
        });
    }
    //更新工作经历信息
    function updateDataGrid4() {
        var row = maingrid4.getSelectedRow();

        if (row == null) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        else {
            strEmployeWorkResultID = row.ID;
            $.ligerDialog.open({ title: '工作事故修改', top: 0, width: 700, height: 300, buttons:
        [{ text: '确定', onclick: f_SaveDateGrid4 },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'EmployeWorkResultfaultEidt.aspx?strEmployeID=' + strEmployeID + '&strEmployeWorkResultID=' + strEmployeWorkResultID + '&strEmployeWorkResultType=2'
            });
        }
    }
    //删除工作经历信息
    function f_deleteDataGrid4() {

        var row = maingrid4.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        else {
            strEmployeWorkResultID = row.ID;
            $.ligerDialog.confirm('您确定要删除该员工档案工作事故信息吗？', function (yes) {
                if (yes) {
                    DelEmployeWorkResultfaultData();
                }
                else
                { return; }
            })
        }
    }

    //删除业绩成果事件
    function DelEmployeWorkResultfaultData() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "EmployeHander.ashx?action=DelEmployeWorkResultData&strEmployeWorkResultID=" + strEmployeWorkResultID,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    $.ligerDialog.success('数据操作成功！');
                    //                    createDataGrid4();
                    maingrid4.loadData();
                    return;
                } else {
                    $.ligerDialog.warn('数据操作失败！'); return;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });
    }
    //保存数据
    function f_SaveDateGrid4(item, dialog) {

        var fnSave = dialog.frame.GetBaseInfoStr || dialog.frame.window.GetBaseInfoStr;
        var strReques = fnSave();
        if (strReques != "") {
            SaveEmployeWorkResultfaultDetail(strReques);
            dialog.close();
        }
    }


    function SaveEmployeWorkResultfaultDetail(strReques) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "EmployeHander.ashx?action=SaveEmployeWorkResultDetail" + strReques + "",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    $.ligerDialog.success('数据保存成功！');
                    maingrid4.loadData();
                    return;
                }
                else {
                    $.ligerDialog.warn('数据保存失败！'); return;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！'); return
            }
        });
    }
    //#endregion
    // ===================End 构造ligerUIGrid 员工工作事故明细================


    // ===================Begin 构造ligerUIGrid 员工考核历史明细================
    //#region 考核历史
    function CreateGrid5() {

        window['grid5'] = maingrid5 = $("#maingrid5").ligerGrid({
            columns: [
            { display: '考核年度', name: 'EX_YEAR', align: 'left', width: 100, minWidth: 60 },
            { display: '考核评价', name: 'EX_INFO', align: 'left', width: 100, minWidth: 60 },
            { display: '附件信息', name: 'ATTFILE', align: 'left', width: 100, minWidth: 60 },
             { display: '上传时间', name: 'UPLOAD_DATE', width: 100, minWidth: 60 }
            ],
            width: '100%', height: '100%',
            pageSizeOptions: [5, 10],
            url: "EmployeHander.ashx?action=GetEmployeExDetail&strFileType=EmployeExam&strEmployeID=" + strEmployeID,
//            url: "EmployeHander.ashx?action=GetAttFiles&strFileType=EmployeExam&strEmployeID=" + strEmployeID,
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 5,
            alternatingRow: false,
            checkbox: true,
            rownumbers: true,
            toolbar: { items: [
                { id: 'add', text: '增加', click: createDataGrid5, icon: 'add' },
                { line: true },
                { id: 'modify', text: '修改', click: updateDataGrid5, icon: 'modify' },
                { line: true },
                { id: 'fileup', text: '附件上传', click: upLoadFileGrid5, icon: 'fileup' },
                { line: true },
                { id: 'filedown', text: '附件下载', click: downLoadFileGrid5, icon: 'filedown' },
                { line: true },
                { id: 'del', text: '删除', click: f_deleteDataGrid5, icon: 'delete' }
                ]
            },
            whenRClickToSelect: true,
            onCheckRow: function (data, rowindex, rowobj) {
                for (var rowid in this.records)
                    this.unselect(rowid);
                this.select(rowindex);
                //                strAttID = rowindex.ID;
                strEmployeExamID = rowindex.ID;
            },
            //rownumber: true,
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }

        });
        $("#pageloading").hide();
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll
    }

    function createDataGrid5() {
        strEmployeExamID = "";
        $.ligerDialog.open({ title: '考核信息增加', top: 0, width: 400, height: 200, buttons:
        [{ text: '确定', onclick: f_SaveDateGrid5 },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'EmployeExamEdit.aspx?strEmployeID=' + strEmployeID
        });
    }
    function updateDataGrid5() {
        var row = maingrid1.getSelectedRow();

        if (row == null) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        else {
            strEmployeExamID = row.ID;
            $.ligerDialog.open({ title: '考核信息修改', top: 0, width: 700, height: 300, buttons:
        [{ text: '确定', onclick: f_SaveDateGrid5 },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'EmployeExamEdit.aspx?strEmployeID=' + strEmployeID + '&strEmployeExamID=' + strEmployeExamID
            });
        }
    }
    //保存数据
    function f_SaveDateGrid5(item, dialog) {

        var fnSave = dialog.frame.GetBaseInfoStr || dialog.frame.window.GetBaseInfoStr;
        var strReques = fnSave();
        if (strReques != "") {
            SaveEmployeExDetail(strReques);
            dialog.close();
        }
    }

    function SaveEmployeExDetail(strReques) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "EmployeHander.ashx?action=SaveEmployeExDetail" + strReques + "",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    $.ligerDialog.success('数据保存成功！');
                    maingrid5.loadData();
                    return;
                }
                else {
                    $.ligerDialog.warn('数据保存失败！'); return;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！'); return
            }
        });
    }

    function SaveExamHistoryData() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "EmployeHander.ashx?action=SaveExamHistoryData&strEmployeID=" + strEmployeID,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    strEmployeExamID = data;
                    upLoadFile();
                }
                else {
                    return;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！'); return
            }
        });
    }

    ///附件上传
    function upLoadFile() {
        var row = maingrid5.getSelectedRow();
        if (row != null) {
            strAttID = row.BUSINESS_ID;
        }
        else {
            strAttID = strEmployeExamID;
        }
        $.ligerDialog.open({ title: '考核附件上传', width: 500, height: 270,
            buttons: [
            {
                text:
                    '上传', onclick: function (item, dialog) {
                        dialog.frame.upLoadFile();
                    }
            },
            {
                text:
                 '关闭', onclick: function (item, dialog) { GetFileSataus(item, dialog), dialog.close(); }
            }], url: '../../OA/ATT/AttFileUpload.aspx?filetype=EmployeExam&id=' + strAttID
        });
    }
    function GetFileSataus(item, dialog) {
        var fn = dialog.frame.getUpLoadStatus();
        if (fn == "1") {
            maingrid5.loadData();
        }
        else {
            DelExamHistoryData();
            maingrid5.loadData();
        }
    }
    ///附件下载
    function downLoadFile() {
        //        if (strEmployeID == "") {
        //            $.ligerDialog.warn('业务ID参数错误');
        //            return;
        //        }
        var row = maingrid5.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择一条记录!');
            return;
        }
        else {
            strAttID = row.ATTID;
            $.ligerDialog.open({ title: '考核附件下载', width: 500, height: 270,
                buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileDownLoad.aspx?strAttID=' + strAttID
            });
        }
    }

    //删除工作经历信息
    function f_deleteDataGrid5() {

        var row = maingrid5.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        else {
            //strAttID = row.ATTID;
            strEmployeExamID = row.ID;
            $.ligerDialog.confirm('您确定要删除该员工档案考核信息吗？', function (yes) {
                if (yes) {
                    DelExamHistoryData();
                }
                else
                { return; }
            })
        }
    }
    //删除没添加附件的考核历史记录信息
    function DelExamHistoryData() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "EmployeHander.ashx?action=DelExamHistoryData&strEmployeExamID=" + strEmployeExamID,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    $.ligerDialog.success('数据操作成功！');
                    maingrid5.loadData();
                    return;
                } else {
                    $.ligerDialog.warn('数据操作失败！'); return;
                }
            }
        });
    }
    //删除考核历史事件
    function DelEmployeAttFiles() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "EmployeHander.ashx?action=DelAttFiles&strAttID=" + strAttID,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    $.ligerDialog.success('数据操作成功！');
                    maingrid5.loadData();
                    return;
                } else {
                    $.ligerDialog.warn('数据操作失败！'); return;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });
    }
    //#endregion
    // ===================End 构造ligerUIGrid 员工考核历史明细================



    // ===================Begin 构造ligerUIGrid 员工培训履历明细================
    //#region 培训履历
    function CreateGrid6() {

        window['grid6'] = maingrid6 = $("#maingrid6").ligerGrid({
            columns: [
            { display: '培训项目', name: 'ATT_NAME', align: 'left', width: 160, minWidth: 60 },
            { display: '培训日期', name: 'ATT_URL', width: 140, minWidth: 60 },
            { display: '培训结果', name: 'TRAIN_RESULT', width: 100, minWidth: 60 },
            { display: '证书号', name: 'BOOK_NUM', width: 100, minWidth: 60 },
            { display: '培训内容', name: 'ATT_INFO', width: 320, minWidth: 60 },
            { display: '附件信息', name: 'ATTFILE', width: 200, minWidth: 60 },
            { display: '上传时间', name: 'UPLOAD_DATE', width: 100, minWidth: 60 }
            ],
            width: '100%', height: '100%',
            pageSizeOptions: [5, 10],
            url: "EmployeHander.ashx?action=GetEmployeTrainHistoryDetail&strFileType=EmployeTrain&strEmployeID=" + strEmployeID,
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 5,
            alternatingRow: false,
            checkbox: true,
            rownumbers: true,
            toolbar: { items: [
                { id: 'add', text: '增加', click: createDataGrid6, icon: 'add' },
                { line: true },
                { id: 'modify', text: '修改', click: updateDataGrid6, icon: 'modify' },
                { line: true },
                { id: 'fileup', text: '附件上传', click: upLoadFileGrid6, icon: 'fileup' },
                { line: true },
                { id: 'filedown', text: '附件下载', click: downLoadFileGrid6, icon: 'filedown' },
                { line: true },
                { id: 'del', text: '删除', click: f_deleteDataGrid6, icon: 'delete' }
                ]
            },
            whenRClickToSelect: true,
            onCheckRow: function (data, rowindex, rowobj) {
                for (var rowid in this.records)
                    this.unselect(rowid);
                this.select(rowindex);
                strEmployeTrainID = rowindex.ID;
            },
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }

        });
        $("#pageloading").hide();
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll
    }

    //新增培训经历信息
    function createDataGrid6() {
        strEmployeTrainID = "";
        $.ligerDialog.open({ title: '培训履历增加', top: 0, width: 700, height: 300, buttons:
        [{ text: '确定', onclick: f_SaveDateGrid6 },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'EmployeTrainHistoryEdit.aspx?strEmployeID=' + strEmployeID
        });
    }
    //更新培训经历信息
    function updateDataGrid6() {
        var row = maingrid6.getSelectedRow();

        if (row == null) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        else {
            strEmployeTrainID = row.ID;
            $.ligerDialog.open({ title: '工作经历修改', top: 0, width: 700, height: 300, buttons:
        [{ text: '确定', onclick: f_SaveDateGrid6 },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'EmployeTrainHistoryEdit.aspx?strEmployeID=' + strEmployeID + '&strEmployeTrainID=' + strEmployeTrainID
            });
        }
    }
    //删除培训经历信息
    function f_deleteDataGrid6() {

        var row = maingrid6.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        else {
            strEmployeTrainID = row.ID;
            $.ligerDialog.confirm('您确定要删除该员工档案工作履历信息吗？', function (yes) {
                if (yes) {
                    DelEmployeTrainHistoryData();
                }
                else
                { return; }
            })
        }
    }

    //删除培训经历事件
    function DelEmployeTrainHistoryData() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "EmployeHander.ashx?action=DelEmployeTrainHistoryData&strEmployeTrainID=" + strEmployeTrainID,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    $.ligerDialog.success('数据操作成功！');
                    maingrid6.loadData();
                    return;
                } else {
                    $.ligerDialog.warn('数据操作失败！'); return;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });
    }
    //保存数据
    function f_SaveDateGrid6(item, dialog) {

        var fnSave = dialog.frame.GetBaseInfoStr || dialog.frame.window.GetBaseInfoStr;
        var strReques = fnSave();
        if (strReques != "") {
            SaveEmployeTrainHistoryDetail(strReques);
            dialog.close();
        }
    }


    function SaveEmployeTrainHistoryDetail(strReques) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "EmployeHander.ashx?action=SaveEmployeTrainHistoryDetail" + strReques + "",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    $.ligerDialog.success('数据保存成功！');
                    maingrid6.loadData();
                    return;
                }
                else {
                    $.ligerDialog.warn('数据保存失败！'); return;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！'); return
            }
        });
    }
    //#endregion
    // ===================End 构造ligerUIGrid 员工培训履历明细================

    // ===================Begin 构造ligerUIGrid 证书附件上传===============
    //#region 证书附件
    ///附件上传
    function upLoadFileGrid1() {
        var row = maingrid1.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择一条记录!');
            return;
        }
        else {
            strAttID = row.ID;
            $.ligerDialog.open({ title: '附件上传', width: 500, height: 270,
                buttons: [
            {
                text:
                    '上传', onclick: function (item, dialog) {
                        dialog.frame.upLoadFile();
                    }
            },
            {
                text:
                 '关闭', onclick: function (item, dialog) { GetFileSatausGrid1(item, dialog), dialog.close(); }
            }], url: '../../OA/ATT/AttFileUpload.aspx?filetype=EmployeQual&id=' + strAttID
            });
        }
    }
    function GetFileSatausGrid1(item, dialog) {
        var fn = dialog.frame.getUpLoadStatus();
        if (fn == "1") {
            maingrid1.loadData();
        }
        else {
            maingrid1.loadData();
        }
    }
    ///附件下载
    function downLoadFileGrid1() {
        var row = maingrid1.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择一条记录!');
            return;
        }
        else {
            strAttID = row.ATTID;
            if (strAttID == "") {
                $.ligerDialog.warn('当前选择记录没有可供下载的附件!'); return;
            }
            $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
                buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileDownLoad.aspx?strAttID=' + strAttID
            });
        }
}
    //#endregion
    // ===================End 构造ligerUIGrid 证书附件上传================

    // ===================Begin 构造ligerUIGrid 业绩成果附件上传===============
    //#region 业绩成果附件
    ///附件上传
    function upLoadFileGrid3() {
        var row = maingrid3.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择一条记录!');
            return;
        }
        else {
            strAttID = row.ID;
            $.ligerDialog.open({ title: '附件上传', width: 500, height: 270,
                buttons: [
            {
                text:
                    '上传', onclick: function (item, dialog) {
                        dialog.frame.upLoadFile();
                    }
            },
            {
                text:
                 '关闭', onclick: function (item, dialog) { GetFileSatausGrid3(item, dialog), dialog.close(); }
            }], url: '../../OA/ATT/AttFileUpload.aspx?filetype=EmployeWorkResult&id=' + strAttID
            });
        }
    }
    function GetFileSatausGrid3(item, dialog) {
        var fn = dialog.frame.getUpLoadStatus();
        if (fn == "1") {
            maingrid3.loadData();
        }
        else {
            maingrid3.loadData();
        }
    }
    ///附件下载
    function downLoadFileGrid3() {
        var row = maingrid3.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择一条记录!');
            return;
        }
        else {
            strAttID = row.ATTID;
            if (strAttID == "") {
                $.ligerDialog.warn('当前选择记录没有可供下载的附件!'); return;
            }
            $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
                buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileDownLoad.aspx?strAttID=' + strAttID
            });
        }
}
    //#endregion
    // ===================End 构造ligerUIGrid 业绩成果附件上传================

    // ===================Begin 构造ligerUIGrid 考核附件上传===============
    //#region 考核附件
    ///附件上传
    function upLoadFileGrid5() {
        var row = maingrid5.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择一条记录!');
            return;
        }
        else {
            strAttID = row.ID;
            $.ligerDialog.open({ title: '附件上传', width: 500, height: 270,
                buttons: [
                {
                    text:
                        '上传', onclick: function (item, dialog) {
                            dialog.frame.upLoadFile();
                        }
                },
                {
                    text:
                     '关闭', onclick: function (item, dialog) { GetFileSatausGrid5(item, dialog), dialog.close(); }
                }], url: '../../OA/ATT/AttFileUpload.aspx?filetype=EmployeExam&id=' + strAttID
            });
        }
    }
    function GetFileSatausGrid5(item, dialog) {
        var fn = dialog.frame.getUpLoadStatus();
        if (fn == "1") {
            maingrid5.loadData();
        }
        else {
            maingrid5.loadData();
        }
    }
    ///附件下载
    function downLoadFileGrid5() {
        var row = maingrid5.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择一条记录!');
            return;
        }
        else {
            strAttID = row.ATTID;
            if (strAttID == "") {
                $.ligerDialog.warn('当前选择记录没有可供下载的附件!'); return;
            }
            $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
                buttons: [
                {
                    text:
                     '关闭', onclick: function (item, dialog) { dialog.close(); }
                }], url: '../../OA/ATT/AttFileDownLoad.aspx?strAttID=' + strAttID
            });
        }
    }
    //#endregion
    // ===================End 构造ligerUIGrid 考核附件上传================

    // ===================Begin 构造ligerUIGrid 培训履历附件上传===============
    //#region 培训履历附件
    ///附件上传
    function upLoadFileGrid6() {
        var row = maingrid6.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择一条记录!');
            return;
        }
        else {
            strAttID = row.ID;
            $.ligerDialog.open({ title: '附件上传', width: 500, height: 270,
                buttons: [
            {
                text:
                    '上传', onclick: function (item, dialog) {
                        dialog.frame.upLoadFile();
                    }
            },
            {
                text:
                 '关闭', onclick: function (item, dialog) { GetFileSatausGrid6(item, dialog), dialog.close(); }
            }], url: '../../OA/ATT/AttFileUpload.aspx?filetype=EmployeTrain&id=' + strAttID
            });
        }
    }
    function GetFileSatausGrid6(item, dialog) {
        var fn = dialog.frame.getUpLoadStatus();
        if (fn == "1") {
            maingrid6.loadData();
        }
        else {
            maingrid6.loadData();
        }
    }
    ///附件下载
    function downLoadFileGrid6() {
        var row = maingrid6.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择一条记录!');
            return;
        }
        else {
            strAttID = row.ATTID;
            if (strAttID == "") {
                $.ligerDialog.warn('当前选择记录没有可供下载的附件!'); return;
            }
            $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
                buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileDownLoad.aspx?strAttID=' + strAttID
            });
        }
}
    //#endregion
    // ===================End 构造ligerUIGrid 培训履历附件上传================
})