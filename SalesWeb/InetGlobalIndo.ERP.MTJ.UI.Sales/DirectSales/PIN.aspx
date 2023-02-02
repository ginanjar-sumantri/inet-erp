<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PIN.aspx.cs" Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.DirectSales.PIN" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Serial Number</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" border="0" width="0" style="background-color: #F2F2F2">
            <tr>
                <td align="center">
                    Serial Number
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-style: solid;
                        border-spacing: 1px; border-left: none; border-right: none">
                        <tr>
                            <th>
                                Serial Number
                            </th>
                            <th>
                                PIN
                            </th>
                        </tr>
                        <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                            <ItemTemplate>
                                <tr id="RepeaterItemTemplate" runat="server">
                                    <td>
                                        <asp:Literal runat="server" ID="SerialNmbrLiteral"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="PINLiteral"></asp:Literal>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
