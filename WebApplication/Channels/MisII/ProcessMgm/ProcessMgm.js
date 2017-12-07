
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

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

   // var strPlanId = $.getUrlVar('strPlanId');
    var strPlanId = $("#strPlanId").val();
    //alert(strPlanId);
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "ProcessMgm.aspx?type=createDate&strPlanId=" + strPlanId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {

                $.each(data, function (index, val) {
                    $("#" + index).val(val);
                });
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax加载监测类别数据失败！');
        }
    });


    $("#taskDateStart").ligerDateEditor({ width: 200, onChangeDate: function (value) {

        var strType = "TASK_DATE_TOTAL";
        var strStart = $("#taskDateStart").val();
        var strFinish = $("#taskDateFinish").val();
        var bStartMofified = "true";
        saveDate(strType, strStart, strFinish, bStartMofified);
    }
    });

    $("#taskDateFinish").ligerDateEditor({ width: 200, onChangeDate: function (value) {
        var strType = "TASK_DATE_TOTAL";
        var strStart = $("#taskDateStart").val();
        var strFinish = $("#taskDateFinish").val();
        var bStartMofified = "false";
        saveDate(strType, strStart, strFinish, bStartMofified);
    }
    });

    $("#sampleDateStart").ligerDateEditor({ width: 200, onChangeDate: function (value) {
        var strType = "SAMPLE_DATE";
        var strStart = $("#sampleDateStart").val();
        var strFinish = $("#sampleDateFinish").val();
        var bStartMofified = "true";
        saveDate(strType, strStart, strFinish, bStartMofified);
    }
    });

    $("#sampleDateFinish").ligerDateEditor({ width: 200, onChangeDate: function (value) {
        var strType = "SAMPLE_DATE";
        var strStart = $("#sampleDateStart").val();
        var strFinish = $("#sampleDateFinish").val();
        var bStartMofified = "false";
        saveDate(strType, strStart, strFinish, bStartMofified);
    }
    });

    $("#analyseDateStart").ligerDateEditor({ width: 200, onChangeDate: function (value) {

        var strType = "ANALYSE_DATE";
        var strStart = $("#analyseDateStart").val();
        var strFinish = $("#analyseDateFinish").val();
        var bStartMofified = "true";
        saveDate(strType, strStart, strFinish, bStartMofified);
    }
    });

    $("#analyseDateFinish").ligerDateEditor({ width: 200, onChangeDate: function (value) {
        var strType = "ANALYSE_DATE";
        var strStart = $("#analyseDateStart").val();
        var strFinish = $("#analyseDateFinish").val();
        var bStartMofified = "false";
        saveDate(strType, strStart, strFinish, bStartMofified);
    }
    });

    $("#auditLabLeaderFinish").ligerDateEditor({ width: 200, onChangeDate: function (value) {

        var strType = "AUDIT_LAB_LEADER";
        var strStart = "";
        var strFinish = $("#auditLabLeaderFinish").val();
        var bStartMofified = "false";
        saveDate(strType, strStart, strFinish, bStartMofified);
    }
    });

    $("#auditQCFinish").ligerDateEditor({ width: 200, onChangeDate: function (value) {

        var strType = "AUDIT_QC";
        var strStart = "";
        var strFinish = $("#auditQCFinish").val();
        var bStartMofified = "false";
        saveDate(strType, strStart, strFinish, bStartMofified);
    }
    });

    $("#auditCaptionFinish").ligerDateEditor({ width: 200, onChangeDate: function (value) {

        var strType = "AUDIT_CAPTION";
        var strStart = "";
        var strFinish = $("#auditCaptionFinish").val();
        var bStartMofified = false;
        saveDate(strType, strStart, strFinish, bStartMofified);
    }
    });

    $("#auditQCLeaderFinish").ligerDateEditor({ width: 200, onChangeDate: function (value) {
        var strType = "AUDIT_QC_LEADER";
        var strStart = "";
        var strFinish = $("#auditQCLeaderFinish").val();
        var bStartMofified = false;
        saveDate(strType, strStart, strFinish, bStartMofified);
    }
    });

    $("#AuditTechLeaderFinish").ligerDateEditor({ width: 200, onChangeDate: function (value) {

        var strType = "AUDIT_TECH_LEADER";
        var strStart = "";
        var strFinish = $("#AuditTechLeaderFinish").val();
        var bStartMofified = false;
        saveDate(strType, strStart, strFinish, bStartMofified);
    }
    });

    function TogetDate(date) {
        var strD = "";
        var thisYear = date.getYear();
        thisYear = (thisYear < 1900) ? (1900 + thisYear) : thisYear;
        var thisMonth = date.getMonth() + 1;
        //如果月份长度是一位则前面补0    
        if (thisMonth < 10) thisMonth = "0" + thisMonth;
        var thisDay = date.getDate();
        //如果天的长度是一位则前面补0    
        if (thisDay < 10) thisDay = "0" + thisDay;
        {

            strD = thisYear + "-" + thisMonth + "-" + thisDay;
        }
        return strD;
    }

    function TogetAfterDate(date, afterDate) {

        date = date.valueOf();
        date = date + afterDate * 24 * 60 * 60 * 1000;
        date = new Date(date);

        return TogetDate(date);
    }

    // 保存数据到数据库
    function saveDate(strType, strStart, strFinish, bStartMofified) {
        var strReturn;
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "ProcessMgm.aspx?type=saveDate&strType=" + strType + "&strStart=" + strStart + "&strFinish=" + strFinish + "&bStartMofified=" + bStartMofified + "&strPlanId=" + strPlanId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {

                    $.each(data, function (index, val) {
                        $("#" + index).val(val);
                    });
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载监测类别数据失败！');
            }
        });
        return strReturn;
    }

})