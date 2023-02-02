<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="WrhsAreaView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.WarehouseArea.WrhsAreaView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="EditButton" runat="server">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
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
                                Warehouse Area Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="WrhsAreaCodeTextBox" Width="100" MaxLength="10" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Warehouse Area Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="WrhsAreaNameTextBox" Width="350" MaxLength="50" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Address 1
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="Address1TextBox" TextMode="MultiLine" Width="200"
                                    Height="50" MaxLength="60" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
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
                                <asp:TextBox runat="server" ID="Address2TextBox" TextMode="MultiLine" Width="200"
                                    Height="50" MaxLength="60" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
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
                                <asp:TextBox runat="server" ID="ZipCodeTextBox" MaxLength="10" ReadOnly="true" BackColor="#CCCCCC" Width="70">
                                </asp:TextBox>
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
                                <asp:TextBox runat="server" ID="PhoneTextBox" MaxLength="30" ReadOnly="true" BackColor="#CCCCCC" Width="210">
                                </asp:TextBox>
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
                                <asp:TextBox runat="server" ID="FaxTextBox" MaxLength="30" ReadOnly="true" BackColor="#CCCCCC" Width="210">
                                </asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Contact Detail</legend>
                        <table width="0" cellpadding="3" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    Contact Person
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="ContactPersonTextBox" MaxLength="40" ReadOnly="true"
                                        BackColor="#CCCCCC" Width="280">
                                    </asp:TextBox>
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
                                    <asp:TextBox runat="server" ID="ContactTitleTextBox" MaxLength="30" ReadOnly="true"
                                        BackColor="#CCCCCC" Width="210">
                                    </asp:TextBox>
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
                                    <asp:TextBox runat="server" ID="ContactPhoneTextBox" MaxLength="30" ReadOnly="true"
                                        BackColor="#CCCCCC" Width="210">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Contact Email
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="ContactEmailTextBox" MaxLength="40" Width="280" ReadOnly="true"
                                        BackColor="#CCCCCC">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                            <%--</td>
                            <td>--%>
                                &nbsp;<asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
