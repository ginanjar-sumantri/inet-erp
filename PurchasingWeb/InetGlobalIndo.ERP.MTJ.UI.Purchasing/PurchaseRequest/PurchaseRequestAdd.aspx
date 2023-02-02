<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="PurchaseRequestAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseRequest.PurchaseRequestAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="NextButton">
        <table cellpadding="3" cellspacing="0" width="0" border="0">
            <tr>
                <td class="tahoma_14_black" colspan="3">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Trans Date
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                    <%--<input type="button" value="..." id="Img1" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)" />--%>
                    <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    Organization Unit
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="OrgUnitDDL">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="OrgUnitCustomValidator" runat="server" ErrorMessage="Organization Unit Must Be Filled"
                        Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="OrgUnitDDL"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Request By
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="RequestByTextBox" Width="200" MaxLength="40"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Currency
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList ID="CurrCodeDropDownList" runat="server">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="CurrCodeCustomValidator" runat="server" Text="*" ControlToValidate="CurrCodeDropDownList"
                        ErrorMessage="Currency Must Be Filled" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                </td>
            </tr>
            <tr valign="top">
                <td>
                    Remark
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>
                    <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                    characters left
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="NextButton" runat="server" OnClick="NextButton_Click" />
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
    </asp:Panel>
</asp:Content>
