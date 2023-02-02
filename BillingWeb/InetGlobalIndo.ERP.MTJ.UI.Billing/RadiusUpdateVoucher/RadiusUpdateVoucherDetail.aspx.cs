using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.RadiusUpdateVoucher
{
    public partial class RadiusUpdateVoucherDetail : RadiusUpdateVoucherBase
    {
        private RadiusUpdateVoucherBL _radiusUpdateVoucherBL = new RadiusUpdateVoucherBL();
        private PermissionBL _permBL = new PermissionBL();
        private RadiusBL _radiusBL = new RadiusBL();

        private string _currPageKey = "CurrentPage";

        private string _imgGetApproval = "get_approval.jpg";
        private string _imgApprove = "approve.jpg";
        private string _imgPosting = "posting.jpg";
        private string _imgUnposting = "unposting.jpg";
        private string _imgPreview = "preview.jpg";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.ViewState[this._currPageKey] = 0;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";

                this.SetAttribute();
                this.SetButtonPermission();

                this.ClearLabel();
                this.ShowData(0);
            }
        }

        private void SetAttribute()
        {
            this.TransNoTextBox.Attributes.Add("ReadOnly", "True");
            this.FileNmbrTextBox.Attributes.Add("ReadOnly", "True");
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.RadiusTextBox.Attributes.Add("ReadOnly", "True");
            this.ExpiredDateTextBox.Attributes.Add("ReadOnly", "True");
            this.SeriesTextBox.Attributes.Add("ReadOnly", "True");
            this.SeriesNoFromTextBox.Attributes.Add("ReadOnly", "True");
            this.SeriesNoToTextBox.Attributes.Add("ReadOnly", "True");
            this.SellingAmountTextBox.Attributes.Add("ReadOnly", "True");
            this.AssociatedServiceTextBox.Attributes.Add("ReadOnly", "True");
            this.ExpireTimeTextBox.Attributes.Add("ReadOnly", "True");
            this.ExpireTimeUnitTextBox.Attributes.Add("ReadOnly", "True");
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ShowData(Int32 _prmCurrentPage)
        {
            _radiusUpdateVoucherBL = new RadiusUpdateVoucherBL();

            BILTrRadiusUpdateVoucher _bilTrRadiusUpdateVoucher = this._radiusUpdateVoucherBL.GetSingleRadiusUpdateVoucher(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNoTextBox.Text = _bilTrRadiusUpdateVoucher.TransNmbr;
            this.FileNmbrTextBox.Text = _bilTrRadiusUpdateVoucher.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_bilTrRadiusUpdateVoucher.TransDate);
            this.RadiusTextBox.Text = this._radiusBL.GetRadiusNameByCode(_bilTrRadiusUpdateVoucher.RadiusCode);
            this.ExpiredDateTextBox.Text = DateFormMapper.GetValue(_bilTrRadiusUpdateVoucher.ExpiredDate);
            this.SeriesTextBox.Text = _bilTrRadiusUpdateVoucher.Series;
            this.SeriesNoFromTextBox.Text = _bilTrRadiusUpdateVoucher.SerialNoFrom;
            this.SeriesNoToTextBox.Text = _bilTrRadiusUpdateVoucher.SerialNoTo;
            this.AssociatedServiceTextBox.Text = _bilTrRadiusUpdateVoucher.AssociatedService;
            int _expireTime = (_bilTrRadiusUpdateVoucher.ExpireTime == null) ? 0 : Convert.ToInt32(_bilTrRadiusUpdateVoucher.ExpireTime);
            this.ExpireTimeTextBox.Text = _expireTime.ToString("#,##0");
            decimal _sellamount = (_bilTrRadiusUpdateVoucher.SellingAmount == null) ? 0 : Convert.ToDecimal(_bilTrRadiusUpdateVoucher.SellingAmount);
            this.SellingAmountTextBox.Text = _sellamount.ToString("#,##0");
            this.ExpireTimeUnitTextBox.Text = RadiusUpdateVoucherDataMapper.GetExpiredTimeUnitText(Convert.ToInt32(_bilTrRadiusUpdateVoucher.ExpireTimeUnit));

            this.StatusLabel.Text = RadiusUpdateVoucherDataMapper.GetStatusText(_bilTrRadiusUpdateVoucher.Status);
            this.StatusHiddenField.Value = _bilTrRadiusUpdateVoucher.Status.ToString();

            if (this.StatusHiddenField.Value.Trim().ToLower() == RadiusUpdateVoucherDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.EditButton.Visible = false;
            }
            else
            {
                this.EditButton.Visible = true;
            }

            this.Panel1.Visible = true;

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.ShowActionButton();
        }

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == RadiusUpdateVoucherDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;

                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == RadiusUpdateVoucherDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == RadiusUpdateVoucherDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == RadiusUpdateVoucherDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                //this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgUnposting;

                //this._permUnposting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting);

                //if (this._permUnposting == PermissionLevel.NoAccess)
                //{
                this.ActionButton.Visible = false;
                //}
            }
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ActionButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();

            if (this.StatusHiddenField.Value.Trim().ToLower() == RadiusUpdateVoucherDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                string _result = this._radiusUpdateVoucherBL.GetApproval(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == RadiusUpdateVoucherDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                string _result = this._radiusUpdateVoucherBL.Approve(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == RadiusUpdateVoucherDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                string _result = this._radiusUpdateVoucherBL.Posting(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == RadiusUpdateVoucherDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                string _result = this._radiusUpdateVoucherBL.UnPosting(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result;
            }

            this.ShowData(0);
        }
    }
}