using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockTypeFile
{
    public partial class StockTypeFileEdit : StockTypeFileBase
    {
        private StockTypeBL _stockTypeBL = new StockTypeBL();
        private AccountBL _accountBl = new AccountBL();
        private PermissionBL _permBL = new PermissionBL();

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

                this.SetAttribute();
                this.ClearLabel();
                this.ShowAccount();
                this.ShowData();

            }
        }

        protected void SetAttribute()
        {
            this.AccountDescDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountDescDropDownList.ClientID + "," + this.AccountTextBox.ClientID + ");");
            this.AccountTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountDescDropDownList.ClientID + "," + this.AccountTextBox.ClientID + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected bool simpen()
        {
            MsStockType _msStockType = this._stockTypeBL.GetSingleStockType(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msStockType.StockTypeCode = this.StockTypeCodeTextBox.Text;
            _msStockType.StockTypeName = this.StockTypeNameTextBox.Text;
            _msStockType.Account = this.AccountTextBox.Text;
            _msStockType.UserId = HttpContext.Current.User.Identity.Name;
            _msStockType.UserDate = DateTime.Now;

            bool _result = this._stockTypeBL.Edit(_msStockType);
            return _result;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            //MsStockType _msStockType = this._stockTypeBL.GetSingleStockType(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            //_msStockType.StockTypeCode = this.StockTypeCodeTextBox.Text;
            //_msStockType.StockTypeName = this.StockTypeNameTextBox.Text;
            //_msStockType.Account = this.AccountTextBox.Text;
            //_msStockType.UserId = HttpContext.Current.User.Identity.Name;
            //_msStockType.UserDate = DateTime.Now;

            bool _result = simpen();//this._stockTypeBL.EditStockType(_msStockType);
            if (_result == true)
            {
                Response.Redirect(this._homePage);
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
            this.ShowData();
        }
        private void ShowData()
        {
            MsStockType _msStockType = this._stockTypeBL.GetSingleStockType(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.StockTypeCodeTextBox.Text = _msStockType.StockTypeCode;
            this.AccountDescDropDownList.SelectedValue = _msStockType.Account;
            this.StockTypeNameTextBox.Text = _msStockType.StockTypeName;
            this.AccountTextBox.Text = _msStockType.Account;

        }

        private void ShowAccount()
        {
            this.AccountDescDropDownList.Items.Clear();
            this.AccountDescDropDownList.DataTextField = "AccountName";
            this.AccountDescDropDownList.DataValueField = "Account";
            this.AccountDescDropDownList.DataSource = this._accountBl.GetListDDLAccForStockType();
            this.AccountDescDropDownList.DataBind();
            this.AccountDescDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }
        protected void ViewDetailButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.StockTypeCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
        }
        protected void SaveAndViewDetailButton_Click(object sender, EventArgs e)
        {
            bool _result = simpen();
            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.StockTypeCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }
    }
}