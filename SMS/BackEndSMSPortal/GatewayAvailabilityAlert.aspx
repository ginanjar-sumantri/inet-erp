<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GatewayAvailabilityAlert.aspx.cs" Inherits="SMS.BackEndSMSPortal.GatewayAvailabilityAlert" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GATEWAY ALERT</title>
</head>
<body style="background-color:Black;color:White;font-size:20px;font-weight:bold;font-family:'Showcard Gothic';">
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager"></asp:ScriptManager>
    <div>
        <asp:UpdatePanel runat="server" ID="UpdatePanelAttentionAll">
            <ContentTemplate>
                <asp:Timer runat="server" ID="TimerAttention" Interval="5000" 
                    ontick="TimerAttention_Tick"></asp:Timer>
                <asp:HiddenField runat="server" ID="AttentionCounter" Value="0" />
                <asp:Panel runat="server" ID="AttentionPanel" Visible="false">
                    <div id="attention" style="text-align:center;font-size:100px;">ATTENTION !!!</div>
                </asp:Panel>
                <center>
                    <table border="1" cellpadding="5px" cellspacing="0">
                    <tr style="background-color:#333;"><th>Organization Name</th><th>Hosted</th><th>Gateway Available</th></tr>
                    <asp:Repeater runat="server" ID="OrganizationRepeater" 
                            onitemdatabound="OrganizationRepeater_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Label runat="server" ID="OrganizationNameLiteral"></asp:Label></td>
                                <td><asp:Label runat="server" ID="HostedLiteral"></asp:Label></td>
                                <td><asp:Label runat="server" ID="GatewayAvailableLiteral"></asp:Label></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </table>
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script language="javascript">
            function BlinkingAttention() {
                if (document.getElementById('attention').style.color != "Red")
                    document.getElementById('attention').style.color = "Red";
                else
                    document.getElementById('attention').style.color = "White";
            }
            setInterval("BlinkingAttention();", 500);
        </script>
    </div>
    </form>
</body>
</html>
