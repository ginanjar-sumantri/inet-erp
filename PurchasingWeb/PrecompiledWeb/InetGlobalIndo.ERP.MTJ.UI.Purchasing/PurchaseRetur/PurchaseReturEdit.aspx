<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseRetur.PurchaseReturEdit, App_Web_l83mqzis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="scriptMgr" EnablePartialRendering="true" runat="server" />
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
                                Trans No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TransNoTextBox" runat="server" Width="160" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox ID="FileNoTextBox" runat="server" Width="160" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                <%--<input type="button" id="Button1" value="..." onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)" />--%>
                                <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Supplier
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SupplierTextBox" runat="server" Width="500" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <%--<tr>
                            <td>
                                RR No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="RRNoTextBox" Width="160" BackColor="#CCCCCC" ReadOnly="true">
                                </asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                Use Reference
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:RadioButtonList ID="UseReferenceRBL" runat="server" OnSelectedIndexChanged="UseReferenceRBL_SelectedIndexChanged"
                                    AutoPostBack="true" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Reference No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ReferenceNoTextBox" Width="200px">
                                </asp:TextBox>
                                <asp:CustomValidator ID="ReferenceNoCustomValidator" runat="server" ClientValidationFunction="TextBoxValidation"
                                    ErrorMessage="Reference No. Must Be Filled" Text="*" ControlToValidate="ReferenceNoTextBox">
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Delivery Back
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:RadioButtonList ID="DeliveryBackRBL" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Currency / Rate
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList runat="server" ID="CurrCodeDropDownList" AutoPostBack="True" OnSelectedIndexChanged="CurrCodeDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CurrCodeCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                            ErrorMessage="Currency Must Be Filled" Text="*" ControlToValidate="CurrCodeDropDownList">
                                        </asp:CustomValidator>
                                        <asp:TextBox runat="server" ID="CurrRateTextBox" Width="150">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="CurrRateRequiredFieldValidator" runat="server" ErrorMessage="Currency Rate Must Be Filled"
                                            Text="*" ControlToValidate="CurrRateTextBox" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="CurrCodeDropDownList" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                PPN %
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="PPNTextBox" OnTextChanged="PPNTextBox_TextChanged"
                                    AutoPostBack="true">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <table cellpadding="3" cellspacing="0" width="0" border="0">
                                    <tr>
                                        <td style="text-align: center; background-color: Gray; color: White; width: 139px;">
                                            <b>Base Forex</b>
                                        </td>
                                        <td>
                                        </td>
                                        <td style="text-align: center; background-color: Gray; color: White; width: 139px;">
                                            <b>PPN Forex</b>
                                        </td>
                                        <td>
                                        </td>
                                        <td style="text-align: center; background-color: Gray; color: White; width: 139px;">
                                            <b>Total Forex</b>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Amount
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="BaseForexTextBox" AutoPostBack="true" OnTextChanged="BaseForexTextBox_TextChanged"
                                    ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                                <asp:TextBox runat="server" ID="PPNForexTextBox" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                                <asp:TextBox runat="server" ID="TotalForexTextBox" ReadOnly="true" BackColor="#CCCCCC">
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
