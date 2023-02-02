<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="ClearanceAdd.aspx.cs" Inherits="VTSWeb.UI.ClearanceAdd" %>

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
                                <tr>
                                    <td colspan="2">
                                        No
                                    </td>
                                    <td style="width: 0">
                                        :
                                    </td>
                                    <td width="120" style="width: 60px" colspan="2">
                                        <asp:TextBox ID="NoTextBox" runat="server" Width="300"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Date
                                    </td>
                                    <td style="width: 0">
                                        :
                                    </td>
                                    <td width="120" style="width: 60px" colspan="2">
                                        <asp:TextBox ID="DateTextBox" runat="server" Width="300"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Company
                                    </td>
                                    <td colspan="2">
                                        :
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:DropDownList ID="CustomerDropDownList" runat="server" OnSelectedIndexChanged="CustomerDropDownList_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CustomerCustomValidator" runat="server" ControlToValidate="CustomerDropDownList"
                                            ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="Company Must Be Chosen"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;Visitor
                                    </td>
                                    <td colspan="2">
                                        :
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:DropDownList ID="ContactNameDropDownList" runat="server" OnSelectedIndexChanged="ContactNameDropDownList_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="ContactNameValidator" runat="server" ControlToValidate="ContactNameDropDownList"
                                            ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="Visitor Must Be Chosen"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Remark
                                    </td>
                                    <td colspan="2">
                                        :
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:TextBox ID="RemarkTextBox" runat="server" Width="300" Height="100" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <%-- <tr>
                <td>
                    Complete</td><td colspan="2">
                    :
                </td>
               <td align=left colspan="2">
                    <asp:CheckBox ID="CompleteChecked" ReadOnly="true" BackColor="#CCCCCC" runat="server" /></td></tr>--%>
                            </table>
                            <table cellpadding="3" cellspacing="0" border="0" width="0">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="NextButton" runat="server" OnClick="NextButton_Click" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />&nbsp;
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
            <td>
                <asp:Panel runat="server" ID="Panel1">
                    <fieldset>
                        <table>
                            <tr>
                                <td align="center" colspan="2">
                                    <b>Visitor Information</b><br />
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Image ID="PhotoImage" runat="server" />
                                    <asp:Image ID="NoPhotoImage" runat="server" /><br />
                                    <asp:Label ID="NoPhotoLabel" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td colspan="2">
                                                ID Card
                                            </td>
                                            <td style="width: 0">
                                                :
                                            </td>
                                            <td width="120" style="width: 60px" colspan="2">
                                                <asp:Label ID="IDCardLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                Phone
                                            </td>
                                            <td style="width: 0">
                                                :
                                            </td>
                                            <td width="120" style="width: 60px" colspan="2">
                                                <asp:Label ID="PhoneLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="5">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Access
                                            </td>
                                            <td colspan="2">
                                                :
                                            </td>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="AccessLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Good In
                                            </td>
                                            <td colspan="2">
                                                :
                                            </td>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="GoodInLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Good Out
                                            </td>
                                            <td colspan="2">
                                                :
                                            </td>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="GoodOutLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Additional Visitor
                                            </td>
                                            <td colspan="2">
                                                :
                                            </td>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="AdditionalVisitorLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Contact Authorization
                                            </td>
                                            <td colspan="2">
                                                :
                                            </td>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="ContactAuthorizationLabel" runat="server"></asp:Label>
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
