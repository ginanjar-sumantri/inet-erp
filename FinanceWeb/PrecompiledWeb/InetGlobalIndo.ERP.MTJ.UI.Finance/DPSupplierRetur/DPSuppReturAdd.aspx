﻿<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.DPSupplierRetur.DPSuppReturAdd, App_Web_z_knjefe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="NextButton">
        <table cellpadding="3" cellspacing="0" width="0" border="0">
            <tr>
                <td class="tahoma_14_black" colspan="3">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Trans Date
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                    <%--<input id="Button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)"
                        value="..." />--%>
                    <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>
                    <%-- <img src="../calendar/img.gif" id="headline_date_start" style="cursor: pointer; border: 1px solid red;"
                        title="Date selector" onmouseover="this.style.background='red';" onmouseout="this.style.background=''"
                        align="absmiddle" />

                    <script type="text/javascript">
	                Calendar.setup({
		                inputField     :    "ctl00_DefaultBodyContentPlaceHolder_DateTextBox",    	// id of the input field
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
                    Supplier
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="SuppDropDownList">
                    </asp:DropDownList>
                    <asp:CustomValidator runat="server" ID="SuppCustomValidator" ErrorMessage="Supplier Must be Filled"
                        Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="SuppDropDownList"></asp:CustomValidator>
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
                    <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>
                    <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                    characters left
                </td>
            </tr>
            <tr>
                <td colspan="3">
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
