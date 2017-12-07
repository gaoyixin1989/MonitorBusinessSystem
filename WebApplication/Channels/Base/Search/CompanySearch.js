// Create by 邵世卓 2012.11.28  "企业查询"功能
var firstManager;
var secondManager;
var thirdManager;

var strCompanyID = "";
var strID = "";
var objDivAttr;
var intSelectCount = 0;
var AttributeTypeAndInfoLst = [];
var PointValue = [];
var managertmp;
var obj;

var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
$(document).ready(function () {
    var gridHeight = $(window).height() / 2;
    var topHeight = $(window).height() / 2;

    $("#layout1").ligerLayout({ topHeight: topHeight, leftWidth: "50%", rightWidth: "50%", allowLeftCollapse: false, allowRightCollapse: false, height: "100%" });

    strCompanyID = request('comId');
    strID = request('strid');
    if (!strID)
        strID = "";

    //企业信息grid
    window['g'] =
    firstManager = $("#firstgrid").ligerGrid({
        columns: [
                { display: '企业名称', name: 'COMPANY_NAME', align: 'left', width: 200, minWidth: 60 },
                { display: '所在区域', name: 'AREA', minWidth: 250, render: function (record) {
                    return getDictText('administrative_area', record.AREA);
                }
                },
                { display: '行业类别', name: 'INDUSTRY', minWidth: 140, render: function (record) {
                    return getIndustryName(record.INDUSTRY);
                }
                },
                { display: '联系人', name: 'CONTACT_NAME', minWidth: 250 }
                ],
        title: "企业信息",
        width: '100%',
        height: gridHeight,
        pageSizeOptions: [5, 10, 15, 20],
        url: 'CompanySearch.aspx?type=getCompanyInfo',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'srhAll', text: '所有记录', click: itemclick_OfToolbar_UnderItem, icon: 'refresh' },
                { line: true },
                { id: 'srh', text: '查询', click: itemclick_OfToolbar_UnderItem, icon: 'search' },
                { line: true },
                { id: 'view', text: '详细', click: itemclick_OfToolbar_UnderItem, icon: 'archives' }
                ]
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showDetail({
                COMPANY_NAME: data.COMPANY_NAME,
                COMPANY_CODE: data.COMPANY_CODE,
                PINYIN: data.PINYIN,
                DIRECTOR_DEPT: data.DIRECTOR_DEPT,
                ECONOMY_TYPE_NAME: getDictText("company_economic_type", data.ECONOMY_TYPE),
                AREA_NAME: getDictText("administrative_area", data.AREA),
                SIZE_NAME: getDictText("company_size", data.SIZE),
                WEB_SITE: data.WEB_SITE,
                INDUSTRY_NAME: getIndustryName(data.INDUSTRY),
                GAS_LEAVEL_NAME: getDictText('control_level', data.GAS_LEAVEL),
                WATER_LEAVEL_NAME: getDictText("control_level", data.WATER_LEAVEL),
                PRACTICE_DATE: data.PRACTICE_DATE,
                CONTACT_NAME: data.CONTACT_NAME,
                LINK_DEPT: data.LINK_DEPT,
                EMAIL: data.EMAIL,
                LINK_PHONE: data.LINK_PHONE,
                FACTOR: data.FACTOR,
                PHONE: data.PHONE,
                MOBAIL_PHONE: data.MOBAIL_PHONE,
                FAX: data.FAX,
                POST: data.POST,
                MONITOR_ADDRESS: data.MONITOR_ADDRESS,
                CONTACT_ADDRESS: data.CONTACT_ADDRESS,
                LONGITUDE: data.LONGITUDE,
                LATITUDE: data.LATITUDE
            }, false);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            //点击的时候加载点位数据
            secondManager.set('url', "CompanySearch.aspx?type=getCompanyPointInfo&comId=" + rowdata.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    //企业点位信息
    secondManager = $("#secondgrid").ligerGrid({
        columns: [
                 { display: '监测点位', name: 'POINT_NAME', align: 'left', isSort: false, width: 200 },
                 { display: '监测类别', name: 'MONITOR_ID', align: 'left', width: 100, isSort: false, render: function (record) {
                     return getMonitorName(record.MONITOR_ID);
                 }
                 },
                 { display: '委托类型', name: 'POINT_TYPE', align: 'left', width: 100, isSort: false, render: function (record) {
                     return getDictText('Contract_Type', record.POINT_TYPE);
                 }
                 },
                { display: '监测频次', name: 'FREQ', align: 'left', isSort: false, width: 80 },
                { display: '序号', name: 'NUM', align: 'left', isSort: false, width: 50 }
                ],
        title: "",
        width: '100%',
        height: gridHeight, heightDiff: -30,
        pageSizeOptions: [5, 10, 15, 20],
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            //点击的时候加载点位项目数据
            thirdManager.set('url', "CompanySearch.aspx?type=getCompanyPointItemInfo&pId=" + rowdata.ID);
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            updateData(data.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    //点位项目信息
    thirdManager = $("#thirdgrid").ligerGrid({
        columns: [
            { display: '监测点', name: 'Point_Name', width: 200, align: 'left', isSort: false, render: function (record) {
                return getPointName(record.POINT_ID);
            }
            },
            { display: '监测项目', name: 'ITEM_NAME', width: 150, align: 'left', isSort: false, render: function (record) {
                return getItemName(record.ITEM_ID);
            }
            }
        ],
        title: "",
        width: '100%',
        height: gridHeight, heightDiff: -30,
        pageSizeOptions: [5, 10, 15, 20],
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );

    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
});


// Create by 邵世卓 2012.11.28  "企业信息查询、查看"功能

//企业信息grid 的Toolbar click事件
function itemclick_OfToolbar_UnderItem(item) {
    switch (item.id) {
        case 'view':
            var selected = firstManager.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择要查看的记录！'); ; return }

            showDetail({
                COMPANY_NAME: selected.COMPANY_NAME,
                COMPANY_CODE: selected.COMPANY_CODE,
                PINYIN: selected.PINYIN,
                DIRECTOR_DEPT: selected.DIRECTOR_DEPT,
                ECONOMY_TYPE_NAME: getDictText("company_economic_type", selected.ECONOMY_TYPE),
                AREA_NAME: getDictText("administrative_area", selected.AREA),
                SIZE_NAME: getDictText("company_size", selected.SIZE),
                WEB_SITE: selected.WEB_SITE,
                INDUSTRY_NAME: getIndustryName(selected.INDUSTRY),
                GAS_LEAVEL_NAME: getDictText('control_level', selected.GAS_LEAVEL),
                WATER_LEAVEL_NAME: getDictText("control_level", selected.WATER_LEAVEL),
                PRACTICE_DATE: selected.PRACTICE_DATE,
                CONTACT_NAME: selected.CONTACT_NAME,
                LINK_DEPT: selected.LINK_DEPT,
                EMAIL: selected.EMAIL,
                LINK_PHONE: selected.LINK_PHONE,
                FACTOR: selected.FACTOR,
                PHONE: selected.PHONE,
                MOBAIL_PHONE: selected.MOBAIL_PHONE,
                FAX: selected.FAX,
                POST: selected.POST,
                MONITOR_ADDRESS: selected.MONITOR_ADDRESS,
                CONTACT_ADDRESS: selected.CONTACT_ADDRESS,
                LONGITUDE: selected.LONGITUDE,
                LATITUDE: selected.LATITUDE
            }, false);
            break;
        case 'srh':
            showDetailSrh();
            break;
        case 'srhAll':
            firstManager.set('url', "CompanySearch.aspx?type=getCompanyInfo");
            break;
        default:
            break;
    }
}

function itemclick_OfToolbar_UnderPoint(item) {
    switch (item.id) {
        case 'view':
            var selected = secondManager.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择要查看的记录！'); return; }

            updateData(selected.ID);
            break;
        default:
            break;
    }
}

//企业信息grid 的编辑对话框及save函数
var detailWin = null, curentData = null, currentIsAddNew;
function showDetail(data, isAddNew) {
    curentData = data;
    currentIsAddNew = isAddNew;
    if (detailWin) {
        detailWin.show();
    }
    else {
        //创建表单结构
        var mainform = $("#editFirstForm");
        mainform.ligerForm({
            inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right',
            fields: [
                { name: "ID", type: "hidden" },
                { display: "企业名称", name: "COMPANY_NAME", newline: true, type: "label", width: 530, group: "基本信息", groupicon: groupicon },
                { display: "企业法人代码", name: "COMPANY_CODE", newline: true, type: "label" },
                { display: "拼音编码", name: "PINYIN", newline: false, type: "label" },
                { display: "主管部门", name: "DIRECTOR_DEPT", newline: true, type: "label" },
                { display: "经济类型", name: "ECONOMY_TYPE_NAME", newline: false, type: "label" },
                { display: "所在区域", name: "AREA_NAME", newline: true, type: "label" },
                { display: "企业规模", name: "SIZE_NAME", newline: false, type: "label" },
                { display: "企业网址", name: "WEB_SITE", newline: true, type: "label" },
                { display: "行业类别", name: "INDUSTRY_NAME", newline: false, type: "label" },
                { display: "废气控制级别", name: "GAS_LEAVEL_NAME", newline: true, type: "label" },
                { display: "废水控制级别", name: "WATER_LEAVEL_NAME", newline: false, type: "label" },
                { display: "开业时间", name: "PRACTICE_DATE", newline: true, type: "label" },
                { display: "联系人", name: "CONTACT_NAME", newline: true, type: "label", group: "联系方式", groupicon: groupicon },
                { display: "联系部门", name: "LINK_DEPT", newline: false, type: "label" },
                { display: "电子邮件", name: "EMAIL", newline: true, type: "label" },
                { display: "联系电话", name: "LINK_PHONE", newline: false, type: "label" },
                { display: "委托代理人", name: "FACTOR", newline: true, type: "label" },
                { display: "办公电话", name: "PHONE", newline: false, type: "label" },
                { display: "移动电话", name: "MOBAIL_PHONE", newline: true, type: "label" },
                { display: "传真号码", name: "FAX", newline: false, type: "label" },
                { display: "邮政编码", name: "POST", newline: true, type: "label" },
                { display: "监测地址", name: "MONITOR_ADDRESS", newline: true, type: "label", width: 530 },
                { display: "通讯地址", name: "CONTACT_ADDRESS", newline: true, type: "label", width: 530 },
                { display: "经度", name: "LONGITUDE", newline: true, type: "label" },
                { display: "纬度", name: "LATITUDE", newline: false, type: "label" }
                ]
        });

        detailWin = $.ligerDialog.open({
            target: $("#detail"),
            width: 700, height: 450, top: 5, title: "企业信息信息",
            buttons: [
                  { text: '取消', onclick: function () { clearDialogValue(); detailWin.hide(); } }
                  ]
        });
    }
    if (curentData) {
        $("#COMPANY_NAME").val(curentData.COMPANY_NAME);
        $("#COMPANY_CODE").val(curentData.COMPANY_CODE);
        $("#PINYIN").val(curentData.PINYIN);
        $("#DIRECTOR_DEPT").val(curentData.DIRECTOR_DEPT);
        $("#ECONOMY_TYPE_NAME").val(curentData.ECONOMY_TYPE_NAME);
        $("#AREA_NAME").val(curentData.AREA_NAME);
        $("#SIZE_NAME").val(curentData.SIZE_NAME);
        $("#WEB_SITE").val(curentData.WEB_SITE);
        $("#INDUSTRY_NAME").val(curentData.INDUSTRY_NAME);
        $("#GAS_LEAVEL_NAME").val(curentData.GAS_LEAVEL_NAME);
        $("#WATER_LEAVEL_NAME").val(curentData.WATER_LEAVEL_NAME);
        $("#PRACTICE_DATE").val(curentData.PRACTICE_DATE);
        $("#CONTACT_NAME").val(curentData.CONTACT_NAME);
        $("#LINK_DEPT").val(curentData.LINK_DEPT);
        $("#EMAIL").val(curentData.EMAIL);
        $("#LINK_PHONE").val(curentData.LINK_PHONE);
        $("#FACTOR").val(curentData.FACTOR);
        $("#PHONE").val(curentData.PHONE);
        $("#MOBAIL_PHONE").val(curentData.MOBAIL_PHONE);
        $("#FAX").val(curentData.FAX);
        $("#POST").val(curentData.POST);
        $("#MONITOR_ADDRESS").val(curentData.MONITOR_ADDRESS);
        $("#CONTACT_ADDRESS").val(curentData.CONTACT_ADDRESS);
        $("#LONGITUDE").val(curentData.LONGITUDE);
        $("#LATITUDE").val(curentData.LATITUDE);
    }
}

//企业信息grid 的编辑对话框元素的值 清除
function clearDialogValue() {
    $("#COMPANY_NAME").val("");
    $("#COMPANY_CODE").val("");
    $("#PINYIN").val("");
    $("#DIRECTOR_DEPT").val("");
    $("#ECONOMY_TYPE_NAME").val("");
    $("#AREA_NAME").val("");
    $("#SIZE_NAME").val("");
    $("#WEB_SITE").val("");
    $("#INDUSTRY_NAME").val("");
    $("#GAS_LEAVEL_NAME").val("");
    $("#WATER_LEAVEL_NAME").val("");
    $("#PRACTICE_DATE").val("");
    $("#CONTACT_NAME").val("");
    $("#LINK_DEPT").val("");
    $("#EMAIL").val("");
    $("#LINK_PHONE").val("");
    $("#FACTOR").val("");
    $("#PHONE").val("");
    $("#MOBAIL_PHONE").val("");
    $("#FAX").val("");
    $("#POST").val("");
    $("#MONITOR_ADDRESS").val("");
    $("#CONTACT_ADDRESS").val("");
    $("#LONGITUDE").val("");
    $("#LATITUDE").val("");
}

//企业信息grid 的查询对话框
var detailWinSrh = null;
function showDetailSrh() {
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构
        var searchform = $("#searchFirstForm");
        searchform.ligerForm({
            inputWidth: 170, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                        { display: "企业名称", name: "SEA_COMPANY_NAME", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                        { display: "所在区域", name: "SEA_AREA", newline: true, type: "select", comboboxName: "SEA_AREA_BOX", options: { valueFieldID: "AREA", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "CompanySearch.aspx?type=getDict&dictType=administrative_area"} },
                        { display: "行业类别", name: "SEA_INDUSTRY", newline: true, type: "select", comboboxName: "SEA_INDUSTRY_BOX", options: { valueFieldID: "INDUSTRY", valueField: "ID", textField: "INDUSTRY_NAME", url: "CompanySearch.aspx?type=getIndustry"} }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 400, height: 200, top: 90, title: "查询企业信息",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SEA_COMPANY_NAME = encodeURIComponent($("#SEA_COMPANY_NAME").val());
        var SEA_AREA = $("#AREA").val();
        var SEA_SEA_INDUSTRY = $("#INDUSTRY").val();

        firstManager.set('url', "CompanySearch.aspx?type=getCompanyInfo&srhCompayName= " + SEA_COMPANY_NAME + "&srh_Area=" + SEA_AREA + "&srh_Industry=" + SEA_SEA_INDUSTRY);
    }
}

//企业信息grid 的查询对话框元素的值 清除
function clearSearchDialogValue() {
    $("#SEA_COMPANY_NAME").val("");
    $("#SEA_AREA_BOX").ligerGetComboBoxManager().setValue("");
    $("#SEA_INDUSTRY_BOX").ligerGetComboBoxManager().setValue("");
}

//点位查看
function updateData(pointId) {
    if (pointId == "") {
        $.ligerDialog.warn('请选择一条记录进行查看');
        return;
    }
    $.ligerDialog.open({ title: '点位信息查询', top: 0, width: 650, height: 450, buttons:
        [{ text: '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'PointView.aspx?strid=' + pointId + '&CompanyID=' + strCompanyID
    });
}

//获取DictText
function getDictText(type, code) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "CompanySearch.aspx/getDictName",
        data: "{'strType':'" + type + "','strCode':'" + code + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                strReturn = data.d;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        }
    });
    return strReturn;
}

//获取监测点位信息
function getPointName(strPointID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "CompanySearch.aspx/getPointName",
        data: "{'strValue':'" + strPointID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data != null) {
                strValue = data.d;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        }
    });
    return strValue;
}

//获取监测项目信息
function getItemName(strItemID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "CompanySearch.aspx/getItemName",
        data: "{'strValue':'" + strItemID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data != null) {
                strValue = data.d;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        }
    });
    return strValue;
}

//获取行业类别信息
function getIndustryName(strIndustryCode) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "CompanySearch.aspx/getIndustryCode",
        data: "{'strValue':'" + strIndustryCode + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data != null) {
                strValue = data.d;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        }
    });
    return strValue;
}

//获取监测类别信息
function getMonitorName(strMonitorID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "CompanySearch.aspx/getMonitorName",
        data: "{'strValue':'" + strMonitorID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data != null) {
                strValue = data.d;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        }
    });
    return strValue;
}

//点位属性类别名称
function getAttName(typeid) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "CompanySearch.aspx/getAttName",
        data: "{'strValue':'" + typeid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data != null) {
                strValue = data.d;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        }
    });
    return strValue;
}
