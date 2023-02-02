<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="CompanyView.aspx.cs"
    MaintainScrollPositionOnPostback="true" Inherits="InetGlobalIndo.ERP.MTJ.UI.Settings.Company.CompanyView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <style type="text/css">
        .style1
        {
            height: 27px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                Company Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="NameTextBox" Width="150" MaxLength="50" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Logo
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="LogoTextBox" Width="150" MaxLength="50" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Primary Address
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AddressTextBox" Width="200" MaxLength="500" TextMode="MultiLine"
                                    Height="100" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Company Tag
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CompanyTagTextBox" Width="50" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tax Branch No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TaxBranchNoTextBox" Width="50" MaxLength="3" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Default Company
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="DefaultCheckBox" Enabled="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="UpdateReportTemplateButton" runat="server" CausesValidation="False"
                                    OnClick="UpdateReportTemplateButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="EditMenuButton" runat="server" OnClick="EditMenuButton_Click1" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Database</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" border="0">
                                        <tr>
                                            <td style="width: 50px">
                                                <asp:ImageButton runat="server" ID="AddButton" OnClick="AddButton_Click" />
                                            </td>
                                            <td style="width: 50px">
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
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label1" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="CheckHidden" runat="server" />
                                    <asp:HiddenField ID="TempHidden" runat="server" />
                                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 5px">
                                                <asp:CheckBox runat="server" ID="AllCheckBox" />
                                            </td>
                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                                <b>Database Name</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Status</b>
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
                                                        <asp:Literal runat="server" ID="DatabaseLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="StatusLiteral"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>User</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" border="0">
                                        <tr>
                                            <td style="width: 50px">
                                                <asp:ImageButton runat="server" ID="AddButton2" OnClick="AddButton2_Click" />
                                            </td>
                                            <td style="width: 50px">
                                                <asp:ImageButton runat="server" ID="DeleteButton2" OnClick="DeleteButton2_Click" />
                                            </td>
                                            <td align="right">
                                                <asp:Panel DefaultButton="DataPagerButton2" ID="Panel2" runat="server">
                                                    <table border="0" cellpadding="2" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="DataPagerButton2" runat="server" OnClick="DataPagerButton2_Click" />
                                                            </td>
                                                            <td valign="middle">
                                                                <b>Page :</b>
                                                            </td>
                                                            <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater2" runat="server" OnItemCommand="DataPagerTopRepeater2_ItemCommand"
                                                                OnItemDataBound="DataPagerTopRepeater2_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <td>
                                                                        <asp:LinkButton ID="PageNumberLinkButton2" runat="server"></asp:LinkButton>
                                                                        <asp:TextBox Visible="false" ID="PageNumberTextBox2" runat="server" Width="30px"></asp:TextBox>
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
                                    <asp:Label runat="server" ID="Label2" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="CheckHidden2" runat="server" />
                                    <asp:HiddenField ID="TempHidden2" runat="server" />
                                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 5px">
                                                <asp:CheckBox runat="server" ID="AllCheckBox2" />
                                            </td>
                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Action</b>
                                            </td>
                                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                                <b>User Name</b>
                                            </td>
                                        </tr>
                                        <asp:Repeater runat="server" ID="ListRepeater2" OnItemDataBound="ListRepeater2_ItemDataBound">
                                            <ItemTemplate>
                                                <tr id="RepeaterItemTemplate2" runat="server">
                                                    <td align="center">
                                                        <asp:CheckBox runat="server" ID="ListCheckBox2" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="NoLiteral2"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <table cellpadding="0" cellspacing="0" width="0" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton runat="server" ID="ViewButton" />
                                                                </td>
                                                                <td style="padding-left: 4px">
                                                                    <asp:ImageButton runat="server" ID="EditButton" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="UserLiteral"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Role</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" border="0">
                                        <tr>
                                            <td style="width: 50px">
                                                <asp:ImageButton runat="server" ID="AddButton3" OnClick="AddButton3_Click" />
                                            </td>
                                            <td style="width: 50px">
                                                <asp:ImageButton runat="server" ID="DeleteButton3" OnClick="DeleteButton3_Click" />
                                            </td>
                                            <td align="right">
                                                <asp:Panel DefaultButton="DataPagerButton3" ID="Panel3" runat="server">
                                                    <table border="0" cellpadding="2" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="DataPagerButton3" runat="server" OnClick="DataPagerButton3_Click" />
                                                            </td>
                                                            <td valign="middle">
                                                                <b>Page :</b>
                                                            </td>
                                                            <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater3" runat="server" OnItemCommand="DataPagerTopRepeater3_ItemCommand"
                                                                OnItemDataBound="DataPagerTopRepeater3_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <td>
                                                                        <asp:LinkButton ID="PageNumberLinkButton3" runat="server"></asp:LinkButton>
                                                                        <asp:TextBox Visible="false" ID="PageNumberTextBox3" runat="server" Width="30px"></asp:TextBox>
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
                                    <asp:Label runat="server" ID="Label3" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="CheckHidden3" runat="server" />
                                    <asp:HiddenField ID="TempHidden3" runat="server" />
                                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 5px">
                                                <asp:CheckBox runat="server" ID="AllCheckBox3" />
                                            </td>
                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <%--<td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Action</b>
                                            </td>--%>
                                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                                <b>Role Name</b>
                                            </td>
                                        </tr>
                                        <asp:Repeater runat="server" ID="ListRepeater3" OnItemDataBound="ListRepeater3_ItemDataBound">
                                            <ItemTemplate>
                                                <tr id="RepeaterItemTemplate3" runat="server">
                                                    <td align="center">
                                                        <asp:CheckBox runat="server" ID="ListCheckBox3" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="NoLiteral3"></asp:Literal>
                                                    </td>
                                                    <%--<td align="center">
                                                        <table cellpadding="0" cellspacing="0" width="0" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton runat="server" ID="ViewButton" />
                                                                </td>
                                                                <td style="padding-left: 4px">
                                                                    <asp:ImageButton runat="server" ID="EditButton" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>--%>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="RoleNameLiteral"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Report List</legend>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Panel ID="Panel6" DefaultButton="GoImageButton" runat="server">
                                        <table cellpadding="0" cellspacing="2" border="0">
                                            <tr>
                                                <td>
                                                    <b>Quick Search :</b>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CategoryDropDownList" runat="server">
                                                        <asp:ListItem Value="ReportGroup" Text="Report Group"></asp:ListItem>
                                                        <asp:ListItem Value="ReportName" Text="Report Name"></asp:ListItem>
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
                                </td>
                            </tr>
                        </table>
                        <asp:Panel DefaultButton="DataPagerButton4" ID="Panel4" runat="server">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:Button ID="DataPagerButton4" runat="server" OnClick="DataPagerButton4_Click" />
                                    </td>
                                    <td valign="middle">
                                        <b>Page :</b>
                                    </td>
                                    <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater4" runat="server" OnItemCommand="DataPagerTopRepeater4_ItemCommand"
                                        OnItemDataBound="DataPagerTopRepeater4_ItemDataBound">
                                        <ItemTemplate>
                                            <td>
                                                <asp:LinkButton ID="PageNumberLinkButton4" runat="server"></asp:LinkButton>
                                                <asp:TextBox Visible="false" ID="PageNumberTextBox4" runat="server" Width="30px"></asp:TextBox>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Label runat="server" ID="Label4" CssClass="warning"></asp:Label>
                        <asp:HiddenField ID="TempHidden4" runat="server" />
                        <table cellpadding="3" cellspacing="1" width="0" border="0">
                            <tr class="bgcolor_gray" height="30">
                                <th width="5" class="tahoma_11_white">
                                    No.
                                </th>
                                <th class="tahoma_11_white">
                                    Active
                                </th>
                                <th class="tahoma_11_white">
                                    Report Group
                                </th>
                                <th class="tahoma_11_white">
                                    Report Name
                                </th>
                            </tr>
                            <asp:Repeater ID="ListRepeater4" runat="server" OnItemDataBound="ListRepeater4_ItemDataBound"
                                OnItemCommand="ListRepeater4_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Literal ID="NoLiteral4" runat="server"></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:Button ID="ReportListButton" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Literal ID="ReportGroupLiteral" runat="server"></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:Literal ID="ReportNameLiteral" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td align="right">
                                <asp:Panel ID="Panel7" DefaultButton="GoImageButton" runat="server">
                                    <table cellpadding="0" cellspacing="2" border="0">
                                        <tr>
                                            <td>
                                                <b>Quick Search :</b>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="CategoryDropDownList2" runat="server">
                                                    <asp:ListItem Value="ReportGroup" Text="Report Group"></asp:ListItem>
                                                    <%--<asp:ListItem Value="ReportName" Text="Report Name"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="KeywordTextBox2" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="GoImageButton2" runat="server" OnClick="GoImageButton2_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <fieldset>
                        <legend>Print Preview List</legend>
                        <asp:Panel DefaultButton="DataPagerButton5" ID="Panel5" runat="server">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:Button ID="DataPagerButton5" runat="server" OnClick="DataPagerButton5_Click" />
                                    </td>
                                    <td valign="middle">
                                        <b>Page :</b>
                                    </td>
                                    <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater5" runat="server" OnItemCommand="DataPagerTopRepeater5_ItemCommand"
                                        OnItemDataBound="DataPagerTopRepeater5_ItemDataBound">
                                        <ItemTemplate>
                                            <td>
                                                <asp:LinkButton ID="PageNumberLinkButton5" runat="server"></asp:LinkButton>
                                                <asp:TextBox Visible="false" ID="PageNumberTextBox5" runat="server" Width="30px"></asp:TextBox>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Label runat="server" ID="Label5" CssClass="warning"></asp:Label>
                        <asp:HiddenField ID="TempHidden5" runat="server" />
                        <table cellpadding="3" cellspacing="1" width="0" border="0">
                            <tr class="bgcolor_gray" height="30">
                                <th width="5" class="tahoma_11_white">
                                    No.
                                </th>
                                <th class="tahoma_11_white">
                                    Report Group
                                </th>
                                <th class="tahoma_11_white">
                                    Print Preview Selection
                                </th>
                                <th class="tahoma_11_white">
                                    Save
                                </th>
                            </tr>
                            <asp:Repeater ID="ListRepeater5" runat="server" OnItemDataBound="ListRepeater5_ItemDataBound"
                                OnItemCommand="ListRepeater5_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Literal ID="NoLiteral5" runat="server"></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:Literal ID="ReportGroupIDLiteral" runat="server"></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="PrintPreviewSelection" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="BtnSave" runat="server"></asp:Button>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
