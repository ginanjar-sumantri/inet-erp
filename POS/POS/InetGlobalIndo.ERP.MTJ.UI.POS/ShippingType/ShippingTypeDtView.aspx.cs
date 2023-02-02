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
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.ShippingType
{
    public partial class ShippingTypeDtView : ShippingTypeBase
    {
        private ShippingBL _shippingBL = new ShippingBL();
        private PermissionBL _permBL = new PermissionBL();
        private CityBL _cityBL = new CityBL();
        private UnitBL _unitBL = new UnitBL();
        private CurrencyBL _currencyBL = new CurrencyBL();

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private string _currPageKey = "CurrentPage";

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;
        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

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
                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.CodeTextBox.Attributes.Add("ReadOnly", "True");
                this.NameTextBox.Attributes.Add("ReadOnly", "True");
                this.CityTextBox.Attributes.Add("ReadOnly", "True");
                this.ProductShapeTextBox.Attributes.Add("ReadOnly", "True");
                this.Price1TextBox.Attributes.Add("ReadOnly", "True");
                this.Price2TextBox.Attributes.Add("ReadOnly", "True");
                this.UnitTextBox.Attributes.Add("ReadOnly", "True");
                this.EstimationTimeTextBox.Attributes.Add("ReadOnly", "True");
                this.FgActiveTextBox.Attributes.Add("ReadOnly", "True");
                this.RemarkTextBox.Attributes.Add("ReadOnly", "True");
                this.SetButtonPermission();
                this.ShowData();

            }
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        public void ShowData()
        {
            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|');
            POSMsShippingType _posMsShippingType = this._shippingBL.GetSinglePOSMsShippingType(_tempSplit[0]);
            this.CodeTextBox.Text = _posMsShippingType.ShippingTypeCode;
            this.NameTextBox.Text = _posMsShippingType.ShippingTypeName;

            POSMsShippingTypeDt _posMsShippingTypeDt = this._shippingBL.GetSinglePOSMsShippingTypeDt(_tempSplit[0], _tempSplit[1], _tempSplit[2]);
            this.CityTextBox.Text = this._cityBL.GetCityNameByCode(_posMsShippingTypeDt.CityCode);
            String _productShape = "";
            if (_posMsShippingTypeDt.ProductShape == "0")
                _productShape = "Document";
            else
                _productShape = "Non Document";
            this.ProductShapeTextBox.Text = _productShape;

            this.CurrencyTextBox.Text = this._currencyBL.GetCurrencyName(_posMsShippingTypeDt.CurrCode);
            this.Price1TextBox.Text = Convert.ToDecimal(_posMsShippingTypeDt.Price1).ToString("#0.00");
            this.Price2TextBox.Text = Convert.ToDecimal(_posMsShippingTypeDt.Price2).ToString("#0.00");
            this.UnitTextBox.Text = this._unitBL.GetUnitNameByCode(_posMsShippingTypeDt.UnitCode);
            this.EstimationTimeTextBox.Text = _posMsShippingTypeDt.EstimationTime;
            this.FgActiveTextBox.Text = _posMsShippingTypeDt.FgActive.ToString();
            this.RemarkTextBox.Text = _posMsShippingTypeDt.Remark;
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}