var strQCStep = "", strReqVal = "", strTask_Id = "", strPlanId = "";
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
    strTask_Id = $.getUrlVar('strTask_Id');
    strPlanId = $.getUrlVar('strPlanId');

    //委托书信息
    $.post('SamplingQCStep.aspx?type=getContractInfo&strTask_Id=' + strTask_Id + '&strPlanId=' + strPlanId, function (data) {
        SetContractInfo(data);
    }, 'json');
})

function SetContractInfo(dataArr) {
    if (dataArr != null) {
//        $("#txtQcStep").val(dataArr.QC_STEP);
        $("#txtQcStep").val(dataArr.REMARK1);
    }
}

function GetRequestValue() {
    strReqVal = "";
    if ($("#txtQcStep").val() != "") {
        strReqVal += $("#txtQcStep").val();
    }
    return strReqVal;
}