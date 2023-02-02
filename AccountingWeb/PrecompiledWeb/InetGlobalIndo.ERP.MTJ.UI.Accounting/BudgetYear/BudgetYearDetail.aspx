<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.BudgetYear.BudgetYearDetail, App_Web_eew1p9ls" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script type="text/javascript" src="../calendar/JScript.js"></script>

    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <asp:Panel ID="ReasonPanel" runat="server" Visible="false">
                <tr>
                    <td>
                        <fieldset>
                            <legend>Reason</legend>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Insert Reason UnPosting
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="ReasonTextBox" runat="server" MaxLength="500" Width="400px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="ReasonRequiredFieldValidator" runat="server" Text="*"
                                            ErrorMessage="Reason Text Box Must Be Filled" ControlToValidate="ReasonTextBox"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="YesButton" runat="server" Text="Yes" OnClick="YesButton_OnClick" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="NoButton" runat="server" Text="No" OnClick="NoButton_OnClick" CausesValidation="False" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </asp:Panel>
            <tr>
                <td>
                    <asp:Label runat="server" ID="Label" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Header</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    Year
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="YearTextBox" runat="server" Width="100" BackColor="#CCCCCC" ReadOnly="true">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Revisi
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="RevisiTextBox" runat="server" Width="100" BackColor="#CCCCCC" ReadOnly="true">
                                    </asp:TextBox>
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
                                    <asp:TextBox ID="RemarkTextBox" runat="server" Width="300" Height="80" BackColor="#CCCCCC"
                                        ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Status
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label ID="StatusLabel" runat="server"></asp:Label>
                                    <asp:HiddenField ID="StatusHiddenField" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    FgActive
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="FgActiveCheckBox" Enabled="false"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                                                &nbsp;<asp:ImageButton ID="BackButton" runat="server" OnClick="BackButton_Click" />
                                                &nbsp;<asp:ImageButton ID="ActionButton" runat="server" OnClick="ActionButton_Click" />
                                                &nbsp;<asp:ImageButton ID="RevisiButton" runat="server" OnClick="RevisiButton_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Detail</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="AddButton" runat="server" OnClick="AddButton_Click" />
                                                &nbsp;<asp:ImageButton ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" />
                                            </td>
                                            <td align="right">
                                                <asp:Panel DefaultButton="DataPagerButton" ID="DataPagerPanel" runat="server">
                                                    <table border="0" cellpadding="2" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="DataPagerButton" runat="server" OnClick="DataPagerButton_Click" />
                                                            </td>
                                                            <td valign="middle">
                                                                <b>Page :</b>
                                                            </td>
                                                            <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater" runat="server" OnItemCommand="DataPagerTopRepeater_ItemCommand"
                                                                OnItemDataBound="DataPagerTopRepeater_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <td>
                                                                        <asp:LinkButton ID="PageNumberLinkButton" runat="server"></asp:LinkButton>
                                                                        <asp:TextBox Visible="false" ID="PageNumberTextBox" runat="server" Width="30px"></asp:TextBox>
                                                                    </td>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="CheckHidden" runat="server" />
                                    <asp:HiddenField ID="TempHidden" runat="server" />
                                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 5px">
                                                <asp:CheckBox runat="server" ID="AllCheckBox" />
                                            </td>
                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Action</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Account</b>
                                            </td>
                                            <td style="width: 20px" class="tahoma_11_white" align="center">
                                                <b>Days</b>
                                            </td>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>Amount 01</b>
                                            </td>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>Amount 02</b>
                                            </td>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>Amount 03</b>
                                            </td>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>Amount 04</b>
                                            </td>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>Amount 05</b>
                                            </td>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>Amount 06</b>
                                            </td>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>Amount 07</b>
                                            </td>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>Amount 08</b>
                                            </td>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>Amount 09</b>
                                            </td>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>Amount 10</b>
                                            </td>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>Amount 11</b>
                                            </td>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>Amount 12</b>
                                            </td>
                                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                                <b>Remark</b>
                                            </td>
                                        </tr>
                                        <asp:Panel ID="AddPanel" runat="server" Visible="false">
                                            <tr>
                                                <td align="center">
                                                </td>
                                                <td align="center">
                                                </td>
                                                <td align="left">
                                                    <table cellpadding="0" cellspacing="0" width="0" border="0">
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton runat="server" ID="SaveButton" OnClick="SaveButton_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="left">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="AccountTextBox" Style="width: 70px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="SearchAccountButton" runat="server" CausesValidation="false" Text="..."
                                                                    Style="width: 30px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="right">
                                                    <asp:TextBox runat="server" ID="DayTextBox" Style="width: 30px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:TextBox runat="server" ID="Amount01TextBox" Style="width: 70px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:TextBox runat="server" ID="Amount02TextBox" Style="width: 70px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:TextBox runat="server" ID="Amount03TextBox" Style="width: 70px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:TextBox runat="server" ID="Amount04TextBox" Style="width: 70px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:TextBox runat="server" ID="Amount05TextBox" Style="width: 70px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:TextBox runat="server" ID="Amount06TextBox" Style="width: 70px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:TextBox runat="server" ID="Amount07TextBox" Style="width: 70px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:TextBox runat="server" ID="Amount08TextBox" Style="width: 70px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:TextBox runat="server" ID="Amount09TextBox" Style="width: 70px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:TextBox runat="server" ID="Amount10TextBox" Style="width: 70px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:TextBox runat="server" ID="Amount11TextBox" Style="width: 70px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:TextBox runat="server" ID="Amount12TextBox" Style="width: 70px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:TextBox runat="server" ID="RemarkAddTextBox" Style="width: 200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound"
                                            OnItemCommand="ListRepeater_ItemCommand">
                                            <ItemTemplate>
                                                <tr id="RepeaterItemTemplate" runat="server">
                                                    <td align="center">
                                                        <asp:CheckBox runat="server" ID="ListCheckBox" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <table cellpadding="0" cellspacing="0" width="0" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton runat="server" ID="SaveButton" />
                                                                </td>
                                                                <td style="padding-left: 4px">
                                                                    <asp:ImageButton runat="server" ID="EditButton" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="AccountLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:TextBox runat="server" ID="DayTextBox" Style="width: 30px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:TextBox runat="server" ID="Amount01TextBox" Style="width: 70px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:TextBox runat="server" ID="Amount02TextBox" Style="width: 70px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:TextBox runat="server" ID="Amount03TextBox" Style="width: 70px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:TextBox runat="server" ID="Amount04TextBox" Style="width: 70px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:TextBox runat="server" ID="Amount05TextBox" Style="width: 70px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:TextBox runat="server" ID="Amount06TextBox" Style="width: 70px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:TextBox runat="server" ID="Amount07TextBox" Style="width: 70px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:TextBox runat="server" ID="Amount08TextBox" Style="width: 70px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:TextBox runat="server" ID="Amount09TextBox" Style="width: 70px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:TextBox runat="server" ID="Amount10TextBox" Style="width: 70px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:TextBox runat="server" ID="Amount11TextBox" Style="width: 70px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:TextBox runat="server" ID="Amount12TextBox" Style="width: 70px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:TextBox runat="server" ID="RemarkTextBox" Style="width: 200px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="Panel2">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="1500px">
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
