<%@ page language="C#" masterpagefile="~/SMSMasterPage.master" autoeventwireup="true" inherits="SMS.SMSWeb.Message.ContactsAdd, App_Web_tpn8tx_m" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<style type="text/css">
    .formRow {clear:both;padding:3px 3px 3px 3px;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAtas" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContentTitle"
    runat="Server">
    <asp:Literal ID="SubPageTitleLiteral" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
    <asp:ScriptManager runat="server" ID="ScriptManager"></asp:ScriptManager>
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
        <asp:Label runat="server" ID="WarningLabel" ForeColor="Red" Font-Bold="true"></asp:Label>
                    <div class="formRow">
                        <div style="float:left; width:120px">Group Name</div>
                        <div style="float:left; width:10px">:</div>
                        <div style="float:left; width:10px">
                            <asp:TextBox runat="server" ID="GroupNameTextBox" Width="250" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="GroupNameRequiredFieldValidator" runat="server" ErrorMessage="Group Name Must Be Filled"
                            Text="*" ControlToValidate="GroupNameTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>                    
                    <div class="formRow" style="height:20px">&nbsp;</div>
                    <asp:Panel ID="Panel2" DefaultButton="GoImageButton" runat="server">
                        <table cellpadding="0" cellspacing="2" border="0">
                            <tr>
                                <td>
                                    <b>Quick Search :</b>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ContactFilterDropDownList" runat="server">
                                        <asp:ListItem Value="Company" Text="Company"></asp:ListItem>
                                        <asp:ListItem Value="Religion" Text="Religion"></asp:ListItem>
                                        <asp:ListItem Value="City" Text="City"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="KeywordTextBox" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:ImageButton ID="GoImageButton" runat="server" OnClick="GoImageButton_Click"
                                        CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:UpdatePanel runat="server" ID="updatePanelCheckList">
                        <ContentTemplate>
                            <asp:HiddenField ID="CheckHidden" runat="server" />
                            <asp:HiddenField ID="TempHidden" runat="server" />
                            <asp:HiddenField ID="AllHidden" runat="server" />
                            <fieldset>                    
                            <legend>
                                Contact List
                            </legend>
                            <asp:Panel DefaultButton="DataPagerButton" ID="DataPagerPanel" runat="server">
                                <div class="formRow">
                                    <div style="float:left">
                                        <asp:CheckBox runat="server" ID="AllCheckBox" Text="Check All" 
                                            AutoPostBack="true" oncheckedchanged="AllCheckBox_CheckedChanged" />
                                        <asp:CheckBox runat="server" ID="GrabAllCheckBox" Text="Grab All" />
                                    </div>
                                    <div style="float:right">
                                        <table>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Button ID="DataPagerButton" runat="server" CausesValidation="false" OnClick="DataPagerButton_Click" />
                                                    <b>Page :</b>
                                                </td>
                                                <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater" runat="server" OnItemCommand="DataPagerTopRepeater_ItemCommand"
                                                    OnItemDataBound="DataPagerTopRepeater_ItemDataBound">
                                                    <ItemTemplate>
                                                        <td>
                                                            <asp:LinkButton ID="PageNumberLinkButton" runat="server" CausesValidation="false"></asp:LinkButton>
                                                            <asp:TextBox Visible="false" ID="PageNumberTextBox" runat="server" Width="30px"></asp:TextBox>
                                                        </td>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div style="clear:both">
                                <asp:CheckBoxList ID="ContactCheckBoxList" runat="server" RepeatColumns="5">
                                </asp:CheckBoxList>
                            </div>
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="formRow" style="font-size:10px">
                    * Use Check All to select all checkbox visible on this page<br />
                    * Use Grab All to get all data regarding filter selected
                    </div>
        <table cellpadding="3" cellspacing="0" border="0" width="0" style="clear:both">
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
    </asp:Panel>
</asp:Content>
