<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="ProductTypeDetailView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.ProductTypeDetailView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="EditButton" runat="server">
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
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                Warehouse Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="WrhsTypeTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Inventory (IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccInventTextBox" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                                <asp:TextBox runat="server" ID="AccInventName" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="350">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Sales (IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccSalesTextBox" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                                <asp:TextBox runat="server" ID="AccSalesName" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="350">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account COGS (IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccCOGSTextBox" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                                <asp:TextBox runat="server" ID="AccCOGSName" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="350">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account WIP (IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccWIPTextBox" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                                <asp:TextBox runat="server" ID="AccWIPName" ReadOnly="true" BackColor="#CCCCCC" Width="350">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Transit SJ (IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccTransitSJTextBox" Width="100" ReadOnly="true"
                                    BackColor="#CCCCCC">
                                </asp:TextBox>
                                <asp:TextBox runat="server" ID="AccTransitSJName" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="350">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Transit Wrhs (IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccTransitWrhsTextBox" Width="100" ReadOnly="true"
                                    BackColor="#CCCCCC">
                                </asp:TextBox>
                                <asp:TextBox runat="server" ID="AccTransitWrhsName" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="350">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Sales Retur
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccSReturTextBox" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                                <asp:TextBox runat="server" ID="AccSReturName" ReadOnly="true" BackColor="#CCCCCC" Width="350">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Purchase Retur
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccPReturTextBox" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                                <asp:TextBox runat="server" ID="AccPReturName" ReadOnly="true" BackColor="#CCCCCC" Width="350">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Transit Reject
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccTransitRejectTextBox" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                                <asp:TextBox runat="server" ID="AccTransitRejectName" ReadOnly="true" BackColor="#CCCCCC" Width="350">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Exp Loss
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccExpLossTextBox" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                                <asp:TextBox runat="server" ID="AccExpLossName" ReadOnly="true" BackColor="#CCCCCC" Width="350">
                                </asp:TextBox>
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
                                <asp:CheckBox ID="FgActiveCheckBox" runat="server" Enabled="false"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Remark
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80px" MaxLength="500"
                                    TextMode="MultiLine" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
