<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="ContractTemplateEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.ContractTemplate.ContractTemplateEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
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
                <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="3" cellspacing="0" width="0" border="0">
                    <tr>
                            <td>
                                Template Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TemplateNameTextBox" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="TemplateNameFieldValidator" runat="server" ControlToValidate="TemplateNameTextBox"
                                    Display="Dynamic" ErrorMessage="Template Name Must Be Filled" Text="*">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                File
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:FileUpload ID="FileNameFileUpload" runat="server" /><br />
                                <asp:Button ID="UploadButton" runat="server" Text="Upload" CausesValidation="false"
                                    OnClick="UploadButton_Click" />
                                <asp:HiddenField runat="server" ID="FileNameHiddenField" />
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Path Upload File
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:Label runat="server" ID="PathFileLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Active
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="FgActiveCheckBox" runat="server" />
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
                        <td>
                            <asp:ImageButton ID="ViewDetailButton" runat="server" CausesValidation="False" OnClick="ViewDetailButton_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="SaveAndViewDetailButton" runat="server" OnClick="SaveAndViewDetailButton_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
