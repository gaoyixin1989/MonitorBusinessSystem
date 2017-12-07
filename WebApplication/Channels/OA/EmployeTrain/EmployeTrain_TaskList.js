///-------------------------------------------------------------------------------------
///定义变量
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var vEmployeList=null,vEmployeDept=null,vTypeItems=null,vExamTypeItems=null,maingrid=null,maingrid1=null;
var strFileId = "", strtaskID = "";
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


    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Employe/EmployeHander.ashx?action=GetDict&type=dept",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vEmployeDept = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });


    //获取培训分类
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Employe/EmployeHander.ashx?action=GetDict&type=TrainType",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vTypeItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    //获取考核办法
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Employe/EmployeHander.ashx?action=GetDict&type=TrainExamType",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vExamTypeItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });


    //获取员工档案列表
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Employe/EmployeHander.ashx?action=GetEmployeInfor",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vEmployeList = data.Rows;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    maingrid = $("#maingrid").ligerGrid({
        columns: [
                { display: '培训主题', name: 'TRAIN_BT', align: 'left', width: 220 },
                { display: '申请部门', name: 'DEPT_ID', width: 120, minWidth: 60, render: function (item) {
                    if (vEmployeDept != null) {
                        for (var i = 0; i < vEmployeDept.length; i++) {
                            if (vEmployeDept[i].DICT_CODE == item.DEPT_ID) {
                                return vEmployeDept[i].DICT_TEXT;
                            }
                        }
                    }
                    return item.TRAIN_TYPE;
                }
                },
                { display: '组织培训机构', name: 'TRAIN_COMPANY', width: 120, minWidth: 60 },
                { display: '培训人员', name: 'TRAIN_TO', align: 'left', width: 120, minWidth: 60, render: function (item) {
                    var strUserIdArr = item.TRAIN_TO.split(";");
                    strEmployeNames = "";
                    if (vEmployeList != null && strUserIdArr.length > 0) {
                        for (var n = 0; n < strUserIdArr.length; n++) {
                            for (var i = 0; i < vEmployeList.length; i++) {
                                if (vEmployeList[i].ID == strUserIdArr[n]) {
                                    strEmployeNames += vEmployeList[i].EMPLOYE_NAME + ";";
                                }
                            }
                        }
                        return strEmployeNames;
                    }
                    return item.TRAIN_TO;
                }
                },
           { display: '拟培训时间', name: 'TRAIN_DATE', width: 80, minWidth: 60 },
           { display: '培训内容', name: 'TRAIN_INFO', align: 'left', width: 220, minWidth: 60, render: function (item) {
               var strInfor = item.TRAIN_INFO;
               if (strInfor.length > 15) {
                   strInfor = item.TRAIN_INFO.substring(0, 15) + "...";
               }
               return "<a title='详细:" + item.TRAIN_INFO + "'>" + strInfor + "<a/>"
           }
           },
                { display: '流转情况', name: 'FLOW_STATUS', width: 80, minWidth: 60, render: function (item) {
                    if (item.FLOW_STATUS == "9") {
                        return "<a style='color:Red'>已办结(归档)</a>";
                    }
                    return item.FLOW_STATUS;
                }
                },
                { display: '审批情况', name: 'APP_FLOW', width: 120, minWidth: 60 }
                ],
        width: '100%',
        height: '50%',
        pageSizeOptions: [5, 10, 15, 20],
        pageSize: 10,
        url: 'TrainHander.ashx?action=GetTrainViewList&strFlowStatus=9&strTypes=2',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        rownumbers: true,
        checkbox: true,
        toolbar: { items: [
                 { id: 'fileup', text: '总结附件上传', click: upLoadFileGrid, icon: 'fileup' },
                { line: true },
                { id: 'filedown', text: '总结附件下载', click: downLoadFileGrid, icon: 'filedown' }
                ]
        },
        whenRClickToSelect: true,
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            strtaskID = rowdata.ID;
            SeletFile();
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    SeletFile();

    function SeletFile() {
        maingrid1 = $("#maingrid1").ligerGrid({
            columns: [
            { display: '附件名称', name: 'ATTACH_NAME', align: 'left', width: 160, minWidth: 60 },
            { display: '附件类型', name: 'ATTACH_TYPE', width: 140, minWidth: 60, render: function (items) {
                return items.ATTACH_TYPE.toUpperCase();
            }
            },
            { display: '上传人', name: 'UPLOAD_PERSON', width: 100, minWidth: 60 },
             { display: '上传时间', name: 'UPLOAD_DATE', width: 100, minWidth: 60 }
            ],
            width: '100%', height: '100%',
            pageSizeOptions: [5, 10],
            url: "TrainHander.ashx?action=GetTrainFile&strFileType=BunisessTrain&strtaskID=" + strtaskID,
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 5,
            alternatingRow: false,
            checkbox: true,
            rownumbers: true,
            toolbar: { items: [
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
                strFileId = rowindex.ID;
            },
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }

        });
        $("#pageloading").hide();
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll
    }


    // ===================Begin 构造ligerUIGrid 附件上传下载===============
    ///附件上传


    function upLoadFileGrid1() {
        var row = maingrid.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择一条记录!');
            return;
        }
        else {
            CreateTrainFile();
            if (strFileId != "") {
                $.ligerDialog.open({ title: '附件上传', width: 500, height: 270,
                    buttons: [
            {
                text:
                    '上传', onclick: function (item, dialog) {
                        dialog.frame.upLoadFile();
                        maingrid1.loadData();
                    }
            },
            {
                text:
                 '关闭', onclick: function (item, dialog) { GetFileSatausGrid1(item, dialog), dialog.close(); }
            }], url: '../../OA/ATT/AttFileUpload.aspx?filetype=BunisessTrain&id=' + strFileId
                });
            }
        }
    }

    function CreateTrainFile() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "TrainHander.ashx?action=CreateTrainFile&strtaskID=" + strtaskID,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != null) {
                    strFileId = data;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }

    function DelTrainFile() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "TrainHander.ashx?action=DelTrainFile&strFileId=" + strFileId,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != null) {
                }
                else {
                    $.ligerDialog.warn('操作数据失败！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }

    function GetFileSatausGrid1(item, dialog) {
        var fn = dialog.frame.getUpLoadStatus();
        if (fn == "1") {
            maingrid1.loadData();
        }
        else {
            DelTrainFile();
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
            strFileId = row.ATTID;
            if (strFileId == "") {
                $.ligerDialog.warn('当前选择记录没有可供下载的附件!'); return;
            }
            $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
                buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileDownLoad.aspx?strAttID=' + strFileId
            });
        }
    }

    //附件删除操作
    function f_deleteDataGrid1() {
        var row = maingrid1.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择一条记录!');
            return;
        } else {
            strFileId = row.ID;
            $.ligerDialog.confirm("您确定要删除该培训计划的附件信息吗？\r\n", function (result) {
                if (result == true) {
                    $.ajax({
                        cache: false,
                        async: false, //设置是否为异步加载,此处必须
                        type: "POST",
                        url: "TrainHander.ashx?action=DelTrainFile&strFileId=" + strFileId,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (data != "") {
                                maingrid1.loadData();
                                $.ligerDialog.success('数据操作成功！');
                            }
                            else {
                                $.ligerDialog.warn('数据操作失败！');
                            }
                        },
                        error: function (msg) {
                            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
                        }
                    });
                }
            })
        }

    }
    // ===================End 构造ligerUIGrid 附件上传下载================



    // ===================Begin 构造ligerUIGrid 总结附件上传下载===============
    ///附件上传
    function upLoadFileGrid() {
        var row = maingrid.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择一条记录!');
            return;
        }
        else {
            var strId = row.ID;
            if (strId != "") {
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
            }], url: '../../OA/ATT/AttFileUpload.aspx?filetype=BunisessSumTrain&id=' + strId
                });
            }
        }
    }



    function GetFileSatausGrid1(item, dialog) {
        var fn = dialog.frame.getUpLoadStatus();
        if (fn == "1") {
            maingrid.loadData();
        }
        else {
            maingrid.loadData();
        }
    }
    ///附件下载
    function downLoadFileGrid() {
        var row = maingrid.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择一条记录!');
            return;
        }
        else {
            var strId = row.ID;
            if (strId == "") {
                $.ligerDialog.warn('当前选择记录没有可供下载的附件!'); return;
            }
            $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
                buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileDownLoad.aspx?filetype=BunisessSumTrain&id=' + strId
            });
        }
    }
    // ===================End 构造ligerUIGrid 附件上传下载================
})