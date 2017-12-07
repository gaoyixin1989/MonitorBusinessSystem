<%@ Page Language="C#" AutoEventWireup="True" Inherits="Sys_General_UserProxySet" Codebehind="UserProxySet.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"  type="text/css" />

    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    
    <script src="../../Scripts/comm.js" type="text/javascript"></script>

    <script type="text/javascript">
        var groupicon = "../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
        var strIsAdmin;

        $(document).ready(function () {
            strIsAdmin = request('IsAdmin');
            if (!strIsAdmin)
                strIsAdmin = "";

            if (strIsAdmin == "") {
                //创建被代理人信息  本人选择代理，被代理人就是本人
                $("#divUser").ligerForm({
                    inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right',
                    fields: [
                { display: "被代理人", name: "UserName", newline: true, type: "text", disabled: true, group: "被代理人信息", groupicon: groupicon },
                { display: "代理人", name: "ProxyID", newline: true, type: "select", group: "代理人信息", groupicon: groupicon }
                ]
                });

                $("#ProxyID").ligerComboBox({
                    onBeforeOpen: Proxy_select, valueFieldID: 'hidProxyID'
                });

                $("#UserName").val($("#hidUserName").val());
                $("#hidUserID").val($("#hidUserID1").val());
                $("#UserName").ligerTextBox().setDisabled();

                var strProxy = getProxyOfUser($("#hidUserID").val());
                $("#hidProxyID").val(strProxy.split('|')[0]);
                $("#ProxyID").val(strProxy.split('|')[1]);
            }
            else {
                //创建被代理人信息 管理员强制代理，被代理人可选
                $("#divUser").ligerForm({
                    inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right',
                    fields: [
                    { display: "被代理人", name: "selUserName", newline: true, type: "select", group: "被代理人信息", groupicon: groupicon },
                    { display: "代理人", name: "ProxyID", newline: true, type: "select", group: "代理人信息", groupicon: groupicon }
                    ]
                });

                $("#selUserName").ligerComboBox({
                    onBeforeOpen: User_select, valueFieldID: 'hidUserID'
                });

                $("#ProxyID").ligerComboBox({
                    onBeforeOpen: Proxy_select, valueFieldID: 'hidProxyID'
                });
            }
        });

        //cancel按钮
        function selectCancel(item, dialog) {
            dialog.close();
        }

        //弹出被代理人选择页面
        function User_select() {
            $.ligerDialog.open({ title: '选择被代理人', name: 'winselector', width: 700, height: 370, url: 'SelUser.aspx?hidUserID=' + $("#hidUserID").val(),
                buttons: [
                { text: '确定', onclick: User_selectOK },
                { text: '取消', onclick: selectCancel }
            ]
            });
            return false;
        }

        //被代理人选择页面 ok按钮
        function User_selectOK(item, dialog) {
            var fn = dialog.frame.f_select || dialog.frame.window.f_select;
            var data = fn();
            if (!data) {
                $.ligerDialog.warn('请选择被代理人！');
                return;
            }
            $("#selUserName").val(data.REAL_NAME);
            $("#hidUserID").val(data.ID);
            dialog.close();

            $("#hidProxyID").val("");
            $("#ProxyID").val("");
            if ($("#hidProxyID").val().length > 0 && $("#hidUserID").val().length) {
                SaveProxy();
            }
            else {
                var strProxy = getProxyOfUser($("#hidUserID").val());
                $("#hidProxyID").val(strProxy.split('|')[0]);
                $("#ProxyID").val(strProxy.split('|')[1]);
            }
        }

        //弹出代理人选择页面
        function Proxy_select() {
            $.ligerDialog.open({ title: '选择代理人', name: 'winselector', width: 700, height: 370, url: 'SelUser.aspx?hidUserID=' + $("#hidUserID").val(),
                buttons: [
                { text: '确定', onclick: Proxy_selectOK },
                { text: '取消', onclick: selectCancel }
            ]
            });
            return false;
        }

        //代理人选择页面 ok按钮
        function Proxy_selectOK(item, dialog) {
            var fn = dialog.frame.f_select || dialog.frame.window.f_select;
            var data = fn();
            if (!data) {
                $.ligerDialog.warn('请选择代理人！');
                return;
            }
            $("#ProxyID").val(data.REAL_NAME);
            $("#hidProxyID").val(data.ID);
            dialog.close();

            if ($("#hidProxyID").val().length > 0 && $("#hidUserID").val().length) {
                SaveProxy();
            }
        }

        function SaveProxy() {
            $.ajax({
                cache: false,
                type: "POST",
                url: "UserProxySet.aspx/SaveData",
                data: "{'strUserID':'" + $("#hidUserID").val() + "','strProxyID':'" + $("#hidProxyID").val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.d == "1") {
                    }
                    else {
                        $.ligerDialog.warn('数据保存失败');
                    }
                }
            });
        }

        //获取指定用户的代理人
        function getProxyOfUser(strUserID) {
            var strValue = "";
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: "UserProxySet.aspx/getProxyOfUser",
                data: "{'strUserID':'" + strUserID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    strValue = data.d;
                }
            });
            return strValue;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divUser"></div>
        <div id="detail" style="display:none;"><form id="editform" method="post"></form> </div>
        
        <input type="hidden" id="hidUserID" name="hidUserID"  />
        <input type="hidden" id="hidProxyID" name="hidProxyID"  />
        
        <input type="hidden" id="hidUserID1" name="hidUserID1" runat="server"  />
        <input type="hidden" id="hidUserName" name="hidUserName" runat="server"  />
    </form>
</body>
</html>
