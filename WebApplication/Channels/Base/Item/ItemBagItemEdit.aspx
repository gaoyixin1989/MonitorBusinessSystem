<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Base_Item_ItemBagItemEdit" Codebehind="ItemBagItemEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"  type="text/css" />
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>

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
            var strData = "{";
            strData += "'strBagID':'" + request("selBagID") + "',";
            strData += "'strSelItem_IDs':'" + $("#txtdel").val() + "'";
            strData += "}";

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
</head>
<body>
    <form id="form1" runat="server">
    <div class="l-form">
        <div class="l-group l-group-hasicon">
            <img src="../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />
            <span><asp:Label ID="POINT_IDs" runat="server"></asp:Label></span>
        </div>
        <ul>
            <li style="width:240px;text-align:left;">
                所有的监测项目：<input id="allItemSrh" ClientIDMode="Static" style="width:100px;" class="InputA"  onkeyup="javascript:allItemSrh_TextChanged();" type="text" />
            </li>
            <li style="width:120px;text-align:left;">
            </li>
            <li style="width:120px;text-align:left;">
                已选择的监测项目：
            </li>
        </ul>
        <ul>
            <li>
                <table class="tblMain">
                    <tr>
                        <td class="w01"  align="center" width="45%">
                             <asp:ListBox ID="ListBox1"  ClientIDMode="Static" runat="server" Width="200px" Height="200px"  SelectionMode="Multiple"></asp:ListBox>
                        </td>
                        <td align="center" width="10%">
                          <asp:Button ID="btn1" ClientIDMode="Static" runat="server" CssClass="sAdd" Text=">>" OnClientClick="return exchange(this,'ListBox1','ListBox2','txtdel');" />
                            <br />
                            <br />
                            <br />
                            <br />
                          <asp:Button ID="btn2" ClientIDMode="Static" runat="server" CssClass="sAdd" Text="<<" OnClientClick="return exchange(this,'ListBox1','ListBox2','txtdel');"/>
                        </td>
                        <td class="w01"  align="center" width="45%">
                        <asp:ListBox ID="ListBox2" ClientIDMode="Static" runat="server" Width="200px" Height="200px" SelectionMode="Multiple"></asp:ListBox>
                         </td>
                    </tr>
                 </table>
            </li>
        </ul>
    </div>
    
     <input id="txtdel" ClientIDMode="Static" runat="server" type="hidden" /> 
    </form>
</body>
</html>
