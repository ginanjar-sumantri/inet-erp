<%@ page language="C#" autoeventwireup="true" inherits="Search_Search, App_Web_zljk9oac" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search</title>
    <link href="../Style/Style.css" type="text/css" rel="Stylesheet" />
    <asp:Literal ID="javaScriptDeclaration" runat="server"></asp:Literal>
    <style type="text/css">
        BODY
        {
            font-family: "Tahoma";
            font-size: 12px;
        }
        th
        {
            padding: 5px 5px 5px 5px;
            height: 30;
            background-color: #999999;
        }
        th a:link, a:visited
        {
            text-decoration: none;
            font-family: "Tahoma";
            font-size: 12px;
            color: #ffffff;
        }
    </style>
</head>
<body style="margin: 0 0 0 0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptManager" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <div>
        <div style="height: 5px; background-color: Red">
        </div>
        <div style="background-color: #feed8a; font-size: 14px; font-weight: bold; padding: 5px 5px 5px 5px">
            Search</div>
        <br />
        <fieldset>
            <legend>Search Criteria</legend>
            <table>
                <tr>
                    <td>
                        Search Field
                    </td>
                    <td colspan="2">
                        <asp:UpdatePanel ID="updPanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="whereField" runat="server" AutoPostBack="true" OnSelectedIndexChanged="whereField_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        Condition
                    </td>
                    <td>
                        <asp:UpdatePanel ID="updPanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="whereType" runat="server">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="whereField" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:TextBox ID="whereValue" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:ImageButton ID="btnFilter" runat="server" Text="Search" OnClick="btnFilter_Click" />
                        <asp:ImageButton ID="btnShowAll" runat="server" Text="Show All" OnClick="btnShowAll_Click" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="whereCon" runat="server" />
            <asp:HiddenField ID="sortField" runat="server" />
            <asp:HiddenField ID="ascDesc" Value="false" runat="server" />
        </fieldset>
        <br />
        <table border="1" cellspacing="0" cellpadding="3">
            <tr>
                <th width="20">
                </th>
                <asp:Repeater ID="headerTabel" runat="server" OnItemDataBound="headerTabel_ItemDataBound"
                    OnItemCommand="headerTabel_ItemCommand">
                    <ItemTemplate>
                        <th>
                            <asp:LinkButton ID="headerText" runat="server"></asp:LinkButton>
                        </th>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>
            <asp:Repeater ID="hasilPencarian" runat="server" OnItemDataBound="hasilPencarian_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Button ID="btnPilih" runat="server" />
                        </td>
                        <asp:Literal ID="rowContent" runat="server"></asp:Literal>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    </form>
</body>
</html>
