<%@ page title="" language="C#" masterpagefile="~/SMSMasterPage.master" autoeventwireup="true" inherits="SMS.SMSWeb.Statistic.StatisticDeatilTransaction, App_Web_sm2j45sc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAtas" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContentTitle" Runat="Server">
STATISTIC
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">
<div style="padding:5px">
    <asp:Button runat="server" ID="BackButton" Text="Back" 
        onclick="BackButton_Click1"/>
    <table width="100%" border="1" cellspacing="0" cellpadding="3px">
        <tr><th>Trans Date</th><th>Description</th><th>Amount</th><th>Inc/Dec</th></tr>
        <asp:Repeater runat="server" ID="DetailTransRepeater" 
            onitemdatabound="DetailTransRepeater_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td><asp:Literal runat="server" ID="TransDateLiteral"></asp:Literal></td>
                    <td><asp:Literal runat="server" ID="DescriptionLiteral"></asp:Literal></td>
                    <td><asp:Literal runat="server" ID="AmountLiteral"></asp:Literal></td>
                    <td><asp:Literal runat="server" ID="IncDecLiteral"></asp:Literal></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</div>
</asp:Content>

