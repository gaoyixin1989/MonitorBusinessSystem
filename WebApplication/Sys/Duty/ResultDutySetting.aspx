<%@ Page Language="C#" MasterPageFile="~/Masters/SearchEx.master" AutoEventWireup="True" Inherits="Sys_Duty_ResultDutySetting" Codebehind="ResultDutySetting.aspx.cs" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">

    <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
     <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
     
    <script src="../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>

    <script src="ResultDutySetting.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cphData" runat="Server">
    <div class="l-loading" style="display:block" id="pageloading"></div>

    <div id="layoutResultDutySetting" style="text-align:left">
        <div position="center"  title="">
            <div id="maingrid3"></div>
        </div>
    </div>

        <!--监测项目设置-->
    <div id="targerdiv" class="l-form" style="display:none;">
        <div  class="l-group l-group-hasicon">
        <img src="../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" /><span>岗位职责设置-分析</span>
        </div>
        <ul>
        <li  style="width:37px; text-align:left;">类 别检 索&nbsp;</li>
        <li  style="width:240px;text-align:left; margin-bottom:10px;">
            <input type="text" id="Monitor" name="Monitor" class="l-text l-text-editing" />
            <input type="text" value="" id="txtSeach" class="l-text l-text-editing" onKeyUp="javascript:txtSeachOption();" style="width:200px; margin-top:4px;" />
        </li>
            <li>
                <table class="tabletool" style="margin-left:37px;">
                    <tr>
                        <td  align="center" valign="top" rowspan="4">
                             <b>未选择项目</b>
                        <select size="20" name="listLeft" multiple="multiple" id="listLeft"  title="双击可实现右移" class="searcha"></select> 
                        </td>
                        <td align="center" rowspan="4">&nbsp;
                            </td>
                        <td align="center" rowspan="4">
                          <input type="button" id="btnRight" value=">>"  class="l-button l-button-submit" /><br />
                            <br />
                            <br />
                        <input type="button" id="btnLeft" value="<<" class="l-button l-button-submit" />
                        </td>
                        <td align="center" rowspan="4">&nbsp;
                            </td>
                        <td style="width:13%"  align="center" valign="top"  rowspan="4">
                             <b>
                            
                             已选择项目</b>
                        <select size="20" name="listRight" multiple="multiple" id="listRight"  title="双击可实现左移" 
                                class="searcha"> </select>
                                </td>
                           <td  valign="top" style="width:8%" rowspan="4">&nbsp;
                               </td>
                         
                           <td  valign="top" style="width:8%" rowspan="4">
                               <br />
                               <br />
                               <br />
                               
                          <input type="button" id="btnAuRight" name="btnAuRight" value=">>"  class="l-button l-button-submit" /><br />
                        <input type="button" id="btnAuLeft"  name="btnAuLeft" value="<<" class="l-button l-button-submit" /><br />
                               <br />
                               <br />
                               <br />
                               <br />
                          <input type="button" id="btnExRight" name="btnExRight" value=">>"  class="l-button l-button-submit" /><br />
                               <input 
                                   type="button" id="btnExLeft" name="btnExLeft" value="<<" 
                                   class="l-button l-button-submit" /></td>
                         
                           <td  valign="top" style="width:8%" rowspan="4">&nbsp;
                               </td>
                         
                         <td class="w01"  valign="top" width="40%">
                             <b> 默认负责人项目</b>
                        </td>
                         <td class="w01"  valign="bottom" width="40%" rowspan="4">&nbsp;
                             </td> 
                    </tr>
                    <tr>
                         <td class="w01"  valign="top" width="40%">
                        <select size="8" name="listDefaultAu" id="listDefaultAu" multiple="multiple"  title="双击可实现移出" 
                                class="searchb"> </select></td>
                    </tr>
                    <tr>
                         <td  valign="top" width="40%">
                             <b style="text-align: center">
                             默认协同人项目</b>
                             <select size="8" name="listDefaultEx" id="listDefaultEx" multiple="multiple"  title="双击可实现移出" 
                                 class="searchb"> </select></td>
                    </tr>
                    <tr>
                         <td  valign="top" width="40%">
                             </td>
                    </tr>
                    </table>
            </li>
        </ul>
    </div>
    <div id="detailSrh" style="display:none;"><form id="searchForm" method="post"></form> </div>
</asp:Content>
