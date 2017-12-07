//创建日期：2013-6-26
// 创建人  ：李焕明

var isAdd = true;
var strFWID = "";

//$.extend({
//    getUrlVars: function () {
//        var vars = [], hash;
//        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
//        for (var i = 0; i < hashes.length; i++) {
//            hash = hashes[i].split('=');
//            vars.push(hash[0]);
//            vars[hash[0]] = hash[1];
//        }
//        return vars;
//    },
//    getUrlVar: function (name) {
//        return $.getUrlVars()[name];
//    }
//});

$(document).ready(function () {

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

    $("#btnFiledownLoad").bind("click", function () {
        downLoadFile();
    })

    ///附件查看
    function downLoadFile() {
        if (strFWID == "") {
            $.ligerDialog.warn('业务ID参数错误');
            return;
        }
        $.ligerDialog.open({ title: '附件下载', width: 800, height: 350,
            buttons: [
                  { text: '直接下载', onclick: function (item, dialog) {
                 dialog.frame.aa(); //调用下载按钮
                  }
                  },
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttMoreFileUpLoad.aspx?filetype=FWFiles&id=' + strFWID 
        });
    }
})

function BackSend() {
    $("#hidBtnType").val("back");
}

function SendSave() {
}