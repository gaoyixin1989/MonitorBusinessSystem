<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_OA_SW_ZZ_SWHandle" Codebehind="SWHandle.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../../Controls/AutoComplete/css/jquery.autocomplete.css" rel="stylesheet"
        type="text/css" />
    <!--Jquery 基础文件-->
    <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <!--自动完成-->
    <script src="../../../../Controls/AutoComplete/jquery.autocomplete.js" type="text/javascript"></script>
    <!--提示-->
    <script src="../../../../Controls/ligerui/lib/jquery-validation/jquery.validate.min.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/jquery-validation/jquery.metadata.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <!--LigerUI-->
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenuBar.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
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
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="SWHandle.js" type="text/javascript"></script>

    <style type="text/css">
        .l-minheight div 
        {
        	height:90px; overflow:hidden;
        }
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
    <h1 class="h12013">收 文 处 理 流 程</h1>
    <div class="tablelayout">
        <div class="tabletop " style="height:18px">
        <p> 日期：<asp:Label ID="SW_REG_DATE" runat="server" Text=""></asp:Label></p>
		</div>
        <table  border="0" cellpadding="0" cellspacing="0" class="tableedit mlr8">
            <tr>
                <th>原号</th>
                <td><asp:TextBox ID="FROM_CODE" runat="server" Text=""></asp:TextBox></td>
                <td style="width:60px; text-align:right;">来文机关</td>
                <td><asp:TextBox ID="SW_FROM" runat="server" Text=""></asp:TextBox></td>
                <td style="width:60px; text-align:right;">编号</td>
                <td><asp:TextBox ID="SW_CODE" runat="server" Text=""></asp:TextBox> </td>
            </tr>
            <tr>
                <th >
                    登记人  </th>
                <td > <asp:TextBox ID="SW_SIGN_ID" runat="server" Text="" style="width:160px;"></asp:TextBox>
                                   </td>
                <td style="  text-align:right;">登记日期</td>
                <td> <asp:TextBox ID="SW_SIGN_DATE" runat="server" Text="" style="width:100px;"></asp:TextBox></td>
                <td style="  text-align:right;" >
                    办结日期  </td>
                <td >
                    <asp:TextBox ID="PIGONHOLE_DATE" runat="server" Text="" Enabled="false"></asp:TextBox>                </td>
                
                
            </tr>
           
            <tr style="display:none;">
                <th >
                    签收人  </th>
                <td >
                                   </td>
                <td style=" text-align:right;width:55px;">
                    收文份数 </td>
                <td style="width:80px;" >
                    <asp:TextBox ID="SW_COUNT" runat="server" Text="" style="width:40px;"></asp:TextBox> &nbsp;份                 </td>
                <td style=" text-align:right;width:55px;" >
                    密级                </td>
                <td >
                    <asp:DropDownList ID="MJ" runat="server" ></asp:DropDownList>                </td>
            </tr>
            <tr>
                <th >
                    标题 </th>
                <td colspan="5"  >
                    <asp:TextBox ID="SW_TITLE" runat="server" Text="" class="tableedit_title"></asp:TextBox> </td>
            </tr>
            <tr>
                <th >
                    主题词 </th>
                <td colspan="5"  >
                    <asp:TextBox ID="SUBJECT_WORD" runat="server" Text="" class="tableedit_title"></asp:TextBox> </td>
            </tr>
            <tr id="trFileUpDown">
                <th>
                    附件信息:                  </th>
                <td align="left"  colspan="5">
                    <div id="divDownLoad" >
              <%--          <a id="btnFiledownLoad" href="#" >点击查看下载附件</a>--%>
                        <a id="btnFileUp"  href="javascript:">点击上传</a> 
                    </div>
                </td>
            </tr>
            <tr id="TReadUserList" runat="server">
                <th>
                    阅办人员:                  </th>
                <td align="left"  colspan="5">
                  
                        <input type="text" id="ReadUserNames" runat="server" style="width:450px;" onclick="javascript:return ACCEPT_REALNAMES_select('ReadUserNames','Hid_ReadUserIDs');" />
                        <input id="Hid_ReadUserIDs" runat="server" type="text" style="display:none;" />
                </td>
            </tr>
            <tr id="TMakeUserList" runat="server">
                <th>
                    办结人员:                  </th>
                <td align="left"  colspan="5">
                  
                        <input type="text" id="MakeUserNames" runat="server" style="width:450px;" onclick="javascript:return ACCEPT_REALNAMES_select('MakeUserNames','Hid_MakeUserIDs');" />
                        <input id="Hid_MakeUserIDs" runat="server" type="text" style="display:none;" />
                </td>
            </tr>
             <tr>
                <th>
                    办公室主任阅示                                </th>
                <td colspan="5" >
                    <textarea cols="100" rows="8" class="l-textarea" id="SW_PLAN2_INFO"  runat="server"
                            style=" padding:2px; margin-left:0; line-height:20px;" name="S1" ></textarea>                </td>
            </tr>
            <tr>
                <th>
                    主要领导批示                                </th>
                <td colspan="5" >
                    <textarea cols="100" rows="12" class="l-textarea" id="SW_PLAN3_INFO" runat="server"
                            style=" padding:2px; margin-left:0; line-height:20px;" name="S1" ></textarea>                </td>
            </tr>
            <tr>
                <th rowspan="2" >
                    分管领导阅办                 </th>
                <td colspan="5" >
                    <textarea cols="100" rows="8" class="l-textarea" id="SW_PLAN4_INFO" runat="server"
                            style=" padding:2px; margin-left:0; line-height:20px;" name="S1" ></textarea>                </td>
            </tr>
            <tr >
                <td colspan="5" id="TdReadList" >
                    <a id="ReadList"  href="javascript:" onclick="javascript:return ShowSuggion('3');">查看阅办人意见</a>                </td>
            </tr>
            <tr>
                <th rowspan="2" >
                    科室办结                 </th>
                <td colspan="5" >
                    <textarea cols="100" rows="8" class="l-textarea" id="SW_PLAN5_INFO" runat="server"
                            style=" padding:2px; margin-left:0; line-height:20px;" name="S1" ></textarea>                </td>
            </tr>
            <tr >
                <td colspan="5" id="TdMakeList">
                    <a id="MakeList"  href="javascript:" onclick="javascript:return ShowSuggion('4');">查看办结人意见</a>                </td>
            </tr>
            <tr id="trHandlerList">
                <th>下一环节操作人</th>
                <td colspan="5">
                    <asp:DropDownList ID="HandlerList" runat="server" ></asp:DropDownList> 
                </td>
            </tr>
        </table>
        <div id="divBack" runat="server" style="text-align:center;">
            <input type="button" value="保存" id="btn_Save" name="btn_Save" class="l-button l-button-submit" style=" display:inline-block; margin-top:8px;" />
            <input type="button" value="发送" id="btn_Send" name="btn_Send" class="l-button l-button-submit" style=" display:inline-block; margin-top:8px;" />
            <input type="button" value="完成" id="btn_Finish" name="btn_Finish" class="l-button l-button-submit" style=" display:inline-block; margin-top:8px;" />
            <input type="button" value="返回" id="btn_Back" name="btn_Back" class="l-button l-button-submit" style=" display:inline-block; margin-top:8px;" />
            <asp:Button ID="btn_Print" Text="打印" runat="server"  class="l-button l-button-submit" style=" display:inline-block; margin-top:8px;" onclick="btn_Print_Click" />

        </div>

        <div>
            <input type="hidden"  id="hidUserID" runat="server" />
            <input type="hidden"  id="hidTaskId" runat="server" />
            <input type="hidden"  id="hidTaskStatus" runat="server"  />
            <input type="hidden"  id="hidPreUrl" runat="server" />
            <input type="hidden"  id="hidPreTitle" runat="server" />
            <input type="hidden" id="hid_FwId" runat="server" />
        </div>
    </div>
    </form>
    <div id="divSuggion" style="display:none;">
        <div id="divSugDetail">bbbbbbb</div>
    </div>

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

