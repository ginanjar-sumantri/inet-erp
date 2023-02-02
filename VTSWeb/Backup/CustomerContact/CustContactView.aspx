<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="CustContactView.aspx.cs" Inherits="VTSWeb.UI.CustContactView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

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
                <table cellpadding="3" cellspacing="0" width="0" border="0">
                    <tr>
                        <td colspan="2">
                            Company Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CustNameTextBox" BackColor="#CCCCCC" ReadOnly="True" runat="server"
                                Width="200"> </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Item no
                        </td>
                        <td>
                            :
                        </td>
                        <td width="120">
                            <asp:TextBox ID="ItemTextBox" BackColor="#CCCCCC" ReadOnly="True" runat="server"
                                Width="100"> </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4">
                            <b>Profile</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Name
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="NameTextBox" BackColor="#CCCCCC" ReadOnly="True" runat="server"
                                Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Title
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="TitleTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="True"
                                Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Type
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="TextBox1" BackColor="#CCCCCC" ReadOnly="True" runat="server" Width="30"> </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Religion
                        </td>
                        <td colspan="2">
                        </td>
                        <td align="left">
                            <asp:TextBox ID="ReligionTextBox" BackColor="#CCCCCC" ReadOnly="True" runat="server"
                                Width="100"> </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Birthday
                        </td>
                        <td valign="top" colspan="2">
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="BirthdayTextBox" runat="server" Width="100" BackColor="#CCCCCC"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Remark
                        </td>
                        <td valign="top" colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="RemarkTextBox" BackColor="#CCCCCC" ReadOnly="True" runat="server"
                                Width="300" Height="100" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4">
                            <b>Contact</b>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Address 1
                        </td>
                        <td valign="top" colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="Address1TextBox" BackColor="#CCCCCC" ReadOnly="True" runat="server"
                                Width="300" Height="100" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Address 2
                        </td>
                        <td valign="top" colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="Address2TextBox" BackColor="#CCCCCC" ReadOnly="True" runat="server"
                                Width="300" Height="100" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Country
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="CountryTextBox" BackColor="#CCCCCC" ReadOnly="True" runat="server"
                                Width="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Zip Code
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="ZipCodeTextBox" BackColor="#CCCCCC" ReadOnly="True" runat="server"
                                Width="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Phone
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="PhoneTextBox" BackColor="#CCCCCC" ReadOnly="True" runat="server"
                                Width="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Fax
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="FaxTextBox" BackColor="#CCCCCC" ReadOnly="True" runat="server" Width="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Email
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="EmailTextBox" BackColor="#CCCCCC" ReadOnly="True" runat="server"
                                Width="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Goods In
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:CheckBox ID="FgGoodsInChecked" Enabled="False" runat="server" />&nbsp;&nbsp;&nbsp;
                            Goods Out:
                            <asp:CheckBox ID="FgGoodsOutChecked" Enabled="False" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Access
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:CheckBox ID="FgAccessChecked" Enabled="False" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Additional Visitor
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:CheckBox ID="FgAdditionalVisitorChecked" Enabled="False" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Contact Authorization
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:CheckBox ID="FgContactAuthorizationChecked" Enabled="False" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Card ID
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CardIDTextBox" BackColor="#CCCCCC" ReadOnly="True" runat="server"
                                Width="200"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <tr>
                    <td colspan="5">
                        <table cellpadding="3" cellspacing="0" border="0" width="0">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" OnClick="EditButton_Click" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </td>
        </tr>
    </table>
</asp:Content>
