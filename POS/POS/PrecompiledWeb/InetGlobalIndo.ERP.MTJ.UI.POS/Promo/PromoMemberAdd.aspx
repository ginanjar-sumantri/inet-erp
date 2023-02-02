<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.POS.Promo.PromoMemberAdd, App_Web_bxcwfygp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script type="text/javascript" language="javascript">
        function HarusAngka(x) { if (isNaN(x.value)) x.value = ""; }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
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
                                Promo Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="PromoCodeTextBox" width="100" MaxLength="50"
                                    BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Member Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="MemberTypeDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="MemberTypeCustomValidator" runat="server" ErrorMessage="Member Type Must Be Filled"
                                    Text="*" ControlToValidate="MemberTypeDropDownList" Display="Dynamic" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td valign="middle" class="width">
                                Amount Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:RadioButtonList ID="AmountTypeRBL" runat="server">
                                    <asp:ListItem Value="A" Text="Amount"></asp:ListItem>
                                    <asp:ListItem Value="P" Text="Percentage"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Amount
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AmountTextBox" width="100" MaxLength="15"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="AmountRequiredFieldValidator" runat="server" ErrorMessage="Amount Must Be Filled"
                                    Text="*" ControlToValidate="AmountTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="width">
                                FgGender
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="FgGenderCheckBox" runat="server" AutoPostBack="true" OnCheckedChanged="FgGenderCheckBox_CheckedChanged"
                                    Width="20px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="width">
                                Gender
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:RadioButtonList ID="GenderRBL" runat="server" RepeatDirection="Horizontal" Width="120px">
                                    <asp:ListItem Value="M" Text="Male"></asp:ListItem>
                                    <asp:ListItem Value="F" Text="Female"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="width">
                                FgActive
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="FgActiveCheckBox" runat="server" />
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
                                    TextMode="MultiLine"></asp:TextBox>
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
</asp:Content>
