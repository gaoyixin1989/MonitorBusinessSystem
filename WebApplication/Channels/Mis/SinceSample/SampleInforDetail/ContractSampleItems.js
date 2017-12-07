
//@ Create By Castle(胡方扬) 2012-12-01
//@ Company: Comleader(珠海高凌)
//@ 功能：委托书监测点位监测项目设置
//@ *修改人（时间）:
//@ *修改原因：
var mdivsamp = null, sampdiv = null;
var Itemgrid = null;
var gridPointSelectId = "";
var strContractPointId = "";
var GetFRQData = null, GetMonitorItemData = null,GetIndustryItemData=null;
var boolresult = false;
var moveid = "", strListBox = "";
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
    //获取URL 参数
    var strContratId = $.getUrlVar('ContractId'); //$.query.get('standartId');
    var strurlMonitorTypeId = $.getUrlVar('MonitorTypeId');
    var strCompanyIdFrim = $.getUrlVar('CompanyID');
    var strContractTypeId = $.getUrlVar('ContractTypeId');
    var strProject = $.getUrlVar('strProject');
    var strContractPointId = $.getUrlVar('strid')


    //加载行业类别，填充行业类别下拉框
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../Contract/MethodHander.ashx?action=GetIndustryInfo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != "0") {
                GetIndustryItemData = data.Rows;
            }
            else {
                return;
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax加载数据失败！');
        }
    });
    $("#txtIndustry").ligerComboBox({ data: GetIndustryItemData, width: 150, valueFieldID: 'Industry_OP', valueField: 'ID', textField: 'INDUSTRY_NAME', isMultiSelect: false, onSelected: function (ivalue) {
        ListBoxClear();
        GetReloadLisboxData(ivalue, strContractPointId, strurlMonitorTypeId);
    }
});
function ListBoxClear() {
    strListBox = $("#listRight option");
    strListBox.each(function () {
        this.selected = true;
    })
    vSelect = $("#listRight option:selected");

    if (vSelect.length > 0) {
        vSelect.clone().appendTo("#listLeft");
        vSelect.remove();
    }
}

function GetReloadLisboxData(strIndustry_Id, strPoint_Id, strMonitor_Id) {
    var strUrl = "GetIndurstyAllItems&strIndustryId=" + strIndustry_Id + "&strPointId=" + strPoint_Id + "&strMonitroType=" + strMonitor_Id;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../Contract/MethodHander.ashx?action=" + strUrl,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != "0") {
                GetSelectedComboxData = data.Rows;
                SeachIndustryItems(GetSelectedComboxData);
            }
            else {
                return;
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax加载数据失败！');
        }
    });
}


//选择行业类别后 将该行业类别下符合条件的监测项目添加到右边列表
function SeachIndustryItems(ComboxData) {
    var vSelect = "", strListBox = "";
    if (ComboxData.length > 0) {
        for (var i = 0; i < ComboxData.length; i++) {
            strListBox = $("#listLeft option");
            strListBox.each(function () {
                if ($(this).val() == ComboxData[i].ITEM_ID) {
                    this.selected = true;
                }
            })
            vSelect = $("#listLeft option:selected");
            if (vSelect.length > 0) {
                vSelect.clone().appendTo("#listRight");
                vSelect.remove();
            }
        }
    }

}
    GetMonitorSubItems();
    getItems();

    //DIV 设置条件项目监测项目部分

    var vSelectedData = null, vItemData = null;

    //初始化左侧ListBox
    function GetMonitorSubItems() {
        vItemData = null;
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../../Contract/MethodHander.ashx?action=GetMonitorSubSampleItems&strSampleId=" + strContractPointId + "&strMonitroType=" + strurlMonitorTypeId + "&strContratId=" + strContratId + "&strCompanyIdFrim=" + strCompanyIdFrim + "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    vItemData = data.Rows;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });

        //bind data
        var vlist = "";
        //遍历json数据,获取监测项目列表
        jQuery.each(vItemData, function (i, n) {
            vlist += "<option value=" + vItemData[i].ID + ">" + vItemData[i].ITEM_NAME + "</option>";
        });
        //绑定数据到listLeft
        $("#listLeft").append(vlist);

        $("#pageloading").hide();
    }




    //right move
    $("#btnRight").click(function () {
        moveright();
    });
    //double click to move left
    $("#listLeft").dblclick(function () {
        //克隆数据添加到listRight中
        moveright();
    });

    //left move 
    $("#btnLeft").click(function () {
        moveleft();
    });

    //double click to move right
    $("#listRight").dblclick(function () {
        moveleft();
    });

    function moveright() {
        //数据option选中的数据集合赋值给变量vSelect
        var isExist = false;
        var vSelect = $("#listLeft option:selected");
        //克隆数据添加到listRight中
        if ($("#listRight option").length > 0) {
            $("#listRight option").each(function () {
                if ($(this).val() == $("#listLeft option:selected").val()) {
                    $.ligerDialog.warn('所选数据已存在！');
                    return isExist = false;
                }
                else {
                    isExist = true;
                }
            });
        }
        else {
            isExist = true;
        }
        if (isExist) {
            vSelect.clone().appendTo("#listRight");
            vSelect.remove();
        }
    }
    function moveleft() {
        moveid = "";
        var vSelect = $("#listRight option:selected");
        if (vSelect.length > 0) {
            for (var i = 0; i < vSelect.length; i++) {
                moveid += vSelect[i].value + ";";
            }
        }
        vSelect.clone().appendTo("#listLeft");
        vSelect.remove();
    }
    function getItems() {
        vSelectedData = null;
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../../Contract/MethodHander.ashx?action=GetSelectedMonitorSampleItems&strSampleId=" + strContractPointId + "&strMonitroType=" + strurlMonitorTypeId + "&strContratId=" + strContratId + "&strCompanyIdFrim=" + strCompanyIdFrim + "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    vSelectedData = data.Rows;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });

        var vItemlist = "";
        jQuery.each(vSelectedData, function (i, n) {
            vItemlist += "<option value=" + vSelectedData[i].ID + ">" + vSelectedData[i].ITEM_NAME + "</option>";
        });
        if (vItemlist.length > 0) {
            $("#listRight").append(vItemlist);
        }
    }
    $("#pageloading").hide();
});


function GetMoveItems() {
    var strData = "";
    moveid = moveid.substring(0, moveid.length - 1);
    strData = moveid;

    return strData;
}

function GetSelectItems() {
    var strData = "";
    if ($("#listRight option").length > 0) {
        var strevmonitorItems = "";
        $("#listRight option").each(function () {
            strevmonitorItems += $(this).val() + ";";
        });
        strevmonitorItems = strevmonitorItems.substring(0, strevmonitorItems.length - 1);
        strData = strevmonitorItems;
    }

    return strData;
}

function txtSeachOption() {
    //移除上次查询的底色
    $('#listLeft option').css({ 'background-color': '' })
    //获取所有包含查询内容的文本ListBox,并遍历
    $('#listLeft option:contains("' + $('#txtSeach').val() + '")').each(function () {
        $(this).css({ 'background-color': '#6FC8F5' });
    });
}