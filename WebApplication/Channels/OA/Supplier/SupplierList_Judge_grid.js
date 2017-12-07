// Create by 潘德军 2012.11.07  "服务商管理的指定服务商评价列表"功能

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
                SUPPLIER_ID: selected.SUPPLIER_ID,
                PARTNAME: selected.PARTNAME,
                MODEL: selected.MODEL,
                REFERENCEPRICE: selected.REFERENCEPRICE,
                PRODUCTSTANDARD: selected.PRODUCTSTANDARD,
                DELIVERYPERIOD: selected.DELIVERYPERIOD,
                QUANTITY: selected.QUANTITY,
                ENTERPRISECODE: selected.ENTERPRISECODE,
                QUATITYSYSTEM: selected.QUATITYSYSTEM,
                SINCERITY: selected.SINCERITY
            }, false);

            break;
        case 'del':
            var selectedMain = mainmanager.getSelectedRow();
            var selectedSub = submanager.getSelectedRow();

            if (!selectedMain || selectedMain.ID.length == 0) {
                $.ligerDialog.warn('请先选择服务商！');
            }
            else if (!selectedSub || selectedSub.ID.length == 0) {
                $.ligerDialog.warn('请先选择评价信息！');
            }
            else {
                var strDelMainID = selectedMain.ID;
                var strDelSubID = selectedSub.ID;
                jQuery.ligerDialog.confirm('确定删除吗?', function (confirm) {
                    if (confirm)
                        delJudge(strDelMainID, strDelSubID);
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
            }], url: '../../OA/ATT/AttFileUpload.aspx?filetype=SupplierJudge&id=' + submanager.getSelectedRow().ID
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
            }], url: '../../OA/ATT/AttFileDownLoad.aspx?filetype=SupplierJudge&id=' + submanager.getSelectedRow().ID
    });
}
//subgrid的右键菜单
function itemclick_OfMenu_sub(item) {
    var selectedMain = mainmanager.getSelectedRow();
    var strDelMainID = selectedMain.ID;

    switch (item.id) {
        case 'menumodify':
            showDetailSub({
                ID: actionJudgeID,
                SUPPLIER_ID: actionSupplierID,
                PARTNAME: actionPartName,
                MODEL: actionModel,
                REFERENCEPRICE: actionPrice,
                PRODUCTSTANDARD: actionStandard,
                DELIVERYPERIOD: actionPeriod,
                QUANTITY: actionQuanlity,
                ENTERPRISECODE: actionEnterprisecode,
                QUATITYSYSTEM: actionQuatitySystem,
                SINCERITY: actionSincerity
            }, false);

            break;
        case 'menudel':
            var strDelID = actionJudgeID;

            jQuery.ligerDialog.confirm('确定删除吗?', function (confirm) {
                if (confirm)
                    delJudge(strDelMainID, strDelID);
            });

            break;
        default:
            break;
    }
}

//subgrid的删除函数
function delJudge(strDelMainID, strDelID) {
    $.ajax({
        cache: false,
        type: "POST",
        url: "SupplierList.aspx/delJudge",
        data: "{'strDelIDs':'" + strDelID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "1") {
                submanager.set('url', "SupplierList.aspx?Action=GetJudge&selSuppierID=" + strDelMainID);
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
        $.ligerDialog.warn('请先选择服务商！');
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
            inputWidth: 160, labelWidth: 120, space: 40, labelAlign: 'right',
            fields: [
                      { name: "ID", type: "hidden" },
                      { display: "服务商", name: "SUPPLIER_ID", newline: true, type: "select", comboboxName: "SUPPLIER_ID1", group: "资质信息", groupicon: groupicon, options: { valueFieldID: "SUPPLIER_ID", url: "../../base/MonitorType/Select.ashx?view=T_OA_SUPPLIER_INFO&idfield=ID&textfield=SUPPLIER_NAME"} },
                      { display: "产品/服务项目", name: "PARTNAME", width: 480, newline: true, type: "text" },
                      { display: "规格型号", name: "MODEL", newline: true, type: "text" },
                      { display: "参考价", name: "REFERENCEPRICE", newline: false, type: "text" },
                      { display: "产品生产标准", name: "PRODUCTSTANDARD", width: 480, newline: true, type: "text" },
                      { display: "最短供货期", name: "DELIVERYPERIOD", newline: true, type: "text" },
                      { display: "供货数量", name: "QUANTITY", newline: false, type: "text" },
                      { display: "资质变化情况", name: "ENTERPRISECODE", width: 480, newline: true, type: "text" },
                      { display: "质量保证体系", name: "QUATITYSYSTEM", width: 480, newline: true, type: "text" },
                      { display: "合同信守情况", name: "SINCERITY", width: 480, newline: true, type: "text" }
                    ]
        });
        $("#PARTNAME").attr("validate", "[{required:true,msg:'请填写产品/服务项目'},{maxlength:128,msg:'产品/服务项目录入的最大长度为128'}]");
        $("#MODEL").attr("validate", "[{maxlength:32,msg:'规格型号录入的最大长度为32'}]");
        $("#REFERENCEPRICE").attr("validate", "[{maxlength:64,msg:'参考价录入的最大长度为64'}]");
        $("#PRODUCTSTANDARD").attr("validate", "[{maxlength:64,msg:'产品生产标准录入的最大长度为64'}]");
        $("#DELIVERYPERIOD").attr("validate", "[{maxlength:4,msg:'最短供货期录入的最大长度为4'}]");
        $("#QUANTITY").attr("validate", "[{maxlength:16,msg:'供货数量录入的最大长度为16'}]");
        $("#ENTERPRISECODE").attr("validate", "[{maxlength:256,msg:'资质变化情况录入的最大长度为256'}]");
        $("#QUATITYSYSTEM").attr("validate", "[{maxlength:256,msg:'质量保证体系录入的最大长度为256'}]");
        $("#SINCERITY").attr("validate", "[{maxlength:256,msg:'合同信守情况录入的最大长度为256'}]");


        detailWinSub = $.ligerDialog.open({
            target: $("#detailSub"),
            width: 650, height: 400, top: 90, title: "评价信息",
            buttons: [
                  { text: '确定', onclick: function () { saveSub(); } },
                  { text: '取消', onclick: function () { clearDialogValue_Sub(); detailWinSub.hide(); } }
                  ]
        });
    }
    if (curentDataSub) {
        $("#ID").val(curentDataSub.ID);
        $("#PARTNAME").val(curentDataSub.PARTNAME);
        $("#MODEL").val(curentDataSub.MODEL);
        $("#REFERENCEPRICE").val(curentDataSub.REFERENCEPRICE);
        $("#PRODUCTSTANDARD").val(curentDataSub.PRODUCTSTANDARD);
        $("#DELIVERYPERIOD").val(curentDataSub.DELIVERYPERIOD);
        $("#QUANTITY").val(curentDataSub.QUANTITY);
        $("#ENTERPRISECODE").val(curentDataSub.ENTERPRISECODE);
        $("#QUATITYSYSTEM").val(curentDataSub.QUATITYSYSTEM);
        $("#SINCERITY").val(curentDataSub.SINCERITY);
    }

    $("#SUPPLIER_ID").val(selectedMain.ID);
    $("#SUPPLIER_ID1").ligerGetComboBoxManager().setValue(selectedMain.ID);
    $("#SUPPLIER_ID1").ligerGetComboBoxManager().setDisabled();

    function saveSub() {
        //添加表单验证
        if (!$("#editSubForm").validate())
            return false;
        var strData = "{";
        strData += currentIsAddNewSub ? "" : "'strID':'" + $("#ID").val() + "',";
        strData += currentIsAddNewSub ? "'strSUPPLIER_ID':'" + $("#SUPPLIER_ID").val() + "'," : ""; ;
        strData += "'strPARTNAME':'" + $("#PARTNAME").val() + "',";
        strData += "'strMODEL':'" + $("#MODEL").val() + "',";
        strData += "'strREFERENCEPRICE':'" + $("#REFERENCEPRICE").val() + "',";
        strData += "'strPRODUCTSTANDARD':'" + $("#PRODUCTSTANDARD").val() + "',";
        strData += "'strDELIVERYPERIOD':'" + $("#DELIVERYPERIOD").val() + "',";
        strData += "'strQUANTITY':'" + $("#QUANTITY").val() + "',";
        strData += "'strENTERPRISECODE':'" + $("#ENTERPRISECODE").val() + "',";
        strData += "'strQUATITYSYSTEM':'" + $("#QUATITYSYSTEM").val() + "',";
        strData += "'strSINCERITY':'" + $("#SINCERITY").val() + "'";
        strData += "}";

        $.ajax({
            cache: false,
            type: "POST",
            url: "SupplierList.aspx/" + (currentIsAddNewSub ? "AddJudge" : "EditJudge"),
            data: strData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    var selectedMain1 = mainmanager.getSelectedRow();
                    detailWinSub.hidden();
                    submanager.set('url', "SupplierList.aspx?Action=GetJudge&selSuppierID=" + selectedMain1.ID);
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
    $("#SUPPLIER_ID").val("");
    $("#SUPPLIER_ID1").val("");
    $("#PARTNAME").val("");
    $("#MODEL").val("");
    $("#REFERENCEPRICE").val("");
    $("#PRODUCTSTANDARD").val("");
    $("#DELIVERYPERIOD").val("");
    $("#QUANTITY").val("");
    $("#ENTERPRISECODE").val("");
    $("#QUATITYSYSTEM").val("");
    $("#SINCERITY").val("");
}