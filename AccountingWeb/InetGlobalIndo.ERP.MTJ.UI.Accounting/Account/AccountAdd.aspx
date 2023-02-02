<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="AccountAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.Account.AccountAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function Selected(_prmDDL, _prmTextBox) {
            if (_prmDDL.value != "null") {
                _prmTextBox.value = _prmDDL.value;
            }
            else {
                _prmTextBox.value = "";
            }
        }

        function Blur(_prmDDL, _prmTextBox) {
            _prmDDL.value = _prmTextBox.value;

            if (_prmDDL.value == '') {
                _prmDDL.value = "null";
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="scriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
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
                                <asp:DropDownList ID="BranchAccDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="BranchAccCustomValidator" runat="server" ControlToValidate="BranchAccDropDownList"
                                    ErrorMessage="Branch Account Must Be Filled" ClientValidationFunction="DropDownValidation"
                                    Text="*"></asp:CustomValidator>
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
                                <asp:UpdatePanel ID="LastCodeUpdatePanel" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox runat="server" ID="CodeTextBox" Width="100px" MaxLength="6"></asp:TextBox>
                                        &nbsp;<asp:DropDownList ID="AccClassDropDownList" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="AccClassDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="AccountRequiredFieldValidator" runat="server" ErrorMessage="Account Must Be Filled"
                                            Text="*" ControlToValidate="CodeTextBox"></asp:RequiredFieldValidator>
                                        <asp:Label ID="LastCodeLabel" runat="server"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
                                <asp:TextBox ID="DetailTextBox" runat="server" MaxLength="4" Width="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="DetailRequiredFieldValidator" runat="server" ControlToValidate="DetailTextBox"
                                    Display="Dynamic" ErrorMessage="Detail Must Be Filled" Text="*"></asp:RequiredFieldValidator>
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
                                <asp:CustomValidator ID="CurrencyCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="CurrCodeDropDownList" ErrorMessage="Currency Must Be Choosed"
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
                                <asp:TextBox ID="DescTextBox" runat="server" MaxLength="50" Width="350"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DescTextBox"
                                    Display="Dynamic" ErrorMessage="Description Must Be Filled" Text="*"></asp:RequiredFieldValidator>
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
                            <asp:ListItem Value="D" Selected="True">Debit</asp:ListItem>
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
                                <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="CancelButton" runat="server" OnClick="CancelButton_Click" CausesValidation="False" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ResetButton" runat="server" OnClick="ResetButton_Click" CausesValidation="False" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
