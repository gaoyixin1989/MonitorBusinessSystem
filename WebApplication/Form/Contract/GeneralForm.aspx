<%@ Page Language="C#" AutoEventWireup="True" Inherits="n26.Form_Contract_GeneralForm" Codebehind="GeneralForm.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript">

         function Save() {

             //alert('bb');
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <asp:Label ID="lblFormTitle" runat="server" Text="通用表单"></asp:Label>
    </div>
    <div>
    </div>
    <asp:HiddenField ID="hidContractId" runat="server" />
    <asp:HiddenField ID="hidIsNew" runat="server" />
    </form>
</body>
</html>
