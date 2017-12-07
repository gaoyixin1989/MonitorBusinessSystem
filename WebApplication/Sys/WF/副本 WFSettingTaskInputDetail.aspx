<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Input.master" AutoEventWireup="True" Inherits="Sys_WF_WFSettingTaskInputDetail" Codebehind="副本 WFSettingTaskInputDetail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphInput" runat="Server">
    <div style="text-align: left;">
        <table style="width: 100%;">
            <tr>
                <td>
                    <div style="text-align: left;">
                        <input type="button" value="流程列表" onclick="javascript:window.location.href='WFSettingFlowList.aspx'"
                            id="btnGoBack" />
                        <asp:Button ID="btnGoBackStep" Text="环节列表" runat="server" OnClick="btnBack_Click" />
                        <asp:Button ID="btnGoNextStep" Text="下个环节" runat="server" OnClick="btnGoNextStep_Click" />
                    </div>
                </td>
                <td>
                    <asp:HiddenField ID="WF_ID" runat="server" />
                    <asp:HiddenField ID="WF_TASK_ID" runat="server" />
                    <asp:HiddenField ID="TASK_ORDER" runat="server" />
                </td>
                <td>
                    当前流程为：
                    <asp:Label ID="lblCurFlowName" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="text-align: right;">
                                简称:
                            </td>
                            <td>
                                <asp:TextBox ID="TASK_CAPTION" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">
                                描述:
                            </td>
                            <td>
                                <asp:TextBox ID="TASK_NOTE" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                处理者
                                <asp:RadioButtonList ID="rdbtnlstOperType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdbtnlstOperType_SelectedIndexChanged">
                                    <asp:ListItem Text="用户" Selected="True" Value="01">用户</asp:ListItem>
                                    <asp:ListItem Text="职位" Value="02">职位</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td colspan="3">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ListBox ID="lsbAll" Width="120px" Height="190px" runat="server"></asp:ListBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAdd" Text="==>" runat="server" OnClick="btnAdd_Click" /><br />
                                            <asp:Button ID="btnSub" Text="<==" runat="server" OnClick="btnSub_Click" /><br />
                                            <asp:Button ID="btnClear" Text="清空" runat="server" OnClick="btnClear_Click" />
                                        </td>
                                        <td>
                                            <asp:ListBox ID="lsbStep" Width="120px" Height="190px" runat="server"></asp:ListBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="vertical-align: top;">
                    <div>
                        <div style="text-align: left;">
                            <span style="font-weight: bold;">请选择节点命令集</span>
                            <asp:CheckBoxList ID="ckbxlstCMDList" RepeatColumns="4" RepeatDirection="Horizontal"
                                runat="server">
                                <asp:ListItem Text="发送" Value="01" Selected="True">发送</asp:ListItem>
                                <asp:ListItem Text="回退" Value="00">回退</asp:ListItem>
                                <asp:ListItem Text="跳转" Value="08">跳转</asp:ListItem>
                                <asp:ListItem Text="转发" Value="02">转发</asp:ListItem>
                                <asp:ListItem Text="销毁" Value="09">销毁</asp:ListItem>
                                <asp:ListItem Text="挂起" Value="03">挂起</asp:ListItem>
                                <asp:ListItem Text="恢复" Value="04">恢复</asp:ListItem>
                                <asp:ListItem Text="恢复" Value="05">暂停</asp:ListItem>
                                <asp:ListItem Text="返元" Value="06">返元</asp:ListItem>
                                <asp:ListItem Text="返元" Value="10">保存</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                        <div style="text-align: left;">
                            <span style="font-weight: bold;">请选择附加功能</span>
                            <asp:CheckBoxList ID="ckbxlstPowerList" RepeatColumns="2" RepeatDirection="Horizontal"
                                runat="server">
                                <asp:ListItem Text="发送" Value="31">选择人员</asp:ListItem>
                                <asp:ListItem Text="回退" Value="32">上传附件</asp:ListItem>
                                <asp:ListItem Text="转发" Value="33">审批评论</asp:ListItem>
                                <asp:ListItem Text="销毁" Value="34">发表意见</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                        <div style="text-align: left;">
                            <span style="font-weight: bold;">是否为路由节点</span>
                            <asp:RadioButtonList ID="rdbtnlstAndOr" RepeatDirection="Horizontal" runat="server">
                                <asp:ListItem Text="OR" Selected="True" Value="0">否</asp:ListItem>
                                <asp:ListItem Text="AND" Value="1">是</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div style="text-align: left;">
                            <span style="font-weight: bold;">页面控件类型</span>
                            <br />
                            <asp:RadioButtonList ID="UCM_TYPE" Enabled="true" RepeatDirection="Horizontal" runat="server">
                                <asp:ListItem Text="页面" Selected="True" Value="01">页面</asp:ListItem>
                                <asp:ListItem Text="表单" Value="02">表单</asp:ListItem>
                                <asp:ListItem Text="控件" Value="03">控件</asp:ListItem>
                                <asp:ListItem Text="组件" Value="04">组件</asp:ListItem>
                            </asp:RadioButtonList>
                            <br />
                            <asp:TextBox ID="UCM_ID" runat="server" Text="~/"></asp:TextBox>
                        </div>
                    </div>
                    <div style="vertical-align: bottom;">
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
                        <asp:Button ID="btnBack" runat="server" Text="返回" OnClick="btnBack_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
