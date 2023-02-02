using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockTransferExternalRR
{
    public partial class StockTransferExternalRRDetailAdd : StockTransferExternalRRBase
    {
        private StockTransferExternalRRBL _stcTransInBL = new StockTransferExternalRRBL();
        private StockTransferExternalSJBL _stcTransOutBL = new StockTransferExternalSJBL();
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

                //this.ShowProduct();
                this.ShowLocation();

                this.SetAttribute();
                this.ClearData();
            }


            if (this.tempProductCode.Value != this.ProductPicker1.ProductCode)
            {
                this.tempProductCode.Value = this.ProductPicker1.ProductCode;

                if (ProductPicker1.ProductCode != "")
                {
                    this.ShowLocationSrc();
                }
                else
                {
                    this.LocationSrcDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                    this.LocationSrcDropDownList.SelectedValue = "null";
                }
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.QtyLossTextBox.Attributes.Add("ReadOnly", "True");
            this.UnitTextBox.Attributes.Add("ReadOnly", "True");
            this.QtySJTextBox.Attributes.Add("ReadOnly", "True");

            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.QtyTextBox.Attributes.Add("OnBlur", "CountQtyLoss(" + this.QtySJTextBox.ClientID + "," + this.QtyTextBox.ClientID + "," + this.QtyLossTextBox.ClientID + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ClearData()
        {
            this.ClearLabel();

            //this.ProductDropDownList.SelectedValue = "null";
            this.ProductPicker1.ProductCode = "";
            this.ProductPicker1.ProductName = "";
            this.LocationSrcDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.LocationSrcDropDownList.SelectedValue = "null";
            this.LocationDropDownList.SelectedValue = "null";
            this.QtyTextBox.Text = "0";
            this.QtySJTextBox.Text = "0";
            this.QtyLossTextBox.Text = "0";
            this.UnitTextBox.Text = "";
            this.RemarkTextBox.Text = "";
        }

        //private void ShowProduct()
        //{
        //    STCTransInHd _stcTransInHd = this._stcTransInBL.GetSingleSTCTransInHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataSource = this._stcTransOutBL.GetListProductFromVSTTransferSJForRR(_stcTransInHd.TransReff);
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        private void ShowLocation()
        {
            STCTransInHd _stcTransInHd = this._stcTransInBL.GetSingleSTCTransInHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.LocationDropDownList.Items.Clear();
            this.LocationDropDownList.DataTextField = "WLocationName";
            this.LocationDropDownList.DataValueField = "WLocationCode";
            this.LocationDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(_stcTransInHd.WrhsDest);
            this.LocationDropDownList.DataBind();
            this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowLocationSrc()
        {
            STCTransInHd _stcTransInHd = this._stcTransInBL.GetSingleSTCTransInHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.LocationSrcDropDownList.Items.Clear();
            this.LocationSrcDropDownList.DataTextField = "WLocationName";
            this.LocationSrcDropDownList.DataValueField = "WLocationCode";
            this.LocationSrcDropDownList.DataSource = this._stcTransOutBL.GetListLocationSrcFromVSTTransferSJForRR(_stcTransInHd.TransReff, this.ProductPicker1.ProductCode );
            this.LocationSrcDropDownList.DataBind();
            this.LocationSrcDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCTransInDt _stcTransInDt = new STCTransInDt();
            STCTransInHd _stcTransInHd = this._stcTransInBL.GetSingleSTCTransInHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            _stcTransInDt.TransNmbr = _transNo;
            _stcTransInDt.ProductCode = this.ProductPicker1.ProductCode ;
            _stcTransInDt.LocationSrc = this.LocationSrcDropDownList.SelectedValue;
            _stcTransInDt.LocationCode = this.LocationDropDownList.SelectedValue;
            _stcTransInDt.Unit = _stcTransOutBL.GetUnitFromVSTTransferSJForRR(_stcTransInHd.TransReff, this.ProductPicker1.ProductCode, this.LocationSrcDropDownList.SelectedValue);
            _stcTransInDt.QtySJ = Convert.ToDecimal(this.QtySJTextBox.Text);
            _stcTransInDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcTransInDt.QtyLoss = Convert.ToDecimal(this.QtyLossTextBox.Text);
            _stcTransInDt.PriceCost = 0;
            _stcTransInDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._stcTransInBL.AddSTCTransInDt(_stcTransInDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
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

        //protected void ProductDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ProductPicker1.ProductCode != "")
        //    {
        //        this.ShowLocationSrc();
        //    }
        //    else
        //    {
        //        this.LocationSrcDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //        this.LocationSrcDropDownList.SelectedValue = "null";
        //    }
        //}

        protected void LocationSrcDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LocationSrcDropDownList.SelectedValue != "null")
            {
                STCTransInHd _stcTransInHd = this._stcTransInBL.GetSingleSTCTransInHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                this.QtySJTextBox.Text = _stcTransOutBL.GetQtyFromVSTTransferSJForRR(_stcTransInHd.TransReff, this.ProductPicker1.ProductCode, this.LocationSrcDropDownList.SelectedValue).ToString("#,##0.##");
                this.QtyTextBox.Text = this.QtySJTextBox.Text;
                decimal _qtyLoss = Convert.ToDecimal(this.QtySJTextBox.Text) - Convert.ToDecimal(this.QtyTextBox.Text);
                this.QtyLossTextBox.Text = (_qtyLoss == 0) ? "0" : _qtyLoss.ToString("#,##0.##");
                this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_stcTransOutBL.GetUnitFromVSTTransferSJForRR(_stcTransInHd.TransReff, this.ProductPicker1.ProductCode, this.LocationSrcDropDownList.SelectedValue));
            }
            else
            {
                this.QtySJTextBox.Text = "0";
                this.QtyTextBox.Text = "0";
                this.QtyLossTextBox.Text = "0";
                this.UnitTextBox.Text = "";
            }
        }
    }
}