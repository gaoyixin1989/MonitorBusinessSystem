//流程变量
var wf_inst_task_id = "", wf_inst_id = "", wf_id = "", vFlowInfo = null;
var strtaskID = "", vTrainDetail = null;
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
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Employe/EmployeHander.ashx?action=GetDict&type=dept",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                DeptItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Employe/EmployeHander.ashx?action=GetDict&type=TrainExamType",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vTrainExamType = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Employe/EmployeHander.ashx?action=GetDict&type=TrainType",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vTrainType = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });


    //获取员工档案列表
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Employe/EmployeHander.ashx?action=GetEmployeInfor",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vEmployeList = data.Rows;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

//    var strImgInfor = '<img src="../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="年度培训计划登记"/><span>年度培训计划登记</span>';
//    $(strImgInfor).appendTo(divImgInfor);
    $("#layout1").ligerLayout({ topHeight: 0, allowLeftCollapse: false, allowRightCollapse: false, height: 1000 });

    $("#TrainYear").ligerDateEditor({ format: "yyyy", initValue: GetNowYear(), width: 200 });
    $("#TrainDate").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 200 });
    $("#TrainTo").ligerComboBox({ width: 600 });
    $("#TrainDept").ligerComboBox({ data: DeptItems, width: 200, valueFieldID: 'TrainDept_ID', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false });

    $("#ExamType").ligerComboBox({ data: vTrainExamType, width: 200, valueFieldID: 'ExamType_ID', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false });

    $("#TrainType").ligerComboBox({ data: vTrainType, width: 200, valueFieldID: 'TrainType_ID', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false });

    //获取流程业务参数ID，得到业务ID 和流程ID
    wf_inst_task_id = $.getUrlVar("WF_INST_TASK_ID");
    wf_inst_id = $.getUrlVar("WF_INST_ID");
    wf_id = $.getUrlVar("WF_ID");


    if (wf_inst_task_id != "" & wf_inst_id != "") {

        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../../Mis/Contract/ProgrammingHandler.ashx?action=GetFlowTaskInfo&wf_inst_task_id=" + wf_inst_task_id + "&wf_inst_id=" + wf_inst_id + "&service_key_name=task_id",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    vFlowInfo = data.Rows;
                    strtaskID = vFlowInfo[0].SERVICE_KEY_VALUE;
                    $("#hidTaskId").val(strtaskID);
                    GetTrainInfor();
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
    else {
        $.ligerDialog.warn('流程业务参数错误！'); return;
    }




    function GetTrainInfor() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "TrainHander.ashx?action=GetTrainInfor&strtaskID=" + strtaskID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    vTrainDetail = data.Rows;
                    SetVauleModify();
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }

    $("#trOption").attr("style", " display:");
    function SetVauleModify() {
        $("#TrainBt").val(vTrainDetail[0].TRAIN_BT);
        if (vEmployeList != null) {
            var EmpIdArr = vTrainDetail[0].TRAIN_TO.split(";"); ;
            var strUNames = "", strUId = "";
            for (var i = 0; i < EmpIdArr.length; i++) {
                for (var n = 0; n < vEmployeList.length; n++) {
                    if (vEmployeList[n].ID == EmpIdArr[i]) {
                        strUNames += vEmployeList[n].EMPLOYE_NAME + ";";
                        strUId += vEmployeList[n].ID + ";";
                    }
                }
            }

            $("#TrainTo").val(strUNames.substring(0, strUNames.length - 1));
            $("#hidEmployeID").val(strUId.substring(0, strUId.length - 1));
        }
        $("#TrainInfor").val(vTrainDetail[0].TRAIN_INFO);
        $("#TrainTarger").val(vTrainDetail[0].TRAIN_TARGET);
        $("#TrainDept").ligerGetComboBoxManager().setValue(vTrainDetail[0].DEPT_ID);

        $("#TrainOption").val(vTrainDetail[0].APP_INFO);
        $("#TrainYear").ligerGetDateEditorManager().setValue(vTrainDetail[0].PLAN_YEAR);

        $("#TrainDate").ligerGetDateEditorManager().setValue(toDateFormart(vTrainDetail[0].TRAIN_DATE));

        $("#ExamType").ligerGetComboBoxManager().setValue(vTrainDetail[0].EXAMINE_METHOD);
        $("#TrainType").ligerGetComboBoxManager().setValue(vTrainDetail[0].TRAIN_TYPE);

        $("#TrainBt").ligerTextBox({ disabled: true });
        $("#TrainType").ligerTextBox({ disabled: true });
        $("#TrainTo").ligerTextBox({ disabled: true });
        $("#TrainInfor").ligerTextBox({ disabled: true });
        $("#TrainTarger").ligerTextBox({ disabled: true });
        $("#TrainDept").ligerTextBox({ disabled: true });

        $("#TrainYear").ligerGetDateEditorManager().setDisabled();
        $("#TrainDate").ligerGetDateEditorManager().setDisabled();
        $("#ExamType").ligerGetDateEditorManager().setDisabled();
        $("#TrainType").ligerGetDateEditorManager().setDisabled();

    }
    function GetNowYear() {
        var d = new Date(), str = '';

        str += d.getFullYear();
        return str;
    }
    //JS 获取当前时间
    function currentTime() {

        var d = new Date(), str = '';
        str += d.getFullYear() + '-';
        str += d.getMonth() + 1 + '-';
        str += d.getDate();
        return str;
    }



    $("#btnFiledownLoad").bind("click", function () {
        downLoadFile();
    })


    ///附件下载
    function downLoadFile() {
        if (strtaskID == "") {
            $.ligerDialog.warn('业务ID参数错误');
            return;
        }
        $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
            buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileDownLoad.aspx?filetype=Train&id=' + strtaskID
        });
    }
    function GetFile() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../../Mis/Contract/ProgrammingHandler.ashx?action=GetFile&task_id=" + strtaskID + "&strFileType=Train",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    $("#divDownLoad").attr("style", "display:");
                } else {
                    $("#divDownLoad").attr("style", "display:");
                    $("#btnFileUpLoad").removeAttr("href");
                    $("#btnFileUpLoad").attr("style", "color:Red;");
                    $("#btnFileUpLoad").html("当前培训登记表未上传附件");
                    $("#btnFileUpLoad").unbind("click");
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
})


function toDateFormart(v) {
    var createDate = new Date(Date.parse(v.replace(/-/g, '/')))
    var strData = createDate.getFullYear() + "-";
    strData += (createDate.getMonth() + 1) + "-";
    strData += createDate.getDate();

    return strData;
}


function SendSave() {
    $("#hidLeaderOption").val($("#TrainOption").val());
    $("#hidTaskId").val(strtaskID);
    //防止多次点击按钮产生多个任务
    $("#divContratSubmit")[0].style.display = "none";
}
function BackSend() {
    $("#hidBtnType").val("back");
    //防止多次点击按钮产生多个任务
    $("#divContratSubmit")[0].style.display = "none";
}
