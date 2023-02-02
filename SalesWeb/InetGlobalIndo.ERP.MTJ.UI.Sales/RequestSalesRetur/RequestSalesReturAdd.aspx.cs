using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.RequestSalesRetur
{
    public partial class RequestSalesReturAdd : RequestSalesReturBase
    {
        private CustomerBL _custBL = new CustomerBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private RequestSalesReturBL _requestSalesReturBL = new RequestSalesReturBL();
        private BillOfLadingBL _billOfLadingBL = new BillOfLadingBL();
        private PermissionBL _permBL = new PermissionBL();
        private ProductBL _productBL = new ProductBL();
        private PriceGroupBL _priceGroupBL = new PriceGroupBL();
        private CompanyConfig _companyConfigBL = new CompanyConfig();
        private DirectSalesBL _directSalesBL = new DirectSalesBL();
        private POSBL _posBL = new POSBL();

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

                this.btnSearchCustNo.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findCustomer&configCode=customer','_popSearch','width=1200,height=1000,toolbar=0,location=0,status=0,scrollbars=1')";
                String spawnJS = "<script language='JavaScript'>\n";
                ////////////////////DECLARE FUNCTION FOR CATCHING CUSTOMER SEARCH
                spawnJS += "function findCustomer(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.CustomerTextBox.ClientID + "').value = dataArray[0];\n";
                spawnJS += "document.getElementById('" + this.CustomerNameTextBox.ClientID + "').value = dataArray[1];\n";
                spawnJS += "}\n";
                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.CustomerTextBox.Attributes.Add("ReadOnly", "True");
                this.CustomerNameTextBox.Attributes.Add("ReadOnly", "True");
                this.CalendarScriptLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                //this.ShowCust();
                this.ShowCurrency();

                this.ClearLabel();
                this.ClearData();
                this.SetAttribute();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");

            this.CurrRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        //public void ShowCust()
        //{
        //    this.CustDropDownList.Items.Clear();
        //    this.CustDropDownList.DataTextField = "CustName";
        //    this.CustDropDownList.DataValueField = "CustCode";
        //    this.CustDropDownList.DataSource = this._custBL.GetListCustomerForDDL();
        //    this.CustDropDownList.DataBind();
        //    this.CustDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //public void ShowSuratJalan()///DO  ==> STCSjHd
        //{
        //    this.SuratJalanDropDownList.Items.Clear();
        //    this.SuratJalanDropDownList.DataTextField = "FileNmbr";
        //    this.SuratJalanDropDownList.DataValueField = "TransNmbr";
        //    this.SuratJalanDropDownList.DataSource = this._billOfLadingBL.GetSJFileNmbrForDDL(this.CustomerTextBox.Text);
        //    this.SuratJalanDropDownList.DataBind();
        //    this.SuratJalanDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //public void ShowSuratJalanDirectSales()///DS ==> SALTrDirectSalesHd
        //{
        //    this.SuratJalanDropDownList.Items.Clear();
        //    this.SuratJalanDropDownList.DataTextField = "FileNo";
        //    this.SuratJalanDropDownList.DataValueField = "TransNmbr";
        //    this.SuratJalanDropDownList.DataSource = this._billOfLadingBL.GetDSFileNmbrForDDL(this.CustomerTextBox.Text);
        //    this.SuratJalanDropDownList.DataBind();
        //    this.SuratJalanDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //public void ShowSuratJalanPOS()///POS ==> SALTrRetail
        //{
        //    this.SuratJalanDropDownList.Items.Clear();
        //    this.SuratJalanDropDownList.DataTextField = "FileNmbr";
        //    this.SuratJalanDropDownList.DataValueField = "TransNmbr";
        //    this.SuratJalanDropDownList.DataSource = this._billOfLadingBL.GetPOSFileNmbrForDDL(this.CustomerTextBox.Text);
        //    this.SuratJalanDropDownList.DataBind();
        //    this.SuratJalanDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        public void ShowCurrency()
        {
            this.CurrCodeDropDownList.Items.Clear();
            this.CurrCodeDropDownList.DataTextField = "CurrCode";
            this.CurrCodeDropDownList.DataValueField = "CurrCode";
            this.CurrCodeDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrCodeDropDownList.DataBind();
            this.CurrCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.WarningLabel.Text = "";
            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            //this.CustDropDownList.SelectedValue = "null";
            this.CustomerTextBox.Text = "";
            this.CustomerNameTextBox.Text = "";
            this.CurrCodeDropDownList.SelectedValue = "null";
            this.CurrRateTextBox.Text = "";
            //this.CurrRateTextBox.Attributes.Remove("ReadOnly");
            this.TotalForexTextBox.Text = "0";
            this.AttnTextBox.Text = "";
            this.RemarkTextBox.Text = "";
            //this.SuratJalanDropDownList.Items.Clear();
            //this.SuratJalanDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            //this.SuratJalanDropDownList.SelectedValue = "null";
            this.SuratJalanTextBox.Text = "";
            this.DecimalPlaceHiddenField.Value = "";
        }

        protected void CustDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangesDDL();
        }

        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrCodeDropDownList.SelectedValue != "null")
            {
                string _currCode = this._custBL.GetCurr(CustomerTextBox.Text);
                string _currCodeHome = _currencyBL.GetCurrDefault();
                byte _decimalPlace = _currencyBL.GetDecimalPlace(this.CurrCodeDropDownList.SelectedValue);

                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                this.CurrRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                if (this.CurrCodeDropDownList.SelectedValue == _currCodeHome)
                {
                    this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.CurrRateTextBox.Text = "1";
                }
                else
                {
                    this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                }
            }
            else
            {
                this.CurrRateTextBox.Text = "0";
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            MKTReqReturHd _mktReqReturHd = new MKTReqReturHd();

            _mktReqReturHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _mktReqReturHd.Status = RequestSalesReturDataMapper.GetStatus(TransStatus.OnHold);
            _mktReqReturHd.CustCode = this.CustomerTextBox.Text;
            _mktReqReturHd.UseReferense = Convert.ToChar(this.UseReferenceRBL.SelectedValue);
            if (this.UseReferenceRBL.SelectedValue == "Y")
            {
                //_mktReqReturHd.TransType = this.TransTypeDropDownList.SelectedValue;
                //_mktReqReturHd.SJNo = this.SuratJalanDropDownList.SelectedValue;
                _mktReqReturHd.SJNo = this.SuratJalanTextBox.Text;
            }
            else
            {
                _mktReqReturHd.TransType = "";
                _mktReqReturHd.SJNo = "";
            }
            _mktReqReturHd.FgDeliveryBack = Convert.ToChar(this.DeliveryBackRBL.SelectedValue);
            _mktReqReturHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _mktReqReturHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _mktReqReturHd.Remark = this.RemarkTextBox.Text;
            _mktReqReturHd.Attn = this.AttnTextBox.Text;
            _mktReqReturHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _mktReqReturHd.CreatedBy = HttpContext.Current.User.Identity.Name;
            _mktReqReturHd.CreatedDate = DateTime.Now;

            List<MKTReqReturDt> _listDetail = new List<MKTReqReturDt>();
            
            //if (this.TransTypeDropDownList.SelectedValue == "DO")
            //{
            //    List<STCSJDt> _listSTCSJDt = this._billOfLadingBL.GetListSTCSJDt(this.SuratJalanDropDownList.SelectedValue);

            //    foreach (STCSJDt _item in _listSTCSJDt)
            //    {
            //        MKTReqReturDt _mktReqReturDt = new MKTReqReturDt();

            //        _mktReqReturDt.ProductCode = _item.ProductCode;
            //        _mktReqReturDt.Unit = _item.Unit;
            //        _mktReqReturDt.Qty = _billOfLadingBL.GetSingleQtyFromSTCSJDt(this.SuratJalanDropDownList.SelectedValue, _item.ProductCode);

            //        string _productType = _productBL.GetSingleProduct(_item.ProductCode).ProductType;
            //        Boolean _isUsingPG = _productBL.GetSingleProductType(_productType).IsUsingPG;

            //        if (_isUsingPG == true)
            //        {
            //            String _priceGroupCode = _productBL.GetSingleProduct(_item.ProductCode).PriceGroupCode;
            //            int _year = Convert.ToInt32(_companyConfigBL.GetSingle(CompanyConfigure.ActivePGYear).SetValue);
            //            _mktReqReturDt.PriceForex = _priceGroupBL.GetSingle(_priceGroupCode, _year).AmountForex;

            //        }
            //        else
            //        {
            //            _mktReqReturDt.PriceForex = 0;
            //        }
            //        _mktReqReturDt.AmountForex = 0;
            //        _mktReqReturDt.Remark = "";

            //        _listDetail.Add(new MKTReqReturDt("", _mktReqReturDt.ProductCode, _mktReqReturDt.Qty, _mktReqReturDt.Unit, _mktReqReturDt.PriceForex, _mktReqReturDt.AmountForex));
            //    }
            //}
            //if (this.TransTypeDropDownList.SelectedValue == "DS")
            //{
            //    List<SALTrDirectSalesDt> _listSTCSJDt = this._directSalesBL.GetListDirectSalesDt(this.SuratJalanDropDownList.SelectedValue);

            //    foreach (SALTrDirectSalesDt _item in _listSTCSJDt)
            //    {
            //        MKTReqReturDt _mktReqReturDt = new MKTReqReturDt();

            //        _mktReqReturDt.ProductCode = _item.ProductCode;
            //        _mktReqReturDt.Unit = _item.Unit;
            //        _mktReqReturDt.Qty = _directSalesBL.GetSingleQtyFromSTCSJDt(this.SuratJalanDropDownList.SelectedValue, _item.ProductCode);

            //        string _productType = _productBL.GetSingleProduct(_item.ProductCode).ProductType;
            //        Boolean _isUsingPG = _productBL.GetSingleProductType(_productType).IsUsingPG;

            //        if (_isUsingPG == true)
            //        {
            //            String _priceGroupCode = _productBL.GetSingleProduct(_item.ProductCode).PriceGroupCode;
            //            int _year = Convert.ToInt32(_companyConfigBL.GetSingle(CompanyConfigure.ActivePGYear).SetValue);
            //            _mktReqReturDt.PriceForex = _priceGroupBL.GetSingle(_priceGroupCode, _year).AmountForex;

            //        }
            //        else
            //        {
            //            _mktReqReturDt.PriceForex = 0;
            //        }
            //        _mktReqReturDt.AmountForex = 0;
            //        _mktReqReturDt.Remark = "";

            //        _listDetail.Add(new MKTReqReturDt("", _mktReqReturDt.ProductCode, _mktReqReturDt.Qty, _mktReqReturDt.Unit, _mktReqReturDt.PriceForex, _mktReqReturDt.AmountForex));
            //    }
            //}
            //if (this.TransTypeDropDownList.SelectedValue == "POS")
            //{
            //    List<SAL_TrRetailItem> _listSTCSJDt = this._posBL.GetListPOSDt(this.SuratJalanDropDownList.SelectedValue);

            //    foreach (SAL_TrRetailItem _item in _listSTCSJDt)
            //    {
            //        MKTReqReturDt _mktReqReturDt = new MKTReqReturDt();

            //        _mktReqReturDt.ProductCode = _item.ProductCode;
            //        _mktReqReturDt.Unit = _productBL.GetUnitCodeByCode(_item.ProductCode);
            //        _mktReqReturDt.Qty = _posBL.GetSingleQtyFromSTCSJDt(this.SuratJalanDropDownList.SelectedValue, _item.ProductCode);

            //        string _productType = _productBL.GetSingleProduct(_item.ProductCode).ProductType;
            //        Boolean _isUsingPG = _productBL.GetSingleProductType(_productType).IsUsingPG;

            //        if (_isUsingPG == true)
            //        {
            //            String _priceGroupCode = _productBL.GetSingleProduct(_item.ProductCode).PriceGroupCode;
            //            int _year = Convert.ToInt32(_companyConfigBL.GetSingle(CompanyConfigure.ActivePGYear).SetValue);
            //            _mktReqReturDt.PriceForex = _priceGroupBL.GetSingle(_priceGroupCode, _year).AmountForex;

            //        }
            //        else
            //        {
            //            _mktReqReturDt.PriceForex = 0;
            //        }
            //        _mktReqReturDt.AmountForex = 0;
            //        _mktReqReturDt.Remark = "";

            //        _listDetail.Add(new MKTReqReturDt("", _mktReqReturDt.ProductCode, _mktReqReturDt.Qty, _mktReqReturDt.Unit, _mktReqReturDt.PriceForex, _mktReqReturDt.AmountForex));
            //    }
            //}
            string _result = this._requestSalesReturBL.AddMKTReqReturHd(_mktReqReturHd, _listDetail);
            if (_result != "")
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }

        protected void TransTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangesDDL();
        }

        protected void ChangesDDL()
        {
            if (this.CustomerTextBox.Text != "null") //&& this.TransTypeDropDownList.SelectedValue != "null"
            {
                this.AttnTextBox.Text = _custBL.GetCustContact(this.CustomerTextBox.Text);
                string _currCode = this._custBL.GetCurr(CustomerTextBox.Text);
                byte _decimalPlace = _currencyBL.GetDecimalPlace(_currCode);

                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                if (_currCode != "")
                {
                    this.CurrCodeDropDownList.SelectedValue = _currCode;
                }
                this.CurrRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                if (this.CurrCodeDropDownList.SelectedValue == _currencyBL.GetCurrDefault())
                {
                    this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.CurrRateTextBox.Text = "1";
                }
                else
                {
                    this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                }

                //if (this.TransTypeDropDownList.SelectedValue == "DO")
                //{
                //    this.ShowSuratJalan();
                //}
                //else if (this.TransTypeDropDownList.SelectedValue == "DS")
                //{
                //    this.ShowSuratJalanDirectSales();
                //}
                //else if (this.TransTypeDropDownList.SelectedValue == "POS")
                //{
                //    this.ShowSuratJalanPOS();
                //}
            }
            else
            {
                this.AttnTextBox.Text = "";
                this.CurrCodeDropDownList.SelectedValue = "null";
                this.CurrRateTextBox.Text = "0";
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");

                //this.SuratJalanDropDownList.Items.Clear();
                //this.SuratJalanDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.SuratJalanTextBox.Text = "";
            }
        }

        protected void UseReferenceRBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.UseReferenceRBL.SelectedValue == "Y")
            {
                this.ReferenceTable.Visible = true;
            }
            else
            {
                this.ReferenceTable.Visible = false;
            }
        }
    }
}