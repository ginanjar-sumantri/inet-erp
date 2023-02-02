<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="PettyTypeAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.PettyType.PettyTypeAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function SelectedAccount(_prmDDL, _prmTextBox)
        {
            if(_prmDDL.value != "null")
            {
                _prmTextBox.value = _prmDDL.value;
            }
            else{
                _prmTextBox.value = "";
            }
        }
    
        function BlurAccountCode(_prmDDL, _prmTextBox)
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
                    Petty Type Code
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="CodeTextBox" Width="70" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CodeRequiredFieldValidator" runat="server" ErrorMessage="Petty Type Code Must Be Filled"
                        Text="*" ControlToValidate="CodeTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Petty Type Name
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="NameTextBox" Width="150" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server" ErrorMessage="Petty Type Name Must Be Filled"
                        Text="*" ControlToValidate="NameTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
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
                    <asp:TextBox ID="AccountCodeTextBox" runat="server" MaxLength="12" Width="80"></asp:TextBox>
                    <asp:DropDownList runat="server" ID="PettyDDL">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="AccountRequiredFieldValidator" runat="server" ErrorMessage="Account Must Be Filled"
                        Text="*" ControlToValidate="AccountCodeTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <%--<tr>
            <td>
                <asp:CheckBox runat="server" ID="CheckBox1" Text="Fg Report" />
            </td>
        </tr>--%>
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
