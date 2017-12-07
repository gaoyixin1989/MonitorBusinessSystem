<%@ Control Language="C#" AutoEventWireup="True"
    Inherits="Sys_WF_UCWFControls" Codebehind="UCWFControls.ascx.cs" %>
<style type="text/css">
    .style1
    {
        font-family: Arial, Tahoma, 宋体;
    }
    .listfooter2012 input
    {
        margin: 4px;
        display: inline-block;
    }
    .listfooter2012
    {
        clear: both;
        text-align: left;
        padding-left: 7px;
        border-top: 1px solid #ebebeb;
        padding-top: 6px;
    }
</style>
<script type="text/javascript">

    function OpenOpinion() {
        var strUrl = $("#cphInput_wfControl_hiddUrl").val() || $("#wfControl_hiddUrl").val(); //hdnURL
        
        $(document).ready(function () { $.ligerDialog.open({ url: strUrl, width: 500, height: 400, modal: false }); });
    }
    function OpenFileList() {
        var strUrl = $("#cphInput_wfControl_hiddFile").val() || $("#wfControl_hiddFile").val(); //hiddFile

        $(document).ready(function () { $.ligerDialog.open({ url: strUrl, width: 500, height: 300, modal: false }); });
    }
    /* Start 胡方扬 2013-02-23  原因：实现用户选择跳转环节，根据跳转环节更新环节人员*/
    $(document).ready(function () {
        var hidSelectUserId = $("#wfControl_chkbxlstSelectUsers option:selected").val() || $("#cphInput_wfControl_chkbxlstSelectUsers option:selected").val();

        $("#wfControl_hidSelectUserId").val(hidSelectUserId);
        $("#cphInput_wfControl_hidSelectUserId").val(hidSelectUserId);
        var TempUserArr = [];
        //绑定环节选择事件
        $("#cphInput_wfControl_STEP_NAME").bind("change", function () {
            change_event();
        })
        $("#wfControl_STEP_NAME").bind("change", function () {
            change_event();
        })
        $("#wfControl_chkbxlstSelectUsers").bind("change", function () {
            changeUser_event();
        })
        $("#cphInput_wfControl_chkbxlstSelectUsers").bind("change", function () {
            changeUser_event();
        })
        function setUserListDropListValue(strurl, strUserArr) {
            TempUserArr = [];
            if (strUserArr.length < 0) {
                return;
            } else {
                for (var i = 0; i < strUserArr.length; i++) {
                    getUserInfor(strurl, strUserArr[i]);
                }
                CreatOptionItems(TempUserArr);
            }
        }

        function getUserInfor(strurl, strUserID) {
            $.ajax({
                cache: false,
                async: false, //设置是否为异步加载,此处必须
                type: "POST",
                url: "" + strurl + "?action=getUserInfor&strUserId=" + strUserID,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data != null) {
                        if (data.Rows.length > 0) {
                            TempUserArr.push(data.Rows[0]);
                        }
                    }
                },
                error: function (msg) {
                    return;
                }
            });
        }

        function CreatOptionItems(strArryList) {
            //清除原有的用户数据
            $("#wfControl_chkbxlstSelectUsers option").each(function () {
                $(this).remove();
            })
            $("#cphInput_wfControl_chkbxlstSelectUsers option").each(function () {
                $(this).remove();
            })
            //添加新数据
            for (var i = 0; i < strArryList.length; i++) {
                $("#wfControl_chkbxlstSelectUsers").append($("<option value='" + strArryList[i].ID + "'>" + strArryList[i].REAL_NAME + "</option>"));
                $("#cphInput_wfControl_chkbxlstSelectUsers").append($("<option value='" + strArryList[i].ID + "'>" + strArryList[i].REAL_NAME + "</option>"));
            }
            hidSelectUserId = $("#wfControl_chkbxlstSelectUsers option:selected").val() || $("#cphInput_wfControl_chkbxlstSelectUsers option:selected").val();
            $("#wfControl_hidSelectUserId").val(hidSelectUserId);
            $("#cphInput_wfControl_hidSelectUserId").val(hidSelectUserId);
        }

        function change_event() {
            var strWfId = $("#cphInput_wfControl_hiddWF_ID").val() || $("#wfControl_hiddWF_ID").val();
            var strSetpValue = $("#cphInput_wfControl_STEP_NAME option:selected").val() || $("#wfControl_STEP_NAME option:selected").val();
            var strAjaxUr = $("#cphInput_wfControl_hiddAjaxUrl").val() || $("#wfControl_hiddAjaxUrl").val();
            GetUserListWF(strAjaxUr, strWfId, strSetpValue);
        }

        function changeUser_event() {
            hidSelectUserId = $("#wfControl_chkbxlstSelectUsers option:selected").val() || $("#cphInput_wfControl_chkbxlstSelectUsers option:selected").val();
            $("#wfControl_hidSelectUserId").val(hidSelectUserId);
            $("#cphInput_wfControl_hidSelectUserId").val(hidSelectUserId);
        }
        function GetUserListWF(strurl, wfid, setpValue) {
            $.ajax({
                cache: false,
                async: false, //设置是否为异步加载,此处必须
                type: "POST",
                url: "" + strurl + "?action=SelectedChange&strWfId=" + wfid + "&strSetpValue=" + setpValue,
                contentType: "application/text; charset=utf-8",
                dataType: "text",
                success: function (data) {
                    if (data != "") {
                        var strUserList = data.split('|');
                        setUserListDropListValue(strurl, strUserList);
                    }
                },
                error: function (msg) {
                    $.ligerDialog.warn('Ajax加载数据失败！' + msg);
                }
            });
        }
        //如果存在评论信息则改变“更多评论”的显示颜色 Add By：weilin
        if ($("#wfControl_hidColor").val() != "" && $("#wfControl_hidColor").val() != undefined)
            $("#aOpinion")[0].style.color = $("#wfControl_hidColor").val();
    })
    /* End*/
</script>
<div style="width: 100%;" class="l-form">
    <div style="display: block; clear: both; padding-top: 10px; padding-left: 7px;">
        <div>

            <div id="dvToOpUsers" runat="server" visible="true" style="float: left; display: inline;
                overflow: hidden; margin-right: 15px; margin-bottom: 8px;">
                <table>
                    <tr>
                        <td class="l-table-edit-td">
                            下环节操作人：
                        </td>
                        <td class="l-table-edit-td">
                                    <asp:DropDownList ID="chkbxlstSelectUsers" runat="server" 
                                        CssClass="l-text l-text-editing divform" RepeatColumns="10">
                                    </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div runat="server" visible="false" id="dvOpUsers" style="float: left; display: inline;
                overflow: hidden; margin-right: 15px; margin-bottom: 8px;">
                <table>
                    <tr>
                        <td class="l-table-edit-td">
                            指定选择下环节操作者：
                        </td>
                        <td>
                            <asp:DropDownList ID="chkbxlstAllUsers" CssClass="l-text l-text-editing" RepeatColumns="10"
                                runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="dvGoToStep" visible="false" runat="server" style="float: left; display: inline;
            overflow: hidden; margin-bottom: 8px;">
            <table>
                <tr>
                    <td>
                        请选择环节：
                    </td>
                    <td class="l-table-edit-td">
                        <asp:DropDownList ID="STEP_NAME" runat="server" 
                            CssClass="l-text l-text-editing" >
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div id="dvUpLoad" visible="false" runat="server" style="clear: both; display: block;
            overflow: hidden; margin-right: 10px;">
            <table>
                <tr>
                    <td class="l-table-edit-td">
                        文件路径：
                    </td>
                    <td class="l-table-edit-td">
                        <asp:FileUpload ID="FileUpload1" CssClass="l-text l-text-editing" Width="200px" runat="server" />
                    </td>
                    <td class="l-table-edit-td">
                        <asp:Button ID="btnUpLoading" runat="server" CssClass="l-button l-button-submit"
                            Text="上传" OnClick="btnUpLoading_Click" />
                    </td>
                    <td class="l-table-edit-td" style="padding-left: 8px;">
                        <a href="#" onclick="OpenFileList();">更多附件</a>
                        <asp:HiddenField ID="UpLoadFileName" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div runat="server" visible="false" id="dvOpinions" style="margin: 7px;">
        <table width="100%" style="margin: 8px 0;">
            <tr>
                <td class="l-table-edit-td" style="width: 60px; vertical-align: top; text-align: right;">
                    评论意见：
                </td>
                <td class="l-table-edit-td" style="text-align: left;">
                    <asp:TextBox ID="txtOpinionText" runat="server" Rows="3" CssClass="l-text l-text-editing"
                        TextMode="MultiLine" Width="80%" Height="36"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td class="l-table-edit-td" style="text-align: left;">
                    <a id="aOpinion" onclick="OpenOpinion();" href="#">更多评论</a>
                    <asp:HiddenField ID="hidColor" runat="server" />
                    <%--<asp:HiddenField ID="hdnURL" runat="server" />--%>
                </td>
            </tr>
        </table>
    </div>
    <div class="l-form listfooter2012">
        <asp:Button ID="btnSave" runat="server" Visible="false" Text="保存" CssClass="l-button l-button-submit"
            OnClick="btnSave_Click" OnClientClick="return Save();" />
        <asp:Button ID="btnSend" runat="server" Visible="false" CssClass="l-button l-button-submit"
            Text="发送" OnClick="btnSend_Click" OnClientClick="return SendSave();" />
        <asp:Button ID="btnCallBack" runat="server" Visible="false" CssClass="l-button l-button-submit"
            Text="回收" OnClick="btnCallBack_Click" />
        <asp:Button ID="btnZSend" runat="server" Visible="false" CssClass="l-button l-button-submit"
            Text="转发" OnClick="btnZSend_Click" />
        <asp:Button ID="btnBack" runat="server" Visible="false" CssClass="l-button l-button-submit"
            Text="退回" OnClick="btnBack_Click"  OnClientClick="return BackSend();" />
        <asp:Button ID="btnHold" runat="server" Visible="false" CssClass="l-button l-button-submit"
            Text="挂起" OnClick="btnHold_Click" />
        <asp:Button ID="btnReLoad" runat="server" Visible="false" CssClass="l-button l-button-submit"
            Text="恢复" OnClick="btnReLoad_Click" />
        <asp:Button ID="btnPause" runat="server" Visible="false" CssClass="l-button l-button-submit"
            Text="暂停" OnClick="btnPause_Click" />
        <asp:Button ID="btnReStart" runat="server" Visible="false" CssClass="l-button l-button-submit"
            Text="返元" OnClick="btnReStart_Click" />
        <asp:Button ID="btnKill" runat="server" Visible="false" CssClass="l-button l-button-submit"
            Text="销毁" OnClick="btnKill_Click" />
    </div>
    <input type="hidden" id="hiddUrl" name="hiddUrl" runat="server" />
    <input type="hidden" id="hiddFile" name="hiddFile" runat="server" />
    <input type="hidden" id="hiddWF_ID" name="hiddWF_ID" runat="server" />
        <input type="hidden" id="hiddAjaxUrl" name="hiddAjaxUrl" runat="server" />
              <input type="hidden" id="hidSelectUserId" name="hidSelectUserId" runat="server" />
</div>
