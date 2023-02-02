<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="PaymentNonPurchaseDbAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.PaymentNonPurchase.PaymentNonPurchaseDbAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function Selected(_prmDDL, _prmTextBox)
        {
            if(_prmDDL.value != "null")
            {
                _prmTextBox.value = _prmDDL.value;
            }
            else{
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
                                        Account
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="AccountTextBox" Width="80" MaxLength="12"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="AccountDropDownList" AutoPostBack="true" OnSelectedIndexChanged="AccountDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="AccountRequiredFieldValidator" runat="server" ErrorMessage="Account Must Be Filled"
                                            Text="*" ControlToValidate="AccountTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:HiddenField runat="server" ID="FgSubledHiddenField" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Subled
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="SubledDropDownList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <tr>
                            <td>
                                Nominal
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="NominalTextBox" Width="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="NominalRequiredFieldValidator" runat="server" ErrorMessage="Nominal Must Be Filled"
                                    Text="*" ControlToValidate="NominalTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:HiddenField runat="server" ID="DecimalPlaceHiddenField" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Remark
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>
                                <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                characters left
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
