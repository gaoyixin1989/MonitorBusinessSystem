<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_OA_SW_SWAudit" Codebehind="SWAudit.aspx.cs" %>

<%@ Register TagPrefix="UC" TagName="WFControl" Src="~/Sys/WF/UCWFControls.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../Controls/AutoComplete/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
     <!--Jquery 基础文件-->
     <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>

     <!--自动完成-->
    <script src="../../../Controls/AutoComplete/jquery.autocomplete.js" type="text/javascript"></script>
    <!--提示-->
    <script src="../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
     <!--LigerUI-->
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenuBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
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
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="SWAudit.js" type="text/javascript"></script>

    <style type="text/css">
    
    .l-minheight div{ height:90px; overflow:hidden;}
        .style1
        {
            width: 15%;
            height: 22px;
        }
        
        h1
        {
            color: Green;
        }
        #listLeft
        {
            width: 160px;
            height: 260px;
            text-align: right;
        }
        
        #listRight
        {
            width: 160px;
            height: 260px;
            text-align: left;
        }
        .normal
        {
            font-size: 12px;
            width: 10px;
            text-align: left;
        }
    </style>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
		<h1 class="h12013">收 文 呈 批 表</h1>
        <div class="tablelayout">
        <div class="tabletop "> 原文编号：<asp:Label ID="FROM_CODE" runat="server" Text=""></asp:Label><p> 日期：<asp:Label ID="SW_REG_DATE" runat="server"></asp:Label>
		</p>
            </div>
        <table  border="0" cellpadding="0" cellspacing="0" class="tableedit mlr8">
            <tr>
                <th >
                    文件标题 </th>
                <td colspan="5"  >
                    <asp:Label ID="SW_TITLE" runat="server" Text="" class="tableedit_title"></asp:Label> </td>
            </tr>
            <tr>
                <th >
                    来文单位  </th>
                <td >
                    <asp:Label ID="SW_FROM" runat="server" Text="" style="width:160px;"></asp:Label>                </td>
                <td style=" text-align:right;width:65px;">
                    收文份数 </td>
                <td style="width:80px;" >
                    <asp:Label ID="SW_COUNT" runat="server" Text=""></asp:Label>份                 </td>
                <td style=" text-align:right;width:65px;" >
                    紧急程度                </td>
                <td >
                    <asp:Label ID="MJ" runat="server" ></asp:Label>                </td>
            </tr>
            <tr>
                <th >
                    签收人  </th>
                <td >
                    <asp:Label ID="SW_SIGN_ID" runat="server" Text="" style="width:160px;"></asp:Label>                </td>
                <td style="  text-align:right;" >
                    签收日期  </td>
                <td >
                    <asp:Label ID="SW_SIGN_DATE" runat="server" Text="" style="width:100px;"></asp:Label>                </td>
                <td style="  text-align:right;">收文字号</td>
                <td>
                    <asp:Label ID="SW_CODE" runat="server" Text="" style="width:100px;"></asp:Label>                </td>
            </tr>

            <tr>
                <th>
                    附件信息:                  </th>
                <td align="left"  colspan="5">
                    <div id="divDownLoad" >
                        <a id="btnFiledownLoad" href="#" >点击查看下载附件</a>
                    </div>
                </td>
            </tr>

            <tr id="dAcceptUserLst" runat="server" visible="false">
                <th>
                    办理人员:                  </th>
                <td align="left"  colspan="5">
                  
                        <input type="text" id="ACCEPT_USERIDS" style="width:450px;" onclick="javascript:return ACCEPT_REALNAMES_select();" />
                        <input id="HID_USERIDS" runat="server" type="text" style="display:none;" />
                </td>
            </tr>

            <tr>
                <th rowspan="2" >
                    收文拟办审核                                </th>
                <td colspan="5" >
                    <textarea cols="100" rows="3" class="l-textarea" id="SW_PLAN_INFO"  runat="server"
                            style=" padding:2px; margin-left:0; line-height:20px;" name="S1" ></textarea>                </td>
            </tr>
            <tr>
                <td colspan="5"style="  text-align:right;"> 
                   <span> 办理人：<asp:Label ID="SW_PLAN_ID" runat="server" Text=""></asp:Label></span>
                    <span>办理时间：<asp:Label ID="SW_PLAN_DATE" runat="server" Text=""></span></asp:Label>                </td>
            </tr>
            <tr>
                <th rowspan="2" >
                    收文批办审核                                </th>
                <td colspan="5" >
                    <textarea cols="100" rows="3" class="l-textarea" id="SW_PLAN_APP_INFO" runat="server"
                            style=" padding:2px; margin-left:0; line-height:20px;" name="S1" ></textarea>                </td>
            </tr>
            <tr>
                <td colspan="5" style="  text-align:right;" >
                    <span>办理人：<asp:Label ID="SW_PLAN_APP_ID" runat="server" Text=""></asp:Label></span>
                   <span> 办理时间：<asp:Label ID="SW_PLAN_APP_DATE" runat="server" Text=""></asp:Label>  </span>              </td>
            </tr>
            <tr>
                <th rowspan="2" >
                    收文办理负责人意见                 </th>
                <td colspan="5" >
                    <textarea cols="100" rows="3" class="l-textarea" id="SW_APP_INFO" runat="server"
                            style=" padding:2px; margin-left:0; line-height:20px;" name="S1" ></textarea>                </td>
            </tr>
            <tr>
                <td colspan="5" style="  text-align:right;">
                    <span>办理人：<asp:Label ID="SW_APP_ID" runat="server" Text=""></asp:Label></span>
                    <span>办理时间：<asp:Label ID="SW_APP_DATE" runat="server" Text=""></asp:Label></span>                </td>
            </tr>
      </table>
	<div id="divContratSubmit"  runat="server">
        <asp:Button ID="Button1" runat="server" Text="发送" Visible="false" class="l-button l-button-submit" />
        <UC:WFControl ID="wfControl" EnableViewState="true" runat="server" />
    </div>
    <div id="divBack" visible="false" runat="server" style="text-align:center;">
        <input type="button" value="返回" id="btn_Back" name="btn_Back" class="l-button l-button-submit"
            style=" display:inline-block; margin-top:8px;" onclick="javascript:self.location='FWList.aspx';"  />
    </div>
    <div>
        <input type="hidden"  id="hidTaskId" runat="server" />
        <input type="hidden"  id="hidTask_Tatus" value="0" runat="server"  />
        <input type="hidden"  id="hidBtnType" runat="server" />
    </div>
    </div>

    </form>

    <div id="targerdiv" class="l-form" style="width: 350px; margin: 3px; display: none;">
        <ul>
            <li style="width: 37px; text-align: left;">部门： </li>
            <li style="width: 240px; text-align: left;">
                <input id="Dept" class="l-text l-text-editing" name="Dept" type="text" />
            </li>
            <li>
                <table cellpadding="0" cellspacing="0" class="l-table-edit">
                    <tr>
                        <td align="center" colspan="2">
                            <div>
                                <b>未选用户</b>
                                <select size="10" name="listLeft" multiple="multiple" id="listLeft" class="normal"
                                    title="双击可实现右移">
                                </select>
                            </div>
                        </td>
                        <td align="center">
                            &nbsp;
                        </td>
                        <td align="center">
                            <input type="button" id="btnRight" value=">>" class="l-button l-button-submit" /><br />
                            <input type="button" id="btnLeft" value="<<" class="l-button l-button-submit" />
                        </td>
                        <td align="center" class="l-table-edit-td">
                            &nbsp;
                        </td>
                        <td align="center" class="l-table-edit-td">
                            <b>已选用户</b>
                            <select size="10" multiple="multiple" name="listRight" id="listRight" class="normal"
                                title="双击可实现左移">
                            </select>
                        </td>
                    </tr>
                </table>
            </li>
        </ul>
    </div>
</body>
</html>
