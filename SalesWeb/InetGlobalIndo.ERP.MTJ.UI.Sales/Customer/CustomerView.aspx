<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="CustomerView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.Customer.CustomerView" %>

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
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                Customer
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CustCodeTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox ID="CustNameTextBox" runat="server" Width="250" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="CustGroupTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="CustTypeTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Verification Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="VerificationCodeTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Customer Info</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    Address #1
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="Addr1TextBox" runat="server" Width="250" TextMode="MultiLine" ReadOnly="true"
                                        BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address #2
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="Addr2TextBox" runat="server" Width="250" TextMode="MultiLine" ReadOnly="true"
                                        BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    City
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="CityTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Postal
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="ZipCodeTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Telephone
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="PhoneTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Fax
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="FaxTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Email
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="EmailTextBox" runat="server" Width="250" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Currency
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox runat="server" ID="CurrencyTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Term
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox runat="server" ID="TermTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    NPWP
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="NPWPTextBox" runat="server" Width="150" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <%--  <tr>
                                <td>
                                    NPPKP
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="NPPKPTextBox" runat="server" Width="150" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    NPWP Address
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="NPWPAddressTextBox" runat="server" Width="150" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>--%>
                            <tr>
                                <td>
                                    Active
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:CheckBox ID="IsActiveCheckBox" runat="server" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Sales Person
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="SalesPersonTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"
                                        MaxLength="10">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Due Date Cycle
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="DueDateCycleTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"
                                        Width="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    PPN
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:CheckBox ID="FgPPNCheckBox" runat="server" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Faktur Pajak
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:CheckBox ID="PrintFPCheckBox" runat="server" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Stamp
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:CheckBox ID="StampCheckBox" runat="server" Enabled="false" />
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
                                    <asp:TextBox runat="server" ID="RemarkTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                        Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>
                                    <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                    characters left
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Contact Info</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    Contact Name
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="ContactNameTextBox" runat="server" Width="300" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Contact Title
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="ContactTitleTextBox" runat="server" Width="300" ReadOnly="true"
                                        BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Contact Phone
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="ContactHPTextBox" runat="server" Width="300" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Contact Email
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="ContactEmailTextBox" runat="server" Width="300" ReadOnly="true"
                                        BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Customer Limit</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    Customer Limit
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="CustLimitTextBox" runat="server" Width="150" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Use Limit
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="UseLimitTextBox" runat="server" Width="150" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Grace Period
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="GracePeriodTextBox" runat="server" Width="50" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                    days
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Limit
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:CheckBox ID="FgLimitCheckBox" runat="server" Enabled="false" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Bill To</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    Customer
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="BillTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="BackButton" runat="server" OnClick="BackButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Customer Contact</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td style="width: 50px">
                                                <asp:ImageButton runat="server" ID="AddButton" OnClick="AddButton_Click" />
                                            </td>
                                            <td style="width: 50px">
                                                <asp:ImageButton runat="server" ID="DeleteButton" OnClick="DeleteButton_Click" />
                                            </td>
                                            <td align="right">
                                                &nbsp;
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
                                                <b>Contact Name</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Contact Title</b>
                                            </td>
                                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                                <b>Address</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Country</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Phone</b>
                                            </td>
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Email</b>
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="ListRepeater" runat="server" OnItemDataBound="ListRepeater_ItemDataBound">
                                            <ItemTemplate>
                                                <tr id="RepeaterListTemplate" runat="server">
                                                    <td align="center">
                                                        <asp:CheckBox ID="ListCheckBox" runat="server" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal ID="NoLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <table cellpadding="0" cellspacing="0" width="0" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton runat="server" ID="ViewButton" />
                                                                </td>
                                                                <td style="padding-left: 4px">
                                                                    <asp:ImageButton runat="server" ID="EditButton2" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal ID="ContactNameLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal ID="ContactTitleLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal ID="Address1Literal" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal ID="CountryLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal ID="PhoneLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal ID="EmailLiteral" runat="server"></asp:Literal>
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
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Customer Address</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td style="width: 50px">
                                                <asp:ImageButton runat="server" ID="AddButton1" OnClick="AddButton1_Click" />
                                            </td>
                                            <td style="width: 50px">
                                                <asp:ImageButton runat="server" ID="DeleteButton1" OnClick="DeleteButton1_Click" />
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="WarningLabel1" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="CheckHidden2" runat="server" />
                                    <asp:HiddenField ID="TempHidden2" runat="server" />
                                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 5px">
                                                <asp:CheckBox runat="server" ID="AllCheckBox1" />
                                            </td>
                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                                <b>Action</b>
                                            </td>
                                            <td style="width: 120px" class="tahoma_11_white" align="center">
                                                <b>Delivery Code</b>
                                            </td>
                                            <td style="width: 120px" class="tahoma_11_white" align="center">
                                                <b>Delivery Name</b>
                                            </td>
                                            <td style="width: 120px" class="tahoma_11_white" align="center">
                                                <b>Address #1</b>
                                            </td>
                                            <td style="width: 120px" class="tahoma_11_white" align="center">
                                                <b>Address #2</b>
                                            </td>
                                            <td style="width: 120px" class="tahoma_11_white" align="center">
                                                <b>Country</b>
                                            </td>
                                            <td style="width: 120px" class="tahoma_11_white" align="center">
                                                <b>Postal Code</b>
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="ListRepeater1" runat="server" OnItemDataBound="ListRepeater1_ItemDataBound">
                                            <ItemTemplate>
                                                <tr id="RepeaterListTemplate1" runat="server">
                                                    <td align="center">
                                                        <asp:CheckBox ID="ListCheckBox1" runat="server" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal ID="NoLiteral1" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <table cellpadding="0" cellspacing="0" width="0" border="0">
                                                            <tr>
                                                                <td style="padding-left: 4px">
                                                                    <asp:ImageButton runat="server" ID="EditButton3" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal ID="DeliveryCodeLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal ID="DeliveryNameLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal ID="Address1Literal" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal ID="Address2Literal" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal ID="Country2Literal" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal ID="ZipCodeLiteral" runat="server"></asp:Literal>
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
</asp:Content>
