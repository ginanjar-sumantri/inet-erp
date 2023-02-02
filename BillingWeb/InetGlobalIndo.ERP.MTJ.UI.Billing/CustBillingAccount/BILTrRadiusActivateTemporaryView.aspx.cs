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
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.CustBillingAccountBase
{
    public partial class BILTrRadiusActivateTemporaryView : CustBillingAccountBase
    {
        private CompanyConfig _companyConfig = new CompanyConfig();
        private CustBillAccountBL _custBillAccountBL = new CustBillAccountBL();
        private CustomerBL _custBL = new CustomerBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _imgGetApproval = "get_approval.jpg";
        private string _imgApprove = "approve.jpg";
        private string _imgPosting = "posting.jpg";
        //private string _imgUnposting = "unposting.jpg";

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

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";

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
            this.CustNameDropDownList.DataSource = this._custBL.GetListCustForDDLForReport();
            this.CustNameDropDownList.DataValueField = "CustCode";
            this.CustNameDropDownList.DataTextField = "CustName";
            this.CustNameDropDownList.DataBind();
            this.CustNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", ""));
        }

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == BilTrRadiusActivateTempDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;

                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == BilTrRadiusActivateTempDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == BilTrRadiusActivateTempDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == BilTrRadiusActivateTempDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.ActionButton.Visible = false;
            }

        }

        protected void ShowData()
        {
            this.ClearLabel();

            //BILTrRadiusActivateTemporary _bilTrRadiusActivateTemporary = this._custBillAccountBL.GetSingleBILTrRadiusActiveTemp(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            BILTrRadiusActivateTemporary _bilTrRadiusActivateTemporary = this._custBillAccountBL.GetSingleBILTrRadiusActiveTemp(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransDateTextBox.Text = DateFormMapper.GetValue(_bilTrRadiusActivateTemporary.Transdate);
            this.TransNoTextBox.Text = _bilTrRadiusActivateTemporary.TransNmbr;
            this.FileNmbrTextBox.Text = _bilTrRadiusActivateTemporary.FileNmbr;
            this.ReasonTextBox.Text = _bilTrRadiusActivateTemporary.Reason;
            this.CustNameDropDownList.SelectedValue = _bilTrRadiusActivateTemporary.CustCode;
            this.YearTextBox.Text = _bilTrRadiusActivateTemporary.Year.ToString();
            this.PeriodTextBox.Text = _bilTrRadiusActivateTemporary.Period.ToString();
            if (_bilTrRadiusActivateTemporary.Status != null || _bilTrRadiusActivateTemporary.Status.ToString() != "")
                this.StatusLabel.Text = BilTrRadiusActivateTempDataMapper.GetStatusText((char)_bilTrRadiusActivateTemporary.Status);

            this.StatusHiddenField.Value = _bilTrRadiusActivateTemporary.Status.ToString();

            string _strImagePath = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + "/" + "imagesUpload/" + _bilTrRadiusActivateTemporary.AttachmentFile;

            this.PictureImage.Attributes.Add("src", "" + _strImagePath + "?t=");
            this.PictureImage.Attributes.Add("width", "115");
            this.PictureImage.Attributes.Add("height", "160");

            this.ShowActionButton();
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPageTrRadiusTemporary + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey)));
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePageTrRadiusTemporary);
        }

        protected void ActionButton_Click(object sender, ImageClickEventArgs e)
        {
            DateTime _transdate = DateFormMapper.GetValue(this.TransDateTextBox.Text);
            int _year = _transdate.Year;
            int _period = _transdate.Month;

            this.ClearLabel();

            if (this.StatusHiddenField.Value.Trim().ToLower() == BilTrRadiusActivateTempDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                BILTrRadiusActivateTemporary _bilTrRadiusActivateTemporary = this._custBillAccountBL.GetSingleBILTrRadiusActiveTemp(this.TransNoTextBox.Text);
                _bilTrRadiusActivateTemporary.Status = BilTrRadiusActivateTempDataMapper.GetStatus(TransStatus.WaitingForApproval);

                bool _result = this._custBillAccountBL.GetAppr(_bilTrRadiusActivateTemporary);

                if (_result == true)
                {
                    this.WarningLabel.Text = "Get Approved  Success";
                }
                else
                {
                    this.WarningLabel.Text = "Get Approval Failed";

                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == BilTrRadiusActivateTempDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                BILTrRadiusActivateTemporary _bilTrRadiusActivateTemporary = this._custBillAccountBL.GetSingleBILTrRadiusActiveTemp(this.TransNoTextBox.Text);

                _bilTrRadiusActivateTemporary.Status = BilTrRadiusActivateTempDataMapper.GetStatus(TransStatus.Approved);
                bool _result = this._custBillAccountBL.Approve(_bilTrRadiusActivateTemporary);

                if (_result == true)
                {
                    this.WarningLabel.Text = "Approved  Success";
                }
                else
                {
                    this.WarningLabel.Text = "Approval Failed";

                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == BilTrRadiusActivateTempDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {

                string _date = "";
                string _updateRadiusExpired = "";
                int _yearRadius = 0;
                int _mountRadius = 0;

                //UPDATE Status IN BILTrRadiusActivateTemporary
                BILTrRadiusActivateTemporary _bilTrRadiusActivateTemporary = this._custBillAccountBL.GetSingleBILTrRadiusActiveTemp(this.TransNoTextBox.Text);
                _bilTrRadiusActivateTemporary.Status = BilTrRadiusActivateTempDataMapper.GetStatus(TransStatus.Posted);

                //UPDATE RadiusExpiredDate IN Master_CustBillAccount
                Master_CustBillAccount _msCustBillAccount = this._custBillAccountBL.GetSingleCustBillAccount(_bilTrRadiusActivateTemporary.CustBillCode);
                MsCustomer _msCustomer = this._custBL.GetSingleCust(_msCustBillAccount.CustCode);
                String _expiredDate = DateFormMapper.GetValue(_msCustBillAccount.RadiusExpiredDate);
                String[] _newExpiredDate = _expiredDate.Split('-');
                CompanyConfiguration _companyConfiguration = this._companyConfig.GetSingle("BillingRadiusToleranceDay");
                _date = (_newExpiredDate[2] + "-" + _newExpiredDate[1] + "-" + (Convert.ToInt32(_newExpiredDate[0]) + (Convert.ToInt32(_companyConfiguration.SetValue))));

                string[] _date2 = _date.Split('-');
                if (Convert.ToInt32(_date2[1]) > 12)
                {
                    _yearRadius = Convert.ToInt32(_date2[0]) + 1;
                    _mountRadius = Convert.ToInt32(_date2[1]) - 12;
                }
                if (_yearRadius == 0)
                {
                    _updateRadiusExpired = _date;
                }
                else
                {
                    _updateRadiusExpired = _yearRadius + "-" + _mountRadius + "-" + _msCustomer.DueDateCycle.ToString();
                }

                bool _result = this._custBillAccountBL.Posting(_bilTrRadiusActivateTemporary, _msCustBillAccount, Convert.ToDateTime(_updateRadiusExpired));

                if (_result == true)
                {
                    this.WarningLabel.Text = "Posted Success";
                }
                else
                {
                    this.WarningLabel.Text = "Posted Failed";

                }
            }

            this.ShowData();
        }

    }

}
