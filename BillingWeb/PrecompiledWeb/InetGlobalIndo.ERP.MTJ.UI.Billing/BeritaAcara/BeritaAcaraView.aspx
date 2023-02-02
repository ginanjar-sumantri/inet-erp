<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.BillingBeritaAcara.BeritaAcaraView, App_Web_pymggnms" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
                    <div>
                        <table cellpadding="3" cellspacing="0" width="0" border="0" style="float: left;width:459px">                            
                            <tr>
                                <td>
                                    Approved By Customer Name
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="ApprovedByCustomerNameTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="validatorCustomerApprover" ControlToValidate="ApprovedByCustomerNameTextBox"
                                        Enabled="false" Text="*" ErrorMessage="Customer Approver Name Must be Filled."></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Transaction Date
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="TransDateTextBox" ReadOnly="true" BackColor="#CCCCCC" Width="100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Sales Confirmation No. Reference
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="SalesConfirmationTextBox" Width="180" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Trial Day
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="TrialDayTextBox" ReadOnly="true" BackColor="#CCCCCC" Width="80" runat="server">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        Remark :<br />
                        <asp:TextBox TextMode="MultiLine" ID="RemarkTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server" Rows="5" Columns="50"
                            Style="float: left"></asp:TextBox>
                        <div style="clear: both">
                        </div>
                    </div>
                    <table style="width:100%">
                        <tr>
                            <td valign="top" width="450">
                        <fieldset>
                            <legend>Default Information</legend>
                            <table>
                                <tr>
                                    <td>
                                        Technical Service Spesification
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TechnicalServiceSpesificationTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Technical Bandwidth INT
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TechnicalBandwidthINTTextBox" Width="80" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Technical Bandwidth Local Loop
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TechnicalBandwidthLocalLoopTextBox" Width="80" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Technical Bandwidth INT Ratio Upstream
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TechnicalBandwidthINTRatioUpstreamTextBox" Width="80" ReadOnly="true" BackColor="#CCCCCC"
                                            runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Technical Bandwidth INT Ratio Downstream
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TechnicalBandwidthINTRatioDownstreamTextBox" Width="80" ReadOnly="true" BackColor="#CCCCCC"
                                            runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Technical Termination Point
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TechnicalTerminationPointTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        </td>
                            <td valign="top" width="450">
                        <fieldset>
                            <legend>Dedicated technical Information</legend>
                            <table>
                                <tr>
                                    <td>
                                        Technical Account MRTG
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TechincalAccountTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Technical Account MRTG Password
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TechincalAccountPasswordTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Technical IP Allocation
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TechnicalIPAllocationTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Technical Transmision
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TechnicalTransmisionTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        </td>
                        </tr>
                    </table>
                    <table style="width:100%">
                        <tr>
                            <td valign="top" width="450">
                        <fieldset>
                            <label>
                                Mikrotik Technical Information</label>
                            <table>
                                <tr>
                                    <td>
                                        Mikrotik PPTP UserName
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="MikrotikPPTPUserNameTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Mikrotik PPPOE UserName
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="MikrotikPPPOEUserNameTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Mikrotik Hotspot UserName
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="MikrotikHotspotUserNameTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Mikrotik Queue Name DownLink
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="MikrotikQueueNameDownLinkTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Mikrotik Queue Name UpLink
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="MikrotikQueueNameUpLinkTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        </td>
                            <td valign="top" width="450">
                        <fieldset>
                            <legend>NAP Technical Information</legend>
                            <table>
                                <tr>
                                    <td>
                                        BGP AS Number
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="BGPASNumberTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        BGP IP Address Router
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="BGPIPAddressRouterTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        BGP Advertise IP
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="BGPAdvertiseIPTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Type Received Routes
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TypeReceivedRoutesTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        </td>
                        </tr>
                    </table>
                    <fieldset>
                        <legend>Collocation Service</legend>
                        <table>
                            <tr>
                                <td>
                                    Collocation Rack No.
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="CollocationRackNoTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Collocation Server Position No.
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="CollocationServerPositionNoTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="WarningLabel" runat="server" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                                &nbsp;<asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False"
                                    OnClick="CancelButton_Click" />
                                &nbsp;<asp:ImageButton ID="ApproveButton" runat="server" CausesValidation="False"
                                    OnClick="ApproveButton_Click" />
                                &nbsp;<asp:ImageButton ID="PreviewButton" runat="server" Visible="false" OnClick="PreviewButton_Click" />
                                <asp:HiddenField ID="StatusHiddenField" runat="server" />
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
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%">
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
