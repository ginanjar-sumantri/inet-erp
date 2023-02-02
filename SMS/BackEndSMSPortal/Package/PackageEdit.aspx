<%@ Page Language="C#" MasterPageFile="~/SMSAdminMasterPage.master" AutoEventWireup="true"
    CodeFile="PackageEdit.aspx.cs" Inherits="SMS.BackEndSMSPortal.Package.PackageEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script language="javascript">
    function HarusAngka(x) {if (isNaN(x.value)) x.value = "0";}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContentTitle"
    runat="Server">
    MANAGE PACKAGE EDIT
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
                            Package Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="PackageNameTextBox" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PackageNameValidator" runat="server" Text="*"
                                ErrorMessage="Package Name must be filled" ControlToValidate="PackageNameTextBox"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            SMS Per Day
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="SMSPerDayTextBox" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="SMSPerDayValidator" runat="server" Text="*" ErrorMessage="SMS Per Day must be filled"
                                ControlToValidate="SMSPerDayTextBox"></asp:RequiredFieldValidator>
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
                            <asp:TextBox ID="DescriptionTextBox" TextMode="MultiLine" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:ImageButton runat="server" ID="SaveImageButton" 
                                onclick="SaveImageButton_Click" />
                            <asp:ImageButton runat="server" ID="CancelImageButton"
                                onclick="CancelImageButton_Click" 
                                CausesValidation="false"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
