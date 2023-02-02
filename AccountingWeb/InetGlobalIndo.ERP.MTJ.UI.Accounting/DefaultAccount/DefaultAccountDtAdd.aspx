<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="DefaultAccountDtAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.DefaultAccount.DefaultAccountDtAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="scriptMgr" runat="server" EnablePartialRendering="true" />
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
                            Setup Code
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="SetCodeTextBox" Width="70" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Setup Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="SetDescriptionTextBox" Width="420" BackColor="#CCCCCC"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <tr>
                                <td>
                                    Currency
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="CurrCodeDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CurrCode_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="CurrencyCustomValidator" runat="server" ErrorMessage="Currency Must Be Choosed"
                                        Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="CurrCodeDropDownList"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td align="left">
                                    <asp:Panel ID="Panel2" DefaultButton="GoImageButton" runat="server">
                                        <table cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td>
                                                    <b>Quick Search :</b>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CategoryDropDownList" runat="server">
                                                        <asp:ListItem Value="Code" Text="Account Code"></asp:ListItem>
                                                        <asp:ListItem Value="Name" Text="Account Name"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="KeywordTextBox" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="GoImageButton" runat="server" CausesValidation="False" OnClick="GoImageButton_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td colspan="5" align="right">
                                    <asp:Panel DefaultButton="DataPagerButton" ID="DataPagerPanel" runat="server">
                                        <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                            <tr>
                                                <%--<td align="right">
                                                    <table>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:CheckBox runat="server" ID="AllCheckBox" Text="Check All" />
                                                           </td>
                                                            <td>
                                                                <asp:HiddenField ID="CheckHidden" runat="server" />
                                                        <asp:HiddenField ID="TempHidden" runat="server" />
                                                            </td>
                                                            <td align="right">
                                                               
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>--%>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="DataPagerButton" runat="server" CausesValidation="false" OnClick="DataPagerButton_Click" />
                                                            </td>
                                                            <td valign="middle">
                                                                <b>Page :</b>
                                                            </td>
                                                            <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater" runat="server" OnItemCommand="DataPagerTopRepeater_ItemCommand"
                                                                OnItemDataBound="DataPagerTopRepeater_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <td>
                                                                        <asp:LinkButton ID="PageNumberLinkButton" runat="server" CausesValidation="false"></asp:LinkButton>
                                                                        <asp:TextBox Visible="false" ID="PageNumberTextBox" runat="server" Width="30px"></asp:TextBox>
                                                                    </td>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    Account
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:RadioButtonList runat="server" ID="AccountRBL" RepeatDirection="Vertical" RepeatColumns="2">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="3" cellspacing="0" border="0" width="0">
                    <tr>
                        <td>
                            <asp:ImageButton runat="server" ID="SaveButton" Text="Save" OnClick="SaveButton_Click" />
                        </td>
                        <td>
                            <asp:ImageButton runat="server" ID="CancelButton" Text="Cancel" OnClick="CancelButton_Click"
                                CausesValidation="False" />
                        </td>
                        <td>
                            <asp:ImageButton runat="server" ID="ResetButton" Text="Reset" OnClick="ResetButton_Click"
                                CausesValidation="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
