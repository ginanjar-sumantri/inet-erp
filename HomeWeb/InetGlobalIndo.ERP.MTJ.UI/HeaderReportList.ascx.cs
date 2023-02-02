using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

public partial class HeaderReportList : System.Web.UI.UserControl
{
    protected string ReportType = "NCPPeriod";

    private ReportListBL _reporListBL = new ReportListBL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) {
            this.ReportNameDDL.DataSource = _reporListBL.GetReportListDDL(this.ReportType);
            this.ReportNameDDL.DataValueField = "Value";
            this.ReportNameDDL.DataTextField = "Key";
            this.ReportNameDDL.DataBind();
        
        }

    }
   
}
