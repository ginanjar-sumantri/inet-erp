<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.Contract.ContractDetail, App_Web_0iuy3kfz" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
                                <asp:TextBox ID="TransNmbrTextBox" runat="server" Width="160" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                            <td width="10px">
                            </td>
                            <td>
                                File No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="FileNmbrTextBox" runat="server" Width="160" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="DateTextBox" Width="80" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Finance Customer PIC
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="FinaceCustomerPICTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Sales Confirmation No Ref
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SalesConfirmationNoRefTextBox" runat="server" Width="160" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Finance Customer Phone
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="FinanceCustomerPhoneTextBox" runat="server" BackColor="#CCCCCC"
                                    ReadOnly="true">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Company Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CompanyNameTextBox" Width="180" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Finance Customer Fax
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="FinanceCustomerFaxTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Responsible Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ResponsibleNameTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Finance Customer Email
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="FinanceCustomerEmailTextBox" runat="server" BackColor="#CCCCCC"
                                    ReadOnly="true">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Title Name
                            </td>
                            <td>
                                :
                            </td>
                            <td colspan="5">
                                <asp:TextBox runat="server" ID="TitleNameTextBox" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Letter Provider Information
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="LetteProviderInformationTextBox" BackColor="#CCCCCC"
                                    ReadOnly="true" Width="200" Height="60" TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Letter Customer Information
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="LetteCustomerInformationTextBox" runat="server" BackColor="#CCCCCC"
                                    ReadOnly="true" Width="200" Height="60" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Status
                            </td>
                            <td>
                                :
                            </td>
                            <td colspan="5">
                                <asp:Label ID="StatusLable" runat="server"></asp:Label>
                                <asp:HiddenField ID="StatusHiddenField" runat="server" />
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
                            </td>
                            <td>
                                <asp:ImageButton ID="BackButton" runat="server" CausesValidation="False" OnClick="BackButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ActionButton" runat="server" CausesValidation="False" OnClick="ActionButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="PreviewButton" runat="server" OnClick="PreviewButton_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:DropDownList ID="ContractTemplateDDL" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="Panel2">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" runat="server">
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
