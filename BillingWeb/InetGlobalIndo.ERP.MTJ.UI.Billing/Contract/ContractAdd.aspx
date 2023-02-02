<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="ContractAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.Contract.ContractAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <asp:Panel runat="server" ID="Panel1" DefaultButton="NextButton">
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
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                Trans Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <input id="DateButton" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />
                            </td>
                            <td width="10px">
                            </td>
                            <td>
                                Finance Customer PIC
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="FinaceCustomerPICTextBox" runat="server" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="FinaceCustomerPICRequiredFieldValidator" runat="server"
                                    ControlToValidate="FinaceCustomerPICTextBox" Display="Dynamic" ErrorMessage="Finace Customer PIC Must Be Filled"
                                    Text="*">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <tr>
                                    <td>
                                        Sales Confirmation No Ref
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="SalesConfirmationNoRefDDL" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="SalesConfirmationNoRefDDL_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="SalesConfirmationNoRefCustomValidator" runat="server" ErrorMessage="Sales Confirmation No Ref Must Be Select"
                                            Text="*" ControlToValidate="SalesConfirmationNoRefDDL" ClientValidationFunction="DropDownValidation">
                                        </asp:CustomValidator>
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
                                        <asp:TextBox ID="FinanceCustomerPhoneTextBox" runat="server" MaxLength="30">
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
                                        <asp:TextBox runat="server" ID="CompanyNameTextBox" Width="150" BackColor="#CCCCCC"
                                            MaxLength="50"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ID="YearRequiredFieldValidator" runat="server" ControlToValidate="YearTextBox"
                                    Display="Dynamic" ErrorMessage="Year Must Be Filled" Text="*">
                                </asp:RequiredFieldValidator>--%>
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
                                        <asp:TextBox ID="FinanceCustomerFaxTextBox" runat="server" MaxLength="30">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="FinanceCustomerFaxValidator" runat="server" ControlToValidate="FinanceCustomerFaxTextBox"
                                            Display="Dynamic" ErrorMessage="Finance Customer Fax Must Be Filled" Text="*">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <tr>
                            <td>
                                Responsible Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ResponsibleNameTextBox" runat="server" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ResponsibleNameFieldValidator" runat="server" ControlToValidate="ResponsibleNameTextBox"
                                    Display="Dynamic" ErrorMessage="Responsible Name Must Be Filled" Text="*">
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
                                <asp:TextBox ID="FinanceCustomerEmailTextBox" runat="server" MaxLength="50">
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
                                <asp:TextBox runat="server" ID="TitleNameTextBox" MaxLength="50"></asp:TextBox>
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
                            <td rowspan="4">
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
                            <td rowspan="4">
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
