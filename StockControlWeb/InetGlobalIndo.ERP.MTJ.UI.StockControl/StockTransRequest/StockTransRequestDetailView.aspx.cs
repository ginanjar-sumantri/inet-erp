using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockTransRequest
{
    public partial class StockTransRequestDetailView : StockTransRequestBase
    {
        private StockTransRequestBL _stockTransReqBL = new StockTransRequestBL();
        private ProductBL _productBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _imgClose = "close.jpg";

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

                STCTransReqHd _stcRequestHd = this._stockTransReqBL.GetSingleSTCTransReqHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                if (_stcRequestHd.Status != StockTransRequestDataMapper.GetStatus(TransStatus.Posted))
                {
                    this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                }
                else
                {
                    this.EditButton.Visible = false;
                }
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowData();
                this.SetButtonPermission();
            }
        }

        private void SetButtonPermission()
        {
            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }

        }

        private void ShowData()
        {
            STCTransReqDt _stcRequestDt = this._stockTransReqBL.GetSingleSTCTransReqDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey));
            STCTransReqHd _stcRequestHd = this._stockTransReqBL.GetSingleSTCTransReqHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.ProductTextBox.Text = _productBL.GetProductNameByCode(_stcRequestDt.ProductCode);
            this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_stcRequestDt.Unit);
            this.QtyTextBox.Text = (_stcRequestDt.Qty == 0) ? "0" : _stcRequestDt.Qty.ToString("#,##0.##");
            this.RemarkTextBox.Text = _stcRequestDt.Remark;
            this.StatusLabel.Text = StockTransRequestDataMapper.GetStatusTextDetail(_stcRequestDt.DoneClosing);
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._productKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}