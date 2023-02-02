<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.FAProcess.FAProcessDetail, App_Web_xagvabuk" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
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
                                <%--<tr>
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
                                </tr>--%>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </asp:Panel>
            <tr>
                <td>
                    <fieldset>
                        <legend>Header</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td colspan="3">
                                    <asp:Label runat="server" ID="Label" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Year
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="YearTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"
                                        Width="50">
                                    </asp:TextBox>
                                    <asp:HiddenField runat="server" ID="PeriodHiddenField" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Period
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="PeriodTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"
                                        Width="150">
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
                                    <asp:TextBox runat="server" ID="RemarkTextBox" TextMode="MultiLine" Width="300" MaxLength="500"
                                        Height="80" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
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
                                    <asp:Label runat="server" ID="StatusLabel"></asp:Label>
                                    <asp:HiddenField runat="server" ID="StatusHiddenField" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                                                <%--</td>
                                                <td>--%>
                                                &nbsp;<asp:ImageButton ID="BackButton" runat="server" OnClick="BackButton_Click" />
                                                <%--</td>
                                                <td>--%>
                                                &nbsp;<asp:ImageButton ID="ActionButton" runat="server" OnClick="ActionButton_Click" />
                                                <%--</td>
                                                <td>--%>
                                                &nbsp;<asp:ImageButton ID="PreviewButton" runat="server" OnClick="PreviewButton_Click" />
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
                                                <%--</td>
                                                <td>--%>
                                                &nbsp;<asp:ImageButton ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" />
                                                <%--</td>
                                                <td>--%>
                                                &nbsp;<asp:ImageButton ID="DeleteAllButton" runat="server" OnClick="DeleteAllButton_Click" />
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
                                <td align="left">
                                    <asp:Panel ID="Panel3" DefaultButton="GoImageButton" runat="server">
                                        <table cellpadding="0" cellspacing="2" border="0">
                                            <tr>
                                                <td>
                                                    <b>Quick Search :</b>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CategoryDropDownList" runat="server">
                                                        <asp:ListItem Value="Code" Text="Fixed Asset Code"></asp:ListItem>
                                                        <asp:ListItem Value="Name" Text="Fixed Asset Name"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="KeywordTextBox" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="GoImageButton" runat="server" OnClick="GoImageButton_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
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
                                    <asp:HiddenField ID="sortField" runat="server" />
                                    <asp:HiddenField ID="ascDesc" Value="false" runat="server" />
                                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 5px" rowspan="2">
                                                <asp:CheckBox runat="server" ID="AllCheckBox" />
                                            </td>
                                            <td rowspan="2" style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td rowspan="2" style="width: 50px" class="tahoma_11_white" align="center">
                                                <b>Action</b>
                                            </td>
                                            <td rowspan="2" style="width: 100px" class="tahoma_11_white" align="center">
                                                <asp:LinkButton Text="Fixed Asset Code" ID="field1" class="SortLinkButton" runat="server"
                                                    OnClick="field1_Click"></asp:LinkButton>
                                            </td>
                                            <td rowspan="2" style="width: 150px" class="tahoma_11_white" align="center">
                                                <asp:LinkButton Text="Fixed Asset Name" ID="field2" class="SortLinkButton" runat="server"
                                                    OnClick="field2_Click"></asp:LinkButton>
                                            </td>
                                            <td colspan="2" style="width: 200px" class="tahoma_11_white" align="center">
                                                <b>Balance</b>
                                            </td>
                                            <td colspan="3" style="width: 300px" class="tahoma_11_white" align="center">
                                                <b>Depreciation</b>
                                            </td>
                                        </tr>
                                        <tr class="bgcolor_gray">
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <asp:LinkButton Text="Life" ID="field3" class="SortLinkButton" runat="server" OnClick="field3_Click"></asp:LinkButton>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <asp:LinkButton Text="Amount" ID="field4" class="SortLinkButton" runat="server" OnClick="field4_Click"></asp:LinkButton>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <asp:LinkButton Text="Amount" ID="field5" class="SortLinkButton" runat="server" OnClick="field5_Click"></asp:LinkButton>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <asp:LinkButton Text="Adjust" ID="field6" class="SortLinkButton" runat="server" OnClick="field6_Click"></asp:LinkButton>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <asp:LinkButton Text="Total" ID="field7" class="SortLinkButton" runat="server" OnClick="field7_Click"></asp:LinkButton>
                                            </td>
                                        </tr>
                                        <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
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
                                                                <td style="padding-left: 4px">
                                                                    <asp:ImageButton runat="server" ID="EditButton2" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="FACodeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="FANameLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="BalanceLifeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="BalanceAmountLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="AmountDeprLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="AdjustDeprLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="TotalDeprLiteral"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td colspan="3">
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td align="right">
                                                        <b>Total :</b>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal ID="AmountTotalLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal ID="AdjustTotalLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal ID="GrandTotalLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <tr class="bgcolor_gray">
                                            <td style="width: 1px" colspan="25">
                                            </td>
                                        </tr>
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
