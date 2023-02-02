<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="FixedAssetMovingEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.FixedAssetMoving.FixedAssetMovingEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
        <table cellpadding="3" cellspacing="0" width="0" border="0">
            <tr>
                <td class="tahoma_14_black" colspan="3">
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
                                Trans No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="NoTextBox" Width="150" MaxLength="20" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                File No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="FileNmbrTextBox" Width="150" MaxLength="20" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Trans Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <%--<input id="button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />--%>
                                    <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Source
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="SourceTypeDropDownList" AutoPostBack="True"
                                    OnSelectedIndexChanged="SourceTypeDropDownList_SelectedIndexChanged">
                                    <asp:ListItem Value="null">[Choose Item]</asp:ListItem>
                                    <asp:ListItem Value="GENERAL">GENERAL</asp:ListItem>
                                    <asp:ListItem Value="EMPLOYEE">EMPLOYEE</asp:ListItem>
                                    <asp:ListItem Value="CUSTOMER">CUSTOMER</asp:ListItem>
                                    <asp:ListItem Value="SUPPLIER">SUPPLIER</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList runat="server" ID="SourceDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="SourceCustomValidator" runat="server" ErrorMessage="Source Must Be Choosed"
                                    Text="*" ControlToValidate="SourceDropDownList" Display="Dynamic"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Destination
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="DestinationTypeDropDownList" AutoPostBack="True"
                                    OnSelectedIndexChanged="DestinationTypeDropDownList_SelectedIndexChanged">
                                    <asp:ListItem Value="null">[Choose Item]</asp:ListItem>
                                    <asp:ListItem Value="GENERAL">GENERAL</asp:ListItem>
                                    <asp:ListItem Value="EMPLOYEE">EMPLOYEE</asp:ListItem>
                                    <asp:ListItem Value="CUSTOMER">CUSTOMER</asp:ListItem>
                                    <asp:ListItem Value="SUPPLIER">SUPPLIER</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList runat="server" ID="DestinationDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Destination Must Be Choosed"
                                    Text="*" ControlToValidate="DestinationDropDownList" Display="Dynamic"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Operator
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="OperatorTextBox" Width="280" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Remark
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>
                                <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                characters left
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
                            <td>
                                <asp:ImageButton ID="ViewDetailButton" runat="server" CausesValidation="False" OnClick="ViewDetailButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="SaveAndViewDetailButton" runat="server" OnClick="SaveAndViewDetailButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
