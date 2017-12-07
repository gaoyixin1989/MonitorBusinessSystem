// 公共方法 Create by 熊卫华 2012.10.23

//定义江河水评价标准类别代码
var GlobalMonitorType = "EnvRiver";
var emptyArray = jQuery.parseJSON("{\"Rows\": [], \"Total\": \"0\"}");
//选择列表所有的数据
function chkSelectAll(obj) {
    var chks = document.getElementsByName('chk');
    for (var i = 1; i < chks.length; i++) {
        chks[i].checked = obj.checked;
    }
}
//编辑列表界面
function chkEdit() {
    var chks = document.getElementsByName('chk');
    document.getElementById("cphData_txtHidden").value = "";
    var count = 0;
    for (var i = 1; i < chks.length; i++) {
        if (chks[i].checked == true) {
            document.getElementById("cphData_txtHidden").value = chks[i].value;
            count++;
        }
    }
    if (count != 1) {
        alert("请选择一条需要编辑的数据");
        return false;
    }
    return true;
}
//删除列表数据
function chkDelete() {
    var chks = document.getElementsByName('chk');
    document.getElementById("cphData_txtHidden").value = "";
    var count = 0;
    var spit = "";
    for (var i = 1; i < chks.length; i++) {
        if (chks[i].checked == true) {
            document.getElementById("cphData_txtHidden").value = document.getElementById("cphData_txtHidden").value + spit + chks[i].value;
            spit = ",";
            count++;
        }
    }
    if (count == 0) {
        alert("请选择需要删除的数据");
        return false;
    }
    return true;
}
//保存于更新数据
function DataSave() {
    var isSuccess = false;
    var strSrc = window.location.href;
    ajaxSubmit(strSrc, function () {
        return true;
    }, function (responseText, statusText) {
        if (responseText == "1")
            isSuccess = true;
    });
    return isSuccess;
}
//提交form数据到后台
function ajaxSubmit(url, fnbeforeSubmit, fnSuccess) {
    
    var options = {
        cache: false,
        async: false,
        beforeSubmit: fnbeforeSubmit,
        success: fnSuccess,
        url: url,
        type: "post"
    };
    //表单提交前进行校验 create by ssz
    try {
        if (!$("#form1").validate()) {
            return false;
        }
    }
    catch (e) {
    }

    $("#form1").ajaxSubmit(options);
    return false;
}
//将Json绑定到表单
function bindJsonToPage(data) {
    var mainform = $("#form1");
    for (var p in data) {
        var ele = $("[name=" + p + "]", mainform);
        //针对复选框和单选框 处理
        if (ele.is(":checkbox,:radio")) {
            ele[0].checked = data[p] ? true : false;
        }
        else {
            ele.val(data[p]);
        }
    }
}
//获取request
function request(strParame) {
    var args = new Object();
    var query = location.search.substring(1);

    var pairs = query.split("&"); // Break at ampersand 
    for (var i = 0; i < pairs.length; i++) {
        var pos = pairs[i].indexOf('=');
        if (pos == -1) continue;
        var argname = pairs[i].substring(0, pos);
        var value = pairs[i].substring(pos + 1);
        value = decodeURIComponent(value);
        args[argname] = value;
    }
    return args[strParame];
}
//-------------------------------------------------------分析环节数据获取-------------------------
//获取企业名称信息
function getCompanyName(strTaskId, strCompanyId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getCompanyName",
        data: "{'strTaskId':'" + strTaskId + "','strCompanyId':'" + strCompanyId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取监测类别信息
function getMonitorTypeName(strMonitorTypeId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getMonitorTypeName",
        data: "{'strMonitorTypeId':'" + strMonitorTypeId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取用户名称
function getUserNameEx(strUserId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getUserName",
        data: "{'strUserId':'" + strUserId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取监测项目资料信息
function getItemInfoName(strItemCode, strItemName) {
    
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getItemInfoName",
        data: "{'strItemCode':'" + strItemCode + "','strItemName':'" + strItemName + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取字典项信息
function getDictName(strDictCode, strDictType) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getDictName",
        data: "{'strDictCode':'" + strDictCode + "','strDictType':'" + strDictType + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取Url参数 Create By 魏林
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
//用于点位复制数据 Create By 魏林
function DataCopy(c) {
    var strJson = "";
    var strSrc = window.location.href;
    
    ajaxSubmit(strSrc + "?" + c, function () {
        return true;
    }, function (responseText, statusText) {
        strJson = responseText;
    });
    return strJson;
}
//验证是否为数字 Create By 魏林
function isDouble(value) {
    //var reg = /^[-\+]?\d+(\.\d+)?$/;
    var reg = /^([1-9]\d*|0)(\.[0-9]{1,8})?$/;
    return reg.test(value);
    //var r = v.match(reg);   
}
//保存于更新数据 Create By 魏林
function DataSaveN() {
    var obj = null;
    var strSrc = window.location.href;
    ajaxSubmitN(strSrc, function () { //表单提交前进行校验
        if (!$("#form1").validate()) {
            return false;
        }
        else {
            return true;
        }
    }, function (responseText, statusText) {
        obj = responseText;
    });
    return obj;
}
//提交form数据到后台
function ajaxSubmitN(url, fnbeforeSubmit, fnSuccess) {
    var options = {
        cache: false,
        async: false,
        beforeSubmit: fnbeforeSubmit,
        success: fnSuccess,
        url: url,
        type: "post",
        dataType: "json"
    };
    //表单提交前进行校验 create by ssz
    try {
        
    }
    catch (e) {
    }

    $("#form1").ajaxSubmit(options);
    return false;
}