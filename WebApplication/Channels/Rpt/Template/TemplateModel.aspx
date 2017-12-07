<%@ Page Language="C#" AutoEventWireup="True" EnableTheming="false" Theme=""
    Inherits="Channels_Rpt_Template_TemplateModel" Codebehind="TemplateModel.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html style="overflow: hidden;" xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <script language="javascript" type="text/javascript" for="WebOffice" event="OnMenuClick(vIndex,vCaption)">
   if (vIndex==1)
   {  
      //打开本地文件
      WebOpenLocal();
   }
   if (vIndex==2)
   {  
      //打印文档
      WebOpenPrint();
   }
   if (vIndex==3)
   {  
      //定义标签
      WebOpenBookMarks();
   }
   if (vIndex==4)
   {  
      //保存模板
      SaveDocument();
   }
   if (vIndex==5)
   {  
      //关闭
      self.close();
   }
    </script>
    <script language="javascript" type="text/javascript">

        //作用：显示操作状态
        function StatusMsg(mString) {
            window.status = mString;
        }

        //作用：载入iWebOffice
        function Load() {
            try {
                //以下属性必须设置，实始化iWebOffice
                webform.WebOffice.WebUrl = "<%=mServerUrl%>";  //WebUrl:系统服务器路径，与服务器文件交互操作，如保存、打开文档，重要文件 
                webform.WebOffice.RecordID = "<%=mRecordID%>";   //RecordID:本文档记录编号
                webform.WebOffice.Template = "<%=mTemplate%>";   //Template:模板编号
                webform.WebOffice.FileName = "<%=mFileName%>";   //FileName:文档名称
                webform.WebOffice.FileType = "<%=mFileType%>";   //FileType:文档类型  .doc  .xls  .wps
                webform.WebOffice.EditType = "<%=mEditType%>";   //EditType:编辑类型  方式一、方式二  <参考技术文档>
                webform.WebOffice.UserName = "<%=mUserName%>";   //UserName:操作用户名

                //以下属性可以不要
                webform.WebOffice.ShowMenu = "1";  //ShowMenu:1 显示菜单  0 隐藏菜单
                webform.WebOffice.AppendMenu("1", "打开文件");
                webform.WebOffice.AppendMenu("2", "打印文档");
                webform.WebOffice.AppendMenu("3", "定义标签");
                webform.WebOffice.AppendMenu("4", "保存模板");
                webform.WebOffice.AppendMenu("5", "关闭");
                webform.WebOffice.DisableMenu("宏;选项;帮助");  //禁止菜单

                //交互OfficeServer的OPTION="LOADTEMPLATE"
                webform.WebOffice.WebOpen();  	//打开该文档    
                StatusMsg(webform.WebOffice.Status);
            }
            catch (e) {

            }
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
            } catch (e) {

            }
        }


        //作用：打开文档
        function LoadDocument() {
            StatusMsg("正在打开文档...");
            if (!webform.WebOffice.WebLoadTemplate()) {  //交互OfficeServer的OPTION="LOADTEMPLATE"
                StatusMsg(webform.WebOffice.Status);
            } else {
                StatusMsg(webform.WebOffice.Status);
            }
        }

        //作用：保存文档
        function SaveDocument() {
            webform.WebOffice.WebClearMessage();            //清空iWebOffice变量
            //标签直接保存入模板文件，不单独存储
            //if (!webform.WebOffice.WebSaveBookMarks())
            //{    
            ////交互OfficeServer的OPTION="SAVEBOOKMARKS"
            //StatusMsg(webform.WebOffice.Status);
            //return false;
            //}
            if (!webform.WebOffice.WebSaveTemplate()) {
                //交互OfficeServer的OPTION="SAVETEMPLATE"
                StatusMsg(webform.WebOffice.Status);
                alert(webform.WebOffice.Status);
                return false;
            }
            else {
                StatusMsg(webform.WebOffice.Status);
                if (confirm("保存成功，关闭本页面?")) {
                    self.close();
                }
                return true;
            }

        }

        //作用：填充模板
        function LoadBookmarks() {
            StatusMsg("正在填充模扳...");
            if (!webform.WebOffice.WebLoadBookmarks()) {
                //交互OfficeServer的OPTION="LOADBOOKMARKS"
                StatusMsg(webform.WebOffice.Status);
            }
            else {
                StatusMsg(webform.WebOffice.Status);
            }
        }

        //作用：设置书签值  vbmName:标签名称，vbmValue:标签值 标签名称注意大小写
        function SetBookmarks(vbmName, vbmValue) {
            if (!webform.WebOffice.WebSetBookmarks(vbmName, vbmValue)) {
                StatusMsg(webform.WebOffice.Status);
            }
            else {

            }
        }

        //作用：根据标签名称获取标签值  vbmName:标签名称
        function GetBookmarks(vbmName) {
            var vbmValue;
            vbmValue = webform.WebOffice.WebGetBookmarks(vbmName);
            return vbmValue;
        }

        //作用：打印文档
        function WebOpenPrint() {
            try {
                webform.WebOffice.WebOpenPrint();
                StatusMsg(webform.WebOffice.Status);
            }
            catch (e) {
            }
        }

        //作用：页面设置
        function WebOpenPageSetup() {
            try {
                if (webform.WebOffice.FileType == ".doc") {
                    webform.WebOffice.WebObject.Application.Dialogs(178).Show();
                }
                if (webform.WebOffice.FileType == ".xls") {
                    webform.WebOffice.WebObject.Application.Dialogs(7).Show();
                }
            }
            catch (e) {

            }
        }

        //作用：标签管理
        function WebOpenBookMarks() {
            try {
                webform.WebOffice.WebOpenBookmarks();    //交互OfficeServer的OPTION="LISTBOOKMARKS"
                StatusMsg(webform.WebOffice.Status);
            }
            catch (e) {
            }
        }

        //作用：存为本地文件
        function WebSaveLocal() {
            try {
                webform.WebOffice.WebSaveLocal();
                StatusMsg(webform.WebOffice.Status);
            }
            catch (e) {
            }
        }

        //作用：打开本地文件
        function WebOpenLocal() {
            try {
                webform.WebOffice.WebOpenLocal();
                StatusMsg(webform.WebOffice.Status);
            }
            catch (e) {
            }
        }
    </script>
</head>
<body style="margin: 0px;" onload="Load()" onunload="UnLoad()">
    <form id="webform" runat="server" onsubmit="return SaveDocument();">
    <input type="hidden" name="RecordID" value="<%=mRecordID%>" />
    <input type="hidden" name="Template" value="<%=mTemplate%>" />
    <input type="hidden" name="FileType" value="<%=mFileType%>" />
    <input type="hidden" name="EditType" value="<%=mEditType%>" />
    <input type="hidden" name="FileName" value="<%=mFileName%>" />
    <div>
        <!--调用iWebOffice，注意版本号，可用于升级-->
        <script src="iWebOffice2003.js" type="text/jscript"></script>
    </div>
    <div style="display: none;" id="StatusBar">
        状态栏</div>
    </form>
</body>
</html>
