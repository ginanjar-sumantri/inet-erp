<%@ control language="C#" autoeventwireup="true" inherits="HeaderReportList, App_Web_3ejdezsv" %>
<asp:Panel ID="Panel1" runat="server" Height="29px" Width="594px">
    <%--<asp:Literal ID="lblReportName" runat="server" Text="Report Name"></asp:Literal>--%>
    &nbsp;<asp:DropDownList ID="ReportNameDDL" runat="server" Width="367px" 
    AutoPostBack="true"
    OnSelectedIndexChanged="ReportNameDDL_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:HiddenField ID="selectedReportType" runat="server" />
    <asp:HiddenField ID="selectedReportTypeText" runat="server" />
    <asp:HiddenField ID="selectedIndexType" runat="server" />
    <asp:HiddenField ID="ReportType" runat="server" />
</asp:Panel>
