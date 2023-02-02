<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="ProductTypeEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.ProductTypeEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
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
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                Product Type Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ProductTypeCodeTextBox" Width="100" MaxLength="10"
                                    ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Product Type Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ProductTypeNameTextBox" Width="350" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ProductTypeNameRequiredFieldValidator" runat="server"
                                    ErrorMessage="Product Type Name Must Be Filled" Text="*" ControlToValidate="ProductTypeNameTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Product Category
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="CategoryDropDownList" AutoPostBack="True" OnSelectedIndexChanged="CategoryDropDownList_SelectedIndexChanged">
                                    <asp:ListItem Text="[Choose One]" Value="null"></asp:ListItem>
                                    <asp:ListItem Text="Material" Value="Material"></asp:ListItem>
                                    <asp:ListItem Text="Semi FG" Value="Semi FG"></asp:ListItem>
                                    <asp:ListItem Text="Finish Goods" Value="Finish Goods"></asp:ListItem>
                                    <asp:ListItem Text="Spare Part" Value="Spare Part"></asp:ListItem>
                                    <asp:ListItem Text="Subkon" Value="Subkon"></asp:ListItem>
                                    <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CategoryCustomValidator" runat="server" ErrorMessage="Product Category Must Be Filled"
                                    Text="*" ControlToValidate="CategoryDropDownList" Display="Dynamic" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Using Price Group
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="IsUsingPGCheckBox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Using Unique ID
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="IsUsingUniqueIDCheckBox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Stock
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:Label runat="server" ID="StockLabel"></asp:Label>
                                <asp:HiddenField ID="StockHiddenField" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="105px">
                                Send To Kitchen
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="SendToKitchenCheckBox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                With Tax
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="WithTaxCheckBox" runat="server" AutoPostBack="true" OnCheckedChanged="WithTaxCheckBox_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <div id="TaxDiv" runat="server" visible="false">
                                            <fieldset>
                                                <legend>Tax</legend>
                                                <table>
                                                    <tr>
                                                        <td width="95px">
                                                            Tax Type
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="TaxTypeDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="TaxTypeDropDownList_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Tax Percentage
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TaxPercentageTextBox" runat="server"></asp:TextBox>
                                                            %
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                            <table>
                                                <tr>
                                                    <td width="105px">
                                                        Service Charge
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="ServiceChargerTextBox" runat="server"></asp:TextBox>
                                                        %
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="105px">
                                                        Service Charge Calculate
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="ServiceChargesCalculateCheckBox" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
                                <asp:CheckBox ID="FgActiveCheckBox" runat="server" />
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
                                    TextMode="MultiLine"></asp:TextBox>
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
                            <td>
                                <asp:ImageButton ID="ViewDetailButton" runat="server" CausesValidation="False" OnClick="ViewDetailButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="SaveAndViewDetailButton" runat="server" OnClick="SaveAndViewDetailButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
