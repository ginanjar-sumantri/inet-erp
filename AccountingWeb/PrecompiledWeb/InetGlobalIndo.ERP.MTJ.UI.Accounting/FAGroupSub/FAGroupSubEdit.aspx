<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.FASubGroup.FAGroupSubEdit, App_Web_p6ocnxdl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
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
                                Fixed Asset Sub Group Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="FAGroupSubCodeTextBox" Width="120" MaxLength="10" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fixed Asset Sub Group Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="FAGroupSubNameTextBox" Width="350" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="FAGroupSubRequiredFieldValidator" runat="server"
                                    ErrorMessage="Fixed Asset Sub Group Name Must Be Filled" Text="*" ControlToValidate="FAGroupSubNameTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fixed Asset Group
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="FAGroupDDL">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Moving
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="MovingCheckBox" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Process
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="ProcessCheckbox" />
                            </td>
                        </tr>
                        <tr runat="server" id="EnableCodeCounter">
                            <td>
                                Code Counter
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CodeCounterTextBox" Width="120" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="EnableLastCounter">
                            <td>
                                Last Counter
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="LastCounterTextBox" Width="100" MaxLength="7"></asp:TextBox>
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
