using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseRequest
{
    public partial class PurchaseRequestEdit : PurchaseRequestBase
    {
        private PurchaseRequestBL _purchaseRequestBL = new PurchaseRequestBL();
        private OrganizationStructureBL _organizationStructureBL = new OrganizationStructureBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();

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
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.ShowCurrency();
                this.ShowDepartment();

                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            PRCRequestHd _prcRequestHd = this._purchaseRequestBL.GetSinglePRCRequestHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransactionNoTextBox.Text = _prcRequestHd.TransNmbr;
            this.FileNmbrTextBox.Text = _prcRequestHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_prcRequestHd.TransDate);
            this.OrgUnitDDL.SelectedValue = _prcRequestHd.OrgUnit;
            this.CurrCodeDropDownList.SelectedValue = _prcRequestHd.CurrCode;
            this.RequestByTextBox.Text = _prcRequestHd.RequestBy;
            this.RemarkTextBox.Text = _prcRequestHd.Remark;
        }

        public void ShowDepartment()
        {
            this.OrgUnitDDL.Items.Clear();
            this.OrgUnitDDL.DataTextField = "Description";
            this.OrgUnitDDL.DataValueField = "OrgUnit";
            this.OrgUnitDDL.DataSource = this._organizationStructureBL.GetListOrganizationUnitForDDL();
            this.OrgUnitDDL.DataBind();
            this.OrgUnitDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCurrency()
        {
            this.CurrCodeDropDownList.Items.Clear();
            this.CurrCodeDropDownList.DataTextField = "CurrCode";
            this.CurrCodeDropDownList.DataValueField = "CurrCode";
            this.CurrCodeDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrCodeDropDownList.DataBind();
            this.CurrCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void SetAttribute()
        {
            this.TransactionNoTextBox.Attributes.Add("ReadOnly", "True");
            this.DateTextBox.Attributes.Add("ReadOnly", "True");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
            //this.ViewDetailButton.Attributes.Add("OnClick", "return AskYouFirstToSave(" + this.CheckHidden.ClientID + ");");
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            PRCRequestHd _prcRequestHd = this._purchaseRequestBL.GetSinglePRCRequestHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _prcRequestHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _prcRequestHd.OrgUnit = this.OrgUnitDDL.SelectedValue;
            _prcRequestHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _prcRequestHd.RequestBy = this.RequestByTextBox.Text;
            _prcRequestHd.Remark = this.RemarkTextBox.Text;

            _prcRequestHd.EditBy = HttpContext.Current.User.Identity.Name;
            _prcRequestHd.EditDate = DateTime.Now;

            bool _result = this._purchaseRequestBL.EditPRCRequestHd(_prcRequestHd);

            if (_result == true)
            {
                Response.Redirect(_homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {

            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));

        }
        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.Page.IsValid == true)
            {
                PRCRequestHd _prcRequestHd = this._purchaseRequestBL.GetSinglePRCRequestHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _prcRequestHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                _prcRequestHd.OrgUnit = this.OrgUnitDDL.SelectedValue;
                _prcRequestHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
                _prcRequestHd.RequestBy = this.RequestByTextBox.Text;
                _prcRequestHd.Remark = this.RemarkTextBox.Text;

                _prcRequestHd.EditBy = HttpContext.Current.User.Identity.Name;
                _prcRequestHd.EditDate = DateTime.Now;

                bool _result = this._purchaseRequestBL.EditPRCRequestHd(_prcRequestHd);

                if (_result == true)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "Your Failed Edit Data";
                }
            }
        }
    }
}