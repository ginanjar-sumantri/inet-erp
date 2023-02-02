<%@ page language="C#" masterpagefile="~/SMSMasterPage.master" autoeventwireup="true" inherits="SMS.SMSWeb.Message.Compose, App_Web_tpn8tx_m" %>

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
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SendButton">
        <asp:Label ID="WarningLabel" ForeColor="Red" Font-Bold="true" runat="server"></asp:Label>
        <div class="formRow">
            <div style="float: left; width: 150px">
                Category</div>
            <span style="float: left">: &nbsp; </span>
            <div style="float: left">
                <asp:TextBox runat="server" ID="CategoryTextBox" Width="120" MaxLength="50"></asp:TextBox></div>
        </div>
        <div class="formRow">
            <div style="float: left; width: 150px">
                Destination Phone No.</div>
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
        <asp:Panel runat="server" ID="PanelMasking" Visible="false">
            <div class="formRow">
                <div style="float: left; width: 150px">
                    SMS Setting</div>
                <span style="float: left">: &nbsp; </span>
                <div style="float: left">
                    <asp:CheckBox runat="server" ID="MaskingCheckBox" Text="Use Masking" />
                    <asp:CheckBox runat="server" ID="FooterMessageCheckBox" Text="Use Footer Additional Message" Checked="true" />
                </div>
            </div>
        </asp:Panel>
        <div class="formRow">
            <div style="float: left; width: 150px">
                Message
            </div>
            <div style="float: left;">
                : &nbsp;</div>
            <div style="float: left;">
                <asp:TextBox runat="server" Style="float: left" ID="MessageTextBox"
                    Width="300" TextMode="MultiLine" Height="150"></asp:TextBox>
                <br />
                <asp:TextBox runat="server" ID="CounterTextBox" Width="50px"></asp:TextBox>
                <asp:TextBox runat="server" ID="CountSMSTextBox" Width="20px"></asp:TextBox>
            </div>
        </div>
        <asp:Panel runat="server" ID="panelReply">
            <div class="formRow">
                <div style="float: left; width: 150px">
                    Original Message</div>
                <span style="float: left">: &nbsp; </span>
                <asp:Label runat="server" ID="OriginalMessageLabel" Width="300"></asp:Label>
            </div>
        </asp:Panel>
        <div class="formRow">
            <asp:HiddenField ID="CodeHiddenField" runat="server" />
            <asp:HiddenField ID="ContactHiddenField" runat="server" />
            <asp:ImageButton ID="SendButton" runat="server" OnClick="SendButton_Click" />
            <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
            <asp:ImageButton runat="server" ID="UploadButton" onclick="UploadButton_Click"/>
        </div>
    </asp:Panel>
</asp:Content>
