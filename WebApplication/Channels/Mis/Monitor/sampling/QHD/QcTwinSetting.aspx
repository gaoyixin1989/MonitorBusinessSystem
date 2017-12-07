<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Monitor_sampling_QHD_QcTwinSetting" Codebehind="QcTwinSetting.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css"
        rel="stylesheet" type="text/css" />
    <script src="../../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="../../../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script type="text/javascript">
        var strUrl = "QcTwinSetting.aspx";

        $(document).ready(function () {
            //往列表中加载数据
            GetSampleItemToList($("#txtSampleId").val(), "#listAll", "0");
            GetSampleItemToList($("#txtSampleId").val(), "#listSelected", $("#txtQcType").val());

            $("#btnLeft").click(function () {
                $("#listAll :selected").appendTo($("#listSelected"));
                $("#listAll :selected").remove();
            });
            $("#btnRight").click(function () {
                $("#listSelected :selected").appendTo($("#listAll"));
                $("#listSelected :selected").remove();
            });
        });

        //将获取的质控数据填充到列表中
        function GetSampleItemToList(strSampleID, strControlName, strQcType) {
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "/GetSampleItem",
                data: "{'strSampleID':'" + strSampleID + "','strQcType':'" + strQcType + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != null) {
                        vItemData = data.d;
                    }
                    else {
                        $.ligerDialog.warn('获取数据失败！');
                    }
                },
                error: function () {
                    $.ligerDialog.warn('Ajax加载数据失败！');
                }
            });
            //bind data
            var vlist = "";
            //遍历json数据,获取监测项目列表
            jQuery.each(vItemData, function (i, n) {
                vlist += "<option value=" + vItemData[i].ID + ">" + vItemData[i].ITEM_NAME + "</option>";
            });
            //绑定数据到listLeft
            $(strControlName).append(vlist);
        }
        //质控数据保存
        function QcSave() {
            var sum = "";
            var spit = "";
            $("#listSelected option").each(function () {
                sum = sum + spit + $(this).val();
                spit = ",";
            });
            $("#txtItemId").val(sum);
            var strValue = "0";
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "/QcSave",
                data: "{'strSampleID':'" + $("#txtSampleId").val() + "','strQcType':'" + $("#txtQcType").val() + "','strItemId':'" + sum + "','strQcCount':'" + $("#dropQcCount").val() + "','iSampleCode':'" + $("#iSampleCode").val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.d == "1")
                        strValue = data.d;
                }
            });
            return strValue;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!--质控监测项目设置-->
    <div class="l-form" style="text-align: center">
        <div style="float: left;" class="l-group l-group-hasicon">
            <img src="../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" /><span>平行设置
        </div>
        <ul>
            <li>
                <table width="400px">
                    <tr>
                        <td align="center">
                            <b>所有监测项目</b><br />
                            <select size="20" name="listAll" multiple="multiple" id="listAll" style="width: 180px;
                                height: 240px">
                            </select>
                        </td>
                        <td align="center">
                            <table>
                                <tr>
                                    <td>
                                        <input type="button" id="btnLeft" name="btnLeft" value=">>" class="l-button l-button-submit" /><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="button" id="btnRight" name="btnRight" value="<<" class="l-button l-button-submit" /><br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center">
                            <b>已选监测项目</b> 质控数量：<asp:DropDownList ID="dropQcCount" runat="server">
                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <select size="20" name="listSelected" id="listSelected" multiple="multiple" style="width: 180px;
                                height: 235px;">
                            </select>
                        </td>
                    </tr>
                </table>
            </li>
        </ul>
    </div>
    <asp:HiddenField ID="txtQcType" runat="server" />
    <asp:HiddenField ID="txtSampleId" runat="server" />
    <asp:HiddenField ID="iSampleCode" runat="server" />
    <input id="txtItemId" type="text" style="visibility: hidden;" />
    </form>
</body>
</html>