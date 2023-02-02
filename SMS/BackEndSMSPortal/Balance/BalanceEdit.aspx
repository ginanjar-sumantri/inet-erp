<%@ Page Language="C#" MasterPageFile="~/SMSAdminMasterPage.master" AutoEventWireup="true"
    CodeFile="BalanceEdit.aspx.cs" Inherits="SMS.BackEndSMSPortal.Balance.BalanceEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">
    function HarusAngka(x) { if (isNaN(x.value)) x.value = "0"; }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContentTitle"
    runat="Server">
    BALANCE EDIT
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
    <table width="100%">        
        <tr>
            <td>
                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                <asp:Label ID="WarningLabel" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="15px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            Organization Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="OrganizationNameTextBox" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Current Account Balance
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="AccountBalanceTextBox" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Account Balance Changes
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="AccountBalanceChangeTextBox" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="AccountBalanceValidator" runat="server" Text="*" ErrorMessage="Account Balance must be filled"
                                ControlToValidate="AccountBalanceTextBox"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Description
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="DescriptionTextBox" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="IncreaseButton" runat="server" Text="Increase" 
                                onclick="IncreaseButton_Click" />
                            &nbsp;
                            <asp:Button ID="DecreaseButton" runat="server" Text="Decrease" 
                                onclick="DecreaseButton_Click" />
                            &nbsp;
                            <asp:Button ID="CancelButton" runat="server" Text="Cancel" OnClick="CancelButton_Click" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
