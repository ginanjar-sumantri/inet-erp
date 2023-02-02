<%@ Page Title="" Language="C#" MasterPageFile="~/SMSAdminMasterPage.master" AutoEventWireup="true" 
CodeFile="BackEndSetting.aspx.cs" Inherits="SMS.BackEndSMSPortal.BackEndSetting.BackEndSetting_BackEndSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript">function HarusAngka(x){if(isNaN(x.value))x.value="0";</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContentTitle" Runat="Server">
BACK END SETTING
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">
    <div style="padding:5px">
        <asp:Label runat="server" ID="WarnningLabel" ForeColor="Red" Font-Bold="true"></asp:Label>
        <div style="clear:both">
            <div style="float:left;width:120px">Masking CID</div>
            <div style="float:left">: &nbsp</div>
            <div style="float:left">
                <asp:TextBox runat="server" ID="MaskingCIDTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="MaskingCIDRequiredFieldValidator"
                Text="*" ErrorMessage="MaskingCID Must Be Filled" ControlToValidate="MaskingCIDTextBox">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div style="clear:both">
            <div style="float:left;width:120px">Masking Price</div>
            <div style="float:left">: &nbsp</div>
            <div style="float:left">
                <asp:TextBox runat="server" ID="MaskingPriceTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="MaskingPriceRequiredFieldValidator"
                Text="*" ErrorMessage="Masking Price Must Be Filled" ControlToValidate="MaskingPriceTextBox">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div style="clear:both">
            <div style="float:left;width:120px">Masking PWD</div>
            <div style="float:left">: &nbsp</div>
            <div style="float:left">
                <asp:TextBox runat="server" ID="MaskingPWDTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="MaskingPWDRequiredFieldValidator"
                Text="*" ErrorMessage="Masking PWD Must Be Filled" ControlToValidate="MaskingPWDTextBox">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div style="clear:both">
            <div style="float:left;width:120px">Masking URL</div>
            <div style="float:left">: &nbsp</div>
            <div style="float:left">
                <asp:TextBox runat="server" ID="MaskingURLTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="MaskingURLRequiredFieldValidator"
                Text="*" ErrorMessage="Masking URL Must Be Filled" ControlToValidate="MaskingURLTextBox">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div style="clear:both">
            <div style="float:left;width:120px">Web Domain Name (Redirect from MWeb)</div>
            <div style="float:left">: &nbsp</div>
            <div style="float:left">
                <asp:TextBox runat="server" ID="WebDomainNameTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="WebDomainNameRequiredFieldValidator"
                Text="*" ErrorMessage="Web Domain Name Must Be Filled" ControlToValidate="WebDomainNameTextBox">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div style="clear:both">
            <asp:Button runat="server" ID="SaveButton" Text="Save" 
                onclick="SaveButton_Click" />
            &nbsp;
            <input type="reset" value="Reset" />
        </div>
        <br /><br />
        <div style="clear:both">
            <div style="float:left;width:180px">Back End Admin User</div>
            <div style="float:left">: &nbsp</div>
            <div style="float:left">
                <asp:TextBox runat="server" ID="BackEndAdminUserTextBox"></asp:TextBox>
            </div>
        </div>
        <div style="clear:both">
            <div style="float:left;width:180px">Back End Admin Password</div>
            <div style="float:left">: &nbsp</div>
            <div style="float:left"><asp:TextBox runat="server" ID="BackEndAdminPasswordTextBox" TextMode="Password"></asp:TextBox></div>
        </div>
        <div style="clear:both">
            <asp:Button runat="server" ID="ChangePasswordButton" Text="Change Password" 
                CausesValidation="false" onclick="ChangePasswordButton_Click" />
        </div>
    </div>
</asp:Content>

