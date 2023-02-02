<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="CustomerAdd.aspx.cs" Inherits="VTSWeb.UI.CustomerAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <table>
        <tr>
            <td class="tahoma_14_black">
                <b>
                    <asp:Literal ID="PageTitleLiteral" runat="server" /></b>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="WarningLabel" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="3" cellspacing="0" width="0" border="0">
                    <tr>
                        <td>
                            Company Code
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CustomerCodeTextBox" MaxLength="5" Width="250"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CustomerCodeRequiredFieldValidator" runat="server"
                                ErrorMessage="Company Code Must Be Filled" Text="*" ControlToValidate="CustomerCodeTextBox"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Company Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CustomerNameTextBox" Width="250" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CustomerNameRequiredFieldValidator" runat="server"
                                ErrorMessage="Company Name Must Be Filled" Text="*" ControlToValidate="CustomerNameTextBox"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            Type
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="CustTypeDropDownList" runat="server">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustTypeCustomValidator" runat="server" ControlToValidate="CustTypeDropDownList"
                                ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="Type Must Be Chosen"></asp:CustomValidator>
                        </td>
                    </tr>
                </table>
                <table cellpadding="3" cellspacing="0" width="0" border="0">
                    <tr>
                        <td width="120" colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="3">
                            <b>Company Info</b>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Address 1
                        </td>
                        <td valign="top">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CustomerAddressTextBox" runat="server" Width="300" Height="100"
                                TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CustomerAddressRequiredFieldValidator" runat="server"
                                ErrorMessage="Company Address Must Be Filled" Text="*" ControlToValidate="CustomerAddressTextBox"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Address 2
                        </td>
                        <td valign="top">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CustomerAddress2TextBox" runat="server" Width="300" Height="100"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            City
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="CityDropDownList" runat="server">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CityCustomValidator" runat="server" ControlToValidate="CityDropDownList"
                                ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="City Must Be Chosen"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Zip Code
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CustomerZipCodeTextBox" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Phone
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CustomerPhoneTextBox" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Fax
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CustomerFaxTextBox" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Email
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CustomerEmailTextBox" runat="server" Width="300"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="EmailRequiredFieldValidator" runat="server" ErrorMessage="Email Must Be Filled"
                                Text="*" ControlToValidate="CustomerEmailTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            <%--<asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                                                                ErrorMessage="Email Not Valid" Text="*" ControlToValidate="EmailTextBox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style14" colspan="3">
                            Contact Info
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;Email
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CustomerContactMailTextBox" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CustomerContNameTextBox" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;Title
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CustomerContTitleTextBox" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;Hp
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CustomerContHpTextBox" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            FgActive
                        </td>
                        <td valign="top">
                            :
                        </td>
                        <td>
                            <asp:CheckBox ID="CustomerFgActiveChecked" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
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
            </td>
        </tr>
    </table>
</asp:Content>
