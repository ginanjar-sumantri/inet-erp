<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="PaymentTypeEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.PaymentType.PaymentTypeEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function Selected(_prmDDL, _prmTextBox)
        {
            if(_prmDDL.value != "null")
            {
                _prmTextBox.value = _prmDDL.value;
            }
            else
            {
                _prmTextBox.value = "";
            }
        }
        
        function Blur(_prmDDL, _prmTextBox)
        {
            _prmDDL.value = _prmTextBox.value;
            
            if(_prmDDL.value == '')
            {
                _prmDDL.value = "null";
                _prmTextBox.value = "";
            }
        }
        
       function EnableOrDisable(_prmModeDDL, _prmBankDDL, _prmNoRekeningTextBox,_prmRekOwnerTextBox, _prmAccBankChangeTextBox, _prmAccBankChangeDDL, _prmExpenseGiroTextBox, _prmAccCustChangeTextBox, _prmAccCustChangeDDL, _prmCustChargeRevenueTextBox, _prmFgBankCharge, _prmFgCustRevenue)
        {
            if(_prmModeDDL.value == 'B')
            {
                _prmBankDDL.disabled = false;
                
                _prmNoRekeningTextBox.readOnly = false;
                _prmNoRekeningTextBox.style.background = '#FFFFFF';
                
                _prmRekOwnerTextBox.readOnly=false;
                _prmRekOwnerTextBox.style.background = '#FFFFFF';
                
                _prmAccBankChangeTextBox.readOnly = false;
                _prmAccBankChangeTextBox.style.background = '#FFFFFF';
                
                _prmAccBankChangeDDL.disabled = false;
                
                _prmExpenseGiroTextBox.readOnly = false;
                _prmExpenseGiroTextBox.style.background = '#FFFFFF';
                
                _prmAccCustChangeTextBox.readOnly = false;
                _prmAccCustChangeTextBox.style.background = '#FFFFFF';
                
                _prmAccCustChangeDDL.disabled = false;
                
                _prmCustChargeRevenueTextBox.readOnly = false;
                _prmCustChargeRevenueTextBox.style.background = '#FFFFFF';
                
                _prmFgBankCharge.disabled = false;
                _prmFgCustRevenue.disabled = false;
            }
            else
            {
                _prmBankDDL.value = "null";
                _prmBankDDL.disabled = true;
                
                _prmNoRekeningTextBox.value = "";
                _prmNoRekeningTextBox.readOnly = true;
                _prmNoRekeningTextBox.style.background = '#CCCCCC';
                
                _prmRekOwnerTextBox.value = "";
                _prmRekOwnerTextBox.readOnly=true;
                _prmRekOwnerTextBox.style.background = '#CCCCCC';
                
                _prmAccBankChangeTextBox.value = "";
                _prmAccBankChangeTextBox.readOnly = true;
                _prmAccBankChangeTextBox.style.background = '#CCCCCC';
                
                _prmAccBankChangeDDL.value = "null";
                _prmAccBankChangeDDL.disabled = true;
                
                _prmExpenseGiroTextBox.value = "0";
                _prmExpenseGiroTextBox.readOnly = true;
                _prmExpenseGiroTextBox.style.background = '#CCCCCC';
                
                _prmAccCustChangeTextBox.value = "";
                _prmAccCustChangeTextBox.readOnly = true;
                _prmAccCustChangeTextBox.style.background = '#CCCCCC';
                
                _prmAccCustChangeDDL.value = "null";
                _prmAccCustChangeDDL.disabled = true;
                
                _prmCustChargeRevenueTextBox.value = "0";
                _prmCustChargeRevenueTextBox.readOnly = true;
                _prmCustChargeRevenueTextBox.style.background = '#CCCCCC';
                
                _prmFgBankCharge.disabled = true;
                _prmFgCustRevenue.disabled = true;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="scriptMgr" runat="server" EnablePartialRendering="true" />
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
        <table cellpadding="3" cellspacing="0" width="0" border="0">
            <tr>
                <td class="tahoma_14_black">
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
                    <asp:TextBox runat="server" ID="PaymentNameTextBox" Width="150" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PaymentNameRequiredFieldValidator" runat="server"
                        ErrorMessage="Payment Name Must Be Filled" Text="*" ControlToValidate="PaymentNameTextBox"
                        Display="Dynamic"></asp:RequiredFieldValidator>
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
                    <asp:TextBox runat="server" ID="AccountTextBox" Width="80" MaxLength="12"></asp:TextBox>
                    <asp:DropDownList runat="server" ID="AccountDropDownList">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="AccountRequiredFieldValidator" runat="server" ErrorMessage="Account Must Be Filled"
                        Text="*" ControlToValidate="AccountTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
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
                    <asp:DropDownList runat="server" ID="ModeDropDownList">
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
                    <asp:DropDownList runat="server" ID="TypeDropDownList">
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
                    <asp:DropDownList runat="server" ID="BankDropDownList">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    No. Rekening
                </td>
                <td>
                    :
                </td>
                <td valign="top">
                    <asp:TextBox runat="server" ID="NoRekeningTextBox" Width="150" MaxLength="30"></asp:TextBox>
                    &nbsp; a / n :
                    <asp:TextBox ID="NoRekeningOwnerTextBox" runat="server" MaxLength="100" Width="150"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Account Bank Charge
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:TextBox runat="server" ID="AccBankChargeTextBox" Width="80" MaxLength="12"></asp:TextBox>
                            <asp:DropDownList runat="server" ID="AccBankChargeDropDownList" AutoPostBack="true"
                                OnSelectedIndexChanged="AccBankChargeDropDownList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:HiddenField ID="BankChargeDecimalPlaceHiddenField" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
                    <asp:TextBox runat="server" ID="ExpenseGiroTextBox" Width="100"></asp:TextBox>
                    <asp:RadioButtonList runat="server" ID="FgBankChargeRadioButtonList" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Percentage" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Amount" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    Account Customer Charge
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:TextBox runat="server" ID="AccCustChargeTextBox" Width="80" MaxLength="12"></asp:TextBox>
                            <asp:DropDownList runat="server" ID="AccCustChargeDropDownList" AutoPostBack="true"
                                OnSelectedIndexChanged="AccCustChargeDropDownList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:HiddenField ID="CustChargeDecimalPlaceHiddenField" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
                    <asp:TextBox runat="server" ID="CustChargeRevenueTextBox" Width="100"></asp:TextBox>
                    <asp:RadioButtonList runat="server" ID="CustChargeRevenueRadioButtonList" RepeatDirection="Horizontal">
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
