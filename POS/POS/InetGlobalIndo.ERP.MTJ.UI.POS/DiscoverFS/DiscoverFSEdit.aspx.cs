using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;
using System.Web.UI.WebControls;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.DiscoverFS
{
    public partial class DiscoverFSEdit : DiscoverFSBase
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

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;
                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.CodeTextBox.Attributes.Add("ReadOnly", "True");
                //this.VendorTextBox.Attributes.Add("ReadOnly", "True");
                this.PercentageTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.ClearLabel();
                this.ShowVendor();
                this.ShowData();
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

        public void ShowData()
        {
            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('-');
            POSMsShippingDF _posMsShippingDF = this._discoverFSBL.GetSingle(_tempSplit[0]);

            this.CodeTextBox.Text = _posMsShippingDF.DFSCode.ToString();
            this.NameTextBox.Text = _posMsShippingDF.DFSName.ToString();
            this.PercentageTextBox.Text = Convert.ToDecimal(_posMsShippingDF.Percentage).ToString("#0.00");
            if (_posMsShippingDF.FgActive == 'Y')
                this.FgActiveCheckBox.Checked = true;
            else
                this.FgActiveCheckBox.Checked = false;
            this.RemarkTextBox.Text = _posMsShippingDF.Remark.ToString();

            //this.VendorTextBox.Text = this._vendorBL.GetSingle(_posMsShippingDF.VendorCode).VendorName;
            this.VendorDropDownList.SelectedValue = _posMsShippingDF.VendorCode;
        }

        protected void CheckValidData()
        {
            String _errorMsg = "";
            this.ClearLabel();

            if (this.VendorDropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of Vendor.";

            this.WarningLabel.Text = _errorMsg.ToString();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('-');
                POSMsShippingDF _posMsShippingDF = this._discoverFSBL.GetSingle(_tempSplit[0]);
                _posMsShippingDF.DFSName = this.NameTextBox.Text;
                _posMsShippingDF.VendorCode = this.VendorDropDownList.SelectedValue;
                _posMsShippingDF.Percentage = Convert.ToDecimal(this.PercentageTextBox.Text);
                if (this.FgActiveCheckBox.Checked == true)
                    _posMsShippingDF.FgActive = 'Y';
                else
                    _posMsShippingDF.FgActive = 'N';
                _posMsShippingDF.Remark = this.RemarkTextBox.Text;
                _posMsShippingDF.ModifiedBy = HttpContext.Current.User.Identity.Name;
                _posMsShippingDF.ModifiedDate = DateTime.Now;

                bool _result = this._discoverFSBL.EditSubmit();

                if (_result == true)
                {
                    Response.Redirect(this._homePage);
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "You Failed Edit Data";
                }
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}