<%@ Page Language="C#" MasterPageFile="~/SMSMasterPage.master" AutoEventWireup="true"
    CodeFile="ContactsView.aspx.cs" Inherits="SMS.SMSWeb.Message.ContactsView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAtas" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContentTitle"
    runat="Server">
    <asp:Literal ID="SubPageTitleLiteral" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
    <table cellpadding="3" cellspacing="0" border="0" width="0">
        <tr>
            <td valign="top">
                <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                <table cellpadding="3" cellspacing="0" width="0" border="0">
                                    <tr>
                                        <td>
                                            Name
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="NameTextBox" Width="250" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Phone Number
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="PhoneNumberTextBox" Width="150" MaxLength="50" ReadOnly="true"
                                                BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Company
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="CompanyTextBox" Width="250" MaxLength="50" ReadOnly="true"
                                                BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            DateOfBirth
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="DateOfBirthTextBox" Width="100" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Religion
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="ReligionTextBox" Width="150" MaxLength="50" ReadOnly="true"
                                                BackColor="#CCCCCC"></asp:TextBox>
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
                                            <asp:TextBox runat="server" ID="EmailTextBox" Width="250" MaxLength="50" ReadOnly="true"
                                                BackColor="#CCCCCC"></asp:TextBox>
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
                                            <asp:TextBox runat="server" ID="CityTextBox" Width="150" MaxLength="50" ReadOnly="true"
                                                BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            PhoneBook Group
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="PhoneBookGroupTextBox" Width="150" MaxLength="50"
                                                ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Use Birthday Wishes
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="fgBirthDayCheckBox" runat="server" Enabled="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Birthday Wishes
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="BirthdayWishesTexBox" Width="250" MaxLength="500"
                                                Height="80px" TextMode="MultiLine" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Remark
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="RemarkTextBox" Width="250" MaxLength="500" Height="80px"
                                                TextMode="MultiLine" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
