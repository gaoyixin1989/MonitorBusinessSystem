<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_OA_FW_FWAudit" Codebehind="FWAudit.aspx.cs" %>

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
    <script src="FWAudit.js" type="text/javascript"></script>

    <style type="text/css">
    
    .l-minheight div{ height:90px; overflow:hidden;}
        .style1
        {
            width: 15%;
            height: 22px;
        }
    </style>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
		<h1 class="h12013">发 文 呈 批 表</h1>
        <div class="tablelayout">
        <div class="tabletop "> 编号：<asp:Label ID="YWNO" runat="server" Text=""></asp:Label><p> 日期：<asp:Label ID="FW_DATE" runat="server"></asp:Label>
		</p></div>
        <table  border="0" cellpadding="0" cellspacing="0" class="tableedit mlr8">
            <tr>
                <th >
                    文件标题 </th>
                <td colspan="5"  >
                    <asp:Label ID="FW_TITLE" runat="server" Text="" class="tableedit_title"></asp:Label> </td>
            </tr>
            <tr>
                <th >
                    主办部门  </th>
                <td >
                    <asp:Label ID="ZB_DEPT" runat="server" Text="" style="width:240px;"></asp:Label>                </td>
                <td style=" text-align:right;width:55px;">
                    经办人 </td>
                <td style="width:80px;" >
                    <asp:Label ID="DRAFT_ID" runat="server" Text=""></asp:Label>                </td>
                <td style=" text-align:right;width:55px;" >
                    密级                </td>
                <td >
                    <asp:Label ID="MJ" runat="server" ></asp:Label>                </td>
            </tr>
            <tr>
                <th >
                    办理期限                </th>
                <td >
                    <asp:Label ID="START_DATE" runat="server" Text=""></asp:Label>                
                    至 
                    <asp:Label ID="END_DATE" runat="server" Text="" ></asp:Label>              </td>
                <td style="  text-align:right;">发文字号</td>
                <td colspan="3" >
                    <asp:Label ID="FWNO" runat="server" Text="" style="width:200px;"></asp:Label>                </td>
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
            <tr>
                <th rowspan="2" >
                    发起科室负责人意见                                </th>
                <td colspan="5" >
                    <textarea cols="100" rows="3" class="l-textarea" id="APP_INFO" runat="server"
                            style=" padding:2px; margin-left:0; line-height:20px;" name="S1" ></textarea>                </td>
            </tr>
            <tr>
                <td colspan="5"style="  text-align:right;"> 
                
                   <span> 办理人：<asp:Label ID="APP_ID" runat="server" Text=""></asp:Label></span>
                    <span>办理时间：<asp:Label ID="APP_DATE" runat="server" Text=""></asp:Label></span>               </td>
            </tr>
            <tr>
                <th rowspan="2" >
                    站长签发意见审核                                </th>
                <td colspan="5" >
                    <textarea cols="100" rows="3" class="l-textarea" id="ISSUE_INFO"  runat="server"
                            style=" padding:2px; margin-left:0; line-height:20px;" name="S1" ></textarea>                </td>
            </tr>
            <tr>
                <td colspan="5" style="  text-align:right;" >
                    
                    <span>办理人：<asp:Label ID="ISSUE_ID" runat="server" Text=""></asp:Label></span>
                   <span> 办理时间：<asp:Label ID="ISSUE_DATE" runat="server" Text=""></asp:Label>  </span>              </td>
            </tr>
            <tr>
                <th rowspan="2" >
                    受理科室负责人意见                 </th>
                <td colspan="5" >
                    <textarea cols="100" rows="3" class="l-textarea" id="REG_INFO"  runat="server"
                            style=" padding:2px; margin-left:0; line-height:20px;" name="S1" ></textarea>                </td>
            </tr>
            <tr>
                <td colspan="5" style="  text-align:right;">
                   
                    <span>办理人：<asp:Label ID="REG_ID" runat="server" Text=""></asp:Label></span>
                    <span>办理时间：<asp:Label ID="REG_DATE" runat="server" Text=""></asp:Label></span>                </td>
            </tr>
      </table>
	      <div id="divContratSubmit"  >
        <UC:WFControl ID="wfControl" EnableViewState="true" runat="server" />
    </div>
    <div>
        <input type="hidden"  id="hidTaskId" runat="server" />
        <input type="hidden"  id="hidTask_Tatus" value="0" runat="server"  />
        <input type="hidden"  id="hidBtnType" runat="server" />
    </div>
    </div>

    </form>
</body>
</html>
