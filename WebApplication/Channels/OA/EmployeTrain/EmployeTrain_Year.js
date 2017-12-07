var wf_inst_task_id = "", wf_inst_id = "", wf_id = "", vFlowInfo = null;
var DeptItems = null, vTrainInforList = null, vTrainInforList = null, vTrainExamType = null, vTrainDetail = null, vTrainType = null;
var isAdd = true, isView = false, strView = "", strType = "", strWf = "", strWFfirst = "", strIsWfStart = "1";
var strTrainType = "", strTrainExamType = "", strEmployeName = "", strEmployeId = "", vEmployeList = null;

var strtaskID = "", strTrainBt = "", strTrainType = "", strTrainTo = "", strTrainInfor = "", strTrainTarger = "", strTrainDate = "", strDept = "", strExamType = "", strPlanYear = "", strDarptId = "",
 strDarptDate = "", strAppId = "", strAppDate = "", strAppInfor = "", strAppResult = "", strFlowStatus = "", strTypes = "1";
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
    $("#TrainTo").ligerComboBox({ onBeforeOpen: f_selectUser, valueFieldID: 'hidEmployeID', width: 600 });
    $("#TrainDept").ligerComboBox({ data: DeptItems, width: 200, valueFieldID: 'TrainDept_ID', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false });

    $("#ExamType").ligerComboBox({ data: vTrainExamType, width: 200, valueFieldID: 'ExamType_ID', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false });

    $("#TrainType").ligerComboBox({ data: vTrainType, width: 200, valueFieldID: 'TrainType_ID', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false });

    //    $("#PageBtnSave").bind("click", function () {
    //        SaveTrainDate();
    //        if (strtaskID != "") {
    //            $("#divFileUp").attr("style", "display:");
    //        }
    //    })
    $("#trOption").attr("style", " display:none");
    strType = $.getUrlVar('type');
    strWf = $.getUrlVar('WF_ID');
    if (!strType) {
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
        SetVauleModify();
        GetFile();
//        $("#divDownLoad").attr("style", "display:");
//        $("#btnFileUp").attr("style", "display:");
        $("#divContratSubmit").attr("style", "");
    } else {
        if (strType == "true") {
            isAdd = true;

            $("#divDownLoad").attr("style", " display:none;");
            $("#divContratSubmit").attr("style", "");
        }
        else {
            isAdd = false;
            strtaskID = $.getUrlVar('strtaskID');
            strView = $.getUrlVar('view');

            GetTrainInfor();
            if (strView == "true") {
                isView = true;
                //            $("#PageBtnSave").remove();
                $("#divContratSubmit").attr("style", "display:none");
            }
            SetVauleModify();
            GetFile();
        }
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
    function SetVauleModify() {
        $("#TrainBt").val(vTrainDetail[0].TRAIN_BT);
        //        $("#TrainType").val(strTrainType);
        //        $("#TrainTo").val(vTrainDetail[0].TRAIN_TO);
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
        if (isView == false) {
            $("#divContratSubmit").attr("style", "");
        }

        if (isView == true) {

            $("#TrainBt").ligerTextBox({ disabled: true });
            $("#TrainType").ligerTextBox({ disabled: true });
            $("#TrainTo").ligerTextBox({ disabled: true });
            $("#TrainInfor").ligerTextBox({ disabled: true });
            $("#TrainTarger").ligerTextBox({ disabled: true });
            $("#TrainDept").ligerTextBox({ disabled: true });
            $("#TrainOption").ligerTextBox({ disabled: true });

            $("#TrainYear").ligerGetDateEditorManager().setDisabled();
            $("#TrainDate").ligerGetDateEditorManager().setDisabled();
            $("#ExamType").ligerGetDateEditorManager().setDisabled();
            $("#TrainType").ligerGetDateEditorManager().setDisabled();
            $("#divContratSubmit").remove();
        }
    }
    function GetNowYear() {
        var d = new Date(), str = '';
        if (isAdd == true) {
            str += d.getFullYear();
        }
        return str;
    }
    //JS 获取当前时间
    function currentTime() {

        var d = new Date(), str = '';
        if (isAdd == true) {
            str += d.getFullYear() + '-';
            str += d.getMonth() + 1 + '-';
            str += d.getDate();
        }
        return str;
    }


    function f_selectUser() {
        $.ligerDialog.open({ title: '员工选择', name: 'winselector', width: 700, height: 400, url: 'GetSelectUserList.aspx', buttons: [
                { text: '确定', onclick: f_selectUserOK },
                { text: '返回', onclick: f_selectUserCancel }
            ]
        });
        return false;
    }

    function f_selectUserOK(item, dialog) {
        var fn = dialog.frame.f_select || dialog.frame.window.f_select;
        var data = fn();
        if (!data) {
            $.ligerDialog.warn('请选择行!');
            return;
        }
        var tempUserId = $("#hidEmployeID").val();
        var tempUserIdArr = tempUserId.split(";");

        for (var i = 0; i < data.length; i++) {
            if ($.inArray(data[i].ID, tempUserIdArr) < 0) {
                strEmployeName += data[i].EMPLOYE_NAME + ";";
                strEmployeId += data[i].ID + ";";
            }
        }

        $("#TrainTo").val(strEmployeName.substring(0, strEmployeName.length - 1));
        $("#hidEmployeID").val(strEmployeId.substring(0, strEmployeId.length - 1));
        dialog.close();
    }

    function f_selectUserCancel(item, dialog) {
        dialog.close();
    }


    $("#TrainBt").attr("validate", "[{required:true, msg:'请输入培训计划主题'},{minlength:2,maxlength:60,msg:'培训主题最小长度为2，最大长度为60'}]");
    $("#TrainTo").attr("validate", "[{required:true, msg:'请选择培训对象'}]");
    $("#TrainDept").attr("validate", "[{required:true, msg:'请选择负责部门'}]");
    $("#TrainInfor").attr("validate", "[{required:true, msg:'请输入培训计划内容'},{minlength:2,maxlength:255,msg:'培训内容最小长度为2，最大长度为255'}]");
    $("#TrainDate").attr("validate", "[{required:true, msg:'请选择培训时间'}]");
    $("#TrainType").attr("validate", "[{required:true, msg:'请选择培训分类'}]");
    $("#ExamType").attr("validate", "[{required:true, msg:'请选择考核方式'}]");

    $("#btnFileUp").bind("click", function () {
        SaveTrainDate();
        upLoadFile();
    })

    $("#btnFiledownLoad").bind("click", function () {
        downLoadFile();
    })
    ///附件上传
    function upLoadFile() {
        if (strtaskID == "") {
            //            $.ligerDialog.warn('业务ID参数错误，请先保存!');
            return;
        }
        $.ligerDialog.open({ title: '附件上传', width: 500, height: 270,
            buttons: [
            {
                text:
                    '上传', onclick: function (item, dialog) {
                        dialog.frame.upLoadFile();
                        GetFile();
                    }
            },
            {
                text:
                 '关闭', onclick: function (item, dialog) { GetFileSataus(item, dialog), dialog.close(); }
            }], url: '../../OA/ATT/AttFileUpload.aspx?filetype=Train&id=' + strtaskID
        });
    }
    function GetFileSataus(item, dialog) {
        var fn = dialog.frame.getUpLoadStatus();
        if (fn == "1") {
            $("#divDownLoad").attr("style", "float:left; margin-right:10px; width:100px; overflow:hidden;");
            $("#divFileUp").attr("style", " float:left; margin-right:10px; width:100px; overflow:hidden;");
        }
        else {
            $("#divFileUp").attr("style", " float:left; margin-right:10px; width:100px; overflow:hidden;");
            $("#divDownLoad").attr("style", " float:left; margin-right:10px; width:100px; overflow:hidden;");
        }
    }
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
                    if (strType) {
                        if (isAdd == false && isView == false) {
                            $("#divDownLoad").attr("style", "float:left; margin-right:10px; width:100px; overflow:hidden;");
                            $("#divFileUp").attr("style", " float:left; margin-right:10px; width:100px; overflow:hidden;");
                            //                        $("#PageBtnSave").remove();
                        }
                        if (isAdd == false && isView == true) {
                            $("#divDownLoad").attr("style", "float:left; margin-right:10px; width:100px; overflow:hidden;");
                            $("#divFileUp").attr("style", " display:none");
                        }
                    }
                    else {
                        if (isAdd == false && isView == false) {
                            $("#divDownLoad").attr("style", "display:none");
                            $("#divFileUp").attr("style", "float:left; margin-right:10px; width:100px; overflow:hidden;");

                        }
                        if (isAdd == false && isView == true) {
                            $("#divDownLoad").attr("style", "display:none");
                            $("#btnFileUp").attr("style", "display:none");
                        }
                    }
                } else {
                    $("#divDownLoad").attr("style", "float:left; margin-right:10px; width:100px; overflow:hidden;");
                    $("#btnFileUp").attr("style", "float:left; margin-right:10px; width:100px; overflow:hidden;");
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }




})

//得到基本信息保存参数
function GetExamInputtInfor() {
    var strDate = "";
    strFlowStatus = "0";
    strTrainBt = encodeURI($("#TrainBt").val());
    strTrainInfor = encodeURI($("#TrainInfor").val());
    strTrainTarger = encodeURI($("#TrainTarger").val());
    strTrainDate = $("#TrainDate").val();
    strTrainTo = $("#hidEmployeID").val();
    strDept = $("#TrainDept_ID").val();
    strExamType = $("#ExamType_ID").val();
    strTrainType = $("#TrainType_ID").val();
    strPlanYear = $("#TrainYear").val();

    strDate += "&strtaskID=" + strtaskID;
    strDate += "&strTypes=" + strTypes;
    strDate += "&strFlowStatus=" + strFlowStatus;
    strDate += "&strTrainBt=" + strTrainBt;
    strDate += "&strTrainDate=" + strTrainDate;
    strDate += "&strTrainExamType=" + strTrainExamType;
    strDate += "&strTrainInfor=" + strTrainInfor;
    strDate += "&strTrainTarger=" + strTrainTarger;
    strDate += "&strTrainType=" + strTrainType;
    strDate += "&strTrainTo=" + strTrainTo;
    strDate += "&strDept=" + strDept;
    strDate += "&strExamType=" + strExamType;
    strDate += "&strPlanYear=" + strPlanYear;
    return strDate;
}

function toDateFormart(v) {
    var createDate = new Date(Date.parse(v.replace(/-/g, '/')))
    var strData = createDate.getFullYear() + "-";
    strData += (createDate.getMonth() + 1) + "-";
    strData += createDate.getDate();

    return strData;
}

function SaveTrainDate() {
    var isTrue = $("#form1").validate();
    if (isTrue) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "TrainHander.ashx?action=SaveTrainDate" + GetExamInputtInfor(),
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    strtaskID = data;
                    $("#hidTaskId").val(strtaskID);
                    $("#hidTaskProjectName").val($("#TrainBt").val());
                    $("#divDownLoad").attr("style", "display:none");
                    $("#divFileUp").attr("style", " display:");
                }
                else {
                    return;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('AJAX数据请求失败！');
                return;
            }
        });
    }
}

function SendSave() {
    SaveTrainDate();
    //防止多次点击按钮产生多个任务
    $("#divContratSubmit")[0].style.display = "none";
}