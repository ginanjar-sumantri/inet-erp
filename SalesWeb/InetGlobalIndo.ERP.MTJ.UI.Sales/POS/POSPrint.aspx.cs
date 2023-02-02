using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;

public partial class POS_POSPrint : System.Web.UI.Page
{
    private string _reportPath0 = "";
    private POSBL _posBL = new POSBL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Request.QueryString["datacount"]) > 10)
            _reportPath0 = "POS/POSNotaPenjualan.rdlc";
        else
            _reportPath0 = "POS/POSNotaPenjualanHalf.rdlc";

        ReportDataSource _reportDataSource1 = new ReportDataSource();
        _reportDataSource1 = _posBL.GetTransactionData(Request.QueryString["transNmbr"]);
        this.ReportViewer.LocalReport.DataSources.Clear();
        this.ReportViewer.LocalReport.DataSources.Add(_reportDataSource1);
        this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
        this.ReportViewer.DataBind();

        ReportParameter[] _reportParam = new ReportParameter[2];
        _reportParam[0] = new ReportParameter("TransNmbr", Request.QueryString["transNmbr"], true);
        _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);

        this.ReportViewer.LocalReport.SetParameters(_reportParam);
        this.ReportViewer.LocalReport.Refresh();

        //String _isiSetruk = "" ;
        //String[] _dataAct = Request.QueryString["dataAct"].Split(':');
        //String[] _dataChunk = Request.QueryString["dataChunk"].Split(',');
        //String[] _dataPayment = Request.QueryString["dataPayment"].Split(':');

        //_isiSetruk += "Transaction Date : " + _dataAct[0] + "<br>" ;
        //_isiSetruk += "Customer Name : " + _dataAct[1] + "<br>";
        //_isiSetruk += "Sales Name : " + _dataAct[2] + "<br>";
        //_isiSetruk += "<table width='300'>";
        //_isiSetruk += "<tr><th>Item</th><th>Qty</th><th>Price</th><th>Total</th></tr>" ;        
        //foreach (String _dataRow in _dataChunk) {
        //    String[] _dataItem = _dataRow.Split(':');
        //    _isiSetruk += "<tr><td>" + _dataItem[2] + "</td><td>" + _dataItem[3] + "</td><td>" + _dataItem[5] + "</td><td>" + _dataItem[6] + "</td></tr>" ;
        //}
        //_isiSetruk += "</table>";
        //_isiSetruk += "Sub Total : " + _dataPayment[0] + "<br>" ;
        //_isiSetruk += "Discount (" + _dataPayment[1] + " %) : " + _dataPayment[2] + "<br>";
        //_isiSetruk += "Tax (" + _dataPayment[3] + " %) : " + _dataPayment[4] + "<br>";
        //_isiSetruk += "Total : " + _dataPayment[5] + "<br>";
        //_isiSetruk += "Payment : " + _dataPayment[6] + "<br>";
        //_isiSetruk += "Return Cash : " + _dataPayment[7] + "<br>";

        //this.setruk.Text = _isiSetruk;
    }
}
