<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Input.master" AutoEventWireup="True" Inherits="Sys_General_MenuEdit" Codebehind="MenuEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../Controls/ligerui/lib/ligerUI/skins/Gray/css/all.css" rel="stylesheet" type="text/css" />
    <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/fullscreen.css" rel="stylesheet" type="text/css" />

        <!--加载zTree菜单树控件必须文件-->
    <link href="../../Controls/zTree3.4/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <script src="../../Controls/zTree3.4/js/jquery.ztree.core-3.4.min.js" type="text/javascript"></script>
    <script src="../../Controls/zTree3.4/js/jquery.ztree.exedit-3.4.min.js" type="text/javascript"></script>

    <script src="../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>

    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="MenuEdit.js" type="text/javascript"></script>
    <style type="text/css">
        .ztree li span.button.add {margin-left:2px; margin-right: -1px; background-position:-144px 0; vertical-align:top; *vertical-align:middle}
	</style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphInput" runat="Server">
    <%--<div id="layout1" style="text-align:left;overflow:hidden;" >
        <div position="left" title="" >
            
        </div>
        <div position="center"  title="" >
            
        </div>
    </div> 
--%>

    <div class="innerCon01" style=" position:relative; width:100%;">
    
        <div class="ztreeleft2013" id="div_Tree">
            <ul id="tree" class="ztree"></ul>
        </div>
        <div class="ztreeright2013" id="div_Table">
        
            <table  class="tablelist2012" cellspacing="0"cellpadding="0" style=" margin:20px; width:auto; border:none;">
                <tr>
                    <th class="wd20">
                        父菜单：
                    </th>
                    <td>
                    <input type="text" id="Parent_Text" name="Parent_Text" disabled="disabled"  style="width: 400px;" class="ipt01" />
                    </td>
                </tr>
                <tr>
                    <th>
                        名称：
                    </th>
                    <td>
                        <input id="MENU_TEXT" type="text" name="MENU_TEXT" style="width: 400px;" validate="[{required:true, msg:'请输入名称'},{minlength:2,maxlength:10,msg:'录入最小长度为2，最大长度为10'}]" />
                    </td>
                </tr>
                <tr>
                    <th id="thMENU_URL">
                        路径：
                    </th>
                    <td>
                        <input id="MENU_URL" name=="MENU_URL" type="text" style="width: 400px;" class="ipt01" validate="[{maxlength:128,msg:'录入最大长度为128'}]" />
                    </td>
                </tr>
                <tr id="trIMGURL">
                    <div style=" display:none;" ></div>
                    <th>
                        图标：
                    </th>
                    <td>
                        <input type="radio" name="radiodata" value="control_data.gif"  class="radio" /><img id="MENU_IMGURL_0" src="../../Images/Menu/control_data.gif" />
                        <input type="radio" name="radiohelp" value="control_help.gif" class="radio" /><img id="Img6" src="../../Images/Menu/control_help.gif" />
                        <input type="radio" name="radiomanage" value="control_manage.gif" class="radio" /><img id="Img7" src="../../Images/Menu/control_manage.gif" />
                        <input type="radio" name="radiooset" value="control_oset.gif" class="radio" /><img id="Img8" src="../../Images/Menu/control_oset.gif" />
                        <input type="radio" name="radioset" value="control_set.gif" class="radio"  /><img id="Img9" src="../../Images/Menu/control_set.gif" />
                        <input type="radio" name="radiosysset" value="control_sysset.gif" class="radio" /><img id="Img10" src="../../Images/Menu/control_sysset.gif" />
                    </td>
                </tr>
                <tr id="trRabIsShortcutMenu">
                    <th>
                        快捷菜单：
                    </th>
                    <td>
                        <input  type="radio" name="radiofalse" value="0" class="radio" />否
                        <input  type="radio" name="radiotrue" value="1"class="radio"  />是
                    </td>
                </tr>
            </table>
            <div style=" text-align:left; margin-left:135px;">
                <input type="button" id="btnAdd" value="增加" class="btn02" />
                <input type="button" id="btnEdit" value="修改" class="btn02" />
            </div>
            <input type="hidden" id="txtHidden" />
            <input type="hidden" id="txtStatus" />
        </div>     
            
    </div>

</asp:Content>
