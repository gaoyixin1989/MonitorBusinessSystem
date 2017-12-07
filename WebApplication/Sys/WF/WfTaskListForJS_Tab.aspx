﻿<%@ Page Language="C#" MasterPageFile="~/Masters/SearchEx.master" AutoEventWireup="True" Inherits="Sys_WF_WfTaskListForJS_Tab" Codebehind="WfTaskListForJS_Tab.aspx.cs" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>

    <script type="text/javascript">
        var tab;

        //窗口改变时的处理函数
        function f_heightChanged(options) {
            if (tab)
                tab.addHeight(options.diff);
        }

        $(document).ready(function () {
            $("#layoutxxx").ligerLayout({ height: '100%', heightDiff: -3, onHeightChanged: f_heightChanged });

            var bodyHeight = $(".l-layout-center:first").height();
            //Tab
            tab = $("#navtab1").ligerTab({ height: bodyHeight, contextmenu: true, onAfterSelectTabItem: function (tabid) {
                tab.reload(tabid);
            }
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cphData" runat="Server">
    <div id="layoutxxx" style="text-align:left">
        <div position="center"  title="">
            <div id="navtab1" style="overflow:hidden;  " position="center" >
                <div tabid="home" title="已确认" lselected="true" >
                    <iframe frameborder="0" name="showmessage" src="WfTaskListForJS.aspx?isQueding=1"></iframe>
                </div>
                <div  title="未确认"   >
                   <iframe frameborder="0" name="showmessage" src="WfTaskListCFMForJS.aspx"></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>