//作用：填充标签
function LoadBookmarks() {
    StatusMsg("正在加载标签信息...");
    webform.WebOffice.WebSetMsgByName("TASKID", document.getElementById("TASKID").value); //监测任务ID
    //监测类别   未出综合报告前，报告分类别出，临时修改
    var ItemTypeID = document.getElementById("ItemTypeID").value;
    webform.WebOffice.WebSetMsgByName("ItemTypeID", ItemTypeID);

    if (!webform.WebOffice.WebLoadBookmarks()) {
        StatusMsg(webform.WebOffice.Status);
    }
    else {
        StatusMsg(webform.WebOffice.Status);
    }
}

//加载样品信息表格
function LoadSampleInfo_Table() {
    StatusMsg("正在加载样品信息...");
    var contract_type = "";
    var ItemTypeID = document.getElementById("ItemTypeID").value;

    if (webform.WebOffice.WebObject.BookMarks.Exists("SAMPLE_TABLE") | webform.WebOffice.WebObject.BookMarks.Exists("SAMPLE_SEND_TABLE") ) {
        //【监测结果-废水】
        contract_type = "SAMPLE_TABLE";
        if (webform.WebOffice.WebObject.BookMarks.Exists("SAMPLE_SEND_TABLE"))
            contract_type = "SAMPLE_SEND_TABLE";

        WebOffice_SendMsg("4", contract_type, ItemTypeID);

        drawTable(contract_type, 1);
    }
    if (webform.WebOffice.WebObject.BookMarks.Exists("SAMPLE_TABLE_WATER")) {
        contract_type = "SAMPLE_TABLE_WATER";

        WebOffice_SendMsg("4", contract_type, ItemTypeID);

        drawTable(contract_type, 0);
    }
    if (webform.WebOffice.WebObject.BookMarks.Exists("SAMPLE_SEND_TABLE_WATER")) {
        contract_type = "SAMPLE_SEND_TABLE_WATER";

        WebOffice_SendMsg("4", contract_type, ItemTypeID);

        drawTable(contract_type, 0);
    }
    if (webform.WebOffice.WebObject.BookMarks.Exists("SAMPLE_SEND_TABLE_GAS")) {
        contract_type = "SAMPLE_SEND_TABLE_GAS";

        WebOffice_SendMsg("4", contract_type, ItemTypeID);

        drawTable(contract_type, 0);
    }
}

//加载监测参数
function LoadTestAttribute() {
    StatusMsg("正在加载监测参数信息...");
    var ItemTypeID = document.getElementById("ItemTypeID").value;
    var intLineStyle = 0; //intLineStyle，0，无边框；1，有边框

    if (webform.WebOffice.WebObject.BookMarks.Exists("ATTRIBUTE_TABLE")) {
        var contract_type = "ATTRIBUTE_TABLE";
        
        intLineStyle = 1; //intLineStyle，0，无边框；1，有边框

        WebOffice_SendMsg("2", contract_type, ItemTypeID);

        drawTable(contract_type, intLineStyle);
    }
    //油烟监测参数
    if (webform.WebOffice.WebObject.BookMarks.Exists("ATTRIBUTE_TABLE_YY")) {
        contract_type = "ATTRIBUTE_TABLE_YY";

        intLineStyle = 0; //intLineStyle，0，无边框；1，有边框

        WebOffice_SendMsg("2", contract_type, ItemTypeID);

        drawTable(contract_type, intLineStyle);
    }
    //烟气黑度监测参数
    if (webform.WebOffice.WebObject.BookMarks.Exists("ATTRIBUTE_TABLE_YH")) {
        contract_type = "ATTRIBUTE_TABLE_YH";

        intLineStyle = 0; //intLineStyle，0，无边框；1，有边框

        WebOffice_SendMsg("2", contract_type, ItemTypeID);

        drawTable(contract_type, intLineStyle);
    }
}

//加载监测项目
function LoadTestItem() {
    StatusMsg("正在加载监测项目信息...");
    var TEST_ITEM_type = "";
    var intLineStyle = 0; //intLineStyle，0，无边框；1，有边框

    if (webform.WebOffice.WebObject.BookMarks.Exists("TEST_ITEM_TABLE") | webform.WebOffice.WebObject.BookMarks.Exists("TEST_ITEM_TABLE_QY")) {
        var contract_type = "TEST_ITEM_TABLE";
        if (webform.WebOffice.WebObject.BookMarks.Exists("TEST_ITEM_TABLE_QY")) {
            contract_type = "TEST_ITEM_TABLE_QY";
            intLineStyle = 0; //intLineStyle，0，无边框；1，有边框
        }

        //加载监测项目
        WebOffice_SendMsg("1", contract_type, TEST_ITEM_type);

        drawTable(contract_type, intLineStyle);
    }
}

//加载监测结果
function LoadTestResult() {
    StatusMsg("正在加载监测结果信息...");
    var contract_type = "";
    var ItemTypeID = document.getElementById("ItemTypeID").value;

    var arrayContract_type = new Array("RESULT_WATER", "RESULT_GAS", "RESULT_SOLID", "RESULT_NOISE", "RESULT_EMISSIVE", "RESULT_WATER_QY1", "RESULT_WATER_QY2",
    "RESULT_NOISE_QY", "RESULT_SOLID_QY1", "RESULT_SOLID_QY2", "RESULT_SOLID_QY3", "RESULT_SOLID_QY4", "RESULT_AIR_QY1", "RESULT_AIR_QY_YY", "RESULT_AIR_QY_YH",
    "RESULT_AIR_QY2", "RESULT_AIR_QY3", "ATTRIBUTE_TABLE_AIR", "RESULT_AIR_QY4", "RESULT_AIR_QY5");
    //【监测结果-废水】,【监测结果-废气】,【监测结果-固体】,【监测结果-噪声】,【监测结果-放射性】,【监测结果-废水-清远-竖】,【监测结果-废水送样-清远-竖】,
    //【监测结果-噪声-清远-竖】,【监测结果-土壤-清远-竖】,【监测结果-固废-清远-竖】,【监测结果-底泥-清远-竖】,【监测结果-煤质-清远-竖】,【监测结果-废气-清远-竖】,【监测结果-油烟-清远-竖】,【监测结果-烟气黑度-清远-竖】
    //【监测结果-烟气黑度-清远-竖】,【监测结果-SO2-清远-竖】

    var arrayIntLineStyle = new Array(0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 0);

    for (i = 0; i < arrayContract_type.length; i++) {
        contract_type = arrayContract_type[i];
        intLineStyle = arrayIntLineStyle[i];

        if (webform.WebOffice.WebObject.BookMarks.Exists(contract_type)) {
            WebOffice_SendMsg("3", contract_type, ItemTypeID);

            drawTable(contract_type, intLineStyle);
        }
    }
}

function WebOffice_SendMsg(strCommand, strContractType, strItemTypeID) {
    webform.WebOffice.WebSetMsgByName("COMMAND", strCommand);
    webform.WebOffice.WebSetMsgByName("CONTRACT_TYPE", strContractType);
    webform.WebOffice.WebSetMsgByName("ItemTypeID", strItemTypeID);
    webform.WebOffice.WebSendMessage();
}

//intLineStyle，0，无边框；1，有边框
function drawTable(contract_type, intLineStyle)
{
    //加载数据
    var iTable, iRows, iCells, iName, iText;

    var CellCount = contract_type + "CellsCount";
    var ColumnCount = contract_type + "ColumnsCount";
    var TableCount = contract_type + "TableCount";
    iCells = webform.WebOffice.WebGetMsgByName(CellCount);
    iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
    iTables = webform.WebOffice.WebGetMsgByName(TableCount);

    if (iCells == "" || iColumns == "" || iTables == "") {
        return;
    }

    for (var x = 0; x < iTables; x++) {
        var mTable;
        if (x == 0) {
            mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks(contract_type).Range, iCells, iColumns, "1", "0");
        }
        else {
            webform.WebOffice.WebObject.Application.Selection.MoveDown(5, 1);
            webform.WebOffice.WebObject.Application.Selection.TypeParagraph();
            webform.WebOffice.WebObject.Application.Selection.TypeParagraph();
            webform.WebOffice.WebObject.Application.Selection.MoveUp(5, 1);
            webform.WebOffice.WebObject.Application.Keyboard(1033);
            webform.WebOffice.WebObject.Application.Keyboard(2052);
            //webform.WebOffice.WebObject.Application.Selection.TypeText("续表" + x.toString());
            webform.WebOffice.WebObject.Application.Selection.TypeText("续表");
            webform.WebOffice.WebObject.Application.Selection.TypeParagraph();
            mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.Application.Selection.Range, iCells, iColumns, "1", "0");
        }

        mTable.Style = "网格型";
        for (var i = 1; i <= iColumns; i++) {
            for (var j = 1; j <= iCells; j++) {
                iName = contract_type + x.toString() + "-" + i + "-" + j;
                iText = webform.WebOffice.WebGetMsgByName(iName);
                mTable.Columns(i).Cells(j).Range.Text = iText;
            }
        }

        //设置文本的居中方向
        mTable.Select();
        webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
        webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
        webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
        if(contract_type=="TEST_ITEM_TABLE_QY"){
            webform.WebOffice.WebObject.Application.Selection.Columns(1).Width = 85;
            webform.WebOffice.WebObject.Application.Selection.Columns(2).Width = 140;
            webform.WebOffice.WebObject.Application.Selection.Columns(3).Width = 110;
            webform.WebOffice.WebObject.Application.Selection.Columns(4).Width = 80;
        }
        webform.WebOffice.WebObject.Application.Selection.Borders(2).LineStyle = intLineStyle; //intLineStyle，0，无边框；1，有边框
        webform.WebOffice.WebObject.Application.Selection.Borders(4).LineStyle = intLineStyle; //intLineStyle，0，无边框；1，有边框
        webform.WebOffice.WebObject.Application.Selection.EndKey();
    }
}

//作用：保存文档
function SaveDocument() {
    var iOldRptID = webform.WebOffice.RecordID;
    if (!webform.WebOffice.WebSave()) {
        //交互OfficeServer的OPTION="SAVEFILE"
        StatusMsg(webform.WebOffice.Status);
        alert(webform.WebOffice.Status);
        return false;
    }
    else {
        StatusMsg(webform.WebOffice.Status);
        if (iOldRptID == "") {//iOldRptID为空，说明是新保存，就是在报告编制环节，而且是新建的报告
            var iNewReportID = webform.WebOffice.WebGetMsgByName("RECORDID");
            document.getElementById("ReportWf").value = "ReportSchedule";
            //保存成果，重载报告
            webform.WebOffice.WebClose();
            webform.WebOffice.WebSetMsgByName("RECORDID", iNewReportID);
            webform.WebOffice.WebSetMsgByName("ReportWf", "ReportSchedule");
            webform.WebOffice.WebSetMsgByName("EDIT_TYPE", "1");
            webform.WebOffice.RecordID = iNewReportID;   //RecordID:本文档记录编号
            webform.WebOffice.Template = iNewReportID + ".doc";   //Template:模板编号
            webform.WebOffice.FileType = ".doc";   //FileType:文档类型
            webform.WebOffice.EditType = "1";   //EditType:编辑类型
            webform.WebOffice.WebOpen();
            WebProtect(false);
        }
        else {
            if (confirm("保存成功，关闭本页面?")) {
                self.close();
            }
        }

        return true;
    }
}

//作用：签名
function WebWriteSignature() {
    try {
        StatusMsg("正在加载签名...");
        var contract_type = "";

        var strRWF = document.getElementById("ReportWf").value;

        if (strRWF == "ReportSchedule") {
            contract_type = "REPORT_SCHEDULE_USERID";
        }
        if (strRWF == "ReportCheck") {
            contract_type = "REPORT_CHECK_USERID";
        }
        if (strRWF == "ReportQCCheck") {
            contract_type = "REPORT_QCCHECK_USERID";
        }
        if (strRWF == "ReportApprove") {
            contract_type = "REPORT_APPROVE_USERID";

            var Nowdate = new Date();
            var Y = Nowdate.getFullYear();
            var M = Number(Nowdate.getMonth()) + 1;
            var D = Nowdate.getDate();

            if (webform.WebOffice.WebObject.BookMarks.Exists("REPORT_APPROVE_DATE")) {
                webform.WebOffice.WebSetBookMarks("REPORT_APPROVE_DATE", Y + "年" + M + "月" + D + "日");
            }
        }

        if (webform.WebOffice.WebObject.BookMarks.Exists(contract_type)) {
            webform.WebOffice.WebSetBookMarks(contract_type, "");
            webform.WebOffice.WebInsertImage(contract_type, "admin.jpg", true, 4);
        }
    }
    catch (e) {
        alert(e.description);
    }
}

/////清远
//加载【清远烟尘监测情况】 
function Load_QY_SO2_TESTINFO() {
    StatusMsg("正在加载监测情况信息...");
    var ItemTypeID = document.getElementById("ItemTypeID").value;
    var intLineStyle = 0; //intLineStyle，0，无边框；1，有边框

    if (webform.WebOffice.WebObject.BookMarks.Exists("QY_SO2_TESTINFO")) {
        var contract_type = "QY_SO2_TESTINFO";
        intLineStyle = 0; //intLineStyle，0，无边框；1，有边框

        WebOffice_SendMsg("6", contract_type, ItemTypeID);

        drawTable(contract_type, intLineStyle);
    }
}

//加载【清远烟尘监测结果】 
function LoadQY_SO2_RESULT() {
    StatusMsg("正在加载监测结果信息...");
    
    var ItemTypeID = document.getElementById("ItemTypeID").value;
    var intLineStyle = 0;  //intLineStyle，0，无边框；1，有边框

    if (webform.WebOffice.WebObject.BookMarks.Exists("QY_SO2_RESULT")) {
        var contract_type = "QY_SO2_RESULT";
        intLineStyle = 0; //intLineStyle，0，无边框；1，有边框

        WebOffice_SendMsg("7", contract_type, ItemTypeID);

        drawTable(contract_type, intLineStyle);
    }
}

//作用：点位图
function LoadSketchMap() {
    StatusMsg("正在加载点位图...");
    var contract_type = "SKETCH_MAP";

    if (webform.WebOffice.WebObject.BookMarks.Exists(contract_type)) {
        webform.WebOffice.WebSetBookMarks(contract_type, "         ");
        webform.WebOffice.WebSetMsgByName("CONTRACT_TYPE", contract_type);
        webform.WebOffice.WebInsertImage(contract_type, "admin.jpg", true, 4);
    }
}