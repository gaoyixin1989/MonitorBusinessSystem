// Create by 潘德军 2012.11.06  "分包单位管理的分包单位列表"功能

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
                COMPANY_NAME: selected.COMPANY_NAME,
                COMPANY_CODE: selected.COMPANY_CODE,
                LINK_MAN: selected.LINK_MAN,
                PHONE: selected.PHONE,
                POST: selected.POST,
                ADDRESS: selected.ADDRESS,
                APTITUDE: selected.APTITUDE
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
                        delOutCompany(strDelID);
                });
            }

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
                ID: actionOutID,
                COMPANY_NAME: actionCompanyName,
                COMPANY_CODE: actionCompanyCode,
                LINK_MAN: actionLinkMan,
                PHONE: actionPhone,
                POST: actionPost,
                ADDRESS: actionAddress,
                APTITUDE: actionAptitude
            }, false);

            break;
        case 'menudel':
            var strDelID = actionOutID;

            jQuery.ligerDialog.confirm('确定删除吗?', function (confirm) {
                if (confirm)
                    delOutCompany(strDelID);
            });

            break;
        default:
            break;
    }
}

//maingrid 的删除函数
function delOutCompany(ids) {
    $.ajax({
        cache: false,
        type: "POST",
        url: "OutCompanyList.aspx/deleteOutCompany",
        data: "{'strDelIDs':'" + ids + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "1") {
                mainmanager.loadData();
            }
            else {
                $.ligerDialog.warn('删除监测项目数据失败！');
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
                      { display: "外包单位", name: "COMPANY_NAME", newline: true, type: "text", validate: { required: true, minlength: 3 }, group: "基本信息", groupicon: groupicon },
                      { display: "法人代码", name: "COMPANY_CODE", newline: false, type: "text", validate: { required: true, minlength: 3} },
                      { display: "联系人", name: "LINK_MAN", newline: true, type: "text" },
                      { display: "联系电话", name: "PHONE", newline: false, type: "text" },
                      { display: "邮政编码", name: "POST", newline: true, type: "text" },
                      { display: "详细地址", name: "ADDRESS", newline: false, type: "text" },
                      { display: "资质", name: "APTITUDE", width: 450, newline: true, type: "text" }
                    ]
        });
        //add validate by ssz
        $("#COMPANY_NAME").attr("validate", "[{required:true,msg:'请填写外包单位'},{maxlength:256,msg:'外包单位最大长度为256'}]");
        $("#COMPANY_CODE").attr("validate", "[{maxlength:32,msg:'法人代码最大长度为32'}]");
        $("#LINK_MAN").attr("validate", "[{maxlength:64,msg:'联系人最大长度为64'}]");
        $("#PHONE").attr("validate", "[{maxlength:64,msg:'联系电话最大长度为64'}]");
        $("#POST").attr("validate", "[{maxlength:64,msg:'邮政编码最大长度为64'}]");
        $("#ADDRESS").attr("validate", "[{maxlength:512,msg:'详细地址最大长度为512'}]");
        $("#APTITUDE").attr("validate", "[{maxlength:2048,msg:'资质最大长度为2048'}]");

        detailWinMain = $.ligerDialog.open({
            target: $("#detailMain"),
            width: 650, height: 250, top: 90, title: "外包单位信息",
            buttons: [
                  { text: '确定', onclick: function () { saveMain(); } },
                  { text: '取消', onclick: function () { clearMainDialogValue(); detailWinMain.hide(); } }
                  ]
        });
    }
    if (curentDataMain) {
        $("#ID").val(curentDataMain.ID);
        $("#COMPANY_NAME").val(curentDataMain.COMPANY_NAME);
        $("#COMPANY_CODE").val(curentDataMain.COMPANY_CODE);
        $("#LINK_MAN").val(curentDataMain.LINK_MAN);
        $("#PHONE").val(curentDataMain.PHONE);
        $("#POST").val(curentDataMain.POST);
        $("#ADDRESS").val(curentDataMain.ADDRESS);
        $("#APTITUDE").val(curentDataMain.APTITUDE);
    }

    function saveMain() {
        //表单验证
        if (!$("#editMainForm").validate())
            return false;
        var strData = "{";
        strData += currentIsAddNewMain ? "" : "'strID':'" + $("#ID").val() + "',";
        strData += "'strCOMPANY_NAME':'" + $("#COMPANY_NAME").val() + "',";
        strData += "'strCOMPANY_CODE':'" + $("#COMPANY_CODE").val() + "',";
        strData += "'strLINK_MAN':'" + $("#LINK_MAN").val() + "',";
        strData += "'strPHONE':'" + $("#PHONE").val() + "',";
        strData += "'strPOST':'" + $("#POST").val() + "',";
        strData += "'strADDRESS':'" + $("#ADDRESS").val() + "',";
        strData += "'strAPTITUDE':'" + $("#APTITUDE").val() + "'";
        strData += "}";

        $.ajax({
            cache: false,
            type: "POST",
            url: "OutCompanyList.aspx/" + (currentIsAddNewMain ? "AddOutCompany" : "EditOutCompany"),
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
                    $.ligerDialog.warn('保存外包单位数据失败！');
                }
            }
        });
    }
}

//maingrid 的编辑对话框元素的值 清除
function clearMainDialogValue() {
    $("#ID").val("");
    $("#COMPANY_NAME").val("");
    $("#COMPANY_CODE").val("");
    $("#LINK_MAN").val("");
    $("#PHONE").val("");
    $("#POST").val("");
    $("#ADDRESS").val("");
    $("#APTITUDE").val("");
}

