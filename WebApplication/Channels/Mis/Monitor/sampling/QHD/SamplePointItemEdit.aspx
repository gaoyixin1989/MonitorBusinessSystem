<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="Channels_Mis_Monitor_sampling_QHD_SamplePointItemEdit" Codebehind="SamplePointItemEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css"
        rel="stylesheet" type="text/css" />
    <script src="../../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function exchange(object, listLeft, listRight, txtHidden) {
            var option;
            var sum = '';
            var left = $("#" + listLeft)[0];
            var right = $("#" + listRight)[0];

            if (object.id == "btn1") {
                for (var i = 0; i < left.length; i++) {
                    if (left[i].selected) {
                        option = left.removeChild(left[i--]);
                        right.appendChild(option);
                    }
                }
            }
            else {
                for (var i = 0; i < right.length; i++) {
                    if (right[i].selected) {
                        option = right.removeChild(right[i--]);
                        left.appendChild(option);
                    }
                }
            }

            for (var i = 0; i < right.length; i++) {
                sum = sum + right[i].value + ",";
            }

            if (sum.length > 0) sum = sum.substr(0, sum.length - 1);
            $("#" + txtHidden).val(sum);

            return false;
        }

        function allItemSrh_TextChanged() {
            for (var i = 0; i < $("#ListBox1")[0].length; i++) {
                $("#ListBox1")[0][i].selected = false;
            }
            for (var i = 0; i < $("#ListBox1")[0].length; i++) {
                if ($("#ListBox1")[0][i].text.toLowerCase().indexOf($("#allItemSrh").val().toLowerCase()) >= 0) {
                    $("#ListBox1")[0][i].selected = true;
                    break;
                }
            }
            return false;
        }

        //得到保存信息
        function getSaveDate() {
            var strData = "";
            strData += "'strSample':'" + request("PointID") + "',";
            strData += "'strSelItem_IDs':'" + $("#txtdel").val() + "'";
            strData += "";

            return strData;
        }

        //获取request
        function request(strParame) {
            var args = new Object();
            var query = location.search.substring(1);

            var pairs = query.split("&"); // Break at ampersand 
            for (var i = 0; i < pairs.length; i++) {
                var pos = pairs[i].indexOf('=');
                if (pos == -1) continue;
                var argname = pairs[i].substring(0, pos);
                var value = pairs[i].substring(pos + 1);
                value = decodeURIComponent(value);
                args[argname] = value;
            }
            return args[strParame];
        } 
    </script>
    <style type="text/css">
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
        .inputdiv .l-text-wrapper
        {
            float: right;
            display: inline;
            overflow: hidden;
            clear: right;
            margin-top: -2px;
            margin-bottom: 4px;
        }
        
        .l-table-edit-td .l-text
        {
            width: 152px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="l-form">
        <div class="l-group l-group-hasicon">
            <img src="../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />
            <span>
                <asp:Label ID="POINT_IDs" runat="server"></asp:Label></span>
        </div>
        <table cellpadding="0" cellspacing="0" class="tabletool">
            <tr>
                <td class="l-table-edit-td" style="width: 182px; padding-left: 12px;">
                    <span style="display: inline-block; padding-right: 4px;">检索</span><input id="allItemSrh"
                        clientidmode="Static" class="l-text l-text-editing" onkeyup="javascript:allItemSrh_TextChanged();"
                        type="text" />
                    <b style="width: 180px; margin-top: 8px; display: inline-block; margin-left: 0;">所有监测项目</b>
                    <asp:ListBox ID="ListBox1" ClientIDMode="Static" runat="server" Width="182px" Height="200px"
                        SelectionMode="Multiple" Style="border: 1px solid #a0e1ff"></asp:ListBox>
                </td>
                <td align="center" class="l-table-edit-td">
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btn1" ClientIDMode="Static" runat="server" CssClass="l-button l-button-submit"
                        Text=">>" OnClientClick="return exchange(this,'ListBox1','ListBox2','txtdel');" /><br />
                    <asp:Button ID="btn2" ClientIDMode="Static" runat="server" CssClass="l-button l-button-submit"
                        Text="<<" OnClientClick="return exchange(this,'ListBox1','ListBox2','txtdel');" />
                </td>
                <td align="center" class="l-table-edit-td">
                    &nbsp;
                </td>
                <td align="left" class="inputdiv" style="padding-top: 30px;">
                    <b style="width: 180px; clear: both; margin-top: 4px; overflow: hidden;">已选监测项目</b>
                    <asp:ListBox ID="ListBox2" ClientIDMode="Static" runat="server" Width="182px" Height="200px"
                        Style="border: 1px solid #a0e1ff" SelectionMode="Multiple"></asp:ListBox>
                </td>
            </tr>
        </table>
    </div>
    <input id="txtdel" clientidmode="Static" runat="server" type="hidden" />
    </form>
</body>
</html>
