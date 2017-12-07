<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Base_Search_EvaluationTapView" Codebehind="EvaluationTapView.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
     <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/welcome.css" rel="stylesheet" type="text/css" />
        <!--加载zTree菜单树控件必须文件-->
   <link rel="stylesheet" href="../../../Controls/zTree3.4/css/divuniontable.css" type="text/css"/>
    <link href="../../../Controls/zTree3.4/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
     <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../Controls/zTree3.4/js/jquery.ztree.core-3.4.min.js" type="text/javascript"></script>
    <script src="../../../Controls/zTree3.4/js/jquery.ztree.exedit-3.4.min.js" type="text/javascript"></script>

    <script src="../../../Controls/ligerui/lib/jquery-validation/jquery.validate.min.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
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

    <script src="EvaluationTapView.js" type="text/javascript"></script>
    	<style type="text/css">
.ztree li span.button.add {margin-left:2px; margin-right: -1px; background-position:-144px 0; vertical-align:top; vertical-align:middle}
	</style>

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
<body> 
<div class="navbar"><div class="navbar-l"></div><div class="navbar-r"></div>
<div class="navbar-icon"><img src="../../../Images/icons/32X32/communication.gif" /></div>
<div class="navbar-inner"> 
<b>评价标准信息</b> 
</div>
</div>
<div style=" float:left" >
<div id="inputform"></div>
</div>
<div class="content_wrap">
	<div class="zTreeDemoBackground left">
    <ul>
    <div class="navbar">
<div class="navbar-l"></div><div class="navbar-r"></div>
<div class="navbar-icon"><img src="../../../Images/icons/32X32/communication.gif" />
</div>
<div class="navbar-inner"> 
<b>条件项目树</b> 
</div>
</div></ul>
		<ul id="tree" class="ztree"></ul>
	</div>
<div class="right">
<div class="navbar" style="width:100%">
<div class="navbar-l"></div><div class="navbar-r"></div>
<div class="navbar-icon"><img src="../../../Images/icons/32X32/communication.gif" />
</div>
<div class="navbar-inner"> 
<b>条件项目阀值设置</b> 
</div>
</div>
            <div id="maingrid" style="position:absolute; top:130px; left:266px;">
                </div>
            <div id="dvtap" style="display:none;"><div id="editEvaluform" ></div> 
            </div>
            </div>
	</div>

<!--监测项目设置-->
<div id="targerdiv" style="width:400px; margin:3px; display:none;" >
<div class="l-loading" style="display:block" id="pageloading"></div>
 <table cellpadding="0" cellspacing="0" class="l-table-edit" >
             <tr>
                <td align="center" class="l-table-edit-td">
                <div>
                 检 索<input type="text" value="" id="txtSeach" class="l-text l-text-editing" onkeyup="javascript:txtSeachOption();"; /> <br />
                <b>未选监测项目</b>
            <select size="10" name="listLeft" multiple="multiple" id="listLeft"  class="normal"  title="双击可实现右移"> 
            </select> 
            </div>
                </td>
                <td align="center" class="l-table-edit-td">&nbsp;</td>
                <td >
                
                <input type="button" id="btnRight" value=">>"  class="l-button l-button-submit"/><br />

                <input type="button" id="btnLeft" value="<<" class="l-button l-button-submit"/>
                </td>
                <td  align="center" class="l-table-edit-td">&nbsp;</td>
                <td align="center"  class="l-table-edit-td"> 
                 <br /> 
                    <b>已选监测项目</b>
                <select size="10" name="listRight" multiple="multiple" id="listRight"  class="normal"   title="双击可实现左移"> 
                </select>
        </td>
            </tr>
</table>
</div>
      </body>
</html>
