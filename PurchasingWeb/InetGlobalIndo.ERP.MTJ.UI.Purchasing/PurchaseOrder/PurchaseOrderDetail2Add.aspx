<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="PurchaseOrderDetail2Add.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseOrder.PurchaseOrderDetail2Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
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
                        <%--<tr>
                            <td>
                                Product
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="ProductDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ProductDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="ProductCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="ProductDropDownList" ErrorMessage="Product Must Be Filled"
                                    Text="*"></asp:CustomValidator>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                Request No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="RequestFileNoTextBox" runat="server" MaxLength="30" Width="160"></asp:TextBox>
                                <asp:HiddenField ID="RequestNoHiddenField" runat="server"></asp:HiddenField>
                                <asp:Button ID="btnSearchPRNo" runat="server" Text="..." />
                                <asp:RequiredFieldValidator ID="RequestFileNoRequiredFieldValidator" runat="server"
                                    ErrorMessage="Request No Must Be Filled" Text="*" ControlToValidate="RequestFileNoTextBox"
                                    Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Request By
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="RequestByTextBox" runat="server" MaxLength="30" Width="200"></asp:TextBox>
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
                                <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="BackButton" runat="server" CausesValidation="False" OnClick="BackButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Purchase Request Reference List</legend>
                        <asp:Panel runat="server" ID="Panel3">
                            <table cellpadding="3" cellspacing="0" width="0" border="0">
                                <tr>
                                    <td>
                                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="DeleteButton2" runat="server" OnClick="DeleteButton2_Click" />
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
                                                <td style="width: 150px" class="tahoma_11_white" align="center">
                                                    <b>Request No.</b>
                                                </td>
                                                <%--<td style="width: 100px" class="tahoma_11_white" align="center">
                                                    <b>Qty Outstanding</b>
                                                </td>
                                                <td style="width: 100px" class="tahoma_11_white" align="center">
                                                    <b>Unit</b>
                                                </td>--%>
                                                <td style="width: 100px" class="tahoma_11_white" align="center">
                                                    <b>RequestBy</b>
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
                                                        <td align="center">
                                                            <asp:Literal runat="server" ID="RequestNoLiteral"></asp:Literal>
                                                        </td>
                                                        <%--<td align="right">
                                                            <asp:Literal runat="server" ID="QtyOutstandingLiteral"></asp:Literal>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Literal runat="server" ID="UnitLiteral"></asp:Literal>
                                                        </td>--%>
                                                        <td align="left">
                                                            <asp:Literal runat="server" ID="RequestByLiteral"></asp:Literal>
                                                        </td>
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
        </table>
    </asp:Panel>
</asp:Content>
