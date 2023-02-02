<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="SupplierInvConsignmentDetailAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierInvConsignment.SupplierInvConsignmentDetailAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>

    <script language="javascript" type="text/javascript">
        function Count(_prmQty, _prmPrice, _prmAmount, _prmDecimalPlace) {
            var _amount = parseInt(_prmQty.value) * parseFloat(GetCurrency2(_prmPrice.value, _prmDecimalPlace.value));
            _prmAmount.value = (_amount == 0 ? "0" : FormatCurrency2(_amount, _prmDecimalPlace.value));
            _prmPrice.value = (_prmPrice.value == 0) ? "0" : FormatCurrency2(_prmPrice.value, _prmDecimalPlace.value);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="BackButton">
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
                        <asp:ScriptManager ID="scriptMgr" runat="server" EnablePartialRendering="true" />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <tr>
                                    <td>
                                        SJ Type
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="SJTypeDropDownList" AutoPostBack="True" OnSelectedIndexChanged="SJTypeDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="SJTypeCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                            ErrorMessage="SJ Type Must Be Filled" Text="*" ControlToValidate="SJTypeDropDownList"> </asp:CustomValidator>
                                        <asp:HiddenField ID="TransNmbrHiddenField" runat="server" />
                                        <asp:HiddenField ID="SuppCodeHiddenField" runat="server" />
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
                                        <asp:TextBox ID="ProductCodeTextBox" runat="server" AutoPostBack="True" OnTextChanged="ProductCodeTextBox_TextChanged"></asp:TextBox>
                                        <asp:Button ID="SearchButton" runat="server" CausesValidation="false" Text="..." />
                                    </td>
                                </tr>
                                <%--                                <tr>
                                    <td>
                                        SJ No
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="SJNoDropDownList">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="SJNoCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                            ErrorMessage="SJ No Must Be Filled" Text="*" ControlToValidate="SJNoDropDownList">
                                        </asp:CustomValidator>
                                    </td>
                                </tr>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <%--<td>
                                <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                            </td>--%>
                            <%--                            <td>
                                <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                            </td>--%>
                            <td>
                                <asp:ImageButton ID="BackButton" runat="server" CausesValidation="False" OnClick="BackButton_Click" />
                            </td>
                            <%--<td>
                                <asp:ImageButton ID="ResetButton" runat="server" CausesValidation="False" OnClick="ResetButton_Click" />
                            </td>--%>
                            <td>
                                <asp:ImageButton ID="GoImageButton" runat="server" OnClick="GoImageButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                    <tr>
                                        <td align="right">
                                            <asp:Panel DefaultButton="DataPagerButton" ID="DataPagerPanel" runat="server">
                                                <table border="0" cellpadding="2" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="DataPagerButton" runat="server" OnClick="DataPagerButton_Click" />
                                                        </td>
                                                        <td valign="middle">
                                                            <b>Page :</b>
                                                        </td>
                                                        <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater" runat="server" OnItemCommand="DataPagerTopRepeater_ItemCommand"
                                                            OnItemDataBound="DataPagerTopRepeater_ItemDataBound">
                                                            <ItemTemplate>
                                                                <td>
                                                                    <asp:LinkButton ID="PageNumberLinkButton" runat="server"></asp:LinkButton>
                                                                    <asp:TextBox Visible="false" ID="PageNumberTextBox" runat="server" Width="30px"></asp:TextBox>
                                                                </td>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="Label1" CssClass="warning"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="CheckHidden" runat="server" />
                                <asp:HiddenField ID="TempHidden" runat="server" />
                                <table cellpadding="3" cellspacing="1" width="100%" border="0">
                                    <tr class="bgcolor_gray">
                                        <td style="width: 5px" class="tahoma_11_white" align="center">
                                            <b>No.</b>
                                        </td>
                                        <td style="width: 60px" class="tahoma_11_white" align="center">
                                            <b>Action</b>
                                        </td>
                                        <td style="width: 200px" class="tahoma_11_white" align="center">
                                            <b>SJ No</b>
                                        </td>
                                        <td style="width: 100px" class="tahoma_11_white" align="center">
                                            <b>SJ Date</b>
                                        </td>
                                        <td style="width: 200px" class="tahoma_11_white" align="center">
                                            <b>File Nmbr</b>
                                        </td>
                                        <td style="width: 100px" class="tahoma_11_white" align="center">
                                            <b>Product Code</b>
                                        </td>
                                        <td style="width: 300px" class="tahoma_11_white" align="center">
                                            <b>Product Name</b>
                                        </td>
                                        <td style="width: 50px" class="tahoma_11_white" align="center">
                                            <b>Qty</b>
                                        </td>
                                        <td style="width: 100px" class="tahoma_11_white" align="center">
                                            <b>Unit</b>
                                        </td>
                                    </tr>
                                    <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound"
                                        OnItemCommand="ListRepeater_ItemCommand">
                                        <ItemTemplate>
                                            <tr id="RepeaterItemTemplate" runat="server">
                                                <td align="center">
                                                    <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                </td>
                                                <td align="left">
                                                    <table cellpadding="0" cellspacing="0" width="0" border="0">
                                                        <tr>
                                                            <td style="padding-left: 4px">
                                                                <asp:ImageButton runat="server" ID="AddButton" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="center">
                                                    <asp:Literal runat="server" ID="SJNoLiteral"></asp:Literal>
                                                </td>
                                                <td align="center">
                                                    <asp:Literal runat="server" ID="SJDateLiteral"></asp:Literal>
                                                </td>
                                                <td align="center">
                                                    <asp:Literal runat="server" ID="FileNmbrLiteral"></asp:Literal>
                                                </td>
                                                <td align="center">
                                                    <asp:Literal runat="server" ID="ProductCodeLiteral"></asp:Literal>
                                                </td>
                                                <td align="center">
                                                    <asp:Literal runat="server" ID="ProductNameLiteral"></asp:Literal>
                                                </td>
                                                <td align="right">
                                                    <asp:Literal runat="server" ID="QtyLiteral"></asp:Literal>
                                                </td>
                                                <td align="center">
                                                    <asp:Literal runat="server" ID="UnitLiteral"></asp:Literal>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
