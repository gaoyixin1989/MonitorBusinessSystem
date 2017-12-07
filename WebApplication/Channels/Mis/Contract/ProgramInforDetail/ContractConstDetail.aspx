<%@ Page Language="C#" AutoEventWireup="True" Inherits="n13.Channels_Mis_Contract_ProgramInforDetail_ContractConstDetail" Codebehind="ContractConstDetail.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/style.css" rel="stylesheet" type="text/css" />
     <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
     
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/draggable.js" type="text/javascript"></script>
    <!--货币格式-->
    <script src="../../../../Scripts/jquery.formatCurrency-1.4.0.js" type="text/javascript"></script>
    <script src="ContractConstDetail.js" type="text/javascript"></script>
</head>
<body style="padding: 6px; overflow: hidden; width: 100%; height: 100%;">
<div class="l-loading" style="display:block" id="pageloading"></div>
    <div id="layout1" title="监测费用明细">
        <div position="top"> 
        <div id="divstestfree" >
        </div>
    </div>
        <div position="left" title="附加费用明细">
        <div id="divattfreelist" ></div>
        </div>
        <div position="right" title="费用合计">
        <div  id="divCost"  >
        <table>
        <tr>
                        <td align="right" class="l-table-edit-td">监测费用:</td>
                        <td align="left" class="l-table-edit-td">
                        <input type="text" id="Test_Fee" name="Test_Fee"   />
                        </td>
        </tr>
        <tr>
                        <td align="right" class="l-table-edit-td">附加费用:</td>
                        <td align="left" class="l-table-edit-td">
                        <input type="text" id="AttFee" name="AttFee"   />
                        </td>
        </tr>
        <tr>
                        <td align="right" class="l-table-edit-td">合计:</td>
                        <td align="left" class="l-table-edit-td">
                        <input type="text" id="Budget" name="Budget"  />
                        </td>

        </tr>
        <tr>
                        <td align="right" class="l-table-edit-td">实际收费:</td>
                        <td align="left" class="l-table-edit-td">
                        <input type="text" id="Income" name="Income"   />
                        </td>
        </tr>
        </table>
</div>
        </div>
    </div>
    <div id="detailSrh" style="display:none;">
    <div id="SrhForm"></div>
    </div>
</body>
</html>
