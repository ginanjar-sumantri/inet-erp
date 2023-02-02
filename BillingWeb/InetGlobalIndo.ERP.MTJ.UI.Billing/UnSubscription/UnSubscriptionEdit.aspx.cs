using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.UnSubscription
{
    public partial class UnSubscriptionEdit : UnSubscriptionBase
    {
        private CustomerBL _customerBL = new CustomerBL();
        private PermissionBL _permBL = new PermissionBL();
        private UnSubscriptionBL _unSubscriptionBL = new UnSubscriptionBL();
        private SalesConfirmationBL _salesConfirmationBL = new SalesConfirmationBL();

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

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/Save.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/Back.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ShowData();
            }
        }

        public void ShowData()
        {
            BILTrUnSubscriptionHd _bilTrUnSubscriptionHd = _unSubscriptionBL.GetSingleUnSub(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            MsCustomer _msCustomer = _customerBL.GetSingleCust(_bilTrUnSubscriptionHd.CustCode);

            this.TransNmbrTextBox.Text = _bilTrUnSubscriptionHd.TransNmbr;
            this.FileNmbrTextBox.Text = _bilTrUnSubscriptionHd.FileNmbr;
            this.DateTextBox.Text = Convert.ToDateTime(_bilTrUnSubscriptionHd.TransDate).ToString("yyyy-MM-dd");
            this.CustNameTextBox.Text = _msCustomer.CustName;
            this.RemarkTextBox.Text = _bilTrUnSubscriptionHd.Remark;
            this.StatusLable.Text = UnSubDataMapper.GetStatus(Convert.ToByte(_bilTrUnSubscriptionHd.Status)).ToString();
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            BILTrUnSubscriptionHd _bilTrUnSub = _unSubscriptionBL.GetSingleUnSub(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _bilTrUnSub.Remark = this.RemarkTextBox.Text;
            _bilTrUnSub.TransDate = DateFormMapper.GetValue(this.DateTextBox.Text);

            bool _result = _unSubscriptionBL.EditUnSub(_bilTrUnSub);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
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
                BILTrUnSubscriptionHd _bilTrUnSub = _unSubscriptionBL.GetSingleUnSub(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _bilTrUnSub.Remark = this.RemarkTextBox.Text;
                _bilTrUnSub.TransDate = DateFormMapper.GetValue(this.DateTextBox.Text);

                bool _result = _unSubscriptionBL.EditUnSub(_bilTrUnSub);

                if (_result == true)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.WarningLabel.Text = "Your Failed Edit Data";
                }
            }
        }
    }
}