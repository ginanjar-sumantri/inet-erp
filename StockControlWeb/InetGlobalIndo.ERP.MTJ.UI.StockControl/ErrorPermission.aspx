<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="ErrorPermission.aspx.cs"
    Inherits="ErrorPermission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <table cellpadding="3" cellspacing="0" width="100%" border="0">
        <tr>
            <td style="font-size: large; color: Red">
                <b>You Have No Permission For This Page</b>
            </td>
        </tr>
    </table>
</asp:Content>
