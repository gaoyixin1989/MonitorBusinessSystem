<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Search.master" AutoEventWireup="True" Inherits="Sys_WF_WFShowStepDetail" Codebehind="WFShowStepDetail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        body
        {
            font-size: 12px;
        }
        ul, li
        {
            list-style: none;
        }
        ul, li, h1, h2, h3, h4, p
        {
            padding: 0;
            margin: 0;
        }
        .layout20121
        {
            margin: 0 auto;
            width: 330px;
            overflow: hidden;
            color: #333;
        }
        
        .listgreen
        {
            line-height: 22px;
            text-align: center;
        }
        .listgreen p
        {
            border: 1px solid #bfe0bf;
            border-top: none;
            background: #e9f9e9;
            padding: 5px;
            text-align: left;
        }
        .listgreen p span
        {
            width: 5em;
        }
        .listgreen h2
        {
            background: #5a8f5a;
            color: #fff;
            font-size: 12px;
        }
        .listgreen img
        {
            margin: 4px;
        }
        
        .listyellow
        {
            line-height: 22px;
            text-align: center;
        }
        .listyellow p
        {
            border: 1px solid #e5d7bc;
            border-top: none;
            background: #f5ecdb;
            padding: 5px;
            text-align: left;
        }
        .listyellow p span
        {
            width: 5em;
        }
        .listyellow h2
        {
            background: #de9a1d;
            color: #fff;
            font-size: 12px;
        }
        .listyellow img
        {
            margin: 4px;
        }
        
        .listred
        {
            line-height: 22px;
            text-align: center;
        }
        .listred p
        {
            border: 1px solid #ebc8bf;
            border-top: none;
            background: #f8e6e2;
            padding: 5px;
            text-align: left;
        }
        .listred p span
        {
            width: 5em;
        }
        .listred h2
        {
            background: #e34323;
            color: #fff;
            font-size: 12px;
        }
        .listred img
        {
            margin: 4px;
        }
        
        .listgray
        {
            line-height: 22px;
            text-align: center;
        }
        .listgray p
        {
            border: 1px solid #d4d4d4;
            border-top: none;
            background: #eaeaea;
            padding: 5px;
            text-align: left;
        }
        .listgray p span
        {
            width: 5em;
        }
        .listgray h2
        {
            background: #a9a9a9;
            color: #fff;
            font-size: 12px;
        }
        .listgray img
        {
            margin: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphData" runat="Server">
    <div class="layout20121">
        <table >
            <tr>
                <td>
                    <div id="dvWFPic" style="width: 100%;">
                        <span id="spShowPic" style="text-align: left; width: 100%;" runat="server"></span>
                    </div>
                </td>
                <td>
                    <div id="dvOperate">
                    </div>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdID" runat="server" />
        <asp:HiddenField ID="hdType" runat="server" />
    </div>
</asp:Content>
