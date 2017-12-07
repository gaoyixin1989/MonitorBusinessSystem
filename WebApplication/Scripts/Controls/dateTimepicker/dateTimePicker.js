$.fn.dateTimePicker = function (options) {
    var opt = { format: "%z-%m-%d" };
    var targetControl = $(this);
    $.extend(opt, options);

    var src = $("#jsJquery").attr("src");
    var src = src.substring(0, src.indexOf("/Scripts"));

    $.getScript(src + "/Scripts/anytime/anytime.js", function () {

        targetControl.each(function () {
            $(this).AnyTime_picker({
                format: opt.format,
                monthAbbreviations: ['一', '二', '三', '四', '五', '六', '七', '八', '九', '十', '十一', '十二'],
                dayAbbreviations: ['日', '一', '二', '三', '四', '五', '六'],
                labelDayOfMonth: '日期',
                labelHour: '小时',
                labelMinute: '分钟',
                labelMonth: '月份',
                labelSecond: '秒',
                labelYear: '年份',
                labelTitle: '选择日期'
            });
        });
    });
}