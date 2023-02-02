<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="ViewEvent.aspx.cs" Inherits="ITV.UI.MsEvent.VIewEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <table>
        <tr>
            <td>
                <asp:Literal ID="PageTitleLIteral" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                Event Name
            </td>
            <td>
                :
            </td>
            <td>
                <asp:TextBox ID="EventName" runat="server"></asp:TextBox>
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
            <td>
                Image File
            </td>
            <td>
                :
            </td>
            <td>
                <asp:FileUpload ID="ImageUpload" runat="server" />
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
                <asp:FileUpload ID="VideoUpload" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2">
                <asp:ImageButton runat="server" ID="EditImageButton" Text="Edit" />
                <asp:ImageButton runat="server" ID="BackImageButton" Text="Back" />
            </td>
        </tr>
    </table>
</asp:Content>
