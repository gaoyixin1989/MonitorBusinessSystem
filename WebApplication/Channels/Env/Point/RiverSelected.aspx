<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Env_Point_RiverSelected" Codebehind="RiverSelected.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../../../Controls/zTree3.4/js/jquery-1.4.4.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#oneList").change(function () {
                $("#twoList").empty();
                $("#threeList").empty();
                $("#fourList").empty();
                $("#fiveList").empty();
                var arr = getData($('#oneList option:selected').val());
                for (var i = 0; i < arr.length; i++) {
                    $("#twoList").append("<option value='" + arr[i]["ID"] + "'>" + arr[i]["DICT_TEXT"] + "</option>");
                }
                $("#txtMainValue").val($('#oneList option:selected').val());
                $("#txtValue").val($('#oneList option:selected').val());
                $("#txtSelectedName").text($('#oneList option:selected').text());
            });

            $("#twoList").change(function () {
                $("#threeList").empty();
                $("#fourList").empty();
                $("#fiveList").empty();
                var arr = getData($('#twoList option:selected').val());
                for (var i = 0; i < arr.length; i++) {
                    $("#threeList").append("<option value='" + arr[i]["ID"] + "'>" + arr[i]["DICT_TEXT"] + "</option>");
                }
                $("#txtValue").val($('#twoList option:selected').val());
                $("#txtSelectedName").text($('#twoList option:selected').text());
            });

            $("#threeList").change(function () {
                $("#fourList").empty();
                $("#fiveList").empty();
                var arr = getData($('#threeList option:selected').val());
                for (var i = 0; i < arr.length; i++) {
                    $("#fourList").append("<option value='" + arr[i]["ID"] + "'>" + arr[i]["DICT_TEXT"] + "</option>");
                }
                $("#txtValue").val($('#threeList option:selected').val());
                $("#txtSelectedName").text($('#threeList option:selected').text());
            });

            $("#fourList").change(function () {
                $("#fiveList").empty();
                var arr = getData($('#fourList option:selected').val());
                for (var i = 0; i < arr.length; i++) {
                    $("#fiveList").append("<option value='" + arr[i]["ID"] + "'>" + arr[i]["DICT_TEXT"] + "</option>");
                }
                $("#txtValue").val($('#fourList option:selected').val());
                $("#txtSelectedName").text($('#fourList option:selected').text());
            });

            $("#fiveList").change(function () {
                $("#txtValue").val($('#fiveList option:selected').val());
                $("#txtSelectedName").text($('#fiveList option:selected').text());
            });
            function getData(strParentCode) {
                var arr = [];
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: "RiverSelected.aspx?type=loadData&PARENT_CODE=" + strParentCode,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        arr = data;
                    }
                });
                return arr;
            }
        });
        function getRiverInfo() {
            var arr = [];
            arr["WatershedValue"] = $('#oneList option:selected').val();
            arr["WatershedText"] = $('#oneList option:selected').text();
            arr["RiverValue"] = $("#txtValue").val();
            arr["RiverText"] = $("#txtSelectedName").text();
            return arr;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td align="center" width="20%">
                流域<br />
                <asp:ListBox ID="oneList" runat="server" Width="100%" Height="200px"></asp:ListBox>
            </td>
            <td align="center" width="20%">
                干流<br />
                <asp:ListBox ID="twoList" runat="server" Width="100%" Height="200px"></asp:ListBox>
            </td>
            <td align="center" width="20%">
                一级支流<br />
                <asp:ListBox ID="threeList" runat="server" Width="100%" Height="200px"></asp:ListBox>
            </td>
            <td align="center" width="20%">
                二级支流<br />
                <asp:ListBox ID="fourList" runat="server" Width="100%" Height="200px"></asp:ListBox>
            </td>
            <td align="center" width="20%">
                其它河流<br />
                <asp:ListBox ID="fiveList" runat="server" Width="100%" Height="200px"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="5">
                <span id="txtSelectedName"></span>
            </td>
        </tr>
    </table>
    <input type="hidden" id="txtMainValue" />
    <input type="hidden" id="txtValue" />
    </form>
</body>
</html>
