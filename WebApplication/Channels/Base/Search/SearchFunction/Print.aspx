<%@ Page Language="C#" AutoEventWireup="True" EnableEventValidation="false"    ValidateRequest="false" Inherits="Channels_Base_Search_SearchFunction_Print" Codebehind="Print.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {

            document.getElementById("btnxls").click();

            setTimeout(function () {
                //parent.$.ligerDialog.close();
                //parent.window.location.reload();
                parent.detailExcelWin.hide();
            }, 2000);
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        导出中...
        <div style="visibility: hidden">
            <%--secondgrid为父页面被导出的表ID--%>
            <asp:Button ID="btnxls" runat="server" Text="导出Excel" OnClick="Button1_Click"  />
            <%--<asp:Button ID="btndoc" runat="server" Text="导出Word" OnClick="Button2_Click"  />--%>
        </div>
        <input type="hidden" id="TASK_ID" runat="server" />
    </div>
    </form>
</body>
</html>

