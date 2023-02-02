using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockServiceIn
{
    public partial class StockServiceInDetailEdit : StockServiceInBase
    {
        private StockServiceInBL _stockServiceInBL = new StockServiceInBL();
        private ProductBL _productBL = new ProductBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private UnitBL _unitBL = new UnitBL();
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

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void SetAttribute()
        {
            this.QtyTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
            this.QtyTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.QtyTextBox.ClientID + "," + this.QtyTextBox.ClientID + ",500" + ");");
            this.QtyTextBox.Attributes.Add("onkeydown", "if(event.keyCode==13){return enterQty();}");
            this.UnitTextBox.Attributes.Add("ReadOnly", "true");
            this.DateTextBox.Attributes.Add("ReadOnly", "true"); 
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowData()
        {
            //STCServiceInDt _sTCServiceInDt = this._stockServiceInBL.GetSingleSTCServiceInDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey));
            STCServiceInDt _sTCServiceInDt = this._stockServiceInBL.GetSingleSTCServiceInDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._imeiNoKey), ApplicationConfig.EncryptionKey));
            this.IMEINoTextBox.Text = _sTCServiceInDt.ImeiNo;
            this.ProductTextBox.Text = this._productBL.GetProductNameByCode(_sTCServiceInDt.ProductCode);
            this.LocationTextBox.Text = this._warehouseBL.GetWarehouseLocationNameByCode(_sTCServiceInDt.LocationCode);
            this.QtyTextBox.Text = (_sTCServiceInDt.Qty == 0) ? "0" : _sTCServiceInDt.Qty.ToString("###");
            this.UnitTextBox.Text = this._unitBL.GetUnitNameByCode(_sTCServiceInDt.Unit);
            this.DateTextBox.Text = DateFormMapper.GetValue(_sTCServiceInDt.EstReturnDate);
            this.FgGaransiCheckBox.Checked = _sTCServiceInDt.FgGaransi;
            this.FgSegelOKCheckBox.Checked = Convert.ToBoolean(_sTCServiceInDt.FgSegelOK);
            this.RemarkTextBox.Text = _sTCServiceInDt.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            DateTime _now = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            if (new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, _now.Hour, _now.Minute, _now.Second) >= _now)
            {
                //STCServiceInDt _sTCServiceInDt = this._stockServiceInBL.GetSingleSTCServiceInDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey));
                STCServiceInDt _sTCServiceInDt = this._stockServiceInBL.GetSingleSTCServiceInDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._imeiNoKey), ApplicationConfig.EncryptionKey));

                _sTCServiceInDt.ImeiNo = this.IMEINoTextBox.Text.Trim();
                _sTCServiceInDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
                _sTCServiceInDt.EstReturnDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                _sTCServiceInDt.FgGaransi = this.FgGaransiCheckBox.Checked;
                _sTCServiceInDt.FgSegelOK = this.FgSegelOKCheckBox.Checked;
                _sTCServiceInDt.Remark = this.RemarkTextBox.Text;

                bool _result = this._stockServiceInBL.EditSTCServiceInDt(_sTCServiceInDt);

                if (_result == true)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.WarningLabel.Text = "Your Failed Edit Data";
                }
            }
            else
            {
                this.WarningLabel.Text = "Estimate Return date must more than Now.";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }
    }
}