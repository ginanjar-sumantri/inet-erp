<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseRetur.PurchaseReturAdd, App_Web_l83mqzis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="scriptMgr" EnablePartialRendering="true" runat="server" />
    <asp:Panel runat="server" ID="Panel1" DefaultButton="NextButton">
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
                                Trans Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <%--<input type="button" id="Img1" value="..." onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)" />--%>
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
                                <asp:DropDownList ID="SupplierDropDownList" runat="server">
                                    <%--AutoPostBack="true" OnSelectedIndexChanged="SupplierDropDownList_SelectedIndexChanged"--%>
                                </asp:DropDownList>
                                <asp:CustomValidator ID="SupplierCustomValidator" runat="server" ErrorMessage="Supplier Must Be Choosed"
                                    Text="*" ControlToValidate="SupplierDropDownList" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
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
                                <asp:DropDownList runat="server" ID="RRNoDropDownList">
                                </asp:DropDownList>
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
                                <asp:TextBox ID="RemarkTextBox" runat="server" Height="80" TextMode="MultiLine" Width="300"></asp:TextBox>
                                <asp:TextBox ID="CounterTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                    Width="50"></asp:TextBox>
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
    </asp:Panel>
</asp:Content>
