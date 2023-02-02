<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="CustomerReturDetailAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.CustomerRetur.CustomerReturDetailAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function Count(_prmQty, _prmPrice, _prmAmount, _prmDecimalPlace) {
            var _amount = parseInt(_prmQty.value) * parseFloat(GetCurrency2(_prmPrice.value, _prmDecimalPlace.value));
            _prmAmount.value = (_amount == 0 ? "0" : FormatCurrency2(_amount, _prmDecimalPlace.value));
            _prmPrice.value = (_prmPrice.value == 0) ? "0" : FormatCurrency2(_prmPrice.value, _prmDecimalPlace.value);
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
                        <asp:ScriptManager ID="scriptMgr" runat="server" EnablePartialRendering="true" />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <tr>
                                    <td>
                                        RR No
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:HiddenField runat="server" ID="TransNmbrHiddenField" />
                                        <asp:DropDownList runat="server" ID="RRNoDropDownList">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="RRNoCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                            ErrorMessage="RR No Must Be Filled" Text="*" ControlToValidate="RRNoDropDownList">
                                        </asp:CustomValidator>
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
