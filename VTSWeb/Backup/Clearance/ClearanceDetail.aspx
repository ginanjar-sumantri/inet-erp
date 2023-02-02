<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="ClearanceDetail.aspx.cs" Inherits="VTSWeb.UI.ClearanceDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <table>
        <tr>
            <td class="style1">
                <b>
                    <asp:Literal ID="PageTitleLiteral" runat="server" /></b>
            </td>
        </tr>
        <tr>
            <td class="style1">
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <fieldset>
                    <legend>Header</legend>
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td colspan="2">
                                No
                            </td>
                            <td style="width: 0">
                                :
                            </td>
                            <td width="120" style="width: 60px" colspan="2">
                                <asp:TextBox ID="NoTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server" Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="style16">
                                Date
                            </td>
                            <td style="width: 0">
                                :
                            </td>
                            <td width="120" style="width: 60px" colspan="2">
                                <asp:TextBox ID="DateTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                    Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Company
                            </td>
                            <td colspan="2">
                                :
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox ID="CustomerNameTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                    Width="300"></asp:TextBox>
                                <asp:HiddenField ID="CustomerHiddenField" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;Visitor
                            </td>
                            <td colspan="2">
                                :
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox ID="ContactNameTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                    Width="300"></asp:TextBox>
                                <asp:HiddenField ID="ContactNameHiddenField" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Remark
                            </td>
                            <td colspan="2">
                                :
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox ID="RemarkTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                    Width="300" Height="100" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Status
                            </td>
                            <td colspan="2">
                                :
                            </td>
                            <td align="left" colspan="2">
                                <asp:Label ID="StatusLabel" runat="server" Text="LabelStatus" Enabled="False"></asp:Label>
                                <asp:HiddenField ID="StatusHiddenField" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" OnClick="EditButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="BackButton" runat="server" CausesValidation="False" OnClick="BackButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="CompleteButton" runat="server" CausesValidation="False" OnClick="CompleteButton_Click" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td class="style1">
            </td>
        </tr>
        <tr>
            <td class="style1">
                <fieldset>
                    <legend>Detail</legend>
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td colspan="3">
                                <asp:Label runat="server" CssClass="warning" ID="WarningDeleteLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="AddButton" runat="server" CausesValidation="False" OnClick="AddButton_Click" />
                                &nbsp;<asp:ImageButton ID="DeleteButton" runat="server" CausesValidation="False"
                                    OnClick="DeleteButton_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="CheckHidden" runat="server" />
                                <asp:HiddenField ID="TempHidden" runat="server" />
                                <table cellpadding="3" cellspacing="1" width="100%" border="0" style="height: 0px">
                                    <tr class="bgcolor_gray">
                                        <td style="width: 5px">
                                            <asp:CheckBox runat="server" ID="AllCheckBox" />
                                        </td>
                                        <td style="width: 5px" class="tahoma_11_white" align="center">
                                            <b>No.</b>
                                        </td>
                                        <td style="width: 100px" class="tahoma_11_white" align="center">
                                            <b>Action</b>
                                        </td>
                                        <td style="width: 120px" class="tahoma_11_white" align="center">
                                            <b>Area Name</b>
                                        </td>
                                        <td style="width: 120px" class="tahoma_11_white" align="center">
                                            <b>Purpose Name</b>
                                        </td>
                                        <td style="width: 150px" class="tahoma_11_white" align="center">
                                            <b>Date In</b>
                                        </td>
                                        <td style="width: 150px" class="tahoma_11_white" align="center">
                                            <b>Date Out</b>
                                        </td>
                                    </tr>
                                    <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                                        <%--   OnItemCommand="ListRepeater_ItemCommand">--%>
                                        <ItemTemplate>
                                            <tr id="RepeaterItemTemplate" runat="server">
                                                <td align="center">
                                                    <asp:CheckBox runat="server" ID="ListCheckBox" />
                                                </td>
                                                <td align="center">
                                                    <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                </td>
                                                <td align="left">
                                                    <table cellpadding="0" cellspacing="0" width="0" border="0">
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton runat="server" CausesValidation="False" ID="EditButton2" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="left">
                                                    <asp:Literal runat="server" ID="AreaNameLiteral"></asp:Literal>
                                                </td>
                                                <td align="center">
                                                    <asp:Literal runat="server" ID="PurposeNameLiteral"></asp:Literal>
                                                </td>
                                                <td align="left">
                                                    <asp:Literal runat="server" ID="DateInLiteral"></asp:Literal>
                                                </td>
                                                <td align="center">
                                                    <asp:Literal runat="server" ID="DateOutLiteral"></asp:Literal>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr class="bgcolor_gray">
                                        <td colspan="7">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="Panel2">
                    <fieldset>
                        <legend>Detail Add</legend>
                        <table>
                            <tr>
                                <td colspan="3">
                                    <asp:Label runat="server" CssClass="warning" ID="WarningDetailLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Area
                                </td>
                                <td valign="top">
                                    :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="AreaDropDownList" runat="server">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="AreaCustomValidator" runat="server" ControlToValidate="AreaDropDownList"
                                        ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="Area Must Be Chosen"></asp:CustomValidator>
                                    <%--<asp:TextBox ID="AreaTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                Visible="false"></asp:TextBox>--%>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Rack
                                </td>
                                <td valign="top">
                                    :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="RackDropDownList" runat="server">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="CustomValidator" runat="server" ControlToValidate="RackDropDownList"
                                        ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="Rack Must Be Chosen"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Purpose
                                </td>
                                <td valign="top">
                                    :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="PurposeDropDownList" runat="server">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="PurposeCustomValidator" runat="server" ControlToValidate="PurposeDropDownList"
                                        ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="Purpose Must Be Chosen"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Time In
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left">
                                    Date&nbsp;
                                    <asp:TextBox ID="DateInTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox><input
                                        type="button" id="button" value="..." onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateInTextBox,'yyyy-mm-dd',this)" />
                                    Time (hh-mm)
                                    <asp:DropDownList ID="HHInDropDownList" runat="server">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="MMInDropDownList" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Time Out
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left">
                                    Date&nbsp;
                                    <asp:TextBox ID="DateOutTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox><input
                                        type="button" id="button3" value="..." onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateOutTextBox,'yyyy-mm-dd',this)" />
                                    Time (hh-mm)&nbsp;<asp:DropDownList ID="HHOutDropDownList" runat="server">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="MMOutDropDownList" runat="server">
                                    </asp:DropDownList>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                        </table>
                        <table cellpadding="3" cellspacing="0" border="0" width="0">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="ResetButton" runat="server" CausesValidation="False" OnClick="ResetButton_Click" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
