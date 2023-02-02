using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl
{
    public partial class ProductTypeDetailView : ProductTypeBase
    {
        private ProductBL _prodTypeBL = new ProductBL();
        private AccountBL _accBL = new AccountBL();
        private PermissionBL _permBL = new PermissionBL();

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

                this.ShowData();
                this.SetButtonPermission();
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

        private void ShowData()
        {
            MsProductTypeDt _msProductTypeDt = this._prodTypeBL.GetSingleMsProductTypeDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToByte(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._wrhsKey), ApplicationConfig.EncryptionKey)));

            this.WrhsTypeTextBox.Text =  WarehouseDataMapper.GetWrhsType(_msProductTypeDt.WrhsType);
            this.AccCOGSName.Text = _accBL.GetAccountNameByCode(_msProductTypeDt.AccCOGS);
            this.AccCOGSTextBox.Text = (_msProductTypeDt.AccCOGS == "null") ? "" : _msProductTypeDt.AccCOGS;
            this.AccInventName.Text = _accBL.GetAccountNameByCode(_msProductTypeDt.AccInvent);
            this.AccInventTextBox.Text = (_msProductTypeDt.AccInvent == "null") ? "" : _msProductTypeDt.AccInvent;
            this.AccSalesName.Text = _accBL.GetAccountNameByCode(_msProductTypeDt.AccSales);
            this.AccSalesTextBox.Text = (_msProductTypeDt.AccSales == "null") ? "" : _msProductTypeDt.AccSales;
            this.AccTransitSJName.Text = _accBL.GetAccountNameByCode(_msProductTypeDt.AccTransitSJ);
            this.AccTransitSJTextBox.Text = (_msProductTypeDt.AccTransitSJ == "null") ? "" : _msProductTypeDt.AccTransitSJ;
            this.AccTransitWrhsName.Text = _accBL.GetAccountNameByCode(_msProductTypeDt.AccTransitWrhs);
            this.AccTransitWrhsTextBox.Text = (_msProductTypeDt.AccTransitWrhs == "null") ? "" : _msProductTypeDt.AccTransitWrhs;
            this.AccWIPName.Text = _accBL.GetAccountNameByCode(_msProductTypeDt.AccWIP);
            this.AccWIPTextBox.Text = (_msProductTypeDt.AccWIP == "null") ? "" : _msProductTypeDt.AccWIP;            
            this.AccSReturName.Text = _accBL.GetAccountNameByCode(_msProductTypeDt.AccSRetur);
            this.AccSReturTextBox.Text = (_msProductTypeDt.AccSRetur == "null") ? "" : _msProductTypeDt.AccSRetur;
            this.AccPReturName.Text = _accBL.GetAccountNameByCode(_msProductTypeDt.AccPRetur);
            this.AccPReturTextBox.Text = (_msProductTypeDt.AccPRetur == "null") ? "" : _msProductTypeDt.AccPRetur;
            this.AccTransitRejectName.Text = _accBL.GetAccountNameByCode(_msProductTypeDt.AccTransitReject);
            this.AccTransitRejectTextBox.Text = (_msProductTypeDt.AccTransitReject == "null") ? "" : _msProductTypeDt.AccTransitReject;
            this.AccExpLossName.Text = _accBL.GetAccountNameByCode(_msProductTypeDt.AccExpLoss);
            this.AccExpLossTextBox.Text = (_msProductTypeDt.AccExpLoss == "null") ? "" : _msProductTypeDt.AccExpLoss;
            this.FgActiveCheckBox.Checked = (_msProductTypeDt.FgActive == 'Y') ? true : false;
            this.RemarkTextBox.Text = _msProductTypeDt.Remark;
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._wrhsKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._wrhsKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}