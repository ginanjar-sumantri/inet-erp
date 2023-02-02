<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="MemberView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.POS.Member.MemberView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
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
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                Member Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="MemberTypeTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                            <td>
                                Member Title
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:RadioButtonList ID="MemberTitleRadioButtonList" runat="server" RepeatDirection="Horizontal"
                                    Enabled="false">
                                    <asp:ListItem Value="0" Text="Mr" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Ms"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Mrs"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Member Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="MemberCodeTextBox" Width="150" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                            <td>
                                Gender
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:RadioButtonList ID="GenderRadioButtonList" runat="server" RepeatDirection="Horizontal"
                                    Enabled="false">
                                    <asp:ListItem Text="Male" Value="Male" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Member Barcode
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="BarcodeTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Member Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="MemberNameTextBox" Width="210" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                            <td>
                                Religion
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ReligionTextBox" Width="150" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" rowspan="3">
                                Identity
                            </td>
                            <td valign="top" rowspan="3">
                                :
                            </td>
                            <td rowspan="3">
                                <asp:RadioButtonList ID="IdentityRadioButtonList" runat="server" RepeatDirection="Horizontal"
                                    RepeatColumns="2" Enabled="false">
                                    <asp:ListItem Text="ID Card" Value="0" Selected="True"> </asp:ListItem>
                                    <asp:ListItem Text="Driver License" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Student Card" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Paspor" Value="3"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:TextBox ID="IdentityNumberTextBox" runat="server" Width="210" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                            <td>
                                Date Of Birth
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DateOfBirthTextBox" Width="100" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Place Of Birth
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td valign="top">
                                <asp:TextBox runat="server" ID="PlaceOfBirthTextBox" Width="210" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Reference
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ReferenceTextBox" Width="210" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Telephone 1
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="Telephone1TextBox" Width="210" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                            <td>
                                Telephone 2
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="Telephone2TextBox" Width="210" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Hand Phone 1
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="HandPhone1TextBox" Width="210" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                            <td>
                                Hand Phone 2
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="HandPhone2TextBox" Width="210" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" rowspan="4">
                                Address
                            </td>
                            <td valign="top" rowspan="4">
                                :
                            </td>
                            <td valign="top" rowspan="4">
                                <asp:TextBox runat="server" ID="AddressTextBox" Width="210" Height="80" ReadOnly="true"
                                    BackColor="#CCCCCC" TextMode="MultiLine"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="CityTextBox" Width="150" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                ZipCode
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ZipCodeTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="EmailTextBox" Width="210" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="CompanyTextBox" Width="210" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                            <td>
                                Hobby
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="HobbyTextBox" Width="210" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            Job Title
                                        </td>
                                        <td>
                                            Job Level
                                        </td>
                                        <td>
                                            Salary
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ListBox ID="JobTitleListBox" runat="server" Enabled="false"></asp:ListBox>
                                        </td>
                                        <td>
                                            <asp:ListBox ID="JobLevelListBox" runat="server" Enabled="false"></asp:ListBox>
                                        </td>
                                        <td>
                                            <asp:ListBox ID="SalaryListBox" runat="server" Enabled="false">
                                                <asp:ListItem Value="1" Text="< 1 juta" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="1 juta - 1,9 juta"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="2 juta - 3,9 juta"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="4 juta - 5 juta"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="5 juta - 10 juta"></asp:ListItem>
                                                <asp:ListItem Value="6" Text="10 juta - 15 juta"></asp:ListItem>
                                                <asp:ListItem Value="7" Text="> 15 juta"></asp:ListItem>
                                            </asp:ListBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Education
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="EducationTextBox" Width="150" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                            <td>
                                Source Info
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="SourceInfoTextBox" Width="210" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Status
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="StatusCheckBox" runat="server" Enabled="false" />
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                FgActive
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="FgActiveCheckBox" runat="server" Enabled="false"/>
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
                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80px" MaxLength="500"
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
</asp:Content>
