using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.Common;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.CustBillingAccountBase
{
    public partial class BILTrRadiusActivateTemporaryEdit : CustBillingAccountBase
    {
        private CustBillAccountBL _custBillAccountBL = new CustBillAccountBL();
        private CustomerBL _custBL = new CustomerBL();
        private PermissionBL _permBL = new PermissionBL();

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

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleRadiusTemporaryLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.UploadLabel.Text = "Images dimension : Width " + "1200" + " pixels x Height " + "1600" + " pixels, File size limit: " + ((2097152 / 1024) / 1024) + " MB";


                this.ShowCustDropdownlist();
                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
            }

        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.PeriodTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.PeriodTextBox.Attributes.Add("OnBlur", " ValidatePeriod(" + this.PeriodTextBox.ClientID + ");");

            this.YearTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.YearTextBox.Attributes.Add("OnBlur", " ValidateYear(" + this.YearTextBox.ClientID + ");");

            this.PeriodTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.YearTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
        }

        protected void ShowCustDropdownlist()
        {
            this.CustNameDropDownList.Items.Clear();
            this.CustNameDropDownList.DataSource = this._custBillAccountBL.GetListDDLCustBillAccount();
            this.CustNameDropDownList.DataValueField = "CustBillCode";
            this.CustNameDropDownList.DataTextField = "CustName";
            this.CustNameDropDownList.DataBind();
            this.CustNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", ""));
        }

        protected void ClearData()
        {
            this.CustNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.CustNameDropDownList.SelectedValue = "null";

            this.PeriodTextBox.Text = "";
            this.YearTextBox.Text = "";
            this.ReasonTextBox.Text = "";
        }

        protected void ShowData()
        {
            this.ClearLabel();

            BILTrRadiusActivateTemporary _bilTrRadiusActivateTemporary = this._custBillAccountBL.GetSingleBILTrRadiusActiveTemp(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            //BILTrRadiusActivateTemporary _bilTrRadiusActivateTemporary = this._custBillAccountBL.GetSingleBILTrRadiusActiveTemp("TRN/11DEC15/00000007");

            this.TransDateTextBox.Text = DateFormMapper.GetValue(_bilTrRadiusActivateTemporary.Transdate);
            this.TransNoTextBox.Text = _bilTrRadiusActivateTemporary.TransNmbr;
            this.FileNmbrTextBox.Text = _bilTrRadiusActivateTemporary.FileNmbr;
            this.ReasonTextBox.Text = _bilTrRadiusActivateTemporary.Reason;
            this.CustNameDropDownList.SelectedValue = _bilTrRadiusActivateTemporary.CustBillCode.ToString();
            this.YearTextBox.Text = _bilTrRadiusActivateTemporary.Year.ToString();
            this.PeriodTextBox.Text = _bilTrRadiusActivateTemporary.Period.ToString();

            if (_bilTrRadiusActivateTemporary.Status != null || _bilTrRadiusActivateTemporary.Status.ToString() != "")
                this.StatusLabel.Text = BilTrRadiusActivateTempDataMapper.GetStatusText((char)_bilTrRadiusActivateTemporary.Status);

            string _strImagePath = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + "/" + "imagesUpload/" + _bilTrRadiusActivateTemporary.AttachmentFile;

            this.PictureImage.Attributes.Add("src", "" + _strImagePath + "?t=");
            this.PictureImage.Attributes.Add("width", "115");
            this.PictureImage.Attributes.Add("height", "160");

        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            BILTrRadiusActivateTemporary _bilTrRadiusActivateTemporary = this._custBillAccountBL.GetSingleBILTrRadiusActiveTemp(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            string _custNameDDL = this.CustNameDropDownList.SelectedItem.ToString();
            string[] _custName = _custNameDDL.Split('-');

            _bilTrRadiusActivateTemporary.CustBillCode = new Guid(this.CustNameDropDownList.SelectedValue);
            _bilTrRadiusActivateTemporary.CustCode = _custName[1];
            _bilTrRadiusActivateTemporary.Period = (this.PeriodTextBox.Text == "") ? 0 : Convert.ToByte(this.PeriodTextBox.Text);
            _bilTrRadiusActivateTemporary.Year = (this.YearTextBox.Text == "") ? 0 : Convert.ToInt32(this.YearTextBox.Text);
            _bilTrRadiusActivateTemporary.Transdate = DateTime.Now;
            _bilTrRadiusActivateTemporary.Reason = this.ReasonTextBox.Text;

            _bilTrRadiusActivateTemporary.EditBy = HttpContext.Current.User.Identity.Name;
            _bilTrRadiusActivateTemporary.EditDate = DateTime.Now;

            String _imagepath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + @"\imagesUpload\";

            string _result = this._custBillAccountBL.EditBILTrRadiusActiveTemp(_bilTrRadiusActivateTemporary, this.FotoUpLoad, _imagepath);

            if (_result == "")
            {
                this.WarningLabel.Text = "Your Succcess Edit Data";
            }
            else
            {
                this.WarningLabel.Text = _result;
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePageTrRadiusTemporary);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

    }

}
