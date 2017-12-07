// Create by 潘德军 2012.11.06  "分包单位管理的指定分包单位的资质查新列表"功能

//subgrid的toolbar click事件
function itemclick_OfToolbar_sub(item) {
    switch (item.id) {
        case 'add':
            showDetailSub(null, true);
            break;
        case 'modify':
            var selected = submanager.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择要编辑的记录！'); ; return }

            showDetailSub({
                ID: selected.ID,
                OutCompany_ID: selected.OutCompany_ID,
                COMPANY_NAME: selected.COMPANY_NAME,
                QUALIFICATIONS_INFO: selected.QUALIFICATIONS_INFO,
                PROJECT_INFO: selected.PROJECT_INFO,
                QC_INFO: selected.QC_INFO,
                IS_OK: selected.IS_OK,
                CHECK_USER_ID: selected.CHECK_USER_ID,
                CHECK_DATE: selected.CHECK_DATE,
                COMPLETE_INFO: selected.COMPLETE_INFO,
                APP_INFO: selected.APP_INFO,
                APP_USER_ID: selected.APP_USER_ID,
                APP_DATE: selected.APP_DATE
            }, false);

            break;
        case 'del':
            var selectedMain = mainmanager.getSelectedRow();
            var selectedSub = submanager.getSelectedRow();

            if (!selectedMain || selectedMain.ID.length == 0) {
                $.ligerDialog.warn('请先选择外包单位！');
            }
            else if (!selectedSub || selectedSub.ID.length == 0) {
                $.ligerDialog.warn('请先选择资质信息！');
            }
            else {
                var strDelMainID = selectedMain.ID;
                var strDelSubID = selectedSub.ID;
                jQuery.ligerDialog.confirm('确定删除吗?', function (confirm) {
                    if (confirm)
                        delAllow(strDelMainID, strDelSubID);
                });
            }

            break;
        default:
            break;
    }
}
///附件上传
function upLoadFile() {
    if (submanager.getSelectedRow() == null) {
        $.ligerDialog.warn('上传附件之前请先选择一条记录');
        return;
    }
    $.ligerDialog.open({ title: '附件上传', width: 500, height: 270,
        buttons: [
            {
                text:
                    '上传', onclick: function (item, dialog) {
                        $("iframe")[0].contentWindow.upLoadFile();
                    }
            },
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileUpload.aspx?filetype=OutCompanyAllow&id=' + submanager.getSelectedRow().ID
    });
}
///附件下载
function downLoadFile() {
    if (submanager.getSelectedRow() == null) {
        $.ligerDialog.warn('下载附件之前请先选择一条记录');
        return;
    }
    $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
        buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileDownLoad.aspx?filetype=OutCompanyAllow&id=' + submanager.getSelectedRow().ID
    });
}
//subgrid的右键菜单
function itemclick_OfMenu_sub(item) {
    var selectedMain = mainmanager.getSelectedRow();
    var strDelMainID = selectedMain.ID;

    switch (item.id) {
        case 'menumodify':
            showDetailSub({
                ID: actionAllowID,
                OutCompany_ID: actionOutCompanyID,
                COMPANY_NAME: actionOutCompanyName,
                QUALIFICATIONS_INFO: actionQualification,
                PROJECT_INFO: actionProject,
                QC_INFO: actionQC,
                IS_OK: actionIsOk,
                CHECK_USER_ID: actionCheckUserID,
                CHECK_DATE: actionCheckDate,
                COMPLETE_INFO: actionComplete,
                APP_INFO: actionAppInfo,
                APP_USER_ID: actionAppUserID,
                APP_DATE: actionAppDate
            }, false);

            break;
        case 'menudel':
            var strDelID = actionAllowID;

            jQuery.ligerDialog.confirm('确定删除吗?', function (confirm) {
                if (confirm)
                    delAllow(strDelMainID, strDelID);
            });

            break;
        default:
            break;
    }
}

//subgrid的删除函数
function delAllow(strDelMainID, strDelID) {
    $.ajax({
        cache: false,
        type: "POST",
        url: "OutCompanyList.aspx/delAllow",
        data: "{'strDelIDs':'" + strDelID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "1") {
                submanager.set('url', "OutCompanyList.aspx?Action=GetAllow&selCompanyID=" + strDelMainID);
            }
            else {
                $.ligerDialog.warn('删除资质信息失败！');
            }
        }
    });
}

//subgrid的编辑对话框及save函数
var detailWinSub = null, curentDataSub = null, currentIsAddNewSub;
function showDetailSub(data, isAddNew) {
    var selectedMain = mainmanager.getSelectedRow();
    if (!selectedMain) {
        $.ligerDialog.warn('请先选择分包单位！');
        return;
    }

    curentDataSub = data;
    currentIsAddNewSub = isAddNew;
    if (detailWinSub) {
        detailWinSub.show();
    }
    else {
        //创建表单结构
        var mainform = $("#editSubForm");
        mainform.ligerForm({
            inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { name: "ID", type: "hidden" },
                      { display: "分包单位", name: "OutCompany_ID", newline: true, type: "select", comboboxName: "OutCompany_ID1", group: "基本信息", groupicon: groupicon, options: { valueFieldID: "OutCompany_ID", url: "../MonitorType/Select.ashx?view=T_BASE_OUTCOMPANY_INFO&idfield=ID&textfield=COMPANY_NAME&where=is_del|0"} },
                      { display: "资质变化情况", name: "QUALIFICATIONS_INFO", width: 450, newline: true, type: "text" },
                      { display: "主要项目情况", name: "PROJECT_INFO", width: 450, newline: true, type: "text" },
                      { display: "质保体系情况", name: "QC_INFO", width: 450, newline: true, type: "text" },
                      { display: "委托完成情况", name: "COMPLETE_INFO", width: 450, newline: true, type: "text" },
                      { display: "经办人", name: "CHECK_USER_ID", newline: true, type: "text" },
                      { display: "经办日期", name: "CHECK_DATE", newline: false, type: "date", options: { format: "yyyy-MM-dd"} },
                      { display: "是否通过评审", name: "IS_OK", newline: true, type: "select", comboboxName: "IS_OK1", group: "评审信息", groupicon: groupicon, options: { valueFieldID: "IS_OK", url: "../MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|LAB_CERTIFICATE"} },
                      { display: "评审意见", name: "APP_INFO", newline: true, width: 450, type: "text" },
                      { display: "评审人", name: "APP_USER_ID", newline: true, type: "text" },
                      { display: "评审日期", name: "APP_DATE", newline: false, type: "date", options: { format: "yyyy-MM-dd"} }
                    ]
        });
        //add validate by ssz
        $("#QUALIFICATIONS_INFO").attr("validate", "[{maxlength:256,msg:'资质变化情况最大长度为256'}]");
        $("#PROJECT_INFO").attr("validate", "[{maxlength:512,msg:'主要项目情况最大长度为512'}]");
        $("#QC_INFO").attr("validate", "[{maxlength:64,msg:'质保体系情况最大长度为64'}]");
        $("#COMPLETE_INFO").attr("validate", "[{maxlength:512,msg:'委托完成情况最大长度为512'}]");
        $("#CHECK_USER_ID").attr("validate", "[{maxlength:64,msg:'经办人最大长度为64'}]");
        $("#APP_INFO").attr("validate", "[{maxlength:512,msg:'评审意见最大长度为512'}]");
        $("#APP_USER_ID").attr("validate", "[{maxlength:64,msg:'评审人最大长度为64'}]");

        detailWinSub = $.ligerDialog.open({
            target: $("#detailSub"),
            width: 600, height: 450, top: 90, title: "资质查新信息",
            buttons: [
                  { text: '确定', onclick: function () { saveSub(); } },
                  { text: '取消', onclick: function () { clearDialogValue_Sub(); detailWinSub.hide(); } }
                  ]
        });
    }
    if (curentDataSub) {
        $("#ID").val(curentDataSub.ID);
        $("#QUALIFICATIONS_INFO").val(curentDataSub.QUALIFICATIONS_INFO);
        $("#PROJECT_INFO").val(curentDataSub.PROJECT_INFO);
        $("#QC_INFO").val(curentDataSub.QC_INFO);
        $("#COMPLETE_INFO").val(curentDataSub.COMPLETE_INFO);
        $("#CHECK_USER_ID").val(curentDataSub.CHECK_USER_ID);
        $("#CHECK_DATE").ligerGetDateEditorManager().setValue(curentDataSub.CHECK_DATE);
        $("#IS_OK").val(curentDataSub.IS_OK);
        $("#IS_OK1").ligerGetComboBoxManager().setValue(curentDataSub.IS_OK);
        $("#APP_INFO").val(curentDataSub.APP_INFO);
        $("#APP_USER_ID").val(curentDataSub.APP_USER_ID);
        $("#APP_DATE").ligerGetDateEditorManager().setValue(curentDataSub.APP_DATE);
    }

    $("#OutCompany_ID").val(selectedMain.ID);
    $("#OutCompany_ID1").ligerGetComboBoxManager().setValue(selectedMain.ID);
    $("#OutCompany_ID1").ligerGetComboBoxManager().setDisabled();

    function saveSub() {
        //表单验证
        if (!$("#editSubForm").validate())
            return false;
        var strData = "{";
        strData += currentIsAddNewSub ? "" : "'strID':'" + $("#ID").val() + "',";
        strData += currentIsAddNewSub ? "'strOutCompany_ID':'" + $("#OutCompany_ID").val() + "'," : ""; ;
        strData += "'strQUALIFICATIONS_INFO':'" + $("#QUALIFICATIONS_INFO").val() + "',";
        strData += "'strPROJECT_INFO':'" + $("#PROJECT_INFO").val() + "',";
        strData += "'strQC_INFO':'" + $("#QC_INFO").val() + "',";
        strData += "'strCOMPLETE_INFO':'" + $("#COMPLETE_INFO").val() + "',";
        strData += "'strCHECK_USER_ID':'" + $("#CHECK_USER_ID").val() + "',";
        strData += "'strCHECK_DATE':'" + $("#CHECK_DATE").val() + "',";
        strData += "'strIS_OK':'" + $("#IS_OK").val() + "',";
        strData += "'strAPP_INFO':'" + $("#APP_INFO").val() + "',";
        strData += "'strAPP_USER_ID':'" + $("#APP_USER_ID").val() + "',";
        strData += "'strAPP_DATE':'" + $("#APP_DATE").val() + "'";
        strData += "}";

        $.ajax({
            cache: false,
            type: "POST",
            url: "OutCompanyList.aspx/" + (currentIsAddNewSub ? "AddAllow" : "EditAllow"),
            data: strData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    detailWinSub.hidden();
                    submanager.set('url', "OutCompanyList.aspx?Action=GetAllow&selCompanyID=" + selectedMain.ID);
                    clearDialogValue_Sub();
                }
                else {
                    $.ligerDialog.warn('保存资质查新信息失败！');
                }
            }
        });
    }
}

//sub grid的编辑对话框 清空
function clearDialogValue_Sub() {
    $("#ID").val("");
    $("#QUALIFICATIONS_INFO").val("");
    $("#PROJECT_INFO").val("");
    $("#QC_INFO").val("");
    $("#COMPLETE_INFO").val("");
    $("#CHECK_USER_ID").val("");
    $("#CHECK_DATE").val("");
    $("#IS_OK").val("是");
    $("#IS_OK1").ligerGetComboBoxManager().setValue("是");
    $("#APP_INFO").val("");
    $("#APP_USER_ID").val("");
    $("#APP_DATE").val("");
}