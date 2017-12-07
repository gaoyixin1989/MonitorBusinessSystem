//创建日期：2013-6-26
// 创建人  ：李焕明

var isAdd = true;
var strFWID = "";
var strUrl = "FWAdmin.aspx";
var row_ID = "";
var type = "";
$(document).ready(function () {

    $("#START_DATE").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 100 });
    $("#END_DATE").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 100 });

    strFWID = $('#hidTaskId').val();

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

    $("#btnFileUp").bind("click", function () {
        upLoadFile();
        //SaveFWDate();
    })
    
        $("#btnFiledownLoad").bind("click", function () {
            downLoadFile();
        })
  


    function SaveFWDate() {
        if (strFWID == "") {
            $.ajax({
                cache: false,
                async: false, //设置是否为异步加载,此处必须
                type: "POST",
                url: strUrl + "/SaveFWData",
                data: GetFWInputtInfo(),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {

                    if (data.d != "") {

                        strFWID = data.d;
                        $("#hidTaskId").val(strFWID);

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
    function GetFWInputtInfo() {
        var strData = "";
        strData += "{";
        strData += "'strYWNO':'" + $("#YWNO").val() + "'";
        strData += ",'strFW_TITLE':'" + $("#FW_TITLE").val() + "'";
        strData += ",'strZB_DEPT':'" + $("#ZB_DEPT").val() + "'";
        strData += ",'strDRAFT_ID':'" + $("#USER_ID").val() + "'";
        strData += ",'strMJ':'" + $("#MJ").val() + "'";

        strData += ",'strZS_DEPT':'" + $("#ZS_DEPT").val() + "'";
        strData += ",'strCB_DEPT':'" + $("#CB_DEPT").val() + "'";
        strData += ",'strCS_DEPT':'" + $("#CS_DEPT").val() + "'";
        strData += ",'strREMARK1':'" + $("#REMARK1").val() + "'";
        strData += ",'strSUBJECT_WORD':'" + $("#SUBJECT_WORD").val() + "'";

        strData += "}";

        return strData;
    }

    $("#btn_Back").bind("click", function () {
        if ($("#hidView").val() == "true1") {
            //location = 'FWList.aspx';
            var surl = '../Channels/OA/FW/ZZ/FWList.aspx';
            top.f_overTab('发文列表', surl);
        }
        else {
            //location = 'FWAllList.aspx';
            var surl = '../Channels/OA/FW/ZZ/FWAllList.aspx';
            top.f_overTab('发文查看', surl);
        }
    });
})

function SendSave() {
//    $.ligerDialog.confirm('您确定要将该监测项目发送至下一环节吗？', function (yes) {
//        if (yes == true) {
//            return true;
//        }
//        else {
//            return false;
//        }
    //    });
    if (confirm("确定发送到下一环节！")) {
        return true;
    }
    else {
        return false;
    }
}
function Save() {
    $.ligerDialog.warn("数据保存成功！");
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


///附件上传
function upLoadFile() {
    var wf = $("#hid_FwId").val();//发文保存时ID 
    row_ID = getQueryString("fw_id");
    if (wf == "" && row_ID == "") {
        return;
    }
    if (row_ID == null) {
        if (wf != "") {
            row_ID = wf;
        }
        else {
            return;
        }
    }

    $.ligerDialog.open({ title: '附件上传', width: 800, height: 350, isHidden: false,
        buttons: [
             { text: '直接下载', onclick: function (item, dialog) {
                 dialog.frame.aa(); //调用下载按钮
             }
             },
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttMoreFileUpLoad.aspx?filetype=FWFiles&id=' + row_ID  
    });
}

///附件下载
function downLoadFile() {
    if (strFWID == "") {
        $.ligerDialog.warn('尚未上传附件');
        return;
    }
    $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
        buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttFileDownLoad.aspx?filetype=FWFile&id=' + strFWID
        });
 }

 //打印数据,huangjinjun 20140509
function btnPrint() {
    var id = $("#hidTaskId").val();
    if (id == "") {
        $.ligerDialog.warn("请先保存数据！");
        return false;
    }
}