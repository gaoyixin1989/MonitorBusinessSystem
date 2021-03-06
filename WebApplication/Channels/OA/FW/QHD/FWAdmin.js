﻿var isAdd = true;
var strFWID = "";
var strUrl = "FWAdmin.aspx";

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
    $("#START_DATE").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 100 });
    $("#END_DATE").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 100 });

    var fwread = $.getUrlVar('fwread');
    if (fwread == "1") {
        $("#divBack").attr("style", "display:none");
    } else if (fwread == "0") {
        $("#divBack2").attr("style", "display:none");
    } else {
        $("#divBack").attr("style", "display:none");
        $("#divBack2").attr("style", "display:none");
    }

    strFWID = $.getUrlVar('fw_id');

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
        SaveFWDate();
        upLoadFile();
    })
    $("#btnFiledownLoad").bind("click", function () {
        downLoadFile();
    })

    ///附件上传
    function upLoadFile() {

        if (strFWID == "") {
            return;
        }
        $.ligerDialog.open({ title: '附件上传', width: 500, height: 270,
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
            }], url: '../../../OA/ATT/AttFileUpload.aspx?filetype=FWFile&id=' + strFWID
        });
    }

    ///附件下载
    function downLoadFile() {
        if (strFWID == "" || strFWID == undefined) {
            $.ligerDialog.warn('还没有附件，请添加附件！');
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

    function SaveFWDate() {
        if (strFWID == "" || strFWID == undefined) {
            $.ajax({
                cache: false,
                async: false, //设置是否为异步加载,此处必须
                type: "POST",
                url: strUrl + "/saveFWData",
                data: GetFWInputtInfo(),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data != "") {
                        strFWID = data.d;
                        $("#hidTaskId").val(strFWID);
                        //                        $("#divDownLoad").attr("style", "display:none");
                        //                        $("#divFileUp").attr("style", " display:");
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

    //得到基本信息保存参数
    function GetFWInputtInfo() {
        var strData = "";
        strData += "{";
        strData += "'strYWNO':'" + $("#YWNO").val() + "'";
        strData += ",'strFWNO':'" + $("#FWNO").val() + "'";
        strData += ",'strFW_TITLE':'" + $("#FW_TITLE").val() + "'";
        strData += ",'strZB_DEPT':'" + $("#ZB_DEPT").val() + "'";
        strData += ",'strDRAFT_ID':'" + $("#USER_ID").val() + "'";
        strData += ",'strMJ':'" + $("#MJ").val() + "'";
        strData += ",'strSTART_DATE':'" + $("#START_DATE").val() + "'";
        strData += ",'strEND_DATE':'" + $("#END_DATE").val() + "'";
        strData += "}";
        return strData;
    }
})

function SendSave() {
    //防止多次点击按钮产生多个任务
    $("#divContratSubmit")[0].style.display = "none";
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