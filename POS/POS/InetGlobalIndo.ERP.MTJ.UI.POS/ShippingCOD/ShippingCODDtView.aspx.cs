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

namespace InetGlobalIndo.ERP.MTJ.UI.POS.ShippingCOD
{
    public partial class ShippingCODDtView : ShippingCODBase
    {
        private VendorBL _vendorBL = new VendorBL();
        private PermissionBL _permBL = new PermissionBL();
        private ShippingBL _shippingBL = new ShippingBL();
        private UnitBL _unitBL = new UnitBL();
        private TransportationRSBL _transportationRSBL = new TransportationRSBL();
        private OtherSurchargeBL _otherSurchargeBL = new OtherSurchargeBL();

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
                //this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.SetAttribute();
                //this.SetButtonPermission();
                this.ShowData();
            }
        }

        private void SetAttribute()
        {
            this.TransNmbrTextBox.Attributes.Add("ReadOnly", "True");
            this.AirwayBillTextBox.Attributes.Add("ReadOnly", "True");
            this.VendorTextBox.Attributes.Add("ReadOnly", "True");
            this.ShippingTypeTextBox.Attributes.Add("ReadOnly", "True");
            this.ProductShapeTextBox.Attributes.Add("ReadOnly", "True");
            this.WeightTextBox.Attributes.Add("ReadOnly", "True");
            this.UnitTextBox.Attributes.Add("ReadOnly", "True");
            this.Price1TextBox.Attributes.Add("ReadOnly", "True");
            this.Price2TextBox.Attributes.Add("ReadOnly", "True");
            this.AmountForexTextBox.Attributes.Add("ReadOnly", "True");
            this.DiscPercentageTextBox.Attributes.Add("ReadOnly", "True");
            this.DiscForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TaxTextBox.Attributes.Add("ReadOnly", "True");
            this.PackageAmountTextBox.Attributes.Add("ReadOnly", "True");
            this.InsuranceAmountTextBox.Attributes.Add("ReadOnly", "True");
            this.TRSCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.TRSTextBox.Attributes.Add("ReadOnly", "True");
            this.TRSCode2TextBox.Attributes.Add("ReadOnly", "True");
            this.TRS2TextBox.Attributes.Add("ReadOnly", "True");
            this.TRSCode3TextBox.Attributes.Add("ReadOnly", "True");
            this.TRS3TextBox.Attributes.Add("ReadOnly", "True");
            this.OtherSurchargeCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.OtherSurchargeTextBox.Attributes.Add("ReadOnly", "True");
            this.OtherSurchargeCode2TextBox.Attributes.Add("ReadOnly", "True");
            this.OtherSurcharge2TextBox.Attributes.Add("ReadOnly", "True");
            this.OtherSurchargeCode3TextBox.Attributes.Add("ReadOnly", "True");
            this.OtherSurcharge3TextBox.Attributes.Add("ReadOnly", "True");
            this.DFSValueTextBox.Attributes.Add("ReadOnly", "True");
            this.OtherFeeTextBox.Attributes.Add("ReadOnly", "True");
            this.LineTotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("ReadOnly", "True");
        }

        //private void SetButtonPermission()
        //{
        //    this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

        //    if (this._permEdit == PermissionLevel.NoAccess)
        //    {
        //        this.EditButton.Visible = false;
        //    }
        //}

        public void ShowData()
        {
            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|');
            POSTrShippingDt _posTrShippingDt = this._shippingBL.GetSinglePOSTrShippingDt(_tempSplit[0], Convert.ToInt32(_tempSplit[1]));

            this.TransNmbrTextBox.Text = _posTrShippingDt.TransNmbr;
            this.AirwayBillTextBox.Text = (_posTrShippingDt.AirwayBill == null) ? "" : _posTrShippingDt.AirwayBill;

            POSMsShippingVendor _posMsShippingVendor = this._vendorBL.GetSingle(_posTrShippingDt.VendorCode);
            this.VendorTextBox.Text = _posMsShippingVendor.VendorName;

            String _shippingZoneTypeName = "";
            if (_posMsShippingVendor.FgZone != 'Y')
            {
                _shippingZoneTypeName = this._shippingBL.GetSinglePOSMsShippingType(_posTrShippingDt.ShippingTypeCode).ShippingTypeName;
            }
            else
            {
                _shippingZoneTypeName = this._shippingBL.GetSinglePOSMsZone(_posTrShippingDt.ShippingTypeCode).ZoneName;
            }
            this.ShippingTypeTextBox.Text = _shippingZoneTypeName;

            string _productshape = "";
            if (_posTrShippingDt.ProductShape == "0")
                _productshape = "Document";
            else if (_posTrShippingDt.ProductShape == "1")
                _productshape = "Non Document";
            else
                _productshape = "International Priority";
            this.ProductShapeTextBox.Text = _productshape;

            this.WeightTextBox.Text = Convert.ToDecimal(_posTrShippingDt.Weight).ToString("#,#0.##");
            this.UnitTextBox.Text = this._unitBL.GetUnitNameByCode(_posTrShippingDt.Unit);
            this.Price1TextBox.Text = Convert.ToDecimal(_posTrShippingDt.Price1).ToString("#,#0.##");
            this.Price2TextBox.Text = Convert.ToDecimal(_posTrShippingDt.Price2).ToString("#,#0.##");
            this.AmountForexTextBox.Text = Convert.ToDecimal(_posTrShippingDt.AmountForex).ToString("#,#0.##");
            this.DiscPercentageTextBox.Text = Convert.ToDecimal(_posTrShippingDt.DiscPercentage).ToString("#,#0.##") + " %";
            this.DiscForexTextBox.Text = Convert.ToDecimal(_posTrShippingDt.DiscForex).ToString("#,#0.##");
            this.TaxTextBox.Text = Convert.ToDecimal(_posTrShippingDt.Tax).ToString("#,#0.##");
            this.PackageAmountTextBox.Text = Convert.ToDecimal(_posTrShippingDt.PackageAmount).ToString("#,#0.##");
            this.InsuranceAmountTextBox.Text = Convert.ToDecimal(_posTrShippingDt.InsuranceAmount).ToString("#,#0.##");
            this.TRSCodeTextBox.Text = (_posTrShippingDt.TRSCode == null | _posTrShippingDt.TRSCode == "null") ? "" : this._transportationRSBL.GetSingle(_posTrShippingDt.TRSCode).TRSName;
            this.TRSTextBox.Text = (_posTrShippingDt.TRS == null) ? "0" : Convert.ToDecimal(_posTrShippingDt.TRS).ToString("#,#0.##");
            this.TRSCode2TextBox.Text = (_posTrShippingDt.TRSCode2 == null | _posTrShippingDt.TRSCode2 == "null") ? "" : this._transportationRSBL.GetSingle(_posTrShippingDt.TRSCode2).TRSName; ;
            this.TRS2TextBox.Text = (_posTrShippingDt.TRS2 == null) ? "0" : Convert.ToDecimal(_posTrShippingDt.TRS2).ToString("#,#0.##");
            this.TRSCode3TextBox.Text = (_posTrShippingDt.TRSCode3 == null | _posTrShippingDt.TRSCode3 == "null") ? "" : this._transportationRSBL.GetSingle(_posTrShippingDt.TRSCode3).TRSName;
            this.TRS3TextBox.Text = (_posTrShippingDt.TRS3 == null) ? "0" : Convert.ToDecimal(_posTrShippingDt.TRS3).ToString("#,#0.##");
            this.OtherSurchargeCodeTextBox.Text = (_posTrShippingDt.OtherSurchargeCode == null | _posTrShippingDt.OtherSurchargeCode == "null") ? "" : this._otherSurchargeBL.GetSingle(_posTrShippingDt.OtherSurchargeCode).OtherSurchargeName;
            this.OtherSurchargeTextBox.Text = (_posTrShippingDt.OtherSurcharge == null) ? "0" : Convert.ToDecimal(_posTrShippingDt.OtherSurcharge).ToString("#,#0.##");
            this.OtherSurchargeCodeTextBox.Text = (_posTrShippingDt.OtherSurchargeCode2 == null | _posTrShippingDt.OtherSurchargeCode2 == "null") ? "" : this._otherSurchargeBL.GetSingle(_posTrShippingDt.OtherSurchargeCode2).OtherSurchargeName;
            this.OtherSurcharge2TextBox.Text = (_posTrShippingDt.OtherSurcharge2 == null) ? "0" : Convert.ToDecimal(_posTrShippingDt.OtherSurcharge2).ToString("#,#0.##");
            this.OtherSurchargeCodeTextBox.Text = (_posTrShippingDt.OtherSurchargeCode3 == null | _posTrShippingDt.OtherSurchargeCode3 == "null") ? "" : this._otherSurchargeBL.GetSingle(_posTrShippingDt.OtherSurchargeCode3).OtherSurchargeName;
            this.OtherSurcharge3TextBox.Text = (_posTrShippingDt.OtherSurcharge3 == null) ? "0" : Convert.ToDecimal(_posTrShippingDt.OtherSurcharge3).ToString("#,#0.##");
            this.DFSValueTextBox.Text = Convert.ToDecimal(_posTrShippingDt.DFSValue).ToString("#,#0.##");
            this.OtherFeeTextBox.Text = Convert.ToDecimal(_posTrShippingDt.OtherFee).ToString("#,#0.##");
            this.LineTotalForexTextBox.Text = Convert.ToDecimal(_posTrShippingDt.LineTotalForex).ToString("#,#0.##");
            this.RemarkTextBox.Text = _posTrShippingDt.Remark;
        }

        //protected void EditButton_Click(object sender, ImageClickEventArgs e)
        //{
        //    Response.Redirect(this._editDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        //}

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}