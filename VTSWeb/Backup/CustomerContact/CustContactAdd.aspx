<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="CustContactAdd.aspx.cs" Inherits="VTSWeb.UI.CustContactAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <table>
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
                        <td colspan="2">
                            Company Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="CustomerDropDownList" runat="server">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator" runat="server" ControlToValidate="CustomerDropDownList"
                                ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="Company Name Must Be Chosen"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4">
                            <b>Profile</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Name
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="NameTextBox" runat="server" Width="300"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server" ErrorMessage="Name Must Be Filled"
                                Text="*" ControlToValidate="NameTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Title
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="TitleTextBox" runat="server" Width="300"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="TitleRequiredFieldValidator" runat="server" ErrorMessage="Title Must Be Filled"
                                Text="*" ControlToValidate="TitleTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Type
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="TypeDropDownList" runat="server">
                                <asp:ListItem Value="Mr">Mr</asp:ListItem>
                                <asp:ListItem Value="Mrs">Mrs</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CustomValidator ID="TypeCustomValidator" runat="server" ControlToValidate="TypeDropDownList"
                                ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="Type Must Be Chosen"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Religion
                        </td>
                        <td colspan="2">
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ReligionDropDownList" runat="server">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="ReligionValidator" runat="server" ControlToValidate="ReligionDropDownList"
                                ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="Religion Must Be Chosen"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Birthday
                        </td>
                        <td valign="top" colspan="2">
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="BirthdayTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox><input
                                type="button" id="button2" value="..." onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_BirthdayTextBox,'yyyy-mm-dd',this)" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Remark
                        </td>
                        <td valign="top" colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="RemarkTextBox" runat="server" Width="300" Height="100" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4">
                            <b>Contact</b>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Address 1
                        </td>
                        <td valign="top" colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="Address1TextBox" runat="server" Width="300" Height="100" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Address 2
                        </td>
                        <td valign="top" colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="Address2TextBox" runat="server" Width="300" Height="100" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Country
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="CountryDropDownList" runat="server">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CountryCustomValidator" runat="server" ControlToValidate="CountryDropDownList"
                                ClientValidationFunction="CountryDownValidation" Text="*" ErrorMessage="Country Must Be Chosen"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Zip Code
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="ZipCodeTextBox" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Phone
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="PhoneTextBox" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Fax
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="FaxTextBox" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Email
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="EmailTextBox" runat="server" Width="300"></asp:TextBox></asp:RequiredFieldValidator><%--<asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                                                                ErrorMessage="Email Not Valid" Text="*" ControlToValidate="EmailTextBox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Goods In
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:CheckBox ID="FgGoodsInChecked" runat="server" />&nbsp;&nbsp;&nbsp; Goods Out:
                            <asp:CheckBox ID="FgGoodsOutChecked" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Access
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:CheckBox ID="FgAccessChecked" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Additional Visitor
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:CheckBox ID="FgAdditionalVisitorChecked" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Contact Authorization
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:CheckBox ID="FgContactAuthorizationChecked" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Card ID
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CardIDTextBox" runat="server" Width="200"></asp:TextBox>
                        </td>
                    </tr>
                </table>
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
</asp:Content>
