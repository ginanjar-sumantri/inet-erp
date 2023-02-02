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
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockRejectOut
{
    public partial class StockRejectOutEdit : StockRejectOutBase
    {
        private StockRejectOutBL _stockRejectOutBL = new StockRejectOutBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private CustomerBL _custBL = new CustomerBL();
        private SupplierBL _suppBL = new SupplierBL();
        private PermissionBL _permBL = new PermissionBL();
        private PurchaseReturBL _purchaseReturBL = new PurchaseReturBL();

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

                this.ShowSupplierDDL();
                this.ShowWarehouse();                

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
                this.ShowPurchase();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
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

        public void ShowSupplierDDL()
        {
            this.SupplierDropDownList.Items.Clear();
            this.SupplierDropDownList.DataTextField = "SuppName";
            this.SupplierDropDownList.DataValueField = "SuppCode";
            this.SupplierDropDownList.DataSource = this._suppBL.GetListDDLSupp();
            this.SupplierDropDownList.DataBind();
            this.SupplierDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowPurchase()
        {
            this.PurchaseReturDropDownList.Items.Clear();
            this.PurchaseReturDropDownList.DataTextField = "RRNo";
            this.PurchaseReturDropDownList.DataValueField = "FileNmbr";
            this.PurchaseReturDropDownList.DataSource = this._purchaseReturBL.GetListPRBySupplierForDDL(this.SupplierDropDownList.SelectedValue);
            this.PurchaseReturDropDownList.DataBind();
            this.PurchaseReturDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            STCRejectOutHd _stcRejectOutHd = this._stockRejectOutBL.GetSingleSTCRejectOutHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNoTextBox.Text = _stcRejectOutHd.TransNmbr;
            this.FileNoTextBox.Text = _stcRejectOutHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_stcRejectOutHd.TransDate);
            this.SupplierDropDownList.SelectedValue = _stcRejectOutHd.SuppCode;
            //this.ShowPurchase();
            this.PurchaseReturDropDownList.SelectedValue = _stcRejectOutHd.PurchaseRetur;
            this.AttnTextBox.Text = _stcRejectOutHd.Attn;
            this.WarehouseDropDownList.SelectedValue = _stcRejectOutHd.WrhsCode;
            if (_stcRejectOutHd.WrhsFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                this.SubledDropDownList.Enabled = true;
                this.ShowCust();
                this.SubledDropDownList.SelectedValue = _stcRejectOutHd.WrhsSubLed;
            }
            else if (_stcRejectOutHd.WrhsFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
            {
                this.SubledDropDownList.Enabled = true;
                this.ShowSupp();
                this.SubledDropDownList.SelectedValue = _stcRejectOutHd.WrhsSubLed;
            }
            else
            {
                this.SubledDropDownList.Enabled = false;
                this.SubledDropDownList.SelectedValue = "null";
            }
            this.RemarkTextBox.Text = _stcRejectOutHd.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCRejectOutHd _stcRejectOutHd = this._stockRejectOutBL.GetSingleSTCRejectOutHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcRejectOutHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcRejectOutHd.SuppCode = this.SupplierDropDownList.SelectedValue;
            _stcRejectOutHd.Attn = this.AttnTextBox.Text;
            _stcRejectOutHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            _stcRejectOutHd.PurchaseRetur = this.PurchaseReturDropDownList.SelectedValue;
            if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                _stcRejectOutHd.WrhsFgSubLed = Convert.ToChar(WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled));
            }
            else if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                _stcRejectOutHd.WrhsFgSubLed = Convert.ToChar(WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer));
            }
            else if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                _stcRejectOutHd.WrhsFgSubLed = Convert.ToChar(WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer));
            }
            if (this.SubledDropDownList.SelectedValue != "null")
            {
                _stcRejectOutHd.WrhsSubLed = this.SubledDropDownList.SelectedValue;
            }
            else
            {
                _stcRejectOutHd.WrhsSubLed = "";
            }
            _stcRejectOutHd.Remark = this.RemarkTextBox.Text;
            _stcRejectOutHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcRejectOutHd.DatePrep = DateTime.Now;

            bool _result = this._stockRejectOutBL.EditSTCRejectOutHd(_stcRejectOutHd);

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
            this.ClearLabel();
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            STCRejectOutHd _stcRejectOutHd = this._stockRejectOutBL.GetSingleSTCRejectOutHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcRejectOutHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcRejectOutHd.SuppCode = this.SupplierDropDownList.SelectedValue;
            _stcRejectOutHd.Attn = this.AttnTextBox.Text;
            _stcRejectOutHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                _stcRejectOutHd.WrhsFgSubLed = Convert.ToChar(WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled));
            }
            else if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                _stcRejectOutHd.WrhsFgSubLed = Convert.ToChar(WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer));
            }
            else if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                _stcRejectOutHd.WrhsFgSubLed = Convert.ToChar(WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer));
            }
            if (this.SubledDropDownList.SelectedValue != "null")
            {
                _stcRejectOutHd.WrhsSubLed = this.SubledDropDownList.SelectedValue;
            }
            else
            {
                _stcRejectOutHd.WrhsSubLed = "";
            }
            _stcRejectOutHd.Remark = this.RemarkTextBox.Text;
            _stcRejectOutHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcRejectOutHd.DatePrep = DateTime.Now;

            bool _result = this._stockRejectOutBL.EditSTCRejectOutHd(_stcRejectOutHd);

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
                this.SubledDropDownList.SelectedValue = "null";
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

        protected void SupplierDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SupplierDropDownList.SelectedValue == "null")
            {
                this.AttnTextBox.Text = "";
            }
            else
            {
                this.AttnTextBox.Text = _suppBL.GetSuppContact(this.SupplierDropDownList.SelectedValue);
            }
            this.ShowPurchase();
        }
    }
}