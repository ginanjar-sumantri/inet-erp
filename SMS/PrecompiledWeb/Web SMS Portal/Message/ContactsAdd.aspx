<%@ page language="C#" masterpagefile="~/SMSMasterPage.master" autoeventwireup="true" inherits="SMS.SMSWeb.Message.ContactsAdd, App_Web_gbqxtdk2" %>

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
                                <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
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
                                                <%--<asp:ListItem Value="+1" Text="+1"></asp:ListItem>
                                                <asp:ListItem Value="+20" Text="+20"></asp:ListItem>
                                                <asp:ListItem Value="+21" Text="+21"></asp:ListItem>
                                                <asp:ListItem Value="+22" Text="+22"></asp:ListItem>
                                                <asp:ListItem Value="+23" Text="+23"></asp:ListItem>
                                                <asp:ListItem Value="+24" Text="+24"></asp:ListItem>
                                                <asp:ListItem Value="+25" Text="+25"></asp:ListItem>
                                                <asp:ListItem Value="+26" Text="+26"></asp:ListItem>
                                                <asp:ListItem Value="+27" Text="+27"></asp:ListItem>
                                                <asp:ListItem Value="+29" Text="+29"></asp:ListItem>
                                                <asp:ListItem Value="+30" Text="+30"></asp:ListItem>
                                                <asp:ListItem Value="+31" Text="+31"></asp:ListItem>
                                                <asp:ListItem Value="+32" Text="+32"></asp:ListItem>
                                                <asp:ListItem Value="+33" Text="+33"></asp:ListItem>
                                                <asp:ListItem Value="+34" Text="+34"></asp:ListItem>
                                                <asp:ListItem Value="+35" Text="+35"></asp:ListItem>
                                                <asp:ListItem Value="+36" Text="+36"></asp:ListItem>
                                                <asp:ListItem Value="+37" Text="+37"></asp:ListItem>
                                                <asp:ListItem Value="+38" Text="+38"></asp:ListItem>
                                                <asp:ListItem Value="+39" Text="+39"></asp:ListItem>
                                                <asp:ListItem Value="+40" Text="+40"></asp:ListItem>
                                                <asp:ListItem Value="+41" Text="+41"></asp:ListItem>
                                                <asp:ListItem Value="+42" Text="+42"></asp:ListItem>
                                                <asp:ListItem Value="+43" Text="+43"></asp:ListItem>
                                                <asp:ListItem Value="+44" Text="+44"></asp:ListItem>
                                                <asp:ListItem Value="+45" Text="+45"></asp:ListItem>
                                                <asp:ListItem Value="+46" Text="+46"></asp:ListItem>
                                                <asp:ListItem Value="+47" Text="+47"></asp:ListItem>
                                                <asp:ListItem Value="+48" Text="+48"></asp:ListItem>
                                                <asp:ListItem Value="+49" Text="+49"></asp:ListItem>
                                                <asp:ListItem Value="+50" Text="+50"></asp:ListItem>
                                                <asp:ListItem Value="+51" Text="+51"></asp:ListItem>
                                                <asp:ListItem Value="+52" Text="+52"></asp:ListItem>
                                                <asp:ListItem Value="+53" Text="+53"></asp:ListItem>
                                                <asp:ListItem Value="+54" Text="+54"></asp:ListItem>
                                                <asp:ListItem Value="+55" Text="+55"></asp:ListItem>
                                                <asp:ListItem Value="+56" Text="+56"></asp:ListItem>
                                                <asp:ListItem Value="+57" Text="+57"></asp:ListItem>
                                                <asp:ListItem Value="+58" Text="+58"></asp:ListItem>
                                                <asp:ListItem Value="+59" Text="+59"></asp:ListItem>
                                                <asp:ListItem Value="+60" Text="+60"></asp:ListItem>
                                                <asp:ListItem Value="+61" Text="+61"></asp:ListItem>
                                                <asp:ListItem Value="+62" Text="+62"></asp:ListItem>
                                                <asp:ListItem Value="+63" Text="+63"></asp:ListItem>
                                                <asp:ListItem Value="+64" Text="+64"></asp:ListItem>
                                                <asp:ListItem Value="+65" Text="+65"></asp:ListItem>
                                                <asp:ListItem Value="+66" Text="+66"></asp:ListItem>
                                                <asp:ListItem Value="+67" Text="+67"></asp:ListItem>
                                                <asp:ListItem Value="+68" Text="+68"></asp:ListItem>
                                                <asp:ListItem Value="+69" Text="+69"></asp:ListItem>
                                                <asp:ListItem Value="+7" Text="+7"></asp:ListItem>
                                                <asp:ListItem Value="+80" Text="+80"></asp:ListItem>
                                                <asp:ListItem Value="+81" Text="+81"></asp:ListItem>
                                                <asp:ListItem Value="+82" Text="+82"></asp:ListItem>
                                                <asp:ListItem Value="+84" Text="+84"></asp:ListItem>
                                                <asp:ListItem Value="+85" Text="+85"></asp:ListItem>
                                                <asp:ListItem Value="+86" Text="+86"></asp:ListItem>
                                                <asp:ListItem Value="+87" Text="+87"></asp:ListItem>
                                                <asp:ListItem Value="+88" Text="+88"></asp:ListItem>
                                                <asp:ListItem Value="+90" Text="+90"></asp:ListItem>
                                                <asp:ListItem Value="+91" Text="+91"></asp:ListItem>
                                                <asp:ListItem Value="+92" Text="+92"></asp:ListItem>
                                                <asp:ListItem Value="+93" Text="+93"></asp:ListItem>
                                                <asp:ListItem Value="+94" Text="+94"></asp:ListItem>
                                                <asp:ListItem Value="+95" Text="+95"></asp:ListItem>
                                                <asp:ListItem Value="+96" Text="+96"></asp:ListItem>
                                                <asp:ListItem Value="+97" Text="+97"></asp:ListItem>
                                                <asp:ListItem Value="+98" Text="+98"></asp:ListItem>
                                                <asp:ListItem Value="+99" Text="+99"></asp:ListItem>--%>
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
                                            JobTitle
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="JobTitleTextBox" Width="250" MaxLength="500" ></asp:TextBox>
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
