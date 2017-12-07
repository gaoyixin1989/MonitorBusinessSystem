<%@ Page Language="C#" AutoEventWireup="True" EnableTheming="false" Theme="" Inherits="Channels_Rpt_Template_FileEdit_bak" Codebehind="FileEdit_bak.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html style="overflow: hidden; margin: 0px;" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script language="javascript" type="text/javascript" for="WebOffice" event="OnMenuClick(vIndex,vCaption)">
   //打开本地文件
   if (vIndex==1)
   {  
      WebOpenLocal();
   }
   //打印文档
   if (vIndex==2)
   {  
      WebOpenPrint();
   }
   //保存文档
   if (vIndex==3)
   {  
      SaveDocument();
   }
    if (vIndex==4)
   { 
        WebSaveLocal();
   }
   if (vIndex==5)
   {  
      //定义签名
//      WebOpenSignature();
      WebWriteSignature();
      SaveDocumentNotClose();
   }
   //显示痕迹
   if (vIndex==6)
   {  
      ShowRevision(true);
   }
   //隐藏痕迹
   if (vIndex==7)
   {  
      ShowRevision();
   }
   //关闭
   if (vIndex==8)
   {  
      self.close();
   }
    </script>
    <script language="javascript" type="text/javascript">

        //作用：显示操作状态
        function StatusMsg(mString) {
            window.status = mString;
        }

        //作用：载入iWebOffice
        function Load() {
            //以下属性必须设置，实始化iWebOffice
            webform.WebOffice.WebUrl = "<%=mServerUrl%>";  //WebUrl:系统服务器路径
            webform.WebOffice.RecordID = "<%=mRecordID%>";   //RecordID:本文档记录编号
            webform.WebOffice.Template = "<%=mTemplate%>";   //Template:模板编号
            webform.WebOffice.FileName = "<%=mFileName%>";   //FileName:文档名称
            webform.WebOffice.FileType = "<%=mFileType%>";   //FileType:文档类型
            webform.WebOffice.EditType = "<%=mEditType%>";   //EditType:编辑类型
            webform.WebOffice.UserName = "<%=mUserName%>";   //UserName:操作用户名

            webform.WebOffice.AllowEmpty = false;

            //自定义菜单项
            webform.WebOffice.ShowMenu = "1";
            webform.WebOffice.AppendMenu("1", "打开文件");
            webform.WebOffice.AppendMenu("2", "打印文件");
            webform.WebOffice.AppendMenu("3", "保存文档");
            webform.WebOffice.AppendMenu("4", "保存到本地");
            webform.WebOffice.AppendMenu("5", "定义签名");
            webform.WebOffice.AppendMenu("6", "显示痕迹");
            webform.WebOffice.AppendMenu("7", "隐藏痕迹");
            webform.WebOffice.AppendMenu("8", "关闭");

            //禁止菜单
            webform.WebOffice.DisableMenu("宏;选项;帮助;");

            //禁止审阅菜单Word2007
            webform.WebOffice.Office2007Ribbon = 4;
            webform.WebOffice.RibbonUIXML = '<customUI xmlns="http://schemas.microsoft.com/office/2006/01/customui">' +
    '  <ribbon startFromScratch="false">' +       //true：全部屏蔽  false：不屏蔽
    '    <tabs>' +
    '      <tab idMso="TabReviewWord" visible="false">' +   //关闭审阅工具栏
    '      </tab>' +
    '    </tabs>' +
    '  </ribbon>' +
    '</customUI>';

            //打开该文档
            webform.WebOffice.WebOpen();

            //状态信息 	 
            StatusMsg(webform.WebOffice.Status);

            //设置合同ID
            webform.WebOffice.WebSetMsgByName("TASKID", document.getElementById("TASKID").value);
            webform.WebOffice.WebSetMsgByName("TEMPLATE", webform.WebOffice.Template);
            webform.WebOffice.WebSetMsgByName("ReportWf", document.getElementById("ReportWf").value);

            //加载标签内容
            LoadBookmarks();
            //加载监测项目
            LoadTestItem();
            //加载监测条件
            LoadTestCondition();

            //加载监测结果
            LoadTestResult();

            //加载测点分布示意图
            LoadSketchMap();

            //保护文档
            if (document.getElementById("PROTECT").value == "1") {
                WebProtect(true);
            }

            //禁止审阅菜单Word2003
            WebToolsVisible("Reviewing", false);

            StatusMsg("报告信息加载完成");
        }

        //作用：退出iWebOffice
        function UnLoad() {
            try {
                if (!webform.WebOffice.WebClose()) {
                    StatusMsg(webform.WebOffice.Status);
                }
                else {
                    StatusMsg("关闭文档...");
                }
            }
            catch (e) {
                alert(e.description);
            }
        }
        //加载监测项目
        function LoadTestItem() {
            StatusMsg("正在加载监测项目信息...");
            var TEST_ITEM_type = "";

            //加载监测项目
            webform.WebOffice.WebSetMsgByName("COMMAND", "1");
            webform.WebOffice.WebSetMsgByName("TEST_ITEM_type", TEST_ITEM_type);
            webform.WebOffice.WebSendMessage();

            var iTable, iRows, iCells, iName, iText;

            iCells = webform.WebOffice.WebGetMsgByName("CellsCount");
            iColumns = webform.WebOffice.WebGetMsgByName("ColumnsCount");
            var iColumnsEx = iColumns - 1;

            if (webform.WebOffice.WebObject.BookMarks.Exists("TEST_ITEM_TABLE") && iCells != "" && iColumns != "") {
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("TEST_ITEM_TABLE").Range, iCells, iColumnsEx, "1", "0"); //不显示仪器
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        if (i < 2) {
                            iName = (i + 1).toString() + "-" + (j + 1).toString();
                            iText = webform.WebOffice.WebGetMsgByName(iName);

                            iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText.replace("--", "—");
                        }
                        if (i > 2)//不显示仪器
                        {
                            iName = (i + 1).toString() + "-" + (j + 1).toString();
                            iText = webform.WebOffice.WebGetMsgByName(iName);

                            iTable.Columns(i).Cells(j + 1).Range.Text = iText.replace("--", "—");
                        }
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.Borders(2).LineStyle = 0;
                webform.WebOffice.WebObject.Application.Selection.Borders(4).LineStyle = 0;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
        }

        //加载监测结果
        function LoadTestResult() {
            StatusMsg("正在加载监测结果信息...");
            var contract_type = "";
            var str_contract_type = "";
            var str_contract_type_tables = "";
            var str_contract_type_SG_NOISE = "";
            var str_contract_type_GAS = "";

            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_AIR")) {
                //【监测结果-废气】
                contract_type = "RESULT_AIR";
                str_contract_type = contract_type;
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_GAS_SMALL")) {
                //【监测结果-废气-小型】
                contract_type = "RESULT_GAS_SMALL";
                str_contract_type = contract_type;
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_EMISSIVE")) {
                //【监测结果-放射性】
                contract_type = "RESULT_EMISSIVE";
                str_contract_type_GAS = contract_type;
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_FS")) {
                //【监测结果-放射性】//汕头
                contract_type = "RESULT_FS";
                str_contract_type = contract_type;
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_NOISE")) {
                //【监测结果-噪声】
                contract_type = "RESULT_NOISE";
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_QUAKE")) {
                //【监测结果-振动】
                contract_type = "RESULT_QUAKE";
                str_contract_type = contract_type;
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_SOLID")) {
                //【监测结果-固体】
                contract_type = "RESULT_SOLID";
                str_contract_type = contract_type;
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_WATER_THIN")) {
                //【监测结果-废水-不换行】
                contract_type = "RESULT_WATER_THIN";
                str_contract_type = contract_type;
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_WATER_WIDTH")) {
                //【监测结果-废水-换行】
                contract_type = "RESULT_WATER_WIDTH";
                str_contract_type_tables = contract_type;
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_WATER_VERTICAL")) {
                //【监测结果-废水-竖表】
                contract_type = "RESULT_WATER_VERTICAL";
                str_contract_type = contract_type;
            }

            //监测类别   未出综合报告前，报告分类别出，临时修改
            var ItemTypeID = document.getElementById("ItemTypeID").value;

            webform.WebOffice.WebSetMsgByName("COMMAND", "3");
            webform.WebOffice.WebSetMsgByName("CONTRACT_TYPE", contract_type);
            webform.WebOffice.WebSetMsgByName("ItemTypeID", ItemTypeID);
            webform.WebOffice.WebSendMessage();

            //加载数据
            var iTable, iRows, iCells, iName, iText;

            //【监测结果-废气】
            if (webform.WebOffice.WebObject.BookMarks.Exists(str_contract_type_GAS)) {
                var CellCount = str_contract_type_GAS + "CellsCount";
                var ColumnCount = str_contract_type_GAS + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks(str_contract_type_GAS).Range, iColumns, iCells, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = str_contract_type_GAS + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(j + 1).Cells(i + 1).Range.Text = iText.replace("--", "—");
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.Borders(2).LineStyle = 0;
                webform.WebOffice.WebObject.Application.Selection.Borders(4).LineStyle = 0;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
            //【监测结果-放射性】、【监测结果-放射性】//汕头、【监测结果-废气-小型】、【监测结果-振动】、【监测结果-室内空气质量】、【监测结果-固体】、【监测结果-废水-不换行】、【监测结果-废水-竖表】
            if (webform.WebOffice.WebObject.BookMarks.Exists(str_contract_type)) {
                var CellCount = str_contract_type + "CellsCount";
                var ColumnCount = str_contract_type + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks(str_contract_type).Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = str_contract_type + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText.replace("--", "—");
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.Borders(2).LineStyle = 0;
                webform.WebOffice.WebObject.Application.Selection.Borders(4).LineStyle = 0;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }

            //多表：【监测结果-废水-换行】
            if (webform.WebOffice.WebObject.BookMarks.Exists(str_contract_type_tables)) {
                var CellCount = str_contract_type_tables + "CellsCount";
                var ColumnCount = str_contract_type_tables + "ColumnsCount";
                var TableCount = str_contract_type_tables + "TableCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                iTables = webform.WebOffice.WebGetMsgByName(TableCount);
                if (iCells == "" || iColumns == "" || iTables == "") {
                    return;
                }
                for (var x = 0; x < iTables; x++) {
                    var mTable;
                    if (x == 0) {
                        mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks(str_contract_type_tables).Range, iCells, iColumns, "1", "0");
                    }
                    else {
                        webform.WebOffice.WebObject.Application.Selection.MoveDown(5, 1);
                        webform.WebOffice.WebObject.Application.Selection.TypeParagraph();
                        webform.WebOffice.WebObject.Application.Selection.TypeParagraph();
                        webform.WebOffice.WebObject.Application.Selection.MoveUp(5, 1);
                        webform.WebOffice.WebObject.Application.Keyboard(1033);
                        webform.WebOffice.WebObject.Application.Keyboard(2052);
                        webform.WebOffice.WebObject.Application.Selection.TypeText("续表" + x.toString());
                        webform.WebOffice.WebObject.Application.Selection.TypeParagraph();
                        mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.Application.Selection.Range, iCells, iColumns, "1", "0");
                    }

                    mTable.Style = "网格型";
                    for (var i = 0; i < iColumns; i++) {
                        for (var j = 0; j < iCells; j++) {
                            if (str_contract_type_tables == "RESULT_WATER_WIDTH") {
                                iName = str_contract_type_tables + x.toString() + "-" + (i + 1).toString() + "-" + (j + 1).toString();
                                iText = webform.WebOffice.WebGetMsgByName(iName);
                                mTable.Columns(i + 1).Cells(j + 1).Range.Text = iText.replace("--", "—");
                            }
                            if (str_contract_type_tables == "RESULT_GAS_MEDIUM") {
                                iName = str_contract_type_tables + x.toString() + "-" + (i + 1).toString() + "-" + (j + 1).toString();
                                iText = webform.WebOffice.WebGetMsgByName(iName);

                                mTable.Columns(i + 1).Cells(j + 1).Range.Text = iText.replace("--", "—");
                            }
                        }
                    }

                    //设置文本的居中方向
                    mTable.Select();
                    webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                    webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                    webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                    webform.WebOffice.WebObject.Application.Selection.Borders(2).LineStyle = 0;
                    webform.WebOffice.WebObject.Application.Selection.Borders(4).LineStyle = 0;
                    webform.WebOffice.WebObject.Application.Selection.EndKey();
                }
            }

            //【监测结果-噪声】
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_NOISE")) {
                var CellCount = "RESULT_NOISE" + "CellsCount";
                var ColumnCount = "RESULT_NOISE" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }

                var iCellsEx1 = 0;
                if (iColumns == 2) {
                    iCellsEx1 = Number(iCells) + 2;
                }

                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("RESULT_NOISE").Range, iCellsEx1, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    var iCellsEx = iCellsEx1 - 2;
                    if (iColumns == 2) {
                        iCellsEx = iCellsEx1;
                    }
                    for (var j = 0; j < iCellsEx; j++) {
                        iName = "RESULT_NOISE" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText.replace("--", "—");
                    }
                }

                if (iColumns == 2) {
                    iName = "SOUND_ST";
                    iText = webform.WebOffice.WebGetMsgByName(iName);
                    iTable.Columns(1).Cells(iCellsEx1 - 1).Range.Text = "执行标准";
                    iTable.Columns(2).Cells(iCellsEx1 - 1).Range.Text = iText; //GB 22337－2008表2中1类区B类房间夜间标准
                    iName = "SOUND_evaluation";
                    iText = webform.WebOffice.WebGetMsgByName(iName);
                    iTable.Columns(1).Cells(iCellsEx1).Range.Text = "评价结果";
                    iTable.Columns(2).Cells(iCellsEx1).Range.Text = iText;
                }

                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
        }

        //加载测点分布示意图
        function LoadSketchMap() {
        }

        //作用：打开文档
        function LoadDocument() {
            StatusMsg("正在打开文档...");
            if (!webform.WebOffice.WebOpen()) {
                //打开该文档    交互OfficeServer的OPTION="LOADFILE"
                StatusMsg(webform.WebOffice.Status);
            }
            else {
                StatusMsg(webform.WebOffice.Status);
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
                if (iOldRptID == "") {
                    var iNewReportID = webform.WebOffice.WebGetMsgByName("RECORDID");
                    document.getElementById("ReportWf").value = "ReportSchedule";
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

        //作用：保存文档
        function SaveDocumentNotClose() {
            if (!webform.WebOffice.WebSave()) {
                //交互OfficeServer的OPTION="SAVEFILE"
                StatusMsg(webform.WebOffice.Status);
                alert(webform.WebOffice.Status);
                return false;
            }
            else {
                StatusMsg(webform.WebOffice.Status);
                return true;
            }
        }

        //作用：显示或隐藏痕迹  true表示隐藏痕迹  false表示显示痕迹
        function ShowRevision(mValue) {
            if (mValue) {
                webform.WebOffice.WebShow(true);
                StatusMsg("显示痕迹...");
            }
            else {
                webform.WebOffice.WebShow(false);
                StatusMsg("隐藏痕迹...");
            }
        }

        //作用：获取痕迹
        function WebGetRevisions() {
            var i;
            var Text = "";
            for (i = 1; i <= webform.WebOffice.WebObject.Revisions.Count; i++) {
                Text = Text + webform.WebOffice.WebObject.Revisions.Item(i).Author;
                if (webform.WebOffice.WebObject.Revisions.Item(i).Type == "1") {
                    Text = Text + '插入:' + webform.WebOffice.WebObject.Revisions.Item(i).Range.Text + "\r\n";
                }
                else {
                    Text = Text + '删除:' + webform.WebOffice.WebObject.Revisions.Item(i).Range.Text + "\r\n";
                }
            }
            alert("痕迹内容：\r\n" + Text);
        }

        //作用：刷新文档
        function WebReFresh() {
            webform.WebOffice.WebReFresh();
            StatusMsg("文档已刷新...");
        }

        //作用：打开版本
        function WebOpenVersion() {
            webform.WebOffice.WebOpenVersion();
            StatusMsg(webform.WebOffice.Status);
        }

        //作用：保存版本
        function WebSaveVersion() {
            webform.WebOffice.WebSaveVersion();
            StatusMsg(webform.WebOffice.Status);
        }

        //作用：保存当前版本
        function WebSaveVersionByFileID() {
            var mText = window.prompt("请输入版本说明:", "版本号:V");
            if (mText == null) {
                mText = "已修改版本.";
            }
            webform.WebOffice.WebSaveVersionByFileID(mText);
            StatusMsg(webform.WebOffice.Status);
        }


        //作用：填充标签
        function LoadBookmarks() {
            StatusMsg("正在加载标签信息...");
            webform.WebOffice.WebSetMsgByName("TASKID", document.getElementById("TASKID").value); //合同ID
            //监测类别   未出综合报告前，报告分类别出，临时修改
            var ItemTypeID = document.getElementById("ItemTypeID").value;
            webform.WebOffice.WebSetMsgByName("ItemTypeID", ItemTypeID);

            var isG = "0";
            webform.WebOffice.WebSetMsgByName("isG", isG);

            //是否有单位说明字符串,仅废水、地表水
            var isFUnit = "0";
            var isDUnit = "0";
            if (webform.WebOffice.WebObject.BookMarks.Exists("UNIT_F_INFO1")) {
                isFUnit = "1";
            }
            webform.WebOffice.WebSetMsgByName("mHasFUnit", isFUnit);
            if (webform.WebOffice.WebObject.BookMarks.Exists("UNIT_D_INFO1")) {
                isDUnit = "1";
            }
            webform.WebOffice.WebSetMsgByName("mHasDUnit", isDUnit);

            if (!webform.WebOffice.WebLoadBookmarks()) {
                StatusMsg(webform.WebOffice.Status);
            }
            else {
                StatusMsg(webform.WebOffice.Status);
            }
        }

        //作用：标签管理
        function WebOpenBookMarks() {
            try {
                webform.WebOffice.WebOpenBookmarks();
                StatusMsg(webform.WebOffice.Status);
            }
            catch (e) {
                alert(e.description);
            }
        }

        //作用：设置标签值
        function SetBookmarks(vbmName, vbmValue) {
            if (!webform.WebOffice.WebSetBookmarks(vbmName, vbmValue)) {
                StatusMsg(webform.WebOffice.Status);
            }
            else {
                StatusMsg(webform.WebOffice.Status);
            }
        }

        //作用：根据标签名称获取标签值  vbmName:标签名称
        function GetBookmarks(vbmName) {
            var vbmValue;
            vbmValue = webform.WebOffice.WebGetBookmarks(vbmName);

            return vbmValue;
        }

        //作用：打印文档
        function WebOpenPrint() {
            try {
                webform.WebOffice.WebOpenPrint();
                StatusMsg(webform.WebOffice.Status);
            }
            catch (e) {
                alert(e.description);
            }
        }


        //作用：页面设置
        function WebOpenPageSetup() {
            try {
                if (webform.WebOffice.FileType == ".doc") {
                    webform.WebOffice.WebObject.Application.Dialogs(178).Show();
                }
                if (webform.WebOffice.FileType == ".xls") {
                    webform.WebOffice.WebObject.Application.Dialogs(7).Show();
                }
            }
            catch (e) {
                alert(e.description);
            }
        }

        //作用：插入图片
        function WebOpenPicture() {
            try {
                webform.WebOffice.WebOpenPicture();
                StatusMsg(webform.WebOffice.Status);
            }
            catch (e) {
                alert(e.description);
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

                    if (webform.WebOffice.WebObject.BookMarks.Exists("RPT_APP_YEAR")) {
                        webform.WebOffice.WebSetBookMarks("RPT_APP_YEAR", Y);
                    }
                    if (webform.WebOffice.WebObject.BookMarks.Exists("RPT_APP_MONTH")) {
                        webform.WebOffice.WebSetBookMarks("RPT_APP_MONTH", M);
                    }
                    if (webform.WebOffice.WebObject.BookMarks.Exists("RPT_APP_DAY")) {
                        webform.WebOffice.WebSetBookMarks("RPT_APP_DAY", D);
                    }

                    if (webform.WebOffice.WebObject.BookMarks.Exists("RPT_APP_POSITION")) {
                        webform.WebOffice.WebSetBookMarks("RPT_APP_POSITION", document.getElementById("AppUser_Position").value);
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

        //作用：签名印章
        function WebOpenSignature() {
            try {
                webform.WebOffice.WebOpenSignature();
                StatusMsg(webform.WebOffice.Status);
            }
            catch (e) {
                alert(e.description);
            }
        }

        //作用：文档印章列表
        function WebShowSignature() {
            try {
                webform.WebOffice.WebShowSignature();
                StatusMsg(webform.WebOffice.Status);
            }
            catch (e) {
                alert(e.description);
            }
        }

        //作用：验证印章
        function WebCheckSignature() {
            try {
                var i = webform.WebOffice.WebCheckSignature();  	//交互OfficeServer的OPTION="LOADSIGNATURE"
                alert("检测结果：" + i + "\r\n 注释: (=-1 有非法印章) (=0 没有任何印章) (>=1 有多个合法印章)");
                StatusMsg(i);
            }
            catch (e) {
                alert(e.description);
            }
        }

        //作用：存为本地文件
        function WebSaveLocal() {
            try {
                webform.WebOffice.WebSaveLocal();
                StatusMsg(webform.WebOffice.Status);
            }
            catch (e) {
                alert(e.description);
            }
        }

        //作用：打开本地文件
        function WebOpenLocal() {
            try {
                webform.WebOffice.WebOpenLocal();
                StatusMsg(webform.WebOffice.Status);
            }
            catch (e) {
                alert(e.description);
            }
        }

        //作用：保存为HTML文档
        function WebSaveAsHtml() {
            try {
                if (webform.WebOffice.WebSaveAsHtml()) {
                    webform.HTMLPath.value = "HTML/<%=mRecordID%>.htm";
                    window.open("<%=mHttpUrl%>" + webform.HTMLPath.value);
                }
                StatusMsg(webform.WebOffice.Status);
            }
            catch (e) {
                alert(e.description);
            }
        }


        //作用：保存为文档图片
        function WebSaveAsPage() {
            try {
                if (webform.WebOffice.WebSaveImage()) {
                    webform.HTMLPath.value = "HTMLIMAGE/<%=mRecordID%>.htm";
                    window.open("<%=mHttpUrl%>" + webform.HTMLPath.value);
                }
                StatusMsg(webform.WebOffice.Status);
            }
            catch (e) {
                alert(e.description);
            }
        }


        //作用：关闭或显示工具 
        //参数1表示工具条名称,参数2表示是否打开(true/fals)
        function WebToolsVisible(ToolName, Visible) {
            try {
                webform.WebOffice.WebToolsVisible(ToolName, Visible);
                StatusMsg(webform.WebOffice.Status);
            }
            catch (e) {
                alert(e.description);
            }
        }


        //作用：禁止或启用工具 
        //参数1表示工具条名称  参数2表示工具条铵钮的编号,参数表示是否可用(true/false)
        function WebToolsEnable(ToolName, ToolIndex, Enable) {
            try {
                webform.WebOffice.WebToolsEnable(ToolName, ToolIndex, Enable);
                StatusMsg(webform.WebOffice.Status);
            }
            catch (e) {
                alert(e.description);
            }
        }

        //作用：保护与解除
        //参数：true/false
        function WebProtect(value) {
            try {
                webform.WebOffice.WebSetProtect(value, "");  //""表示密码为空
            }
            catch (e) {
                alert(e.description);
            }
        }

        //作用：是否允许拷贝(true/false)
        function WebEnableCopy(value) {
            try {
                webform.WebOffice.CopyType = value;
            }
            catch (e) {
                alert(e.description);
            }
        }

        //作用：获取文档的Text正文
        function WebGetWordContent() {
            try {
                alert(webform.WebOffice.WebObject.Content.Text);
            }
            catch (e) {
                alert(e.description);
            }
        }


        //作用：打印黑白文档
        function WebWordPrintBlackAndWhile() {
            var i, n;

            //图片变黑白
            i = 0;
            n = webform.WebOffice.WebObject.Shapes.Count;
            for (var i = 1; i <= n; i++) {
                webform.WebOffice.WebObject.Shapes.Item(i).PictureFormat.ColorType = 3;
            }
            i = 0;
            n = webform.WebOffice.WebObject.InlineShapes.Count;
            for (var i = 1; i <= n; i++) {
                webform.WebOffice.WebObject.InlineShapes.Item(i).PictureFormat.ColorType = 3;
            }

            //文字变黑白
            webform.WebOffice.WebObject.Application.Selection.WholeStory();
            webform.WebOffice.WebObject.Application.Selection.Range.Font.Color = 0;
        }

        //作用：导入Text纯文本
        function WebInportText() {
            var mText;
            webform.WebOffice.WebSetMsgByName("COMMAND", "INPORTTEXT");
            if (webform.WebOffice.WebSendMessage()) {
                //交互OfficeServer的OPTION="SENDMESSAGE"
                mText = webform.WebOffice.WebGetMsgByName("CONTENT");
                //取得OfficeServer传递的变量CONTENT值
                webform.WebOffice.WebObject.Application.Selection.Range.Text = mText;
                alert("导入文本成功");
            }
            StatusMsg(webform.WebOffice.Status);
        }


        //作用：导出Text
        function WebExportText() {
            var mText = webform.WebOffice.WebObject.Content.Text;
            //设置变量COMMAND="EXPORTTEXT"，在WebSendMessage()时，一起提交到OfficeServer中
            webform.WebOffice.WebSetMsgByName("COMMAND", "EXPORTTEXT");
            //设置变量CONTENT="mText"，在WebSendMessage()时，一起提交到OfficeServer中，可用于实现全文检索功能，对WORD的TEXT内容进行检索
            webform.WebOffice.WebSetMsgByName("CONTENT", mText);
            //交互OfficeServer的OPTION="SENDMESSAGE"
            if (webform.WebOffice.WebSendMessage()) {
                alert("导出文本成功");
            }
            StatusMsg(webform.WebOffice.Status);
        }


        //作用：获取文档页数
        function WebDocumentPageCount() {
            if (webform.WebOffice.FileType == ".doc") {
                var intPageTotal;
                intPageTotal = webform.WebOffice.WebObject.Application.ActiveDocument.BuiltInDocumentProperties(14);
                alert("文档页总数：" + intPageTotal);
            }
            if (webform.WebOffice.FileType == ".wps") {
                var intPageTotal;
                intPageTotal = webform.WebOffice.WebObject.PagesCount();
                alert("文档页总数：" + intPageTotal);
            }
        }

        //作用：签章锁定文件功能
        function WebSignatureAtReadonly() {
            //解除文档保护
            webform.WebOffice.WebSetProtect(false, "");
            //设置文档痕迹保留的状态  参数1:不显示痕迹  参数2:不保留痕迹  参数3:不打印时有痕迹  参数4:不显痕迹处理工具                 
            webform.WebOffice.WebSetRevision(false, false, false, false);
            try {
                //交互OfficeServer的 A签章列表OPTION="LOADMARKLIST"    B签章调出OPTION="LOADMARKIMAGE"    C确定签章OPTION="SAVESIGNATURE"
                webform.WebOffice.WebOpenSignature();
                StatusMsg(webform.WebOffice.Status);
            }
            catch (e) {
                alert(e.description);
            }
            //锁定文档
            webform.WebOffice.WebSetProtect(true, "");
        }

        //作用：取得服务器端时间，设置本地时间  [V6.0.1.5以上支持]
        function WebDateTime() {
            //交互OfficeServer的OPTION="DATETIME"   true表示返回并设置本地时间为服务器时间；false表示仅返回服务器时间
            mResult = webform.WebOffice.WebDateTime(true);
            //该功能主要用于在痕迹保留时读取服务器时间
            alert("提示：已经设置本地时间为 " + mResult);
        }
    </script>
    <title>iWebOffice在线编辑</title>
</head>
<body style="margin: 0px;" onload="Load()" onunload="UnLoad()">
    <form id="webform" runat="server" onsubmit="return SaveDocument();">
    <input type="hidden" name="RecordID" value="<%=mRecordID%>" />
    <input type="hidden" name="Template" value="<%=mTemplate%>" />
    <input type="hidden" name="FileType" value="<%=mFileType%>" />
    <input type="hidden" name="EditType" value="<%=mEditType%>" />
    <input type="hidden" name="HTMLPath" value="<%=mHTMLPath%>" />
    <input type="hidden" id="TASKID" value="<%=mTask%>" />
    <input type="hidden" id="PROTECT" value="<%=mProtect %>" />
    <input type="hidden" id="ReportWf" value="<%=mReportWf %>" />
    <input type="hidden" id="AppUser_Position" value="<%=mAppUser_Position %>" />
    <!--未出综合报告前，报告分类别出，临时修改-->
    <input type="hidden" id="ItemTypeID" value="<%=mItemType %>" />
    <div style="height: 100%;">
        <!--调用iWebOffice，注意版本号，可用于升级-->
        <script src="iWebOffice2003.js" type="text/jscript"></script>
    </div>
    <div style="display: none;" id="StatusBar">
        状态栏</div>
    </form>
</body>
</html>
