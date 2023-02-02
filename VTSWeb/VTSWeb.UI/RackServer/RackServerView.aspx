<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="RackServerView.aspx.cs" Inherits="VTSWeb.UI.RackServerView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel DefaultButton="EditButton" ID="Panel1" runat="server">
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
                   <%-- <fieldset>
                        <legend>Header</legend>--%>
                        <table>
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td>
                                                Rack Code
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="RackCodeTextBox" Width="150" MaxLength="100" BackColor="#CCCCCC"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Rack Name
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="RackNameTextBox" Width="300" MaxLength="200" BackColor="#CCCCCC"
                                                    ReadOnly="True"></asp:TextBox>
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
                                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" MaxLength="200" BackColor="#CCCCCC"
                                                    ReadOnly="True"></asp:TextBox>
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
                                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" CausesValidation="false" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    <%--</fieldset>--%>
                </td>
            </tr>
          <%--  <tr>
                <td>
                    <fieldset>
                        <legend>Detail</legend>
                        <table>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="AddButton" runat="server" CausesValidation="False" OnClick="AddButton_Click" />
                                    &nbsp;<asp:ImageButton ID="DeleteButton" runat="server" CausesValidation="False"
                                        OnClick="DeleteButton_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table border="0" cellpadding="3" cellspacing="1" width="0">
                                        <tr class="bgcolor_gray">
                                            <td>
                                                <asp:CheckBox ID="AllCheckBox" runat="server" />
                                            </td>
                                            <td align="center" class="tahoma_11_white" style="width: 5px">
                                                <b>No.</b>
                                            </td>
                                            <td align="center" class="tahoma_11_white" style="width: 100px">
                                                <b>Action</b>
                                            </td>
                                            <td align="center" class="tahoma_11_white" style="width: 200px">
                                                <b>Rack Box Name</b>
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="RackBoxListRepeater" runat="server" OnItemDataBound="RackBoxListRepeater_ItemDataBound">
                                            <ItemTemplate>
                                                <tr id="RepeaterItemTemplate" runat="server">
                                                    <td align="center">
                                                        <asp:CheckBox ID="ListCheckBox" runat="server" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal ID="NoLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="0">
                                                            <tr>
                                                                <td style="padding-left: 4px">
                                                                    <asp:ImageButton ID="EditButton" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal ID="RackBoxNameLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr class="bgcolor_gray">
                                            <td colspan="4" style="width: 1px">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>--%>
        </table>
        <%-- <table>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="Panel">
                        <fieldset>
                            <legend>Detail Add</legend>
                            <table cellpadding="3" cellspacing="0" width="0" border="0">
                                <tr>
                                    <td colspan="3">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Rack Box Name
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="RackBoxNameTextBox" runat="server" Width="300"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="NameRequiredFieldValidator0" runat="server" ControlToValidate="RackBoxNameTextBox"
                                            Display="Dynamic" ErrorMessage="Rack Box Name Must Be Filled" Text="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <b>
                                            <asp:Label runat="server" ID="WarningDetailLabel"></asp:Label></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <table cellpadding="3" cellspacing="0" border="0" width="0">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="CancelButton2" runat="server" CausesValidation="False" OnClick="CancelButton2_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ResetButton" runat="server" CausesValidation="False" OnClick="ResetButton_Click" />
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
        </table>--%>
    </asp:Panel>
</asp:Content>
