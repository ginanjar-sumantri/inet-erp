<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="Event.aspx.cs" Inherits="ITV.UI.MsEvent.Event" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <table>
        <tr>
            <td class="tahoma_14_black">
                <b>
                    <asp:Literal ID="PageTitleLiteral" runat="server" />
                </b>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Panel ID="Panel2" DefaultButton="GoImageButton" runat="server">
                    <table cellpadding="0" cellspacing="2" border="0">
                        <tr>
                            <td>
                                <b>Quick Search :</b>
                            </td>
                            <td>
                                Event Name
                            </td>
                            <td>
                                <asp:TextBox ID="KeywordTextBox" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="GoImageButton" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="3" cellspacing="0" width="100%" border="0">
                    <tr>
                        <td align="left">
                            <asp:ImageButton ID="AddButton" runat="server" CausesValidation="False" />
                            <asp:ImageButton ID="DeleteButton" runat="server" CausesValidation="False" />
                        </td>
                        <td align="right">
                            <asp:Panel DefaultButton="DataPagerButton" ID="DataPagerPanel" runat="server">
                                <table border="0" cellpadding="2" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Button ID="DataPagerButton" runat="server" />
                                        </td>
                                        <td valign="middle">
                                            <b>Page :</b>
                                        </td>
                                        <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater" runat="server">
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
                <b>
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="CheckHidden" runat="server" />
                <asp:HiddenField ID="TempHidden" runat="server" />
                <table cellpadding="3" cellspacing="1" width="0" border="0">
                    <tr class="bgcolor_gray">
                        <td>
                            <asp:CheckBox runat="server" ID="AllCheckBox" />
                        </td>
                        <td style="width: 5px" class="tahoma_11_white" align="center">
                            <b>No.</b>
                        </td>
                        <td style="width: 100px" class="tahoma_11_white" align="center">
                            <b>Action</b>
                        </td>
                        <td style="width: 120px" class="tahoma_11_white" align="center">
                            <b>EventName</b>
                        </td>
                        <td style="width: 200px" class="tahoma_11_white" align="center">
                            <b>Description</b>
                        </td>
                        <td style="width: 120px" class="tahoma_11_white" align="center">
                            <b>Image File</b>
                        </td>
                        <td style="width: 200px" class="tahoma_11_white" align="center">
                            <b>Video File</b>
                        </td>
                    </tr>
                    <asp:Repeater runat="server" ID="ListRepeater">
                        <ItemTemplate>
                            <tr id="RepeaterItemTemplate" runat="server">
                                <td align="center">
                                    <asp:CheckBox runat="server" ID="ListCheckBox" />
                                </td>
                                <td align="center">
                                    <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                </td>
                                <td align="center">
                                    <table cellpadding="0" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td style="padding-left: 4px">
                                                <asp:ImageButton runat="server" ID="ViewButton" />
                                            </td>
                                            <td style="padding-left: 4px">
                                                <asp:ImageButton runat="server" ID="EditButton" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="left">
                                    <asp:Literal runat="server" ID="EventNameLiteral"></asp:Literal>
                                </td>
                                <td align="left">
                                    <asp:Literal runat="server" ID="DescriptionLiteral"></asp:Literal>
                                </td>
                                <td align="left">
                                    <asp:Literal runat="server" ID="ImageFileLiteral"></asp:Literal>
                                </td>
                                <td align="left">
                                    <asp:Literal runat="server" ID="VideoLiteral"></asp:Literal>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="bgcolor_gray">
                        <td style="width: 1px" colspan="5">
                        </td>
                    </tr>
                </table>
                <table cellpadding="3" cellspacing="0" border="0" style="width: 100%">
                    <tr>
                        <td align="left">
                            <asp:ImageButton runat="server" ID="AddButton2" />&nbsp
                            <asp:ImageButton runat="server" ID="DeleteButton2" />
                        </td>
                        <td align="right">
                            <asp:Panel DefaultButton="DataPagerBottomButton" ID="Panel3" runat="server">
                                <table border="0" cellpadding="2" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Button ID="DataPagerBottomButton" runat="server" />
                                        </td>
                                        <td>
                                            <b>Page :</b>
                                        </td>
                                        <asp:Repeater EnableViewState="true" ID="DataPagerBottomRepeater" runat="server">
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
    </table>
</asp:Content>
