<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.FixedAsset.FixedAssetChangePhoto, App_Web_tvnfkugk" %>

<%@ OutputCache Duration="1" VaryByParam="none" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel DefaultButton="UploadButton" ID="Panel1" runat="server">
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
                                Fixed Asset
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="FACodeTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                &nbsp;
                                <asp:TextBox ID="FANameTextBox" runat="server" Width="300" MaxLength="50" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Preview
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:Image ID="FAPhoto" runat="server" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Photo
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:FileUpload ID="PhotoFileUpload" runat="server" />
                                <br />
                                <asp:Label ID="UploadLabel" runat="server" CssClass="tooltip"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:ImageButton ID="UploadButton" runat="server" OnClick="UploadButton_Click" Text="Upload" />
                            </td>
                            <td>
                                <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
