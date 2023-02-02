<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.Report.ReceiptSummaryPerReceiptType, App_Web__h_hzsvf" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function ValidatePeriod(_prmPeriod) {
            var _tempPeriod = parseInt(_prmPeriod.value);
            if (isNaN(_tempPeriod) == true) {
                _tempPeriod = 0;
            }

            if (_tempPeriod < 1 || _tempPeriod > 12) {
                _prmPeriod.value = "";
            }
        }

        function ValidateYear(_prmYear) {
            var _tempYear = parseInt(_prmYear.value);
            if (isNaN(_tempYear) == true) {
                _tempYear = 0;
            }

            if (_tempYear < 1000 || _tempYear > 9999) {
                _prmYear.value = "";
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <table cellpadding="3" cellspacing="0" width="100%" border="0">
        <tr>
            <td class="tahoma_14_black">
                <b>
                    <asp:Literal ID="PageTitleLiteral" runat="server" />
                </b>
            </td>
        </tr>
        <tr>
            <td class="warning">
                <asp:Literal ID="Literal1" runat="server" Text="* This Report is Best Printed on Legal Size Paper" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="MenuPanel">
                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td>
                                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="WarningLabel" runat="server" CssClass="warning"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellpadding="3" cellspacing="0" width="0">
                                    <tr>
                                        <td>
                                            Start Period
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="StartPeriodTextBox" runat="server" Width="50" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="StartPeriodRequiredFieldValidator" runat="server"
                                                ErrorMessage="Start Period Must Be Filled" Text="*" ControlToValidate="StartPeriodTextBox"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                            &nbsp; Year
                                            <asp:TextBox runat="server" ID="StartYearTextBox" Width="50" MaxLength="4"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="StartYearRequiredFieldValidator" runat="server" ErrorMessage="Start Year Must Be Filled"
                                                Text="*" ControlToValidate="StartYearTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            End Period
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EndPeriodTextBox" runat="server" Width="50" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="EndPeriodRequiredFieldValidator" runat="server" ErrorMessage="End Period Must Be Filled"
                                                Text="*" ControlToValidate="EndPeriodTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                            &nbsp; Year
                                            <asp:TextBox runat="server" ID="EndYearTextBox" Width="50" MaxLength="4"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="EndYearRequiredFieldValidator" runat="server" ErrorMessage="End Year Must Be Filled"
                                                Text="*" ControlToValidate="EndYearTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Divide By
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="DivideByDropDownList">
                                                <asp:ListItem Value="0">1</asp:ListItem>
                                                <asp:ListItem Value="1">10</asp:ListItem>
                                                <asp:ListItem Value="2">100</asp:ListItem>
                                                <asp:ListItem Value="3">1,000</asp:ListItem>
                                                <asp:ListItem Value="4">10,000</asp:ListItem>
                                                <asp:ListItem Value="5">100,000</asp:ListItem>
                                                <asp:ListItem Value="6">1,000,000</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Type
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="TypeDropDownList">
                                                <asp:ListItem Value="null" Text="[Choose One]"></asp:ListItem>
                                                <asp:ListItem Value="0">Summary Per Receipt Type</asp:ListItem>
                                                <asp:ListItem Value="1">Summary Per Customer</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="TypeCustomValidator" runat="server" ErrorMessage="Type Must Be Choosed"
                                                Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="TypeDropDownList"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Filter
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="FilterDropDownList">
                                                <asp:ListItem Value="null" Text="[Choose One]"></asp:ListItem>
                                                <asp:ListItem Value="0">All</asp:ListItem>
                                                <asp:ListItem Value="1">Trade</asp:ListItem>
                                                <asp:ListItem Value="2">Non Trade</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="FilterCustomValidator" runat="server" ErrorMessage="Filter Must Be Choosed"
                                                Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="FilterDropDownList"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td align="right">
                                            <asp:Panel DefaultButton="DataPagerButton" ID="DataPagerPanel" runat="server">
                                                <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td align="left">
                                                            <table>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:CheckBox runat="server" ID="AllCheckBox" Text="Check All" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:HiddenField ID="CheckHidden" runat="server" />
                                                                        <asp:HiddenField ID="TempHidden" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="DataPagerButton" runat="server" CausesValidation="false" OnClick="DataPagerButton_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
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
                                    <tr>
                                        <td valign="top">
                                            Receipt Type
                                        </td>
                                        <td valign="top">
                                            :
                                        </td>
                                        <td>
                                            <asp:CheckBoxList runat="server" ID="PayTypeCheckBoxList" RepeatColumns="3">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Customer Group
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="CustomerGroupDropDownList" AutoPostBack="True"
                                                OnSelectedIndexChanged="CustomerGroupDropDownList_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Customer Type
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="CustomerTypeDropDownList" AutoPostBack="True"
                                                OnSelectedIndexChanged="CustomerTypeDropDownList_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td align="right">
                                            <asp:Panel DefaultButton="DataPagerButton2" ID="Panel1" runat="server">
                                                <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td align="left">
                                                            <table>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:CheckBox runat="server" ID="AllCheckBox2" Text="Check All" />
                                                                        <asp:CheckBox runat="server" ID="GrabAllCheckBox2" Text="Grab All" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:HiddenField ID="CheckHidden2" runat="server" />
                                                                        <asp:HiddenField ID="TempHidden2" runat="server" />
                                                                        <asp:HiddenField ID="AllHidden2" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="DataPagerButton2" runat="server" CausesValidation="false" OnClick="DataPagerButton2_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td valign="middle">
                                                                        <b>Page :</b>
                                                                    </td>
                                                                    <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater2" runat="server" OnItemCommand="DataPagerTopRepeater2_ItemCommand"
                                                                        OnItemDataBound="DataPagerTopRepeater2_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <td>
                                                                                <asp:LinkButton ID="PageNumberLinkButton2" runat="server" CausesValidation="false"></asp:LinkButton>
                                                                                <asp:TextBox Visible="false" ID="PageNumberTextBox2" runat="server" Width="30px"></asp:TextBox>
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
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="note">
                                            * Use Check All to select all checkbox visible on this page<br />
                                            * Use Grab All to get all data regarding filter selected
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            Customer
                                        </td>
                                        <td valign="top">
                                            :
                                        </td>
                                        <td>
                                            <asp:CheckBoxList runat="server" ID="CustCodeCheckBoxList" RepeatColumns="3">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Currency Type
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="CurrencyTypeDropDownList" runat="server">
                                                <asp:ListItem Text="Forex Currency" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Default Currency" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <table border="0" cellpadding="3" cellspacing="0" width="0">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ViewButton" runat="server" OnClick="ViewButton_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ResetButton" runat="server" CausesValidation="false" OnClick="ResetButton_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" ShowPrintButton="true"
                        ShowZoomControl="true" ZoomMode="Percent" ZoomPercent="100">
                </rsweb:ReportViewer>
            </td>
        </tr>
    </table>
</asp:Content>
