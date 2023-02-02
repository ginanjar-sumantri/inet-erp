<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="CustomerContactEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.Customer.CustomerContactEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
        <table width="0" border="0" cellpadding="3" cellspacing="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="0" border="0" cellpadding="3" cellspacing="0">
                        <tr>
                            <td>
                                Customer Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CustCodeTextBox" Width="100" ReadOnly="true" BackColor="#cccccc"
                                    runat="server"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ItemNoTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Contact Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="ContactTypeDropDownList" runat="server">
                                    <asp:ListItem Value="Mr" Text="Mr"></asp:ListItem>
                                    <asp:ListItem Value="Mrs" Text="Mrs"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:CustomValidator ID="ContactTypeDDLCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Contact Type Must Be Filled" Text="*" ControlToValidate="ContactTypeDropDownList"></asp:CustomValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Country
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="CountryDropDownList" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Contact Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ContactNameTextBox" runat="server" Width="200"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ContactNameRequiredFieldValidator" runat="server"
                                    ErrorMessage="Contact Name Must Be Filled" Text="*" ControlToValidate="ContactNameTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Postal Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PostalCodeTextBox" runat="server" Width="100" MaxLength="5"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Contact Title
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ContactTitleTextBox" runat="server" Width="200" MaxLength="100"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Telephone
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PhoneTextBox" runat="server" Width="200" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Birthday
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="BirthDateTextBox" runat="server" BackColor="#CCCCCC">
                                </asp:TextBox>
                                &nbsp;
                                <%--<input type="button" id="birth_date_start" value="..." onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_BirthDateTextBox,'yyyy-mm-dd',this)" />--%>
                               <asp:Literal ID="BirthDateLiteral" runat="server"></asp:Literal>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Fax
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="FaxTextBox" runat="server" Width="200" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Religion
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="ReligionDropDownList" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Email
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="EmailTextBox" runat="server" Width="300" MaxLength="40"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                                    Text="*" ErrorMessage="Email is not Valid" ControlToValidate="EmailTextBox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Address #1
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="Addr1TextBox" runat="server" Width="250" TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Remark
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="RemarkTextBox" runat="server" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>
                                <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                characters left
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Address #2
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="Addr2TextBox" runat="server" Width="250" TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="CancelButton" runat="server" OnClick="CancelButton_Click" CausesValidation="false" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ResetButton" runat="server" OnClick="ResetButton_Click" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
