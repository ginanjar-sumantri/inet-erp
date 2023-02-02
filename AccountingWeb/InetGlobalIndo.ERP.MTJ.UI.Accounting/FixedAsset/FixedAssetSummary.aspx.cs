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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FixedAsset
{
    public partial class FixedAssetSummary : FixedAssetBase
    {
        private FixedAssetsBL _fixedAssetBL = new FixedAssetsBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.ShowData();
            }
        }

        private void ShowData()
        {
            decimal _tempGrandTotal = this._fixedAssetBL.GetGrandTotalFixedAsset();
            this.GrandTotalLabel.Text = (_tempGrandTotal == 0) ? "0" : _tempGrandTotal.ToString("#,###.##");
        }
    }
}