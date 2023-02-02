<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="BillOfLadingAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.BillOfLading.BillOfLadingAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <%--<link href="../calendar/calendar-blue2.css" rel="stylesheet" type="text/css" media="all"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script type="text/javascript" src="../calendar/lang/calendar-en.js"></script>

    <script type="text/javascript" src="../calendar/calendar-setup.js"></script>--%>
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script type="text/javascript">
        function CheckUncheck(_prmFgLocationCheckBox, _prmLocationDDL) {
            if (_prmFgLocationCheckBox.checked == true) {
                _prmLocationDDL.disabled = false;
            }
            else if (_prmFgLocationCheckBox.checked == false) {
                _prmLocationDDL.disabled = true;
                _prmLocationDDL.value = "null";
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="NextButton" runat="server">
        <table border="0" width="0" cellpadding="3" cellspacing="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" width="0" cellpadding="3" cellspacing="0">
                        <tr>
                            <td>
                                Trans Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TransDateTextBox" runat="server" MaxLength="60" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <%--<input id="button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_TransDateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />--%>
                                    <asp:Literal ID="TransDateLiteral" runat="server"></asp:Literal>
                                <%--<img src="../calendar/img.gif" id="headline_date_start" style="cursor: pointer; border: 1px solid red;"
                        title="Date selector" onmouseover="this.style.background='red';" onmouseout="this.style.background=''"
                        align="absmiddle" />

                    <script type="text/javascript">
	                Calendar.setup({
		                inputField     :    "ctl00_DefaultBodyContentPlaceHolder_TransDateTextBox",    	// id of the input field
		                ifFormat       :    "%Y-%m-%d",        	// format of the input field
		                showsTime      :    false,            	// will display a time selector
		                button         :    "headline_date_start", 	 	// trigger for the calendar (button ID)
		                singleClick    :    true,           	// double-click mode
		                step           :    1,              	// show all years in drop-down boxes (instead of every other year as default)
		                showOthers 	   :	true
	                });
                    </script>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Customer
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="CustNameDropDownList" AutoPostBack="True" OnSelectedIndexChanged="CustNameDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="Customer Must Be Filled"
                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="CustNameDropDownList"></asp:CustomValidator>
                            </td>
                        </tr>
                        <%--<tr>
                            <td>
                                DO No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="DONoDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="DO No Must Be Filled"
                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="DONoDropDownList"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Warehouse
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="WarehouseCodeDropDownList" AutoPostBack="true"
                                    OnSelectedIndexChanged="WarehouseCodeDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="WarehouseCustomValidator" runat="server" ErrorMessage="Warehouse Must Be Filled"
                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="WarehouseCodeDropDownList"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Warehouse Subled
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="WrhsSubledDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Warehouse Location
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="FgLocationCheckBox" runat="server" Text="Single Location" />
                                <asp:DropDownList ID="WrhsLocationDropDownList" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                Car No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CarNoTextBox" runat="server" MaxLength="30" Width="210"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Driver
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="DriverTextBox" runat="server" MaxLength="40" Width="280"></asp:TextBox>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Remark
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="RemarkTextBox" runat="server" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>
                                <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                characters left
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
                                <asp:ImageButton ID="NextButton" runat="server" OnClick="NextButton_Click" />
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
    </asp:Panel>
</asp:Content>
