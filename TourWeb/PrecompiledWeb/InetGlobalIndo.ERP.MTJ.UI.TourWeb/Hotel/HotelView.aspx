<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Tour.Hotel.HotelView, App_Web_kuezhuqt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel2" DefaultButton="EditButton" runat="server">
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
                                Hotel Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="HotelCodeTextBox" Width="250" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Hotel Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="HotelNameTextBox" Width="250" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Supplier
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SupplierTextBox" runat="server" Width="250" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Product
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ProductCodeTextBox" Width="250" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Currency
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CurrCodeTextBox" runat="server" Width="250" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <fieldset>
                                    <legend>Hotel Info</legend>
                                    <table>
                                        <tr>
                                            <td valign="top">
                                                Address 1
                                            </td>
                                            <td valign="top">
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="Address1TextBox" Width="250" TextMode="MultiLine"
                                                    Height="100" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
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
                                                <asp:TextBox runat="server" ID="Address2TextBox" Width="250" TextMode="MultiLine"
                                                    Height="100" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                City
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="CityTextBox" runat="server" Width="250" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp
                                            </td>
                                            <td>
                                                Postcode
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="PostCodeTextBox" Width="250" BackColor="#CCCCCC"
                                                    ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Telephone
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="TelephoneTextBox" Width="250" BackColor="#CCCCCC"
                                                    ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp
                                            </td>
                                            <td>
                                                Fax
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="FaxTextBox" Width="250" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Email
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="EmailTextBox" Width="250" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Active
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="FgActiveCheckBox" runat="server" Enabled="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Remark
                                            </td>
                                            <td valign="top">
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="250" TextMode="MultiLine" Height="100"
                                                    BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <fieldset>
                                    <legend>Contact Info</legend>
                                    <table>
                                        <tr>
                                            <td>
                                                Contact Name
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContactPersonTextBox" runat="server" Width="250" BackColor="#CCCCCC"
                                                    ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Contact Title
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContactTitleTextBox" runat="server" Width="250" BackColor="#CCCCCC"
                                                    ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Contact Phone
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContactPhoneTextBox" runat="server" Width="250" BackColor="#CCCCCC"
                                                    ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                                &nbsp;<asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False"
                                    OnClick="CancelButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
