<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.EmployeeAuthor.EmployeeAuthorAdd, App_Web_nhn2uf_h" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
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
                                <table cellpadding="3" cellspacing="0" width="0" border="0">
                                    <tr>
                                        <td>
                                            Employee
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="EmployeeDropDownList" runat="server">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="Employee Must Be Choosed"
                                                Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="EmployeeDropDownList"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Transaction Type
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TransTypeDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="TransTypeDropDownList_SelectedIndexChanged">
                                            </asp:DropDownList>
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
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td align="right">
                                                        Page :
                                                        <asp:Label ID="PageLabel" runat="server"></asp:Label>
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
                                            <table cellpadding="3" cellspacing="1" width="0" border="0">
                                                <tr class="bgcolor_gray">
                                                    <td style="width: 5px" align="center">
                                                        <asp:CheckBox runat="server" ID="AllCheckBox" />
                                                    </td>
                                                    <td style="width: 5px" class="tahoma_11_white" align="center">
                                                        <b>No.</b>
                                                    </td>
                                                    <td style="width: 100px" class="tahoma_11_white" align="center">
                                                        <b>Transaction Type</b>
                                                    </td>
                                                    <td style="width: 150px" class="tahoma_11_white" align="center">
                                                        <b>Transaction Type Name</b>
                                                    </td>
                                                    <td style="width: 100px" class="tahoma_11_white" align="center">
                                                        <b>Account</b>
                                                    </td>
                                                    <td style="width: 150px" class="tahoma_11_white" align="center">
                                                        <b>Account Name</b>
                                                    </td>
                                                </tr>
                                                <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr id="RepeaterListTemplate" runat="server">
                                                            <td align="center">
                                                                <asp:CheckBox runat="server" ID="ListCheckBox" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Literal runat="server" ID="TransTypeLiteral"></asp:Literal>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Literal runat="server" ID="TransTypeNameLiteral"></asp:Literal>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Literal runat="server" ID="AccountLiteral"></asp:Literal>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Literal runat="server" ID="AccountNameLiteral"></asp:Literal>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="GrabAllCheckBox" runat="server" Text="Grab data from all pages." />
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td align="right">
                                                        Page :
                                                        <asp:Label ID="PageLabel2" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
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
