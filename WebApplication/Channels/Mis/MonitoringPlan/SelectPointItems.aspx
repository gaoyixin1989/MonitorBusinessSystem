<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Contract_ProgramInforDetail_SelectPointItems" Codebehind="SelectPointItems.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
     <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/welcome.css" rel="stylesheet" type="text/css" />
     <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
     <script src="../../../Controls/ligerui/lib/json2.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="SelectPointItems.js" type="text/javascript"></script>
    <style type="text/css">
        h1{color:Green;}
        #listLeft{width:160px;
                  height:260px;
            text-align: right;
        }
        
        #listRight{width:160px;
                  height:260px;
            text-align: left;
        }
        .normal{ font-size:12px;
            width: 10px;
            text-align: left;
        }
        .inputdiv .l-text-wrapper{ float:right; display:inline; overflow: hidden; clear:right; margin-top:-2px; margin-bottom:4px; }

    </style>
</head>
<body>
<!--监测项目设置-->
<div id="targerdiv" style="width:400px; margin:3px;" >
<div class="l-loading" style="display:block" id="pageloading"></div>
 <table cellpadding="0" cellspacing="0" class="tabletool" >
             <tr>
                <td align="center" class="l-table-edit-td">
                 <span style=" display:inline-block; padding-right:4px;">检索</span><input type="text" value="" id="txtSeach" class="l-text l-text-editing" onkeyup="javascript:txtSeachOption();"; /> <br />
                <b style="width:158px; margin-top:4px;">未选监测项目</b>
            <select size="10" name="listLeft" multiple="multiple" id="listLeft"  class="normal" style="width:160px;"  title="双击可实现右移"> 
            </select> 

                </td>
                <td align="center" class="l-table-edit-td">&nbsp;</td>
                <td >
                
                <input type="button" id="btnRight" value=">>"  class="l-button l-button-submit"/><br />

                <input type="button" id="btnLeft" value="<<" class="l-button l-button-submit"/>
                </td>
                <td  align="center" class="l-table-edit-td">&nbsp;</td>
                <td align="left" class="inputdiv">                 
                 <b style="width:180px; clear:both; margin-top:4px; overflow:hidden;">已选监测项目</b>
                <select size="10" name="listRight" multiple="multiple" id="listRight"  class="normal"   style="width:182px;" title="双击可实现左移"> 
                </select>
        </td>
            </tr>
</table>
</div>
</body>
</html>
