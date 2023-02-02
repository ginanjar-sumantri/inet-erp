<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.CustBillingAccountBase.BILTrRadiusActivateTemporaryView, App_Web_cajrmfjr" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
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
                        <td width="180px">
                            Trans No.
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="TransNoTextBox" Width="160" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="10px">
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
                            <asp:TextBox runat="server" ID="FileNmbrTextBox" Width="160" MaxLength="20" BackColor="#CCCCCC"
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
                            <asp:TextBox runat="server" ID="TransDateTextBox" Width="100" BackColor="#CCCCCC"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Customer Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="CustNameDropDownList" runat="server" AutoPostBack="true" Enabled="false" BackColor="#CCCCCC"
                                ReadOnly="true">
                            </asp:DropDownList>
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
                            <asp:TextBox runat="server" ID="PeriodTextBox" Width="50" BackColor="#CCCCCC"
                                ReadOnly="true"></asp:TextBox>
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
                            <asp:TextBox runat="server" ID="YearTextBox" Width="50" BackColor="#CCCCCC"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Attachment File
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Image ID="PictureImage" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Reason
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox TextMode="MultiLine" runat="server" ID="ReasonTextBox" Height="60px" BackColor="#CCCCCC"
                                ReadOnly="true"></asp:TextBox>
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
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="3" cellspacing="0" border="0" width="0">
                    <tr>
                        <td>
                            <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" CausesValidation="False" />
                        </td>
                        <td>
                            <asp:ImageButton ID="BackButton" runat="server" OnClick="BackButton_Click" CausesValidation="False" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ActionButton" runat="server" OnClick="ActionButton_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
