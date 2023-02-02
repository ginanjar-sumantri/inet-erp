<%@ Page Title="" Language="C#" MasterPageFile="~/SMSAdminMasterPage.master" AutoEventWireup="true"
    CodeFile="Package.aspx.cs" Inherits="SMS.BackEndSMSPortal.Package.Package" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script language="javascript">
function CheckBox_Click (tempHidden, currCheckBox, code , allCheckBox, checkHidden){
    if ( currCheckBox.checked )
        tempHidden.value += "," + code ;
    else {
        splittedTempHidden = tempHidden.value.split(',') ;
        temporaryImplode = "" ;
        for ( i = 0 ; i < splittedTempHidden.length ; i ++ ){
            if ( splittedTempHidden[i] != code )
                temporaryImplode += "," + splittedTempHidden[i] ;
        }
        tempHidden.value = (temporaryImplode=="")?",":temporaryImplode ;
    }
    if ( tempHidden.value.charAt ( 0 ) == ',')
        tempHidden.value = tempHidden.value.substring (1) ;
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContentTitle"
    runat="Server">
    MANAGE PACKAGE
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
    <asp:Panel runat="server" ID="Panel1">
        <asp:Panel ID="Panel2" DefaultButton="GoImageButton" runat="server">
            <table cellpadding="0" cellspacing="2" border="0">
                <tr>
                    <td>
                        <b>Quick Search :</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="CategoryDropDownList" runat="server">
                            <asp:ListItem Value="PackageName" Text="Package Name"></asp:ListItem>
                            <asp:ListItem Value="Description" Text="Description"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="KeywordTextBox" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="GoImageButton" runat="server" OnClick="GoImageButton_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <asp:ImageButton runat="server" ID="AddButton" OnClick="AddButton_Click" />
                    <asp:ImageButton runat="server" ID="DeleteButton" OnClick="DeleteButton_Click" />
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
        <asp:Label runat="server" ID="WarningLabel" style="color:red;font-weight:bold;"></asp:Label>
        <asp:HiddenField ID="CheckHidden" runat="server" />
        <asp:HiddenField ID="TempHidden" runat="server" />
        <table cellpadding="3" cellspacing="1" border="0">
            <tr class="bgcolor_gray">
                <td style="width: 5px">
                    <asp:CheckBox runat="server" ID="AllCheckBox" />
                </td>
                <td style="width: 5px" class="tahoma_11_white" align="center">
                    <b>No.</b>
                </td>
                <td style="width: 80px" class="tahoma_11_white" align="center">
                    <b>Action</b>
                </td>
                <td style="width: 150px" class="tahoma_11_white" align="center">
                    <b>Package Name</b>
                </td>
                <td style="width: 100px" class="tahoma_11_white" align="center">
                    <b>SMS Per Day</b>
                </td>
                <td style="width: 250px" class="tahoma_11_white" align="center">
                    <b>Description</b>
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
                        <td align="center">
                            <asp:ImageButton ID="EditButton" runat="server" />
                        </td>
                        <td align="left">
                            <asp:Literal runat="server" ID="PackageNameLiteral"></asp:Literal>
                        </td>
                        <td align="left">
                            <asp:Literal runat="server" ID="SMSPerDayLiteral"></asp:Literal>
                        </td>
                        <td align="left">
                            <asp:Literal runat="server" ID="DescriptionLiteral"></asp:Literal>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr class="bgcolor_gray">
                <td style="width: 1px" colspan="25">
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
