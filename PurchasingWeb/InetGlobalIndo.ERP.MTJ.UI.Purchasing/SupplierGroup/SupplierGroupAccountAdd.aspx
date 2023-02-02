<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="SupplierGroupAccountAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.SupplierGroup.SupplierGroupAccountAdd" %>

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

            if (_prmDDL.value == "") {
                _prmDDL.value = "null";
                _prmTextBox.value = "";
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
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
                        <asp:ScriptManager ID="scriptMgr" runat="server" />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <tr>
                                    <td>
                                        Currency
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="CurrencyDropDownList" AutoPostBack="True" OnSelectedIndexChanged="CurrencyDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CurrencyCustomValidator" runat="server" ErrorMessage="Currency Must Be Filled"
                                            Text="*" ControlToValidate="CurrencyDropDownList" Display="Dynamic" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Account AP
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="AccountAPTextBox" Width="80" MaxLength="12"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="AccountAPDropDownList">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="AccountAPRequiredFieldValidator" runat="server" ErrorMessage="Account AP Must Be Filled"
                                            Text="*" ControlToValidate="AccountAPTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Account AP Transit
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="AccountAPTransitTextBox" Width="80" MaxLength="12"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="AccountAPTransitDropDownList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Account Debit AP
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="AccountDebitAPTextBox" Width="80" MaxLength="12"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="AccountDebitAPDropDownList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Account DP
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="AccountDPTextBox" Width="80" MaxLength="12"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="AccountDPDropDownList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Account Variant PO (IDR)
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="AccountVariantPOTextBox" Width="80" MaxLength="12"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="AccountVariantPODropDownList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Account PPn (IDR)
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="AccountPPnTextBox" Width="80" MaxLength="12"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="AccountPPnDropDownList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Account PPh (IDR)
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="AccountPPhTextBox" Width="80" MaxLength="12"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="AccountPPhDropDownList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Account Other Purchase
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="AccountOtherPurchaseTextBox" Width="80" MaxLength="12"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="AccountOtherPurchaseDropDownList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Account Disc Purchase
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="AccountDiscPurchaseTextBox" Width="80" MaxLength="12"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="AccountDiscPurchaseDropDownList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Account Duty Transit
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="AccountDutyTransitTextBox" Width="80" MaxLength="12"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="AccountDutyTransitDropDownList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Account Handling Transit
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="AccountHandlingTransitTextBox" Width="80" MaxLength="12"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="AccountHandlingTransitDropDownList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        FgActive
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="FgActiveCheckBox" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Remark
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80px" MaxLength="500"
                                            TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
