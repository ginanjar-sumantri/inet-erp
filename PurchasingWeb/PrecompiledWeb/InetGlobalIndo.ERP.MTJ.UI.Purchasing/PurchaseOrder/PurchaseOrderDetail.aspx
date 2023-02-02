<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseOrder.PurchaseOrderDetail, App_Web_qefmmyl6" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel4" DefaultButton="EditButton">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <asp:Panel ID="ReasonPanel" runat="server" Visible="false">
                <tr>
                    <td>
                        <fieldset>
                            <legend>Reason</legend>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Insert Reason UnPosting
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="ReasonTextBox" runat="server" MaxLength="500" Width="400px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="ReasonRequiredFieldValidator" runat="server" Text="*"
                                            ErrorMessage="Reason Text Box Must Be Filled" ControlToValidate="ReasonTextBox"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="YesButton" runat="server" Text="Yes" OnClick="YesButton_OnClick" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="NoButton" runat="server" Text="No" OnClick="NoButton_OnClick" CausesValidation="False" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </asp:Panel>
            <tr>
                <td>
                    <asp:Label runat="server" ID="WarningLabel3" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr runat="server" class="warning" id="ApproveRow" visible="false">
                <td>
                    <asp:Label ID="ApproveForceLabel" runat="server" Text="Are you sure want to approve this transaction ?"></asp:Label>&nbsp;
                    <asp:Button ID="ApproveForceButton" runat="server" Text="Yes" Width="50" OnClick="ApproveForceButton_Click" />&nbsp;
                    <asp:Button ID="NotApproveForceButton" runat="server" Text="No" Width="50" OnClick="NotApproveForceButton_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
                        <fieldset>
                            <legend>Header</legend>
                            <table cellpadding="3" cellspacing="0" width="0" border="0">
                                <tr>
                                    <td colspan="3">
                                        <asp:Label runat="server" ID="Label" CssClass="warning"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Trans No.
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="TransNoTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                            Width="160"></asp:TextBox>
                                        <%--<asp:DropDownList ID="RevisiDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RevisiDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>--%>
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
                                        <asp:TextBox runat="server" ID="FileNmbrTextBox" Width="160" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                        <asp:TextBox runat="server" ID="DateTextBox" Width="100" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        Term
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TermTextBox" runat="server" Width="200" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Supplier Reference
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="SupplierPONoTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"
                                            Width="200"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        Shipment
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ShipmentTextBox" runat="server" Width="200" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Supplier
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="SupplierTextBox" Width="300" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        Shipment Name
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ShipmentNameTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"
                                            Width="200"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Attn
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="AttnTextBox" Width="200" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        Shipping Curr / Rate
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="ShippingCurrCodeTextBox" Width="50" BackColor="#CCCCCC"
                                            ReadOnly="True"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="ShippingCurrRateTextBox" Width="100" BackColor="#CCCCCC"
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Delivery Site
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="DeliveryTextBox" runat="server" Width="200" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        Shipping Forex
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ShippingForexTextBox" runat="server" Width="100" BackColor="#CCCCCC"
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Delivery Date
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="DeliveryDateTextBox" Width="100" ReadOnly="true"
                                            BackColor="#CCCCCC"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        Subject
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="SubjectTextBox" Width="350" MaxLength="500" BackColor="#CCCCCC"
                                            ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Currency / Rate
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="CurrCodeTextBox" Width="50" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="ForexRateTextBox" Width="100" BackColor="#CCCCCC"
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td valign="top" rowspan="3">
                                        Remark
                                    </td>
                                    <td valign="top" rowspan="3">
                                        :
                                    </td>
                                    <td valign="top" rowspan="3">
                                        <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" MaxLength="500" Height="80"
                                            TextMode="MultiLine" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td colspan="5">
                                        <table width="0">
                                            <tr class="bgcolor_gray" height="20">
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>DP %</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>DP Forex</b>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        DP
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td colspan="5">
                                        <table>
                                            <tr>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="DPPercentTextBox" runat="server" Width="100" BackColor="#CCCCCC"
                                                        ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="DPForexTextBox" runat="server" Width="100" BackColor="#cccccc" ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td colspan="5">
                                        <table>
                                            <tr class="bgcolor_gray" height="20">
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>Currency</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>Base Forex</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>Discount %</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>Discount Forex</b>
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
                                    <td colspan="5">
                                        <table>
                                            <tr>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="CurrTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                                    </asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="BaseForexTextBox" runat="server" Width="100" BackColor="#CCCCCC"
                                                        ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="DiscTextBox" runat="server" Width="100" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="DiscForexTextBox" runat="server" Width="100" BackColor="#CCCCCC"
                                                        ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td colspan="5">
                                        <table width="0">
                                            <tr class="bgcolor_gray" height="20">
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>PPh %</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>PPh Forex</b>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        PPh
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td colspan="5">
                                        <table>
                                            <tr>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="PPHPercentTextBox" runat="server" Width="100" BackColor="#CCCCCC"
                                                        ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="PPHForexTextBox" runat="server" Width="100" BackColor="#cccccc"
                                                        ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td colspan="5">
                                        <table width="0">
                                            <tr class="bgcolor_gray" height="20">
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>PPN %</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>PPN Forex</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>Total Forex</b>
                                                </td>
                                            </tr>
                                        </table>
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
                                        <table>
                                            <tr>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="PPNPercentTextBox" runat="server" Width="100" BackColor="#CCCCCC"
                                                        ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="PPNForexTextBox" runat="server" Width="100" BackColor="#cccccc"
                                                        ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="TotalForexTextBox" runat="server" Width="100" BackColor="#cccccc"
                                                        ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
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
                                        <asp:Label ID="StatusLabel" runat="server"></asp:Label>
                                        <asp:HiddenField ID="StatusHiddenField" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        <table cellpadding="3" cellspacing="0" border="0" width="0">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                                                    &nbsp;<asp:ImageButton ID="BackButton" runat="server" OnClick="BackButton_Click" />
                                                    &nbsp;<asp:ImageButton ID="ActionButton" runat="server" OnClick="ActionButton_Click" />
                                                    <%--&nbsp;<asp:ImageButton ID="ReviseButton" runat="server" OnClick="ReviseButton_Click" />--%>
                                                    <%--&nbsp;<asp:ImageButton ID="DeleteHeaderButton" runat="server" OnClick="DeleteHeaderButton_Click" />--%>
                                                    &nbsp;<asp:ImageButton ID="PreviewButton" runat="server" OnClick="PreviewButton_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                </td>
            </tr>
            <!--<tr>
            <td>
                <div class="tabber">
                    <div class="tabbertab">
                        <h2>
                            Detail 2</h2>
                        
                    </div>
                    <div class="tabbertab">
                        <h2>
                            Detail</h2>
                        
                    </div>
                </div>
            </td>
        </tr>-->
            <tr>
                <td>
                    <fieldset>
                        <legend>Purchase Request Reference List</legend>
                        <asp:Panel runat="server" ID="Panel3" DefaultButton="AddButton2">
                            <table cellpadding="3" cellspacing="0" width="0" border="0">
                                <tr>
                                    <td>
                                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="AddButton2" runat="server" OnClick="AddButton2_Click" />
                                                    <%--</td>
                                                    <td>--%>
                                                    &nbsp;<asp:ImageButton ID="DeleteButton2" runat="server" OnClick="DeleteButton2_Click" />
                                                    &nbsp;<asp:ImageButton ID="GenerateDetailButton" runat="server" OnClick="GenerateDetailButton_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="WarningLabel2" CssClass="warning"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="CheckHidden2" runat="server" />
                                        <asp:HiddenField ID="TempHidden2" runat="server" />
                                        <table cellpadding="3" cellspacing="1" width="100%" border="0">
                                            <tr class="bgcolor_gray">
                                                <td style="width: 5px">
                                                    <asp:CheckBox runat="server" ID="AllCheckBox2" />
                                                </td>
                                                <td style="width: 5px" class="tahoma_11_white" align="center">
                                                    <b>No.</b>
                                                </td>
                                                <%--<td style="width: 100px" class="tahoma_11_white" align="center">
                                                    <b>Action</b>
                                                </td>--%>
                                                <td style="width: 160px" class="tahoma_11_white" align="center">
                                                    <b>Request No.</b>
                                                </td>
                                                <td style="width: 150px" class="tahoma_11_white" align="center">
                                                    <b>Request By</b>
                                                </td>
                                            </tr>
                                            <asp:Repeater runat="server" ID="ListRepeater2" OnItemDataBound="ListRepeater2_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr id="RepeaterItemTemplate" runat="server">
                                                        <td align="center">
                                                            <asp:CheckBox runat="server" ID="ListCheckBox2" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Literal runat="server" ID="NoLiteral2"></asp:Literal>
                                                        </td>
                                                        <%--<td align="left">
                                                            <table cellpadding="0" cellspacing="0" width="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton runat="server" ID="ViewButton2" />
                                                                    </td>
                                                                    <td style="padding-left: 4px">
                                                                        <asp:ImageButton runat="server" ID="EditButton2" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>--%>
                                                        <%--<td align="left">
                                                            <asp:Literal runat="server" ID="ProductLiteral"></asp:Literal>
                                                        </td>--%>
                                                        <td align="center">
                                                            <asp:Literal runat="server" ID="RequestNoLiteral"></asp:Literal>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Literal runat="server" ID="RequestByLiteral"></asp:Literal>
                                                        </td>
                                                        <%--<td align="left">
                                                            <asp:Literal runat="server" ID="UnitLiteral"></asp:Literal>
                                                        </td>--%>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
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
                        <legend>Detail</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="CheckHidden" runat="server" />
                                    <asp:HiddenField ID="TempHidden" runat="server" />
                                    <asp:HiddenField ID="DescriptionHiddenField" runat="server" />
                                    <table cellpadding="3" cellspacing="1" width="100%" border="0">
                                        <tr class="bgcolor_gray" style="height: 20px">
                                            <td style="width: 5px">
                                                <asp:CheckBox runat="server" ID="AllCheckBox" />
                                            </td>
                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td style="width: 60px" class="tahoma_11_white" align="center">
                                                <b>Action</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Product Code</b>
                                            </td>
                                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                                <b>Product Name</b>
                                            </td>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>Qty</b>
                                            </td>
                                            <td style="width: 80px" class="tahoma_11_white" align="center">
                                                <b>Unit</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Price</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Amount</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Disc</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Netto</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Done Closing</b>
                                            </td>
                                        </tr>
                                        <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound"
                                            OnItemCommand="ListRepeater_ItemCommand">
                                            <ItemTemplate>
                                                <tr id="RepeaterItemTemplate" runat="server">
                                                    <td align="center">
                                                        <asp:CheckBox runat="server" ID="ListCheckBox" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <table cellpadding="0" cellspacing="0" width="0" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton runat="server" ID="ViewButton" />
                                                                </td>
                                                                <td style="padding-left: 4px">
                                                                    <asp:ImageButton runat="server" ID="EditButton" />
                                                                </td>
                                                                <td style="padding-left: 4px">
                                                                    <asp:ImageButton runat="server" ID="CloseButton" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="ProductCodeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="ProductNameLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="QtyLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="UnitLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="PriceLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="AmountLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="DiscLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="NettoLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="DoneClosingLiteral"></asp:Literal>
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
    <asp:Panel runat="server" ID="Panel2">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" ShowPrintButton="true" ShowZoomControl="true"
                        ZoomMode="Percent" ZoomPercent="100" runat="server">
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
