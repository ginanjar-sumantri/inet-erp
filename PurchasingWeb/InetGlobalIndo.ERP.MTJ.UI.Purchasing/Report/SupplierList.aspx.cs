using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.Report
{
    public partial class SupplierList : ReportBase
    {
        private ReportPurchaseBL _reportBL = new ReportPurchaseBL();
        private SupplierBL _suppBL = new SupplierBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath0 = "Report/SupplierList.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDSupplierList, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer.Attributes.Add("style", "margin-bottom: 30px;");

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = "Supplier List Report";

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer.Visible = false;

                this.ShowSuppGroup();
                this.ShowSuppType();

                this.ClearData();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ShowSuppGroup()
        {
            this.SuppGroupCheckBoxList.ClearSelection();
            this.SuppGroupCheckBoxList.Items.Clear();
            this.SuppGroupCheckBoxList.DataSource = this._suppBL.GetListSuppGroupForDDL();
            this.SuppGroupCheckBoxList.DataValueField = "SuppGroupCode";
            this.SuppGroupCheckBoxList.DataTextField = "SuppGroupName";
            this.SuppGroupCheckBoxList.DataBind();
        }

        protected void ShowSuppType()
        {
            this.SuppTypeCheckBoxList.ClearSelection();
            this.SuppTypeCheckBoxList.Items.Clear();
            this.SuppTypeCheckBoxList.DataSource = this._suppBL.GetListSuppTypeForDDL();
            this.SuppTypeCheckBoxList.DataValueField = "SuppTypeCode";
            this.SuppTypeCheckBoxList.DataTextField = "SuppTypeName";
            this.SuppTypeCheckBoxList.DataBind();
        }

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();

            this.SuppTypeCheckBoxList.ClearSelection();
            this.SuppGroupCheckBoxList.ClearSelection();
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer.Visible = true;

            string _suppGroupCode = "";
            string _suppTypeCode = "";

            var _hasilSuppGroup = this._suppBL.GetListSuppGroupForDDL();
            var _hasilSuppType = this._suppBL.GetListSuppTypeForDDL();

            for (var i = 0; i < _hasilSuppGroup.Count(); i++)
            {
                if (this.SuppGroupCheckBoxList.Items[i].Selected == true)
                {
                    if (_suppGroupCode == "")
                    {
                        _suppGroupCode += this.SuppGroupCheckBoxList.Items[i].Value;
                    }
                    else
                    {
                        _suppGroupCode += "," + this.SuppGroupCheckBoxList.Items[i].Value;
                    }
                }
            }

            for (var i = 0; i < _hasilSuppType.Count(); i++)
            {
                if (this.SuppTypeCheckBoxList.Items[i].Selected == true)
                {
                    if (_suppTypeCode == "")
                    {
                        _suppTypeCode += this.SuppTypeCheckBoxList.Items[i].Value;
                    }
                    else
                    {
                        _suppTypeCode += "," + this.SuppTypeCheckBoxList.Items[i].Value;
                    }
                }
            }

            ReportDataSource _reportDataSource1 = this._reportBL.SupplierList(_suppGroupCode, _suppTypeCode);

            this.ReportViewer.LocalReport.DataSources.Clear();
            this.ReportViewer.LocalReport.DataSources.Add(_reportDataSource1);

            this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;

            this.ReportViewer.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[3];
            _reportParam[0] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            _reportParam[1] = new ReportParameter("StrGroup", _suppGroupCode, true);
            _reportParam[2] = new ReportParameter("StrType", _suppTypeCode, true);

            this.ReportViewer.LocalReport.SetParameters(_reportParam);
            this.ReportViewer.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}