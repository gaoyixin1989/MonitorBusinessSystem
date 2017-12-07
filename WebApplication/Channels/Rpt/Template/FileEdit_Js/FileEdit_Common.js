//作用：显示操作状态
function StatusMsg(mString) {
    window.status = mString;
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