<%@ Page Language="C#" MasterPageFile="~/Masters/SearchEx.master" AutoEventWireup="True" Inherits="Channels_Statistice_QCAccount" Codebehind="QCAccount.aspx.cs" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/style.css" rel="stylesheet"
        type="text/css" />
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenuBar.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/draggable.js" type="text/javascript"></script>
    <script src="QCAccount.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#navtab1").ligerTab({ contextmenu: false });
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphData" runat="Server">
    <form id="form1" runat="server">
    <div id="mainsearch" style="width: 98%">
        <div>
            <div id="divQCSrh" class="l-form">
            </div>
            <table>
                <tr>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td>
                        <div id="btnQCSrh"></div>
                    </td>
                    <td>
                        <div id="btnQCExcel"></div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="navtab1" style="width: 98%; overflow: hidden; border: 1px solid #A3C0E8;">
        <div tabid="home" title="质量控制结果统计表" lselected="true" style="height: 90%">
            <div id="grid_QCAll">
            </div>
        </div>
        <div title="现场空白">
            <div id="grid_QCOutEmpty">
            </div>
        </div>
        <div title="实验室空白">
            <div id="grid_QCInEmpty">
            </div>
        </div>
        <div title="现场密码平行">
            <div id="grid_QCOutTwin">
            </div>
        </div>
        <div title="实验室明码平行">
            <div id="grid_QCInTwin">
            </div>
        </div>
        <div title="实验室密码平行">
            <div id="grid_QCPwdTwin">
            </div>
        </div>
        <div title="加标回收">
            <div id="grid_QCAdd">
            </div>
        </div>
        <div title="标样">
            <div id="grid_QCSt">
            </div>
        </div>
    </div>
    <asp:Button ID="btnImport" ClientIDMode="Static" runat="server" OnClick="btnImport_Click"
        Style="display: none;" />
    <input type="hidden" clientidmode="Static" id="hdQC_BEGIN_DATE" runat="server" />
    <input type="hidden" clientidmode="Static" id="hdQC_END_DATE" runat="server" />
    <input type="hidden" clientidmode="Static" id="hdMONITOR_ID" runat="server" />
    </form>
</asp:Content>
