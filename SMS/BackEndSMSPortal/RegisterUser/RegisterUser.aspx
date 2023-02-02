<%@ Page Title="" Language="C#" MasterPageFile="~/SMSAdminMasterPage.master" AutoEventWireup="true"
    CodeFile="RegisterUser.aspx.cs" Inherits="SMS.BackEndSMSPortal.RegisterUser.RegisterUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContentTitle"
    runat="Server">
    REGISTER USER
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
                            <asp:ListItem Value="OrganizationName" Text="Organization Name"></asp:ListItem>
                            <asp:ListItem Value="UserID" Text="User ID"></asp:ListItem>
                            <asp:ListItem Value="PackageName" Text="Package Name"></asp:ListItem>
                            <asp:ListItem Value="Email" Text="Email"></asp:ListItem>
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
        <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
        <asp:HiddenField ID="CheckHidden" runat="server" />
        <asp:HiddenField ID="TempHidden" runat="server" />
        <table cellpadding="3" cellspacing="1" border="0">
            <tr class="bgcolor_gray">
               <%-- <td style="width: 5px">
                    <asp:CheckBox runat="server" ID="AllCheckBox" />
                </td>--%>
                <td style="width: 5px" class="tahoma_11_white" align="center">
                    <b>No.</b>
                </td>
                <td style="width: 80px" class="tahoma_11_white" align="center">
                    <b>Action</b>
                </td>
                <td style="width: 200px" class="tahoma_11_white" align="center">
                    <b>Organization Name</b>
                </td>
                <td style="width: 120px" class="tahoma_11_white" align="center">
                    <b>User ID</b>
                </td>
                <td style="width: 120px" class="tahoma_11_white" align="center">
                    <b>Fg Admin</b>
                </td>
                <td style="width: 200px" class="tahoma_11_white" align="center">
                    <b>Package Name</b>
                </td>
                <td style="width: 200px" class="tahoma_11_white" align="center">
                    <b>Email</b>
                </td>
            </tr>
            <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                <ItemTemplate>
                    <tr id="RepeaterItemTemplate" runat="server">
                        <%--<td align="center">
                            <asp:CheckBox runat="server" ID="ListCheckBox" />
                        </td>--%>
                        <td align="center">
                            <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                        </td>
                        <td align="left">
                            <asp:ImageButton ID="EditButton" runat="server" />
                        </td>
                        <td align="left">
                            <asp:Literal runat="server" ID="OrganizationNameLiteral"></asp:Literal>
                        </td>
                        <td align="left">
                            <asp:Literal runat="server" ID="UserIDLiteral"></asp:Literal>
                        </td>
                        <td align="left">
                            <asp:Literal runat="server" ID="FgAdminLiteral"></asp:Literal>
                        </td>
                        <td align="left">
                            <asp:Literal runat="server" ID="PackageNameLiteral"></asp:Literal>
                        </td>
                        <td align="left">
                            <asp:Literal runat="server" ID="EmailLiteral"></asp:Literal>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr class="bgcolor_gray">
                <td style="width: 1px" colspan="25">
                </td>
            </tr>
        </table>
        <%--<table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <asp:ImageButton runat="server" ID="AddButton2" OnClick="AddButton_Click" />
                    &nbsp;<asp:ImageButton ID="DeleteButton2" runat="server" OnClick="DeleteButton_Click" />
                    &nbsp;<asp:ImageButton ID="SendSMSButton2" runat="server" onclick="SendSMSButton_Click"/>
                </td>
                <td align="right">
                    <asp:Panel DefaultButton="DataPagerBottomButton" ID="Panel3" runat="server">
                        <table border="0" cellpadding="2" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:Button ID="DataPagerBottomButton" runat="server" OnClick="DataPagerBottomButton_Click" />
                                </td>
                                <td>
                                    <b>Page :</b>
                                </td>
                                <asp:Repeater EnableViewState="true" ID="DataPagerBottomRepeater" runat="server"
                                    OnItemCommand="DataPagerTopRepeater_ItemCommand" OnItemDataBound="DataPagerTopRepeater_ItemDataBound">
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
        </table>--%>
    </asp:Panel>
</asp:Content>
