<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="CustomerGroupAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.CustomerGroup.CustomerGroupAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function DoEnableOrDisable(_prmCheckBox, _prmTextBox)
        {
            if(_prmCheckBox.checked == true)
            {
                _prmTextBox.readOnly = false;
                _prmTextBox.style.background = '#FFFFFF';
            }
            else
            {
                _prmTextBox.readOnly = true;
                _prmTextBox.value = "0";
                _prmTextBox.style.background = '#CCCCCC';
            }
        }
        
        function Format(_prmTextBox)
        {
            _prmTextBox.value = (_prmTextBox.value == 0 ? "0" : FormatCurrency(_prmTextBox.value));
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
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
                                Customer Group Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CustGroupCodeTextBox" Width="100" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="CustGroupCodeRequiredFieldValidator" runat="server"
                                    ErrorMessage="Code Must Be Filled" Text="*" ControlToValidate="CustGroupCodeTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Customer Group Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CustGroupNameTextBox" Width="150" MaxLength="30"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="CustGroupNameRequiredFieldValidator" runat="server"
                                    ErrorMessage="Description Must Be Filled" Text="*" ControlToValidate="CustGroupNameTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
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
                                    <asp:ListItem Selected="True" Text="LOKAL" Value="LOKAL"></asp:ListItem>
                                    <asp:ListItem Text="EXPORT" Value="EXPORT"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                PKP
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="FgPKPCheckBox" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                PPh
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="FgPPhCheckBox" />
                                <asp:TextBox runat="server" ID="PPhTextBox" Width="50" BackColor="#CCCCCC"></asp:TextBox>%
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
