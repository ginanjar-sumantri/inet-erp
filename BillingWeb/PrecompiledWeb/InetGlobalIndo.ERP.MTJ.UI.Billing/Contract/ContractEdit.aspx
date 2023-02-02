<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.Contract.ContractEdit, App_Web_0iuy3kfz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <table cellpadding="3" cellspacing="0" width="0" border="0">
        <tr>
            <td class="tahoma_14_black">
                <b>
                    <asp:Literal ID="PageTitleLiteral" runat="server" />
                </b>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="3" cellspacing="0" width="0" border="0">
                    <tr>
                        <td>
                            Trans No.
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="TransNmbrTextBox" runat="server" Width="160" BackColor="#CCCCCC"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            File No.
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="FileNmbrTextBox" runat="server" Width="160" BackColor="#CCCCCC"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
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
                            <asp:TextBox runat="server" ID="DateTextBox" Width="80" BackColor="#CCCCCC"></asp:TextBox>
                            <input id="DateButton" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)"
                                value="..." />
                        </td>
                        <td>
                        </td>
                        <td>
                            Finance Customer PIC
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="FinaceCustomerPICTextBox" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="FinaceCustomerPICRequiredFieldValidator" runat="server"
                                ControlToValidate="FinaceCustomerPICTextBox" Display="Dynamic" ErrorMessage="Finace Customer PIC Must Be Filled"
                                Text="*">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Sales Confirmation No Ref
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="SalesConfirmationNoRefTextBox" runat="server" Width="160" BackColor="#CCCCCC"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            Finance Customer Phone
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="FinanceCustomerPhoneTextBox" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="FinanceCustomerPhoneRequiredFieldValidator" runat="server"
                                ControlToValidate="FinanceCustomerPhoneTextBox" Display="Dynamic" ErrorMessage="Finance Customer Phone Must Be Filled"
                                Text="*">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Company Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CompanyNameTextBox" Width="180" BackColor="#CCCCCC"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            Finance Customer Fax
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="FinanceCustomerFaxTextBox" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="FinanceCustomerFaxRequiredFieldValidator" runat="server"
                                ControlToValidate="FinanceCustomerFaxTextBox" Display="Dynamic" ErrorMessage="Finance Customer Fax Be Filled"
                                Text="*">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Responsible Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="ResponsibleNameTextBox" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ResponsibleNameRequiredFieldValidator" runat="server"
                                ControlToValidate="ResponsibleNameTextBox" Display="Dynamic" ErrorMessage="Responsible Name Must Be Filled"
                                Text="*">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                        <td>
                            Finance Customer Email
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="FinanceCustomerEmailTextBox" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="FinanceCustomerEmailRequiredFieldValidator" runat="server"
                                ControlToValidate="FinanceCustomerEmailTextBox" Display="Dynamic" ErrorMessage="Finance Customer Email Must Be Filled"
                                Text="*">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Title Name
                        </td>
                        <td>
                            :
                        </td>
                        <td colspan="5">
                            <asp:TextBox runat="server" ID="TitleNameTextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="TitleNameRequiredFieldValidator" runat="server" ControlToValidate="TitleNameTextBox"
                                Display="Dynamic" ErrorMessage="Title Name Must Be Filled" Text="*">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            Letter Provider Information
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="LetteProviderInformationTextBox" MaxLength="300"
                                Width="200" Height="60" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="LetteProviderInformationRequiredFieldValidator" runat="server"
                                ControlToValidate="LetteProviderInformationTextBox" Display="Dynamic" ErrorMessage="Letter Provider Information Must Be Filled"
                                Text="*">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                        <td>
                            Letter Customer Information
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="LetteCustomerInformationTextBox" runat="server" MaxLength="300"
                                Width="200" Height="60" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="LetteCustomerInformationRequiredFieldValidator" runat="server"
                                ControlToValidate="LetteCustomerInformationTextBox" Display="Dynamic" ErrorMessage="Letter Customer Information Must Be Filled"
                                Text="*">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
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
                        <td>
                            <asp:ImageButton ID="ViewDetailButton" runat="server" CausesValidation="False" OnClick="ViewDetailButton_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="SaveAndViewDetailButton" runat="server" OnClick="SaveAndViewDetailButton_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
