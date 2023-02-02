<%@ page title="" language="C#" masterpagefile="~/SMSMasterPage.master" autoeventwireup="true" inherits="SMS.SMSWeb.Download.Download, App_Web_zyftiawf" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style type="text/css">
    a.downloadLink:link,a.downloadLink:visited {text-decoration : none ;color: #666;font-size:20px;}
    a.downloadLink:hover{color:#ccc;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAtas" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContentTitle" Runat="Server">
    Download Client Application
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">
    <ul>
        <li><a class="downloadLink" href="../GatewayApplication.zip">Gateway Application (69 KB)</a></li>
        <li><a class="downloadLink" href="../dotnetfx35.zip">.Net Framework (201 MB)</a></li>
        <li><a class="downloadLink" href="../WEBSMS_MANUAL_GUIDE-CORPORATE_LICENSE.pdf">WEBSMS MANUAL GUIDE - CORPORATE LICENSE (944 KB)</a></li>
    </ul>
</asp:Content>

