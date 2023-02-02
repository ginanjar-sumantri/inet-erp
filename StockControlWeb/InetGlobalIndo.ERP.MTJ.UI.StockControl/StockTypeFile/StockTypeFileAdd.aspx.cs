using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockTypeFile
{
    public partial class StockTypeFileAdd : StockTypeFileBase
    {
        private StockTypeBL _stockTypeBL = new StockTypeBL();
        private AccountBL _accountBL = new AccountBL();
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

            if (!this.Page.IsPostBack == true)
            {

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ClearData();
                this.ShowAccount();
                this.SetAttribute();
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

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            MsStockType _msStockType = new MsStockType();

            _msStockType.StockTypeCode = this.StockTypeCodeTextBox.Text;
            _msStockType.StockTypeName = this.StockTypeNameTextBox.Text;
            _msStockType.Account = this.AccountTextBox.Text;

            _msStockType.UserId = HttpContext.Current.User.Identity.Name;
            _msStockType.UserDate = DateTime.Now;

            bool _result = this._stockTypeBL.AddStockType(_msStockType);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_msStockType.StockTypeCode, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
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

        private void ClearData()
        {
            this.ClearLabel();
            this.StockTypeCodeTextBox.Text = "";
            this.StockTypeNameTextBox.Text = "";
            this.AccountTextBox.Text = "";
        }

        private void ShowAccount()
        {
            this.AccountDescDropDownList.Items.Clear();
            this.AccountDescDropDownList.DataTextField = "AccountName";
            this.AccountDescDropDownList.DataValueField = "Account";
            this.AccountDescDropDownList.DataSource = this._accountBL.GetListDDLAccForStockType();
            this.AccountDescDropDownList.DataBind();
            this.AccountDescDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }
    }
}