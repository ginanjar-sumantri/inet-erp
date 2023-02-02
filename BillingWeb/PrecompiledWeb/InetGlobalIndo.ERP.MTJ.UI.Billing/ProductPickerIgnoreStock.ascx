<%@ control language="C#" autoeventwireup="true" inherits="ProductPickerIgnoreStock, App_Web_ffowzz1w" %>
<asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
<asp:TextBox ID="productCode" Width="80" runat="server" OnTextChanged="productCode_TextChanged"
    AutoPostBack="true"></asp:TextBox>
<asp:Button ID="btnSearch" runat="server" Text="..." />
<asp:TextBox ID="productName" Width="220" runat="server"></asp:TextBox>
<asp:RequiredFieldValidator runat="server" ID="validatorProduct"
    ControlToValidate="productCode" Text="*" ErrorMessage="Product Must Be Filled"></asp:RequiredFieldValidator>
<asp:HiddenField ID="productPickerMode" Value="productNoStock" runat="server" />