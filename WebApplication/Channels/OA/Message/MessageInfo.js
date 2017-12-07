// Create by 苏成斌 2012.12.1  "消息发送"功能
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strID = "";
var strSuperContent = "";
var struserItems = "", struserNames = "";
var strPost_Dept = "", vRItems = "";
var gridMonitSelectId = "";
var manager = null;

$(document).ready(function () {
    strID = request('strid');
    strSuperContent = request('supercontent');
    if (!strID)
        strID = "";
    if (!strSuperContent)
        strSuperContent = "";
    
    //创建表单结构 
    $("#divEdit").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 30, labelAlign: 'right',
        fields: [
                { name: "ID", type: "hidden" },
                { display: "消息标题", name: "MESSAGE_TITLE", newline: true, type: "text", width: 440, group: "发送信息", groupicon: groupicon },
                { display: "发送人", name: "SEND_BY", newline: true, type: "text" },
                { name: "SEND_BY_ID", type: "hidden" },
                { display: "发送时间", name: "SEND_DATE", newline: false, type: "text" },
                { display: "接收方式", name: "ACCEPT_TYPE", newline: true, type: "select", comboboxName: "ACCEPT_TYPE_BOX", options: { data: [{ text: '全站', id: '1' }, { text: '个人', id: '2'}]} },
                { display: "接收人员", name: "ACCEPT_REALNAMES", newline: true, width: 440, type: "select" },
                { name: "ACCEPT_USERIDS", type: "hidden" },
                { name: "REMARK1", type: "hidden" },
                { display: "接收部门", name: "ACCEPT_DEPTNAMES", newline: true, width: 440, type: "select", comboboxName: "ACCEPT_DEPTNAMES_BOX", options: { valueFieldID: "ACCEPT_DEPTNAMES_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|DEPT", isShowCheckBox: true, isMultiSelect: true} },
                { name: "ACCEPT_DEPTIDS", type: "hidden"}//,
        //{ display: "消息内容", name: "MESSAGE_CONTENT", newline: true, width: 440, type: "text" }
                ]
    });

    //添加表单验证
    $("#MESSAGE_TITLE").attr("validate", "[{required:true,msg:'请填写消息标题'},{maxlength:256,msg:'消息标题录入最大长度为256'}]");
    $("#MESSAGE_CONTENT").attr("validate", "[{maxlength:2048,msg:'消息内容录入最大长度为2048'}]");

    //    $("#ACCEPT_TYPE").click(function () {
    //        if ($("#ACCEPT_TYPE").val() == "全站") {
    //            $("#ACCEPT_REALNAMES").ligerGetComboBoxManager().setDisabled();
    //            $("#ACCEPT_DEPTNAMES").ligerGetComboBoxManager().setDisabled();
    //        }
    //    });

    $("#ACCEPT_REALNAMES").ligerComboBox({
        onBeforeOpen: ACCEPT_REALNAMES_select, valueFieldID: 'ACCEPT_USERIDS'
    });

    $("#SEND_DATE").val(currentTime())
    $("#SEND_BY").val($("#UserRealName").val());
    $("#SEND_BY_ID").val($("#UserID").val());

    //JS 获取当前时间
    function currentTime() {
        var d = new Date(), str = '';
        str += d.getFullYear() + '-';
        str += d.getMonth() + 1 + '-';
        str += d.getDate();
        //      str += d.getHours() + '时';
        //      str += d.getMinutes() + '分';
        //      str += d.getSeconds() + '秒';
        return str;
    }

    $("#SEND_BY").ligerGetComboBoxManager().setDisabled();
    $("#SEND_DATE").ligerGetComboBoxManager().setDisabled();

    //加载数据
    if (strID != "") {
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "MessageInfo.aspx?type=loadData&strid=" + strID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                bindJsonToPage(data);
            }
        });
        $("#ACCEPT_TYPE_BOX").ligerGetComboBoxManager().setDisabled();
        $("#ACCEPT_REALNAMES").ligerGetComboBoxManager().setDisabled();
        $("#ACCEPT_DEPTNAMES_BOX").ligerGetComboBoxManager().setDisabled();
    }
    //如果是任务督办发送时
    if (strSuperContent != "") {
        $("#MESSAGE_TITLE").val("任务督办");
        $("#ACCEPT_TYPE_BOX").ligerGetComboBoxManager().selectValue("2");
        $("#MESSAGE_CONTENT").val(strSuperContent);
    }
});

//得到保存信息
function getSaveDate() {
    //添加表单验证
    if (!$("#divEdit").validate())
        return false;
    var strData = "{";

    if (!strID)
        strData += "'strID':'',";
    else
        strData += "'strID':'" + strID + "',";

    strData += "'strMessageTitle':'" + $("#MESSAGE_TITLE").val() + "',";
    strData += "'strSendBy':'" + $("#SEND_BY_ID").val() + "',";
    strData += "'strSendDate':'" + $("#SEND_DATE").val() + "',";
    strData += "'strAcceptType':'" + $("#ACCEPT_TYPE_BOX").val() + "',";
    strData += "'strAcceptRealNames':'" + $("#ACCEPT_REALNAMES").val() + "',";
    strData += "'strAcceptUserIDs':'" + $("#ACCEPT_USERIDS").val() + "',";
    strData += "'strAcceptDeptNames':'" + $("#ACCEPT_DEPTNAMES_BOX").val() + "',";
    strData += "'strAcceptDeptIDs':'" + $("#ACCEPT_DEPTNAMES").val() + "',";
    strData += "'strMessageContent':'" + $("#MESSAGE_CONTENT").val() + "',";
    strData += "'strUserID':'" + $("#REMARK1").val() + "'";
    strData += "}";

    return strData;
}

//cancel按钮
function selectCancel(item, dialog) {
    dialog.close();
}

//弹出人员选择框
function ACCEPT_REALNAMES_select() {
    if ($("#ACCEPT_TYPE").val() == ('全站')) {
        $.ligerDialog.warn('请先选择监测类型!');
        return;
    }

    SetControlValue();
    mtardiv = $.ligerDialog.open({ target: $("#targerdiv"), title: '消息接收人员设置', name: 'winselector', width: 450, height: 400,
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

//人员选择弹出grid ok按钮
function ACCEPT_REALNAMES_selectOK(item, dialog) {
    $("#ACCEPT_USERIDS").val(struserItems);
    $("#ACCEPT_REALNAMES").val(struserNames);
}
function GetItemsValue() {

    struserItems = "";
    $("#listRight option").each(function () {
        struserItems += $(this).val() + ",";
        struserNames += $(this).text() + "，";
    });
}

function SaveDivData() {
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "MessageInfo.aspx/InsertSelectedUser",
        data: "{'strUserId':'" + struserItems + "','strMonitor':'" + gridMonitSelectId + "','strMoveUserId':'" + moveid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == true) {
                $.ligerDialog.success('数据保存成功！');
                SelectUserList();
                SetControlValue();
            }
            else {
                $.ligerDialog.warn('数据操作失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax加载数据失败！');
        }
    });
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
        url: "MessageInfo.aspx/GetDeptItems",
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
        url: "MessageInfo.aspx/GetSubUserItems",
        data: "{'strPost_Dept':'" + strDept + "','strMessageId':'" + $("#ID").val() + "'}",
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
        url: "MessageInfo.aspx/GetSelectUserItems",
        data: "{'strMessageId':'" + $("#ID").val() + "'}",
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
