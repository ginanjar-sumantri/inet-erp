<%@ Page Title="" Language="C#" MasterPageFile="~/SMSAdminMasterPage.master" AutoEventWireup="true" 
CodeFile="OrganizationSetting.aspx.cs" Inherits="SMS.BackEndSMSPortal.OrganizationSetting.OrganizationSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContentTitle" Runat="Server">
ORGANIZATIONS BALANCE
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="GoImageButton" runat="server">
        <table cellpadding="0" cellspacing="2" border="0">
            <tr>
                <td>
                    <b>Quick Search :</b>
                </td>
                <td>
                    <asp:DropDownList ID="CategoryDropDownList" runat="server">
                        <asp:ListItem Value="OrganizationName" Text="Organization Name"></asp:ListItem>                            
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
                <td>&nbsp;</td>
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
        <asp:Label runat="server" ID="WarningLabel" style="font-weight:bold;color:Red;"></asp:Label>
        <table cellpadding="3" cellspacing="1" border="0">
            <tr class="bgcolor_gray">
                <td style="width: 5px" class="tahoma_11_white" align="center">
                    <b>No.</b>
                </td>
                <td style="width: 80px" class="tahoma_11_white" align="center">
                    <b>Action</b>
                </td>
                <td style="width: 150px" class="tahoma_11_white" align="center">
                    <b>Organization Name</b>
                </td>
                <td style="width: 150px" class="tahoma_11_white" align="center">
                    <b>User Limit</b>
                </td>
                <td style="width: 100px" class="tahoma_11_white" align="center">
                    <b>Masking SD</b>
                </td>
            </tr>
            <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                <ItemTemplate>
                    <tr id="RepeaterItemTemplate" runat="server">
                        <td align="center">
                            <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                        </td>
                        <td align="center">
                            <asp:ImageButton ID="EditButton" runat="server" />
                        </td>
                        <td align="left">
                            <asp:Literal runat="server" ID="OrganizationNameLiteral"></asp:Literal>
                        </td>                        
                        <td align="left">
                            <asp:Literal runat="server" ID="UserLimitLiteral"></asp:Literal>
                        </td>
                        <td align="left">
                            <asp:Literal runat="server" ID="MaskingSDLiteral"></asp:Literal>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr class="bgcolor_gray">
                <td style="width: 1px" colspan="25">
                </td>
            </tr>
        </table>        
</asp:Content>

