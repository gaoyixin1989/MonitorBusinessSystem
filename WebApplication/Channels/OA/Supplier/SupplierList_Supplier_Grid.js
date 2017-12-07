// Create by 潘德军 2012.11.07  "服务商管理的服务商列表"功能

//maingrid 的Toolbar click事件
function itemclick_OfToolbar_Main(item) {
    switch (item.id) {
        case 'add':
            showDetailMain(null, true);
            break;
        case 'modify':
            var selected = mainmanager.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择要编辑的记录！'); return; }

            showDetailMain({
                ID: selected.ID,
                SUPPLIER_NAME: selected.SUPPLIER_NAME,
                SUPPLIER_TYPE: selected.SUPPLIER_TYPE,
                PRODUCTS: selected.PRODUCTS,
                LINK_MAN: selected.LINK_MAN,
                TEL: selected.TEL,
                ADDRESS: selected.ADDRESS,
                FAX: selected.FAX,
                EMAIL: selected.EMAIL,
                POST_CODE: selected.POST_CODE,
                BANK: selected.BANK,
                ACCOUNT_FOR: selected.ACCOUNT_FOR
            }, false);

            break;
        case 'del':
            var rows = mainmanager.getCheckedRows();
            var strDelID = "";
            $(rows).each(function () {
                strDelID += (strDelID.length > 0 ? "," : "") + this.ID;
            });

            if (strDelID.length == 0) {
                $.ligerDialog.warn('请先选择要删除的记录！');
            }
            else {
                jQuery.ligerDialog.confirm('确定删除吗?', function (confirm) {
                    if (confirm)
                        delSupplier(strDelID);
                });
            }

            break;
        case 'srh':
            showDetailSrh();
            break;
        default:
            break;
    }
}

//maingrid 的右键菜单
function itemclick_OfMenu_main(item) {
    switch (item.id) {
        case 'menumodify':
            showDetailMain({
                ID: actionSupplierID,
                SUPPLIER_NAME: actionSupplierName,
                SUPPLIER_TYPE: actionType,
                PRODUCTS: actionProducts,
                LINK_MAN: actionLinkMan,
                TEL: actionTel,
                ADDRESS: actionAddress,
                FAX: actionFax,
                EMAIL: actionEmail,
                POST_CODE: actionPost,
                BANK: actionBank,
                ACCOUNT_FOR: actionAccountFor
            }, false);

            break;
        case 'menudel':
            var strDelID = actionSupplierID;

            jQuery.ligerDialog.confirm('确定删除吗?', function (confirm) {
                if (confirm)
                    delSupplier(strDelID);
            });

            break;
        default:
            break;
    }
}

//maingrid 的删除函数
function delSupplier(ids) {
    $.ajax({
        cache: false,
        type: "POST",
        url: "SupplierList.aspx/deleteSupplier",
        data: "{'strDelIDs':'" + ids + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "1") {
                mainmanager.loadData();
            }
            else {
                $.ligerDialog.warn('删除服务商失败！');
            }
        }
    });
}

//maingrid 的编辑对话框及save函数
var detailWinMain = null, curentDataMain = null, currentIsAddNewMain;
function showDetailMain(data, isAddNew) {
    curentDataMain = data;
    currentIsAddNewMain = isAddNew;
    if (detailWinMain) {
        detailWinMain.show();
    }
    else {
        //创建表单结构
        var mainform = $("#editMainForm");
        mainform.ligerForm({
            inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { name: "ID", type: "hidden" },
                      { display: "服务商", name: "SUPPLIER_NAME", newline: true, type: "text", validate: { required: true, minlength: 3 }, group: "基本信息", groupicon: groupicon },
                      { display: "供应物质类别", name: "SUPPLIER_TYPE", newline: false, type: "select", comboboxName: "SUPPLIER_TYPE1", options: { valueFieldID: "SUPPLIER_TYPE", url: "../../base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_TEXT&textfield=DICT_TEXT&where=DICT_TYPE|SUPPLIER_TYPE"} },
                      { display: "经营范围", name: "PRODUCTS", newline: true, width: 450, type: "text", validate: { required: true, minlength: 3} },
                      { display: "开户行", name: "BANK", newline: true, type: "text" },
                      { display: "帐号", name: "ACCOUNT_FOR", newline: false, type: "text" },
                      { display: "联系人", name: "LINK_MAN", newline: true, type: "text", group: "联系信息", groupicon: groupicon },
                      { display: "联系电话", name: "TEL", newline: false, type: "text" },
                      { display: "传真", name: "FAX", newline: true, type: "text" },
                      { display: "邮件", name: "EMAIL", newline: false, type: "text" },
                      { display: "邮政编码", name: "POST_CODE", newline: true, type: "text" },
                      { display: "联系地址", name: "ADDRESS", newline: false, type: "text" }
                    ]
        });

        $("#SUPPLIER_NAME").attr("validate", "[{required:true,msg:'请填写服务商'},{maxlength:64,msg:'服务商录入的最大长度为64'}]");
        $("#PRODUCTS").attr("validate", "[{maxlength:512,msg:'经营范围录入的最大长度为512'}]");
        $("#BANK").attr("validate", "[{maxlength:64,msg:'开户行的最大长度为64'}]");
        $("#ACCOUNT_FOR").attr("validate", "[{maxlength:64,msg:'帐号录入的最大长度为64'}]");
        $("#LINK_MAN").attr("validate", "[{maxlength:64,msg:'联系人录入的最大长度为64'}]");
        $("#TEL").attr("validate", "[{maxlength:16,msg:'联系电话录入的最大长度为16'}]");
        $("#FAX").attr("validate", "[{maxlength:16,msg:'传真录入的最大长度为16'}]");
        $("#EMAIL").attr("validate", "[{maxlength:128,msg:'邮件录入的最大长度为128'}]");
        $("#POST_CODE").attr("validate", "[{maxlength:8,msg:'邮政编码录入的最大长度为8'}]");
        $("#ADDRESS").attr("validate", "[{maxlength:512,msg:'联系地址录入的最大长度为512'}]");

        detailWinMain = $.ligerDialog.open({
            target: $("#detailMain"),
            width: 650, height: 350, top: 90, title: "服务商信息",
            buttons: [
                  { text: '确定', onclick: function () { saveMain(); } },
                  { text: '取消', onclick: function () { clearMainDialogValue(); detailWinMain.hide(); } }
                  ]
        });
    }
    if (curentDataMain) {
        $("#ID").val(curentDataMain.ID);
        $("#SUPPLIER_NAME").val(curentDataMain.SUPPLIER_NAME);
        $("#SUPPLIER_TYPE").val(curentDataMain.SUPPLIER_TYPE);
        $("#SUPPLIER_TYPE1").val(curentDataMain.SUPPLIER_TYPE);
        $("#PRODUCTS").val(curentDataMain.PRODUCTS);
        $("#LINK_MAN").val(curentDataMain.LINK_MAN);
        $("#TEL").val(curentDataMain.TEL);
        $("#FAX").val(curentDataMain.FAX);
        $("#EMAIL").val(curentDataMain.EMAIL);
        $("#POST_CODE").val(curentDataMain.POST_CODE);
        $("#ADDRESS").val(curentDataMain.ADDRESS);
        $("#BANK").val(curentDataMain.BANK);
        $("#ACCOUNT_FOR").val(curentDataMain.ACCOUNT_FOR);
    }

    function saveMain() {
        //添加表单验证
        if (!$("#editMainForm").validate())
            return false;
        var strData = "{";
        strData += currentIsAddNewMain ? "" : "'strID':'" + $("#ID").val() + "',";
        strData += "'strSUPPLIER_NAME':'" + $("#SUPPLIER_NAME").val() + "',";
        strData += "'strSUPPLIER_TYPE':'" + $("#SUPPLIER_TYPE").val() + "',";
        strData += "'strPRODUCTS':'" + $("#PRODUCTS").val() + "',";
        strData += "'strLINK_MAN':'" + $("#LINK_MAN").val() + "',";
        strData += "'strTEL':'" + $("#TEL").val() + "',";
        strData += "'strFAX':'" + $("#FAX").val() + "',";
        strData += "'strEMAIL':'" + $("#EMAIL").val() + "',";
        strData += "'strPOST_CODE':'" + $("#POST_CODE").val() + "',";
        strData += "'strADDRESS':'" + $("#ADDRESS").val() + "',";
        strData += "'strBANK':'" + $("#BANK").val() + "',";
        strData += "'strACCOUNT_FOR':'" + $("#ACCOUNT_FOR").val() + "'";
        strData += "}";

        $.ajax({
            cache: false,
            type: "POST",
            url: "SupplierList.aspx/" + (currentIsAddNewMain ? "AddSupplier" : "EditSupplier"),
            data: strData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    detailWinMain.hidden();
                    mainmanager.loadData();
                    clearMainDialogValue();
                }
                else {
                    $.ligerDialog.warn('保存服务商数据失败！');
                }
            }
        });
    }
}

//maingrid 的编辑对话框元素的值 清除
function clearMainDialogValue() {
    $("#ID").val("");
    $("#SUPPLIER_NAME").val("");
    $("#SUPPLIER_TYPE").val("");
    $("#SUPPLIER_TYPE1").val("");
    $("#PRODUCTS").val("");
    $("#LINK_MAN").val("");
    $("#TEL").val("");
    $("#FAX").val("");
    $("#EMAIL").val("");
    $("#POST_CODE").val("");
    $("#ADDRESS").val("");
    $("#BANK").val("");
    $("#ACCOUNT_FOR").val("");
}

var detailWinSrh = null;
function showDetailSrh() {
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构
        var mainform = $("#searchForm");
        mainform.ligerForm({
            inputWidth: 160, labelWidth: 120, space: 40, labelAlign: 'right',
            fields: [
                      { display: "服务商", name: "srhSUPPLIER_NAME", newline: true, type: "text", group: "查询信息", groupicon: groupicon },
                      { display: "供应物质类别", name: "srhSUPPLIER_TYPE", newline: true, type: "select", comboboxName: "srhSUPPLIER_TYPE1", options: { valueFieldID: "srhSUPPLIER_TYPE", url: "../../base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_TEXT&textfield=DICT_TEXT&where=DICT_TYPE|SUPPLIER_TYPE"} }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 350, height: 200, top: 90, title: "查询服务商",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var srhSUPPLIER_NAME = $("#srhSUPPLIER_NAME").val();
        var srhSUPPLIER_TYPE = escape($("#srhSUPPLIER_TYPE").val());

        mainmanager.set('url', "SupplierList.aspx?Action=GetSupplier&srhSUPPLIER_NAME=" + srhSUPPLIER_NAME + "&srhSUPPLIER_TYPE=" + srhSUPPLIER_TYPE);
    }
}

//maingrid 的查询对话框元素的值 清除
function clearSearchDialogValue() {
    $("#srhSUPPLIER_NAME").val("");
    $("#srhSUPPLIER_TYPE").val("");
    $("#srhSUPPLIER_TYPE1").val("");
}