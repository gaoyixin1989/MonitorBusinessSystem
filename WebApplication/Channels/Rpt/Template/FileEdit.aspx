<%@ Page Language="C#" AutoEventWireup="True" EnableTheming="false" Theme="" Inherits="n20.Channels_Rpt_Template_FileEdit" Codebehind="FileEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html style="overflow: hidden; margin: 0px;" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <script language="javascript" type="text/javascript" for="WebOffice" event="OnMenuClick(vIndex,vCaption)">
        //打开本地文件
       if (vIndex==1)
       {  
          WebOpenLocal();
       }
       //打印文档
       if (vIndex==2)
       {  
          WebOpenPrint();
       }
       //保存文档
       if (vIndex==3)
       {  
          SaveDocument();
       }
        if (vIndex==4)
       { 
            WebSaveLocal();
       }
       if (vIndex==5)
       {  
          //定义签名
          WebWriteSignature();
          SaveDocumentNotClose();
       }
       //显示痕迹
       if (vIndex==6)
       {  
          ShowRevision(true);
       }
       //隐藏痕迹
       if (vIndex==7)
       {  
          ShowRevision();
       }
       //关闭
       if (vIndex==8)
       {  
          self.close();
       }
    </script>

    <script src="FileEdit_Js/FileEdit_Common.js" type="text/javascript"></script>
    <script src="FileEdit_Js/FileEdit_Functoin.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        //作用：载入iWebOffice
        function Load() {
            //以下属性必须设置，实始化iWebOffice
            webform.WebOffice.WebUrl = "<%=mServerUrl%>";    //WebUrl:系统服务器路径
            webform.WebOffice.RecordID = "<%=mRecordID%>";   //RecordID:本文档记录编号
            webform.WebOffice.Template = "<%=mTemplate%>";   //Template:模板编号
            webform.WebOffice.FileName = "<%=mFileName%>";   //FileName:文档名称
            webform.WebOffice.FileType = "<%=mFileType%>";   //FileType:文档类型
            webform.WebOffice.EditType = "<%=mEditType%>";   //EditType:编辑类型
            webform.WebOffice.UserName = "<%=mUserName%>";   //UserName:操作用户名

            var mIfSign = "<%=mIfSign%>"; //是否签名，如清远需要签名ifSign=1，郑州不需要签名不需要设置该值

            webform.WebOffice.AllowEmpty = false;

            //自定义菜单项
            webform.WebOffice.ShowMenu = "1";
            webform.WebOffice.AppendMenu("1", "打开文件");
            webform.WebOffice.AppendMenu("2", "打印文件");
            //保护文档
            if (document.getElementById("PROTECT").value == "1") {
            }
            else {
                webform.WebOffice.AppendMenu("3", "保存文档");
            }
            webform.WebOffice.AppendMenu("4", "保存到本地");
            //if (mIfSign == "1")//是否签名，如清远需要签名ifSign=1，郑州不需要签名不需要设置该值
                webform.WebOffice.AppendMenu("5", "定义签名");
            webform.WebOffice.AppendMenu("6", "显示痕迹");
            webform.WebOffice.AppendMenu("7", "隐藏痕迹");
            webform.WebOffice.AppendMenu("8", "关闭");

            //禁止菜单
            webform.WebOffice.DisableMenu("宏;选项;帮助;");

            //禁止审阅菜单Word2007
            webform.WebOffice.Office2007Ribbon = 4;
            webform.WebOffice.RibbonUIXML = '<customUI xmlns="http://schemas.microsoft.com/office/2006/01/customui">' +
    '  <ribbon startFromScratch="false">' +       //true：全部屏蔽  false：不屏蔽
    '    <tabs>' +
    '      <tab idMso="TabReviewWord" visible="false">' +   //关闭审阅工具栏
    '      </tab>' +
    '    </tabs>' +
    '  </ribbon>' +
    '</customUI>';

            //打开该文档
            webform.WebOffice.WebOpen();

            //状态信息 	 
            StatusMsg(webform.WebOffice.Status);

            //设置合同ID
            webform.WebOffice.WebSetMsgByName("TASKID", document.getElementById("TASKID").value);
            webform.WebOffice.WebSetMsgByName("TEMPLATE", webform.WebOffice.Template);
            webform.WebOffice.WebSetMsgByName("ReportWf", document.getElementById("ReportWf").value);
            webform.WebOffice.WebSetMsgByName("wei", "aaa");

            if (document.getElementById("TASKID").value.length > 0 && webform.WebOffice.Template.length > 0) {
                //加载标签内容
                LoadBookmarks();

                //加载样品信息表格
                LoadSampleInfo_Table();

                //加载监测参数
                LoadTestAttribute();

                //加载监测项目
                LoadTestItem();

                //加载监测结果
                LoadTestResult();

                //加载【清远烟尘监测结果】 
                LoadQY_SO2_RESULT();

                /////清远
                //加载【清远烟尘监测情况】 
                Load_QY_SO2_TESTINFO();

                //加载测点分布示意图
                LoadSketchMap();
            }

            //保护文档
            if (document.getElementById("PROTECT").value == "1") {
                WebProtect(true);
            }

            //禁止审阅菜单Word2003
            WebToolsVisible("Reviewing", false);

            StatusMsg("报告信息加载完成");
        }

        //作用：退出iWebOffice
        function UnLoad() {
            try {
                if (!webform.WebOffice.WebClose()) {
                    StatusMsg(webform.WebOffice.Status);
                }
                else {
                    StatusMsg("关闭文档...");
                }
            }
            catch (e) {
                alert(e.description);
            }
        }
    </script>
    <title>iWebOffice在线编辑</title>
</head>
<body style="margin: 0px;" onload="Load()" onunload="UnLoad()">
    <form id="webform" runat="server" onsubmit="return SaveDocument();">
        <input type="hidden" name="RecordID" value="<%=mRecordID%>" />
        <input type="hidden" name="Template" value="<%=mTemplate%>" />
        <input type="hidden" name="FileType" value="<%=mFileType%>" />
        <input type="hidden" name="EditType" value="<%=mEditType%>" />
        <input type="hidden" name="HTMLPath" value="<%=mHTMLPath%>" />
        <input type="hidden" id="TASKID" value="<%=mTask%>" />
        <input type="hidden" id="weilin" value="aabb" />
        <input type="hidden" id="PROTECT" value="<%=mProtect %>" />
        <input type="hidden" id="ReportWf" value="<%=mReportWf %>" />
        <input type="hidden" id="AppUser_Position" value="<%=mAppUser_Position %>" />
        <!--未出综合报告前，报告分类别出，临时修改-->
        <input type="hidden" id="ItemTypeID" value="<%=mItemType %>" />
        <div style="height: 100%;">
            <!--调用iWebOffice，注意版本号，可用于升级-->
            <script src="iWebOffice2003.js" type="text/jscript"></script>
        </div>
        <div style="display: none;" id="StatusBar"> 状态栏</div>
    </form>
</body>
</html>
