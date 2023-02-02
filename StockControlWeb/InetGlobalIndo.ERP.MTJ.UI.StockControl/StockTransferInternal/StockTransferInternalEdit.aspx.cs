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

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockTransferInternal
{
    public partial class StockTransferInternalEdit : StockTransferInternalBase
    {
        private WarehouseBL _wrhsBL = new WarehouseBL();
        private StockTransferInternalBL _stcTransInternalBL = new StockTransferInternalBL();
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

        public void ShowWarehouseDest(string _prmWrhsAreaCode)
        {
            this.WrhsDestDropDownList.Items.Clear();
            this.WrhsDestDropDownList.DataTextField = "WrhsName";
            this.WrhsDestDropDownList.DataValueField = "WrhsCode";
            this.WrhsDestDropDownList.DataSource = this._wrhsBL.GetWarehouseActiveAndWrhsArea(_prmWrhsAreaCode);
            this.WrhsDestDropDownList.DataBind();
            this.WrhsDestDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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
            STCTransferHd _stcTransInternalHd = this._stcTransInternalBL.GetSingleSTCTransferHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNoTextBox.Text = _stcTransInternalHd.TransNmbr;
            this.FileNoTextBox.Text = _stcTransInternalHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_stcTransInternalHd.TransDate);
            this.WrhsAreaTextBox.Text = _wrhsBL.GetWrhsAreaNameByCode(_stcTransInternalHd.WrhsArea);
            //this.WrhsDestDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            //this.DestSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            if (_stcTransInternalHd.WrhsArea != "")
            {
                this.ShowWarehouseDest(_stcTransInternalHd.WrhsArea);
                this.WrhsSrcTextBox.Text = _wrhsBL.GetWarehouseNameByCode(_stcTransInternalHd.WrhsSrc);
            }
            else
            {
                this.WrhsDestDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            this.WrhsDestDropDownList.SelectedValue = _stcTransInternalHd.WrhsDest;
            this.OperatorTextBox.Text = _stcTransInternalHd.Operator;
            this.RemarkTextBox.Text = _stcTransInternalHd.Remark;
            if (_wrhsBL.GetWarehouseFgSubledByCode(_stcTransInternalHd.WrhsSrc) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.SrcSubledTextBox.Text = "";
            }
            else
            {
                if (_wrhsBL.GetWarehouseFgSubledByCode(_stcTransInternalHd.WrhsSrc) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.SrcSubledTextBox.Text = _custBL.GetNameByCode(_stcTransInternalHd.WrhsSrcSubLed);
                }
                else if (_wrhsBL.GetWarehouseFgSubledByCode(_stcTransInternalHd.WrhsSrc) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.SrcSubledTextBox.Text = _suppBL.GetSuppNameByCode(_stcTransInternalHd.WrhsSrcSubLed);
                }
            }
            if (_wrhsBL.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.DestSubledDropDownList.Enabled = false;
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
            this.DestSubledDropDownList.SelectedValue = _stcTransInternalHd.WrhsDestSubLed;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCTransferHd _stcTransInternalHd = this._stcTransInternalBL.GetSingleSTCTransferHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcTransInternalHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcTransInternalHd.WrhsDest = this.WrhsDestDropDownList.SelectedValue;
            _stcTransInternalHd.WrhsDestFgSubled = _wrhsBL.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue);
            if (this.DestSubledDropDownList.SelectedValue != "null")
            {
                _stcTransInternalHd.WrhsDestSubLed = this.DestSubledDropDownList.SelectedValue;
            }
            else
            {
                _stcTransInternalHd.WrhsDestSubLed = "";
            }
            _stcTransInternalHd.Operator = this.OperatorTextBox.Text;
            _stcTransInternalHd.Remark = this.RemarkTextBox.Text;
            _stcTransInternalHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcTransInternalHd.DatePrep = DateTime.Now;

            bool _result = this._stcTransInternalBL.EditSTCTransferHd(_stcTransInternalHd);

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

        protected void WrhsDestDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.WrhsDestDropDownList.SelectedValue == "null")
            {
                this.DestSubledDropDownList.SelectedValue = "null";
            }
            else
            {
                if (_wrhsBL.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
                {
                    this.DestSubledDropDownList.SelectedValue = "null";
                    this.DestSubledDropDownList.Enabled = false;
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
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            STCTransferHd _stcTransInternalHd = this._stcTransInternalBL.GetSingleSTCTransferHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcTransInternalHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcTransInternalHd.WrhsDest = this.WrhsDestDropDownList.SelectedValue;
            _stcTransInternalHd.WrhsDestFgSubled = _wrhsBL.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue);
            if (this.DestSubledDropDownList.SelectedValue != "null")
            {
                _stcTransInternalHd.WrhsDestSubLed = this.DestSubledDropDownList.SelectedValue;
            }
            else
            {
                _stcTransInternalHd.WrhsDestSubLed = "";
            }
            _stcTransInternalHd.Operator = this.OperatorTextBox.Text;
            _stcTransInternalHd.Remark = this.RemarkTextBox.Text;
            _stcTransInternalHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcTransInternalHd.DatePrep = DateTime.Now;

            bool _result = this._stcTransInternalBL.EditSTCTransferHd(_stcTransInternalHd);

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

        //protected void WrhsAreaDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.WrhsAreaDropDownList.SelectedValue == "null")
        //    {
        //        this.WrhsSrcDropDownList.Items.Clear();
        //        this.WrhsSrcDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //        this.WrhsSrcDropDownList.SelectedValue = "null";

        //        this.WrhsDestDropDownList.Items.Clear();
        //        this.WrhsDestDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //        this.WrhsDestDropDownList.SelectedValue = "null";
        //    }
        //    else
        //    {
        //        this.ShowWarehouseSrc();
        //        this.ShowWarehouseDest();
        //    }
        //}
    }
}