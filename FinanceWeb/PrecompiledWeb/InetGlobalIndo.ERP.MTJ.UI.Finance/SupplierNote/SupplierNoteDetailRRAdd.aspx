<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierNote.SupplierNoteDetailRRAdd, App_Web_9oeaajfn" %>

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
                        <tr>
                            <td>
                                RR No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="RRFileNoTextBox" runat="server" MaxLength="30" Width="160"></asp:TextBox>
                                <asp:HiddenField ID="RRNoHIddenField" runat="server"></asp:HiddenField>
                                <asp:Button ID="btnSearchRRNo" runat="server" Text="..." />
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
                                <asp:TextBox ID="RemarkTextBox" runat="server" MaxLength="30" Width="200"></asp:TextBox>
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
                        <legend>RR Reference List</legend>
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
                                                    <b>RR No.</b>
                                                </td>
                                                <td style="width: 300px" class="tahoma_11_white" align="center">
                                                    <b>Remark</b>
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
                                                            <asp:Literal runat="server" ID="RRNoLiteral"></asp:Literal>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Literal runat="server" ID="RemarkLiteral"></asp:Literal>
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
