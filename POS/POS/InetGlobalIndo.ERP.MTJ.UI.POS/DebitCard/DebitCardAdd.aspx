<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="DebitCardAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.POS.DebitCard.DebitCardAdd" %>

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
                _prmTextBox.value = "";
                _prmDDL.value = "null";
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
                        <tr>
                            <td>
                                Debit Card Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DebitCardCodeTextBox" Width="120" MaxLength="49"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="DebitCardCodeRequiredFieldValidator" runat="server"
                                    ErrorMessage="Debit Card Code Must Be Filled" Text="*" ControlToValidate="DebitCardCodeTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Debit Card Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DebitCardNameTextBox" Width="210" MaxLength="99"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="DebitCardNameRequiredFieldValidator" runat="server"
                                    ErrorMessage="Debit Card Name Must Be Filled" Text="*" ControlToValidate="DebitCardNameTextBox"
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
                                <asp:TextBox ID="AccountCodeTextBox" runat="server" Width="120px"></asp:TextBox>
                                <asp:DropDownList runat="server" ID="AccountDropDownList" MaxLength="19">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="AccountCodeRequiredFieldValidator" runat="server"
                                    ControlToValidate="AccountCodeTextBox" Text="*" ErrorMessage="Account Must Be Filled"></asp:RequiredFieldValidator>
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
