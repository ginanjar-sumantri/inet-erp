<%@ Page Language="C#" MasterPageFile="~/SMSMasterPage.master" AutoEventWireup="true"
    CodeFile="ContactsUpload.aspx.cs" Inherits="SMS.SMSWeb.Message.ContactsUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAtas" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContentTitle"
    runat="Server">
    <asp:Literal ID="SubPageTitleLiteral" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
        <asp:ValidationSummary ID="ValidationSummary" runat="server" />
        <asp:Label runat="server" ID="WarningLabel" ForeColor="Red" Font-Bold="true"></asp:Label>
        <table cellpadding="3" cellspacing="0" width="0" border="0">
            <tr>
                <td colspan="3">*)Use Excell File, put the data in the first to eighth column, with the following order :<br />
                A : Name, B : Phone Number With Valid Area Code Prefix (+62 xxx ...), C : Company,<br />
                D : Date of Birth (mm/dd/yyyy),E : Religion, F : E-mail, G : City, H : Group<br />
                do not use any blank row as data separator.
                </td>
            </tr>
            <tr>
                <td>
                    File Name
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:FileUpload runat="server" ID="UploadFile" />
                    <asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server" ErrorMessage="File Upload Must Be Filled"
                        Text="*" ControlToValidate="UploadFile" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <table cellpadding="3" cellspacing="0" border="0" width="0">
            <tr>
                <td>
                    <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                </td>
                <td>
                    <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                </td>
            </tr>
        </table>
        <img src="../images/ContactUploadExcellGuide.jpg" />
    </asp:Panel>
</asp:Content>
