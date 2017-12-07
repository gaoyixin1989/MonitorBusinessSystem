<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="Channels_Mis_Contract_AcceptanceEx_AcceptanceCreate" Codebehind="AcceptanceCreate.aspx.cs" %>

<%@ Register TagPrefix="UC" TagName="WFControl" Src="~/Sys/WF/UCWFControls.ascx" %>
<%@ Register TagName="UserAdd" TagPrefix="UC" Src="../ContractCompanyAdd.ascx" %>
<%@ Register TagName="UserComfrim" TagPrefix="UC" Src="../ContractCompanyComfrim.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../../Controls/AutoComplete/css/jquery.autocomplete.css" rel="stylesheet"
        type="text/css" />
    <!--Jquery 基础文件-->
    <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <!--自动完成-->
    <script src="../../../../Controls/AutoComplete/jquery.autocomplete.js" type="text/javascript"></script>
    <!--提示-->
    <script src="../../../../Controls/ligerui/lib/jquery-validation/jquery.validate.min.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/jquery-validation/jquery.metadata.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <!--LigerUI-->
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
    <script type="text/javascript" src="AcceptanceCreate.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="CONTRACT_ID" runat="server" />
    <input type="hidden" id="Contract_Type" runat="server" />
    <input type="hidden"  id="hidBtnType" runat="server" />
    <input type="hidden"  id="hidCompanyId" runat="server" />
    <%--委托单位信息--%>
    <div>
        <UC:UserAdd runat="server" ID="ContratAdd" />
    </div>
    <%--受检单位信息--%>
    <div>
        <UC:UserComfrim runat="server" ID="ContratConfrim" />
    </div>
    <%--验收企业自查内容--%>
    <div class="l-form" > 
        <div id="divImgContent"  class="l-group l-group-hasicon tableh1">
        </div>
        <table class="mlr" cellspacing="0" cellpadding="0">
            <tr>
                <th style="width:120px;">项目名称：</th>
                <td align="left" colspan="3" >
                    <input type="text" id="PROJECT_NAME" name="PROJECT_NAME" class="l-text l-text-editing" style="width:480px"/>
                </td>
            </tr>
            <tr>
                <th >是否分段验收：</th>
                <td align="left" style="width:255px;" >
                    <input id="rbYS_0" type="radio" name="rbYS" value="1" checked="checked" /><label for="rbtnl_0">是</label> 
                    <input id="rbYS_1" type="radio" name="rbYS" value="2" /><label for="rbtnl_1">否</label>
                </td>
                <th style="width:120px;">分段验收经过审批：</th>
                <td align="left"  style="width:255px;">
                    <input id="rbCP_0" type="radio" name="rbCP" value="1" checked="checked" /><label for="rbtnl_0">是</label> 
                    <input id="rbCP_1" type="radio" name="rbCP" value="2" /><label for="rbtnl_1">否</label>
                </td>
            </tr>
            <tr>
                <th >本次验收生产负荷：</th>
                <td align="left" colspan="3" >
                    1、主要生产设计产量（批复要求）<input type="text" id="txtPFYQ" name="txtPFYQ" class="l-text l-text-editing" style="width:292px;" /><br />
                    2、主要产品实际产量&nbsp;<input type="text" id="txtSZCL" name="txtSZCL" class="l-text l-text-editing" style="width:360px;" /><br />
                    3、实际产量占设计产量的比例&nbsp;<input type="text" id="txtBL" name="txtBL" class="l-text l-text-editing" style="width:40px;" />%
                </td>
            </tr>
            <tr>
                <th>污染治理情况：</th>
                <td align="left" colspan="3" >
                    是否有废水处理设施：<input id="rbWater_0" type="radio" name="rbWater" value="1" checked="checked" /><label for="rbtnl_0">有</label> 
                                        <input id="rbWater_1" type="radio" name="rbWater" value="2" /><label for="rbtnl_1">无</label><br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        如有，是否运行正常：<input id="rbWaterRun_0" type="radio" name="rbWaterRun" value="1" checked="checked" /><label for="rbtnl_0">是</label> 
                                                            <input id="rbWaterRun_1" type="radio" name="rbWaterRun" value="2" /><label for="rbtnl_1">否</label><br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            排污口是否规范：<input id="rbWaterPWK_0" type="radio" name="rbWaterPWK" value="1" checked="checked" /><label for="rbtnl_0">是</label> 
                                                            <input id="rbWaterPWK_1" type="radio" name="rbWaterPWK" value="2" /><label for="rbtnl_1">否</label><br />
                    是否有废气处理设施：<input id="rbGas_0" type="radio" name="rbGas" value="1" checked="checked" /><label for="rbtnl_0">有</label> 
                                        <input id="rbGas_1" type="radio" name="rbGas" value="2" /><label for="rbtnl_1">无</label><br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        如有，是否运行正常：<input id="rbGasRun_0" type="radio" name="rbGasRun" value="1" checked="checked" /><label for="rbtnl_0">是</label> 
                                                            <input id="rbGasRun_1" type="radio" name="rbGasRun" value="2" /><label for="rbtnl_1">否</label><br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            排污口是否规范：<input id="rbGasPWK_0" type="radio" name="rbGasPWK" value="1" checked="checked" /><label for="rbtnl_0">是</label> 
                                                            <input id="rbGasPWK_1" type="radio" name="rbGasPWK" value="2" /><label for="rbtnl_1">否</label><br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                              废气烟道条数：<input type="text" id="txtGasNum" name="txtGasNum" class="l-text l-text-editing" style="width:40px;"/>
                </td>
            </tr>
            <tr>
                <th>备注：</th>
                <td align="left" colspan="3" >
                    <input type="text" id="txtRemark" name="txtRemark" class="l-text l-text-editing" style="width:480px;" />
                </td>
            </tr>
        </table>
    </div>
    <%--勘查结果内容--%>
    <div id="divProspecting" style="display:none;" class="l-form" > 
        <div id="divImgPro"  class="l-group l-group-hasicon tableh1">
        </div>
        <table class="mlr" cellspacing="0" cellpadding="0">
            
            <tr>
                <th style="width:120px;">是否分段验收：</th>
                <td align="left" style="width:255px;" >
                    <input id="rbPYS_0" type="radio" name="rbPYS" disabled="disabled" value="1" checked="checked" /><label for="rbtnl_0">是</label> 
                    <input id="rbPYS_1" type="radio" name="rbPYS" disabled="disabled" value="2" /><label for="rbtnl_1">否</label>
                </td>
                <th style="width:120px;">分段验收经过审批：</th>
                <td align="left"  style="width:255px;">
                    <input id="rbPCP_0" type="radio" name="rbPCP" disabled="disabled" value="1" checked="checked" /><label for="rbtnl_0">是</label> 
                    <input id="rbPCP_1" type="radio" name="rbPCP" disabled="disabled" value="2" /><label for="rbtnl_1">否</label>
                </td>
            </tr>
            <tr>
                <th >本次验收生产负荷：</th>
                <td align="left" colspan="3" >
                    1、主要生产设计产量（批复要求）<input type="text" id="txtPPFYQ" name="txtPPFYQ" disabled="disabled" class="l-text l-text-editing" style="width:292px;" /><br />
                    2、主要产品实际产量&nbsp;<input type="text" id="txtPSZCL" name="txtPSZCL" disabled="disabled" class="l-text l-text-editing" style="width:360px;" /><br />
                    3、实际产量占设计产量的比例&nbsp;<input type="text" id="txtPBL" name="txtPBL" disabled="disabled" class="l-text l-text-editing" style="width:40px;" />%
                </td>
            </tr>
            <tr>
                <th>污染治理情况：</th>
                <td align="left" colspan="3" >
                    是否有废水处理设施：<input id="rbPWater_0" type="radio" name="rbPWater" disabled="disabled" value="1" checked="checked" /><label for="rbtnl_0">有</label> 
                                        <input id="rbPWater_1" type="radio" name="rbPWater" disabled="disabled" value="2" /><label for="rbtnl_1">无</label><br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        如有，是否运行正常：<input id="rbPWaterRun_0" type="radio" name="rbPWaterRun" disabled="disabled" value="1" checked="checked" /><label for="rbtnl_0">是</label> 
                                                            <input id="rbPWaterRun_1" type="radio" name="rbPWaterRun" disabled="disabled" value="2" /><label for="rbtnl_1">否</label><br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            排污口是否规范：<input id="rbPWaterPWK_0" type="radio" name="rbPWaterPWK" disabled="disabled" value="1" checked="checked" /><label for="rbtnl_0">是</label> 
                                                            <input id="rbPWaterPWK_1" type="radio" name="rbPWaterPWK" disabled="disabled" value="2" /><label for="rbtnl_1">否</label><br />
                    是否有废气处理设施：<input id="rbPGas_0" type="radio" name="rbPGas" disabled="disabled" value="1" checked="checked" /><label for="rbtnl_0">有</label> 
                                        <input id="rbPGas_1" type="radio" name="rbPGas" disabled="disabled" value="2" /><label for="rbtnl_1">无</label><br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        如有，是否运行正常：<input id="rbPGasRun_0" type="radio" name="rbPGasRun" disabled="disabled" value="1" checked="checked" /><label for="rbtnl_0">是</label> 
                                                            <input id="rbPGasRun_1" type="radio" name="rbPGasRun" disabled="disabled" value="2" /><label for="rbtnl_1">否</label><br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            排污口是否规范：<input id="rbPGasPWK_0" type="radio" name="rbPGasPWK" disabled="disabled" value="1" checked="checked" /><label for="rbtnl_0">是</label> 
                                                            <input id="rbPGasPWK_1" type="radio" name="rbPGasPWK" disabled="disabled" value="2" /><label for="rbtnl_1">否</label><br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                              废气烟道条数：<input type="text" id="txtPGasNum" name="txtPGasNum" disabled="disabled" class="l-text l-text-editing" style="width:40px;"/>
                </td>
            </tr>
        </table>
    </div>
    <%--选择委托类型--%>
    <div class="l-form" > 
        <table>
            <tr>
                <td>
                    <div id="divContractType">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <table>
        <tr>
            <td style="padding-left: 35px; padding-top: 10px">
                <div id="divContractCode">
                    委托书单号：<label id="Contract_Code" style="color: Red; font-weight: bold"></label>
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <%--信息提交--%>
                <div id="divContractSubmit" style="width:800px;">
                    <UC:WFControl ID="wfControl" EnableViewState="true" runat="server" />
                </div>
            </td>
        </tr>
    </table>
    <div>
    <input type="hidden"  id="hidTaskId" runat="server" />
    </div>
    </form>
</body>
</html>
