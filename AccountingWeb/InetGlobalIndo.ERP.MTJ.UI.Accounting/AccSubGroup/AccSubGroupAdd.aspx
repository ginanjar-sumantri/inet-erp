<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="AccSubGroupAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.AccSubGroup.AccSubGroupAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function SelectedAccGroup(_prmDDL, _prmTextBox)
        {
            if(_prmDDL.value != "null")
            {
                _prmTextBox.value = _prmDDL.value;
            }
            else{
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
                        <tr>
                            <td>
                                Account Sub Group Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AccGroupCodeTextBox" runat="server" BackColor="#CCCCCC" Width="30"
                                    MaxLength="2"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccSubGroupCodeTextBox" Width="50" MaxLength="2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="AccSubGroupCodeRequiredFieldValidator" runat="server"
                                    ErrorMessage="Account Sub Group Code Must Be Filled" Text="*" ControlToValidate="AccSubGroupCodeTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Sub Group Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccSubGroupNameTextBox" Width="280" MaxLength="40"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="AccSubGroupNameRequiredFieldValidator" runat="server"
                                    ErrorMessage="Account Sub Group Name Must Be Filled" Text="*" ControlToValidate="AccSubGroupNameTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Group
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="AccGroupDDL">
                                </asp:DropDownList>
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
                                <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" Style="height: 28px" />
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
