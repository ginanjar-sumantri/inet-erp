<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="ClearanceEdit.aspx.cs" Inherits="VTSWeb.UI.ClearanceEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
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
            </td>
        </tr>
        <tr>
            <td>
                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style20">
                <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="3" cellspacing="0" width="0" border="0">
                    <tr>
                        <td colspan="2" class="style16">
                            No
                        </td>
                        <td style="width: 0">
                            :
                        </td>
                        <td width="120" style="width: 60px" colspan="2">
                            <asp:TextBox ID="NoTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="style16">
                            Date
                        </td>
                        <td style="width: 0">
                            :
                        </td>
                        <td width="120" style="width: 60px" colspan="2">
                            <asp:TextBox ID="DateTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                Width="300"></asp:TextBox>
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
                            <asp:TextBox ID="VistorTextBox" BackColor="#CCCCCC" ReadOnly="true" runat="server"
                                Width="100"></asp:TextBox>
                            <asp:DropDownList ID="ContactNameDropDownList" runat="server">
                            </asp:DropDownList>
                            </asp:TextBox><asp:CustomValidator ID="ContactNameValidator" runat="server" ControlToValidate="ContactNameDropDownList"
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
                    <tr>
                        <td>
                            Status
                        </td>
                        <td colspan="2">
                            :
                        </td>
                        <td align="left" colspan="2">
                            <asp:Label runat="server" ID="StatusLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
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
