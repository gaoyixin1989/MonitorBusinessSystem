/*
组合多种：[{required:true, msg:'请输入监测点'},{minlength:2,maxlength:10,msg:'录入最小长度为2，最大长度为10'}]
组合：[{required:true, msg:'请输入IP地址'},{ip:true,msg:'请输入正确的IP地址，例如：192.168.1.1'}]
录入长度：[{minlength:2,maxlength:10,msg:'录入最小长度为2，最大长度为10'}]
录入长度：[{minlength:2,msg:'录入最小长度为2'}]
录入长度：[{maxlength:2,msg:'录入最小长度为2'}]
必录：[{required:true, msg:'请输入监测点'}]
IP地址：[{ip:true,msg:'请输入正确的IP地址，例如：192.168.1.1'}]
数字验证：[{isnumber:true,msg:'请输入正确的数据类型，例如：1'}]
文件格式：[{suffix:'.jpg|.rar',msg:'只允许上传.rar、.jpg格式的文件'}]
身份证号验证：[{iscard:true,msg:'请输入正确的证件号!'}]
*/
//("validate", "[{required:true, msg:'请输入监测点'},{minlength:2,msg:'录入最小长度为2'},{minlength:2,maxlength:10,msg:'录入最小长度为2，最大长度为10'}]");
//身份证号 地区规范代码
var aCity = { 11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "内蒙古",
    21: "辽宁", 22: "吉林", 23: "黑龙江", 31: "上海", 32: "江苏", 33: "浙江",
    34: "安徽", 35: "福建", 36: "江西", 37: "山东", 41: "河南", 42: "湖北",
    43: "湖南", 44: "广东", 45: "广西", 46: "海南", 50: "重庆", 51: "四川"
, 52: "贵州", 53: "云南", 54: "西藏", 61: "陕西", 62: "甘肃", 63: "青海",
    64: "宁夏", 65: "新疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外"
};

$.fn.validate = function () {
    var msg = "";
    var target = null;
    $("span.validate").remove();
    $(".validateBorder").removeClass("validateBorder");
    $(this).find("[validate]").each(function () {
        var rules = eval("(" + $(this).attr("validate") + ")");
        //将所有的验证框背景色去除
        $(this).css("background-color", "");
        for (var i = 0; i < rules.length; i++) {
            if (rules[i].required && $.trim($(this).val()).length == 0) {
                msg = rules[i].msg;
                target = $(this);
                return false;
            }
            //            else if ((rules[i].minlength && $.trim($(this).val()).length < rules[i].minlength) || (rules[i].maxlength && $.trim($(this).val()).length > rules[i].maxlength)) {
            //                msg = rules[i].msg;
            //                target = $(this);
            //                return false;
            //            }
            //修改人： 吕晓林，时间：2011-9-25，目的：当输入为空时也能通过验证
            else if (($.trim($(this).val()).length > 0 && $.trim($(this).val()).length < rules[i].minlength) || ($.trim($(this).val()).length > 0 && $.trim($(this).val()).length > rules[i].maxlength)) {
                msg = rules[i].msg;
                target = $(this);
                return false;
            }
            //修改人： 吕晓林，时间：2011-9-25，目的：当输入为空时也能通过验证
            else if (rules[i].maxlength && $.trim($(this).val()).length > 0 && $.trim($(this).val()).length > rules[i].maxlength) {
                msg = rules[i].msg;
                target = $(this);
                return false;
            }
            //修改人： 吕晓林，时间：2011-9-25，目的：当输入为空时也能通过验证
            else if (rules[i].minlength && $.trim($(this).val()).length > 0 && $.trim($(this).val()).length < rules[i].minlength) {
                msg = rules[i].msg;
                target = $(this);
                return false;
            }
            else if (rules[i].fixlength) {    //增加 必输的固定项，长度为14位1开头的数字 by 吕晓林2011-7-26 ,修改：吕晓林 时间：2011-11-10 11:21:00
                // var exp = /^1+\d{parseInt(rules[i].fixlength-1)}$/;^[A-Za-z0-9]+$
                var exp = "";
                var bolFlag = false;
                //先验证位数
                if ($.trim($(this).val()).length == rules[i].fixlength) {
                    //第二 验证为数字
                    exp = /^[0-9]+$/;
                    if (exp.test($(this).val())) {
                        //                        //第三 验证首位字符为“1”
                        //                        if ($(this).val().substr(0, 1) == "1")
                        bolFlag = true;
                    }
                }
                if (!bolFlag) {
                    msg = rules[i].msg;
                    target = $(this);
                    return false;
                }
            }
            else if (rules[i].ip) {
                var exp = /^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$/;
                if ($(this).val().match(exp) == null && $(this).val().length > 0) {
                    msg = rules[i].msg;
                    target = $(this);
                    return false;
                }
            }
            else if (rules[i].isnumber) { //增加 数字验证项，可以为小数和整数 by 吕晓林2011-8-28
                //var exp = /^[1-9]*(\.\d*)?$|^-?0(\.\d*)?$/;
                //var exp = /^[1-9]+(.[0-9]{0,2})?$|^-?0(\.\d{0,2})?$/;
                var exp = /^([1-9]\d*|0)(\.[0-9]{1,8})?$/;
                if (!exp.test($(this).val()) && $(this).val().length > 0) {
                    //if (isNaN($(this).val()) && $(this).val().length > 0) {
                    msg = rules[i].msg;
                    target = $(this);
                    return false;
                }
            } else if (rules[i].suffix) {
                var exp = new RegExp(".+(\\" + rules[i].suffix + ")$", "ig");
                if (!exp.test($(this).val())) {
                    msg = rules[i].msg;
                    target = $(this);
                    return false;
                }
            }
            else if (rules[i].isdate) {     //增加 日期验证项，by 吕晓林2011-9-15
                var exp = new RegExp("(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-02-29)");
                if (!exp.test($(this).val()) && $(this).val().length > 0) {
                    msg = rules[i].msg;
                    target = $(this);
                    return false;
                }
            }
            //Begin  增加 身份证验证项，by  胡方扬 2013-01-07
            else if (rules[i].iscard) {
                var person_id = $(this).val();
                //合法性验证
                var sum = 0;
                //出生日期
                var birthday;
                //验证长度与格式规范性的正则
                var pattern = new RegExp(/(^\d{15}$)|(^\d{17}(\d|x|X)$)/i);
                if (pattern.exec(person_id)) {
                    //验证身份证的合法性的正则
                    pattern = new RegExp(/^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$/);
                    if (pattern.exec(person_id)) {
                        //获取15位证件号中的出生日期并转位正常日期		
                        birthday = "19" + person_id.substring(6, 8) + "-" + person_id.substring(8, 10) + "-" + person_id.substring(10, 12);
                    }
                    else {
                        person_id = person_id.replace(/x|X$/i, "a");
                        //获取18位证件号中的出生日期
                        birthday = person_id.substring(6, 10) + "-" + person_id.substring(10, 12) + "-" + person_id.substring(12, 14);

                        //校验18位身份证号码的合法性
                        for (var i = 17; i >= 0; i--) {
                            sum += (Math.pow(2, i) % 11) * parseInt(person_id.charAt(17 - i), 11);
                        }
                        if (sum % 11 != 1) {
                            msg = rules[0].msg + '提示:输入身份证号不符合国定标准!';
                            target = $(this);
                            return false;
                        }
                    }
                    //检测证件地区的合法性								
                    if (aCity[parseInt(person_id.substring(0, 2))] == null) {
                        msg = rules[i].msg + '提示:输入的身份证号的地区非法';
                        target = $(this);
                        return false;
                    }
                    var dateStr = new Date(birthday.replace(/-/g, "/"));

                    if (birthday != (dateStr.getFullYear() + "-" + Append_zore(dateStr.getMonth() + 1) + "-" + Append_zore(dateStr.getDate()))) {
                        msg = rules[i].msg + '提示:输入的身份证号的出生日期非法';
                        target = $(this);
                        return false;
                    }
                }
                else {
                    msg = rules[i].msg + '提示:输入的身份证号的长度或格式错误';
                    target = $(this);
                    return false;
                }
                return true;
            }
            //End  增加 身份证验证项，by  胡方扬 2013-01-07
        }
    });
    if (msg.length != 0 && target != null) {
        target.focus();
        target.css("background-color", "#d7f0fe");
        //Begin  修改   By Castle （胡方扬）2013-01-04   修改为LigerUI弹出方式
        //alert(msg);
        $.ligerDialog.warn(msg);
        //End By Castle （胡方扬） 2013-01-04 

        //        var i = 0;
        //        var valInterval = setInterval(function () {
        //            if (i == 6) {
        //                clearInterval(valInterval);
        //            }

        //            if (i % 2 == 0) {
        //                target.addClass("validateBorder");


        //            } else {
        //                target.removeClass("validateBorder");
        //            }
        //            i++;
        //        }, 200);


        return false;
    }
    return true;
}

//为值添加0 验证身份证号使用
function Append_zore(temp) {
    if (temp < 10) {
        return "0" + temp;
    }
    else {
        return temp;
    }
}    