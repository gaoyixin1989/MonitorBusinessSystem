<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Contract_ProgramCreate_ccflow_Printf" Codebehind="Printf.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title></title>
</head>
<body>
    <form id="form2" runat="server">
    <div>

    </div>
    </form>
</body>
</html>--%>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
      <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
     <link href="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
     <link href="../../../../Controls/AutoComplete/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
    <script src="../../../../Controls/ligerui/lib/json2.js" type="text/javascript"></script>
    <!--Jquery 表单验证-->
     <script src="../../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
    <!--自动完成-->
    <script src="../../../../Controls/AutoComplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script> 
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
        <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
            <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
               <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
                <script src="../../../../Controls/ligerui/lib/draggable.js" type="text/javascript"></script>
                 <script src="../../../../Controls/ligerui/lib/json2.js" type="text/javascript"></script> 
    <script src="../../../../Controls/ligerui/lib/draggable.js" type="text/javascript"></script>

    <script src="../../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="Printf.js" type="text/javascript"></script>
    <title></title>
         <style type="text/css"> 

            body{ padding:5px; margin:0; padding-bottom:15px;}
            #layout1{  width:100%;margin:0; padding:0;  }  
            h4{ margin:20px;}
                .l-button-submit
             {}
                </style>
    <script language="javascript" type="text/javascript">

    </script>
</head>
<body style="padding: 6px; overflow: hidden; width: 100%; height: 100%;">
    <form id="form1" runat="server">
    <div class="l-loading"></div>
    <div id="layout1"  style="width: 99%;overflow:hidden; border:1px solid #A3C0E8; ">
    <div position="top">
    <table>
    <tr>
                    <td align="right"  class="l-table-edit-td" style=" padding-top:5px">样品编号: </td>
                    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
                        <input type="text" id="Name22" name="Name"  class="l-text l-text-editing" style="width:200px" runat="server"/>
                    </td>
                     <td align="right"  class="l-table-edit-td" style=" padding-top:5px">&nbsp;&nbsp;监测项目: </td>
                    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
                        <input type="text" id="std" name="std"  class="l-text l-text-editing" style="width:200px"/>
                    </td>
                    <td align="right"  class="l-table-edit-td" style=" padding-top:5px">&nbsp;&nbsp;采样时间: </td>
                    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
                        <input type="text" id="Time" name="Time"  class="l-text l-text-editing" style="width:200px"/>
                    </td>
                    <td align="right"  class="l-table-edit-td" style=" padding-top:5px">&nbsp;&nbsp;&nbsp;&nbsp;条形码编号: </td>
                    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
                        <input type="text" id="Number" name="Number"  class="l-text l-text-editing" style="width:200px"/>
                    </td>
    </tr>
                    <tr>
 
                     <td align="center"  class="l-table-edit-td" style=" padding-top:5px; width:100px">
                        &nbsp;<asp:Button ID = "btn_Ok" runat="server" Text ="打 印" 
                             CssClass ="l-button l-button-submit" onclick="btn_Ok_Click" Height="23px" 
                             Width="61px" />
                    </td>
                </tr>
    </table>
    </div>
        </div>
    </form>
</body>
</html>
