using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Conversion
{
    public partial class ConvertionEdit : ConversionBase
    {
        private UnitBL _convBL = new UnitBL();
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
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

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
            this.RateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
        }

        public void ShowData()
        {
            string _tempUnitKey = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._unitKey), ApplicationConfig.EncryptionKey);
            string _tempCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            MsConvertion _msConvertion = this._convBL.GetSingleConvertion(_tempUnitKey, _tempCode);

            this.UnitCodeTextBox.Text = _convBL.GetUnitNameByCode(_msConvertion.UnitCode);
            this.UnitConvertTextBox.Text = _convBL.GetUnitNameByCode(_msConvertion.UnitConvert);
            this.RateTextBox.Text = _msConvertion.Rate.ToString("#,##0.##");
            if (_msConvertion.Rate < 1 && _msConvertion.Rate > 0)
            {
                this.RateTextBox.Text = "0" + this.RateTextBox.Text;
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsConvertion _msConvertion = this._convBL.GetSingleConvertion(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._unitKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msConvertion.Rate = Convert.ToDecimal(this.RateTextBox.Text);
            _msConvertion.UserID = HttpContext.Current.User.Identity.Name;
            _msConvertion.UserDate = DateTime.Now;

            bool _result = this._convBL.EditConvertion(_msConvertion);

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
            this.ClearLabel();
            this.ShowData();
        }
    }
}