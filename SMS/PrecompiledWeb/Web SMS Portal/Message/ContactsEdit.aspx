<%@ page language="C#" masterpagefile="~/SMSMasterPage.master" autoeventwireup="true" inherits="SMS.SMSWeb.Message.ContactsEdit, App_Web_tpn8tx_m" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script type="text/javascript">
        function HarusAngka(x) { if (isNaN(x.value)) x.value = ""; }
    </script>

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
                <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ForeColor="Red" runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                            </td>
                        </tr>
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
                                            <asp:TextBox runat="server" ID="NameTextBox" Width="250" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server" ErrorMessage="Name Must Be Filled"
                                                Text="*" ControlToValidate="NameTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                            <asp:DropDownList runat="server" ID="PrefixPhoneNumber">
                                            </asp:DropDownList>
                                            <asp:TextBox runat="server" ID="PhoneNumberTextBox" Width="150" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PhoneNumberRequiredFieldValidator" runat="server"
                                                ErrorMessage="Phone Number Must Be Filled" Text="*" ControlToValidate="PhoneNumberTextBox"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
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
                                            <asp:TextBox runat="server" ID="CompanyTextBox" Width="250" MaxLength="50"></asp:TextBox>
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
                                            <asp:TextBox runat="server" ID="DateOfBirthTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                            <input id="button1" type="button" onclick="displayCalendar(ctl00_ContentPlaceHolderContent_DateOfBirthTextBox,'yyyy-mm-dd',this)"
                                                value="..." />
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
                                            <asp:DropDownList runat="server" ID="ReligionDropDownList">
                                                <asp:ListItem Text="--" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Budha" Value="Budha"></asp:ListItem>
                                                <asp:ListItem Text="Hindu" Value="Hindu"></asp:ListItem>
                                                <asp:ListItem Text="Islam" Value="Islam"></asp:ListItem>
                                                <asp:ListItem Text="Katolik" Value="Katolik"></asp:ListItem>
                                                <asp:ListItem Text="Kristen" Value="Kristen"></asp:ListItem>
                                            </asp:DropDownList>
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
                                            <asp:TextBox runat="server" ID="EmailTextBox" Width="250" MaxLength="50"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                                                ControlToValidate="EmailTextBox" ErrorMessage="Email is not Valid" Text="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
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
                                            <asp:TextBox runat="server" ID="CityTextBox" Width="150" MaxLength="50"></asp:TextBox>
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
                                            <asp:TextBox runat="server" ID="PhoneBookGroupTextBox" Width="150" MaxLength="50"></asp:TextBox>
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
                                            <asp:CheckBox ID="fgBirthDayCheckBox" runat="server" />
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
                                                Height="80px" TextMode="MultiLine"></asp:TextBox>
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
                                                TextMode="MultiLine"></asp:TextBox>
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
                                            <asp:TextBox runat="server" ID="TextBox1" Width="250" MaxLength="500" Height="80px"
                                                TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            JobTitle
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="JobTitleTextBox" Width="250" MaxLength="500"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Address
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="AddressTextBox" Width="250" MaxLength="500" Height="80px"
                                                TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Name Card Picture
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:Image ID="NameCardImage" Height="170" Width="255" runat="server" BorderColor="Black"
                                                BorderStyle="Solid" ImageAlign="Baseline" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Changes Picture
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="NameCardFileUpload" runat="server" />
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
                                            <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ResetButton" runat="server" CausesValidation="False" OnClick="ResetButton_Click" />
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
