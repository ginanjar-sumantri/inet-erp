<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderReportList.ascx.cs" Inherits="HeaderReportList" %>
<asp:Panel ID="Panel1" runat="server" Height="29px" Width="594px">
    
    &nbsp;<asp:DropDownList ID="ReportNameDDL" runat="server" 
        Width="367px" onselectedindexchanged="ReportNameDDL_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:HiddenField ID="selectedReportType" runat="server" />
    <asp:HiddenField ID="selectedReportTypeText" runat="server" />
    <asp:HiddenField ID="selectedIndexType" runat="server" />
    <asp:HiddenField ID="ReportType" runat="server" />
</asp:Panel>
