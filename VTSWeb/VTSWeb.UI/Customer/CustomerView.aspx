<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="CustomerView.aspx.cs" Inherits="VTSWeb.UI.CustomerView" %>

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
                <table cellpadding="3" cellspacing="0" width="0" border="0">
                    <tr>
                        <td>
                            Company Code
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CustomerCodeTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                MaxLength="5" Width="250"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Company Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CustomerNameTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                Width="250" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            Type
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="TypeTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                MaxLength="5" Width="250"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table cellpadding="3" cellspacing="0" width="0" border="0">
                    <tr>
                        <td width="120" colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="3">
                            <b>Company Info</b>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                    </tr>
                    <tr>
                        <td valign="top">
                            Address 1
                        </td>
                        <td valign="top">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CustomerAddressTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                Width="300" Height="100" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Address 2
                        </td>
                        <td valign="top">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CustomerAddress2TextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                Width="300" Height="100" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            City
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="CityTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                MaxLength="5" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Zip Code
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CustomerZipCodeTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Phone
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CustomerPhoneTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Fax
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CustomerFaxTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style15">
                            Email
                        </td>
                        <td class="style15">
                            :
                        </td>
                        <td class="style15">
                            <asp:TextBox ID="CustomerEmailTextBox" readonly="true" backcolor="#CCCCCC" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style14" colspan="3">
                            <b>Contact Info</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;Email
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CustomerContactMailTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CustomerContNameTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;Title
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CustomerContTitleTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;Hp
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CustomerContHpTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            FgActive
                        </td>
                        <td valign="top">
                            :
                        </td>
                        <td>
                            <asp:CheckBox ID="CustomerFgActiveChecked" Enabled="False" runat="server" />
                        </td>
                    </tr>
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
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
