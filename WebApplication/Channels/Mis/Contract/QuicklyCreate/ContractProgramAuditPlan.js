//Create By 胡方扬 2012-12-10
//监测方案上传

$(document).ready(function () {
    var strdivImg = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="监测方案"/><span>监测方案</span>';
    $(strdivImg).appendTo(divImgPlan);


    $("#btnFileUpLoad").bind("click", function () {
        upLoadFile();
    })

    $("#btnFiledownLoad").bind("click", function () {
        downLoadFile();
    })

    if (strContratId != "") {
        GetFile();
    } else {
        $.ligerDialog.warn('业务参数错误！');
    }

    ///附件上传
    function upLoadFile() {
        if (strContratId == "") {
            $.ligerDialog.warn('业务ID参数错误');
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
            }], url: '../../../OA/ATT/AttFileUpload.aspx?filetype=Contract&id=' + strContratId
        });
    }
    function GetFileSataus(item, dialog) {
        var fn = dialog.frame.getUpLoadStatus();
        if (fn == "1"||fn=="3") {
            $("#divDownLoad").attr("style", "");
        }
        else {
            $("#divDownLoad").attr("style", " display:none");
        }
    }
    ///附件下载
    function downLoadFile() {
        if (strContratId == "") {
            $.ligerDialog.warn('业务ID参数错误');
            return;
        }
        $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
            buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttFileDownLoad.aspx?filetype=Contract&id=' + strContratId
        });
    }
    function GetFile() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../ProgrammingHandler.ashx?action=GetFile&task_id=" + strContratId + "&strFileType=Contract",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    $("#divDownLoad").attr("style", "");
                }
                else {
                    $("#divDownLoad").attr("style", "display:none");
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
})

