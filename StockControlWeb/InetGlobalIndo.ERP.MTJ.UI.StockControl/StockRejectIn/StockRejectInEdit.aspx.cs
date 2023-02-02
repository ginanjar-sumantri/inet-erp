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

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockRejectIn
{
    public partial class StockRejectInEdit : StockRejectInBase
    {
        private StockRejectInBL _stockRejectInBL = new StockRejectInBL();
        private StockRejectOutBL _stockRejectOutBL = new StockRejectOutBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private CustomerBL _cust = new CustomerBL();
        private SupplierBL _supp = new SupplierBL();
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

                this.ShowTransReff();
                this.ShowWarehouse();

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.SupplierTextBox.Attributes.Add("ReadOnly", "True");
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
            this.SubledDropDownList.DataSource = this._cust.GetListCustomerForDDL();
            this.SubledDropDownList.DataBind();
            this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowSupp()
        {
            this.SubledDropDownList.Items.Clear();
            this.SubledDropDownList.DataTextField = "SuppName";
            this.SubledDropDownList.DataValueField = "SuppCode";
            this.SubledDropDownList.DataSource = this._supp.GetListDDLSupp();
            this.SubledDropDownList.DataBind();
            this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        //public void ShowSupplierDDL()
        //{
        //    this.SupplierDropDownList.Items.Clear();
        //    this.SupplierDropDownList.DataTextField = "SuppName";
        //    this.SupplierDropDownList.DataValueField = "SuppCode";
        //    this.SupplierDropDownList.DataSource = this._supp.GetListDDLSupp();
        //    this.SupplierDropDownList.DataBind();
        //    this.SupplierDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        public void ShowTransReff()
        {
            this.TransReffDropDownList.Items.Clear();
            this.TransReffDropDownList.DataTextField = "FileNmbr";
            this.TransReffDropDownList.DataValueField = "TransNmbr";
            this.TransReffDropDownList.DataSource = this._stockRejectOutBL.GetListRejNoRejectOutDDL();
            this.TransReffDropDownList.DataBind();
            this.TransReffDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            STCRejectInHd _stcRejectInHd = this._stockRejectInBL.GetSingleSTCRejectInHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNoTextBox.Text = _stcRejectInHd.TransNmbr;
            this.FileNoTextBox.Text = _stcRejectInHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_stcRejectInHd.TransDate);
            this.TransReffDropDownList.SelectedValue = _stcRejectInHd.TransReff;
            this.SupplierTextBox.Text = _supp.GetSuppNameByCode(_stcRejectInHd.SuppCode);
            this.WarehouseDropDownList.SelectedValue = _stcRejectInHd.WrhsCode;
            if (_stcRejectInHd.WrhsFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                this.SubledDropDownList.Enabled = true;
                this.ShowCust();
                this.SubledDropDownList.SelectedValue = _stcRejectInHd.WrhsSubLed;
            }
            else if (_stcRejectInHd.WrhsFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
            {
                this.SubledDropDownList.Enabled = true;
                this.ShowSupp();
                this.SubledDropDownList.SelectedValue = _stcRejectInHd.WrhsSubLed;
            }
            else
            {
                this.SubledDropDownList.Enabled = false;
                this.SubledDropDownList.SelectedValue = "null";
            }
            this.RemarkTextBox.Text = _stcRejectInHd.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCRejectInHd _stcRejectInHd = this._stockRejectInBL.GetSingleSTCRejectInHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcRejectInHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcRejectInHd.TransReff = this.TransReffDropDownList.SelectedValue;
            _stcRejectInHd.SuppCode = _stockRejectOutBL.GetSuppCodeByCode(this.TransReffDropDownList.SelectedValue);
            _stcRejectInHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                _stcRejectInHd.WrhsFgSubLed = Convert.ToChar(WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled));
                this.SubledDropDownList.SelectedValue = "null";
            }
            else if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                _stcRejectInHd.WrhsFgSubLed = Convert.ToChar(WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer));
            }
            else if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                _stcRejectInHd.WrhsFgSubLed = Convert.ToChar(WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer));
            }
            if (this.SubledDropDownList.SelectedValue != "null")
            {
                _stcRejectInHd.WrhsSubLed = this.SubledDropDownList.SelectedValue;
            }
            else
            {
                _stcRejectInHd.WrhsSubLed = "";
            }
            _stcRejectInHd.Remark = this.RemarkTextBox.Text;
            _stcRejectInHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcRejectInHd.DatePrep = DateTime.Now;

            bool _result = this._stockRejectInBL.EditSTCRejectInHd(_stcRejectInHd);

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
            STCRejectInHd _stcRejectInHd = this._stockRejectInBL.GetSingleSTCRejectInHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcRejectInHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcRejectInHd.TransReff = this.TransReffDropDownList.SelectedValue;
            _stcRejectInHd.SuppCode = _stockRejectOutBL.GetSuppCodeByCode(this.TransReffDropDownList.SelectedValue);
            _stcRejectInHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                _stcRejectInHd.WrhsFgSubLed = Convert.ToChar(WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled));
            }
            else if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                _stcRejectInHd.WrhsFgSubLed = Convert.ToChar(WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer));
            }
            else if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                _stcRejectInHd.WrhsFgSubLed = Convert.ToChar(WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer));
            }
            if (this.SubledDropDownList.SelectedValue != "null")
            {
                _stcRejectInHd.WrhsSubLed = this.SubledDropDownList.SelectedValue;
            }
            else
            {
                _stcRejectInHd.WrhsSubLed = "";
            }
            _stcRejectInHd.Remark = this.RemarkTextBox.Text;
            _stcRejectInHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcRejectInHd.DatePrep = DateTime.Now;

            bool _result = this._stockRejectInBL.EditSTCRejectInHd(_stcRejectInHd);

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

        protected void TransReffDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.TransReffDropDownList.SelectedValue != "null")
            {
                this.SupplierTextBox.Text = _supp.GetSuppNameByCode(_stockRejectOutBL.GetSuppCodeByCode(this.TransReffDropDownList.SelectedValue));
            }
            else
            {
                this.SupplierTextBox.Text = "";
            }
        }
    }
}