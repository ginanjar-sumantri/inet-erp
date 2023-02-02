<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.POS.Synchronize.Synchronize, App_Web_jtqsijmk" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <style type="text/css">
        .width
        {
            width: 120px;
        }
    </style>
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SyncButton">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
        <asp:Timer ID="TimerRefresher" runat="server" OnTick="TimerRefresher_Tick">
        </asp:Timer>
        <asp:HiddenField ID="StatusHiddenField" runat="server" />
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
                    <fieldset>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
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
                                                Server Source
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ServerSourceDDL" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ServerSourceDDL_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:CustomValidator ID="ServerSourceCustomValidator" runat="server" ErrorMessage="Server Source Must Be Filled"
                                                    Text="*" ControlToValidate="ServerSourceDDL" Display="Dynamic" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ServerSourceLocationTextBox" runat="server" Width="200" ReadOnly="true"
                                                    BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Server Destination
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ServerDestinationDDL" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ServerDestinationDDL_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:CustomValidator ID="ServerDestinationCustomValidator" runat="server" ErrorMessage="Server Destination Must Be Filled"
                                                    Text="*" ControlToValidate="ServerDestinationDDL" Display="Dynamic" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ServerDestinationLocationTextBox" runat="server" Width="200" ReadOnly="true"
                                                    BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Synchronize Data
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="MemberCheckBox" runat="server" Text="Member" Checked="true" />
                                            </td>
                                            <td>
                                                <asp:Label ID="MemberLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ProductCheckBox" runat="server" Text="Product" Checked="true" />
                                            </td>
                                            <td>
                                                <asp:Label ID="ProductLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="SupplierCheckBox" runat="server" Text="Supplier" Checked="true" />
                                            </td>
                                            <td>
                                                <asp:Label ID="SupplierLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="AirLineCheckBox" runat="server" Text="AirLine" Checked="true" />
                                            </td>
                                            <td>
                                                <asp:Label ID="AirLineLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="HotelCheckBox" runat="server" Text="Hotel" Checked="true" />
                                            </td>
                                            <td>
                                                <asp:Label ID="HotelLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ShippingCheckBox" runat="server" Text="Shipping" Checked="true" />
                                            </td>
                                            <td>
                                                <asp:Label ID="ShippingLabel" runat="server"></asp:Label>
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
                                                <asp:ImageButton ID="SyncButton" runat="server" OnClick="SyncButton_Click" />
                                            </td>
                                            <%--<td>
                                                <asp:ImageButton ID="PrintLogButton" runat="server" OnClick="PrintLogButton_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="PrintManualButton" runat="server" OnClick="PrintManualButton_Click" />
                                            </td>--%>
                                        </tr>
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
                        <legend>Report</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td style="width: 120px;">
                                    <asp:Literal runat="server" ID="StartDateLiteral"> Start Date </asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="StartDateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                    <input type="button" id="Button1" value="..." onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_StartDateTextBox,'yyyy-mm-dd',this)" />
                                </td>
                                <td style="width: 120px;">
                                    <asp:Literal runat="server" ID="EndDateLiteral"> End Date </asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="EndDateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                    <input type="button" id="Button2" value="..." onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_EndDateTextBox,'yyyy-mm-dd',this)" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="ViewButton" runat="server" OnClick="ViewButton_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="PrintLogButton" runat="server" OnClick="PrintLogButton_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="PrintManualButton" runat="server" OnClick="PrintManualButton_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Log</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Synchronize Data</b>
                                            </td>
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Start Time</b>
                                            </td>
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>End Time</b>
                                            </td>
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Status</b>
                                            </td>
                                            <td style="width: 300px" class="tahoma_11_white" align="center">
                                                <b>Remark</b>
                                            </td>
                                        </tr>
                                        <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                                            <ItemTemplate>
                                                <tr id="RepeaterItemTemplate" runat="server">
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="DataLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="StartTimeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="EndTimeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="StatusLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="RemarkLiteral"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr class="bgcolor_gray">
                                            <td style="width: 1px" colspan="5">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Manual Synchronize</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Table Relation</b>
                                            </td>
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Table Name</b>
                                            </td>
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Value</b>
                                            </td>
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Insert Date</b>
                                            </td>
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Update Date</b>
                                            </td>
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Status</b>
                                            </td>
                                            <td style="width: 300px" class="tahoma_11_white" align="center">
                                                <b>Remark</b>
                                            </td>
                                        </tr>
                                        <asp:Repeater runat="server" ID="ListRepeaterManual" OnItemDataBound="ListRepeaterManual_ItemDataBound">
                                            <ItemTemplate>
                                                <tr id="RepeaterItemTemplate" runat="server">
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="RelationLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="NameLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="ValueLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="InsertDateLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="UpdateDateLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="StatusLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="RemarkLiteral"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr class="bgcolor_gray">
                                            <td style="width: 1px" colspan="5">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div>
        <asp:Panel runat="server" ID="ReportPanel" Visible="false">
            <table cellpadding="3" cellspacing="0" width="100%" border="0">
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server">
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
