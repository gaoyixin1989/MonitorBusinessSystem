﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="CCFlow.WF.UC.MyFlow" CodeBehind="MyFlow.ascx.cs" %>
<%@ Register Src="Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<%@ Register Src="UCEn.ascx" TagName="UCEn" TagPrefix="uc2" %>
<%@ Register Src="../Comm/UC/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc3" %>
<script src="../Comm/JS/Calendar/WdatePicker.js" type="text/javascript"></script>
<link href="../Comm/JS/Calendar/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
    $(function () {
        SetHegiht();
    });

    //. 保存自定义表单. add 2015-01-22 for GaoLing.
    function SaveSelfFrom() {
        var frm = document.getElementById('SelfForm');
        if (frm == null) {
            alert('系统错误.');
        }
        //执行保存.
        return frm.contentWindow.Save();
    }

    function SendSelfFrom() {
        if (SaveSelfFrom() == false) {
            alert('表单保存失败，不能发送。');
            return false;
        }
        return true;
    }

    var winSelectAccepter = null;
    // 打开选择人接收器.
    function OpenSelectAccepter(flowNo, nodeid, workid, fid) {
        var url = "./WorkOpt/Accepter.aspx?WorkID=" + workid + "&FK_Node=" + nodeid + "&FK_Flow=" + flowNo + "&FID=" + fid + "&type=1";
        if (winSelectAccepter == null)
            winSelectAccepter = window.open(url, winSelectAccepter, 'height=600, width=600,scrollbars=yes');
        else
            winSelectAccepter.focus(); // (0, 0);
        return false;
    }

    function OpenAccepter() {

        var url = '/WF/CCForm/FrmPopVal.aspx?FK_MapExt=' + popNameInXML + '&CtrlVal=' + ctrl.value;
        var v = window.showModalDialog(url, 'opp', 'dialogHeight: 550px; dialogWidth: 650px; dialogTop: 100px; dialogLeft: 150px; center: yes; help: no');
        if (v == null || v == '' || v == 'NaN') {
            return;
        }
        ctrl.value = v;
        return;
        window.sho
    }

    function SetHegiht() {
        var screenHeight = document.documentElement.clientHeight;


        var messageHeight = $('#Message').height();
        var topBarHeight = 40;
        var childHeight = $('#childThread').height();
        var infoHeight = $('#flowInfo').height();

        var allHeight = messageHeight + topBarHeight + childHeight + childHeight + infoHeight;
        try {

            if ("<%=BtnWord %>" == "2")
                allHeight = allHeight + 30;

            var frmHeight = "<%=Height %>";
            if (frmHeight == NaN || frmHeight == "" || frmHeight == null)
                frmHeight = 0;

            if (screenHeight > parseFloat(frmHeight) + allHeight) {
                $("#divCCForm").height(screenHeight - allHeight);

                $("#TDWorkPlace").height(screenHeight - allHeight - 10);

            }
            else {
                $("#divCCForm").height(parseFloat(frmHeight) + allHeight);
                $("#TDWorkPlace").height(parseFloat(frmHeight) + allHeight - 10);
            }
        }
        catch (e)
        { }
    }

    $(window).resize(function () {
        SetHegiht();
    });
    function SysCheckFrm() {
    }
    function Change() {
        var btn = document.getElementById('ContentPlaceHolder1_MyFlowUC1_MyFlow1_ToolBar1_Btn_Save');
        if (btn != null) {
            if (btn.value.valueOf('*') == -1)
                btn.value = btn.value + '*';
        }
    }
    var longCtlID = 'ContentPlaceHolder1_MyFlowUC1_MyFlow1_UCEn1_';
    function KindEditerSync() {
        try {
            if (editor1 != null) {
                editor1.sync();
            }
        }
        catch (err) {
        }
    }
    // ccform 为开发者提供的内置函数. 
    // 获取DDL值 
    function ReqDDL(ddlID) {
        var v = document.getElementById(longCtlID + 'DDL_' + ddlID).value;
        if (v == null) {
            alert('没有找到ID=' + ddlID + '的下拉框控件.');
        }
        return v;
    }
    // 获取TB值
    function ReqTB(tbID) {
        var v = document.getElementById(longCtlID + 'TB_' + tbID).value;
        if (v == null) {
            alert('没有找到ID=' + tbID + '的文本框控件.');
        }
        return v;
    }
    // 获取CheckBox值
    function ReqCB(cbID) {
        var v = document.getElementById(longCtlID + 'CB_' + cbID).value;
        if (v == null) {
            alert('没有找到ID=' + cbID + '的单选控件.');
        }
        return v;
    }
    // 获取附件文件名称,如果附件没有上传就返回null.
    function ReqAthFileName(athID) {
        var v = document.getElementById(athID);
        if (v == null) {
            return null;
        }
        var fileName = v.alt;
        return fileName;
    }

    /// 获取DDL Obj
    function ReqDDLObj(ddlID) {
        var v = document.getElementById(longCtlID + 'DDL_' + ddlID);
        if (v == null) {
            alert('没有找到ID=' + ddlID + '的下拉框控件.');
        }
        return v;
    }
    // 获取TB Obj
    function ReqTBObj(tbID) {
        var v = document.getElementById(longCtlID + 'TB_' + tbID);
        if (v == null) {
            alert('没有找到ID=' + tbID + '的文本框控件.');
        }
        return v;
    }
    // 获取CheckBox Obj值
    function ReqCBObj(cbID) {
        var v = document.getElementById(longCtlID + 'CB_' + cbID);
        if (v == null) {
            alert('没有找到ID=' + cbID + '的单选控件.');
        }
        return v;
    }
    // 设置值.
    function SetCtrlVal(ctrlID, val) {
        document.getElementById(longCtlID + 'TB_' + ctrlID).value = val;
        document.getElementById(longCtlID + 'DDL_' + ctrlID).value = val;
        document.getElementById(longCtlID + 'CB_' + ctrlID).value = val;
    }
    //执行分支流程退回到分合流节点。
    function DoSubFlowReturn(fid, workid, fk_node) {
        var url = 'ReturnWorkSubFlowToFHL.aspx?FID=' + fid + '&WorkID=' + workid + '&FK_Node=' + fk_node;
        var v = WinShowModalDialog(url, 'df');
        window.location.href = window.history.url;
    }
    function To(url) {
        //window.location.href = url;
        window.name = "dialogPage"; window.open(url, "dialogPage")
    }

    // 退回，获取配置的退回信息的字段.
    function ReturnWork(url, field) {
        var urlTemp;
        if (field == '' || field == null) {
            urlTemp = url;
        }
        else {
            // alert(field);
            //  alert(ReqTB(field));
            urlTemp = url + '&Info=' + ReqTB(field);
        }
        window.name = "dialogPage"; window.open(urlTemp, "dialogPage")
    }



    function WinOpen(url, winName) {
        var newWindow = window.open(url, winName, 'width=700,height=400,top=100,left=300,scrollbars=yes,resizable=yes,toolbar=false,location=false,center=yes,center: yes;');
        newWindow.focus();
        return;
    }
    function DoDelSubFlow(fk_flow, workid) {
        if (window.confirm('您确定要终止进程吗？') == false)
            return;
        var url = 'Do.aspx?DoType=DelSubFlow&FK_Flow=' + fk_flow + '&WorkID=' + workid;
        WinShowModalDialog(url, '');
        window.location.href = window.location.href; //aspxPage + '.aspx?WorkID=';
    }
    function Do(warning, url) {
        if (window.confirm(warning) == false)
            return;
        window.location.href = url;
    }
    //设置底部工具栏
    function SetBottomTooBar() {
        var form;
        //窗口的可视高度 
        var windowHeight = document.all ? document.getElementsByTagName("html")[0].offsetHeight : window.innerHeight;
        var pageHeight = Math.max(windowHeight, document.getElementsByTagName("body")[0].scrollHeight);
        form = document.getElementById('divCCForm');

        //        if (form) {
        //            if (pageHeight > 20) pageHeight = pageHeight - 20;
        //            form.style.height = pageHeight + "px";
        //        }
        //设置toolbar
        var toolBar = document.getElementById("bottomToolBar");
        if (toolBar) {
            document.getElementById("bottomToolBar").style.display = "";
        }
    }

    window.onload = function () {
        //  ResizeWindow();
        SetBottomTooBar();
    };

    //然浏览器最大化.
    function ResizeWindow() {
        if (window.screen) {  //判断浏览器是否支持window.screen判断浏览器是否支持screen     
            var myw = screen.availWidth;   //定义一个myw，接受到当前全屏的宽     
            var myh = screen.availHeight;  //定义一个myw，接受到当前全屏的高     
            window.moveTo(0, 0);           //把window放在左上角     
            window.resizeTo(myw, myh);     //把当前窗体的长宽跳转为myw和myh     
        }
    }
</script>
<%--<style type="text/css">
    .Bar
    {
        width: 500px;
        text-align: center;
        float: left;
    }
    .Btn
    {
        border: 0;
        background: #4D77A7;
        color: #FFF;
        font-size: 12px;
        padding: 6px 10px;
        margin: 5px;
    }
    #divCCForm
    {
        position: relative !important;
    }
    .Message
    {
        margin-top: -5px !important;
        margin-bottom: -5px !important;
    }
</style>--%>
<script type="text/javascript" language="javascript" src="/DataUser/PrintTools/LodopFuncs.js"></script>
<script language="javascript" type="text/javascript">
    var LODOP; //声明为全局变量 

    function printFrom() {
        var url = "PrintSample.aspx?FK_Flow=<%=this.FK_Flow%>&FK_Node=<%=this.FK_Node %>&FID=<%=this.FID %>&WorkID=<%=this.WorkID %>&AtPara=";
        LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
        LODOP.PRINT_INIT("打印表单");
        // LODOP.ADD_PRINT_URL(30, 20, 746, "100%", location.href);
        LODOP.SET_PRINT_PAGESIZE(1, 2400, 2970, "A4");
        //LODOP.ADD_PRINT_HTM(20, 0, "100%", "100%", document.getElementById("divCCForm").innerHTML);
        LODOP.ADD_PRINT_URL(0, 0, "100%", "100%", url);
        LODOP.SET_PRINT_STYLEA(0, "HOrient", 3);
        LODOP.SET_PRINT_STYLEA(0, "VOrient", 3);
        //		LODOP.SET_SHOW_MODE("MESSAGE_GETING_URL",""); //该语句隐藏进度条或修改提示信息
        //		LODOP.SET_SHOW_MODE("MESSAGE_PARSING_URL","");//该语句隐藏进度条或修改提示信息
        //  LODOP.PREVIEW();
        LODOP.PREVIEW();
    }    
</script>
<div style="width: 0px; height: 0px">
    <object id="LODOP_OB" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width="0"
        height="0">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0" height="0" pluginspage="/DataUser/PrintTools/install_lodop32.exe"></embed>
    </object>
</div>

<%--by yinchengyi 2015-7-14 改为在tab页面中显示 以有效利用空间--%>
<div style="margin: 0; padding: 0;" id="D">
    <div style="width: 100%;" class="topBar" id="topBar">
        <uc3:ToolBar ID="ToolBar1" runat="server" />
    </div>
    <div style="width: <%=Width %>px;" class="flowInfo" id="flowInfo">
        <uc1:Pub ID="Pub1" runat="server" />
    </div>
    <div style="width: <%=Width %>px;" class="Message" id='Message'>
        <uc1:Pub ID="FlowMsg" runat="server" />
    </div>
    <div style="width: <%=Width %>px;" class="childThread" id='childThread'>
        <uc1:Pub ID="Pub3" runat="server" />
    </div>
    <center>
        <div style="width:100%; text-align: center;">
            <uc2:UCEn ID="UCEn1" runat="server" />
        </div>
    </center>
    <div style="width: <%=Width %>px;" class="pub2Class">
        <uc1:Pub ID="Pub2" runat="server" />
    </div>
    <div id="bottomToolBar" style="<%=Width %>px; text-align: left; display: none;" class="Bar">
        <uc3:ToolBar ID="ToolBar2" runat="server" />
    </div>
</div>
