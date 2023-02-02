<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="GoodsInOutEdit.aspx.cs" Inherits="VTSWeb.UI.GoodsInOutEdit" %>

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
                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="WarningLabel" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="3" cellspacing="0" width="0" border="0">
                    <tr>
                        <td colspan="2">
                            Transaction Number
                        </td>
                        <td>
                            :
                        </td>
                        <td w>
                            <asp:TextBox ID="TransNumberTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                Width="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            File Number
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="NumberFileTextBox" runat="server" Width="300"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server" ErrorMessage="File Number Must Be Filled"
                                Text="*" ControlToValidate="NumberFileTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Transaction Type
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="TransTypeDropDownList" runat="server">
                                <asp:ListItem Value="In">In</asp:ListItem>
                                <asp:ListItem Value="Out">Out</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CustomValidator ID="TypeCustomValidator" runat="server" ControlToValidate="TransTypeDropDownList"
                                ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="Transaction Type Must Be Chosen"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Company Name
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="CustNameDropDownList" runat="server" OnSelectedIndexChanged="CustNameDropDownList_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustNameCustomValidator" runat="server" ControlToValidate="CustNameDropDownList"
                                ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="Company Name Must Be Chosen"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Rack
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="RackDropDownList" runat="server">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator" runat="server" ControlToValidate="RackDropDownList"
                                ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="Rack Must Be Chosen"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Transaction Date
                        </td>
                        <td colspan="2">
                        </td>
                        <td align="left">
                            <asp:TextBox ID="TransDateTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox><input
                                type="button" id="button1" value="..." onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_TransDateTextBox,'yyyy-mm-dd',this)" />
                            Time (hh-mm)
                            <asp:DropDownList ID="HHDropDownList" runat="server">
                            </asp:DropDownList>
                            <asp:DropDownList ID="MMDropDownList" runat="server">
                            </asp:DropDownList>
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
                            <asp:TextBox ID="RemarkTextBox" runat="server" Width="300" Height="100" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Status
                        </td>
                        <td valign="top" colspan="2">
                            :
                        </td>
                        <td>
                            <asp:Label ID="StatusLabel" runat="server" Text="LabelStatus" Enabled="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Carry By
                        </td>
                        <td valign="top" colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CarryByTextBox" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Requested By
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="RequestByTextBox" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Approved By
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="ApprovedByTextBox" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Posted By
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:Label ID="PostedByLabel" runat="server" Text="PostedByLabel" Enabled="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Entry Date
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="EntryDateTextBox" ReadOnly="true" runat="server" BackColor="#CCCCCC"
                                Width="90"></asp:TextBox>
                            &nbsp;
                            <asp:TextBox ID="HHEntryDateTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                Width="30">
                            </asp:TextBox>:
                            <asp:TextBox ID="MMEntryDateTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                Width="30">
                            </asp:TextBox>
                            &nbsp;WIB
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Entry User Name
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td>
                            <asp:Label ID="EntryUserLabel" runat="server" Text="EntryUserLabel" Enabled="False"></asp:Label>
                            <%--</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                                                                ErrorMessage="Email Not Valid" Text="*" ControlToValidate="EmailTextBox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="5">
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
                        <td>
                            <asp:ImageButton ID="ViewDetailButton" runat="server" CausesValidation="False" OnClick="ViewDetailButton_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
