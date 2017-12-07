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
      //�򿪱����ļ�
      WebOpenLocal();
   }
   if (vIndex==2)
   {  
      //��ӡ�ĵ�
      WebOpenPrint();
   }
   if (vIndex==3)
   {  
      //�����ǩ
      WebOpenBookMarks();
   }
   if (vIndex==4)
   {  
      //����ģ��
      SaveDocument();
   }
   if (vIndex==5)
   {  
      //�ر�
      self.close();
   }
    </script>
    <script language="javascript" type="text/javascript">

        //���ã���ʾ����״̬
        function StatusMsg(mString) {
            window.status = mString;
        }

        //���ã�����iWebOffice
        function Load() {
            try {
                //�������Ա������ã�ʵʼ��iWebOffice
                webform.WebOffice.WebUrl = "<%=mServerUrl%>";  //WebUrl:ϵͳ������·������������ļ������������籣�桢���ĵ�����Ҫ�ļ� 
                webform.WebOffice.RecordID = "<%=mRecordID%>";   //RecordID:���ĵ���¼���
                webform.WebOffice.Template = "<%=mTemplate%>";   //Template:ģ����
                webform.WebOffice.FileName = "<%=mFileName%>";   //FileName:�ĵ�����
                webform.WebOffice.FileType = "<%=mFileType%>";   //FileType:�ĵ�����  .doc  .xls  .wps
                webform.WebOffice.EditType = "<%=mEditType%>";   //EditType:�༭����  ��ʽһ����ʽ��  <�ο������ĵ�>
                webform.WebOffice.UserName = "<%=mUserName%>";   //UserName:�����û���

                //�������Կ��Բ�Ҫ
                webform.WebOffice.ShowMenu = "1";  //ShowMenu:1 ��ʾ�˵�  0 ���ز˵�
                webform.WebOffice.AppendMenu("1", "���ļ�");
                webform.WebOffice.AppendMenu("2", "��ӡ�ĵ�");
                webform.WebOffice.AppendMenu("3", "�����ǩ");
                webform.WebOffice.AppendMenu("4", "����ģ��");
                webform.WebOffice.AppendMenu("5", "�ر�");
                webform.WebOffice.DisableMenu("��;ѡ��;����");  //��ֹ�˵�

                //����OfficeServer��OPTION="LOADTEMPLATE"
                webform.WebOffice.WebOpen();  	//�򿪸��ĵ�    
                StatusMsg(webform.WebOffice.Status);
            }
            catch (e) {

            }
        }

        //���ã��˳�iWebOffice
        function UnLoad() {
            try {
                if (!webform.WebOffice.WebClose()) {
                    StatusMsg(webform.WebOffice.Status);
                }
                else {
                    StatusMsg("�ر��ĵ�...");
                }
            } catch (e) {

            }
        }


        //���ã����ĵ�
        function LoadDocument() {
            StatusMsg("���ڴ��ĵ�...");
            if (!webform.WebOffice.WebLoadTemplate()) {  //����OfficeServer��OPTION="LOADTEMPLATE"
                StatusMsg(webform.WebOffice.Status);
            } else {
                StatusMsg(webform.WebOffice.Status);
            }
        }

        //���ã������ĵ�
        function SaveDocument() {
            webform.WebOffice.WebClearMessage();            //���iWebOffice����
            //��ǩֱ�ӱ�����ģ���ļ����������洢
            //if (!webform.WebOffice.WebSaveBookMarks())
            //{    
            ////����OfficeServer��OPTION="SAVEBOOKMARKS"
            //StatusMsg(webform.WebOffice.Status);
            //return false;
            //}
            if (!webform.WebOffice.WebSaveTemplate()) {
                //����OfficeServer��OPTION="SAVETEMPLATE"
                StatusMsg(webform.WebOffice.Status);
                alert(webform.WebOffice.Status);
                return false;
            }
            else {
                StatusMsg(webform.WebOffice.Status);
                if (confirm("����ɹ����رձ�ҳ��?")) {
                    self.close();
                }
                return true;
            }

        }

        //���ã����ģ��
        function LoadBookmarks() {
            StatusMsg("�������ģ��...");
            if (!webform.WebOffice.WebLoadBookmarks()) {
                //����OfficeServer��OPTION="LOADBOOKMARKS"
                StatusMsg(webform.WebOffice.Status);
            }
            else {
                StatusMsg(webform.WebOffice.Status);
            }
        }

        //���ã�������ǩֵ  vbmName:��ǩ���ƣ�vbmValue:��ǩֵ ��ǩ����ע���Сд
        function SetBookmarks(vbmName, vbmValue) {
            if (!webform.WebOffice.WebSetBookmarks(vbmName, vbmValue)) {
                StatusMsg(webform.WebOffice.Status);
            }
            else {

            }
        }

        //���ã����ݱ�ǩ���ƻ�ȡ��ǩֵ  vbmName:��ǩ����
        function GetBookmarks(vbmName) {
            var vbmValue;
            vbmValue = webform.WebOffice.WebGetBookmarks(vbmName);
            return vbmValue;
        }

        //���ã���ӡ�ĵ�
        function WebOpenPrint() {
            try {
                webform.WebOffice.WebOpenPrint();
                StatusMsg(webform.WebOffice.Status);
            }
            catch (e) {
            }
        }

        //���ã�ҳ������
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

        //���ã���ǩ����
        function WebOpenBookMarks() {
            try {
                webform.WebOffice.WebOpenBookmarks();    //����OfficeServer��OPTION="LISTBOOKMARKS"
                StatusMsg(webform.WebOffice.Status);
            }
            catch (e) {
            }
        }

        //���ã���Ϊ�����ļ�
        function WebSaveLocal() {
            try {
                webform.WebOffice.WebSaveLocal();
                StatusMsg(webform.WebOffice.Status);
            }
            catch (e) {
            }
        }

        //���ã��򿪱����ļ�
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
        <!--����iWebOffice��ע��汾�ţ�����������-->
        <script src="iWebOffice2003.js" type="text/jscript"></script>
    </div>
    <div style="display: none;" id="StatusBar">
        ״̬��</div>
    </form>
</body>
</html>
