<%@ Control Language="C#" AutoEventWireup="True" Inherits="Channels_MisII_Contract_ContratTypeSelect"
    CodeBehind="ContratTypeSelect.ascx.cs" %>
<div class="l-form">
    <div id="divImgSelect" class="l-group l-group-hasicon tableh1">
    </div>
    <table class="mlr">
        <tr>
            <th>
                任务单号：
            </th>
            <td align="left" colspan="3">
                <input type="text" id="txtTASK_CODE" class="l-text l-text-editing" style="width: 300px;" />
            </td>
        </tr>
        <tr>
            <th id="thTYPE">
                <%--委托类型--%>监测目的：
            </th>
            <td align="left">
                <input type="text" id="Contract_Type" name="Contract_Type" class="l-text l-text-editing" />
            </td>
            <td style="width: 8px; color: Red;">
                *
            </td>
            <th>
                委托年度：
            </th>
            <td align="left">
                <input type="text" id="Contrat_Year" name="Contrat_Year" class="l-text l-text-editing" />
            </td>
            <td style="width: 8px; color: Red;">
                *
            </td>
        </tr>
        <tr>
            <th>
                监测类型：
            </th>
            <td align="left">
                <input type="text" id="Monitor_Type" name="Monitor_Type" class="l-text l-text-editing" />
            </td>
            <td style="width: 5px; color: Red;">
                *
            </td>
            <th>
                样品来源：
            </th>
            <td align="left">
                <input type="text" id="Sample_Source" name="Sample_Source" class="l-text l-text-editing" />
            </td>
            <td align="right">
                <input type="button" value="确认" id="btn_OkSelect" name="btn_OkSelect" class="l-button l-button-submit" />
            </td>
            <td align="center" valign="bottom">
            </td>
        </tr>
    </table>
</div>
