using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockServiceIn
{
    public partial class StockServiceInDetailAdd : StockServiceInBase
    {
        private StockServiceInBL _stockServiceInBL = new StockServiceInBL();
        private ProductBL _productBL = new ProductBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
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

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                //this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.ClearLabel();
                this.SetAttribute();
                this.ShowLocation();
                this.ClearData();
            }

            if (this.tempProductCode.Value != this.ProductPicker1.ProductCode)
            {
                this.tempProductCode.Value = this.ProductPicker1.ProductCode;

                if (this.ProductPicker1.ProductCode != "")
                {
                    this.UnitTextBox.Text = this._productBL.GetUnitNameByCode(this.ProductPicker1.ProductCode);
                }
                else
                {
                    this.UnitTextBox.Text = "";
                }
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
            this.DateTextBox.Attributes.Add("ReadOnly", "True"); 
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ShowLocation()
        {
            string _tempWarehouseCode = this._stockServiceInBL.GetWarehouseCodeByCode(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.LocationDropDownList.Items.Clear();
            this.LocationDropDownList.DataTextField = "WLocationName";
            this.LocationDropDownList.DataValueField = "WLocationCode";
            this.LocationDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(_tempWarehouseCode);
            this.LocationDropDownList.DataBind();
            this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearData()
        {
            this.ClearLabel();
            this.DateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            
            this.ProductPicker1.ProductCode = "";
            this.ProductPicker1.ProductName = "";
            this.LocationDropDownList.SelectedValue = "null";
            //this.QtyTextBox.Text = "0";
            this.UnitTextBox.Text = "";
            this.RemarkTextBox.Text = "";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            DateTime _now = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            if (new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, _now.Hour, _now.Minute, _now.Second) >= _now)
            {
                bool _resultImei = this._stockServiceInBL.CekImeiSTCServiceInDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), this.IMEINoTextBox.Text.Trim());
                if (_resultImei == true)
                {
                    STCServiceInDt _sTCServiceInDt = new STCServiceInDt();
                    _sTCServiceInDt.TransNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                    _sTCServiceInDt.ImeiNo = this.IMEINoTextBox.Text.Trim();
                    _sTCServiceInDt.ProductCode = this.ProductPicker1.ProductCode.Trim();
                    _sTCServiceInDt.LocationCode = this.LocationDropDownList.SelectedValue;
                    _sTCServiceInDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
                    _sTCServiceInDt.Unit = this._productBL.GetUnitCodeByCode(this.ProductPicker1.ProductCode.Trim());
                    _sTCServiceInDt.EstReturnDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    _sTCServiceInDt.FgGaransi = this.FgGaransiCheckBox.Checked;
                    _sTCServiceInDt.FgSegelOK = this.FgSegelOKCheckBox.Checked;
                    _sTCServiceInDt.Remark = this.RemarkTextBox.Text.Trim();
                    _sTCServiceInDt.QtyOut = 0;

                    bool _result = this._stockServiceInBL.AddSTCServiceInDt(_sTCServiceInDt);

                    if (_result == true)
                    {
                        Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                    }
                    else
                    {
                        this.WarningLabel.Text = "Failed Add Data";
                    }
                }
                else
                {
                    this.WarningLabel.Text = "IMEI Already Exist";
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
            this.ClearData();
        }
    }
}