<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Input.master" AutoEventWireup="True" Inherits="Sys_Resource_DictDataEdit" Codebehind="DictDataEdit.aspx.cs" %>

<asp:Content ID="Content4" ContentPlaceHolderID="cphInput" runat="Server">
    <!--加载zTree菜单树控件必须文件-->
    <link href="../../Controls/zTree3.4/css/zTreeStyle/zTreeStyle.css" rel="stylesheet"
        type="text/css" />
        <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/fullscreen.css" rel="stylesheet" type="text/css" />
    <script src="../../Controls/zTree3.4/js/jquery.ztree.core-3.4.min.js" type="text/javascript"></script>
    <script src="../../Controls/zTree3.4/js/jquery.ztree.exedit-3.4.min.js" type="text/javascript"></script>
    <script src="Dict.js" type="text/javascript"></script>
    <style type="text/css">
        .ztree li span.button.add {margin-left:2px; margin-right: -1px; background-position:-144px 0; vertical-align:top; *vertical-align:middle}
	</style>
     <div  class="ztreeleft2013">
     <ul id="tree" class="ztree">
      </ul>
      </div>

    <div class="ztreeright2013">

                <table class="tablelist2012" cellspacing="0" cellpadding="0" style=" margin:20px; border:none;">
                    <tr>
                        <th class="wd20">
                            类型：
                        </th>
                        <td >
                            <input type="text" id="txtDictType" disabled="disabled" style="width: 400px;" />
                        </td>
                    </tr>
                    <tr>
                        <th class="wd20">
                            编码：
                        </th>
                        <td >
                            <input type="text" id="txtDictCode" style="width: 400px" />
                        </td>
                    </tr>
                    <tr>
                        <th class="wd20">
                            名称：
                        </th>
                        <td >
                            <input type="text" id="txtDictName" style="width: 400px" />
                        </td>
                    </tr>
                    <tr>
                        <th class="wd20">
                            备注：
                        </th>
                        <td>
                            <textarea id="txtRemark" rows="3" cols="20" style="width: 400px"></textarea>
                        </td>
                    </tr>
                    <tr>
                    <th></th>
                        <td align="center" >
                            <input type="hidden" id="txtHidden" />
                            <input type="hidden" id="txtStatus" />
                            <input type="button" id="btnAdd" value="增加" class="btn02" />
                            <input type="button" id="btnEdit" value="修改" class="btn02" />
                            <input type="button" id="btnSort" value="排序" class="btn02" />
                        </td>
                    </tr>
                </table>

    </div>
</asp:Content>
