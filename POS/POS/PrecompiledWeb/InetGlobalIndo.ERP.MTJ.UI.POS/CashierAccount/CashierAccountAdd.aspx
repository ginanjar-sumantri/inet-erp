﻿<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.POS.CashierAccount.CashierAccountAdd, App_Web_fdftyhq7" title="Untitled Page" %>

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

    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
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
                                Cashier Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:HiddenField runat="server" ID="EmployeeNmbrHiddenField" />
                                <asp:TextBox runat="server" ID="CashierCodeTextBox" Width="120"></asp:TextBox>
                                <asp:Button ID="btnSearchEmployee" runat="server" CausesValidation="false" Text="..." />
                                <asp:RequiredFieldValidator ID="CashierCodeRequiredFieldValidator" runat="server"
                                    ErrorMessage="Cashier Code Must Be Filled" Text="*" ControlToValidate="CashierCodeTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AccountCodeTextBox" runat="server" Width="120px"></asp:TextBox>
                                <asp:DropDownList runat="server" ID="AccountDDL">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="AccountCodeRequiredFieldValidator" runat="server"
                                    ControlToValidate="AccountCodeTextBox" ErrorMessage="Account Name Must Be Filled"
                                    Text="*"></asp:RequiredFieldValidator>
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
