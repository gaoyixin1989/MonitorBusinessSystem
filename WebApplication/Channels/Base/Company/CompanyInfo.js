// Create by 熊卫华 2012.11.05  "企业信息管理"功能
// update by 潘德军 2012.11.08 增加监测点位按钮，并跳转到监测点位页面

var objCompanyInfoGrid = null;
var strCompanyInfoId = "";

//企业信息管理功能
$(document).ready(function () {
    $("#layout1").ligerLayout({ height: '100%' });

    //菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: updateData, icon: 'modify' },
            { id: 'menudel', text: '删除', click: deleteData, icon: 'delete' },
            { line: true },
            { id: 'menudel', text: '监测点位', click: showPoint, icon: 'database' }
            ]
    });

    objCompanyInfoGrid = $("#companyInfoInfoGrid").ligerGrid({
        title: '企业信息',
        dataAction: 'server',
        usePager: true,
        pageSize: 15,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        whenRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [10, 15, 20, 50],
        height: '100%',
        url: 'CompanyInfo.aspx?type=getCompanyInfo',
        columns: [
                { display: '企业名称', name: 'COMPANY_NAME', align: 'left', width: 200, minWidth: 60 },
                { display: '所在区域', name: 'AREA', minWidth: 250, render: function (record) {
                    return getDictName(record.AREA, 'administrative_area');
                }
                },
                { display: '行业类别', name: 'INDUSTRY', minWidth: 140, render: function (record) {
                    return getIndustryName(record.INDUSTRY);
                }
                },
                { display: '联系人', name: 'CONTACT_NAME', minWidth: 250 }
                ],
        toolbar: { items: [
                { text: '增加', click: createData, icon: 'add' },
                { line: true },
                { text: '修改', click: updateData, icon: 'modify' },
                { line: true },
                { text: '删除', click: deleteData, icon: 'delete' },
                 { line: true },
                { text: '查询', click: showDetailSrh, icon: 'search' },
                { line: true },
                { text: '监测点位', click: showPoint, icon: 'database' },
                { line: true },
                { text: '行业类别设置', click: toIndustry, icon: 'settings' },
                { line: true },
                { text: '企业资料导入', click: ExcelImport, icon: 'settings' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            menu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            updateData();
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            strCompanyInfoId = rowdata.ID;
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //编辑监测点位
    function showPoint() {
        if (strCompanyInfoId == "") {
            $.ligerDialog.warn('请先选择选择企业');
            return;
        }

        //        var surl = window.location = 'PointList.aspx?CompanyID=' + strCompanyInfoId;
        var tabid = "tabidPoint" + strCompanyInfoId;
        var surl = '../Channels/Base/Company/PointList.aspx?CompanyID=' + strCompanyInfoId;
        top.f_addTab(tabid, '监测点位', surl);
    }

    //增加数据
    function createData() {
        $.ligerDialog.open({ title: '企业信息增加', top: 0, width: 750, height: 450, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.DataSave()) {
                dialog.close();
                $.ligerDialog.success('数据保存成功')
            }
            else {
                //dialog.close();
                $.ligerDialog.warn('数据保存失败');
            }
            objCompanyInfoGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'CompanyInfoEdit.aspx'
        });
    }
    //修改数据
    function updateData() {
        if (!objCompanyInfoGrid.getSelectedRow()) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        $.ligerDialog.open({ title: '企业信息编辑', top: 0, width: 750, height: 450, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.DataSave()) {
                dialog.close();
                $.ligerDialog.success('数据更新成功')
            }
            else {
                //dialog.close();
                $.ligerDialog.warn('数据更新失败');
            }
            objCompanyInfoGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'CompanyInfoEdit.aspx?id=' + objCompanyInfoGrid.getSelectedRow().ID
        });
    }
    //删除数据
    function deleteData() {
        if (objCompanyInfoGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        $.ligerDialog.confirm('确认删除企业信息 ' + objCompanyInfoGrid.getSelectedRow().COMPANY_NAME + " 吗？", function (yes) {
            if (yes == true) {
                var strValue = objCompanyInfoGrid.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "CompanyInfo.aspx/deleteCompanyInfo",
                    data: "{'strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            objCompanyInfoGrid.loadData();
                            $.ligerDialog.success('删除数据成功')
                        }
                        else {
                            $.ligerDialog.warn('删除数据失败');
                        }
                    }
                });
            }
        });
    }

    //行业类别
    function toIndustry() {
        var surl = '../Channels/Base/Industry/IndustryList.aspx?MenuNo=toIndustry';
        top.f_addTab('toIndustry', '行业类别设置', surl);
    }


    //设置grid 的弹出查询对话框
    var detailWinSrh = null;
    function showDetailSrh() {
        if (detailWinSrh) {
            detailWinSrh.show();
        }
        else {
            //创建表单结构
            var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
            var divmainform = $("#Seachdiv");

            divmainform.ligerForm({
                inputWidth: 160, labelWidth: 120, space: 40, labelAlign: 'right',
                fields: [
                        { display: "企业名称", name: "SEA_COMPANY_NAME", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                        { display: "所在区域", name: "SEA_AREA", newline: true, type: "select", comboboxName: "SEA_AREA_BOX", options: { valueFieldID: "AREA", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "CompanyInfoEdit.aspx?type=getDict&dictType=administrative_area"} },
                        { display: "行业类别", name: "SEA_INDUSTRY", newline: true, type: "select", comboboxName: "SEA_INDUSTRY_BOX", options: { valueFieldID: "INDUSTRY", valueField: "ID", textField: "INDUSTRY_NAME", url: "CompanyInfoEdit.aspx?type=getIndustry"} }
                    ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#Seachdetail"),
                width: 400, height: 200, top: 90, title: "企业信息查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }
    }



    function search() {
        var SEA_COMPANY_NAME = encodeURI($("#SEA_COMPANY_NAME").val());
        var SEA_AREA = $("#AREA").val();
        var SEA_SEA_INDUSTRY = $("#INDUSTRY").val();

        objCompanyInfoGrid.set('url', "CompanyInfo.aspx?type=getCompanyInfo&srhCompayName= " + SEA_COMPANY_NAME + "&srh_Area=" + SEA_AREA + "&srh_Industry=" + SEA_SEA_INDUSTRY);


    }

    function clearSearchDialogValue() {
        $("#SEA_COMPANY_NAME").val("");
        $("#SEA_AREA_BOX").ligerGetComboBoxManager().setValue("");
        $("#SEA_INDUSTRY_BOX").ligerGetComboBoxManager().setValue("");
    }

    //获取行业类别信息
    function getIndustryName(strIndustryCode) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "CompanyInfo.aspx/getIndustryCode",
            data: "{'strValue':'" + strIndustryCode + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strValue = data.d;
            }
        });
        return strValue;
    }
    //获取字典项信息
    function getDictName(strDictCode, strDictType) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "CompanyInfo.aspx/getDictName",
            data: "{'strDictCode':'" + strDictCode + "','strDictType':'" + strDictType + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strValue = data.d;
            }
        });
        return strValue;
    }
});

function trim(s) {
    return trimRight(trimLeft(s));
}

//导入Excel
function ExcelImport() {
    $.ligerDialog.open({ title: "Excel导入界面", width: 500, height: 200, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            var value = $("iframe")[0].contentWindow.Import();
            //dialog.close();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "ImportDemo.aspx"
    });
}