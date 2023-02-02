<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="AddGameBuffet.aspx.cs" Inherits="ITV.UI.GameBuffet.AddGameBuffet" %>

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
                Game Name
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
                Game Buffet Type
            </td>
            <td>
                :
            </td>
            <td>
                <asp:RadioButtonList ID="GBTypeRadioButtonList" runat="server" RepeatDirection="horizontal">
                    <asp:ListItem Value="A" Text="All" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="S" Text="Selected Item"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2">
                <asp:ImageButton runat="server" ID="SaveImageButton" />
                <asp:ImageButton runat="server" ID="ResetImageButton" />
                <asp:ImageButton runat="server" ID="CancelImageButton" />
            </td>
        </tr>
    </table>
</asp:Content>
