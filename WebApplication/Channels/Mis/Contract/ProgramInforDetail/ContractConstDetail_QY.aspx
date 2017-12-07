<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="Channels_Mis_Contract_ProgramInforDetail_ContractConstDetail_QY" Codebehind="ContractConstDetail_QY.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/style.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/draggable.js" type="text/javascript"></script>
    <!--货币格式-->
    <script src="../../../../Scripts/jquery.formatCurrency-1.4.0.js" type="text/javascript"></script>
    <script src="ContractConstDetail_QY.js" type="text/javascript"></script>
    <script type="text/javascript">
                $(function () {
                    $("#layout1").ligerLayout({ topHeight: 220, leftWidth: 360, allowLeftCollapse: false, height: 760 });
                    $("#layout2").ligerLayout({ topHeight: 180, leftWidth: '55%', rightWidth: '45%', allowLeftCollapse: false,
                        allowRightCollapse: false, height: 510
                    });
                    $("#layout5").ligerLayout({ topHeight: 220, leftWidth: '70%', rightWidth: '30%', allowLeftCollapse: false, height: 760 });
                    $("#layout3").ligerLayout({ topHeight: 220, leftWidth: 360, allowLeftCollapse: false, height: 760 });
                    $("#layout4").ligerLayout({ topHeight: 180, leftWidth: '75%', rightWidth: '25%', allowLeftCollapse: false,
                        allowRightCollapse: false, height: 510
                    });
                });
    </script>

    <style type="text/css">
#fixed_div
{
    position:absolute;
    height:240px;
    bottom:-10px;
    width:420px;
}
#fixed_div1
{
    position:absolute;
    left:430px;
    height:240px;
    bottom:-10px;
    width:400px;
}
#fixed_div p
{
    font-weight:bold;
    color:Blue;
    }
#fixed_div1 p
{
    font-weight:bold;
    color:Blue;
    }
</style> 
</head>
<body style="padding: 0px; overflow: hidden; width: 99%; height: 100%;">
    <form id="form1" runat="server">
    <div id="fixed_div" title=""> <p>&nbsp;&nbsp;附加费用明细</p>
     
        <div id="divattfreelist" ></div>
    </div>
    <div id="fixed_div1" title=""> <p>&nbsp;费用合计</p>
           <div position="right" title="费用合计">
                <div id="divCost">
                    <table>
                        <tr>
                            <td align="right" class="l-table-edit-td">
                                监测费用:
                            </td>
                            <td align="left" class="l-table-edit-td">
                                <input type="text" id="Test_Fee" name="Test_Fee" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="l-table-edit-td">
                                附加费用:
                            </td>
                            <td align="left" class="l-table-edit-td">
                                <input type="text" id="AttFee" name="AttFee" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="l-table-edit-td">
                                合计:
                            </td>
                            <td align="left" class="l-table-edit-td">
                                <input type="text" id="Budget" name="Budget" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="l-table-edit-td">
                                实际收费:
                            </td>
                            <td align="left" class="l-table-edit-td">
                                <input type="text" id="Income" name="Income" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
       
    </div>
         <div id="navtab1" style="width: 100%; overflow: hidden; border: 1px solid #A3C0E8;">
            <div id="layout1" title="采样">
                <div position="top">
                    <div id="divstestfree">
                    </div>
                </div>
            </div>
            <div id="layout3" title="样品测试">
                <div position="top">
                    <div id="oneGrid">
                    </div>
                </div>
            </div>
    </div>
       </form>
    
   
</body>
</html>
