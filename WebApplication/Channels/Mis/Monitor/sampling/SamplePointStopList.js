//企业缴费信息记录明细
//创建人：胡方扬 
//创建时间:2013-02-01
var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var maingrid = null;

$(document).ready(function () {
    window['g1'] = maingrid = $("#maingrid").ligerGrid({
        columns: [
            { display: '停产点位名称', name: 'POINT_NAME', align: 'left', width: 160, minWidth: 60 },
            { display: '项目名称', name: 'PROJECT_NAME', width: 200, minWidth: 60 },
            { display: '委托单号', name: 'CONTRACT_CODE', width: 180, minWidth: 60 },
            { display: '受检企业', name: 'COMPANY_NAME', width: 180, minWidth: 60 },
            { display: '操作人', name: 'REAL_NAME', width: 100, minWidth: 60 },
            { display: '停产日期', name: 'ACTIONDATE', width: 100, minWidth: 60 }
            ],
        title: '停产点位列表',
        width: '100%', height: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        url: "SamplePointStopList.aspx?action=GetPointStopList",
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 15,
        alternatingRow: false,
        checkbox: false,
        rownumbers: true,
        toolbar: { items: [
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
                ]
        },
        whenRClickToSelect: true,
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }

    });
    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll


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
                     { display: "点位名称", name: "SEA_POINT_NAME", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "操作人", name: "SEA_REAL_NAME", newline: false, type: "text" },
                     { display: "从", name: "SEA_STARTDATE", newline: true, type: "date" },
                     { display: "至", name: "SEA_ENDDATE", newline: false, type: "date" }
                     ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 600, height: 240, top: 90, title: "停产点位查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); } },
                  { text: '返回', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }

        function search() {
            var SEA_STARTDATE = $("#SEA_STARTDATE").val();
            var SEA_ENDDATE = $("#SEA_ENDDATE").val();
            if (SEA_STARTDATE == "" & SEA_ENDDATE != "") {
                $.ligerDialog.warn('请输入开始日期！'); return;
            }
            if (SEA_STARTDATE != "" & SEA_ENDDATE == "") {
                $.ligerDialog.warn('请输入截止日期！'); return;
            }
            var SEA_POINT_NAME = $("#SEA_POINT_NAME").val();
            var  SEA_REAL_NAME = $("#SEA_REAL_NAME").val();
            SEA_COMPANYNAME = encodeURI($("#SEA_TEST_COMPANYNAME").val());
            maingrid.set('url', "SamplePointStopList.aspx?action=GetPointStopList&strRealName=" +encodeURI( SEA_REAL_NAME) + "&strPointName=" +encodeURI( SEA_POINT_NAME) + "&strStartDate=" + SEA_STARTDATE + "&strEndDate=" + SEA_ENDDATE);
            clearSearchDialogValue();
            detailWinSrh.hide();
        }
    }

    function clearSearchDialogValue() {
        $("#SEA_STARTDATE").val("");
        $("#SEA_ENDDATE").val("");
        $("#SEA_POINT_NAME").val("");
        $("#SEA_REAL_NAME").val("");

    }
})