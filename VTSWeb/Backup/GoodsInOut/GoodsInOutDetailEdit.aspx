<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="GoodsInOutDetailEdit.aspx.cs" Inherits="VTSWeb.UI.GoodsInOutDetailEdit" %>

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
            <td colspan="2">
                Transaction Number
            </td>
            <td>
                :
            </td>
            <td>
                <asp:TextBox ID="TransNumbTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"
                    Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Item No
            </td>
            <td>
                :
            </td>
            <td>
                <asp:TextBox ID="ItemNoTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"
                    Width="300"></asp:TextBox>
            </td>
            <tr>
                <td>
                    Item Code
                </td>
                <td colspan="2">
                    :
                </td>
                <td>
                    <asp:TextBox ID="ItemCodeTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server" Width="300"></asp:TextBox>
                </td>
            </tr>
        </tr>
        <%--  <tr>
                                <td>
                                    Code Contact
                                </td>
                                <td colspan="2">
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="CodeContactTextBox" runat="server" Width="300"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server" ErrorMessage="NumberFile Must Be Filled"
                                        Text="*" ControlToValidate="CodeContactTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>
        <tr>
            <td>
                Product Name
            </td>
            <td colspan="2">
                :
            </td>
            <td>
                <asp:TextBox ID="ProductNameTextBox" runat="server"
                    Width="300"></asp:TextBox>
                <asp:RequiredFieldValidator ID="NameRequiredFieldValidator0" runat="server" ControlToValidate="ProductNameTextBox"
                    Display="Dynamic" ErrorMessage="Product Name Must Be Filled" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Serial Number
            </td>
            <td colspan="2">
                :
            </td>
            <td align="left">
                <asp:TextBox ID="SerialNumberTextBox" runat="server" Width="300"></asp:TextBox>
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
        <%-- <tr>
                        <td valign="top">
                            Status
                        </td>
                        <td valign="top" colspan="2">
                            :
                        </td>
                        <td>
                            <asp:CheckBox ID="StatusComplete" runat="server" />
                            &nbsp;Complete
                        </td>
                    </tr>--%>
        <tr>
            <td valign="top">
                Electri City Numerik
            </td>
            <td valign="top" colspan="2">
                :
            </td>
            <td>
                <asp:TextBox ID="ElectriCityTextBox" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <b>
                    <asp:Label runat="server" ID="WarningLabel"></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td colspan="4">
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
</asp:Content>
