﻿<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="VisitorAdd.aspx.cs" Inherits="VTSWeb.UI.VisitorAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <table style="width: 361px">
        <tr>
            <td class="tahoma_14_black">
                <b>
                    <asp:Literal ID="PageTitleLiteral" runat="server" /></b>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="WarningLabel" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="3" cellspacing="0" width="0" border="0">
                    <tr>
                        <td>
                            Company
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="CustomerDropDownList" runat="server" OnSelectedIndexChanged="CustomerDropDownList_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomerCustomValidator" runat="server" ControlToValidate="CustomerDropDownList"
                                ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="Company Must Be Chosen"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Visitor
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ContactNameDropDownList" runat="server">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="ContactNameCustomValidator" runat="server" ControlToValidate="ContactNameDropDownList"
                                ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="Visitor Must Be Chosen"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            Area
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="AreaNameDropDownList" runat="server">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="AreaNameCustomValidator" runat="server" ControlToValidate="AreaNameDropDownList"
                                ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="Area Must Be Chosen"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <table cellpadding="3" cellspacing="0" border="0" width="0">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ResetButton" runat="server" CausesValidation="False" OnClick="ResetButton_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
