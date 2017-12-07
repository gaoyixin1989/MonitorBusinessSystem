
$(document).ready(function () { });

function GetRequestValue() {
    strReqVal = "";
    if ($("#txtQcStep").val() != "") {
        strReqVal += $("#txtQcStep").val();
    }
    return strReqVal;
}