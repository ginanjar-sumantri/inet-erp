<%@ page language="C#" masterpagefile="~/SMSMasterPage.master" autoeventwireup="true" inherits="SMS.SMSWeb.Message.ContactsEdit, App_Web_gbqxtdk2" %>

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
            <table cellpadding="3" cellspacing="0" width="0" border="0">
                <tr>
                    <td>
                        Group Name
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="GroupNameTextBox" Width="250" MaxLength="50" ReadOnly="true"
                            BackColor="#CCCCCC"></asp:TextBox>
                        <asp:HiddenField ID="GroupNameHiddenField" runat="server" />
                        <asp:Button ID="EditGroupNameButton" runat="server" Text="Edit Group Name" OnClick="EditGroupNameButton_Click" />
                    </td>
                </tr>
            </table>
            <fieldset>
            <legend>Contact List</legend>
            <table cellpadding="3" cellspacing="0" border="0" width="0">
                <tr>
                    <td>
                        <table cellpadding="3" cellspacing="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <asp:ImageButton runat="server" ID="DeleteButton" OnClick="DeleteButton_Click" />
                                </td>
                                <td>
                                    <asp:HiddenField ID="CheckHidden" runat="server" />
                                    <asp:HiddenField ID="TempHidden" runat="server" />
                                    <asp:HiddenField ID="AllHidden" runat="server" />
                                </td>
                                <td align="right">
                                    <asp:Panel DefaultButton="DataPagerButton" ID="DataPagerPanel" runat="server">
                                        <table border="0" cellpadding="2" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="DataPagerButton" runat="server" OnClick="DataPagerButton_Click" />
                                                </td>
                                                <td valign="middle">
                                                    <b>Page :</b>
                                                </td>
                                                <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater" runat="server" OnItemCommand="DataPagerTopRepeater_ItemCommand"
                                                    OnItemDataBound="DataPagerTopRepeater_ItemDataBound">
                                                    <ItemTemplate>
                                                        <td>
                                                            <asp:LinkButton ID="PageNumberLinkButton" runat="server"></asp:LinkButton>
                                                            <asp:TextBox Visible="false" ID="PageNumberTextBox" runat="server" Width="30px"></asp:TextBox>
                                                        </td>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tr>
                                        </table>
                                    </asp:Panel>
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
                        <table cellpadding="3" cellspacing="1" width="100%" border="0">
                            <tr class="bgcolor_gray">
                                <td style="width: 5px">
                                    <asp:CheckBox runat="server" ID="AllCheckBox" style="display:none;" />
                                </td>
                                <td style="width: 5px" class="tahoma_11_white" align="center">
                                    <b>No.</b>
                                </td>
                                <td style="width: 250px" class="tahoma_11_white" align="center">
                                    <b>Contact Name</b>
                                </td>
                                <td style="width: 250px" class="tahoma_11_white" align="center">
                                    <b>Phone No</b>
                                </td>
                            </tr>
                            <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                                <ItemTemplate>
                                    <tr id="RepeaterItemTemplate" runat="server">
                                        <td align="center">
                                            <asp:CheckBox runat="server" ID="ListCheckBox" />
                                        </td>
                                        <td align="center">
                                            <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                        </td>
                                        <td align="left">
                                            <asp:Literal runat="server" ID="ContactNameLiteral"></asp:Literal>
                                        </td>
                                        <td align="left">
                                            <asp:Literal runat="server" ID="PhoneNoLiteral"></asp:Literal>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </td>
                </tr>
            </table>
        </fieldset>
            
            
            <div class="formRow" style="height:20px">&nbsp;</div>
            <asp:Panel ID="Panel3" DefaultButton="GoImageButton" runat="server">
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
                    <asp:HiddenField ID="CheckHidden2" runat="server" />
                    <asp:HiddenField ID="TempHidden2" runat="server" />
                    <asp:HiddenField ID="AllHidden2" runat="server" />
                    <fieldset>                    
                    <legend>
                        Contact List
                    </legend>
                    <asp:Panel DefaultButton="DataPagerButton2" ID="Panel2" runat="server">
                        <div class="formRow">
                            <div style="float:left">
                                <asp:CheckBox runat="server" ID="AllCheckBox2" Text="Check All" 
                                    AutoPostBack="true" oncheckedchanged="AllCheckBox_CheckedChanged" />
                                <%--<asp:CheckBox runat="server" ID="GrabAllCheckBox2" Text="Grab All" />--%>
                            </div>
                            <div style="float:right">
                                <table>
                                    <tr>
                                        <td valign="middle">
                                            <asp:Button ID="DataPagerButton2" runat="server" CausesValidation="false" OnClick="DataPagerButton2_Click" />
                                            <%--<b>Page :</b>--%>
                                        </td>
                                        <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater2" runat="server" OnItemCommand="DataPagerTopRepeater2_ItemCommand"
                                            OnItemDataBound="DataPagerTopRepeater2_ItemDataBound">
                                            <ItemTemplate>
                                                <td>
                                                    <asp:LinkButton ID="PageNumberLinkButton2" runat="server" CausesValidation="false"></asp:LinkButton>
                                                    <asp:TextBox Visible="false" ID="PageNumberTextBox2" runat="server" Width="30px"></asp:TextBox>
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
            <%--<div class="formRow" style="font-size:10px">
            * Use Check All to select all checkbox visible on this page<br />
            * Use Grab All to get all data regarding filter selected
            </div>--%>
            
            
            
            
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
        </asp:Panel>
</asp:Content>
