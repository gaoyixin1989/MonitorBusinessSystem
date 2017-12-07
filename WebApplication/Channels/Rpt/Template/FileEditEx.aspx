<%@ Page Language="C#" AutoEventWireup="True" EnableTheming="false" Theme=""
    Inherits="Channels_Rpt_Template_FileEditEx" Codebehind="FileEditEx.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html style="overflow: hidden; margin: 0px;" xmlns="http://www.w3.org/1999/xhtml">
<head>
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
            //webform.WebOffice.AppendMenu("1","打开文件");
            webform.WebOffice.AppendMenu("2", "打印文件");
            //webform.WebOffice.AppendMenu("3","保存文档");
            //webform.WebOffice.AppendMenu("4","保存到本地");
            //webform.WebOffice.AppendMenu("5","定义签名");
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
            if (webform.WebOffice.WebObject.BookMarks.Exists("SYSTEM1_CONDICTION")) {
                LoadTestCondition_1SYSTEM();
                LoadTestCondition_2SYSTEM();
            }
            else if (webform.WebOffice.WebObject.BookMarks.Exists("SHAOGANG_CONDICTION")) {
                LoadTestCondition_Shaogang();
            }
            else {
                LoadTestCondition();
            }

            //加载监测结果
            LoadTestResult();

            if (webform.WebOffice.WebObject.BookMarks.Exists("SYSTEM1_RESULT")) {
                LoadTestResult_Sys1();
                LoadTestResult_Sys2()
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("SHAOGANG_RESULT")) {
                LoadTestResult_Shaogang();
            }

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
            if (webform.WebOffice.WebObject.BookMarks.Exists("TEST_ITEM_THREE_TABLE")) {
                //【三同时监测项目】
                TEST_ITEM_type = "TEST_ITEM_THREE_type";
            }

            //加载监测项目
            webform.WebOffice.WebSetMsgByName("COMMAND", "1");
            webform.WebOffice.WebSetMsgByName("TEST_ITEM_type", TEST_ITEM_type);
            webform.WebOffice.WebSendMessage();

            var iTable, iRows, iCells, iName, iText;

            iCells = webform.WebOffice.WebGetMsgByName("CellsCount");
            iColumns = webform.WebOffice.WebGetMsgByName("ColumnsCount");

            if (webform.WebOffice.WebObject.BookMarks.Exists("TEST_ITEM_TABLE") && iCells != "" && iColumns != "") {
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("TEST_ITEM_TABLE").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("TEST_ITEM_THREE_TABLE") && iCells != "" && iColumns != "") {
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("TEST_ITEM_THREE_TABLE").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }

            if (webform.WebOffice.WebObject.BookMarks.Exists("G_TEST_ITEM_TABLE")) {
                //【常规监测项目表格】
                TEST_ITEM_type = "G_TEST_ITEM";
                iCells = 0;
                iColumns = 0;

                //加载监测项目
                webform.WebOffice.WebSetMsgByName("COMMAND", "1");
                webform.WebOffice.WebSetMsgByName("TEST_ITEM_type", TEST_ITEM_type);
                webform.WebOffice.WebSendMessage();

                iCells = webform.WebOffice.WebGetMsgByName("CellsCount");
                iColumns = webform.WebOffice.WebGetMsgByName("ColumnsCount");

                if (iCells != "" && iColumns != "") {
                    iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("G_TEST_ITEM_TABLE").Range, iCells, iColumns, "1", "0");
                    iTable.Style = "网格型";

                    for (var i = 0; i < iColumns; i++) {
                        for (var j = 0; j < iCells; j++) {
                            iName = (i + 1).toString() + "-" + (j + 1).toString();
                            iText = webform.WebOffice.WebGetMsgByName(iName);

                            iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                        }
                    }
                    //设置文本的居中方向
                    iTable.Select();
                    webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                    webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                    webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                    webform.WebOffice.WebObject.Application.Selection.EndKey();
                }
            }
        }
        //加载监测条件
        //【韶钢条件参数】
        function LoadTestCondition_Shaogang() {
            StatusMsg("正在加载监测条件信息...");
            var condition_type = "SHAOGANG_CONDICTION";

            webform.WebOffice.WebSetMsgByName("COMMAND", "2");
            webform.WebOffice.WebSetMsgByName("CONDITION_TYPE", condition_type);
            webform.WebOffice.WebSendMessage();

            //加载数据
            var iTable, iRows, iCells, iName, iText;

            var TableCount = "SHAOGANG_CONDICTION" + "TableCount";
            iTables = webform.WebOffice.WebGetMsgByName(TableCount);

            for (var x = 0; x < iTables; x++) {
                var CellCount = "SHAOGANG_CONDICTION" + x.toString() + "CellsCount";
                var ColumnCount = "SHAOGANG_CONDICTION" + x.toString() + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);

                var mTable;
                if (x == 0) {
                    mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("SHAOGANG_CONDICTION").Range, iCells, iColumns, "1", "0");
                }
                else {
                    webform.WebOffice.WebObject.Application.Selection.MoveDown(5, 1);
                    webform.WebOffice.WebObject.Application.Selection.TypeParagraph();
                    webform.WebOffice.WebObject.Application.Selection.MoveUp(5, 1);
                    webform.WebOffice.WebObject.Application.Selection.InsertBreak(7);
                    mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.Application.Selection.Range, iCells, iColumns, "1", "0");
                }

                mTable.Style = "网格型";
                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "SHAOGANG_CONDICTION" + x.toString() + "-" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        mTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                mTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
        }
        //【一系统条件参数】
        function LoadTestCondition_1SYSTEM() {
            StatusMsg("正在加载监测条件信息...");
            var condition_type = "SYSTEM1_CONDICTION";

            webform.WebOffice.WebSetMsgByName("COMMAND", "2");
            webform.WebOffice.WebSetMsgByName("CONDITION_TYPE", condition_type);
            webform.WebOffice.WebSendMessage();

            //加载数据
            var iTable, iRows, iCells, iName, iText;


            var TableCount = "SYSTEM1_CONDICTION" + "TableCount";
            iTables = webform.WebOffice.WebGetMsgByName(TableCount);

            for (var x = 0; x < iTables; x++) {
                var CellCount = "SYSTEM1_CONDICTION" + x.toString() + "CellsCount";
                var ColumnCount = "SYSTEM1_CONDICTION" + x.toString() + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);

                var mTable;
                if (x == 0) {
                    mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("SYSTEM1_CONDICTION").Range, iCells, iColumns, "1", "0");
                }
                else {
                    webform.WebOffice.WebObject.Application.Selection.MoveDown(5, 1);
                    webform.WebOffice.WebObject.Application.Selection.TypeParagraph();
                    webform.WebOffice.WebObject.Application.Selection.MoveUp(5, 1);
                    webform.WebOffice.WebObject.Application.Selection.InsertBreak(7);
                    mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.Application.Selection.Range, iCells, iColumns, "1", "0");
                }

                mTable.Style = "网格型";
                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "SYSTEM1_CONDICTION" + x.toString() + "-" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        mTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                mTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
        }
        function LoadTestCondition_2SYSTEM() {
            StatusMsg("正在加载监测条件信息...");
            var condition_type = "SYSTEM2_CONDICTION";

            webform.WebOffice.WebSetMsgByName("COMMAND", "2");
            webform.WebOffice.WebSetMsgByName("CONDITION_TYPE", condition_type);
            webform.WebOffice.WebSendMessage();

            //加载数据
            var iTable, iRows, iCells, iName, iText;
            //【监测条件-韶冶二系统】
            var TableCount = "SYSTEM2_CONDICTION" + "TableCount";
            iTables = webform.WebOffice.WebGetMsgByName(TableCount);

            for (var x = 0; x < iTables; x++) {
                var CellCount = "SYSTEM2_CONDICTION" + x.toString() + "CellsCount";
                var ColumnCount = "SYSTEM2_CONDICTION" + x.toString() + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);

                var mTable;
                if (x == 0) {
                    mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("SYSTEM2_CONDICTION").Range, iCells, iColumns, "1", "0");
                }
                else {
                    webform.WebOffice.WebObject.Application.Selection.MoveDown(5, 1);
                    webform.WebOffice.WebObject.Application.Selection.TypeParagraph();
                    webform.WebOffice.WebObject.Application.Selection.MoveUp(5, 1);
                    webform.WebOffice.WebObject.Application.Selection.InsertBreak(7);

                    mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.Application.Selection.Range, iCells, iColumns, "1", "0");
                }

                mTable.Style = "网格型";
                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "SYSTEM2_CONDICTION" + x.toString() + "-" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        mTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                mTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
        }
        function LoadTestCondition() {
            StatusMsg("正在加载监测条件信息...");
            var condition_type = "";
            if (webform.WebOffice.WebObject.BookMarks.Exists("CONDITION_EMISSIVE")) {
                //【监测条件-放射性】
                condition_type = "CONDITION_EMISSIVE";
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("CONDITION_GAS_MEDIUM")) {
                //【监测条件-废气-中型】
                condition_type = "CONDITION_GAS_MEDIUM";
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("CONDITION_GAS_SMALL")) {
                //【监测条件-废气-小型】
                condition_type = "CONDITION_GAS_SMALL";
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("CONDITION_NOISE")) {
                //【监测条件-噪声】
                condition_type = "CONDITION_NOISE";
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("CONDITION_QUAKE")) {
                //【监测条件-振动】
                condition_type = "CONDITION_QUAKE";
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_THREE_GAS_CONDITION")) {
                //【监测结果-三同时-废气-条件】
                condition_type = "RESULT_THREE_GAS_CONDITION";
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("CONDITION_GAS_HUGE")) {
                //【监测条件-废气-大型】
                condition_type = "CONDITION_GAS_HUGE";
            }

            webform.WebOffice.WebSetMsgByName("COMMAND", "2");
            webform.WebOffice.WebSetMsgByName("CONDITION_TYPE", condition_type);
            webform.WebOffice.WebSendMessage();

            //加载数据
            var iTable, iRows, iCells, iName, iText;

            //【监测条件-放射性】
            if (webform.WebOffice.WebObject.BookMarks.Exists("CONDITION_EMISSIVE")) {
                var CellCount = "CONDITION_EMISSIVE" + "CellsCount";
                var ColumnCount = "CONDITION_EMISSIVE" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("CONDITION_EMISSIVE").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "CONDITION_EMISSIVE" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
            //【监测条件-废气-中型】
            if (webform.WebOffice.WebObject.BookMarks.Exists("CONDITION_GAS_MEDIUM")) {
                var CellCount = "CONDITION_GAS_MEDIUM" + "CellsCount";
                var ColumnCount = "CONDITION_GAS_MEDIUM" + "ColumnsCount";
                var TableCount = "CONDITION_GAS_MEDIUM" + "TableCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                iTables = webform.WebOffice.WebGetMsgByName(TableCount);

                if (iCells == "" || iColumns == "" || iTables == "") {
                    return;
                }

                for (var x = 0; x < iTables; x++) {
                    var mTable;
                    if (x == 0) {
                        mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("CONDITION_GAS_MEDIUM").Range, iCells, iColumns, "1", "0");
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
                            iName = "CONDITION_GAS_MEDIUM" + x.toString() + "-" + (i + 1).toString() + "-" + (j + 1).toString();
                            iText = webform.WebOffice.WebGetMsgByName(iName);

                            mTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                        }
                    }
                    //设置文本的居中方向
                    mTable.Select();
                    webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                    webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                    webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                    webform.WebOffice.WebObject.Application.Selection.EndKey();
                }
            }
            //【监测条件-废气-小型】
            if (webform.WebOffice.WebObject.BookMarks.Exists("CONDITION_GAS_SMALL")) {
                var CellCount = "CONDITION_GAS_SMALL" + "CellsCount";
                var ColumnCount = "CONDITION_GAS_SMALL" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("CONDITION_GAS_SMALL").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "CONDITION_GAS_SMALL" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
            //【监测条件-噪声】
            if (webform.WebOffice.WebObject.BookMarks.Exists("CONDITION_NOISE")) {
                var CellCount = "CONDITION_NOISE" + "CellsCount";
                var ColumnCount = "CONDITION_NOISE" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("CONDITION_NOISE").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "CONDITION_NOISE" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
            //【监测条件-振动】
            if (webform.WebOffice.WebObject.BookMarks.Exists("CONDITION_QUAKE")) {
                var CellCount = "CONDITION_QUAKE" + "CellsCount";
                var ColumnCount = "CONDITION_QUAKE" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("CONDITION_QUAKE").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "CONDITION_QUAKE" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
            //【监测结果-三同时-废气-条件】
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_THREE_GAS_CONDITION")) {
                var CellCount = "RESULT_THREE_GAS_CONDITION" + "CellsCount";
                var ColumnCount = "RESULT_THREE_GAS_CONDITION" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("RESULT_THREE_GAS_CONDITION").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "RESULT_THREE_GAS_CONDITION" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
            //【监测条件-废气-大型】
            if (webform.WebOffice.WebObject.BookMarks.Exists("CONDITION_GAS_HUGE")) {
                var CellCount = "CONDITION_GAS_HUGE" + "CellsCount";
                var ColumnCount = "CONDITION_GAS_HUGE" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("CONDITION_GAS_HUGE").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "CONDITION_GAS_HUGE" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }

            //【监测条件-废气-常规-小型】
            if (webform.WebOffice.WebObject.BookMarks.Exists("G_CONDITION_GAS_SMALL")) {
                condition_type = "G_CONDITION_GAS_SMALL";
                webform.WebOffice.WebSetMsgByName("COMMAND", "2");
                webform.WebOffice.WebSetMsgByName("CONDITION_TYPE", condition_type);
                webform.WebOffice.WebSendMessage();
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("G_CONDITION_GAS_SMALL")) {
                var CellCount = "G_CONDITION_GAS_SMALL" + "CellsCount";
                var ColumnCount = "G_CONDITION_GAS_SMALL" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("G_CONDITION_GAS_SMALL").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "G_CONDITION_GAS_SMALL" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }

            //【监测条件-废气-常规-中型】
            if (webform.WebOffice.WebObject.BookMarks.Exists("G_CONDITION_GAS_MEDIUM")) {
                condition_type = "G_CONDITION_GAS_MEDIUM";
                webform.WebOffice.WebSetMsgByName("COMMAND", "2");
                webform.WebOffice.WebSetMsgByName("CONDITION_TYPE", condition_type);
                webform.WebOffice.WebSendMessage();
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("G_CONDITION_GAS_MEDIUM")) {
                var CellCount = "G_CONDITION_GAS_MEDIUM" + "CellsCount";
                var ColumnCount = "G_CONDITION_GAS_MEDIUM" + "ColumnsCount";
                var TableCount = "G_CONDITION_GAS_MEDIUM" + "TableCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                iTables = webform.WebOffice.WebGetMsgByName(TableCount);

                if (iCells == "" || iColumns == "" || iTables == "") {
                    return;
                }

                for (var x = 0; x < iTables; x++) {
                    var mTable;
                    if (x == 0) {
                        mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("G_CONDITION_GAS_MEDIUM").Range, iCells, iColumns, "1", "0");
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
                            iName = "G_CONDITION_GAS_MEDIUM" + x.toString() + "-" + (i + 1).toString() + "-" + (j + 1).toString();
                            iText = webform.WebOffice.WebGetMsgByName(iName);

                            mTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                        }
                    }
                    //设置文本的居中方向
                    mTable.Select();
                    webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                    webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                    webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                    webform.WebOffice.WebObject.Application.Selection.EndKey();
                }
            }

            //【监测条件-废气-常规-大型】
            if (webform.WebOffice.WebObject.BookMarks.Exists("G_CONDITION_GAS_HUGE")) {
                condition_type = "G_CONDITION_GAS_HUGE";
                webform.WebOffice.WebSetMsgByName("COMMAND", "2");
                webform.WebOffice.WebSetMsgByName("CONDITION_TYPE", condition_type);
                webform.WebOffice.WebSendMessage();
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("G_CONDITION_GAS_HUGE")) {
                var CellCount = "G_CONDITION_GAS_HUGE" + "CellsCount";
                var ColumnCount = "G_CONDITION_GAS_HUGE" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("G_CONDITION_GAS_HUGE").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "G_CONDITION_GAS_HUGE" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
        }
        //加载监测结果
        //韶刚监测结果
        function LoadTestResult_Shaogang() {
            StatusMsg("正在加载监测结果信息...");
            var contract_type = "SHAOGANG_RESULT";

            webform.WebOffice.WebSetMsgByName("COMMAND", "3-3");
            webform.WebOffice.WebSetMsgByName("CONTRACT_TYPE", contract_type);
            webform.WebOffice.WebSendMessage();

            //加载数据
            var iTable, iRows, iCells, iName, iText;

            var TableCount = "SHAOGANG_RESULT" + "TableCount";
            iTables = webform.WebOffice.WebGetMsgByName(TableCount);

            for (var x = 0; x < iTables; x++) {
                var ColumnCount = "SHAOGANG_RESULT" + x.toString() + "ColumnsCount";
                var CellCount = "SHAOGANG_RESULT" + x.toString() + "CellsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);

                var mTable;
                if (x == 0) {
                    mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("SHAOGANG_RESULT").Range, iCells, iColumns, "1", "0");
                }
                else {
                    webform.WebOffice.WebObject.Application.Selection.MoveDown(5, 1);
                    webform.WebOffice.WebObject.Application.Selection.TypeParagraph();
                    webform.WebOffice.WebObject.Application.Selection.MoveUp(5, 1);

                    webform.WebOffice.WebObject.Application.Selection.InsertBreak(7);
                    mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.Application.Selection.Range, iCells, iColumns, "1", "0");
                }

                mTable.Style = "网格型";
                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "SHAOGANG_RESULT" + x.toString() + "-" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        mTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                mTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
        }
        function LoadTestResult_Sys1() {
            StatusMsg("正在加载监测结果信息...");
            var contract_type = "SYSTEM1_RESULT";

            webform.WebOffice.WebSetMsgByName("COMMAND", "3-1");
            webform.WebOffice.WebSetMsgByName("CONTRACT_TYPE", contract_type);
            webform.WebOffice.WebSendMessage();

            //加载数据
            var iTable, iRows, iCells, iName, iText;

            //【一系统监测结果】
            var TableCount = "SYSTEM1_RESULT" + "TableCount";
            iTables = webform.WebOffice.WebGetMsgByName(TableCount);

            for (var x = 0; x < iTables; x++) {
                var ColumnCount = "SYSTEM1_RESULT" + x.toString() + "ColumnsCount";
                var CellCount = "SYSTEM1_RESULT" + x.toString() + "CellsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);

                var mTable;
                if (x == 0) {
                    mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("SYSTEM1_RESULT").Range, iCells, iColumns, "1", "0");
                }
                else {
                    webform.WebOffice.WebObject.Application.Selection.MoveDown(5, 1);
                    webform.WebOffice.WebObject.Application.Selection.TypeParagraph();
                    webform.WebOffice.WebObject.Application.Selection.MoveUp(5, 1);
                    webform.WebOffice.WebObject.Application.Selection.InsertBreak(7);
                    mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.Application.Selection.Range, iCells, iColumns, "1", "0");
                }

                mTable.Style = "网格型";
                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "SYSTEM1_RESULT" + x.toString() + "-" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        mTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                mTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
        }
        function LoadTestResult_Sys2() {
            StatusMsg("正在加载监测结果信息...");
            var contract_type = "SYSTEM2_RESULT";

            webform.WebOffice.WebSetMsgByName("COMMAND", "3-2");
            webform.WebOffice.WebSetMsgByName("CONTRACT_TYPE", contract_type);
            webform.WebOffice.WebSendMessage();

            //加载数据
            var iTable, iRows, iCells, iName, iText;

            //【二系统监测结果】
            var TableCount = "SYSTEM2_RESULT" + "TableCount"; //
            iTables = webform.WebOffice.WebGetMsgByName(TableCount); //
            if (iCells == "" || iColumns == "" || iTables == "") {
                return;
            }

            for (var x = 0; x < iTables; x++) {
                var ColumnCount = "SYSTEM2_RESULT" + x.toString() + "ColumnsCount";
                var CellCount = "SYSTEM2_RESULT" + x.toString() + "CellsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);

                var mTable;
                if (x == 0) {
                    mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("SYSTEM2_RESULT").Range, iCells, iColumns, "1", "0");
                }
                else {
                    webform.WebOffice.WebObject.Application.Selection.MoveDown(5, 1);
                    webform.WebOffice.WebObject.Application.Selection.TypeParagraph();
                    webform.WebOffice.WebObject.Application.Selection.MoveUp(5, 1);
                    webform.WebOffice.WebObject.Application.Selection.InsertBreak(7);
                    mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.Application.Selection.Range, iCells, iColumns, "1", "0");
                }

                mTable.Style = "网格型";
                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "SYSTEM2_RESULT" + x.toString() + "-" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        mTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                mTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;

                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
        }
        function LoadTestResult() {

            StatusMsg("正在加载监测结果信息...");
            var contract_type = "";
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_EMISSIVE")) {
                //【监测结果-放射性】
                contract_type = "RESULT_EMISSIVE";
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_GAS_MEDIUM")) {
                //【监测结果-废气-中型】
                contract_type = "RESULT_GAS_MEDIUM";
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_GAS_SMALL")) {
                //【监测结果-废气-小型】
                contract_type = "RESULT_GAS_SMALL";
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_NOISE")) {
                //【监测结果-噪声】
                contract_type = "RESULT_NOISE";
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_QUAKE")) {
                //【监测结果-振动】
                contract_type = "RESULT_QUAKE";
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_ROOM")) {
                //【监测结果-室内空气质量】
                contract_type = "RESULT_ROOM";
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_SOLID")) {
                //【监测结果-固体】
                contract_type = "RESULT_SOLID";
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_WATER_THIN")) {
                //【监测结果-废水-不换行】
                contract_type = "RESULT_WATER_THIN";
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_WATER_WIDTH")) {
                //【监测结果-废水-换行】
                contract_type = "RESULT_WATER_WIDTH";
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_WATER_VERTICAL")) {
                //【监测结果-废水-竖表】
                contract_type = "RESULT_WATER_VERTICAL";
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_NOISE_EQUIVALENT")) {
                //【监测结果-室内等效噪声】
                contract_type = "RESULT_NOISE_EQUIVALENT";
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_NOISE_FREQUENCY_DB_LEQ")) {
                //【监测结果-倍频程噪声Leq】
                contract_type = "RESULT_NOISE_FREQUENCY_DB_LEQ";
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_THREE_WATER_VERTICAL")) {
                //【监测结果-三同时-废水-竖表】
                contract_type = "RESULT_THREE_WATER_VERTICAL";
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_THREE_WATER_HORIZONTAL")) {
                //【监测结果-三同时-废水-横表】
                contract_type = "RESULT_THREE_WATER_HORIZONTAL";
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_GAS_HUGE")) {
                //【监测结果-废气-大型】
                contract_type = "RESULT_GAS_HUGE";
            }

            webform.WebOffice.WebSetMsgByName("COMMAND", "3");
            webform.WebOffice.WebSetMsgByName("CONTRACT_TYPE", contract_type);
            webform.WebOffice.WebSendMessage();

            //加载数据
            var iTable, iRows, iCells, iName, iText;
            //【监测结果-放射性】
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_EMISSIVE")) {
                var CellCount = "RESULT_EMISSIVE" + "CellsCount";
                var ColumnCount = "RESULT_EMISSIVE" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("RESULT_EMISSIVE").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "RESULT_EMISSIVE" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
            //【监测结果-废气-中型】
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_GAS_MEDIUM")) {
                var CellCount = "RESULT_GAS_MEDIUM" + "CellsCount";
                var ColumnCount = "RESULT_GAS_MEDIUM" + "ColumnsCount";
                var TableCount = "RESULT_GAS_MEDIUM" + "TableCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                iTables = webform.WebOffice.WebGetMsgByName(TableCount);

                if (iCells == "" || iColumns == "" || iTables == "") {
                    return;
                }

                for (var x = 0; x < iTables; x++) {
                    var mTable;
                    if (x == 0) {
                        mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("RESULT_GAS_MEDIUM").Range, iCells, iColumns, "1", "0");
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
                            iName = "RESULT_GAS_MEDIUM" + x.toString() + "-" + (i + 1).toString() + "-" + (j + 1).toString();
                            iText = webform.WebOffice.WebGetMsgByName(iName);

                            mTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;

                        }
                    }
                    //设置文本的居中方向
                    mTable.Select();
                    webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                    webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                    webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                    webform.WebOffice.WebObject.Application.Selection.EndKey();
                }

            }
            //【监测结果-废气-小型】
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_GAS_SMALL")) {
                var CellCount = "RESULT_GAS_SMALL" + "CellsCount";
                var ColumnCount = "RESULT_GAS_SMALL" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("RESULT_GAS_SMALL").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "RESULT_GAS_SMALL" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.EndKey();
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
                if (iColumns == 2) {
                    iCells = iCells + 2;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("RESULT_NOISE").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    var iCellsEx = iCells - 2;
                    if (iColumns == 2) {
                        iCellsEx = iCells;
                    }
                    for (var j = 0; j < iCellsEx - 2; j++) {
                        iName = "RESULT_NOISE" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }

                if (iColumns == 2) {
                    iName = "SOUND_ST";
                    iText = webform.WebOffice.WebGetMsgByName(iName);
                    iTable.Columns(1).Cells(iCells - 2).Range.Text = "执行标准";
                    iTable.Columns(2).Cells(iCells - 2).Range.Text = iText; //GB 22337－2008表2中1类区B类房间夜间标准
                    iName = "SOUND_evaluation";
                    iText = webform.WebOffice.WebGetMsgByName(iName);
                    iTable.Columns(1).Cells(iCells - 1).Range.Text = "评价结果";
                    iTable.Columns(2).Cells(iCells - 1).Range.Text = iText;
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
            //【监测结果-振动】
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_QUAKE")) {
                var CellCount = "RESULT_QUAKE" + "CellsCount";
                var ColumnCount = "RESULT_QUAKE" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("RESULT_QUAKE").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "RESULT_QUAKE" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
            //【监测结果-室内空气质量】
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_ROOM")) {
                var CellCount = "RESULT_ROOM" + "CellsCount";
                var ColumnCount = "RESULT_ROOM" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("RESULT_ROOM").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "RESULT_ROOM" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
            //【监测结果-固体】
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_SOLID")) {
                var CellCount = "RESULT_SOLID" + "CellsCount";
                var ColumnCount = "RESULT_SOLID" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("RESULT_SOLID").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "RESULT_SOLID" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
            //【监测结果-废水-不换行】
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_WATER_THIN")) {
                var CellCount = "RESULT_WATER_THIN" + "CellsCount";
                var ColumnCount = "RESULT_WATER_THIN" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("RESULT_WATER_THIN").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "RESULT_WATER_THIN" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
            //【监测结果-废水-换行】
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_WATER_WIDTH")) {
                var CellCount = "RESULT_WATER_WIDTH" + "CellsCount";
                var ColumnCount = "RESULT_WATER_WIDTH" + "ColumnsCount";
                var TableCount = "RESULT_WATER_WIDTH" + "TableCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                iTables = webform.WebOffice.WebGetMsgByName(TableCount);
                if (iCells == "" || iColumns == "" || iTables == "") {
                    return;
                }
                for (var x = 0; x < iTables; x++) {
                    var mTable;
                    if (x == 0) {
                        mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("RESULT_WATER_WIDTH").Range, iCells, iColumns, "1", "0");
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
                            iName = "RESULT_WATER_WIDTH" + x.toString() + "-" + (i + 1).toString() + "-" + (j + 1).toString();
                            iText = webform.WebOffice.WebGetMsgByName(iName);

                            mTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                        }
                    }
                    //设置文本的居中方向
                    mTable.Select();
                    webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                    webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                    webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                    webform.WebOffice.WebObject.Application.Selection.EndKey();
                }
            }
            //【监测结果-废水-竖表】
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_WATER_VERTICAL")) {
                var CellCount = "RESULT_WATER_VERTICAL" + "CellsCount";
                var ColumnCount = "RESULT_WATER_VERTICAL" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("RESULT_WATER_VERTICAL").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "RESULT_WATER_VERTICAL" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
            //【监测结果-室内等效噪声】
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_NOISE_EQUIVALENT")) {
                var CellCount = "RESULT_NOISE_EQUIVALENT" + "CellsCount";
                var ColumnCount = "RESULT_NOISE_EQUIVALENT" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);

                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("RESULT_NOISE_EQUIVALENT").Range, iCells, iColumns, "1", "1");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells - 2; j++) {
                        iName = "RESULT_NOISE_EQUIVALENT" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                contract_type = "RESULT_NOISE_EQUIVALENT_STANDARD";
                webform.WebOffice.WebSetMsgByName("COMMAND", "3");
                webform.WebOffice.WebSetMsgByName("CONTRACT_TYPE", contract_type);
                webform.WebOffice.WebSendMessage();
                var iCells1 = webform.WebOffice.WebGetMsgByName(CellCount);
                var iColumns1 = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells1 == "" || iColumns1 == "" || iCells1 == 0 || iColumns1 == 0) {
                    return;
                }
                iName = "RESULT_NOISE_EQUIVALENT_STANDARD2-2";
                iText = webform.WebOffice.WebGetMsgByName(iName);

                iTable.Columns(1).Cells(iCells - 1).Range.Text = "执行标准";
                iTable.Columns(2).Cells(iCells - 1).Range.Text = "GB 22337－2008表2中1类区B类房间夜间标准：" + iText;
                iTable.Columns(1).Cells(iCells).Range.Text = "评价结果";
                iTable.Columns(2).Cells(iCells).Range.Text = iText;

                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
            //【监测结果-倍频程噪声Leq】
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_NOISE_FREQUENCY_DB_LEQ")) {
                var CellCount = "RESULT_NOISE_FREQUENCY_DB_LEQ" + "CellsCount";
                var ColumnCount = "RESULT_NOISE_FREQUENCY_DB_LEQ" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);

                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("RESULT_NOISE_FREQUENCY_DB_LEQ").Range, iCells, iColumns, "1", "1");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells - 2; j++) {
                        iName = "RESULT_NOISE_FREQUENCY_DB_LEQ" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }

                contract_type = "RESULT_NOISE_FREQUENCY_DB_LEQ_STANDARD";
                webform.WebOffice.WebSetMsgByName("COMMAND", "3");
                webform.WebOffice.WebSetMsgByName("CONTRACT_TYPE", contract_type);
                webform.WebOffice.WebSendMessage();
                var iCells1 = webform.WebOffice.WebGetMsgByName(CellCount);
                var iColumns1 = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells1 == "" || iColumns1 == "" || iCells1 == 0 || iColumns1 == 0) {
                    return;
                }
                iName = "RESULT_NOISE_FREQUENCY_DB_LEQ_STANDARD2-2";
                iText = webform.WebOffice.WebGetMsgByName(iName);

                iTable.Columns(1).Cells(iCells - 1).Range.Text = "执行标准";
                iTable.Columns(2).Cells(iCells - 1).Range.Text = "GB 22337－2008表2中2类区A类房间夜间标准35.0";
                iTable.Columns(1).Cells(iCells).Range.Text = "评价结果";
                iTable.Columns(2).Cells(iCells).Range.Text = "";

                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
            //【监测结果-三同时-废水-竖表】
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_THREE_WATER_VERTICAL")) {
                var CellCount = "RESULT_THREE_WATER_VERTICAL" + "CellsCount";
                var ColumnCount = "RESULT_THREE_WATER_VERTICAL" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("RESULT_THREE_WATER_VERTICAL").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "RESULT_THREE_WATER_VERTICAL" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
            //【监测结果-三同时-废水-横表】
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_THREE_WATER_HORIZONTAL")) {
                var CellCount = "RESULT_THREE_WATER_HORIZONTAL" + "CellsCount";
                var ColumnCount = "RESULT_THREE_WATER_HORIZONTAL" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("RESULT_THREE_WATER_HORIZONTAL").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "RESULT_THREE_WATER_HORIZONTAL" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }
            //【监测结果-废气-大型】
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_GAS_HUGE")) {
                var CellCount = "RESULT_GAS_HUGE" + "CellsCount";
                var ColumnCount = "RESULT_GAS_HUGE" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("RESULT_GAS_HUGE").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "RESULT_GAS_HUGE" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }

            //【监测结果-倍频程噪声倍频带声压级】
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_NOISE_FREQUENCY_DB_FREQ")) {
                contract_type = "RESULT_NOISE_FREQUENCY_DB_FREQ";
                webform.WebOffice.WebSetMsgByName("COMMAND", "3");
                webform.WebOffice.WebSetMsgByName("CONTRACT_TYPE", contract_type);
                webform.WebOffice.WebSendMessage();
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_NOISE_FREQUENCY_DB_FREQ")) {
                var CellCount = "RESULT_NOISE_FREQUENCY_DB_FREQ" + "CellsCount";
                var ColumnCount = "RESULT_NOISE_FREQUENCY_DB_FREQ" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);

                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("RESULT_NOISE_FREQUENCY_DB_FREQ").Range, iCells, iColumns, "1", "1");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells - 2; j++) {
                        iName = "RESULT_NOISE_FREQUENCY_DB_FREQ" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }

                contract_type = "RESULT_NOISE_FREQUENCY_DB_FREQ_STANDARD";
                webform.WebOffice.WebSetMsgByName("COMMAND", "3");
                webform.WebOffice.WebSetMsgByName("CONTRACT_TYPE", contract_type);
                webform.WebOffice.WebSendMessage();
                var iCells1 = webform.WebOffice.WebGetMsgByName(CellCount);
                var iColumns1 = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells1 == "" || iColumns1 == "" || iCells1 == 0 || iColumns1 == 0) {
                    return;
                }
                for (var i = 0; i < iColumns; i++) {
                    iName = "RESULT_NOISE_FREQUENCY_DB_FREQ_STANDARD" + (i + 1).toString() + "-" + (2).toString();
                    iText = webform.WebOffice.WebGetMsgByName(iName);

                    iTable.Columns(i + 1).Cells(iCells - 2).Range.Text = iText;
                }

                iTable.Columns(1).Cells(iCells - 2).Range.Text = "执行标准";
                iTable.Columns(1).Cells(iCells - 1).Range.Text = "";
                iTable.Columns(2).Cells(iCells - 1).Range.Text = "GB 22337－2008表3中2类区A类房间夜间标准";
                iTable.Columns(1).Cells(iCells).Range.Text = "评价结果";
                iTable.Columns(2).Cells(iCells).Range.Text = "";

                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }

            //【监测结果-三同时-废气】
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_THREE_GAS")) {
                contract_type = "RESULT_THREE_GAS";
                webform.WebOffice.WebSetMsgByName("COMMAND", "3");
                webform.WebOffice.WebSetMsgByName("CONTRACT_TYPE", contract_type);
                webform.WebOffice.WebSendMessage();
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_THREE_GAS")) {
                var CellCount = "RESULT_THREE_GAS" + "CellsCount";
                var ColumnCount = "RESULT_THREE_GAS" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("RESULT_THREE_GAS").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "RESULT_THREE_GAS" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }

            //【监测结果-三同时-噪声】
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_THREE_NOISE")) {
                contract_type = "RESULT_THREE_NOISE";
                webform.WebOffice.WebSetMsgByName("COMMAND", "3");
                webform.WebOffice.WebSetMsgByName("CONTRACT_TYPE", contract_type);
                webform.WebOffice.WebSendMessage();
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("RESULT_THREE_NOISE")) {
                var CellCount = "RESULT_THREE_NOISE" + "CellsCount";
                var ColumnCount = "RESULT_THREE_NOISE" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);

                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("RESULT_THREE_NOISE").Range, iCells, iColumns, "1", "1");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "RESULT_THREE_NOISE" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }

                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }

            //【监测结果-废水-常规-换行】
            if (webform.WebOffice.WebObject.BookMarks.Exists("G_RESULT_WATER_WIDTH")) {
                contract_type = "G_RESULT_WATER_WIDTH";
                webform.WebOffice.WebSetMsgByName("COMMAND", "3");
                webform.WebOffice.WebSetMsgByName("CONTRACT_TYPE", contract_type);
                webform.WebOffice.WebSendMessage();
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("G_RESULT_WATER_WIDTH")) {
                var CellCount = "G_RESULT_WATER_WIDTH" + "CellsCount";
                var ColumnCount = "G_RESULT_WATER_WIDTH" + "ColumnsCount";
                var TableCount = "G_RESULT_WATER_WIDTH" + "TableCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                iTables = webform.WebOffice.WebGetMsgByName(TableCount);
                if (iCells == "" || iColumns == "" || iTables == "") {
                    return;
                }
                for (var x = 0; x < iTables; x++) {
                    var mTable;
                    if (x == 0) {
                        mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("G_RESULT_WATER_WIDTH").Range, iCells, iColumns, "1", "0");
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
                            iName = "G_RESULT_WATER_WIDTH" + x.toString() + "-" + (i + 1).toString() + "-" + (j + 1).toString();
                            iText = webform.WebOffice.WebGetMsgByName(iName);

                            mTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                        }
                    }
                    //设置文本的居中方向
                    mTable.Select();
                    webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                    webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                    webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                    webform.WebOffice.WebObject.Application.Selection.EndKey();
                }
            }

            //【监测结果-废水-常规-不换行】
            if (webform.WebOffice.WebObject.BookMarks.Exists("G_RESULT_WATER_THIN")) {
                contract_type = "G_RESULT_WATER_THIN";
                webform.WebOffice.WebSetMsgByName("COMMAND", "3");
                webform.WebOffice.WebSetMsgByName("CONTRACT_TYPE", contract_type);
                webform.WebOffice.WebSendMessage();
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("G_RESULT_WATER_THIN")) {
                var CellCount = "G_RESULT_WATER_THIN" + "CellsCount";
                var ColumnCount = "G_RESULT_WATER_THIN" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("G_RESULT_WATER_THIN").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "G_RESULT_WATER_THIN" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
            }

            //【监测结果-废水-常规-竖表】
            if (webform.WebOffice.WebObject.BookMarks.Exists("G_RESULT_WATER_VERTICAL")) {
                contract_type = "G_RESULT_WATER_VERTICAL";
                webform.WebOffice.WebSetMsgByName("COMMAND", "3");
                webform.WebOffice.WebSetMsgByName("CONTRACT_TYPE", contract_type);
                webform.WebOffice.WebSendMessage();
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("G_RESULT_WATER_VERTICAL")) {
                var CellCount = "G_RESULT_WATER_VERTICAL" + "CellsCount";
                var ColumnCount = "G_RESULT_WATER_VERTICAL" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("G_RESULT_WATER_VERTICAL").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "G_RESULT_WATER_VERTICAL" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }

            //【监测结果-废气-常规-小型】
            if (webform.WebOffice.WebObject.BookMarks.Exists("G_RESULT_GAS_SMALL")) {
                contract_type = "G_RESULT_GAS_SMALL";
                webform.WebOffice.WebSetMsgByName("COMMAND", "3");
                webform.WebOffice.WebSetMsgByName("CONTRACT_TYPE", contract_type);
                webform.WebOffice.WebSendMessage();
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("G_RESULT_GAS_SMALL")) {
                var CellCount = "G_RESULT_GAS_SMALL" + "CellsCount";
                var ColumnCount = "G_RESULT_GAS_SMALL" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("G_RESULT_GAS_SMALL").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "G_RESULT_GAS_SMALL" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
                }
                //设置文本的居中方向
                iTable.Select();
                webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                webform.WebOffice.WebObject.Application.Selection.EndKey();
            }

            //【监测结果-废气-常规-中型】
            if (webform.WebOffice.WebObject.BookMarks.Exists("G_RESULT_GAS_MEDIUM")) {
                contract_type = "G_RESULT_GAS_MEDIUM";
                webform.WebOffice.WebSetMsgByName("COMMAND", "3");
                webform.WebOffice.WebSetMsgByName("CONTRACT_TYPE", contract_type);
                webform.WebOffice.WebSendMessage();
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("G_RESULT_GAS_MEDIUM")) {
                var CellCount = "G_RESULT_GAS_MEDIUM" + "CellsCount";
                var ColumnCount = "G_RESULT_GAS_MEDIUM" + "ColumnsCount";
                var TableCount = "G_RESULT_GAS_MEDIUM" + "TableCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                iTables = webform.WebOffice.WebGetMsgByName(TableCount);

                if (iCells == "" || iColumns == "" || iTables == "") {
                    return;
                }

                for (var x = 0; x < iTables; x++) {
                    var mTable;
                    if (x == 0) {
                        mTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("G_RESULT_GAS_MEDIUM").Range, iCells, iColumns, "1", "0");
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
                            iName = "G_RESULT_GAS_MEDIUM" + x.toString() + "-" + (i + 1).toString() + "-" + (j + 1).toString();
                            iText = webform.WebOffice.WebGetMsgByName(iName);

                            mTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;

                        }
                    }
                    //设置文本的居中方向
                    mTable.Select();
                    webform.WebOffice.WebObject.Application.Selection.ParagraphFormat.Alignment = "1";
                    webform.WebOffice.WebObject.Application.Selection.Cells.VerticalAlignment = "1";
                    webform.WebOffice.WebObject.Application.Selection.Cells.Height = 30;
                    webform.WebOffice.WebObject.Application.Selection.EndKey();
                }
            }

            //【监测结果-废气-常规-大型】
            if (webform.WebOffice.WebObject.BookMarks.Exists("G_RESULT_GAS_HUGE")) {
                contract_type = "G_RESULT_GAS_HUGE";
                webform.WebOffice.WebSetMsgByName("COMMAND", "3");
                webform.WebOffice.WebSetMsgByName("CONTRACT_TYPE", contract_type);
                webform.WebOffice.WebSendMessage();
            }
            if (webform.WebOffice.WebObject.BookMarks.Exists("G_RESULT_GAS_HUGE")) {
                var CellCount = "G_RESULT_GAS_HUGE" + "CellsCount";
                var ColumnCount = "G_RESULT_GAS_HUGE" + "ColumnsCount";
                iCells = webform.WebOffice.WebGetMsgByName(CellCount);
                iColumns = webform.WebOffice.WebGetMsgByName(ColumnCount);
                if (iCells == "" || iColumns == "") {
                    return;
                }
                iTable = webform.WebOffice.WebObject.Tables.Add(webform.WebOffice.WebObject.BookMarks("G_RESULT_GAS_HUGE").Range, iCells, iColumns, "1", "0");
                iTable.Style = "网格型";

                for (var i = 0; i < iColumns; i++) {
                    for (var j = 0; j < iCells; j++) {
                        iName = "G_RESULT_GAS_HUGE" + (i + 1).toString() + "-" + (j + 1).toString();
                        iText = webform.WebOffice.WebGetMsgByName(iName);

                        iTable.Columns(i + 1).Cells(j + 1).Range.Text = iText;
                    }
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
            //  StatusMsg("正在加载测点分布示意图...");
            //  
            //  var contract_type = "";
            ////  if(webform.WebOffice.WebObject.BookMarks.Exists("SKETCH_MAP")&&document.getElementById("CONTRACT").value!="")
            ////  {
            ////    //【测点分布示意图】
            ////    contract_type =  "SKETCH_MAP";
            ////  }
            //  
            ////  webform.WebOffice.WebSetMsgByName("COMMAND","4"); 
            //  webform.WebOffice.WebSetMsgByName("CONTRACT_TYPE",contract_type); 
            //  webform.WebOffice.WebSetMsgByName("IMAGENAME","POINT_IMG_NAME"); 
            //  webform.WebOffice.WebSetMsgByName("LABELNAME","SKETCH_MAP");
            //  webform.WebOffice.WebSendMessage();
            //  
            //  var strImageUrl     = webform.WebOffice.WebGetMsgByName("InsertImageUrl");
            ////  strImageUrl     = "20090309130438噪声点位.jpg";
            //  if(webform.WebOffice.WebObject.BookMarks.Exists("SKETCH_MAP")&&document.getElementById("CONTRACT").value!="")
            //  {   
            ////      WebInsertImage('SKETCH_MAP',strImageUrl,true,5);
            //      webform.WebOffice.WebInsertImage('SKETCH_MAP',"测点分布示意图",true,5);
            //  }
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
            if (!webform.WebOffice.WebSave()) {
                //交互OfficeServer的OPTION="SAVEFILE"
                StatusMsg(webform.WebOffice.Status);
                alert(webform.WebOffice.Status);
                return false;
            }
            else {
                StatusMsg(webform.WebOffice.Status);
                if (confirm("保存成功，关闭本页面?")) {
                    self.close();
                }
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

            //带发送环保局报告的常规报告
            var isG = "0";
            if (webform.WebOffice.WebObject.BookMarks.Exists("G_PROJECT_NAME")) {
                isG = "1";
            }
            webform.WebOffice.WebSetMsgByName("isG", isG); //合同ID

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
                if (strRWF == "ReportApprove") {
                    contract_type = "REPORT_APPROVE_USERID";
                }

                if (webform.WebOffice.WebObject.BookMarks.Exists(contract_type)) {
                    webform.WebOffice.WebInsertImage(contract_type, "admin", true, 4);

                }
            }
            catch (e) {
                //    WebProtect(true);
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
    <div>
        <!--调用iWebOffice，注意版本号，可用于升级-->
        <script src="iWebOffice2003.js" type="text/jscript"></script>
    </div>
    <div style="display: none;" id="StatusBar">
        状态栏</div>
    </form>
</body>
</html>
