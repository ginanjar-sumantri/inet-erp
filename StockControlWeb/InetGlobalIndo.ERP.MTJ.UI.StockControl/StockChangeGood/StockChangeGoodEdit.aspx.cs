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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockChangeGood
{
    public partial class StockChangeGoodEdit : StockChangeGoodBase
    {
        private WarehouseBL _wrhsBL = new WarehouseBL();
        private StockChangeGoodBL _stcChangeGoodBL = new StockChangeGoodBL();
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

                this.ShowWarehouseDest();
                this.ShowWarehouseSrc();
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

        public void ShowWarehouseDest()
        {
            this.WrhsDestDropDownList.Items.Clear();
            this.WrhsDestDropDownList.DataTextField = "WrhsName";
            this.WrhsDestDropDownList.DataValueField = "WrhsCode";
            this.WrhsDestDropDownList.DataSource = this._wrhsBL.GetListForDDLActive();
            this.WrhsDestDropDownList.DataBind();
            this.WrhsDestDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowWarehouseSrc()
        {
            this.WrhsSrcDropDownList.Items.Clear();
            this.WrhsSrcDropDownList.DataTextField = "WrhsName";
            this.WrhsSrcDropDownList.DataValueField = "WrhsCode";
            this.WrhsSrcDropDownList.DataSource = this._wrhsBL.GetListForDDLActive();
            this.WrhsSrcDropDownList.DataBind();
            this.WrhsSrcDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCustSrc()
        {
            this.SrcSubledDropDownList.Items.Clear();
            this.SrcSubledDropDownList.DataTextField = "CustName";
            this.SrcSubledDropDownList.DataValueField = "CustCode";
            this.SrcSubledDropDownList.DataSource = this._custBL.GetListCustomerForDDL();
            this.SrcSubledDropDownList.DataBind();
            this.SrcSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCustDest()
        {
            this.DestSubledDropDownList.Items.Clear();
            this.DestSubledDropDownList.DataTextField = "CustName";
            this.DestSubledDropDownList.DataValueField = "CustCode";
            this.DestSubledDropDownList.DataSource = this._custBL.GetListCustomerForDDL();
            this.DestSubledDropDownList.DataBind();
            this.DestSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowSuppSrc()
        {
            this.SrcSubledDropDownList.Items.Clear();
            this.SrcSubledDropDownList.DataTextField = "SuppName";
            this.SrcSubledDropDownList.DataValueField = "SuppCode";
            this.SrcSubledDropDownList.DataSource = this._suppBL.GetListDDLSupp();
            this.SrcSubledDropDownList.DataBind();
            this.SrcSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowSuppDest()
        {
            this.DestSubledDropDownList.Items.Clear();
            this.DestSubledDropDownList.DataTextField = "SuppName";
            this.DestSubledDropDownList.DataValueField = "SuppCode";
            this.DestSubledDropDownList.DataSource = this._suppBL.GetListDDLSupp();
            this.DestSubledDropDownList.DataBind();
            this.DestSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            STCChangeHd _stcChangeGoodHd = this._stcChangeGoodBL.GetSingleSTCChangeHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNoTextBox.Text = _stcChangeGoodHd.TransNmbr;
            this.FileNoTextBox.Text = _stcChangeGoodHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_stcChangeGoodHd.TransDate);
            this.WrhsSrcDropDownList.SelectedValue = _stcChangeGoodHd.WrhsSrc;
            this.WrhsDestDropDownList.SelectedValue = _stcChangeGoodHd.WrhsDest;
            this.OperatorTextBox.Text = _stcChangeGoodHd.Operator;
            this.RemarkTextBox.Text = _stcChangeGoodHd.Remark;
            this.WrhsSrcDropDownList.SelectedValue = _stcChangeGoodHd.WrhsSrc;
            this.WrhsDestDropDownList.SelectedValue = _stcChangeGoodHd.WrhsDest;
            if (_wrhsBL.GetWarehouseFgSubledByCode(this.WrhsSrcDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.SrcSubledDropDownList.Enabled = false;
                this.SrcSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.SrcSubledDropDownList.SelectedValue = "null";
            }
            else
            {
                this.SrcSubledDropDownList.Enabled = true;
                if (_wrhsBL.GetWarehouseFgSubledByCode(this.WrhsSrcDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.ShowCustSrc();
                }
                else if (_wrhsBL.GetWarehouseFgSubledByCode(this.WrhsSrcDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.ShowSuppSrc();
                }
            }
            this.SrcSubledDropDownList.SelectedValue = _stcChangeGoodHd.WrhsSrcSubLed;
            if (_wrhsBL.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.DestSubledDropDownList.Enabled = false;
                this.DestSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.DestSubledDropDownList.SelectedValue = "null";
            }
            else
            {
                this.DestSubledDropDownList.Enabled = true;
                if (_wrhsBL.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.ShowCustDest();
                }
                else if (_wrhsBL.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.ShowSuppDest();
                }
            }
            this.DestSubledDropDownList.SelectedValue = _stcChangeGoodHd.WrhsDestSubLed;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCChangeHd _stcChangeGoodHd = this._stcChangeGoodBL.GetSingleSTCChangeHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcChangeGoodHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcChangeGoodHd.WrhsSrc = this.WrhsSrcDropDownList.SelectedValue;
            _stcChangeGoodHd.WrhsSrcFgSubLed = _wrhsBL.GetWarehouseFgSubledByCode(this.WrhsSrcDropDownList.SelectedValue);
            if (this.SrcSubledDropDownList.SelectedValue != "null")
            {
                _stcChangeGoodHd.WrhsSrcSubLed = this.SrcSubledDropDownList.SelectedValue;
            }
            else
            {
                _stcChangeGoodHd.WrhsSrcSubLed = "";
            }
            _stcChangeGoodHd.WrhsDest = this.WrhsDestDropDownList.SelectedValue;
            _stcChangeGoodHd.WrhsDestFgSubLed = _wrhsBL.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue);
            if (this.DestSubledDropDownList.SelectedValue != "null")
            {
                _stcChangeGoodHd.WrhsDestSubLed = this.DestSubledDropDownList.SelectedValue;
            }
            else
            {
                _stcChangeGoodHd.WrhsDestSubLed = "";
            }
            _stcChangeGoodHd.Operator = this.OperatorTextBox.Text;
            _stcChangeGoodHd.Remark = this.RemarkTextBox.Text;
            _stcChangeGoodHd.EditBy = HttpContext.Current.User.Identity.Name;
            _stcChangeGoodHd.EditDate = DateTime.Now;

            bool _result = this._stcChangeGoodBL.EditSTCChangeHd(_stcChangeGoodHd);

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

        protected void WrhsSrcDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_wrhsBL.GetWarehouseFgSubledByCode(this.WrhsSrcDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.SrcSubledDropDownList.Enabled = false;
                this.SrcSubledDropDownList.SelectedValue = "null";
            }
            else
            {
                this.SrcSubledDropDownList.Enabled = true;
                if (_wrhsBL.GetWarehouseFgSubledByCode(this.WrhsSrcDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.ShowCustSrc();
                }
                else if (_wrhsBL.GetWarehouseFgSubledByCode(this.WrhsSrcDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.ShowSuppSrc();
                }
            }
        }

        protected void WrhsDestDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_wrhsBL.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.DestSubledDropDownList.Enabled = false;
                this.DestSubledDropDownList.SelectedValue = "null";
            }
            else
            {
                this.DestSubledDropDownList.Enabled = true;
                if (_wrhsBL.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.ShowCustDest();
                }
                else if (_wrhsBL.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.ShowSuppDest();
                }
            }
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            STCChangeHd _stcChangeGoodHd = this._stcChangeGoodBL.GetSingleSTCChangeHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcChangeGoodHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcChangeGoodHd.WrhsSrc = this.WrhsSrcDropDownList.SelectedValue;
            _stcChangeGoodHd.WrhsSrcFgSubLed = _wrhsBL.GetWarehouseFgSubledByCode(this.WrhsSrcDropDownList.SelectedValue);
            if (this.SrcSubledDropDownList.SelectedValue != "null")
            {
                _stcChangeGoodHd.WrhsSrcSubLed = this.SrcSubledDropDownList.SelectedValue;
            }
            else
            {
                _stcChangeGoodHd.WrhsSrcSubLed = "";
            }
            _stcChangeGoodHd.WrhsDest = this.WrhsDestDropDownList.SelectedValue;
            _stcChangeGoodHd.WrhsDestFgSubLed = _wrhsBL.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue);
            if (this.DestSubledDropDownList.SelectedValue != "null")
            {
                _stcChangeGoodHd.WrhsDestSubLed = this.DestSubledDropDownList.SelectedValue;
            }
            else
            {
                _stcChangeGoodHd.WrhsDestSubLed = "";
            }
            _stcChangeGoodHd.Operator = this.OperatorTextBox.Text;
            _stcChangeGoodHd.Remark = this.RemarkTextBox.Text;
            _stcChangeGoodHd.EditBy = HttpContext.Current.User.Identity.Name;
            _stcChangeGoodHd.EditDate = DateTime.Now;

            bool _result = this._stcChangeGoodBL.EditSTCChangeHd(_stcChangeGoodHd);

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
    }
}