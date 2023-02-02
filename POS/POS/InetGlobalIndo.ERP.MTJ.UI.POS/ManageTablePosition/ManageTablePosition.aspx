<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageTablePosition.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.POS.ManageTable.ManageTablePosition" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<asp:page onpreload="Page_PreLoad" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>POS Internet Layout Map</title>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    <asp:Literal runat="server" ID="CSSLiteral"></asp:Literal>
    <style type="text/css">
        body
        {
            margin: 0px 0px 0px 0px;
        }
        .divBoxAttributeMonitor
        {
            float: left;
            background: #FE0;
            border: solid 2px #AAA;
            padding: 5px 5px 5px 5px;
        }
        .selectedBox
        {
            background: #6F6;
        }
    </style>

    <script src="../JQuery/jquery-1.2.6.min.js" type="text/javascript"></script>

    <script src="../JQuery/jquery.event.drag-1.5.min.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
    var posXBox = new Array () ;
    var posYBox = new Array () ;
    $(document).ready(function() {
        //$("#Panel").fadeOut("fast");
        //$("#Panel").fadeIn("fast");
    });
    function composePosition (x,y,indexBox){
        strPosition = "" ;
        splitPos = $("#hiddenBoxPosition").attr("value").split('|') ;
        for ( var i = 1 ; i <= <%=_boxCount%> ; i ++ )
            if ( i != indexBox )
                strPosition += "|" + splitPos[i-1] ;
            else
                strPosition += "|" + x + "," + y ;
        $("#hiddenBoxPosition").attr("value",strPosition.substring(1));
    }
    function IsNotCollide (x,y,boxIndex) {
        for ( var i = 1 ; i <= <%=_boxCount %> ; i ++ )
            if ( boxIndex != i )
                if (   (  ( posXBox[i] * 1 <= x && x <= (posXBox[i]*1+<%=_lebarBox%>) ) || ( x <= posXBox[i] * 1 && posXBox[i] * 1 <= (x+<%=_lebarBox%>) )  )  &&  (  ( posYBox[i] * 1 <= y && y <= (posYBox[i]*1+<%=_tinggiBox%>) )  ||  ( y <= posYBox[i] * 1 && posYBox[i] * 1 <= (y+<%=_tinggiBox%>) )  )   ) return false ;
        return true ;
    }
    function clearSelectedBox ( ) {
        for ( var i = 1 ; i <= <%=_boxCount %> ; i ++ )
            $("#divBox" + i).removeClass ("selectedBox");
    }
    $(function() {
        <%for (int i = 1 ; i <= Convert.ToInt16 ( _boxCount ) ; i ++ ) { %>
            $("#divBox<%=i.ToString()%>").bind("drag", function(event) {
                posX = Math.round ( event.offsetX / <%=_gridSpace%> ) * <%=_gridSpace%> ;
                if (posX < <%=_posisiXPanelLayout%>) posX = <%=_posisiXPanelLayout%>;
                if ( posX > <%=_posisiXPanelLayout%> + <%=_lebarPanelLayout%> - <%=_lebarBox%> ) posX = <%=_posisiXPanelLayout%> + <%=_lebarPanelLayout%> - <%=_lebarBox%> ;
                posY = Math.round ( event.offsetY / <%=_gridSpace%> ) * <%=_gridSpace%> ;
                if ( posY < <%=_posisiYPanelLayout%> ) posY = <%=_posisiYPanelLayout%> ;
                if ( posY > <%=_posisiYPanelLayout%> + <%=_tinggiPanelLayout%> - <%=_tinggiBox%> ) posY = <%=_posisiYPanelLayout%> + <%=_tinggiPanelLayout%> - <%=_tinggiBox%> ;
                if( IsNotCollide ( posX, posY, <%=i.ToString()%> ) ) {
                    $(this).css({
                        left: posX,
                        top: posY
                    });
                    posXBox[<%=i.ToString()%>] = posX ;
                    posYBox[<%=i.ToString()%>] = posY ;
                }
            });
            $("#divBox<%=i.ToString()%>").mousedown(function(event) {
                clearSelectedBox ();
                $(this).addClass("selectedBox") ;
                var _stringBoxAttribute = "Box Attribute : <br>" ;
                _stringBoxAttribute += " id : <%=i.ToString()%> <br>" ;
                _stringBoxAttribute += " x : " + posXBox[<%=i.ToString()%>] + "<br>";
                _stringBoxAttribute += " y : " + posYBox[<%=i.ToString()%>] + "<br>";
                $("#BoxAttributeMonitor").html(_stringBoxAttribute) ;
            });
            $("#divBox<%=i.ToString()%>").bind("dragend", function(event) {
                composePosition ( posXBox[<%=i.ToString()%>] , posYBox[<%=i.ToString()%>] , <%=i.ToString()%> ) ;
                var _stringBoxAttribute = "Box Attribute : <br>" ;
                _stringBoxAttribute += " id : <%=i.ToString()%> <br>" ;
                _stringBoxAttribute += " x : " + posXBox[<%=i.ToString()%>] + "<br>";
                _stringBoxAttribute += " y : " + posYBox[<%=i.ToString()%>] + "<br>";
                $("#BoxAttributeMonitor").html(_stringBoxAttribute) ;
            });
        //}
        <%}%>
    });
    </script>

    <!-- bagian atas -->
    <div style="height: 50px;">
        <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
    </div>
    <!-- //bagian atas -->
    <div id="Panel" runat="server" class="divPanel">
        <asp:Literal runat="server" ID="literalBoxes"></asp:Literal>
    </div>
    <asp:HiddenField runat="server" ID="hiddenBoxCount" Value="0" />
    <asp:HiddenField runat="server" ID="hiddenBoxPosition" />
    <div style="float: left; width: 250px; padding: 5px;">
        <fieldset runat="server">
            <legend>Floor</legend>
            <div style="padding: 5px;">
                Floor :
                <asp:DropDownList runat="server" ID="FloorDropDownList" AutoPostBack="true" OnSelectedIndexChanged="FloorDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <table>
                <tr>
                    <td width="380px">
                        <div style="float: left; padding-left: 5px;">
                            <asp:ImageButton runat="server" ID="btnSaveLayout" Text="Save Layout" OnClick="btnSaveLayout_Click" />
                        </div>
                        <div style="float: left; padding-left: 5px;">
                            <asp:ImageButton runat="server" ID="btnDeleteLayout" Text="Delete Layout" OnClick="btnDeleteLayout_Click" />
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset runat="server">
            <legend>Box</legend>
            <div style="clear: both; padding: 5px;">
                <asp:ImageButton runat="server" ID="btnAddBox" Text="Add Box" />
                <asp:ImageButton runat="server" ID="btnDecreaseBox" Text="Decrease Box" />
                <asp:Label runat="server" ID="debuger"></asp:Label>
            </div>
            <div style="height: 100px">
                <div id="BoxAttributeMonitor" class="divBoxAttributeMonitor">
                    Box attribute :</div>
            </div>
        </fieldset>
        <fieldset runat="server">
        <legend>Layout</legend>
            <div>
                <asp:ScriptManager ID="scriptMgr" runat="server" />
                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                    <ContentTemplate>
                        Max Size :
                        <asp:Label ID="sizeLabel" runat="server"></asp:Label></ContentTemplate>
                </asp:UpdatePanel>
                <asp:FileUpload ID="FileUpload" runat="server" />
                <asp:ImageButton ID="Savebutton" runat="server" Text="Save" OnClick="Savebutton_Click" />
            </div>
            <div>
                <div>
                    New Layout :
                    <asp:TextBox runat="server" ID="NewLayoutTextBox"></asp:TextBox></div>
                <div style="float: left;">
                    Resolution :
                    <asp:DropDownList ID="HeightDDL" runat="server">
                        <asp:ListItem Text="400" Value="400"></asp:ListItem>
                        <asp:ListItem Text="485" Value="485"></asp:ListItem>
                    </asp:DropDownList>
                    X
                    <asp:DropDownList ID="WidthDDL" runat="server">
                        <asp:ListItem Text="500" Value="500"></asp:ListItem>
                        <asp:ListItem Text="700" Value="700"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <asp:ImageButton runat="server" ID="NewLayoutButton" Text="New Layout" OnClick="NewLayoutButton_Click" />
            </div>
        </fieldset>
    </div>
    </form>
</body>
</html>
