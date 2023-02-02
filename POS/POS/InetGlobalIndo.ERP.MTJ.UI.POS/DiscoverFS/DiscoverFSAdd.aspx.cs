using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;
using System.Web.UI.WebControls;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.DiscoverFS
{
    public partial class DiscoverFSAdd : DiscoverFSBase
    {
        private DiscoverFSBL _discoverFSBL = new DiscoverFSBL();
        private PermissionBL _permBL = new PermissionBL();
        private VendorBL _vendorBL = new VendorBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.PercentageTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.ShowVendor();
                this.ClearData();
            }
        }

        protected void ShowVendor()
        {
            this.VendorDropDownList.Items.Clear();
            this.VendorDropDownList.DataTextField = "VendorName";
            this.VendorDropDownList.DataValueField = "VendorCode";
            this.VendorDropDownList.DataSource = this._vendorBL.GetList(0, 1000, "", "");
            this.VendorDropDownList.DataBind();
            this.VendorDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.ClearLabel();

            this.CodeTextBox.Text = "";
            this.NameTextBox.Text = "";
            this.VendorDropDownList.SelectedIndex = 0;
            this.PercentageTextBox.Text = "0";
            this.FgActiveCheckBox.Checked = false;
            this.RemarkTextBox.Text = "";
        }

        protected void CheckValidData()
        {
            String _errorMsg = "";
            this.ClearLabel();

            if (this.VendorDropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of Vendor.";

            POSMsShippingDF _posMsShippingDF = this._discoverFSBL.GetSingle(this.CodeTextBox.Text);
            if (_posMsShippingDF != null)
                _errorMsg = _errorMsg + " Code = " + this.CodeTextBox.Text + " Already Exist.";

            this.WarningLabel.Text = _errorMsg.ToString();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                POSMsShippingDF _posMsShippingDF = new POSMsShippingDF();

                _posMsShippingDF.DFSCode = this.CodeTextBox.Text;
                _posMsShippingDF.DFSName = this.NameTextBox.Text;
                _posMsShippingDF.VendorCode = this.VendorDropDownList.SelectedValue;
                _posMsShippingDF.Percentage = Convert.ToDecimal(this.PercentageTextBox.Text);
                if (this.FgActiveCheckBox.Checked == true)
                    _posMsShippingDF.FgActive = 'Y';
                else
                    _posMsShippingDF.FgActive = 'N';
                _posMsShippingDF.Remark = this.RemarkTextBox.Text;
                _posMsShippingDF.CreatedBy = HttpContext.Current.User.Identity.Name;
                _posMsShippingDF.CreatedDate = DateTime.Now;
                _posMsShippingDF.ModifiedBy = "";
                _posMsShippingDF.ModifiedDate = this._defaultdate;

                bool _result = this._discoverFSBL.Add(_posMsShippingDF);

                if (_result == true)
                {
                    Response.Redirect(this._homePage);
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "You Failed Add Data";
                }
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}