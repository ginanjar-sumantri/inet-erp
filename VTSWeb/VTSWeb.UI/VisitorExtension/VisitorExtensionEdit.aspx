<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="VisitorExtensionEdit.aspx.cs" Inherits="VTSWeb.UI.VisitorExtensionEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <table>
        <tr>
            <td class="tahoma_14_black">
                <b>
                    <asp:Literal ID="PageTitleLiteral" runat="server" /></b>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="WarningLabel" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="3" cellspacing="0" width="0" border="0">
                    <tr>
                        <td>
                            Company Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CustNameTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                MaxLength="5" Width="250" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Contact&nbsp; Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="ContactNameTextBox" ReadOnly="true" BackColor="#CCCCCC" MaxLength="5" Width="250" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            Foto
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Image ID="PictureImage" runat="server" />
                            <br />
                            <asp:FileUpload ID="FotoUpLoad" runat="server" />
                            <%--<asp:RegularExpressionValidator ID="FileUpLoadValidator" runat="server" ErrorMessage="Upload Jpgs and Gifs only."
                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|.GIF)$"
                                ControlToValidate="FotoUpLoad">
                            </asp:RegularExpressionValidator>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Foto Must Be Filled"
                                Text="*" ControlToValidate="FotoUpLoad" Display="Dynamic"></asp:RequiredFieldValidator><br />
                            <asp:Label ID="UploadLabel" runat="server" CssClass="tooltip"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
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
            </td>
        </tr>
    </table>
</asp:Content>
