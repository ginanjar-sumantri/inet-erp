using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using BusinessRule.POS;
using BusinessRule.POSInterface;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Threading;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace POS.POSInterface.Retail
{
    public partial class POSRetail : POSRetailBase
    {
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currBL = new CurrencyBL();
        private ProductBL _productBL = new ProductBL();
        private POSDiscountBL _posDiscountBL = new POSDiscountBL();
        private POSRetailBL _posRetailBL = new POSRetailBL();
        private MemberBL _memberBL = new MemberBL();
        private CustomerDOBL _customerDOBL = new CustomerDOBL();
        private POSReasonBL _reasonBL = new POSReasonBL();
        private POSConfigurationBL _pOSConfigurationBL = new POSConfigurationBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack == true)
                {
                    this.JScriptLiteral.Text = "<script src=\"" + ApplicationConfig.HomeWebAppURL + "JScript.js" + "\" type=\"text/javascript\"></script>";
                    this.IsEditedHiddenField.Value = "0";

                    this.SetAttribute();

                    this.ClearLabel();
                    this.ClearData();
                    this.SetDefaultData();
                    this.SetButtonPermission();

                    //this.NewMemberButton.OnClientClick = "window.open('../Registration/Registration.aspx','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1')";
                    this.SearchButton.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProduct&configCode=productPOS','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1')";
                    this.OpenHoldButton.OnClientClick = "window.open('../General/OpenHold.aspx?valueCatcher=findTransNmbr','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1')";
                    this.JoinJobOrderButton.OnClientClick = "window.open('../General/JoinJobOrder.aspx?valueCatcher=findTransNmbr&pos=retail','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1')";
                    this.CheckStatusButton.OnClientClick = "window.open('../General/CheckStatus.aspx?pos=retail','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1')";

                    this.ReferenceNoTextBox.Attributes["onclick"] = "ReferenceKeyBoard(this.id)";
                    this.CustNameTextBox.Attributes["onclick"] = "CustNameKeyBoard(this.id)";
                    this.CustPhoneTextBox.Attributes["onclick"] = "CustPhoneKeyBoard(this.id)";
                    this.ProductCodeTextBox.Attributes["onclick"] = "ProductCodeKeyBoard(this.id)";
                    this.PasswordTextBox.Attributes["onclick"] = "PasswordKeyBoard(this.id)";

                    String spawnJS = "<script language='JavaScript'>\n";
                    ////////////////////DECLARE FUNCTION FOR CATCHING PRODUCT SEARCH
                    spawnJS += "function findProduct(x) {\n";
                    spawnJS += "dataArray = x.split ('|') ;\n";
                    spawnJS += "document.getElementById('" + this.ProductCodeTextBox.ClientID + "').value = dataArray [0];\n";
                    spawnJS += "document.forms[0].submit();\n";
                    spawnJS += "}\n";

                    ////////////////////DECLARE FUNCTION FOR CATCHING ON HOLD SEARCH
                    spawnJS += "function findTransNmbr(x) {\n";
                    spawnJS += "dataArray = x.split ('|') ;\n";
                    spawnJS += "document.getElementById('" + this.TransNmbrTextBox.ClientID + "').value = dataArray [0];\n";
                    spawnJS += "document.forms[0].submit();\n";
                    spawnJS += "}\n";

                    //DECLARE FUNCTION FOR Calling KeyBoard Reference
                    spawnJS += "function ReferenceKeyBoard(x) {\n";
                    spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findReference&titleinput=Reference&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                    spawnJS += "}\n";

                    //DECLARE FUNCTION FOR CATCHING ON Reference
                    spawnJS += "function findReference(x) {\n";
                    spawnJS += "dataArray = x.split ('|') ;\n";
                    spawnJS += "document.getElementById('" + this.ReferenceNoTextBox.ClientID + "').value = dataArray [0];\n";
                    spawnJS += "document.forms[0].submit();\n";
                    spawnJS += "}\n";

                    //DECLARE FUNCTION FOR Calling KeyBoard CustName
                    spawnJS += "function CustNameKeyBoard(x) {\n";
                    spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findCustName&titleinput=Name&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                    spawnJS += "}\n";

                    //DECLARE FUNCTION FOR CATCHING ON CustName
                    spawnJS += "function findCustName(x) {\n";
                    spawnJS += "dataArray = x.split ('|') ;\n";
                    spawnJS += "document.getElementById('" + this.CustNameTextBox.ClientID + "').value = dataArray [0];\n";
                    spawnJS += "document.forms[0].submit();\n";
                    spawnJS += "}\n";

                    //DECLARE FUNCTION FOR Calling KeyBoard CustPhone
                    spawnJS += "function CustPhoneKeyBoard(x) {\n";
                    spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findCustPhone&titleinput=Phone&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                    spawnJS += "}\n";

                    //DECLARE FUNCTION FOR CATCHING ON CustPhone
                    spawnJS += "function findCustPhone(x) {\n";
                    spawnJS += "dataArray = x.split ('|') ;\n";
                    spawnJS += "document.getElementById('" + this.CustPhoneTextBox.ClientID + "').value = dataArray [0];\n";
                    spawnJS += "document.forms[0].submit();\n";
                    spawnJS += "}\n";

                    //DECLARE FUNCTION FOR Calling KeyBoard ProductCode
                    spawnJS += "function ProductCodeKeyBoard(x) {\n";
                    spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findProductCode&titleinput=Product Code&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                    spawnJS += "}\n";

                    //DECLARE FUNCTION FOR CATCHING ON ProductCode
                    spawnJS += "function findProductCode(x) {\n";
                    spawnJS += "dataArray = x.split ('|') ;\n";
                    spawnJS += "document.getElementById('" + this.ProductCodeTextBox.ClientID + "').value = dataArray [0];\n";
                    spawnJS += "document.forms[0].submit();\n";
                    spawnJS += "}\n";

                    //DECLARE FUNCTION FOR Calling KeyBoard Password
                    spawnJS += "function PasswordKeyBoard(x) {\n";
                    spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findPassword&titleinput=Password&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                    spawnJS += "}\n";

                    //DECLARE FUNCTION FOR CATCHING ON Password
                    spawnJS += "function findPassword(x) {\n";
                    spawnJS += "dataArray = x.split ('|') ;\n";
                    spawnJS += "document.getElementById('" + this.PasswordTextBox.ClientID + "').value = dataArray [0];\n";
                    spawnJS += "document.forms[0].submit();\n";
                    spawnJS += "}\n";

                    spawnJS += "</script>\n";
                    this.javascriptReceiver.Text = spawnJS;

                    this.ChangeVisiblePanel(0);
                    //this.FormPanel.Visible = true;
                    //this.ReasonListPanel.Visible = false;
                }

                if (Request.QueryString[this._referenceNo].ToString() != "")
                {
                    this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);
                    string _code = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._referenceNo), ApplicationConfig.EncryptionKey);
                    if (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._referenceNo), ApplicationConfig.EncryptionKey) != "")
                    {
                        string[] _splitCode = _code.Split('-');
                        this.ReferenceNoTextBox.Text = _splitCode[0];
                        this.ReferenceNoTextBox.Enabled = false;
                        this.ReferenceNoHiddenField.Value = _splitCode[0];
                        this.MemberBarcodeTextBox.Enabled = false;
                        this.CustNameTextBox.Text = _splitCode[1];
                        this.CustNameTextBox.Enabled = false;
                        this.CustPhoneTextBox.Text = _splitCode[2];
                        this.CustPhoneTextBox.Enabled = false;
                        this.NewMemberButton.Enabled = false;
                        POSTrDeliveryOrderRef _pOSTrDeliveryOrderRef = this._customerDOBL.GetSingleTrDeliveryOrderRefByReferenceNoTransType(_splitCode[0], POSTransTypeDataMapper.GetTransType(POSTransType.Retail));
                        if (_pOSTrDeliveryOrderRef != null)
                        {
                            this.TransNmbrTextBox.Text = _pOSTrDeliveryOrderRef.TransNmbr;
                        }
                    }
                }

                if (this.TransNmbrTextBox.Text != "")
                {
                    if (this.IsEditedHiddenField.Value == "0")
                    {
                        this.LoadHoldedData();
                        this.IsEditedHiddenField.Value = "1";
                        this.BarcodeTextBox.Focus();
                    }
                }
                else
                {
                    this.ReferenceNoTextBox.Focus();
                }

                //if (this.boughtItems.Value != "")
                //{
                this.showItem();
                //}
                //else
                //{
                //    this.perulanganDataDibeli.Text = "";
                //}
                //this.showItem();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        private void SetButtonPermission()
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDCashier, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                this.GotoCashierButton.Visible = false;
                this.CashierAbuButton.Visible = true;
                this.CashierAbuButton.Enabled = false;
            }
            else
            {
                this.GotoCashierButton.Visible = true;
                this.CashierAbuButton.Visible = false;
            }
        }

        protected void SetAttribute()
        {
            this.TransNmbrTextBox.Attributes.Add("ReadOnly", "True");

            this.ReferenceNoTextBox.Attributes.Add("onfocus", "$('#currActiveInput').val(this.id); $(this).select();");
            this.MemberBarcodeTextBox.Attributes.Add("onfocus", "$('#currActiveInput').val(this.id); $(this).select();");
            this.MemberBarcodeTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.MemberBarcodeTextBox.ClientID + ")");
            this.CustNameTextBox.Attributes.Add("onfocus", "$('#currActiveInput').val(this.id); $(this).select();");
            this.CustPhoneTextBox.Attributes.Add("onfocus", "$('#currActiveInput').val(this.id); $(this).select();");
            this.CustPhoneTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.CustPhoneTextBox.ClientID + ")");

            this.BarcodeTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.BarcodeTextBox.ClientID + ")");
            this.BarcodeTextBox.Attributes.Add("onfocus", "$('#currActiveInput').val(this.id); $(this).select();");
            this.QtyTextBox.Attributes.Add("onfocus", "$('#currActiveInput').val(this.id); $(this).val('');");
            this.ProductCodeTextBox.Attributes.Add("onfocus", "$('#currActiveInput').val(this.id); $(this).val('');");

        }

        protected void ClearLabel()
        {
            this.TotalItemLabel.Text = "0";
            DateTime _now = DateTime.Now;
            this.DateLabel.Text = DateFormMapper.GetValue(_now);
            this.CurrencyLabel.Text = "";
            this.CashierLabel.Text = "";
            this.GrandTotalLabel.Text = "0";

            this.ProductCodeLabel.Text = "";
            this.DescriptionLabel.Text = "";
            this.QtyLabel.Text = "0";
            this.DiscLabel.Text = "0";
            this.PriceLabel.Text = "0";
            this.LineTotalLabel.Text = "0";
            this.WarningLabel.Text = "";
        }

        protected void ClearData()
        {
            this.TransNmbrTextBox.Text = "";
            this.ReferenceNoTextBox.Text = "";
            this.MemberBarcodeTextBox.Text = "";
            this.CustNameTextBox.Text = "";
            this.CustPhoneTextBox.Text = "";
            this.ProductCodeTextBox.Text = "";
            this.QtyTextBox.Text = "1";

            this.itemCount.Value = "0";
            this.SubTotalHiddenField.Value = "0";
            this.DiscountTotalHiddenField.Value = "0";
            this.ServiceChargeHiddenField.Value = "0";
            this.TaxHiddenField.Value = "0";

            this.SubTotalLabel.Text = "0";
            this.DiscProductLabel.Text = "0";
            this.ServiceChargeLabel.Text = "0";
            this.TaxLabel.Text = "0";
            this.TotalLabel.Text = "0";
            this.GrandTotalLabel.Text = "0";

            this.IsEditedHiddenField.Value = "0";
        }

        protected void ClearProduct()
        {
            this.BarcodeTextBox.Text = "";
            this.QtyTextBox.Text = "1";
            //this.ProductImage.ImageUrl = "";
            //this.ProductNameLabel.Text = "";
        }

        protected void SetDefaultData()
        {
            try
            {
                this.CurrencyLabel.Text = this._currBL.GetCurrDefault();
                this.CashierLabel.Text = HttpContext.Current.User.Identity.Name;
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void LoadHoldedData()
        {
            try
            {
                this.boughtItems.Value = "";
                POSTrRetailHd _retailHd = this._posRetailBL.GetSingle(this.TransNmbrTextBox.Text);

                this.ReferenceNoTextBox.Text = _retailHd.ReferenceNo;
                this.MemberBarcodeTextBox.Text = _retailHd.MemberID;

                if (_retailHd.MemberID.ToString().Trim() != "")
                {
                    this.CustNameTextBox.Attributes.Add("ReadOnly", "True");
                    this.CustNameTextBox.Attributes.Add("style", "color: #808080");
                    this.CustPhoneTextBox.Attributes.Add("ReadOnly", "True");
                    this.CustPhoneTextBox.Attributes.Add("style", "color: #808080");
                }

                this.CustNameTextBox.Text = _retailHd.CustName;
                this.CustPhoneTextBox.Text = _retailHd.CustPhone;

                List<POSTrRetailDt> _listRetailDt = this._posRetailBL.GetListRetailDtByTransNmbr(this.TransNmbrTextBox.Text);
                foreach (var _row in _listRetailDt)
                {
                    MsProduct _msProduct = this._productBL.GetSingleProduct(_row.ProductCode);
                    MsProductType _msProductType = this._productBL.GetSingleProductType(_msProduct.ProductType);
                    Boolean _fgServiceCalculate = (_msProductType.fgServiceChargesCalculate == null) ? false : Convert.ToBoolean(_msProductType.fgServiceChargesCalculate);
                    Boolean _fgTax = (_msProductType.fgTax == null) ? false : Convert.ToBoolean(_msProductType.fgTax);
                    Decimal _serviceCharge = 0;
                    Decimal _taxValue = 0;
                    Decimal _pb1Value = 0;

                    Decimal _service = (_msProductType.ServiceCharges == null) ? 0 : Convert.ToDecimal(_msProductType.ServiceCharges);
                    _serviceCharge += _service / 100 * Convert.ToDecimal(this.LineTotalLabel.Text);
                    if (_msProductType.TaxTypeCode == "1")
                    {
                        if (_fgTax == false)
                        {
                            _taxValue += 0;
                        }
                        else
                        {
                            Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                            if (_fgServiceCalculate == false)
                            {
                                _taxValue += _tax / 100 * Convert.ToDecimal(this.LineTotalLabel.Text);
                            }
                            else
                            {
                                _taxValue += _tax / 100 * (Convert.ToDecimal(this.LineTotalLabel.Text) + _serviceCharge);
                            }
                        }
                    }
                    else if (_msProductType.TaxTypeCode == "2")
                    {
                        Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                        _pb1Value = (_tax / 100 * Convert.ToDecimal(_row.AmountForex)) * Convert.ToDecimal(_row.Qty);
                    }


                    if (this.boughtItems.Value == "")
                    {
                        this.boughtItems.Value = _row.ItemNo + "|" + _row.ProductCode + "|";
                        this.boughtItems.Value += _row.Remark + "|" + _row.Qty.ToString() + "|";
                        this.boughtItems.Value += _row.DiscForex.ToString() + "|" + _row.AmountForex.ToString() + "|";
                        this.boughtItems.Value += ((_row.Qty * _row.AmountForex) - _row.DiscForex).ToString();
                        this.boughtItems.Value += "|" + _serviceCharge.ToString() + "|" + _taxValue.ToString();
                        this.boughtItems.Value += "|" + _msProduct.Barcode + "|" + _pb1Value.ToString();
                    }
                    else
                    {
                        this.boughtItems.Value += "^" + _row.ItemNo + "|" + _row.ProductCode + "|";
                        this.boughtItems.Value += _row.Remark + "|" + _row.Qty.ToString() + "|";
                        this.boughtItems.Value += _row.DiscForex.ToString() + "|" + _row.AmountForex.ToString() + "|";
                        this.boughtItems.Value += ((_row.Qty * _row.AmountForex) - _row.DiscForex).ToString();
                        this.boughtItems.Value += "|" + _serviceCharge.ToString() + "|" + _taxValue.ToString();
                        this.boughtItems.Value += "|" + _msProduct.Barcode + "|" + _pb1Value.ToString();
                    }

                    this.itemCount.Value = _row.ItemNo.ToString();
                }

                this.showItem();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void MemberBarcodeTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.MemberBarcodeTextBox.Text.Length == 13)
                {
                    MsMember _member = this._memberBL.GetSingleByBarcode(this.MemberBarcodeTextBox.Text);

                    if (_member != null)
                    {
                        this.CustNameTextBox.Text = _member.MemberName;
                        this.CustNameTextBox.Attributes.Add("ReadOnly", "True");
                        this.CustNameTextBox.Attributes.Add("style", "color: #808080");

                        if (_member.Telephone1 == "" || _member.Telephone1 == null)
                        {
                            this.CustPhoneTextBox.Text = _member.HandPhone1;
                            this.CustPhoneTextBox.Attributes.Add("ReadOnly", "True");
                            this.CustPhoneTextBox.Attributes.Add("style", "color: #808080");
                        }
                        else
                        {
                            this.CustPhoneTextBox.Text = _member.Telephone1;
                            this.CustPhoneTextBox.Attributes.Add("ReadOnly", "True");
                            this.CustPhoneTextBox.Attributes.Add("style", "color: #808080");
                        }
                    }
                }
                else
                {
                    this.MemberBarcodeTextBox.Text = "";
                    this.CustNameTextBox.Attributes.Remove("ReadOnly");
                    this.CustNameTextBox.Attributes.Add("style", "color: #000000");
                    this.CustPhoneTextBox.Attributes.Remove("ReadOnly");
                    this.CustPhoneTextBox.Attributes.Add("style", "color: #000000");
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void BarcodeTextBox_TextChanged(object sender, EventArgs e)
        {

            if (this.NoUrutHiddenField.Value == "")
            {
                if (this.BarcodeTextBox.Text.Length == 13)
                {
                    this.LoadProduct();
                }
            }

        }

        protected void ProductCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.NoUrutHiddenField.Value == "")
            {
                this.LoadProduct();
            }
        }

        private void LoadProduct()
        {
            try
            {
                MsProduct _msProduct = null;
                bool _msProductpos = this._productBL.GetSingleProductForPOS(this.ProductCodeTextBox.Text);
                bool _product = this._productBL.GetSingleProductByBarcodeForPOS(this.BarcodeTextBox.Text);
                if (this.BarcodeTextBox.Text == "")
                {
                    _product = false;
                }

                if (_product == true || _msProductpos == true)
                {
                    if (this.BarcodeTextBox.Text.Length == 13)
                    {
                        _msProduct = this._productBL.GetSingleProductByBarcode(this.BarcodeTextBox.Text);
                    }
                    else
                    {
                        _msProduct = this._productBL.GetSingleProduct(this.ProductCodeTextBox.Text);
                    }
                    this.ProductCodeTextBox.Text = _msProduct.ProductCode;
                    string _strImagePath = ApplicationConfig.ProductPhotoVirDirPath + _msProduct.Photo;
                    if (_msProduct.Photo == null)
                    {
                        _strImagePath = ApplicationConfig.ProductPhotoVirDirPath + ApplicationConfig.ProductImageDefault;
                    }
                    this.ProductImage.Attributes.Add("src", "" + _strImagePath + "?t=" + System.DateTime.Now.ToString());
                    this.ProductNameLabel.Text = _msProduct.ProductName;

                    if (this.QtyTextBox.Text.Trim() != "")
                    {
                        this.itemCount.Value = (Convert.ToInt16(this.itemCount.Value) + 1).ToString();
                        Decimal _subTotal = Convert.ToDecimal(this.SubTotalHiddenField.Value);
                        Decimal _discTotal = 0;
                        Decimal _serviceCharge = 0;
                        Decimal _taxValue = 0;
                        Decimal _pb1Value = 0;

                        //MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductCodeTextBox.Text);

                        this.ProductCodeLabel.Text = _msProduct.ProductCode;
                        this.DescriptionLabel.Text = _msProduct.ProductName;
                        this.QtyLabel.Text = (this.QtyTextBox.Text == "") ? "0" : this.QtyTextBox.Text;

                        Decimal _msProductPrice = this._productBL.GetProductSalesPriceForPOS(this.ProductCodeTextBox.Text, this.CurrencyLabel.Text, _msProduct.Unit);
                        //16/11/11 boby move to settlement
                        //POSMsDiscountConfig _posMsDiscount = this._posDiscountBL.GetDiscountByProductCode(_msProduct.ProductCode);
                        //if (_posMsDiscount != null)
                        //{
                        //    if (_posMsDiscount.AmountType == POSDiscountDataMapper.GetAmountType(POSDiscountAmountType.Amount))
                        //    {
                        //        _discTotal = (_posMsDiscount.DiscAmount == null) ? 0 : Convert.ToDecimal(_posMsDiscount.DiscAmount);
                        //        _discTotal = _discTotal * Convert.ToInt32(this.QtyLabel.Text);
                        //    }
                        //    else
                        //    {
                        //        _discTotal = ((_posMsDiscount.DiscAmount == null) ? 0 : Convert.ToDecimal(_posMsDiscount.DiscAmount)) / 100 * _msProductPrice;
                        //        _discTotal = _discTotal * Convert.ToInt32(this.QtyLabel.Text);
                        //    }
                        //}

                        this.DiscLabel.Text = _discTotal.ToString("#,##0.00");
                        this.PriceLabel.Text = _msProductPrice.ToString("#,##0.00");
                        this.LineTotalLabel.Text = ((Convert.ToInt32(this.QtyTextBox.Text) * _msProductPrice) - _discTotal).ToString("#,##0.00");

                        MsProductType _msProductType = this._productBL.GetSingleProductType(_msProduct.ProductType);
                        Boolean _fgServiceCalculate = (_msProductType.fgServiceChargesCalculate == null) ? false : Convert.ToBoolean(_msProductType.fgServiceChargesCalculate);
                        Boolean _fgTax = (_msProductType.fgTax == null) ? false : Convert.ToBoolean(_msProductType.fgTax);

                        Decimal _service = (_msProductType.ServiceCharges == null) ? 0 : Convert.ToDecimal(_msProductType.ServiceCharges);
                        _serviceCharge += _service / 100 * Convert.ToDecimal(this.LineTotalLabel.Text);
                        if (_msProductType.TaxTypeCode == "1")
                        {
                            if (_fgTax == false)
                            {
                                _taxValue += 0;
                            }
                            else
                            {
                                Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                                if (_fgServiceCalculate == false)
                                {
                                    _taxValue += _tax / 100 * Convert.ToDecimal(this.LineTotalLabel.Text);
                                }
                                else
                                {
                                    _taxValue += _tax / 100 * (Convert.ToDecimal(this.LineTotalLabel.Text) + _serviceCharge);
                                }
                            }
                        }
                        else if (_msProductType.TaxTypeCode == "2")
                        {
                            Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                            _pb1Value = (_tax / 100 * Convert.ToDecimal(_msProductPrice)) * Convert.ToDecimal(this.QtyLabel.Text);
                        }

                        if (this.itemCount.Value == "1")
                        {
                            this.boughtItems.Value = this.itemCount.Value + "|" + this.ProductCodeLabel.Text + "|";
                            this.boughtItems.Value += this.DescriptionLabel.Text + "|" + this.QtyLabel.Text + "|" + this.DiscLabel.Text + "|";
                            this.boughtItems.Value += this.PriceLabel.Text + "|" + this.LineTotalLabel.Text;
                            this.boughtItems.Value += "|" + _serviceCharge.ToString() + "|" + _taxValue.ToString();
                            this.boughtItems.Value += "|" + _msProduct.Barcode + "|" + _pb1Value.ToString();
                            _subTotal += Convert.ToDecimal(this.LineTotalLabel.Text);
                            _discTotal += Convert.ToDecimal(this.DiscLabel.Text);
                        }
                        else
                        {
                            this.boughtItems.Value += "^" + this.itemCount.Value + "|" + this.ProductCodeLabel.Text + "|";
                            this.boughtItems.Value += this.DescriptionLabel.Text + "|" + this.QtyLabel.Text + "|" + this.DiscLabel.Text + "|";
                            this.boughtItems.Value += this.PriceLabel.Text + "|" + this.LineTotalLabel.Text;
                            this.boughtItems.Value += "|" + _serviceCharge.ToString() + "|" + _taxValue.ToString();
                            this.boughtItems.Value += "|" + _msProduct.Barcode + "|" + _pb1Value.ToString();
                            _subTotal += Convert.ToDecimal(this.LineTotalLabel.Text);
                            _discTotal += Convert.ToDecimal(this.DiscLabel.Text);
                        }

                        this.discHiddenField.Value = "0";
                        this.priceHiddenField.Value = "0";
                        this.lineTotalHiddenField.Value = "0";
                        this.SubTotalHiddenField.Value = _subTotal.ToString();
                        this.DiscountTotalHiddenField.Value = _discTotal.ToString();
                        this.ServiceChargeHiddenField.Value = _serviceCharge.ToString();
                        this.TaxHiddenField.Value = _taxValue.ToString();
                        this.pb1HiddenField.Value = _pb1Value.ToString();

                        this.ProductCodeLabel.Text = "";
                        this.DescriptionLabel.Text = "";
                        this.QtyLabel.Text = "0";
                        this.DiscLabel.Text = "0.00";
                        this.PriceLabel.Text = "0.00";
                        this.LineTotalLabel.Text = "0.00";

                        this.DiscProductLabel.Text = _discTotal.ToString("#,##0.00");
                        this.SubTotalLabel.Text = _subTotal.ToString("#,##0.00");

                        //int _service = Convert.ToInt32(new CompanyConfig().GetSingle(CompanyConfigure.POSServiceCharge).SetValue);
                        //Decimal _serviceCharge = _service * _subTotal / 100;
                        //int _tax = Convert.ToInt32(new CompanyConfig().GetSingle(CompanyConfigure.POSTaxCharge).SetValue);
                        //Decimal _taxCharge = _tax * _subTotal / 100;
                        this.ServiceChargeLabel.Text = _serviceCharge.ToString("#,##0.00");
                        this.TaxLabel.Text = (_taxValue + _pb1Value).ToString("#,##0.00");

                        decimal _total = _subTotal + _serviceCharge + _taxValue + _pb1Value;
                        this.TotalLabel.Text = _total.ToString("#,##0.00");
                        this.GrandTotalLabel.Text = _total.ToString("#,##0.00");

                        //this.BarcodeTextBox.Text = "";
                        //this.QtyTextBox.Text = "1";

                        this.showItem();
                        this.ClearProduct();
                        this.BarcodeTextBox.Focus();
                    }
                }
                else
                {
                    this.ProductNameLabel.Text = "Product is Not Found";
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void btnOKPanelNumber_Click(object sender, EventArgs e)
        {
            //bool _product = this._posRetailBL.GetSingleProduct(this.BarcodeTextBox.Text);

            if (this.NoUrutHiddenField.Value == "")
            {
                if (this.QtyTextBox.Text == "")
                {
                    this.QtyTextBox.Text = "1";
                }

                this.BarcodeTextBox.Focus();
            }
            else
            {
                this.IsEditedHiddenField.Value = "1";
                this.ReArrageItemCollection(this.NoUrutHiddenField.Value, this.QtyTextBox.Text);

                this.ClearProduct();
                this.NoUrutHiddenField.Value = "";
            }

        }

        private void ReArrageItemCollection(String _prmNoUrut, String _prmQtyEdit)
        {
            try
            {
                String[] _dataItem = this.boughtItems.Value.Split('^');
                String _strAccumulateLine = "";
                this.itemCount.Value = "0";

                foreach (var _dataRow in _dataItem)
                {
                    this.itemCount.Value = (Convert.ToInt16(this.itemCount.Value) + 1).ToString();

                    String[] _dataField = _dataRow.Split('|');

                    if (_dataField[0] != _prmNoUrut.ToString())
                    {
                        Decimal _lineTotal = Convert.ToInt32(_dataField[3]) * Convert.ToDecimal(_dataField[5]);
                        if (this.itemCount.Value == "1")
                        {
                            _strAccumulateLine = _dataField[0] + "|" + _dataField[1] + "|";
                            _strAccumulateLine += _dataField[2] + "|" + _dataField[3] + "|" + _dataField[4] + "|";
                            _strAccumulateLine += _dataField[5] + "|" + _lineTotal.ToString("#,##0.00");
                            _strAccumulateLine += "|" + _dataField[7] + "|" + _dataField[8];
                            _strAccumulateLine += "|" + _dataField[9] + "|" + _dataField[10];
                        }
                        else
                        {
                            _strAccumulateLine += "^" + _dataField[0] + "|" + _dataField[1] + "|";
                            _strAccumulateLine += _dataField[2] + "|" + _dataField[3] + "|" + _dataField[4] + "|";
                            _strAccumulateLine += _dataField[5] + "|" + _lineTotal.ToString("#,##0.00");
                            _strAccumulateLine += "|" + _dataField[7] + "|" + _dataField[8];
                            _strAccumulateLine += "|" + _dataField[9] + "|" + _dataField[10];
                        }
                    }
                    else
                    {
                        int _qtyNew = Convert.ToInt32(_prmQtyEdit);
                        Decimal _discountAwal = Convert.ToDecimal(_dataField[4]) / Convert.ToInt32(_dataField[3]);
                        Decimal _discTotal = _qtyNew * _discountAwal;
                        Decimal _lineTotal = (_qtyNew * Convert.ToDecimal(_dataField[5])) - _discTotal;
                        //Decimal _serviceTotal = (_qtyNew * (Convert.ToDecimal(_dataField[7]) / Convert.ToDecimal(_dataField[6]) * (Convert.ToDecimal(_dataField[5]) - _discountAwal)));
                        //Decimal _taxTotal = (_qtyNew * (Convert.ToDecimal(_dataField[8]) / Convert.ToDecimal(_dataField[6]) * (Convert.ToDecimal(_dataField[5]) - _discountAwal)));

                        MsProduct _msProduct = this._productBL.GetSingleProduct(_dataField[1]);
                        MsProductType _msProductType = this._productBL.GetSingleProductType(_msProduct.ProductType);
                        Boolean _fgServiceCalculate = (_msProductType.fgServiceChargesCalculate == null) ? false : Convert.ToBoolean(_msProductType.fgServiceChargesCalculate);
                        Boolean _fgTax = (_msProductType.fgTax == null) ? false : Convert.ToBoolean(_msProductType.fgTax);
                        Decimal _serviceCharge = 0;
                        Decimal _taxValue = 0;
                        Decimal _pb1Value = 0;

                        Decimal _service = (_msProductType.ServiceCharges == null) ? 0 : Convert.ToDecimal(_msProductType.ServiceCharges);
                        _serviceCharge += _service / 100 * _lineTotal;
                        if (_msProductType.TaxTypeCode == "1")
                        {
                            if (_fgTax == false)
                            {
                                _taxValue += 0;
                            }
                            else
                            {
                                Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                                if (_fgServiceCalculate == false)
                                {
                                    _taxValue += _tax / 100 * _lineTotal;
                                }
                                else
                                {
                                    _taxValue += _tax / 100 * (_lineTotal + _serviceCharge);
                                }
                            }
                        }
                        else if (_msProductType.TaxTypeCode == "2")
                        {
                            Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                            _pb1Value = (_tax / 100 * Convert.ToDecimal(_dataField[5])) * Convert.ToDecimal(_prmQtyEdit);
                        }

                        if (this.itemCount.Value == "1")
                        {
                            _strAccumulateLine = _dataField[0] + "|" + _dataField[1] + "|";
                            _strAccumulateLine += _dataField[2] + "|" + _prmQtyEdit + "|" + _discTotal.ToString("#,##0.00") + "|";
                            _strAccumulateLine += _dataField[5] + "|" + _lineTotal.ToString("#,##0.00");
                            _strAccumulateLine += "|" + _serviceCharge + "|" + _taxValue;
                            _strAccumulateLine += "|" + _dataField[9] + "|" + _pb1Value;
                        }
                        else
                        {
                            _strAccumulateLine += "^" + _dataField[0] + "|" + _dataField[1] + "|";
                            _strAccumulateLine += _dataField[2] + "|" + _prmQtyEdit + "|" + _discTotal.ToString("#,##0.00") + "|";
                            _strAccumulateLine += _dataField[5] + "|" + _lineTotal.ToString("#,##0.00");
                            _strAccumulateLine += "|" + _serviceCharge + "|" + _taxValue;
                            _strAccumulateLine += "|" + _dataField[9] + "|" + _pb1Value;
                        }
                    }
                }

                this.boughtItems.Value = _strAccumulateLine;
                this.showItem();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void showItem()
        {
            if (this.boughtItems.Value != "")
            {
                String _strGenerateTable = "";
                String[] _dataItem = this.boughtItems.Value.Split('^');
                Decimal _subTotal = 0;
                Decimal _discTotal = 0;
                Decimal _serviceTotal = 0;
                Decimal _taxTotal = 0;
                Decimal _pb1Total = 0;
                int _totalItem = 0;

                foreach (String _dataRow in _dataItem)
                {
                    _strGenerateTable += "<tr valign='top' height='20px'>";
                    String[] _dataField = _dataRow.Split('|');
                    int _index = 0;

                    foreach (String _data in _dataField)
                    {
                        if (_index >= 7)
                        {
                            _index += 1;
                            continue;
                        }

                        decimal _val = 0;
                        if (Decimal.TryParse(_data, out _val))
                        {
                            if (_index == 0) //format for no
                            {
                                _strGenerateTable += "<td>" + _data + "</td>";
                            }
                            else if (_index == 1) //format for productcode
                            {
                                _strGenerateTable += "<td align='left'>" + _data + "</td>";
                            }
                            else if (_index == 3) //format for qty
                            {
                                _strGenerateTable += "<td align='right'>" + _data + "</td>";
                            }
                            else //format for all other decimal
                            {
                                _strGenerateTable += "<td align='right'>" + Convert.ToDecimal(_data).ToString("#,##0.00") + "</td>";
                            }
                        }
                        else //format for string
                        {
                            _strGenerateTable += "<td align='left'>" + _data + "</td>";
                        }

                        if (_index == 3)
                        {
                            _strGenerateTable += "<td align='center'><img class='EditButton' onClick='editQty(\"" + _dataField[0] + "\", \"" + _dataField[9] + "\", \"" + this.NoUrutHiddenField.ClientID + "\", \"" + this.QtyTextBox.ClientID + "\", \"" + this.BarcodeTextBox.ClientID + "\");'/></td>";
                        }

                        _index += 1;
                    }

                    _strGenerateTable += "<td align='center'><img class='CancelButton' onClick='deleteItem(\"" + this.boughtItems.ClientID + "\"," + _dataField[0] + ",\"" + this.itemCount.ClientID + "\");'/></td></tr>";
                    _subTotal += Convert.ToDecimal(_dataField[6]);
                    _discTotal += Convert.ToDecimal(_dataField[4]);
                    _serviceTotal += Convert.ToDecimal(_dataField[7]);
                    _taxTotal += Convert.ToDecimal(_dataField[8]);
                    _pb1Total += Convert.ToDecimal(_dataField[10]);
                    _totalItem += Convert.ToInt32(_dataField[3]);
                }

                this.DiscountTotalHiddenField.Value = _discTotal.ToString();
                this.SubTotalHiddenField.Value = _subTotal.ToString();
                this.ServiceChargeHiddenField.Value = _serviceTotal.ToString();
                this.TaxHiddenField.Value = _taxTotal.ToString();
                this.pb1HiddenField.Value = _pb1Total.ToString();

                this.TotalItemLabel.Text = _totalItem.ToString();
                this.DiscProductLabel.Text = _discTotal.ToString("#,##0.00");
                this.SubTotalLabel.Text = _subTotal.ToString("#,##0.00");

                //int _service = Convert.ToInt32(new CompanyConfig().GetSingle(CompanyConfigure.POSServiceCharge).SetValue);
                //Decimal _serviceCharge = _service * _subTotal / 100;
                //int _tax = Convert.ToInt32(new CompanyConfig().GetSingle(CompanyConfigure.POSTaxCharge).SetValue);
                //Decimal _taxCharge = _tax * _subTotal / 100;
                this.ServiceChargeLabel.Text = _serviceTotal.ToString("#,##0.00");
                this.TaxLabel.Text = (_taxTotal + _pb1Total).ToString("#,##0.00");

                decimal _total = _subTotal + _serviceTotal + _taxTotal + _pb1Total;
                this.TotalLabel.Text = _total.ToString("#,##0.00");
                this.GrandTotalLabel.Text = _total.ToString("#,##0.00");

                this.perulanganDataDibeli.Text = _strGenerateTable;
            }
            else
            {
                Decimal _subTotal = 0;
                Decimal _discTotal = 0;
                Decimal _serviceTotal = 0;
                Decimal _taxTotal = 0;
                Decimal _pb1Total = 0;
                Decimal _total = 0;
                int _totalItem = 0;

                this.SubTotalHiddenField.Value = _subTotal.ToString();
                this.DiscountTotalHiddenField.Value = _discTotal.ToString();
                this.ServiceChargeHiddenField.Value = _serviceTotal.ToString();
                this.TaxHiddenField.Value = _taxTotal.ToString();
                this.pb1HiddenField.Value = _pb1Total.ToString();

                this.TotalItemLabel.Text = _totalItem.ToString();
                this.DiscProductLabel.Text = _discTotal.ToString("#,##0.00");
                this.SubTotalLabel.Text = _subTotal.ToString("#,##0.00");
                this.ServiceChargeLabel.Text = _serviceTotal.ToString("#,##0.00");
                this.TaxLabel.Text = (_taxTotal + _pb1Total).ToString("#,##0.00");
                this.TotalLabel.Text = _total.ToString("#,##0.00");
                this.GrandTotalLabel.Text = _subTotal.ToString("#,##0.00");

                this.perulanganDataDibeli.Text = "";
            }
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            if (this.ReferenceNoHiddenField.Value != "")
            {
                Response.Redirect("../DeliveryOrder/ListDeliveryOrder.aspx" + "?" + "referenceNo=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                Response.Redirect("../" + this._loginPage);
            }
        }

        protected void HoldButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.boughtItems.Value != "" && this.ReferenceNoTextBox.Text != "")
                {
                    DateTime _now = DateTime.Now;
                    POSTrRetailHd _posTrRetailHd = new POSTrRetailHd();
                    _posTrRetailHd.TransType = POSTransTypeDataMapper.GetTransType(POSTransType.Retail);
                    _posTrRetailHd.TransDate = _now;
                    _posTrRetailHd.ReferenceNo = this.ReferenceNoTextBox.Text;
                    _posTrRetailHd.MemberID = this.MemberBarcodeTextBox.Text;
                    _posTrRetailHd.CustName = this.CustNameTextBox.Text;
                    _posTrRetailHd.CustPhone = this.CustPhoneTextBox.Text;
                    _posTrRetailHd.OperatorID = this.CashierLabel.Text;
                    _posTrRetailHd.TableRefNmbr = "";
                    _posTrRetailHd.Status = POSTrRetailDataMapper.GetStatus(POSTrRetailStatus.OnHold);
                    _posTrRetailHd.IsVOID = false;
                    _posTrRetailHd.CurrCode = this.CurrencyLabel.Text;
                    _posTrRetailHd.ForexRate = 1;
                    _posTrRetailHd.SubTotalForex = Convert.ToDecimal(this.SubTotalHiddenField.Value);
                    _posTrRetailHd.DiscPercentage = 0;
                    _posTrRetailHd.DiscForex = Convert.ToDecimal(this.DiscountTotalHiddenField.Value);
                    //int _tax = Convert.ToInt32(new CompanyConfig().GetSingle(CompanyConfigure.POSTaxCharge).SetValue);
                    _posTrRetailHd.PPNPercentage = 0;
                    _posTrRetailHd.PPNForex = Convert.ToDecimal(this.TaxHiddenField.Value);//Convert.ToDecimal(this.TaxLabel.Text);
                    _posTrRetailHd.PB1Forex = Convert.ToDecimal(this.pb1HiddenField.Value);
                    _posTrRetailHd.OtherForex = 0;
                    _posTrRetailHd.TotalForex = Convert.ToDecimal(this.TotalLabel.Text);
                    //_posTrRetailHd.Remark = "";
                    _posTrRetailHd.FakturPajakNmbr = "";
                    _posTrRetailHd.FakturPajakDate = _now;
                    _posTrRetailHd.FakturPajakRate = 0;
                    _posTrRetailHd.UserPrep = this.CashierLabel.Text;
                    _posTrRetailHd.DatePrep = _now;
                    _posTrRetailHd.ServiceChargeAmount = Convert.ToDecimal(this.ServiceChargeLabel.Text);
                    _posTrRetailHd.DoneSettlement = POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet);
                    _posTrRetailHd.DeliveryStatus = POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered);

                    String _result = _posRetailBL.SaveAndHold(_posTrRetailHd, this.boughtItems.Value);

                    if (_result == "")
                    {
                        this.ClearLabel();
                        this.ClearData();

                        this.SetDefaultData();

                        this.perulanganDataDibeli.Text = "";
                        this.ClearProduct();
                        this.ProductImage.Attributes.Remove("src");
                        this.ProductNameLabel.Text = "";
                    }
                    //this.TransRefLabel.Text = _resultSplit[0];
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ClearAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.TransNmbrTextBox.Text != "")
                {
                    this.ChangeVisiblePanel(2);
                    //this.FormPanel.Visible = false;
                    //this.ReasonListPanel.Visible = true;
                    //this.ReasonListRepeater.DataSource = this._reasonBL.GetList();
                    //this.ReasonListRepeater.DataBind();
                }
                else
                {
                    this.ClearLabel();
                    this.ClearData();
                    this.SetDefaultData();
                    this.boughtItems.Value = "";
                    this.showItem();
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void SendToCashierButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ReferenceNoTextBox.Text != "")
                {
                    if (this.boughtItems.Value != "")
                    {
                        String _result = "";

                        if (this.TransNmbrTextBox.Text != "")
                        {
                            POSTrRetailHd _posTrRetailHd = this._posRetailBL.GetSingle(this.TransNmbrTextBox.Text);
                            _posTrRetailHd.TransType = POSTransTypeDataMapper.GetTransType(POSTransType.Retail);
                            _posTrRetailHd.TransDate = DateTime.Now;
                            _posTrRetailHd.ReferenceNo = this.ReferenceNoTextBox.Text;
                            _posTrRetailHd.MemberID = this.MemberBarcodeTextBox.Text;
                            _posTrRetailHd.CustName = this.CustNameTextBox.Text;
                            _posTrRetailHd.CustPhone = this.CustPhoneTextBox.Text;
                            _posTrRetailHd.OperatorID = this.CashierLabel.Text;
                            _posTrRetailHd.TableRefNmbr = "";
                            if (this.ReferenceNoHiddenField.Value == "")
                            {
                                _posTrRetailHd.Status = POSTrRetailDataMapper.GetStatus(POSTrRetailStatus.SendToCashier);
                            }
                            else
                            {
                                _posTrRetailHd.Status = POSTrRetailDataMapper.GetStatus(POSTrRetailStatus.DeliveryOrder);
                            }
                            _posTrRetailHd.IsVOID = false;
                            _posTrRetailHd.CurrCode = this.CurrencyLabel.Text;
                            _posTrRetailHd.ForexRate = 1;
                            _posTrRetailHd.SubTotalForex = Convert.ToDecimal(this.SubTotalHiddenField.Value);
                            _posTrRetailHd.ServiceChargeAmount = Convert.ToDecimal(this.ServiceChargeLabel.Text);
                            _posTrRetailHd.DiscPercentage = 0;
                            _posTrRetailHd.DiscForex = Convert.ToDecimal(this.DiscountTotalHiddenField.Value);
                            //int _tax = Convert.ToInt32(new CompanyConfig().GetSingle(CompanyConfigure.POSTaxCharge).SetValue);
                            _posTrRetailHd.PPNPercentage = 0;
                            _posTrRetailHd.PPNForex = Convert.ToDecimal(this.TaxHiddenField.Value);//Convert.ToDecimal(this.TaxLabel.Text);
                            _posTrRetailHd.PB1Forex = Convert.ToDecimal(this.pb1HiddenField.Value);
                            _posTrRetailHd.OtherForex = 0;
                            _posTrRetailHd.TotalForex = Convert.ToDecimal(this.GrandTotalLabel.Text);
                            //_posTrRetailHd.Remark = "";
                            _posTrRetailHd.FakturPajakNmbr = "";
                            _posTrRetailHd.FakturPajakDate = DateTime.Now;
                            _posTrRetailHd.FakturPajakRate = 0;
                            _posTrRetailHd.UserPrep = this.CashierLabel.Text;
                            _posTrRetailHd.DatePrep = DateTime.Now;
                            _posTrRetailHd.DeliveryStatus = POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered);
                            _posTrRetailHd.DoneSettlement = POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet);
                            //_posTrRetailHd.DeliveryOrderReff = this.ReferenceNoTextBox.Text;
                            _result = this._posRetailBL.SendToCashier(_posTrRetailHd, this.boughtItems.Value);
                            // if DO then back to Page DO
                            if (this.ReferenceNoTextBox.Text.Substring(0, 2) == "DO" & _result == "")
                            {
                                Response.Redirect("../DeliveryOrder/ListDeliveryOrder.aspx" + "?" + "referenceNo=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value, ApplicationConfig.EncryptionKey)));
                            }
                        }
                        else
                        {
                            if (this.ReferenceNoHiddenField.Value == "")
                            {
                                POSTrRetailHd _posTrRetailHd = new POSTrRetailHd();
                                _posTrRetailHd.TransType = POSTransTypeDataMapper.GetTransType(POSTransType.Retail);
                                _posTrRetailHd.TransDate = DateTime.Now;
                                _posTrRetailHd.ReferenceNo = this.ReferenceNoTextBox.Text;
                                _posTrRetailHd.MemberID = this.MemberBarcodeTextBox.Text;
                                _posTrRetailHd.CustName = this.CustNameTextBox.Text;
                                _posTrRetailHd.CustPhone = this.CustPhoneTextBox.Text;
                                _posTrRetailHd.OperatorID = this.CashierLabel.Text;
                                _posTrRetailHd.TableRefNmbr = "";


                                //Status Value For S.To.Cashier = 2
                                _posTrRetailHd.Status = POSTrRetailDataMapper.GetStatus(POSTrRetailStatus.SendToCashier);
                                _posTrRetailHd.IsVOID = false;
                                _posTrRetailHd.CurrCode = this.CurrencyLabel.Text;
                                _posTrRetailHd.ForexRate = 1;
                                _posTrRetailHd.SubTotalForex = Convert.ToDecimal(this.SubTotalHiddenField.Value);
                                _posTrRetailHd.ServiceChargeAmount = Convert.ToDecimal(this.ServiceChargeLabel.Text);
                                _posTrRetailHd.DiscPercentage = 0;
                                _posTrRetailHd.DiscForex = Convert.ToDecimal(this.DiscountTotalHiddenField.Value);
                                //int _tax = Convert.ToInt32(new CompanyConfig().GetSingle(CompanyConfigure.POSTaxCharge).SetValue);
                                _posTrRetailHd.PPNPercentage = 0;
                                _posTrRetailHd.PPNForex = Convert.ToDecimal(this.TaxHiddenField.Value);//Convert.ToDecimal(this.TaxLabel.Text);
                                _posTrRetailHd.PB1Forex = Convert.ToDecimal(this.pb1HiddenField.Value);
                                _posTrRetailHd.OtherForex = 0;
                                _posTrRetailHd.TotalForex = Convert.ToDecimal(this.GrandTotalLabel.Text);
                                //_posTrRetailHd.Remark = "";
                                _posTrRetailHd.FakturPajakNmbr = "";
                                _posTrRetailHd.FakturPajakDate = DateTime.Now;
                                _posTrRetailHd.FakturPajakRate = 0;
                                _posTrRetailHd.UserPrep = this.CashierLabel.Text;
                                _posTrRetailHd.DatePrep = DateTime.Now;
                                _posTrRetailHd.DeliveryStatus = POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered);
                                _posTrRetailHd.DoneSettlement = POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet);

                                _result = this._posRetailBL.SendToCashier(_posTrRetailHd, this.boughtItems.Value);
                            }
                            else
                            {
                                POSTrRetailHd _posTrRetailHd = new POSTrRetailHd();

                                _posTrRetailHd.TransType = POSTransTypeDataMapper.GetTransType(POSTransType.Retail);
                                _posTrRetailHd.TransDate = DateTime.Now;
                                _posTrRetailHd.ReferenceNo = this.ReferenceNoTextBox.Text;
                                _posTrRetailHd.MemberID = this.MemberBarcodeTextBox.Text;
                                _posTrRetailHd.CustName = this.CustNameTextBox.Text;
                                _posTrRetailHd.CustPhone = this.CustPhoneTextBox.Text;
                                _posTrRetailHd.OperatorID = this.CashierLabel.Text;
                                _posTrRetailHd.TableRefNmbr = "";

                                //Status Value For DELIVERY ORDER = 0
                                _posTrRetailHd.Status = POSTrRetailDataMapper.GetStatus(POSTrRetailStatus.DeliveryOrder);
                                _posTrRetailHd.IsVOID = false;
                                _posTrRetailHd.CurrCode = this.CurrencyLabel.Text;
                                _posTrRetailHd.ForexRate = 1;
                                _posTrRetailHd.SubTotalForex = Convert.ToDecimal(this.SubTotalHiddenField.Value);
                                _posTrRetailHd.ServiceChargeAmount = Convert.ToDecimal(this.ServiceChargeLabel.Text);
                                _posTrRetailHd.DiscPercentage = 0;
                                _posTrRetailHd.DiscForex = Convert.ToDecimal(this.DiscountTotalHiddenField.Value);
                                //int _tax = Convert.ToInt32(new CompanyConfig().GetSingle(CompanyConfigure.POSTaxCharge).SetValue);
                                _posTrRetailHd.PPNPercentage = 0;
                                _posTrRetailHd.PPNForex = Convert.ToDecimal(this.TaxHiddenField.Value);//Convert.ToDecimal(this.TaxLabel.Text);
                                _posTrRetailHd.PB1Forex = Convert.ToDecimal(this.pb1HiddenField.Value);
                                _posTrRetailHd.OtherForex = 0;
                                _posTrRetailHd.TotalForex = Convert.ToDecimal(this.GrandTotalLabel.Text);
                                //_posTrRetailHd.Remark = "";
                                _posTrRetailHd.FakturPajakNmbr = "";
                                _posTrRetailHd.FakturPajakDate = DateTime.Now;
                                _posTrRetailHd.FakturPajakRate = 0;
                                _posTrRetailHd.UserPrep = this.CashierLabel.Text;
                                _posTrRetailHd.DatePrep = DateTime.Now;
                                _posTrRetailHd.DeliveryStatus = POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered);
                                _posTrRetailHd.DoneSettlement = POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet);
                                if (this.ReferenceNoTextBox.Text.Substring(0, 2) == "DO")
                                {
                                    _posTrRetailHd.DeliveryOrderReff = this.ReferenceNoTextBox.Text;

                                    POSTrDeliveryOrderRef _posTrDeliveryOrderRef = new POSTrDeliveryOrderRef();
                                    _posTrDeliveryOrderRef.ReferenceNo = this.ReferenceNoTextBox.Text;
                                    _posTrDeliveryOrderRef.TransType = POSTransTypeDataMapper.GetTransType(POSTransType.Retail);

                                    _result = this._posRetailBL.SendToCashierForDeliveryOrder(_posTrRetailHd, this.boughtItems.Value, _posTrDeliveryOrderRef);

                                    if (_result == "")
                                        Response.Redirect("../DeliveryOrder/ListDeliveryOrder.aspx" + "?" + "referenceNo=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value, ApplicationConfig.EncryptionKey)));
                                }
                                else
                                {
                                    _result = this._posRetailBL.SendToCashier(_posTrRetailHd, this.boughtItems.Value);
                                }
                            }
                        }

                        if (_result == "")
                        {
                            this.ClearLabel();
                            this.ClearData();
                            this.SetDefaultData();
                            this.perulanganDataDibeli.Text = "";
                            this.ClearProduct();
                            this.ProductImage.Attributes.Remove("src");
                            this.ProductNameLabel.Text = "";
                        }
                    }
                }
                else
                {
                    this.WarningLabel.Text = "Reference Number Must be Filled";
                }
            }
            catch (ThreadAbortException) { throw; }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ReasonListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                POSMsReason _temp = (POSMsReason)e.Item.DataItem;

                String _reasonCode = _temp.ReasonCode.ToString();

                ImageButton _pickReasonButton = (ImageButton)e.Item.FindControl("PickReasonImageButton");
                _pickReasonButton.Attributes.Add("OnClick", "return CancelPaid();");
                _pickReasonButton.CommandName = "PickButton";
                _pickReasonButton.CommandArgument = _reasonCode;

                Literal _reasonLiteral = (Literal)e.Item.FindControl("ReasonLiteral");
                _reasonLiteral.Text = HttpUtility.HtmlEncode(_temp.ReasonName);
            }
        }

        protected void ReasonListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToString() == "PickButton")
                {
                    Boolean _result = false;
                    _result = this._posRetailBL.SetVOID(this.TransNmbrTextBox.Text, e.CommandArgument.ToString(), true);
                    if (_result == true)
                    {
                        if (this.ReferenceNoHiddenField.Value != "")
                        {
                            Response.Redirect("../DeliveryOrder/ListDeliveryOrder.aspx" + "?" + "referenceNo=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value, ApplicationConfig.EncryptionKey)));
                        }
                        else
                        {
                            //this.ClearLabelAndData();
                            //this.ComposeBoughtItem();
                            //this.JoinJobOrderImageButton.Enabled = true;
                            //this.IsEditedHiddenField.Value = "0";
                            //this.FormPanel.Visible = true;
                            //this.ReasonListPanel.Visible = false;
                            this.ChangeVisiblePanel(0);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Process Cancel Success.');", true);
                        }
                    }
                    else
                    {
                        this.ChangeVisiblePanel(0);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Process Cancel Failed');", true);
                        //this.FormPanel.Visible = true;
                        //this.ReasonListPanel.Visible = false;
                    }
                }
            }
            catch (ThreadAbortException) { throw; }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void Back2ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ChangeVisiblePanel(0);
            //this.FormPanel.Visible = true;
            //this.ReasonListPanel.Visible = false;
        }

        protected void errorhandler(Exception ex)
        {
            //ErrorHandler.Record(ex, EventLogEntryType.Error);
            String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "RETAIL");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
        }

        protected void ChangeVisiblePanel(Byte _prmValue)
        {
            if (_prmValue == 0)
            {
                this.FormPanel.Visible = true;
                this.ReasonListPanel.Visible = false;
                this.PasswordPanel.Visible = false;
            }
            else if (_prmValue == 1)
            {
                this.FormPanel.Visible = false;
                this.ReasonListPanel.Visible = true;
                this.PasswordPanel.Visible = false;
            }
            else if (_prmValue == 2)
            {
                this.FormPanel.Visible = true;
                this.ReasonListPanel.Visible = false;
                this.PasswordPanel.Visible = true;
            }
        }

        protected void OKButton_Click(object sender, EventArgs e)
        {
            try
            {
                String _password = this._pOSConfigurationBL.GetSingle("POSCancelPassword").SetValue;
                if (_password.ToLower() == this.PasswordTextBox.Text.ToLower())
                {
                    this.ChangeVisiblePanel(1);
                    this.ReasonListRepeater.DataSource = this._reasonBL.GetList();
                    this.ReasonListRepeater.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Incorrect Password.');", true);
                }
                this.PasswordTextBox.Text = "";
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void NewMemberButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Registration/Registration.aspx");
        }
    }
}