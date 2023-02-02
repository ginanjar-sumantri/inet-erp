<%@ Page Language="C#" MasterPageFile="~/SMSAdminMasterPage.master" AutoEventWireup="true"
    CodeFile="OrganizationSettingEdit.aspx.cs" Inherits="SMS.BackEndSMSPortal.OrganizationSetting.OrganizationSettingEdit" %>

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
                            User Limit
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="UserLimitTextBox" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserLimitValidator" runat="server" Text="*" ErrorMessage="User Limit must be filled"
                                ControlToValidate="UserLimitTextBox"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Masking SD
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="MaskingSDTextBox" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="MaskingSDValidator" runat="server" Text="*" ErrorMessage="Masking SD must be filled"
                                ControlToValidate="MaskingSDTextBox"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Hosted
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="HostedCheckBox" Text="Hosted"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="SaveButton" runat="server" Text="Update" 
                                onclick="SaveButton_Click" />
                            &nbsp;
                            <asp:Button ID="CancelButton" runat="server" Text="Cancel" OnClick="CancelButton_Click" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
