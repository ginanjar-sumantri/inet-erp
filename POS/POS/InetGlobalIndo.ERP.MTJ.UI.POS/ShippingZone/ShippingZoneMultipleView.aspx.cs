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

namespace InetGlobalIndo.ERP.MTJ.UI.POS.ShippingZone
{
    public partial class ShippingZoneMultipleView : ShippingZoneBase
    {
        private ShippingBL _shippingBL = new ShippingBL();
        private PermissionBL _permBL = new PermissionBL();
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

                this.PageTitleLiteral.Text = this._pageTitleMultipleDetailLiteral;
                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.CodeTextBox.Attributes.Add("ReadOnly", "True");
                this.NameTextBox.Attributes.Add("ReadOnly", "True");
                this.ProductShapeTextBox.Attributes.Add("ReadOnly", "True");
                this.Weight1TextBox.Attributes.Add("ReadOnly", "True");
                this.Unit1TextBox.Attributes.Add("ReadOnly", "True");
                this.Weight2TextBox.Attributes.Add("ReadOnly", "True");
                this.Unit2TextBox.Attributes.Add("ReadOnly", "True");
                this.PriceTextBox.Attributes.Add("ReadOnly", "True");
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
            POSMsZone _posMsZone = this._shippingBL.GetSinglePOSMsZone(_tempSplit[0]);
            this.CodeTextBox.Text = _posMsZone.ZoneCode;
            this.NameTextBox.Text = _posMsZone.ZoneName;

            POSMsZoneMultiplePrice _posMsZoneMultiple = this._shippingBL.GetSinglePOSMsZoneMultiplePrice(_tempSplit[0], _tempSplit[1], Convert.ToDecimal(_tempSplit[2]), Convert.ToDecimal(_tempSplit[3]));

            String _productShapeType = "";
            if (_posMsZoneMultiple.ProductShape == "0")
                _productShapeType = "Document";
            else if (_posMsZoneMultiple.ProductShape == "1")
                _productShapeType = "Non Document";
            else if (_posMsZoneMultiple.ProductShape == "2")
                _productShapeType = "International Priority";

            this.ProductShapeTextBox.Text = _productShapeType;
            this.Weight1TextBox.Text = Convert.ToDecimal(_posMsZoneMultiple.Weight1).ToString("#,#");
            this.Unit1TextBox.Text = this._unitBL.GetUnitNameByCode(_posMsZoneMultiple.Unit1);
            this.Weight2TextBox.Text = Convert.ToDecimal(_posMsZoneMultiple.Weight2).ToString("#,#");
            this.Unit2TextBox.Text = this._unitBL.GetUnitNameByCode(_posMsZoneMultiple.Unit2);
            this.PriceTextBox.Text = Convert.ToDecimal(_posMsZoneMultiple.Price).ToString("#,#");
            if (_posMsZoneMultiple.FgActive == 'Y')
                this.FgActiveCheckBox.Checked = true;
            this.RemarkTextBox.Text = _posMsZoneMultiple.Remark;
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editMultipleDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}