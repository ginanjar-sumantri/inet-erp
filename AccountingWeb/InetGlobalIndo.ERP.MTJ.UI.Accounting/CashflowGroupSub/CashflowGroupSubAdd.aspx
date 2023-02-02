<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="CashflowGroupSubAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.CashflowGroupSub.CashflowGroupSubAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="scriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
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
                                Cashflow Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:UpdatePanel ID="CashflowTypeUpdatePanel" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="CashflowTypeDDL" runat="server" OnSelectedIndexChanged="CashflowTypeDDL_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Value="Null">[Choose One]</asp:ListItem>
                                            <asp:ListItem Value="Statement">Statement</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CashflowTypeCustomValidator" runat="server" ErrorMessage="Cashflow Type Must Be Filled"
                                            Text="*" ControlToValidate="CashflowTypeDDL"></asp:CustomValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Cashflow Group
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:UpdatePanel ID="CashflowUpdatePanel" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="CfGroupDropDownList" runat="server" OnSelectedIndexChanged="CfGroupDropDownList_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CFGroupCustomValidator" runat="server" ErrorMessage="Cashflow Group Code Must Be Filled"
                                            Text="*" ControlToValidate="CfGroupDropDownList"></asp:CustomValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Cashflow group Sub Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:UpdatePanel ID="CFGrpupdatePanel" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="CFGroupCodeTextBox" runat="server" MaxLength="2" BackColor="#CCCCCC"
                                            Width="30">
                                        </asp:TextBox>
                                        <asp:TextBox runat="server" ID="CodeTextBox" Width="50" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="CodeRequiredFieldValidator" runat="server" ErrorMessage="Cashflow Group Sub Code Must Be Filled"
                                            Text="*" ControlToValidate="CodeTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Cashflow group Sub Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="NameTextBox" Width="210" MaxLength="30"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server" ErrorMessage="Cashflow Group Sub Name Must Be Filled"
                                    Text="*" ControlToValidate="NameTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Summary Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="SumTypeDropDownList" runat="server">
                                    <asp:ListItem Value="ACCOUNT">Account</asp:ListItem>
                                    <asp:ListItem Value="SALDO">Saldo</asp:ListItem>
                                </asp:DropDownList>
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
                                <asp:DropDownList ID="OperatorDropDownList" runat="server">
                                    <asp:ListItem Value="+">+</asp:ListItem>
                                    <asp:ListItem Value="-">-</asp:ListItem>
                                </asp:DropDownList>
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
