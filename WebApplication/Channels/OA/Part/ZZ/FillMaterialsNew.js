///物料采购流程
///创建人：魏林 2013-09-10

var strID = "";
var strStatus = "";
var strWF_ID = "";
var strView = "";

$(document).ready(function () {

    strID = $('#hidId').val();
    strStatus = $('#hidStatus').val();
    strWF_ID = $('#hidWF_ID').val();
    strView = $('#hidView').val();

    $("#btnFileUp").bind("click", function () {
         SaveFWDate();
//        upLoadFile();
    })

    $("#btnFiledownLoad").bind("click", function () {
        downLoadFile();
    })

    //设置页面样式状态
    setStylePage();

    ///附件上传
    function upLoadFile() {
        if (strID == "") {
            return;
        }
        $.ligerDialog.open({ title: '附件上传', width: 800, height: 350,
            buttons: [
            {
                text:
                    '直接下载', onclick: function (item, dialog) {
                        dialog.frame.aa();
                    }
            },
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttMoreFileUpLoad.aspx?filetype=PartFile&id=' + strID 
        });
    }

    ///附件下载
    function downLoadFile() {
        if (strID == "") {
            $.ligerDialog.warn('尚未上传附件');
            return;
        }
        $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
            buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttFileDownLoad.aspx?filetype=PartFile&id=' + strID
        });
    }

    function SaveFWDate() {

        if (strID == "") {
            $.ajax({
                cache: false,
                async: false, //设置是否为异步加载,此处必须
                type: "POST",
                url: "FillMaterialsNew.aspx/savePARTPLANData",
                data: GetPartInputInfo(),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "") {
                        strID = data.d;
                        $("#hidId").val(strID);
                        upLoadFile();
                    }
                },
                error: function (msg) {
                    $.ligerDialog.warn('AJAX数据请求失败！');
                }
            });
        }
        else {
            upLoadFile();
        }
    }

    //得到基本信息保存参数
    function GetPartInputInfo() {
        //string PlanBt, string PlanDept, string PlanContent, string PlanType, string UserID
        var strData = "";
        strData += "{";
        strData += "'PlanBt':'" + $("#PlanBt").val() + "'";
        strData += ",'PlanDept':'" + $("#PlanDept").val() + "'";
        strData += ",'PlanContent':'" + $("#PlanContent").val() + "'";
        strData += ",'PlanType':'" + (strWF_ID == 'OW_PARTPLAN' ? '01' : '02') + "'";
        strData += ",'UserID':'" + $("#hidUserID").val() + "'";

        strData += "}";

        return strData;
    }

    ReplaceDisabledControlsToLabels();
})

function SendSave() {
}

//根据状态设置页面布局
function setStylePage() {
    if (strView == "true")   //浏览状态
    {
        ViewStyle();
    }
    else {
        switch (strStatus) {
            case "0":
                ApplyStyle();
                break;
            case "1":
                Audit1Style();
                break;
            case "2":
                Audit2Style();
                break;
            case "3":
                Audit3Style();
                break;
            case "9":
                ViewStyle();
                break;
        }
    }
}
//浏览模式
function ViewStyle() {
    $("#btnFileUp").attr("style", "display:none");
    $("#divContratSubmit").remove();

    if (strWF_ID != "OW_PARTPLAN") {
        $("#trOfficeOption").attr("style", "display:none");
        $("#trOfficeInfor").attr("style", "display:none");
    }
}
//领料申请模式
function ApplyStyle() {
    $("#trTestOption").attr("style", "display:none");
    $("#trTestPInfor").attr("style", "display:none");
    $("#trOfficeOption").attr("style", "display:none");
    $("#trOfficeInfor").attr("style", "display:none");
    $("#trTechOption").attr("style", "display:none");
    $("#trTechInfor").attr("style", "display:none");
}
//科室主任审核模式
function Audit1Style() {
//    $("#btnFileUp").attr("style", "display:none");
    $("#trOfficeOption").attr("style", "display:none");
    $("#trOfficeInfor").attr("style", "display:none");
    $("#trTechOption").attr("style", "display:none");
    $("#trTechInfor").attr("style", "display:none");
}
//办公室主任审核模式
function Audit2Style() {
//    $("#btnFileUp").attr("style", "display:none");
    $("#trTechOption").attr("style", "display:none");
    $("#trTechInfor").attr("style", "display:none");

    if (strWF_ID != "OW_PARTPLAN") {
        $("#trOfficeOption").attr("style", "display:none");
        $("#trOfficeInfor").attr("style", "display:none");
    }
}
//仓库管理员审核模式
function Audit3Style() {
//    $("#btnFileUp").attr("style", "display:none");

    if (strWF_ID != "OW_PARTPLAN") {
        $("#trOfficeOption").attr("style", "display:none");
        $("#trOfficeInfor").attr("style", "display:none");
    }
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