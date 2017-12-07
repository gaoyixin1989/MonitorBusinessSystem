<%@ Page Language="C#" MasterPageFile="~/Masters/Input.master" AutoEventWireup="True" Inherits="Sys_General_PostTree" Codebehind="PostTree.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
    <link rel="stylesheet" href="../../Controls/zTree3.4/css/zTreeStyle/zTreeStyle.css" type="text/css" />
    <script src="../../Controls/zTree3.4/js/jquery.ztree.core-3.4.min.js" type="text/javascript"></script>
    <script src="../../Controls/zTree3.4/js/jquery.ztree.exedit-3.4.min.js" type="text/javascript"></script>
    <script src="PostTree.js" type="text/javascript"></script>
    <style type="text/css">
        .ztree li span.button.add {margin-left:2px; margin-right: -1px; background-position:-144px 0; vertical-align:top; *vertical-align:middle}
	</style>

    <script type="text/javascript">
		<!--
	    var setting = {
            view: {
				addHoverDom: addHoverDom,
				removeHoverDom: removeHoverDom,
				selectedMulti: false,
                showIcon:false
			},
			edit: {
				drag: {
					autoExpandTrigger: true,
					prev: dropPrev,
					inner: dropInner,
					next: dropNext
				},
				enable: true,
				editNameSelectAll: true,
				showRenameBtn: false
			},
			data: {
                keep: {
					parent:true,
					leaf:true
				},
				simpleData: {
					enable: true
				}
			},
			callback: {
				beforeDrag: beforeDrag,
				beforeDrop: beforeDrop,
				beforeDragOpen: beforeDragOpen,
				onDrag: onDrag,
				onDrop: onDrop,
				beforeEditName: beforeEditName,
				beforeRemove: beforeRemove,
				beforeRename: beforeRename,
				onRemove: onRemove,
				onExpand: onExpand,
				onRename: onRename
			}
		};

	    var zNodes = <%= postData.ToString() %>;
		
		$(document).ready(function(){
			$.fn.zTree.init($("#treeDemo"), setting, zNodes);
            $.fn.zTree.getZTreeObj("treeDemo").setting.edit.removeTitle = "删除";
            sendData_callback();

            $("#btnAdd").get(0).style.display = 'none';
            $("#btnEdit").get(0).style.display = 'none';
            $("#btnCancel").get(0).style.display = 'none';
		});
		//-->
	</script>
    
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphInput" runat="Server" >
    <div style="width: 200px; height: 550px;float: left; height:100%; margin:5px 10px 5px 0; overflow-x: hidden; overflow-y: auto;" id="div_Tree">
        <ul id="treeDemo" class="ztree">
        </ul>
    </div>
    <div style="float: left;" id="div_Table">
        <table class="tablelist2012" cellspacing="0" cellpadding="0" style=" margin:30px 0 15px 0; width:400px;">
            <tr>
                <th >
                    职位名：
                </th>
                <td >
                    <input id="POST_NAME" type="text" style="width: 300px;" class="input" validate="[{required:true, msg:'请输入名称'},{minlength:1,maxlength:16,msg:'录入最小长度为1，最大长度为16'}]" />
                    <input type="hidden" id="hidAddEditID" name="hidAddParentID"  />
                </td>
            </tr>
            <tr>
                <th>
                    上级职位：
                </th>
                <td >
                    <input id="PARENT_POST" type="text" style="width: 300px;" class="input" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <th>
                    所属部门：
                </th>
                <td >
                    <select name="POST_DEPT" id="selPOST_DEPT" style="width: 300px;"></select>
                </td>
            </tr>
            <tr>
                <th>
                    行政级别：
                </th>
                <td >
                    <select name="POST_LEVEL" id="selPOST_LEVEL" style="width: 300px;"></select>
                </td>
            </tr>
            <tr>
                <th>
                    职位说明：
                </th>
                <td class="fm_ipt pct28">
                    <textarea id="ROLE_NOTE" rows="5" cols="48" validate="[{maxlength:64,msg:'录入最大长度为64'}]"> </textarea>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="ID" runat="server" Value="" />
        <asp:HiddenField ID="IsRoot" runat="server" Value="" />
            <div >
    <input id="btnAdd" type="button" value="添加" onclick="return btnAdd_onclick()"  class="btn02" />
    <input id="btnEdit" type="button" value="修改" onclick="return btnEdit_onclick()"  class="btn02" />
    <input id="btnCancel" type="button" value="取消" onclick="return btnCancel_onclick()" class="btn02" />
    </div>
    </div>

</asp:Content>
