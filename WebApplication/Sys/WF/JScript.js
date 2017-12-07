/* Start 胡方扬 2013-02-23  原因：实现用户选择跳转环节，根据跳转环节更新环节人员*/
$(document).ready(function () {
    var hidSelectUserId = $("#wfControl_chkbxlstSelectUsers option:selected").val() || $("#wfControl_chkbxlstSelectUsers option:selected").val();
    $("#wfControl_hidSelectUserId").val(hidSelectUserId);
    var TempUserArr = [];
    //绑定环节选择事件
    $("#wfControl_STEP_NAME").bind("change", function () {
        change_event();
    })
    $("#wfControl_chkbxlstSelectUsers").bind("change", function () {
        changeUser_event();
    })
    function setUserListDropListValue(strurl, strUserArr) {
        TempUserArr = [];
        if (strUserArr.length < 0) {
            return;
        } else {
            for (var i = 0; i < strUserArr.length; i++) {
                getUserInfor(strurl, strUserArr[i]);
            }
            CreatOptionItems(TempUserArr);
        }
    }

    function getUserInfor(strurl, strUserID) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "" + strurl + "?action=getUserInfor&strUserId=" + strUserID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    if (data.Rows.length > 0) {
                        TempUserArr.push(data.Rows[0]);
                    }
                }
            },
            error: function (msg) {
                return;
            }
        });
    }

    function CreatOptionItems(strArryList) {
        //清除原有的用户数据
        $("#wfControl_chkbxlstSelectUsers option").each(function () {
            $(this).remove();
        })
        //添加新数据
        for (var i = 0; i < strArryList.length; i++) {
            $("#wfControl_chkbxlstSelectUsers").append($("<option value='" + strArryList[i].ID + "'>" + strArryList[i].REAL_NAME + "</option>"));
        }
        hidSelectUserId = $("#wfControl_chkbxlstSelectUsers option:selected").val() || $("#wfControl_chkbxlstSelectUsers option:selected").val();
        $("#wfControl_hidSelectUserId").val(hidSelectUserId);
    }

    function change_event() {
        var strWfId = $("#cphInput_wfControl_hiddWF_ID").val() || $("#wfControl_hiddWF_ID").val();
        var strSetpValue = $("#cphInput_wfControl_STEP_NAME option:selected").val() || $("#wfControl_STEP_NAME option:selected").val();
        var strAjaxUr = $("#cphInput_wfControl_hiddAjaxUrl").val() || $("#wfControl_hiddAjaxUrl").val();
        GetUserListWF(strAjaxUr, strWfId, strSetpValue);
    }

    function changeUser_event() {
        hidSelectUserId = $("#wfControl_chkbxlstSelectUsers option:selected").val() || $("#wfControl_chkbxlstSelectUsers option:selected").val();
        $("#wfControl_hidSelectUserId").val(hidSelectUserId);
    }
    function GetUserListWF(strurl, wfid, setpValue) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "" + strurl + "?action=SelectedChange&strWfId=" + wfid + "&strSetpValue=" + setpValue,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    var strUserList = data.split('|');
                    setUserListDropListValue(strurl, strUserList);
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
})