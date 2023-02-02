<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.POS.InternetTable.InternetTableAdd, App_Web_jpb2esu7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript">
        function HarusAngka(x) { if (isNaN(x.value)) x.value = ""; }
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
                                Floor Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="FloorTypeDropDownList">
                                    <asp:ListItem Text="Internet" Value="Internet" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Cafe" Value="Cafe"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Table Number
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TableNmbrTextBox" Width="120" MaxLength="9"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="TableNmbrRequiredFieldValidator" runat="server" ErrorMessage="Table Number Must Be Filled"
                                    Text="*" ControlToValidate="TableNmbrTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Floor Number
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="FloorNmbrTextBox" Width="120" MaxLength="9"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="FloorNmbrRequiredFieldValidator" runat="server" ErrorMessage="Floor Number Must Be Filled"
                                    Text="*" ControlToValidate="FloorNmbrTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Status
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="StatusDropDownList" runat="server">
                                    <asp:ListItem Text="Available" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Booking" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Available" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                fg Active
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="fgActiveCheckBox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Position X
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="positionXTextBox" Width="120" MaxLength="9"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="positionXRequiredFieldValidator" runat="server" ErrorMessage="Position X Must Be Filled"
                                    Text="*" ControlToValidate="positionXTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Position Y
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="positionYTextBox" Width="120" MaxLength="9"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="positionYRequiredFieldValidator" runat="server" ErrorMessage="Position Y Must Be Filled"
                                    Text="*" ControlToValidate="positionYTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
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
