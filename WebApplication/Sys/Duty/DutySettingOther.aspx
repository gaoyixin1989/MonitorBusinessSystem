<%@ Page Language="C#" AutoEventWireup="True" Inherits="Sys_Duty_DutySettingOther" Codebehind="DutySettingOther.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title></title>

    <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/style.css" rel="stylesheet" type="text/css" />
        <!--加载zTree菜单树控件必须文件-->
    <link href="../../Controls/zTree3.4/css/zTreeStyle/zTreeStyle.css" rel="stylesheet"
        type="text/css" />
     <link rel="stylesheet" href="../../Controls/zTree3.4/css/divuniontable.css" type="text/css"/>

          <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../Controls/zTree3.4/js/jquery.ztree.core-3.4.min.js" type="text/javascript"></script>
    <script src="../../Controls/zTree3.4/js/jquery.ztree.exedit-3.4.min.js" type="text/javascript"></script>


    <script src="../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script> 

    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
        <script src="../../Controls/ligerui/lib/draggable.js" type="text/javascript"></script>
        <script src="DutySettingOther.js" type="text/javascript"></script>
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
    </style>
</head>
<body style="padding: 0px; overflow: hidden; width: 100%; height: 100%;">
<div class="l-loading" style="display:block" id="pageloading"></div>
<%--<div class="content_wrap">
	<div class="zTreeDemoBackground left">
		
	</div>
    <div class="right">
        
    </div>
</div>--%>

<div id="layout1">
    <div position="left" title="监测类别列表">
        <ul id="tree" class="ztree"></ul>
    </div>
    <div position="right" title="用户列表">
        <div id="divuserlistOther" ></div>
    </div>
</div>

<div id="targerdiv" class="l-form"  style="width:350px; margin:3px; display:none;" >
<ul>
<li  style="width:37px; text-align:left;">
 部门：
</li>
<li  style="width:240px;text-align:left;">
<input id="Dept" class="l-text l-text-editing" name="Dept" type="text" />
</li>
<li>
 <table cellpadding="0" cellspacing="0" class="tabletool" >
             <tr>
                <td align="center" colspan="2" >
         
                    <b>未选用户</b>
            <select size="10" name="listLeft" multiple="multiple" id="listLeft"  title="双击可实现右移" class="searchb" style=" width:170px;"> 
            </select> 
       
                </td>
                <td align="center">&nbsp;</td>
                <td align="center" >

                <input type="button" id="btnRight" value=">>"  class="l-button l-button-submit"/><br />

                <input type="button" id="btnLeft" value="<<" class="l-button l-button-submit"/>
                </td>
                <td  align="center" class="l-table-edit-td">&nbsp;</td>
                <td align="center"  class="l-table-edit-td"> 
                    <b>已选用户</b>
                <select size="10" name="listRight" multiple="multiple" id="listRight"   title="双击可实现左移" class="searchb" style=" width:170px;"> 
                </select>
        </td>
            </tr>
</table>
</li>
</ul>
</div>
</body>
    </html>