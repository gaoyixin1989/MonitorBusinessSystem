//收文办理 Create By：魏林

var strUrl = "SWHandleRead.aspx";
var strPost_Dept = "";
var struserItems = "";
var struserNames = "";
var strSWID = "";
var Action = "";
var Status = "";

$(document).ready(function () {
    //根据参数改变界面布局
    if (getQueryString("action") != null) {
        if (getQueryString("ID") != null) {
            strSWID = getQueryString("ID");
        }
        Action = getQueryString("action");
        switch (Action) {
            case "Add":         //新增
                $("#hidPreUrl").val("SWList.aspx");
                $("#hidPreTitle").val("收文登记");

                StyleOne()
                break;
            case "Update":      //修改
                $("#hidPreUrl").val("SWList.aspx");
                $("#hidPreTitle").val("收文登记");

                StyleOne();
                break;
            case "ViewPer":        //从收文新增页面进来的查看
                $("#hidPreUrl").val("SWList.aspx");
                $("#hidPreTitle").val("收文登记");

                StyleTwo();
                break;
            case "ViewAll":        //从收文查看页面进来的查看
                $("#hidPreUrl").val("SWAllList.aspx");
                $("#hidPreTitle").val("收文查询");

                StyleTwo();
                break;
            case "Handle":      //办理
                Status = getQueryString("Status");
                $("#hidPreUrl").val("SWHandleList.aspx");
                $("#hidPreTitle").val("收文办理");
                //主任阅示
                if (Status == "1") {
                    StyleThree();
                }
                //站长阅示
                if (Status == "2") {
                    StyleFour();
                }
                //分管阅办
                if (Status == "3") {
                    if ($("#Hid_MakeUserIDs").val() != "")
                        StyleFive();
                    else
                        StyleFive1();
                }
                //科室办结
                if (Status == "4") {
                    StyleSix();
                }
                //完成（归档）
                if (Status == "5") {
                    StyleSeven();
                }
                break;
        }

    }
    ReplaceDisabledControlsToLabels();

    $("#SW_SIGN_DATE").ligerDateEditor({ format: "yyyy-MM-dd", initValue: "", width: 100 });

    //right move
    $("#btnRight").click(function () {
        moveright();
    });
    //double click to move left
    $("#listLeft").dblclick(function () {
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
    //上传附件
    $("#btnFileUp").bind("click", function () {
        //附件上传前先保存数据
        if (strSWID == "" || strSWID == "undefined") {
            SaveData();
        }
        upLoadFile();
    });
    //下载附件
    $("#btnFiledownLoad").bind("click", function () {
        downLoadFile();
    });

    //保存按钮
    $("#btn_Save").bind("click", function () {
        if (SaveData()) {
            $.ligerDialog.success('保存成功', function () {
                BackUrl();
            });
        }
    });

    //返回按钮
    $("#btn_Back").bind("click", function () {
        //self.location = $("#hidPreUrl").val();
        BackUrl();
    });

    //发送按钮
    $("#btn_Send").bind("click", function () {
        if (Action == "Add" || Action == "Update") {         //新增、修改状态的发送
            if (!ValiSWInfo())
                return;
            if (!ValiHandle())
                return;
            if (!SaveData())
                return;
        }
        if (Action == "Handle") {
            if (Status == "1") {                             //主任阅示
                if (!ValiHandle())
                    return;
            }
            if (Status == "2") {                             //站长阅示
                if (!ValiReadMake())
                    return;
            }
            if (Status == "3") {                             //领导阅办
                if (!ValiHandleMake())
                    return;
            }
            if (Status == "4") {                             //科室办结
                if (!ValiHandle())
                    return;
            }
        }

        SendSW();
    });

    //完成按钮
    $("#btn_Finish").bind("click", function () {
        FinishSW();
    });
});

//完成（归档）收文
function FinishSW() {

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: strUrl + "/FinishData",
        data: "{'ID':'" + strSWID + "','Status': '" + Status + "','SwReadIDs':'" + $("#Hid_SwReadUserIDs").val() + "','LoginID':'" + $("#hidUserID").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                $.ligerDialog.success('归档成功', function () {
                    BackUrl();
                });
            }
            else {
                $.ligerDialog.warn('归档失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('AJAX数据请求失败！');
        }
    });

}

//发送收文
function SendSW() {

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: strUrl + "/SendData",
        data: SendSWParams(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                $.ligerDialog.success('发送成功', function () {
                    BackUrl();
                });
            }
            else {
                $.ligerDialog.warn('发送失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('AJAX数据请求失败！');
        }
    });

}

//验证收文基础信息
function ValiSWInfo() {
    if ($("#FROM_CODE").val() == "") {
        $.ligerDialog.warn('原文编号不能为空');
        return false;
    }
    if ($("#SW_TITLE").val() == "") {
        $.ligerDialog.warn('文件标题不能为空');
        return false;
    }
    if ($("#SW_CODE").val() == "") {
        $.ligerDialog.warn('收文字号不能为空');
        return false;
    }
    return true;
}
//验证发送操作人
function ValiHandle() {
    if ($("#HandlerList").val() == "") {
        $.ligerDialog.warn('请选择下一操作人');
        return false;
    }
    return true;
}
//验证阅办人和办结人
function ValiReadMake() {
    if ($("#Hid_ReadUserIDs").val() == "" && $("#Hid_MakeUserIDs").val() == "") {
        $.ligerDialog.warn('阅办人和办结人不能同时为空');
        return false;
    }
    return true;
}
//验证操作人和办结人
function ValiHandleMake() {
    if ($("#HandlerList").val() == "" && $("#Hid_MakeUserIDs").val() == "") {
        $.ligerDialog.warn('操作人和办结人不能同时为空');
        return false;
    }
    return true;
}

//返回上一页面
function BackUrl() {
    var surl = '../Channels/OA/SW/ZZ/' + $("#hidPreUrl").val();
    top.f_overTab($("#hidPreTitle").val(), surl);
}

///附件上传
function upLoadFile() {
    if (strSWID == "") {
        return;
    }
    $.ligerDialog.open({ title: '附件上传', width: 500, height: 270, isHidden: false,
        buttons: [
            {
                text:
                    '上传', onclick: function (item, dialog) {
                        dialog.frame.upLoadFile();
                    }
            },
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttFileUpload.aspx?filetype=SWFile&id=' + strSWID
    });
}

///附件下载
function downLoadFile() {
    if (strSWID == "" || strSWID == "undefined") {
        $.ligerDialog.warn('尚未上传附件');
        return;
    }
    $.ligerDialog.open({ title: '附件下载', width: 500, height: 270, isHidden: false,
        buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttFileDownLoad.aspx?filetype=SWFile&id=' + strSWID
    });
}

//保存收文数据
function SaveData() {
    var b = false;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: strUrl + "/SaveData",
        data: GetSWInputtInfo(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                strSWID = data.d;
                $("#hidTaskId").val(strSWID);
                b = true;
            }
            else {
                $.ligerDialog.warn('保存失败！');
                b = false;
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('AJAX数据请求失败！');
            b = false;
        }
    });

    return b;
}

//得到基本信息保存参数
function GetSWInputtInfo() {
    var strData = "";

    strData += "{";
    strData += "'LoginID':'" + $("#hidUserID").val() + "'";
    strData += ",'strID':'" + strSWID + "'";
    strData += ",'strFromCode':'" + $("#FROM_CODE").val() + "'";
    strData += ",'strSwCode':'" + $("#SW_CODE").val() + "'";
    strData += ",'strSwFrom':'" + $("#SW_FROM").val() + "'";
    strData += ",'strSwCount':'" + $("#SW_COUNT").val() + "'";
    strData += ",'strSwMJ':'" + $("#MJ").val() + "'";
    strData += ",'strSwTitle':'" + $("#SW_TITLE").val() + "'";
    strData += ",'strSubWord':'" + $("#SUBJECT_WORD").val() + "'";
    strData += ",'strSwSignID':'" + $("#SW_SIGN_ID").val() + "'";
    strData += ",'strSwSignDate':'" + $("#SW_SIGN_DATE").val() + "'";
    strData += "}";
    return strData;
}

//发送收文参数
function SendSWParams() {
    var strData = "";

    strData += "{";
    strData += "'LoginID':'" + $("#hidUserID").val() + "'";
    strData += ",'Action':'" + Action + "'";
    strData += ",'ID':'" + strSWID + "'";
    strData += ",'Status':'" + Status + "'";
    strData += ",'Reader':'" + $("#Hid_ReadUserIDs").val() + "'";
    strData += ",'Maker':'" + $("#Hid_MakeUserIDs").val() + "'";
    strData += ",'Handler':'" + $("#HandlerList").val() + "'";
    strData += ",'SW_PLAN2':'" + $("#SW_PLAN2_INFO").val() + "'";
    strData += ",'SW_PLAN3':'" + $("#SW_PLAN3_INFO").val() + "'";
    strData += ",'SW_PLAN4':'" + $("#SW_PLAN4_INFO").val() + "'";
    strData += ",'SW_PLAN5':'" + $("#SW_PLAN5_INFO").val() + "'";
    strData += "}";

    return strData;
}

//弹出批办意见查看框
var SuggionDiv = null;
function ShowSuggion(s) {
    var Title = "";
    if (s == '3')
        Title = '阅办意见查看';
    else
        Title = '办结意见查看';

    SetSuggion(s);
    SuggionDiv = $.ligerDialog.open({ target: $("#divSuggion"), title: Title, name: 'suggion', width: 480, height: 400,
        buttons: [
                { text: '确定', onclick: function (item, text) {
                    SuggionDiv.hide();
                }
                }
            ]
    });
}
//批办人意见
function SetSuggion(s) {
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/GetSuggions",
        data: "{'strSWID':'" + strSWID + "','strS':'" + s + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != null) {
                $("#divSugDetail")[0].innerText = data.d;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax加载数据失败！');
        }
    });
}


//弹出人员选择框
var mtardiv = null;
function ACCEPT_REALNAMES_select(Names, IDs) {
    SetControlValue(IDs);
    mtardiv = $.ligerDialog.open({ target: $("#targerdiv"), title: '收文传阅人员设置', name: 'winselector', width: 480, height: 400,
        buttons: [
                { text: '确定', onclick: function (item, text) {
                    GetItemsValue();
                    if (struserItems != "") {
                        ACCEPT_REALNAMES_selectOK(Names, IDs);
                        mtardiv.hide();
                    }
                    else {
                        $.ligerDialog.warn('请选择用户！');
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

function SetControlValue(IDs) {
    $("#listLeft option").remove();
    $("#listRight option").remove();
    GeUserDeptItems(IDs);
    GetSubUserItems(strPost_Dept, IDs);
    GetSelectUserItems(IDs);
    $("#Dept").ligerGetComboBoxManager().setValue("");
}

//初始化部门的ComBox
var DeptComBoxItem = null;
function GeUserDeptItems(IDs) {
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/GetDeptItems",
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
        //seletComboxValue = newvalue;
        //初始化listBox
        $("#listLeft option").remove();
        $("#listRight option").remove();
        GetSubUserItems(newvalue, IDs);
        GetSelectUserItems(IDs);
    }
    });
}

//初始化左侧ListBox
var vItemData = null;
function GetSubUserItems(strDept, IDs) {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: strUrl + "/GetSubUserItems",
        data: "{'strPost_Dept':'" + strDept + "','strUserID':'" + $("#" + IDs).val() + "'}",
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
}
//初始化右侧ListBox
var vSelectedData = null;
function GetSelectUserItems(IDs) {
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/GetSelectUserItems",
        data: "{'strUserID':'" + $("#" + IDs).val() + "'}",
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
function ACCEPT_REALNAMES_selectOK(Names, IDs) {
    $("#" + IDs).val(struserItems.substring(0, struserItems.length - 1));
    $("#" + Names).val(struserNames.substring(0, struserNames.length - 1));
}

function GetItemsValue() {
    struserItems = "";
    struserNames = "";
    $("#listRight option").each(function () {
        struserItems += $(this).val() + ",";
        struserNames += $(this).text() + "，";
    });
}

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
//新增、修改状态样式
function StyleOne() {
    $("#SW_PLAN2_INFO").attr("disabled", "disabled");
    $("#SW_PLAN3_INFO").attr("disabled", "disabled");
    $("#SW_PLAN4_INFO").attr("disabled", "disabled");
    $("#SW_PLAN5_INFO").attr("disabled", "disabled");

    $("#TReadUserList").attr("style", "display:none");
    $("#TMakeUserList").attr("style", "display:none");
    $("#btn_Finish").attr("style", "display:none");
    $("#btn_Print").attr("style", "display:none");

    $("#TdReadList").attr("style", "display:none");
    $("#TdMakeList").attr("style", "display:none");
}
//查看状态的样式
function StyleTwo() {
    $("#FROM_CODE").attr("disabled", "disabled");
    $("#SW_TITLE").attr("disabled", "disabled");
    $("#SUBJECT_WORD").attr("disabled", "disabled");
    $("#SW_FROM").attr("disabled", "disabled");
    $("#SW_COUNT").attr("disabled", "disabled");
    $("#MJ").attr("disabled", "disabled");
    $("#SW_SIGN_ID").attr("disabled", "disabled");
    $("#SW_SIGN_DATE").attr("disabled", "disabled");
    $("#SW_CODE").attr("disabled", "disabled");

    $("#ReadUserNames").attr("disabled", "disabled");
    $("#MakeUserNames").attr("disabled", "disabled");

    $("#SW_PLAN2_INFO").attr("disabled", "disabled");
    $("#SW_PLAN3_INFO").attr("disabled", "disabled");
    $("#SW_PLAN4_INFO").attr("disabled", "disabled");
    $("#SW_PLAN5_INFO").attr("disabled", "disabled");

    $("#btnFileUp").attr("style", "display:none");
    $("#trHandlerList").attr("style", "display:none");
    $("#btn_Save").attr("style", "display:none");
    $("#btn_Send").attr("style", "display:none");
    $("#btn_Finish").attr("style", "display:none");
}
//主任阅示——》站长批示状态的样式
function StyleThree() {
    $("#FROM_CODE").attr("disabled", "disabled");
    $("#SW_TITLE").attr("disabled", "disabled");
    $("#SUBJECT_WORD").attr("disabled", "disabled");
    $("#SW_FROM").attr("disabled", "disabled");
    $("#SW_COUNT").attr("disabled", "disabled");
    $("#MJ").attr("disabled", "disabled");
    $("#SW_SIGN_ID").attr("disabled", "disabled");
    $("#SW_SIGN_DATE").attr("disabled", "disabled");
    $("#SW_CODE").attr("disabled", "disabled");

    $("#SW_PLAN3_INFO").attr("disabled", "disabled");
    $("#SW_PLAN4_INFO").attr("disabled", "disabled");
    $("#SW_PLAN5_INFO").attr("disabled", "disabled");

    $("#TReadUserList").attr("style", "display:none");
    $("#TMakeUserList").attr("style", "display:none");
    $("#btnFileUp").attr("style", "display:none");
    $("#btn_Save").attr("style", "display:none");
    $("#btn_Finish").attr("style", "display:none");

    $("#TdReadList").attr("style", "display:none");
    $("#TdMakeList").attr("style", "display:none");
}
//站长批示——》分管阅办、科室办结状态的样式
function StyleFour() {
    $("#FROM_CODE").attr("disabled", "disabled");
    $("#SW_TITLE").attr("disabled", "disabled");
    $("#SUBJECT_WORD").attr("disabled", "disabled");
    $("#SW_FROM").attr("disabled", "disabled");
    $("#SW_COUNT").attr("disabled", "disabled");
    $("#MJ").attr("disabled", "disabled");
    $("#SW_SIGN_ID").attr("disabled", "disabled");
    $("#SW_SIGN_DATE").attr("disabled", "disabled");
    $("#SW_CODE").attr("disabled", "disabled");

    $("#SW_PLAN2_INFO").attr("disabled", "disabled");
    $("#SW_PLAN4_INFO").attr("disabled", "disabled");
    $("#SW_PLAN5_INFO").attr("disabled", "disabled");

    $("#btnFileUp").attr("style", "display:none");
    $("#trHandlerList").attr("style", "display:none");
    $("#btn_Save").attr("style", "display:none");
    $("#btn_Finish").attr("style", "display:none");

    $("#TdReadList").attr("style", "display:none");
    $("#TdMakeList").attr("style", "display:none");
}
//分管阅办——》科室办结状态的样式
function StyleFive() {
    $("#FROM_CODE").attr("disabled", "disabled");
    $("#SW_TITLE").attr("disabled", "disabled");
    $("#SUBJECT_WORD").attr("disabled", "disabled");
    $("#SW_FROM").attr("disabled", "disabled");
    $("#SW_COUNT").attr("disabled", "disabled");
    $("#MJ").attr("disabled", "disabled");
    $("#SW_SIGN_ID").attr("disabled", "disabled");
    $("#SW_SIGN_DATE").attr("disabled", "disabled");
    $("#SW_CODE").attr("disabled", "disabled");

    $("#ReadUserNames").attr("disabled", "disabled");

    $("#SW_PLAN2_INFO").attr("disabled", "disabled");
    $("#SW_PLAN3_INFO").attr("disabled", "disabled");
    $("#SW_PLAN4_INFO").val("");
    $("#SW_PLAN5_INFO").attr("disabled", "disabled");

    $("#trHandlerList").attr("style", "display:none");
    $("#btnFileUp").attr("style", "display:none");
    $("#btn_Save").attr("style", "display:none");
    $("#btn_Finish").attr("style", "display:none");

    $("#TdMakeList").attr("style", "display:none");
}
//分管阅办——》完成状态的样式
function StyleFive1() {
    $("#FROM_CODE").attr("disabled", "disabled");
    $("#SW_TITLE").attr("disabled", "disabled");
    $("#SUBJECT_WORD").attr("disabled", "disabled");
    $("#SW_FROM").attr("disabled", "disabled");
    $("#SW_COUNT").attr("disabled", "disabled");
    $("#MJ").attr("disabled", "disabled");
    $("#SW_SIGN_ID").attr("disabled", "disabled");
    $("#SW_SIGN_DATE").attr("disabled", "disabled");
    $("#SW_CODE").attr("disabled", "disabled");

    $("#ReadUserNames").attr("disabled", "disabled");
    $("#MakeUserNames").attr("disabled", "disabled");

    $("#SW_PLAN2_INFO").attr("disabled", "disabled");
    $("#SW_PLAN3_INFO").attr("disabled", "disabled");
    $("#SW_PLAN4_INFO").val("");
    $("#SW_PLAN5_INFO").attr("disabled", "disabled");

    $("#btnFileUp").attr("style", "display:none");
    $("#btn_Save").attr("style", "display:none");
    $("#btn_Finish").attr("style", "display:none");

    $("#TdMakeList").attr("style", "display:none");
}
//科室办结——》完成状态的样式
function StyleSix() {
    $("#FROM_CODE").attr("disabled", "disabled");
    $("#SW_TITLE").attr("disabled", "disabled");
    $("#SUBJECT_WORD").attr("disabled", "disabled");
    $("#SW_FROM").attr("disabled", "disabled");
    $("#SW_COUNT").attr("disabled", "disabled");
    $("#MJ").attr("disabled", "disabled");
    $("#SW_SIGN_ID").attr("disabled", "disabled");
    $("#SW_SIGN_DATE").attr("disabled", "disabled");
    $("#SW_CODE").attr("disabled", "disabled");

    $("#ReadUserNames").attr("disabled", "disabled");
    $("#MakeUserNames").attr("disabled", "disabled");

    $("#SW_PLAN2_INFO").attr("disabled", "disabled");
    $("#SW_PLAN3_INFO").attr("disabled", "disabled");
    $("#SW_PLAN4_INFO").attr("disabled", "disabled");
    $("#SW_PLAN5_INFO").val("");

    $("#btnFileUp").attr("style", "display:none");
    $("#btn_Save").attr("style", "display:none");
    $("#btn_Finish").attr("style", "display:none");
}
//完成（归档）状态的样式
function StyleSeven() {
    $("#FROM_CODE").attr("disabled", "disabled");
    $("#SW_TITLE").attr("disabled", "disabled");
    $("#SUBJECT_WORD").attr("disabled", "disabled");
    $("#SW_FROM").attr("disabled", "disabled");
    $("#SW_COUNT").attr("disabled", "disabled");
    $("#MJ").attr("disabled", "disabled");
    $("#SW_SIGN_ID").attr("disabled", "disabled");
    $("#SW_SIGN_DATE").attr("disabled", "disabled");
    $("#SW_CODE").attr("disabled", "disabled");

    $("#ReadUserNames").attr("disabled", "disabled");
    $("#MakeUserNames").attr("disabled", "disabled");

    $("#SW_PLAN2_INFO").attr("disabled", "disabled");
    $("#SW_PLAN3_INFO").attr("disabled", "disabled");
    $("#SW_PLAN4_INFO").attr("disabled", "disabled");
    $("#SW_PLAN5_INFO").attr("disabled", "disabled");

    $("#btnFileUp").attr("style", "display:none");
    $("#btn_Save").attr("style", "display:none");
    $("#btn_Send").attr("style", "display:none");
    $("#trHandlerList").attr("style", "display:none");
}

//将不可用控件置换为标签
function ReplaceDisabledControlsToLabels() {
    var ListInput = document.getElementsByTagName("input");
    var ListSelect = document.getElementsByTagName("select");
    var ListText = ""; // document.getElementsByTagName("textarea");
    var ListSpan = document.getElementsByTagName("span");

    for (var i = 0; i < ListInput.length; i++) {
        if (ListInput[i] != undefined) {
            if (ListInput[i].disabled == true && ListInput[i].type != "hidden" && ListInput[i].type != "submit" && ListInput[i].type != "button" && ListInput[i].type != "reset" && ListInput[i].type != "radio") {
                if (ListInput[i].type == "checkbox") {
                    if (ListInput[i].checked == false) {
                        ListInput[i].outerHTML = "不";
                    }
                    else {
                        ListInput[i].outerHTML = "";
                    }
                }
                else {
                    ListInput[i].outerHTML = ListInput[i].value;
                }
                i = -1;
            }

        }
    }

    for (var j = 0; j < ListSelect.length; j++) {
        if (ListSelect[j] != undefined) {
            if (ListSelect[j].disabled == true) {
                if (ListSelect[j].selectedIndex != "-1") {
                    if (ListSelect[j][ListSelect[j].selectedIndex].text == "请选择") {
                        ListSelect[j].outerHTML = "暂无";
                    }
                    else {
                        ListSelect[j].outerHTML = ListSelect[j][ListSelect[j].selectedIndex].text;
                    }
                    j = -1;
                }
            }
        }
    }

    for (var k = 0; k < ListText.length; k++) {
        if (ListText[k] != undefined) {
            if (ListText[k].disabled == true) {
                ListText[k].outerHTML = ListText[k].value;
                k = -1;
            }
        }
    }

    for (var m = 0; m < ListSpan.length; m++) {
        if (ListSpan[m] != undefined) {
            if (ListSpan[m].disabled == true) {
                ListSpan[m].disabled = false;
            }
            if (ListSpan[m].outerText == '*') {
                ListSpan[m].outerHTML = "";
                m = -1;
            }
        }
    }
} 