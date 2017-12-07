var objGrid = null;
$(document).ready(function () {

    //构建填报表格
    objGrid = $("#maingrid").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: false,
        alternatingRow: true,
        checkbox: true,
        width: '100%',
        height: "320",
        enabledEdit: false
        //toolbar: []
    });
    
    getData(); //一开始获取一次数据
});

//获取数据
function getData() {
    var strID = $("#hidID").val();

    if (strID != "") {
        $.ajax({
            url: "FillFlowHandler.ashx",
            data: "action=GetData&pf_id=" + strID,
            type: "post",
            dataType: "json",
            async: true,
            cache: false,
            beforeSend: function () {
                $.ligerDialog.waitting('数据加载中,请稍候...');
            },
            complete: function () {
                $.ligerDialog.closeWaitting();
            },
            success: function (json) {
                if (parseInt(json.Total) > 0) {

                    //构建表格列
                    //固定的列
                    var columnsArr = [
                    ];

                    //添加所有动态的列
                    $.each(json.UnSureColumns, function (i, n) {
                        columnsArr.push({ display: n.columnName, name: n.columnId, width: parseInt(n.columnWidth)+40, minWidth: 60, align: "center" });
                    });

                    objGrid.set("columns", columnsArr);
                    objGrid.set("data", json);

                    //隐藏不需要显示的列
                    objGrid.toggleCol("ID");
                    //objGrid.toggleCol("SECTION_ID");
                    //objGrid.toggleCol("POINT_ID");

                    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //去除第一行第一列的复选框
                }
                else {
                    objGrid.set("data", json);
                }
            }
        });
    }
}

function SendSave() {
}