// JScript 文件
//创建人：邵世卓
//功能：选择人员
//创建时间：2012-6-29
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
$(document).ready(function () {
    //菜单样式及操作
    $(".acc_container:not('.acc_container:first')").hide();
    $('.acc_trigger').click(function () {
        if ($(this).next().is(':hidden')) {
            $('.acc_trigger').removeClass('active').next().slideUp();
            $(this).toggleClass('active').next().slideDown();
        } else {
            $(this).toggleClass('active');
            $(this).next().slideUp();
        }
        return false;
    });
    //部门过滤人员
    $("#DEPARTMENT_CODE").change(function () {
        $.ajax({
            type: "GET",
            url: "SelectDeptAndUser.aspx",
            async: false,
            data: "action=getitemlist&param=" + Math.random() + "&extype=" + $("#DEPARTMENT_CODE").val() + "&ID=" + $("#hdnSwID")[0].value,
            success: function (data) {
                var arrlist = jQuery.parseJSON(data)["Head"];
                $("#DeptPerson")[0].length = 0;
                $.each(arrlist, function (index, object) {
                    $("#DeptPerson")[0].options.add(new Option(object["REAL_NAME"], object["USER_ID"]));
                });
            }
        });
    });
});

//添加人员 
function btnAdd_Click() {
    $.ajax({
        type: "GET",
        url: "SelectDeptAndUser.aspx",
        async: false,
        data: "action=addPerson&param=" + Math.random() + "&ID=" + $("#hdnSwID")[0].value + "&personID1=" + $("#hdnSelectPerson1")[0].value + "&personID2=" + $("#hdnSelectPerson2")[0].value,
        success: function (data) {
            onOk(data);
        }
    });
}

//项目添加1
function PtItemAdd1() {
    var selectType = $("#hdnType").attr("value");
    switch (selectType) {
        case "hight":
            MoveListBoxToRight("HIGHT", "ListHasSel1");
            break;
        case "person":
            MoveListBoxToRight("PERSON", "ListHasSel1");
            break;
        case "dept":
            MoveListBoxToRight("LIST_DEPARTMENT_CODE", "ListHasSel1");
            break;
        case "person_dept":
            MoveListBoxToRight("DeptPerson", "ListHasSel1");
            break;
        default:
            MoveListBoxToRight("PERSON", "ListHasSel1");
            break;
    }
}

//项目移除1
function PtItemRemove1() {
    //判断是否当前类别
    $.each($("#ListHasSel1 option:selected"), function (index, object) {
        $(object).remove();
        $("ListHasSel1").children("option:first").attr("selected", true);
        $("#hdnSelectPerson1")[0].value = $("#hdnSelectPerson1")[0].value.replace(object.value + ",", "").replace("--", "");
        $("#hdnSelectPersonName1")[0].value = $("#hdnSelectPersonName1")[0].value.replace(object.value + ",", "").replace("--", "");
        orderrole("ListHasSel1");
    });
}

//将ListBox中的数据移动到另外一个ListBox中，leftname为ListBox的name,rightname为另外一个ListBox的name
function MoveListBoxToRight(leftname, rightname) {
    var selectType = $("#hdnType").attr("value");
    var size = $("#" + leftname + " option").size();
    var sizeRight = $("#" + rightname + " option").size();
    var selsize = $("#" + leftname + " option:selected").size();
    if (size > 0 && selsize > 0) {
        $.each($("#" + leftname + " option:selected"), function (id, own) {
            var text = $(own).text();
            var value = $(own).attr("value");
            if (sizeRight > 0) {
                var count = 0; //左右存在相同值次数
                var equalCount = 0; //上下存在相同次数 

                $.each($("#" + rightname + " option"), function (id, own) {
                    var textRight = $(own).text();
                    if (text == textRight) {
                        count++;
                    }
                });

                if (rightname == "ListHasSel1") {
                    $.each($("#ListHasSel2 option"), function (id, own) {
                        var Equaltext = $(own).text();
                        if (text == Equaltext) {
                            equalCount++;
                        }
                    });
                }

                else if (rightname == "ListHasSel2") {
                    $.each($("#ListHasSel1 option"), function (id, own) {
                        var Equaltext = $(own).text();
                        if (text == Equaltext) {
                            equalCount++;
                        }
                    });
                }

                // 左右相同值次数小等于0，即左右、上下不存在相同值，添加
                if (count <= 0 && equalCount <= 0) {
                    if (selectType == "dept")//如果选择部门，则做选择项标记
                    {
                        if (rightname == "ListHasSel1") {
                            $("#" + rightname).prepend("<option value=\"" + value + "\">" + text + "</option>");
                            $("#hdnSelectPerson1")[0].value += "--" + value + ",";
                            $("#hdnSelectPersonName1")[0].value += "--" + text + ",";
                        }
                    }
                    else {
                        if (rightname == "ListHasSel1") {
                            $("#" + rightname).prepend("<option value=\"" + value + "\">" + text + "</option>");
                            $("#hdnSelectPerson1")[0].value += value + ",";
                            $("#hdnSelectPersonName1")[0].value += text + ",";
                        }
                    }
                }
                else {
                    alert("不可重复选择！");
                }
            }
        });
    }
    $.each($("#" + rightname + " option"), function (id, own) {
        orderrole(rightname);
    });
}
//对改变后的ListBox中的数据进行排序
function orderrole(name) {
    var size = $("#" + name + " option").size();
    var one = $("#" + name + " option:first-child");
    if (size > 0) {
        var text = $(one).text();
        var tag = parseInt($(one).attr("tag"));
        $.each($(one).nextAll(), function (id, own) {
            var nextag = parseInt($(own).attr("tag"));
            if (tag > nextag) {
                $(one).remove();
                $(own).after("<option tag=\"" + tag + "\">" + text + "</option>");
                one = $(own).next();
            }
        });
    }
}

function btnAddLand() {
    var strLandText = "";
    var str = $("#lbSelItem")[0];
    $.each(str, function (index, object) {
        strLandText += object.text + ",";
    });
    if (strLandText != "") {
        strLandText = strLandText.substring(0, strLandText.lastIndexOf(','));
    }
    $("#txtItem")[0].value = strLandText;
}
function selectHight() {
    $("#hdnType").attr("value", "hight");
}
function selectPerson() {
    $("#hdnType").attr("value", "person");
}
function selectDept() {
    $("#hdnType").attr("value", "dept");
}
function selectPersonByDept() {
    $("#hdnType").attr("value", "person_dept");
}
function onOk(name) {
    var Arr = [];
    if (name != "") {
        Arr = name.split("|");
    }
    window.returnValue = Arr;
    window.close();
    return false;
}
function onCancel() {
    var name = $("#hdnSwPerson").attr("value");
    var Arr = [];
    if (name != "") {
        Arr = name.split("|");
    }
    window.returnValue = Arr;
    window.close();
    return false;
} 