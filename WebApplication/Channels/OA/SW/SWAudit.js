var isAdd = true;
var strSWID = "";
var struserItems = "", struserNames = "";
var strPost_Dept = "";

$.extend({
    getUrlVars: function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    getUrlVar: function (name) {
        return $.getUrlVars()[name];
    }
});

$(document).ready(function () {

    strSWID = $.getUrlVar('hidTaskId');

    //JS 获取当前时间
    function currentTime() {

        var d = new Date(), str = '';
        if (isAdd == true) {
            str += d.getFullYear() + '-';
            str += d.getMonth() + 1 + '-';
            str += d.getDate();
        }
        else {
            if (vExamInforList != null) {
                str = vExamInforList[0].EXAMINE_DATE;
            }
        }
        return str;
    }

    $("#btnFiledownLoad").bind("click", function () {
        downLoadFile();
    })

//    $("#ACCEPT_USERIDS").ligerComboBox({
//        onBeforeOpen: ACCEPT_REALNAMES_select, valueFieldID: 'ACCEPT_USERIDS'
//    });

    ///附件下载
    function downLoadFile() {
        if (strSWID == "") {
            $.ligerDialog.warn('业务ID参数错误');
            return;
        }
        $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
            buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileDownLoad.aspx?filetype=SWFile&id=' + strSWID
        });
    }
})

function BackSend() {
    $("#hidBtnType").val("back");
    //防止多次点击按钮产生多个任务
    $("#divContratSubmit")[0].style.display = "none";
}

function SendSave() {
    //防止多次点击按钮产生多个任务
    $("#divContratSubmit")[0].style.display = "none";
}

//弹出人员选择框
function ACCEPT_REALNAMES_select() {
    SetControlValue();
    mtardiv = $.ligerDialog.open({ target: $("#targerdiv"), title: '收文办理人员设置', name: 'winselector', width: 480, height: 400,
        buttons: [
                { text: '确定', onclick: function (item, text) {
                    GetItemsValue();
                    if (struserItems != "") {
                        ACCEPT_REALNAMES_selectOK();
                        mtardiv.hide();
                    }
                    else {
                        $.ligerDialog.warn('请选择要用户！');
                        return;
                    }
                }
                },
                { text: '取消', onclick: function (item, text) {
                    mtardiv.hide();
                }
                }
            ]
    });
    return false;
}

function SetControlValue() {
    $("#listLeft option").remove();
    $("#listRight option").remove();
    GeUserDeptItems();
    GetSubUserItems(strPost_Dept);
    GetSelectUserItems();
    $("#Dept").ligerGetComboBoxManager().setValue("");
}

//初始化部门的ComBox
function GeUserDeptItems() {
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "SWAudit.aspx/GetDeptItems",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != null) {
                DeptComBoxItem = data.d;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax加载数据失败！');
        }
    });

    $("#Dept").ligerComboBox({ data: DeptComBoxItem, width: 120, valueFieldID: 'DeptBox', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false, onSelected: function (newvalue) {
        seletComboxValue = newvalue;
        //初始化listBox
        $("#listLeft option").remove();
        $("#listRight option").remove();
        GetSubUserItems(newvalue);
        GetSelectUserItems();
    }
    });
    $("#pageloading").hide();
}
var DeptComBoxItem = null;
//初始化左侧ListBox
function GetSubUserItems(strDept) {
    vItemData = null;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "SWAudit.aspx/GetSubUserItems",
        data: "{'strPost_Dept':'" + strDept + "','strUserID':'" + $("#HID_USERIDS").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != null) {
                vItemData = data.d;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax加载数据失败！');
        }
    });

    //bind data
    var vlist = "";
    //遍历json数据,获取监测项目列表
    jQuery.each(vItemData, function (i, n) {
        vlist += "<option value=" + vItemData[i].ID + ">" + vItemData[i].REAL_NAME + "</option>";
    });
    //绑定数据到listLeft
    $("#listLeft").append(vlist);

    $("#pageloading").hide();
}

function GetSelectUserItems() {
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "SWAudit.aspx/GetSelectUserItems",
        data: "{'strUserID':'" + $("#HID_USERIDS").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != null) {
                vSelectedData = data.d;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax加载数据失败！');
        }
    });

    var vItemlist = "";
    jQuery.each(vSelectedData, function (i, n) {
        vItemlist += "<option value=" + vSelectedData[i].ID + ">" + vSelectedData[i].REAL_NAME + "</option>";
    });
    if (vItemlist.length > 0) {
        $("#listRight").append(vItemlist);
    }
}

//人员选择弹出grid ok按钮
function ACCEPT_REALNAMES_selectOK(item, dialog) {
    $("#HID_USERIDS").val(struserItems.substring(0, struserItems.length - 1));
    $("#ACCEPT_USERIDS").val(struserNames.substring(0, struserNames.length - 1));
}

function GetItemsValue() {

    struserItems = "";
    struserNames = "";
    $("#listRight option").each(function () {
        struserItems += $(this).val() + ",";
        struserNames += $(this).text() + "，";
    });
}

$(document).ready(function () {
    function moveright() {
        //数据option选中的数据集合赋值给变量vSelect
        var isExist = false;
        var vSelect = $("#listLeft option:selected");
        //克隆数据添加到listRight中
        if ($("#listRight option").length > 0) {
            $("#listRight option").each(function () {
                if ($(this).val() == $("#listLeft option:selected").val()) {
                    $.ligerDialog.warn('所选数据已存在！');
                    return isExist = false;
                }
                else {
                    isExist = true;
                }
            });
        }
        else {
            isExist = true;
        }
        if (isExist) {
            vSelect.clone().appendTo("#listRight");
            vSelect.remove();
        }
    }
    function moveleft() {
        //数据option选中的数据集合赋值给变量vSelect
        var isExist = false;
        var vSelect = $("#listRight option:selected");
        //克隆数据添加到listRight中
        if ($("#listLeft option").length > 0) {
            $("#listLeft option").each(function () {
                if ($(this).val() == $("#listRight option:selected").val()) {
                    $.ligerDialog.warn('所选数据已存在！');
                    return isExist = false;
                }
                else {
                    isExist = true;
                }
            });
        }
        else {
            isExist = true;
        }
        if (isExist) {
            vSelect.clone().appendTo("#listLeft");
            vSelect.remove();
        }
    }

    //right move
    $("#btnRight").click(function () {
        moveright();
    });
    //double click to move left
    $("#listLeft").dblclick(function () {
        //克隆数据添加到listRight中
        moveright();
    });

    //left move 
    $("#btnLeft").click(function () {
        moveleft();
    });

    //double click to move right
    $("#listRight").dblclick(function () {
        moveleft();
    });
})