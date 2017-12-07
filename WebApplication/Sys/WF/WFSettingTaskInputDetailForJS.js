var groupicon = "../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

var strID = "";

$(document).ready(function () {
    var strWF_ID = request('WF_ID');
    if (!strWF_ID)
        return;
    var strWF_TASK_ID = request('WF_TASK_ID');
    if (!strWF_TASK_ID)
        strWF_TASK_ID = "";

    //创建表单结构 --点位基本信息
    $("#divEdit").ligerForm({
        inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right',
        fields: [
                { name: "ID", type: "hidden" },
                { display: "环节名称", name: "TASK_CAPTION", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                { display: "环节描述", name: "TASK_NOTE", newline: false, type: "text" },
                { display: "节点命令", name: "COMMAND_NAME", newline: true, type: "select", comboboxName: "COMMAND_NAME1", options: {
                    isShowCheckBox: true, isMultiSelect: true, valueFieldID: "COMMAND_NAME",
                    url: "../../Channels/Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|WF_CMD"
                }
                },
                { display: "附加功能", name: "FUNCTION_LIST", newline: false, type: "select", comboboxName: "FUNCTION_LIST1", options: {
                    isShowCheckBox: true, isMultiSelect: true, valueFieldID: "FUNCTION_LIST",
                    url: "../../Channels/Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|WF_ATT"
                }
                },
                { display: "路由节点", name: "TASK_AND_OR", newline: true, type: "select", comboboxName: "TASK_AND_OR1", options: {
                    valueFieldID: "TASK_AND_OR",
                    url: "../../Channels/Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|WF_OR"
                }
                },
                { display: "页面类型", name: "POSITION_IX", newline: false, type: "select", comboboxName: "POSITION_IX1", options: {
                    valueFieldID: "POSITION_IX",
                    url: "../../Channels/Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|WF_PageType"
                }
                },
                { display: "页面路径", name: "POSITION_IY", width: 530, newline: true, type: "text" },
                { display: "用户", name: "Task_User", width: 530, newline: true, type: "select", group: "办理权限", groupicon: groupicon }
        //                { display: "职位", name: "Task_Post", width: 530, newline: true, type: "select" }
                ]
    });

    $("#Task_User").ligerComboBox({
        onBeforeOpen: TaskUser_select, valueFieldID: 'hidTask_User'
    });

    //    $("#Task_Post").ligerComboBox({
    //        onBeforeOpen: TaskPost_select, valueFieldID: 'hidTask_Post'
    //    });

    //加载数据
    if (strWF_TASK_ID != "") {
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "WFSettingTaskInputDetailForJS.aspx?type=loadData&WF_ID=" + strWF_ID + "&WF_TASK_ID=" + strWF_TASK_ID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                bindJsonToPage(data);
                $("#hidTask_User").val(data.OPER_VALUE);
                $("#Task_User").val(data.OPER_TYPE);
            }
        });
    }
});

//得到保存信息
function getSaveDate() {
    var strWF_ID = request('WF_ID');
    if (!strWF_ID)
        return;
    var strWF_TASK_ID = request('WF_TASK_ID');
    if (!strWF_TASK_ID)
        strWF_TASK_ID = "0";
    var strid =  $("#ID").val();
    if (strid.length==0)
        strid = "0";

    var strData = "{";
    strData += "'strid':'" + strid + "',";
    strData += "'stWF_ID':'" + strWF_ID + "',";
    strData += "'strWF_TASK_ID':'" + strWF_TASK_ID + "',";
    strData += "'strTASK_CAPTION':'" + $("#TASK_CAPTION").val() + "',";
    strData += "'strTASK_NOTE':'" + $("#TASK_NOTE").val() + "',";
    strData += "'strCOMMAND_NAME':'" + $("#COMMAND_NAME").val() + "',";
    strData += "'strCOMMAND_NAME_Text':'" + $("#COMMAND_NAME1").val() + "',";
    strData += "'strFUNCTION_LIST':'" + $("#FUNCTION_LIST").val() + "',";
    strData += "'strTASK_AND_OR':'" + $("#TASK_AND_OR").val() + "',";
    strData += "'strPOSITION_IX':'" + $("#POSITION_IX").val() + "',";
    strData += "'strPOSITION_IY':'" + $("#POSITION_IY").val() + "',";
    strData += "'strOPER_VALUE':'" + $("#hidTask_User").val() + "'";
    strData += "}";

    return strData;
}

    //cancel按钮
    function selectCancel(item, dialog) {
        dialog.close();
    }

    //弹出用户grid
    function TaskUser_select() {
        $.ligerDialog.open({ title: '选择用户', name: 'winselector', width: 720, height: 400, url: 'Duty/TaskUser.aspx?selUserId=' + $("#hidTask_User").val() + '&selUser=' + $("#Task_User").val(), buttons: [
                { text: '确定', onclick: TaskUser_selectOK },
                { text: '取消', onclick: selectCancel }
            ]
        });
        return false;
    }

    //用户弹出grid ok按钮
    function TaskUser_selectOK(item, dialog) {
        var fn = dialog.frame.f_select || dialog.frame.window.f_select;
        var data = fn();
        if (!data) {
            $.ligerDialog.warn('请选择用户!');
            return;
        }
        var strs = new Array();
        var str = data;
        strs = str.split("|");

        $("#Task_User").val(strs[1]);
        $("#hidTask_User").val(strs[0]);
        dialog.close();
    }

    //弹出职位grid
    function TaskPost_select() {
        $.ligerDialog.open({ title: '选择用户', name: 'winselector', width: 720, height: 400, url: 'Duty/TaskPost.aspx?selPostId=' + $("#hidTask_Post").val() + '&selPost=' + $("#Task_Post").val(), buttons: [
                { text: '确定', onclick: TaskPost_selectOK },
                { text: '取消', onclick: selectCancel }
            ]
        });
        return false;
    }

    //职位弹出grid ok按钮
    function TaskPost_selectOK(item, dialog) {
        var fn = dialog.frame.f_select || dialog.frame.window.f_select;
        var data = fn();
        if (!data) {
            $.ligerDialog.warn('请选择职位!');
            return;
        }
        var strs = new Array();
        var str = data;
        strs = str.split("|");

        $("#Task_Post").val(strs[1]);
        $("#hidTask_Post").val(strs[0]);
        dialog.close();
    }