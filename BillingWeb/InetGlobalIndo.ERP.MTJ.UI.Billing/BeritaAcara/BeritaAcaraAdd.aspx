<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="BeritaAcaraAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.BillingBeritaAcara.BeritaAcaraAdd" %>

<%@ Register Src="../ProductPicker.ascx" TagName="ProductPicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">
        function Activate(_prmActiveCheckBox, _prmActivateDate) {
            var currentTime = new Date();
            var month = currentTime.getMonth() + 1;
            var day = currentTime.getDate();
            var year = currentTime.getFullYear();

            if (_prmActiveCheckBox.checked == true) {
                _prmActivateDate.value = (year + "-" + month + "-" + day);
            } else {
                _prmActivateDate.value = "";
            }
        }

        function Selected(_prmDDL, _prmTextBox) {
            if (_prmDDL.value != "null") {
                _prmTextBox.value = _prmDDL.value;
            }
            else {
                _prmTextBox.value = "";
            }
        }

        function Blur(_prmDDL, _prmTextBox) {
            _prmDDL.value = _prmTextBox.value;

            if (_prmDDL.value == '') {
                _prmTextBox.value = "";
                _prmDDL.value = "null";
            }
        }

        function ValidatePeriod(_prmPeriod) {
            var _tempPeriod = _prmPeriod.value;
            if (parseInt(_tempPeriod) < 1 || parseInt(_tempPeriod) > 12) {
                _prmPeriod.value = "";
            }
        }

        function ValidateYear(_prmYear) {
            var _tempYear = _prmYear.value;
            if (parseInt(_tempYear) < 1 || parseInt(_tempYear) > 9999) {
                _prmYear.value = "";
            }
        }

        function ValidateInterval(_prmInterval) {
            var _tempInterval = _prmInterval.value;
            if (parseInt(_tempInterval) < 1) {
                _prmInterval.value = "";
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:ScriptManager runat="server" ID="ScriptManager" EnablePartialRendering="true">
    </asp:ScriptManager>
    <asp:Panel ID="Panel1" DefaultButton="SaveButton" runat="server">
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
                    <div>
                        <table cellpadding="3" cellspacing="0" width="0" border="0" style="float: left;width:459px" >
                            <tr>
                                <td>
                                    Approved By Customer Name
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="ApprovedByCustomerNameTextBox" runat="server"></asp:TextBox>
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
                                    <asp:TextBox runat="server" ID="TransDateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                    <input id="TransDateButton" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_TransDateTextBox,'yyyy-mm-dd',this)"
                                        type="button" value="..." />
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
                                    <asp:UpdatePanel ID="UpdatePanelSalesConfirmation" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="SalesConfirmationDropDownList" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="SalesConfirmationDropDownList_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Sales Confirmation Must Be Chosen"
                                                Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="SalesConfirmationDropDownList"></asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
                                    <asp:UpdatePanel ID="UpdatePanelTrialDay" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="TrialDayTextBox" Text="0" ReadOnly="true" Width="80" runat="server">
                                            </asp:TextBox>
                                            <asp:HiddenField ID="TrialDayHiddenField" runat="server" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="SalesConfirmationDropDownList" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                        Remark :<br />
                        <asp:TextBox TextMode="MultiLine" ID="RemarkTextBox" runat="server" Rows="5" Columns="50"
                            Style="float: left"></asp:TextBox>
                    </div>
                    <table style="width:100%">
                        <tr>
                            <td valign="top" width="450">
                                <fieldset>
                                    <legend>Default Information</legend>
                                    <table>
                                        <tr>
                                            <td>
                                                Service Spesification
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TechnicalServiceSpesificationTextBox" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Bandwidth INT
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TechnicalBandwidthINTTextBox" Text="0" MaxLength="5" Width="80" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="TechnicalBandwidthValidator" Text="*"
                                                ErrorMessage="Technical Bandwidth must be filled." ControlToValidate="TechnicalBandwidthINTTextBox"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Bandwidth Local Loop
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TechnicalBandwidthLocalLoopTextBox" Text="0" MaxLength="5" Width="80" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="TechnicalBandwidthLocalLoopValidator" Text="*"
                                                ErrorMessage="Technical Bandwidth LocalLoop must be filled." ControlToValidate="TechnicalBandwidthLocalLoopTextBox"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Bandwidth INT Ratio Upstream
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TechnicalBandwidthINTRatioUpstreamTextBox" Text="0" MaxLength="5" Width="80"
                                                    runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="TechnicalBandwidthINTRatioUpstreamValidator" Text="*"
                                                ErrorMessage="Technical Bandwidth INT Ratio Upstream must be filled." ControlToValidate="TechnicalBandwidthINTRatioUpstreamTextBox"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Bandwidth INT Ratio Downstream
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TechnicalBandwidthINTRatioDownstreamTextBox" Text="0" MaxLength="5" Width="80"
                                                    runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="TechnicalBandwidthINTRatioDownstreamValidator" Text="*"
                                                ErrorMessage="Technical Bandwidth INT Ratio Downstream must be filled." ControlToValidate="TechnicalBandwidthINTRatioDownstreamTextBox"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Termination Point
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TechnicalTerminationPointTextBox" runat="server"></asp:TextBox></td></tr></table></fieldset>
                            </td>
                            <td valign="top" width="450">
                                <fieldset>
                                    <legend>Dedicated Technical Information</legend><table>
                                        <tr>
                                            <td>
                                                Account MRTG
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TechincalAccountTextBox" runat="server"></asp:TextBox></td></tr><tr>
                                            <td>
                                                Account MRTG Password
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TechincalAccountPasswordTextBox" runat="server"></asp:TextBox></td></tr><tr>
                                            <td>
                                                IP Allocation
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TechnicalIPAllocationTextBox" runat="server"></asp:TextBox></td></tr><tr>
                                            <td>
                                                Transmision
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TechnicalTransmisionTextBox" runat="server"></asp:TextBox></td></tr></table></fieldset>
                            </td>
                        </tr>
                    </table>
                    <table style="width:100%">
                        <tr>
                            <td valign="top" width="450">
                                <fieldset>
                                    <legend>Mikrotik Technical Information</legend><table>
                                        <tr>
                                            <td>
                                                PPTP UserName
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="MikrotikPPTPUserNameTextBox" runat="server"></asp:TextBox></td></tr><tr>
                                            <td>
                                                PPPOE UserName
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="MikrotikPPPOEUserNameTextBox" runat="server"></asp:TextBox></td></tr><tr>
                                            <td>
                                                Hotspot UserName
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="MikrotikHotspotUserNameTextBox" runat="server"></asp:TextBox></td></tr><tr>
                                            <td>
                                                Queue Name DownLink
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="MikrotikQueueNameDownLinkTextBox" runat="server"></asp:TextBox></td></tr><tr>
                                            <td>
                                                Queue Name UpLink
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="MikrotikQueueNameUpLinkTextBox" runat="server"></asp:TextBox></td></tr></table></fieldset>
                            </td>
                            <td valign="top" width="450">
                                <fieldset>
                                    <legend>NAP Technical Information</legend><table>
                                        <tr>
                                            <td>
                                                BGP AS Number
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="BGPASNumberTextBox" runat="server"></asp:TextBox></td></tr><tr>
                                            <td>
                                                BGP IP Address Router
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="BGPIPAddressRouterTextBox" runat="server"></asp:TextBox></td></tr><tr>
                                            <td>
                                                BGP Advertise IP
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="BGPAdvertiseIPTextBox" runat="server"></asp:TextBox></td></tr><tr>
                                            <td>
                                                Type Received Routes
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TypeReceivedRoutesTextBox" runat="server"></asp:TextBox></td></tr></table></fieldset>
                            </td>
                        </tr>
                    </table>
                    <fieldset>
                        <legend>Collocation Service</legend><table>
                            <tr>
                                <td>
                                    Collocation Rack No.
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="CollocationRackNoTextBox" runat="server"></asp:TextBox></td></tr><tr>
                                <td>
                                    Collocation Server Position No.
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="CollocationServerPositionNoTextBox" runat="server"></asp:TextBox></td></tr></table></fieldset>
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
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
