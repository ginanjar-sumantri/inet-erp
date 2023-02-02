<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="AddHotelOutlet.aspx.cs" Inherits="ITV.UI.HotelOutlet.AddHotelOutlet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <table cellpadding="3" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Label ID="WarningLabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table cellpadding="3" cellspacing="0">
                    <tr>
                        <td>
                            Hotel Outlet Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="HotelOutletNameTextBox" runat="server"></asp:TextBox>
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
                            <asp:TextBox ID="DescTextBox" runat="server" TextMode="MultiLine" Height="30px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Video File
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:FileUpload ID="VideoFileUpload" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Image File
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:FileUpload ID="ImageFileUpload" runat="server" />
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
                            <asp:ImageButton ID="SaveImageButton" runat="server" OnClick="SaveImageButton_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ResetImageButton" runat="server" OnClick="ResetImageButton_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="BackImageButton" runat="server" OnClick="BackImageButton_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
