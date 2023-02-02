<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="FAPurchaseDetailEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.FAPurchase.FAPurchaseDetailEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
    function Count(_prmPriceForex, _prmQty, _prmAmountForex, _prmDecimalPlace)
    {
        var _amountForex = parseFloat(GetCurrency2(_prmPriceForex.value, _prmDecimalPlace.value)) * parseFloat(_prmQty.value);
        _prmAmountForex.value = (_amountForex == 0) ? "0" : FormatCurrency2(_amountForex, _prmDecimalPlace.value);
        _prmPriceForex.value = (_prmPriceForex.value == 0) ? "0" : FormatCurrency2(_prmPriceForex.value, _prmDecimalPlace.value);
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
                        <%--<tr>
                            <td>
                                Fixed Asset Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="FACodeTextBox" Width="150" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="FACodeRequiredFieldValidator" runat="server" ErrorMessage="FA Code Must Be Filled"
                                    Text="*" ControlToValidate="FACodeTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                Fixed Asset Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="FANameTextBox" runat="server" Width="200" MaxLength="80" BackColor="#CCCCCC">
                                </asp:TextBox>
                                <%-- <asp:RequiredFieldValidator ID="FANameRequiredFieldValidator" runat="server" ErrorMessage="FA Name Must Be Filled"
                                    Text="*" ControlToValidate="FANameTextBox" Display="Dynamic">
                                </asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fixed Asset Condition
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="FAStatusDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="FAStatusCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="FA Status Must Be Filled" Text="*" ControlToValidate="FAStatusDropDownList">
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fixed Asset Owner
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="FAOwnerCheckBox" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fixed Asset Sub Group
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="FASubGroupDropDownList" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="FASubGroupDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="FASubGroupCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="FA SubGroup Must Be Filled" Text="*" ControlToValidate="FASubGroupDropDownList">
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Location Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="FALocationTypeDropDownList" AutoPostBack="True"
                                    OnSelectedIndexChanged="FALocationTypeDropDownList_SelectedIndexChanged">
                                    <asp:ListItem Value="null">[Choose Item]</asp:ListItem>
                                    <asp:ListItem Value="GENERAL">GENERAL</asp:ListItem>
                                    <asp:ListItem Value="EMPLOYEE">EMPLOYEE</asp:ListItem>
                                    <asp:ListItem Value="CUSTOMER">CUSTOMER</asp:ListItem>
                                    <asp:ListItem Value="SUPPLIER">SUPPLIER</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Location
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="FALocationDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="FALocationCustomValidator" runat="server" ErrorMessage="Fixed Asset Location Must Be Filled"
                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="FALocationDropDownList"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Life Month
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="LifeMonthTextBox" runat="server" Width="50">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="LifeMonthRequiredFieldValidator" runat="server" ErrorMessage="Life Month Must Be Filled"
                                    Text="*" ControlToValidate="LifeMonthTextBox" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Price Forex
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PriceForexTextBox" runat="server" Width="110" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Qty
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="QtyTextBox" runat="server" Width="50">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Qty Must Be Filled"
                                    Text="*" ControlToValidate="QtyTextBox" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Amount Forex
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AmountForexTextBox" runat="server" Width="110">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="AmountForexRequiredFieldValidator" runat="server"
                                    ErrorMessage="Amount Forex Must Be Filled" Text="*" ControlToValidate="AmountForexTextBox"
                                    Display="Dynamic">
                                </asp:RequiredFieldValidator>
                                <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Status Process
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="StatusProcessCheckBox" Enabled="true" Checked="false" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Specification
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SpecificationTextBox" runat="server" Width="300px" Height="80px"
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
