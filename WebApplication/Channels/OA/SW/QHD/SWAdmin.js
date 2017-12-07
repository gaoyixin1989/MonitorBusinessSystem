var isAdd = true;
var strSWID = "";
var strUrl = "SWAdmin.aspx";

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
    $("#SW_SIGN_DATE").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 100 });
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
        SaveSWDate();
        upLoadFile();
    })
    $("#btnFiledownLoad").bind("click", function () {
        downLoadFile();
    })

    ///附件上传
    function upLoadFile() {
        if (strSWID == "") {
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
            }], url: '../../../OA/ATT/AttFileUpload.aspx?filetype=SWFile&id=' + strSWID
        });
    }

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
            }], url: '../../../OA/ATT/AttFileDownLoad.aspx?filetype=SWFile&id=' + strSWID
        });
    }

    function SaveSWDate() {
        if (strSWID == "" || strSWID == "undefined") {
            $.ajax({
                cache: false,
                async: false, //设置是否为异步加载,此处必须
                type: "POST",
                url: strUrl + "/saveSWData",
                data: GetSWInputtInfo(),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data != "") {
                        strSWID = data;
                        $("#hidTaskId").val(strSWID);
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

    //得到基本信息保存参数
    function GetSWInputtInfo() {
        var strData = "";
        strData += "{";
        strData += "'strFROM_CODE':'" + $("#FROM_CODE").val() + "'";
        strData += ",'strSW_CODE':'" + $("#SW_CODE").val() + "'";
        strData += ",'strSW_TITLE':'" + $("#SW_TITLE").val() + "'";
        strData += ",'strSW_FROM':'" + $("#SW_FROM").val() + "'";
        strData += ",'strSW_COUNT':'" + $("#SW_COUNT").val() + "'";
        strData += ",'strMJ':'" + $("#MJ").val() + "'";
        strData += ",'strSW_SIGN_ID':'" + $("#SW_SIGN_ID").val() + "'";
        strData += ",'strSW_SIGN_DATE':'" + $("#SW_SIGN_DATE").val() + "'";
        strData += "}";
        return strData;
    }
})

function SendSave() {
}