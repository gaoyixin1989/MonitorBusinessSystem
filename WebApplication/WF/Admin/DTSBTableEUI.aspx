<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DTSBTableEUI.aspx.cs" Inherits="CCFlow.WF.Admin.DTSBTableEUI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Scripts/easyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../Scripts/CommonUnite.js" type="text/javascript"></script>
    <script type="text/javascript">
        var FK_Flow;
        //检查数据
        function checkData() {
            FK_Flow = Application.common.getArgsFromHref("FK_Flow");
            var params = {
                method: "checkData",
                FK_Flow: FK_Flow
            };
            queryData(params, checkDataBack, this);
        }
        function checkDataBack(js, scorp) {
            if (js.length != 0) {
                $.messager.alert("提示", js, "info");
                return;
            }
            loadData(1, 20);
        }

        function loadData(pageNumber, pageSize) {
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;

            var params = {
                method: "loadData",
                FK_Flow: FK_Flow,
                pageNumber: pageNumber,
                pageSize: pageSize
            };
            queryData(params, loadDataBack, this);
        }
        function loadDataBack(js, scorp) {
            $("#pageloading").hide();
            if (js == "") js = "[]";

            if (js.status && js.status == 500) {
                $("body").html("<b>访问页面出错，请联系管理员。<b>");
                return;
            }

            var pushData = eval('(' + js + ')');

            $('#leftTable').datagrid({
                columns: [[
                    {checkbox:true},
                    { field: 'ZD', title: '参训方队', width: 100, align: 'left' },
                    { field: 'ZDMC', title: '训练日期', width: 60, align: 'center' },
                    { field: 'LX', title: '人员类别', width: 60, align: 'center' }
                ]],
                idField: 'ZD',
                selectOnCheck: false,
                checkOnSelect: true,
                singleSelect: true,
                data: pushData,
                width: 'auto',
                height: 'auto',
                striped: true,
                rownumbers: true,
                pagination: true,
                remoteSort: false,
                fitColumns: true,
                pageNumber: scorp.pageNumber,
                pageSize: scorp.pageSize,
                pageList: [20, 30, 40, 50],
                onDblClickCell: function (index, field, value) {
                },
                loadMsg: '数据加载中......'
            });
            //分页
            var pg = $("#tableD").datagrid("getPager");
            if (pg) {
                $(pg).pagination({
                    onRefresh: function (pageNumber, pageSize) {
                        isAgain = false;
                        loadData(pageNumber, pageSize);
                    },
                    onSelectPage: function (pageNumber, pageSize) {
                        isAgain = false;
                        loadData(pageNumber, pageSize);
                    }
                });
            }
        }
        $(function () {
            checkData();
        });
        function queryData(param, callback, scope, method, showErrMsg) {
            if (!method) method = 'GET';
            $.ajax({
                type: method,
                dataType: "text",
                contentType: "application/json; charset=utf-8",
                url: "DTSBTableEUI.aspx",
                data: param,
                async: false,
                cache: false,
                complete: function () { },
                error: function (XMLHttpRequest, errorThrown) {
                    callback(XMLHttpRequest);
                },
                success: function (msg) {
                    var data = msg;
                    callback(data, scope);
                }
            });
        }

        //全屏
        var x = window.screen.availWidth;
        var y = window.screen.availHeight;
        function openUrl() {
            var newFrm = window.open("AssessResult.aspx?FD=" + FD + "&", "newForm", "menubar=0,toolbar=0,left=0px,top=0px,height=" + y + "px," + "width=" + x + "px");
        }
    </script>
</head>
<body class="easyui-layout">
    <div data-options="region:'west',title:'West',border:false" style="width: 300px;">
        <table id="leftTable" data-options="fit:true,fitcolumns:false" toolbar='#tb' class="easyui-datagrid">
        </table>
    </div>
    <div data-options="region:'center',title:'center title',border:false" style="background: #eee;">
        <table id="rightTable" data-options="fit:true,fitcolumns:false" toolbar='#tb' class="easyui-datagrid">
        </table>
    </div>
</body>
</html>
