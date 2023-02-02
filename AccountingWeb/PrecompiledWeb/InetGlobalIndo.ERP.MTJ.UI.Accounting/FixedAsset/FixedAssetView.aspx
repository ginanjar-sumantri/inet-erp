<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.FixedAsset.FixedAssetView, App_Web_tvnfkugk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
        <table cellpadding="3" cellspacing="0" width="0" border="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                <table cellpadding="3" cellspacing="0" width="0" border="0">
                                    <tr>
                                        <td>
                                            Fixed Asset Code
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox runat="server" ID="FACodeTextBox" Width="150" MaxLength="20" ReadOnly="true"
                                                BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Fixed Asset Name
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox runat="server" ID="FANameTextBox" Width="560" MaxLength="80" ReadOnly="true"
                                                BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            Spesification
                                        </td>
                                        <td valign="top">
                                            :
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox runat="server" ID="SpesificationTextBox" Width="250" TextMode="MultiLine"
                                                ReadOnly="true" BackColor="#CCCCCC" Height="80px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Fixed Asset Condition
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="FAStatusTexBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Fixed Asset Owner
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:CheckBox runat="server" ID="FAOwnerCheckBox" Checked="true" Enabled="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Fixed Asset Sub Group
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="FASubGroupTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Buying Date
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Fixed Asset Location Type
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="FALocationTypeDropDownList" Enabled="false">
                                                <asp:ListItem Value="null">[Choose Item]</asp:ListItem>
                                                <asp:ListItem Value="GENERAL">GENERAL</asp:ListItem>
                                                <asp:ListItem Value="EMPLOYEE">EMPLOYEE</asp:ListItem>
                                                <asp:ListItem Value="CUSTOMER">CUSTOMER</asp:ListItem>
                                                <asp:ListItem Value="SUPPLIER">SUPPLIER</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Fixed Asset Location
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="FALocationTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Currency
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="CurrencyTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Forex Rate
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="ForexRateTextBox" Width="150" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <table cellpadding="3" cellspacing="0" width="0" border="0">
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <table cellpadding="3" cellspacing="1" width="0" border="0">
                                                <tr class="bgcolor_gray">
                                                    <td style="width: 110px" align="center" class="tahoma_11_white">
                                                        <b>Forex</b>
                                                    </td>
                                                    <td style="width: 110px" align="center" class="tahoma_11_white">
                                                        <b>Home</b>
                                                    </td>
                                                    <td style="width: 110px" align="center" class="tahoma_11_white">
                                                        <b>Life In Months</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Buy Price
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <table cellpadding="3" cellspacing="0" width="0" border="0">
                                                <tr>
                                                    <td style="width: 100px" align="center">
                                                        <asp:TextBox runat="server" ID="BuyPriceForexTextBox" Width="100" ReadOnly="true"
                                                            BackColor="#CCCCCC"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 100px" align="center">
                                                        <asp:TextBox runat="server" ID="BuyPriceHomeTextBox" Width="100" ReadOnly="true"
                                                            BackColor="#CCCCCC"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 100px" align="center">
                                                        <asp:TextBox runat="server" ID="BuyPriceLifeInMonthsTextBox" Width="100" ReadOnly="true"
                                                            BackColor="#CCCCCC"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <table cellpadding="3" cellspacing="1" width="0" border="0">
                                                <tr class="bgcolor_gray">
                                                    <td style="width: 110px" align="center" class="tahoma_11_white">
                                                        <b>Begin Depr.</b>
                                                    </td>
                                                    <td style="width: 110px" align="center" class="tahoma_11_white">
                                                        <b>Process Depr.</b>
                                                    </td>
                                                    <td style="width: 110px" align="center" class="tahoma_11_white">
                                                        <b>Total Depr.</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Life In Months
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <table cellpadding="3" cellspacing="0" width="0" border="0">
                                                <tr>
                                                    <td style="width: 100px" align="center">
                                                        <asp:TextBox runat="server" ID="LifeInMonthsBeginDeprTextBox" Width="100" ReadOnly="true"
                                                            BackColor="#CCCCCC"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 100px" align="center">
                                                        <asp:TextBox runat="server" ID="LifeInMonthsProcessDeprTextBox" Width="100" ReadOnly="true"
                                                            BackColor="#CCCCCC"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 100px" align="center">
                                                        <asp:TextBox runat="server" ID="LifeInMonthsTotalDeprTextBox" Width="100" ReadOnly="true"
                                                            BackColor="#CCCCCC"></asp:TextBox>
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
                                            <table cellpadding="3" cellspacing="0" width="0" border="0">
                                                <tr>
                                                    <td style="width: 100px" align="center">
                                                        <asp:TextBox runat="server" ID="AmountBeginDeprTextBox" Width="100" ReadOnly="true"
                                                            BackColor="#CCCCCC"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 100px" align="center">
                                                        <asp:TextBox runat="server" ID="AmountProcessDeprTextBox" Width="100" ReadOnly="true"
                                                            BackColor="#CCCCCC"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 100px" align="center">
                                                        <asp:TextBox runat="server" ID="AmountTotalDeprTextBox" Width="100" ReadOnly="true"
                                                            BackColor="#CCCCCC"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Current Amount
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <table cellpadding="3" cellspacing="0" width="0" border="0">
                                                <tr>
                                                    <td style="width: 100px" align="center">
                                                        <asp:TextBox runat="server" ID="CurrentAmountTextBox" Width="100" ReadOnly="true"
                                                            BackColor="#CCCCCC"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Status Process
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:CheckBox runat="server" ID="StatusProcessCheckBox" Enabled="False" Checked="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Active
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:CheckBox runat="server" ID="ActiveCheckBox" Checked="true" Enabled="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Sold
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:CheckBox runat="server" ID="SoldCheckBox" Enabled="false" />
                                            <asp:HiddenField ID="CreatedFromHiddenField" runat="server" />
                                            <asp:HiddenField ID="CreateJournalHiddenField" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Created From
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="CreatedFromLabel" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Create Journal
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="CreateJournalLabel" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Image runat="server" ID="FAPhoto" />
                                        </td>
                                    </tr>
                                </table>
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
                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                                <%--</td>
                            <td>--%>
                                &nbsp;<asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False"
                                    OnClick="CancelButton_Click" />
                            </td>
                            <%--<td>
                                <asp:ImageButton ID="CreateJournalButton" runat="server" CausesValidation="False"
                                    OnClick="CreateJournalButton_Click" />
                            </td>--%>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
