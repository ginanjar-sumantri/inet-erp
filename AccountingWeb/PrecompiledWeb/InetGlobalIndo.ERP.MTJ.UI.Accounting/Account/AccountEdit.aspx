<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.Account.AccountEdit, App_Web_x_a35nxn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
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
                                Branch Account
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="BranchAccountTextBox" Width="560" BackColor="#CCCCCC"
                                    ReadOnly="true">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Class Account
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CodeTextBox" Width="100" MaxLength="6" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>&nbsp;
                                <asp:TextBox runat="server" ID="AccClassNameTextBox" Width="280" BackColor="#CCCCCC"
                                    ReadOnly="true">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Detail
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DetailTextBox" Width="100" MaxLength="4" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
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
                                <asp:DropDownList runat="server" ID="CurrCodeDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CurrencyCustomValidator" runat="server" ErrorMessage="Currency Must Be Choosed"
                                    ControlToValidate="CurrCodeDropDownList" ClientValidationFunction="DropDownValidation"
                                    Text="*"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Description
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DescTextBox" MaxLength="50" Width="350"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Description Must Be Filled"
                                    Text="*" ControlToValidate="DescTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset title="Saldo Normal">
                        <legend>Normal Balance</legend>
                        <asp:RadioButtonList runat="server" ID="SaldoNormalRBL" RepeatDirection="Horizontal"
                            RepeatColumns="2">
                            <asp:ListItem Value="D">Debit</asp:ListItem>
                            <asp:ListItem Value="C">Credit</asp:ListItem>
                        </asp:RadioButtonList>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset title="Subled Type">
                        <legend>Sub Ledger</legend>
                        <asp:RadioButtonList runat="server" ID="SubledRBL" RepeatDirection="Horizontal" RepeatColumns="2">
                        </asp:RadioButtonList>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <td>
                                Active
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="FgActiveCheckBox" runat="server" />
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
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
