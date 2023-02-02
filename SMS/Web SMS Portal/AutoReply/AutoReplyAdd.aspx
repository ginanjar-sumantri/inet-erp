<%@ Page Language="C#" MasterPageFile="~/SMSMasterPage.master" AutoEventWireup="true"
    CodeFile="AutoReplyAdd.aspx.cs" Inherits="SMS.SMSWeb.AutoReply.AutoReplyAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .formRow
        {
            clear: both;
            padding: 3px 3px 3px 3px;
        }
    </style>

    <script type="text/javascript" language="javascript">
        function textCounter(counterBox, MessageBox, limit, CountSMSBox) {
            counterBox.value = limit - (MessageBox.value.length % limit);
            CountSMSBox.value = Math.ceil(MessageBox.value.length / limit);
        }
        function HarusAngka(x) { if (isNaN(x.value)) x.value = ""; }
        function GaBolehPlus(x) {
            for (i = 0; i < x.value.length; i++) {
                var huruf = x.value.substr(i, 1);
                if (isNaN(huruf))
                    if (!(huruf == "*" || huruf == "#"))
                    x.value = "";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAtas" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContentTitle"
    runat="Server">
    COMPOSE
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
    <asp:ScriptManager runat="server" ID="ScriptManager">
    </asp:ScriptManager>
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
        <asp:Label ID="WarningLabel" ForeColor="Red" Font-Bold="true" runat="server"></asp:Label>        
        <div class="formRow">
            <div style="float: left; width: 150px">
                Sender Phone No.</div>
            <span style="float: left">: &nbsp; </span>
            <div style="float: left">
                <asp:DropDownList ID="SelectionDropDownList" Style="float: left" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="SelectionDropDownList_SelectedIndexChanged">
                    <asp:ListItem Text="Enter Phone Number" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Open Phone Book" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Open Phone Groups" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <asp:Panel runat="server" ID="PanelOneNo">
            <div class="formRow">
                <div style="float: left; width: 150px">
                    &nbsp;</div>
                <span style="float: left">&nbsp; &nbsp; </span>
                <div style="float: left">
                    <asp:DropDownList runat="server" ID="PrefixPhoneNumber">                        
                    </asp:DropDownList>
                    <asp:TextBox runat="server" ID="DestinationTextBox" Width="200" MaxLength="50"></asp:TextBox>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="PanelPhoneBook" Visible="false">
            <asp:UpdatePanel runat="server" ID="UpdatePanelPhoneBook">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="SelectedPhone" />
                    <div style="clear: both">
                        <div style="padding:5px;">Search : <asp:TextBox runat="server" ID="SearchTextBox"></asp:TextBox>
                            <asp:Button runat="server" ID="SearchButton" Text="Search" 
                                onclick="SearchButton_Click" />
                            <asp:HiddenField runat="server" ID="checkAllPerPageValue" />
                            <asp:CheckBox runat="server" ID="CheckAllPhoneBookCheckBox" Text="Check All" 
                                AutoPostBack="true"
                                oncheckedchanged="CheckAllPhoneBookCheckBox_CheckedChanged"/></div>
                        <asp:HiddenField runat="server" ID="ContactPageHiddenField" />
                        <asp:Repeater runat="server" ID="repeaterPaging" OnItemCommand="repeaterPaging_ItemCommand"
                            OnItemDataBound="repeaterPaging_ItemDataBound">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="pageLink"></asp:LinkButton>
                                &nbsp;
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div style="clear: both">
                        <asp:Repeater runat="server" ID="repeaterPhoneBook" OnItemDataBound="repeaterPhoneBook_ItemDataBound">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="PhoneBookCheckBox" Style="display: block; width: 125px;
                                    float: left;" AutoPostBack="true" OnCheckedChanged="PhoneBookCheckBOx_CheckChanged" />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <div style="clear:both;"></div>
        <asp:Panel runat="server" id ="PanelPhoneGroup" Visible="false">
            <div>
                <asp:Repeater runat="server" ID="PhoneGroupRepeater" 
                    onitemdatabound="PhoneGroupRepeater_ItemDataBound">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="PhoneGroupCheckBox" style="display:block;width:125px;float:left;"
                        AutoPostBack="true" OnCheckedChanged="PhoneGroupCheckBox_CheckChanged"  />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>
        <div class="formRow">
            <span style="float: left; width: 153px"><span style="width:150px;display:block;float:left;">Key Word </span>: </span> &nbsp; <asp:TextBox runat="server" ID="KeyWordTextBox"></asp:TextBox>
        </div>
        <div class="formRow">
            <span style="float: left; width: 153px"><span style="width:150px;display:block;float:left;">Reply Message </span>: </span> &nbsp; <asp:TextBox runat="server" ID="ReplyMessageTextBox" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="formRow">
            <asp:ImageButton runat="server" ID="SaveButton" ImageUrl="../images/save.jpg" 
            onclick="SaveButton_Click" />
            <asp:ImageButton runat="server" ID="CancelButton" 
            ImageUrl="../images/cancel.jpg" onclick="CancelButton_Click" />
        </div>
    </asp:Panel>
</asp:Content>
