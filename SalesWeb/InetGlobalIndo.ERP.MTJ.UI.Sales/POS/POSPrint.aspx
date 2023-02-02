<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POSPrint.aspx.cs" Inherits="POS_POSPrint" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--<asp:Literal ID="setruk" runat="server"></asp:Literal>--%>
        <rsweb:ReportViewer ID="ReportViewer" runat="server" Width="100%" ShowPrintButton="true"
            ShowZoomControl="true" ZoomMode="Percent" ZoomPercent="100">
            <LocalReport>
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="TestDataSet_Student" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="subReportDataSet_Phone" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetData"
            TypeName="subReportDataSetTableAdapters.PhoneTableAdapter"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
            TypeName="TestDataSetTableAdapters.StudentTableAdapter"></asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
<%--<script language="javascript">
    window.print();
    window.close();
</script>--%>
