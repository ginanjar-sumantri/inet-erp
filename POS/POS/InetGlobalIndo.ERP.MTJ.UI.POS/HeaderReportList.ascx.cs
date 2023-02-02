using System;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web;

public partial class HeaderReportList : System.Web.UI.UserControl
{
    private ReportListBL _reporListBL = new ReportListBL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.ShowData();
        }
    }

    public void Render()
    {
        this.ShowData();
    }

    private void ShowData()
    {
        this.ReportNameDDL.DataSource = _reporListBL.GetReportListDDL(this.ReportType.Value, new UserBL().GetCompanyId(HttpContext.Current.User.Identity.Name));
        this.ReportNameDDL.DataValueField = "ReportPath";
        this.ReportNameDDL.DataTextField = "ReportName";
        this.ReportNameDDL.DataBind();
        this.ReportNameDDL.SelectedIndex = 0;
        this.selectedReportType.Value = this.ReportNameDDL.SelectedValue;
        this.selectedIndexType.Value = this.ReportNameDDL.SelectedIndex.ToString();
        this.selectedReportTypeText.Value = this.ReportNameDDL.SelectedItem.Text;
    }

    public String SelectedValue
    {
        get
        {
            return this.selectedReportType.Value;
        }
        set
        {
            this.selectedReportType.Value = value;
        }
    }

    public String SelectedText
    {
        get
        {
            return this.selectedReportTypeText.Value;
        }
        set
        {
            this.selectedReportTypeText.Value = value;
        }
    }

    public String ReportGroup
    {
        get
        {
            return this.ReportType.Value;
        }
        set
        {
            this.ReportType.Value = value;
        }
    }

    public String SelectedIndex
    {
        get
        {
            return this.ReportNameDDL.SelectedIndex.ToString();
        }
        set
        {
            this.selectedIndexType.Value = value;
        }
    }

    protected void ReportNameDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.selectedReportType.Value = this.ReportNameDDL.SelectedValue;
        this.selectedIndexType.Value = this.ReportNameDDL.SelectedIndex.ToString();
        this.selectedReportTypeText.Value = this.ReportNameDDL.SelectedItem.Text;
    }
}
