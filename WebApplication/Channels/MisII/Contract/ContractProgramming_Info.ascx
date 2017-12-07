<%@ Control Language="C#" AutoEventWireup="True"
    Inherits="Channels_MisII_Contract_ContractProgramming_Info" Codebehind="ContractProgramming_Info.ascx.cs" %>
<div>
    <div class="l-form">
        <div id="divImgInfo" class="l-group l-group-hasicon tableh1">
        </div>
        <table class="mlr">
            <tr>
                <th>
                    项目名称:
                </th>
                <td colspan="3">
                    <input type="text" id="Project_Name" class="l-text l-text-editing" style="width: 565px" />
                </td>
            </tr>
            <tr>
                <th>
                    合同编号:
                </th>
                <td align="left" class="l-table-edit-td">
                    <input type="text" id="ContractCode" class="l-text l-text-editing" style="width: 200px" />
                </td>
                <th>
                    委托年度:
                </th>
                <td align="left" class="l-table-edit-td">
                    <input type="text" id="Contrat_Year" class="l-text l-text-editing" style="width: 195px" />
                </td>
            </tr>
            <tr>
                <th>
                    委托类型:
                </th>
                <td align="left" class="l-table-edit-td">
                    <input type="text" id="Contract_Type" class="l-text l-text-editing" style="width: 200px" />
                </td>
                <th>
                    监测类型:
                </th>
                <td align="left" class="l-table-edit-td">
                    <input type="text" id="Monitor_Type" class="l-text l-text-editing" style="width: 195px" />
                </td>
            </tr>
            <tr>
                <th>
                    签订日期
                </th>
                <td align="left" class="l-table-edit-td">
                    <input type="text" id="Contract_Date" class="l-text l-text-editing" style="width: 200px" />
                </td>
                <th id="thConst">
                    样品来源:
                </th>
                <td id="tdConst" align="left" class="l-table-edit-td divfloat">
                    <input type="text" id="ContractConst" class="l-text l-text-editing" style="width: 195px" />
                </td>
            </tr>
            <tr>
                <th>
                    监测目的:
                </th>
                <td align="left" colspan="3">
                    <input type="text" id="Monitor_Purpose" class="l-text l-text-editing" style="width: 565px;" />
                </td>
            </tr>
            <tr>
                <th>
                    备 注:
                </th>
                <td align="left" colspan="3">
                    <input type="text" id="Remarks" class="l-text l-text-editing" style="width: 565px" />
                </td>
            </tr>
            <tr>
                <th>
                    任务单号:
                </th>
                <td align="left" colspan="3">
                    <input type="text" id="txtTASK_CODE" class="l-text l-text-editing" style="width: 565px;" />
                </td>
            </tr>
<%--            <tr>
                <th style="width:100px;">
                    计划采样日期：
                </th>
                <td align="left" class="l-table-edit-td">
                    <input type="text" id="txtPLAN_DATE" class="l-text l-text-editing" style="width: 200px" />
                </td>
                <th id="th1"  style="width:100px;">
                    计划任务完成日期:
                </th>
                <td id="td1" align="left" class="l-table-edit-td divfloat">
                    <input type="text" id="txtFINISH_DATE" class="l-text l-text-editing" style="width: 195px" />
                </td>
            </tr>--%>
        </table>
    </div>
</div>
