<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="RequestSalesReturAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.RequestSalesRetur.RequestSalesReturAdd" %>

<%@ Register Src="../ProductPicker.ascx" TagName="ProductPicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="scriptMgr" EnablePartialRendering="true" runat="server" />
    <asp:Panel ID="Panel1" DefaultButton="NextButton" runat="server">
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
                                <%--<input id="button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />--%>
                                <asp:Literal ID="CalendarScriptLiteral" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td width="90px">
                                Customer
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CustomerTextBox" runat="server" MaxLength="30" Width="90" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:Button ID="btnSearchCustNo" runat="server" Text="..." CausesValidation="False" />
                                <asp:TextBox ID="CustomerNameTextBox" runat="server" Width="180" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <%--<tr>
                            <td>
                                Customer
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList runat="server" ID="CustDropDownList" AutoPostBack="True" OnSelectedIndexChanged="CustDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CustCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                            ErrorMessage="Customer Must Be Filled" Text="*" ControlToValidate="CustDropDownList">
                                        </asp:CustomValidator>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="CustDropDownList" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                Attn
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AttnTextBox" runat="server" Width="280" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
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
                            <td colspan="4">
                                <table id="ReferenceTable" runat="server" visible="true">
                                    <%--<tr>
                                        <td width="90px">
                                            Trans Type
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList runat="server" ID="TransTypeDropDownList" AutoPostBack="True" OnSelectedIndexChanged="TransTypeDropDownList_SelectedIndexChanged">
                                                        <asp:ListItem text ="[Choose One]" Value ="null"></asp:ListItem>
                                                        <asp:ListItem Text="DO" Value="DO"></asp:ListItem>
                                                        <asp:ListItem Text="DS" Value="DS"></asp:ListItem>
                                                        <asp:ListItem Text="POS" Value="POS"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="TransTypeCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                                        ErrorMessage="Trans Type Must Be Filled" Text="*" ControlToValidate="TransTypeDropDownList">
                                                    </asp:CustomValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>
                                            Reference No.
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <%--<asp:DropDownList runat="server" ID="SuratJalanDropDownList">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="SuratJalanCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                                        ErrorMessage="SJ No. Must Be Filled" Text="*" ControlToValidate="SuratJalanDropDownList">
                                                    </asp:CustomValidator>--%>
                                                    <asp:TextBox runat="server" ID="SuratJalanTextBox">
                                                    </asp:TextBox>
                                                    <asp:CustomValidator ID="SuratJalanCustomValidator" runat="server" ClientValidationFunction="TextBoxValidation"
                                                        ErrorMessage="SJ No. Must Be Filled" Text="*" ControlToValidate="SuratJalanTextBox">
                                                    </asp:CustomValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
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
                        <%-- <tr>
                            <td>
                                Product Scrap
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:RadioButtonList ID="ProductScrapRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>--%>
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
                                Total Forex
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TotalForexTextBox" runat="server" BackColor="#CCCCCC" Width="150">
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
