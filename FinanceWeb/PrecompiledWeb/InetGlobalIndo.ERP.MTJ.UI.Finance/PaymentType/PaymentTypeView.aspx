<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.PaymentType.PaymentTypeView, App_Web_f8flhnwc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
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
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Payment Code
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="PaymentCodeTextBox" Width="70" MaxLength="10" BackColor="#CCCCCC"
                        ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Payment Name
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="PaymentNameTextBox" Width="150" MaxLength="50" BackColor="#CCCCCC"
                        ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Account
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="AccountTextBox" Width="80" MaxLength="12" BackColor="#CCCCCC"
                        ReadOnly="True"></asp:TextBox>
                    <asp:TextBox runat="server" ID="AccountNameTextBox" Width="300" MaxLength="50" BackColor="#CCCCCC"
                        ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Mode
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ModeDropDownList" Enabled="False">
                        <asp:ListItem Selected="True" Text="Bank" Value="B"></asp:ListItem>
                        <asp:ListItem Text="Giro" Value="G"></asp:ListItem>
                        <asp:ListItem Text="DP" Value="D"></asp:ListItem>
                        <asp:ListItem Text="Kas" Value="K"></asp:ListItem>
                        <asp:ListItem Text="Other" Value="O"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Type
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="TypeDropDownList" Enabled="False">
                        <asp:ListItem Selected="true" Text="Payment" Value="P"></asp:ListItem>
                        <asp:ListItem Text="Receipt" Value="R"></asp:ListItem>
                        <asp:ListItem Text="All" Value="A"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Bank
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="BankTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    No. Rekening
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="NoRekeningTextBox" Width="150" MaxLength="30" BackColor="#CCCCCC"
                        ReadOnly="True"></asp:TextBox>
                    &nbsp; a / n :
                    <asp:TextBox ID="NoRekeningOwnerTextBox" runat="server" MaxLength="100" Width="150"
                        BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Acc Bank Charge
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="AccBankChargeTextBox" Width="80" MaxLength="12" BackColor="#CCCCCC"
                        ReadOnly="True"></asp:TextBox>
                    <asp:TextBox runat="server" ID="AccBankChargeNameTextBox" Width="300" MaxLength="100"
                        BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    Bank Charge Expense
                </td>
                <td valign="top">
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="ExpenseGiroTextBox" Width="100" BackColor="#CCCCCC"
                        ReadOnly="True"></asp:TextBox>
                    <asp:RadioButtonList runat="server" ID="FgBankChargeRadioButtonList" Enabled="false"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Text="Percentage" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Amount" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    Acc Customer Charge
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="AccCustChargeTextBox" Width="80" MaxLength="12" BackColor="#CCCCCC"
                        ReadOnly="True"></asp:TextBox>
                    <asp:TextBox runat="server" ID="AccCustChargeNameTextBox" Width="300" MaxLength="100"
                        BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    Customer Charge Revenue
                </td>
                <td valign="top">
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="CustChargeRevenueTextBox" Width="100" BackColor="#CCCCCC"
                        ReadOnly="True"></asp:TextBox>
                    <asp:RadioButtonList runat="server" ID="CustChargeRevenueRadioButtonList" Enabled="false"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Text="Percentage" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Amount" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
