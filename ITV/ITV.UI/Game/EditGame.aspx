<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="EditGame.aspx.cs" Inherits="ITV.UI.Game.EditGame" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <table cellpadding="3" cellspacing="0" width="100%">
        <tr>
            <td colspan="3">
                <table cellpadding="3" cellspacing="0">
                    <tr>
                        <td>
                            Game Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="GameNameTextBox" runat="server"></asp:TextBox>
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
                            Game File
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="GameFileTextBox" runat="server"></asp:TextBox>
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
                            <asp:TextBox ID="ImageFileText" runat="server"></asp:TextBox>
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
                            <asp:ImageButton ID="SaveImageButton" runat="server" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ResetImageButton" runat="server" />
                        </td>
                        <td>
                            <asp:ImageButton ID="BackImageButton" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
