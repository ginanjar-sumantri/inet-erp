<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="GoodsInOutDetail.aspx.cs" Inherits="VTSWeb.UI.GoodsInOutDetail" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
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
                <asp:Panel ID="PanelHeader" runat="server">
                    <fieldset>
                        <legend>Header</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td colspan="3">
                                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Transaction Number
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="NumberTransTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                        Width="200"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Carry By
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="CarryByTextBox" Width="200" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Status
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label ID="StatusLabel" runat="server" Text="LabelStatus" Enabled="False"></asp:Label>
                                    <asp:HiddenField ID="StatusHiddenField" runat="server" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    Requested By
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="RequestedByTextBox" Width="200" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    File Number
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="NumberFileTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                        Width="200"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Approved By
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="ApprovedByTextBox" runat="server" Width="200" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Transaction Type
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="TransactionTypeTextBox" Width="200" ReadOnly="true"
                                        BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Posted By
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label ID="PostedByLabel" runat="server" Text="EditUserNameLabel" Enabled="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Company Name
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="CompanyNameTextBox" Width="200" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                    <asp:HiddenField ID="CustCodeHiddenField" runat="server" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    Entry Date
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="EntryDateTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                        Width="90"> </asp:TextBox>
                                    &nbsp;
                                    <asp:TextBox ID="HHEntryDateTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                        Width="30">
                                    </asp:TextBox>:
                                    <asp:TextBox ID="MMEntryDateTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                        Width="30">
                                    </asp:TextBox>
                                    &nbsp;WIB
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Transaction Date
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="TransactionDateTextBox" Width="90" ReadOnly="true"
                                        BackColor="#CCCCCC"></asp:TextBox>&nbsp;
                                    <asp:TextBox ID="HHTrTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                        Width="30">
                                    </asp:TextBox>:
                                    <asp:TextBox ID="MMTrTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                        Width="30">
                                    </asp:TextBox>
                                    &nbsp;WIB
                                </td>
                                <td>
                                </td>
                                <td>
                                    Entry User Name
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label ID="EntryUserNameLabel" runat="server" Text="EditUserNameLabel" Enabled="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Rack
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="RackName" Width="200" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Edit Date
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="EditDateTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                        Width="90"> </asp:TextBox>
                                    &nbsp;
                                    <asp:TextBox ID="HHEditDateTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                        Width="30">
                                    </asp:TextBox>
                                    :
                                    <asp:TextBox ID="MMEditDateTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                        Width="30">
                                    </asp:TextBox>
                                    &nbsp;WIB
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Edit User Name
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label ID="EditUserNameLabel" runat="server" Text="EditUserNameLabel" Enabled="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7">
                                    <table border="0" cellpadding="3" cellspacing="0" width="0">
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
                                            <td>
                                                <asp:ImageButton ID="CompleteButton2" runat="server" CausesValidation="False" OnClick="CompleteButton2_Click" />
                                            </td>
                                            <%--<td>
                                                <asp:ImageButton ID="RejectButton" runat="server" OnClick="RejectButton_Click" />
                                            </td>--%>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelDetail" runat="server">
                    <fieldset>
                        <legend>Detail</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <%--<asp:ImageButton ID="AddButton" runat="server" CausesValidation="False" OnClick="AddButton_Click" />--%>
                                                &nbsp;<asp:ImageButton ID="DeleteButton" runat="server" CausesValidation="False"
                                                    OnClick="DeleteButton_Click" />
                                            </td>
                                            <%--<td>
                                                <asp:ImageButton ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" />
                                            </td>--%>
                                            <td align="right">
                                                <%-- <asp:Panel DefaultButton="DataPagerButton" ID="DataPagerPanel" runat="server">--%>
                                                <table border="0" cellpadding="2" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td valign="middle">
                                                            <b></b>
                                                        </td>
                                                        <%--<asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater" runat="server" OnItemCommand="DataPagerTopRepeater_ItemCommand"
                                                            OnItemDataBound="DataPagerTopRepeater_ItemDataBound">
                                                            <ItemTemplate>--%>
                                                        <td>
                                                            <%--<asp:LinkButton ID="PageNumberLinkButton" runat="server"></asp:LinkButton>
                                                                    <asp:TextBox Visible="false" ID="PageNumberTextBox" runat="server" Width="30px"></asp:TextBox>--%>
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
                                    <asp:Label runat="server" ID="WarningDeleteLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="CheckHidden" runat="server" />
                                    <asp:HiddenField ID="TempHidden" runat="server" />
                                    <table cellpadding="3" cellspacing="1" width="100%" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 5px">
                                                <asp:CheckBox runat="server" ID="AllCheckBox" />
                                            </td>
                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                                <b>Action</b>
                                            </td>
                                            <%--<td style="width: 300px" class="tahoma_11_white" align="center">
                                            <b>Contact Name</b>
                                        </td>--%>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>Item Code</b>
                                            </td>
                                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                                <b>Product Name</b>
                                            </td>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>Serial Number</b>
                                            </td>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>Remark</b>
                                            </td>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>ElectriCity Numerik</b>
                                            </td>
                                        </tr>
                                        <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                                            <%--OnItemCommand="ListRepeater_ItemCommand">--%>
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
                                                                    <asp:ImageButton runat="server" ID="EditButton2" CausesValidation="False" />
                                                                </td>
                                                                <%--<td>
                                                                <asp:ImageButton ID="RejectButton" runat="server" />
                                                            </td>--%>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <%-- <td align="left">
                                                    <asp:Literal runat="server" ID="ContactNameLiteral"></asp:Literal>
                                                </td>--%>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="ItemCodeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="ProductNameLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="SerialNumberLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="RemarkLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="ElectriCityLiteral"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Panel runat="server" ID="Panel1">
                                <fieldset>
                                    <legend>Detail Add</legend>
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td colspan="3">
                                                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                                            </td>
                                        </tr>
                                        <%-- <tr>
                                <td colspan="2">
                                    ContactName
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ContactNameDropDownList" runat="server">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="ContactNameCustomValidator" runat="server" ControlToValidate="ContactNameDropDownList"
                                        ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="Company Name Must Be Chosen"></asp:CustomValidator>
                                </td>
                            </tr>
--%>
                                        <%--  <tr>
                                <td>
                                    Code Contact
                                </td>
                                <td colspan="2">
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="CodeContactTextBox" runat="server" Width="300"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server" ErrorMessage="NumberFile Must Be Filled"
                                        Text="*" ControlToValidate="CodeContactTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>
                                        <tr>
                                            <td>
                                                Product Name
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ProductNameTextBox" runat="server" Width="300"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="NameRequiredFieldValidator0" runat="server" ControlToValidate="ProductNameTextBox"
                                                    Display="Dynamic" ErrorMessage="Product Name Must Be Filled" Text="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Serial Number
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="SerialNumberTextBox" runat="server" Width="300"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Remark
                                            </td>
                                            <td valign="top">
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="RemarkTextBox" runat="server" Width="300" Height="100" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <%-- <tr>
                        <td valign="top">
                            Status
                        </td>
                        <td valign="top" colspan="2">
                            :
                        </td>
                        <td>
                            <asp:CheckBox ID="StatusComplete" runat="server" />
                            &nbsp;Complete
                        </td>
                    </tr>--%>
                                        <tr>
                                            <td valign="top">
                                                Electri City Numerik
                                            </td>
                                            <td valign="top">
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ElectriCityTextBox" runat="server" Width="300"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <b>
                                                    <asp:Label runat="server" ID="WarningDetailLabel"></asp:Label></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
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
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </asp:Panel>
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
                                <td>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <b>&nbsp; Item Code</b>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ItemCodeDDL" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ItemCodeDDL_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;
                                                <%--<asp:ImageButton ID="GoImageButton" runat="server" OnClick="GoImageButton_Click" />--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="WarningLabelTrGood"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="CheckHiddenTrGood" runat="server" />
                                    <asp:HiddenField ID="TempHiddenTrGood" runat="server" />
                                    <table cellpadding="3" cellspacing="1" width="100%" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 5px">
                                                <asp:CheckBox runat="server" ID="AllCheckBoxTrGood" />
                                            </td>
                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <%--<td style="width: 300px" class="tahoma_11_white" align="center">
                                            <b>Contact Name</b>
                                        </td>--%>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Item Code</b>
                                            </td>
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Product Name</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Serial Number</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Remark</b>
                                            </td>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>ElectriCity Numerik</b>
                                            </td>
                                        </tr>
                                        <asp:Repeater runat="server" ID="ListRepeaterTrGood" OnItemDataBound="ListRepeater_ItemDataBoundTrGood">
                                            <%--OnItemCommand="ListRepeater_ItemCommand">--%>
                                            <ItemTemplate>
                                                <tr id="RepeaterItemTemplate" runat="server">
                                                    <td align="center">
                                                        <asp:CheckBox runat="server" ID="ListCheckBox" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="ItemCodeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="ProductNameLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="SerialNumberLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="RemarkLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="ElectriCityLiteral"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table border="0" cellpadding="3" cellspacing="0" width="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="SaveButton2" runat="server" CausesValidation="False" OnClick="SaveButton2_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="DeleteButton2" runat="server" CausesValidation="False" OnClick="DeleteButton2_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Panel runat="server" ID="PanelPrintIn">
                    <%--<fieldset>
                        <legend>Print Preview</legend>--%>
                    <table>
                        <tr align="center">
                            <td align="center">
                                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="970" ShowPrintButton="true"
                                    ShowZoomControl="true" ZoomMode="Percent" ZoomPercent="100">
                                </rsweb:ReportViewer>
                            </td>
                        </tr>
                    </table>
                    <%--  </fieldset>--%>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
