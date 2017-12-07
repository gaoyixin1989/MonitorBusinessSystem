var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strCompanyID = "";
var strID = "";
var strurlContractId = "", strurlMonitorTypeId = "", strurlProject = "",strurlContractTypeId="";
var objDivAttr;
var intSelectCount = 0;
var AttributeTypeAndInfoLst = [];
var PointValue = [];
var managertmp;
var obj;

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
    strCompanyID = $.getUrlVar('CompanyID');
    strID = $.getUrlVar('strid');
    strurlContractId = $.getUrlVar('ContractId');
    strurlMonitorTypeId = $.getUrlVar('MonitorTypeId');
    strurlProject = $.getUrlVar('strProject');
    strurlContractTypeId = $.getUrlVar('ContractTypeId');
    if (!strID)
        strID = "";


    //创建表单结构 --点位基本信息
    $("#divEdit").ligerForm({
        inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right', modal: true,
        fields: [
                { name: "ID", type: "hidden" },
                { display: "项目名称", name: "PROJECT_NAME", newline: true, type: "text", width: 530, group: "基本信息", groupicon: groupicon },
                { display: "监测点", name: "POINT_NAME", newline: true, type: "text", width: 530 },
                { display: "监测类型", name: "MONITOR_ID", newline: true, type: "select", comboboxName: "MONITOR_ID_Box", options: { valueFieldID: "MONITOR_ID_OP", url: "../../../Base/MonitorType/Select.ashx?view=T_BASE_MONITOR_TYPE_INFO&idfield=ID&textfield=MONITOR_TYPE_NAME&where=is_del|0"} },
                { display: "委托类型", name: "POINT_TYPE", newline: false, type: "select", comboboxName: "POINT_TYPE_Box", options: { valueFieldID: "POINT_TYPE_OP", url: "../../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|Contract_Type"} },
                { display: "点位属性类别", name: "DYNAMIC_ATTRIBUTE_ID", newline: true, type: "select", comboboxName: "DYNAMIC_ATTRIBUTE_ID_Box", options: { valueFieldID: "DYNAMIC_ATTRIBUTE_ID_OP", url: "../../../Base/MonitorType/Select.ashx?view=T_BASE_ATTRIBUTE_TYPE&idfield=ID&textfield=SORT_NAME&where=MONITOR_ID|" + strurlMonitorTypeId + "-IS_DEL|0"} },
        // { display: "监测频次", name: "FREQ", newline: false, type: "select", comboboxName: "FREQ_Box", options: { valueFieldID: "FREQ_OP", url: "../../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|FREQ"} },
                {display: "监测周期(天数)", name: "SAMPLE_DAY", newline: false, type: "select", comboboxName: "SAMPLE_DAY_Box", options: { valueFieldID: "SAMPLE_DAY_OP", url: "../../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|SampleDay"} },
                { display: "采样频次", name: "SAMPLE_FREQ", newline: true, type: "select", comboboxName: "SAMPLEFREQ_Box", options: { valueFieldID: "SAMPLEFREQ_OP", url: "../../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|POINT_SAMPLEFREQ"} },
                { display: "建成时间", name: "CREATE_DATE", newline: false, type: "date" },
                { display: "监测点位置", name: "ADDRESS", newline: true, type: "text" },
                { display: "经度", name: "LONGITUDE", newline: false, type: "text" },
                { display: "纬度", name: "LATITUDE", newline: true, type: "text" },
                { display: "排列序号", name: "NUM", newline: false, type: "text" }
                ]
    });
    $("#POINT_NAME").attr("validate", "[{required:true, msg:'请输入监测点名称'},{minlength:2,maxlength:20,msg:'监测点名称最小长度为2，最大长度为20'}]");
    $("#divStandard").ligerForm({
        inputWidth: 320, labelWidth: 120, space: 90, labelAlign: 'right',
        fields: [
        { display: "国标", name: "NATIONAL_ST_CONDITION_ID", newline: true, type: "select", group: "执行标准", groupicon: groupicon },
        { display: "地标", name: "LOCAL_ST_CONDITION_ID", newline: true, type: "select" },
        { display: "行标", name: "INDUSTRY_ST_CONDITION_ID", newline: true, type: "select" }
        ]
    });
    $("#PROJECT_NAME").val(strurlProject);
    $("#PROJECT_NAME").ligerGetTextBoxManager().setDisabled();
    if (strurlMonitorTypeId) {
        $("#MONITOR_ID_Box").ligerGetComboBoxManager().setValue(strurlMonitorTypeId);
        $("#MONITOR_ID_Box").ligerGetComboBoxManager().setDisabled();
    }
    $("#POINT_TYPE_Box").ligerGetComboBoxManager().setValue(strurlContractTypeId);
    $("#POINT_TYPE_Box").ligerGetComboBoxManager().setDisabled();
    $("#SAMPLEFREQ_Box").ligerGetComboBoxManager().setValue("1");
    $("#SAMPLE_DAY_Box").ligerGetComboBoxManager().setValue("1");
    $("#NATIONAL_ST_CONDITION_ID").ligerComboBox({
        onBeforeOpen: NATIONAL_select, valueFieldID: 'hidNATIONAL_ST_CON_ID'
    });
    $("#LOCAL_ST_CONDITION_ID").ligerComboBox({
        onBeforeOpen: LOCAL_select, valueFieldID: 'hidLOCAL_ST_CON_ID'
    });
    $("#INDUSTRY_ST_CONDITION_ID").ligerComboBox({
        onBeforeOpen: INDUSTRY_select, valueFieldID: 'hidINDUSTRY_ST_CON_ID'
    });


    if (strID != "") {
        var InitDataList = [];
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "../MethodHander.ashx?action=GetContractPointInfor&strPointId=" + strID + "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                InitDataList = data.Rows;
            }
        });
        SetInitValue();
    }


    function bh_LoadViewState() {
        var v = $("#hBirthday").val();
        if (v == "") return;
        var birthday = new Date(Date.parse(v.replace(/-/g, '/')))
        $("#ddlYear").val(birthday.getFullYear());
        $("#ddlMonth").val(birthday.getMonth() + 1);
        bh_setDayCount();
        $("#ddlDay").val(birthday.getDate());
    }


    function SetInitValue() {
        //编辑模式下 初始化基本信息
        $("#POINT_NAME").val(InitDataList[0].POINT_NAME);
        $("#DYNAMIC_ATTRIBUTE_ID_Box").ligerGetComboBoxManager().setValue(InitDataList[0].DYNAMIC_ATTRIBUTE_ID);
        //$("#FREQ_Box").ligerGetComboBoxManager().setValue(InitDataList[0].FREQ);
        $("#SAMPLE_DAY_Box").ligerGetComboBoxManager().setValue(InitDataList[0].SAMPLE_DAY);
        $("#SAMPLEFREQ_Box").ligerGetComboBoxManager().setValue(InitDataList[0].SAMPLE_FREQ);
        $("#ADDRESS").val(InitDataList[0].ADDRESS);
        //处理 日期函数
        var v = InitDataList[0].CREATE_DATE;
        if (v != "") {
            var createDate = new Date(Date.parse(v.replace(/-/g, '/')))
            var strData = createDate.getFullYear() + "-";
            strData += (createDate.getMonth() + 1) + "-";
            strData += createDate.getDate();
            $("#CREATE_DATE").val(strData);
        }
        $("#LONGITUDE").val(InitDataList[0].LONGITUDE);
        $("#LATITUDE").val(InitDataList[0].LATITUDE);
        $("#NUM").val(InitDataList[0].NUM);




        //初始化标准信息
        var NatinalData = [], LocalData = [], IndustryData = [];
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "../MethodHander.ashx?action=GetStanardInfor&strMonitroType=" + strurlMonitorTypeId + "&strStanardItemId=" + InitDataList[0].NATIONAL_ST_CONDITION_ID + "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                NatinalData = data.Rows;
            }
        });
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "../MethodHander.ashx?action=GetStanardInfor&strMonitroType=" + strurlMonitorTypeId + "&strStanardItemId=" + InitDataList[0].LOCAL_ST_CONDITION_ID + "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                LocalData = data.Rows;
            }
        });
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "../MethodHander.ashx?action=GetStanardInfor&strMonitroType=" + strurlMonitorTypeId + "&strStanardItemId=" + InitDataList[0].INDUSTRY_ST_CONDITION_ID + "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                IndustryData = data.Rows;
            }
        });

        //alert(NatinalData[0].STANDARD_CODE + " " + NatinalData[0].STANDARD_NAME + "|" + InitDataList[0].NATIONAL_ST_CONDITION_ID);
        if (NatinalData.length > 0) {
            $("#NATIONAL_ST_CONDITION_ID").val(NatinalData[0].STANDARD_CODE + " " + NatinalData[0].STANDARD_NAME);
            $("#hidNATIONAL_ST_CON").val(InitDataList[0].NATIONAL_ST_CONDITION_ID);
        }
        if (LocalData.length > 0) {
            $("#LOCAL_ST_CONDITION_ID").val(LocalData[0].STANDARD_CODE + " " + LocalData[0].STANDARD_NAME);
            $("#hidLOCAL_ST_CON").val(InitDataList[0].LOCAL_ST_CONDITION_ID);
        }
        if (IndustryData.length > 0) {
            $("#INDUSTRY_ST_CONDITION_ID").val(IndustryData[0].STANDARD_CODE + " " + IndustryData[0].STANDARD_NAME);
            $("#hidINDUSTRY_ST_CON").val(InitDataList[0].INDUSTRY_ST_CONDITION_ID);
        }
    }
});



function GetIsExiststr() {
    var strData = "";
    var isTrue = $("#form1").validate();
    if (isTrue) {
        strData += "&strCompanyIdFrim=" + strCompanyID;
        strData += "&strPointName=" + encodeURI($("#POINT_NAME").val());
        strData += "&strMonitroType=" + $("#MONITOR_ID_OP").val();
        strData += "&strContratType=" + $("#POINT_TYPE_OP").val();
        strData += "&strDYNAMIC_ATTRIBUTE_ID=" + $("#DYNAMIC_ATTRIBUTE_ID_OP").val();
    }
        return strData;

}



//得到基本信息保存参数
function GetBaseInfoStr() {

        var strData = "";

        strData += "&strPointID=" + strID;

        strData += "&strContratId=" + strurlContractId;
        strData += "&strCompanyIdFrim=" + strCompanyID;
        strData += "&strPointName=" + encodeURIComponent($("#POINT_NAME").val());
        strData += "&strMonitroType=" + $("#MONITOR_ID_OP").val();
        strData += "&strContratType=" + $("#POINT_TYPE_OP").val();
        strData += "&strDYNAMIC_ATTRIBUTE_ID=" + $("#DYNAMIC_ATTRIBUTE_ID_OP").val()
        strData += "&strSampleFREQ=" + $("#SAMPLEFREQ_OP").val();
        //strData += "&strFREQ=" + $("#FREQ_OP").val();
        strData += "&strSampleDay=" + $("#SAMPLE_DAY_OP").val();
        strData += "&strCREATE_DATE=" + $("#CREATE_DATE").val();
        strData += "&strPointAddress=" + encodeURIComponent($("#ADDRESS").val());
        strData += "&strLONGITUDE=" + encodeURIComponent($("#LONGITUDE").val());
        strData += "&strLATITUDE=" + encodeURIComponent($("#LATITUDE").val());
        strData += "&strNUM=" + $("#NUM").val();

        strData += "&strNATIONAL_ST_CONDITION_ID=" + $("#hidNATIONAL_ST_CON").val();
        strData += "&strLOCAL_ST_CONDITION_ID=" + $("#hidLOCAL_ST_CON").val();
        strData += "&strINDUSTRY_ST_CONDITION_ID=" + $("#hidINDUSTRY_ST_CON").val();
        return strData;
}


