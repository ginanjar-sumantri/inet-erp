<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="Role.aspx.cs" Inherits="ITV.UI.Role.Role" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <asp:Label ID="WarningLabel" runat="server"></asp:Label>
    <fieldset>
        <legend>Add / Edit</legend>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    Role Name
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:HiddenField ID="RoleIDHiddenField" runat="server" />
                    <asp:TextBox ID="RoleNameTextBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    System
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:CheckBox ID="SystemCheckBox" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="SaveButton_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <asp:Button ID="EditButton" runat="server" Text="Edit" 
        onclick="EditButton_Click" />
    <asp:DropDownList ID="RoleDropDownList" runat="server">
    </asp:DropDownList>
</asp:Content>
