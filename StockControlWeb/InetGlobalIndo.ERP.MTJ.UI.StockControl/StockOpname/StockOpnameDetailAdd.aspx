<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="StockOpnameDetailAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.StockOpname.StockOpnameDetailAdd" %>

<%@ Register src="../ProductPicker.ascx" tagname="ProductPicker" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript">
        function Calculate(_prmQtySystemTextBox, _prmQtyActualTextBox, _prmQtyOpnameTextBox)
        {
           _prmQtyOpnameTextBox.value = parseFloat(_prmQtyActualTextBox.value) - parseFloat(_prmQtySystemTextBox.value);
        }
    </script>

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
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td colspan="3">
                                <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Product
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <%--<asp:DropDownList ID="ProductDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ProductDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="ProductCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Product Must Be Filled" Text="*" ControlToValidate="ProductDropDownList">
                                </asp:CustomValidator>   --%>                             
                                <uc1:ProductPicker ID="ProductPicker1" runat="server" />                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Location
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="LocationDropDownList" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Location Must Be Filled" Text="*" ControlToValidate="LocationDropDownList">
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Qty System
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="QtySystemTextBox" Width="150" BackColor="#CCCCCC"
                                    MaxLength="18"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QtySystemRequiredFieldValidator" runat="server" ErrorMessage="Qty System Must Be Filled"
                                    Text="*" ControlToValidate="QtySystemTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Qty Actual
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="QtyActualTextBox" Width="150" MaxLength="18"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QtyActualRequiredFieldValidator" runat="server" ErrorMessage="Qty Actual Must Be Filled"
                                    Text="*" ControlToValidate="QtyActualTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Qty Opname
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="QtyOpnameTextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Unit
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="UnitTextBox" Width="210" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>
                                <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                characters left
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
