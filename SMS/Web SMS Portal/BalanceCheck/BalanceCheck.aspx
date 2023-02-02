<%@ Page Language="C#" MasterPageFile="~/SMSMasterPage.master" AutoEventWireup="true"
    CodeFile="BalanceCheck.aspx.cs" Inherits="SMS.SMSWeb.BalanceCheck.BalanceCheck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAtas" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContentTitle"
    runat="Server">
    BALANCE CHECK
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
    <asp:ScriptManager runat="server" ID="ScriptManager">
    </asp:ScriptManager>
    <div style="padding: 5px 5px 5px 5px;">
        <div>
            <span style="float: left">Balance Masking : </span> &nbsp; <asp:Literal runat="server" ID="BalanceMaskingLiteral"></asp:Literal> IDR
        </div>
        <div>
            <span style="float: left">SMS Gateway Status : </span> &nbsp; 
            <asp:UpdatePanel runat="server" ID="UpdatePanelGatewayStatus">
                <ContentTemplate>
                    <asp:Label runat="server" ID="AvailableStatus" Text="Available" ForeColor="LimeGreen" Font-Bold="true"></asp:Label>
                    <asp:Label runat="server" ID="NotAvailableStatus" Text="Not Available" ForeColor="Tomato" Font-Bold="true"></asp:Label>
                    <asp:Timer runat="server" ID="AvailabilityTimer" Interval="3000" 
                        ontick="AvailabilityTimer_Tick"></asp:Timer>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        Mobile Operator Code for Balance Info :
        <asp:TextBox runat="server" ID="balanceCheckCodeTextBox" Width="50px"></asp:TextBox>
        <br />
        <asp:Button runat="server" ID="btnRequestBalanceInfo" Text="Request Balance Info"
            OnClick="btnRequestBalanceInfo_Click" />
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <asp:Label runat="server" ID="BalanceInfoResponse"></asp:Label>
                <asp:Timer runat="server" ID="TimerRequestBalanceInfo" Enabled="false" Interval="5000"
                    OnTick="TimerRequestBalanceInfo_Tick">
                </asp:Timer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
