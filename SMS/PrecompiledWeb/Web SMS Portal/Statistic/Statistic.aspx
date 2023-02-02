<%@ page title="" language="C#" masterpagefile="~/SMSMasterPage.master" autoeventwireup="true" inherits="SMS.SMSWeb.Statistic.Statistic_Statistic, App_Web_sm2j45sc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAtas" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContentTitle" Runat="Server">
STATISTIC
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">
<div style="padding:5px">
    <asp:Panel runat="server" ID="UserPanel" style="padding:5px;border:solid 1px Black;">
        <ul>
            <li>Today Sent SMS : <asp:Literal runat="server" ID="TodaySentSMSLiteral"></asp:Literal></li>
            <li>Today Incoming SMS : <asp:Literal runat="server" ID="TodayIncomingSMSLiteral"></asp:Literal></li>
            <li>Today Junk SMS : <asp:Literal runat="server" ID="TodayJunkLiteral"></asp:Literal></li>
            <hr />
            <li>This Month Sent SMS : <asp:Literal runat="server" ID="ThisMonthSentSMSLiteral"></asp:Literal></li>
            <li>This Month Incoming SMS : <asp:Literal runat="server" ID="ThisMonthIncomingSMSLiteral"></asp:Literal></li>
            <hr />
        </ul>
        SMS By Category Per Month 
            <asp:DropDownList runat="server" ID="CategoryMonthDropDownList" 
            onselectedindexchanged="CategoryMonthDropDownList_SelectedIndexChanged"
            AutoPostBack="true"></asp:DropDownList>
            <asp:DropDownList runat="server" ID="CategoryYearDropDownList" 
            onselectedindexchanged="CategoryYearDropDownList_SelectedIndexChanged"
            AutoPostBack="true"></asp:DropDownList>
        <table border="1" cellpadding="3px" cellspacing="0" style="margin:3px;" width="450px">
            <tr style="background-color:#CCC;"><th>Category</th><th>Number Of SMS</th></tr>
            <asp:Repeater runat="server" ID="SentSMSByCategoryRepeater" 
                onitemdatabound="SentSMSByCategoryRepeater_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td><asp:Literal runat="server" ID="CategoryLiteral"></asp:Literal></td>
                        <td><asp:Literal runat="server" ID="NumberOfSMSLiteral"></asp:Literal></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="AdminPanel" style="padding:5px;border:solid 1px Black;">
        Sent SMS By User Per Month 
            <asp:DropDownList runat="server" ID="UserMonthDropDownList" 
            onselectedindexchanged="UserMonthDropDownList_SelectedIndexChanged"
            AutoPostBack="true"></asp:DropDownList>
            <asp:DropDownList runat="server" ID="UserYearDropDownList" 
            onselectedindexchanged="UserYearDropDownList_SelectedIndexChanged"
            AutoPostBack="true"></asp:DropDownList>
        <table border="1" cellpadding="3px" cellspacing="0" style="margin:3px;" width="450px">
            <tr style="background-color:#CCC;"><th>User Name</th><th>Number Of SMS</th></tr>
            <asp:Repeater runat="server" ID="SentSMSPerUserRepeater"
                onitemdatabound="SentSMSByUserRepeater_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td><asp:Literal runat="server" ID="UserNameLiteral"></asp:Literal></td>
                        <td><asp:Literal runat="server" ID="NumberOfSMSPerUserLiteral"></asp:Literal></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        Balance Incoming : IDR <asp:Literal runat="server" ID="BalanceIncomingLiteral"></asp:Literal>
        <table border="1" cellpadding="3px" cellspacing="0" style="margin:3px;" width="450px">
            <tr style="background-color:#CCC;"><th>Trans. Date</th><th>Amount</th></tr>
            <asp:Repeater runat="server" ID="BalanceIncomingRepeater" 
                onitemdatabound="BalanceIncomingRepeater_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td><asp:Literal runat="server" ID="DateIncomeBalance"></asp:Literal></td>
                        <td><asp:Literal runat="server" ID="IncomeAmountLiteral"></asp:Literal></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        Balance Outgoing per Year : <asp:DropDownList runat="server" 
            ID="OutBalanceYearDropDownList" 
            onselectedindexchanged="OutBalanceYearDropDownList_SelectedIndexChanged"
            AutoPostBack="true"></asp:DropDownList><asp:Button runat="server" 
            ID="ViewTransactionButton" Text="View Transaction Detail" 
            onclick="ViewTransactionButton_Click" />
        <table border="1" cellpadding="3px" cellspacing="0" style="margin:3px;" width="450px">
            <tr style="background-color:#CCC;"><th>Month</th><th>Amount</th></tr>
            <asp:Repeater runat="server" ID="BalanceOutGoingRepeater" 
                onitemdatabound="BalanceOutGoingRepeater_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td><asp:Literal runat="server" ID="MonthOutgoingBalance"></asp:Literal></td>
                        <td><asp:Literal runat="server" ID="OutgoingAmountLiteral"></asp:Literal></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>
</div>
</asp:Content>

