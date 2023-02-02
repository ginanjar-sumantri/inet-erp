<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="GoodsInOutAdd.aspx.cs" Inherits="VTSWeb.UI.GoodsInOutAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <table>
        <tr>
            <td>
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
                                <%--<tr>
                        <td colspan="2">
                            Transaction Number</td>
                        <td>
                            :
                        </td>
                        <td w>
                            <asp:TextBox ID="NumberTransTextBox" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>--%>
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
                                        <asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server" ErrorMessage="Number File Must Be Filled"
                                            Text="*" ControlToValidate="NumberFileTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                        <asp:DropDownList ID="CustNameDropDownList" OnSelectedIndexChanged="CustNameDropDownList_SelectedIndexChanged"
                                            AutoPostBack="True" runat="server">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CustNameCustomValidator" runat="server" ControlToValidate="CustNameDropDownList"
                                            ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="Company Name Must Be Chosen"></asp:CustomValidator>
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
                                        <asp:DropDownList ID="TypeDropDownList" runat="server" OnSelectedIndexChanged="TypeDropDownList_SelectedIndexChanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="">[Choose One]</asp:ListItem>
                                            <asp:ListItem Value="In">In</asp:ListItem>
                                            <asp:ListItem Value="Out">Out</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="TypeCustomValidator" runat="server" ControlToValidate="TypeDropDownList"
                                            ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="Trans Type Must Be Chosen"></asp:CustomValidator>
                                        <asp:HiddenField ID="TypeInHiddenField" runat="server" />
                                        <asp:HiddenField ID="TypeOutHiddenField" runat="server" />
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
                                        :
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
                                        <asp:Label ID="PostedByLabel" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <table cellpadding="3" cellspacing="0" border="0" width="0">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="NextButton" runat="server" OnClick="NextButton_Click" />
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
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <asp:Panel runat="server" ID="Panel1" Height="186px">
                    <fieldset>
                        <table>
                            <tr>
                                <td align="center">
                                    <b>Contact Goods In Out Permission</b><br />
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Name</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>ID Card Number</b>
                                            </td>
                                            <%--<td style="width: 18px" class="tahoma_11_white" align="center">
                                                <b>In</b>
                                            </td>
                                            <td style="width: 18px" class="tahoma_11_white" align="center">
                                                <b>Out</b>
                                            </td>--%>
                                        </tr>
                                        <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                                            <ItemTemplate>
                                                <tr id="RepeaterItemTemplate" runat="server">
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="ContactNameLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="IDCardLiteral"></asp:Literal>
                                                    </td>
                                                    <%--<td align="left">
                                                        <asp:Literal runat="server" ID="InLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="OutLiteral"></asp:Literal>
                                                    </td>--%>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr class="bgcolor_gray">
                                            <td style="width: 1px" colspan="3">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
