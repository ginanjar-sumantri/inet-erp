<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="ProductTypeDetailAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.ProductTypeDetailAdd" %>

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
    <asp:Panel ID="Panel1" DefaultButton="SaveButton" runat="server">
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
                                Warehouse Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="WrhsTypeDropDownList" runat="server">
                                    <asp:ListItem Text="[Choose One]" Value="null"></asp:ListItem>
                                    <asp:ListItem Text="Owner" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Deposit In" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Deposit Out" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:CustomValidator ID="WrhsTypeCustomValidator" runat="server" ErrorMessage="Warehouse Type Name Must Be Choosed"
                                    Text="*" ControlToValidate="WrhsTypeDropDownList" Display="Dynamic" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Inventory (IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccInventTextBox" Width="100">
                                </asp:TextBox>
                                <asp:DropDownList runat="server" ID="AccInventDropDownList">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="AccInventRequiredFieldValidator" runat="server" ErrorMessage="Account Inventory Must Be Filled"
                                    Text="*" ControlToValidate="AccInventTextBox" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Sales (IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccSalesTextBox" Width="100">
                                </asp:TextBox>
                                <asp:DropDownList runat="server" ID="AccSalesDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account COGS (IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccCOGSTextBox" Width="100">
                                </asp:TextBox>
                                <asp:DropDownList runat="server" ID="AccCOGSDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account WIP (IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccWIPTextBox" Width="100">
                                </asp:TextBox>
                                <asp:DropDownList runat="server" ID="AccWIPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Transit SJ (IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccTransitSJTextBox" Width="100">
                                </asp:TextBox>
                                <asp:DropDownList runat="server" ID="AccTransitSJDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Transit Wrhs (IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccTransitWrhsTextBox" Width="100">
                                </asp:TextBox>
                                <asp:DropDownList runat="server" ID="AccTransitWrhsDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Sales Retur
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccSReturTextBox" Width="100">
                                </asp:TextBox>
                                <asp:DropDownList runat="server" ID="AccSReturDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Purchase Retur
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccPReturTextBox" Width="100">
                                </asp:TextBox>
                                <asp:DropDownList runat="server" ID="AccPReturDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Transit Reject
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccTransitRejectTextBox" Width="100">
                                </asp:TextBox>
                                <asp:DropDownList runat="server" ID="AccTransitRejectDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Exp Loss
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccExpLossTextBox" Width="100">
                                </asp:TextBox>
                                <asp:DropDownList runat="server" ID="AccExpLossDropDownList">
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
