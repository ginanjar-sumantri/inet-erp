using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockIssueFixedAsset
{
    public partial class StockIssueFAEdit : StockIssueFixedAssetBase
    {
        private StockIssueFixedAssetBL _stockIssueFABL = new StockIssueFixedAssetBL();
        private StockIssueRequestFABL _stockIssueReqFABL = new StockIssueRequestFABL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private CustomerBL _custBL = new CustomerBL();
        private SupplierBL _suppBL = new SupplierBL();
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
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ShowWarehouse();
                this.ShowReqNo();
                
                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();                
            }
        }

        private void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowReqNo()
        {
            this.ReqNoDropDownList.Items.Clear();
            this.ReqNoDropDownList.DataTextField = "FileNmbr";
            this.ReqNoDropDownList.DataValueField = "TransNmbr";
            this.ReqNoDropDownList.DataSource = this._stockIssueReqFABL.GetListReqNoForDDLIssueFA();
            this.ReqNoDropDownList.DataBind();
            this.ReqNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowWarehouse()
        {
            this.WarehouseDropDownList.Items.Clear();
            this.WarehouseDropDownList.DataTextField = "WrhsName";
            this.WarehouseDropDownList.DataValueField = "WrhsCode";
            this.WarehouseDropDownList.DataSource = this._warehouseBL.GetListForDDLActive();
            this.WarehouseDropDownList.DataBind();
            this.WarehouseDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCust()
        {
            this.SubledDropDownList.Items.Clear();
            this.SubledDropDownList.DataTextField = "CustName";
            this.SubledDropDownList.DataValueField = "CustCode";
            this.SubledDropDownList.DataSource = this._custBL.GetListCustomerForDDL();
            this.SubledDropDownList.DataBind();
            this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowSupp()
        {
            this.SubledDropDownList.Items.Clear();
            this.SubledDropDownList.DataTextField = "SuppName";
            this.SubledDropDownList.DataValueField = "SuppCode";
            this.SubledDropDownList.DataSource = this._suppBL.GetListDDLSupp();
            this.SubledDropDownList.DataBind();
            this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            STCIssueToFAHd _stcIssueToFAHd = this._stockIssueFABL.GetSingleSTCIssueToFAHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNoTextBox.Text = _stcIssueToFAHd.TransNmbr;
            this.FileNoTextBox.Text = _stcIssueToFAHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_stcIssueToFAHd.TransDate);
            this.WarehouseDropDownList.SelectedValue = _stcIssueToFAHd.WrhsCode;
            if (_stcIssueToFAHd.WrhsFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                this.SubledDropDownList.Enabled = true;
                this.ShowCust();
                this.SubledDropDownList.SelectedValue = _stcIssueToFAHd.WrhsSubLed;
            }
            else if (_stcIssueToFAHd.WrhsFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
            {
                this.SubledDropDownList.Enabled = true;
                this.ShowSupp();
                this.SubledDropDownList.SelectedValue = _stcIssueToFAHd.WrhsSubLed;
            }
            else
            {
                this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.SubledDropDownList.Enabled = false;
                this.SubledDropDownList.SelectedValue = "null";
            }
            this.ReqNoDropDownList.SelectedValue = _stcIssueToFAHd.ReqAssetNo;
            this.RequestByTextBox.Text = _stcIssueToFAHd.RequestBy;
            this.RemarkTextBox.Text = _stcIssueToFAHd.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCIssueToFAHd _stcIssueToFAHd = this._stockIssueFABL.GetSingleSTCIssueToFAHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcIssueToFAHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcIssueToFAHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                _stcIssueToFAHd.WrhsFgSubLed = WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled);
            }
            else if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
            {
                _stcIssueToFAHd.WrhsFgSubLed = WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier);
            }
            else if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                _stcIssueToFAHd.WrhsFgSubLed = WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer);
            }
            if (this.SubledDropDownList.SelectedValue != "null")
            {
                _stcIssueToFAHd.WrhsSubLed = this.SubledDropDownList.SelectedValue;
            }
            else
            {
                _stcIssueToFAHd.WrhsSubLed = "";
            }
            _stcIssueToFAHd.ReqAssetNo = this.ReqNoDropDownList.SelectedValue;
            _stcIssueToFAHd.RequestBy = this.RequestByTextBox.Text;
            _stcIssueToFAHd.Remark = this.RemarkTextBox.Text;
            _stcIssueToFAHd.EditBy = HttpContext.Current.User.Identity.Name;
            _stcIssueToFAHd.EditDate = DateTime.Now;

            bool _result = this._stockIssueFABL.EditSTCIssueToFAHd(_stcIssueToFAHd);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            STCIssueToFAHd _stcIssueToFAHd = this._stockIssueFABL.GetSingleSTCIssueToFAHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcIssueToFAHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcIssueToFAHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                _stcIssueToFAHd.WrhsFgSubLed = WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled);
            }
            else if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
            {
                _stcIssueToFAHd.WrhsFgSubLed = WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier);
            }
            else if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                _stcIssueToFAHd.WrhsFgSubLed = WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer);
            }
            if (this.SubledDropDownList.SelectedValue != "null")
            {
                _stcIssueToFAHd.WrhsSubLed = this.SubledDropDownList.SelectedValue;
            }
            else
            {
                _stcIssueToFAHd.WrhsSubLed = "";
            }
            _stcIssueToFAHd.ReqAssetNo = this.ReqNoDropDownList.SelectedValue;
            _stcIssueToFAHd.RequestBy = this.RequestByTextBox.Text;
            _stcIssueToFAHd.Remark = this.RemarkTextBox.Text;
            _stcIssueToFAHd.EditBy = HttpContext.Current.User.Identity.Name;
            _stcIssueToFAHd.EditDate = DateTime.Now;

            bool _result = this._stockIssueFABL.EditSTCIssueToFAHd(_stcIssueToFAHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void WarehouseDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.SubledDropDownList.Enabled = false;
            }
            else
            {
                this.SubledDropDownList.Enabled = true;
                if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.ShowCust();
                }
                else if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.ShowSupp();
                }
            }
        }
    }
}