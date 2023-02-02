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

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockTransferExternalRR
{
    public partial class StockTransferExternalRREdit : StockTransferExternalRRBase
    {
        private WarehouseBL _wrhsBL = new WarehouseBL();
        private StockTransferExternalRRBL _stcTransInBL = new StockTransferExternalRRBL();
        private StockTransferExternalSJBL _stcTransOutBL = new StockTransferExternalSJBL();
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

                this.ShowSJNo();

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

        public void ShowSJNo()
        {
            this.SJNoDropDownList.Items.Clear();
            this.SJNoDropDownList.DataTextField = "FileNmbr";
            this.SJNoDropDownList.DataValueField = "TransNmbr";
            this.SJNoDropDownList.DataSource = this._stcTransOutBL.GetSJNoFromVSTTransferSJForRR();
            this.SJNoDropDownList.DataBind();
            this.SJNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            STCTransInHd _stcTransInHd = this._stcTransInBL.GetSingleSTCTransInHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNoTextBox.Text = _stcTransInHd.TransNmbr;
            this.FileNoTextBox.Text = _stcTransInHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_stcTransInHd.TransDate);
            this.SJNoDropDownList.SelectedValue = _stcTransInHd.TransReff;
            this.WrhsSrcTextBox.Text = _wrhsBL.GetWarehouseNameByCode(_stcTransInHd.WrhsSrc);
            if (_stcTransInHd.WrhsSrcFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                this.SrcSubledTextBox.Text = _custBL.GetNameByCode(_stcTransInHd.WrhsSrcSubLed);
            }
            else if (_stcTransInHd.WrhsSrcFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
            {
                this.SrcSubledTextBox.Text = _suppBL.GetSuppNameByCode(_stcTransInHd.WrhsSrcSubLed);
            }
            else if (_stcTransInHd.WrhsSrcFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.SrcSubledTextBox.Text = "";
            }
            this.WrhsDestTextBox.Text = _wrhsBL.GetWarehouseNameByCode(_stcTransInHd.WrhsDest);
            if (_stcTransInHd.WrhsDestFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                this.DestSubledTextBox.Text = _custBL.GetNameByCode(_stcTransInHd.WrhsDestSubLed);
            }
            else if (_stcTransInHd.WrhsDestFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
            {
                this.DestSubledTextBox.Text = _suppBL.GetSuppNameByCode(_stcTransInHd.WrhsDestSubLed);
            }
            else if (_stcTransInHd.WrhsDestFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.DestSubledTextBox.Text = "";
            }
            this.DriverTextBox.Text = _stcTransInHd.Driver;
            this.CarNoTextBox.Text = _stcTransInHd.CarNo;
            this.OperatorTextBox.Text = _stcTransInHd.Operator;
            this.RemarkTextBox.Text = _stcTransInHd.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCTransInHd _stcTransInHd = this._stcTransInBL.GetSingleSTCTransInHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcTransInHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcTransInHd.TransReff = this.SJNoDropDownList.SelectedValue;
            _stcTransInHd.WrhsSrc = _stcTransOutBL.GetWrhsSrcFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue);
            _stcTransInHd.WrhsSrcFgSubLed = _wrhsBL.GetWarehouseFgSubledByCode(_stcTransOutBL.GetWrhsSrcFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue));
            _stcTransInHd.WrhsSrcSubLed = _stcTransOutBL.GetWrhsSrcSubledFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue);
            _stcTransInHd.WrhsDest = _stcTransOutBL.GetWrhsDestFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue);
            _stcTransInHd.WrhsDestFgSubLed = _wrhsBL.GetWarehouseFgSubledByCode(_stcTransOutBL.GetWrhsDestFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue));
            _stcTransInHd.WrhsDestSubLed = _stcTransOutBL.GetWrhsDestSubledFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue);
            _stcTransInHd.Driver = this.DriverTextBox.Text;
            _stcTransInHd.CarNo = this.CarNoTextBox.Text;
            _stcTransInHd.Operator = this.OperatorTextBox.Text;
            _stcTransInHd.Remark = this.RemarkTextBox.Text;
            _stcTransInHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcTransInHd.DatePrep = DateTime.Now;

            bool _result = this._stcTransInBL.EditSTCTransInHd(_stcTransInHd);

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

        protected void SJNoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SJNoDropDownList.SelectedValue != "null")
            {
                this.WrhsDestTextBox.Text = _wrhsBL.GetWarehouseNameByCode(_stcTransOutBL.GetWrhsDestFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue));
                if (_wrhsBL.GetWarehouseFgSubledByCode(_stcTransOutBL.GetWrhsDestFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue)) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.DestSubledTextBox.Text = _custBL.GetNameByCode(_stcTransOutBL.GetWrhsDestSubledFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue));
                }
                else if (_wrhsBL.GetWarehouseFgSubledByCode(_stcTransOutBL.GetWrhsDestFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue)) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.DestSubledTextBox.Text = _suppBL.GetSuppNameByCode(_stcTransOutBL.GetWrhsDestSubledFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue));
                }
                else if (_wrhsBL.GetWarehouseFgSubledByCode(_stcTransOutBL.GetWrhsSrcFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue)) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
                {
                    this.DestSubledTextBox.Text = "";
                }
                this.WrhsSrcTextBox.Text = _wrhsBL.GetWarehouseNameByCode(_stcTransOutBL.GetWrhsSrcFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue));
                if (_wrhsBL.GetWarehouseFgSubledByCode(_stcTransOutBL.GetWrhsSrcFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue)) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.SrcSubledTextBox.Text = _custBL.GetNameByCode(_stcTransOutBL.GetWrhsSrcSubledFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue));
                }
                else if (_wrhsBL.GetWarehouseFgSubledByCode(_stcTransOutBL.GetWrhsSrcFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue)) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.SrcSubledTextBox.Text = _suppBL.GetSuppNameByCode(_stcTransOutBL.GetWrhsSrcSubledFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue));
                }
                else if (_wrhsBL.GetWarehouseFgSubledByCode(_stcTransOutBL.GetWrhsSrcFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue)) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
                {
                    this.SrcSubledTextBox.Text = "";
                }
            }
            else
            {
                this.WrhsSrcTextBox.Text = "";
                this.SrcSubledTextBox.Text = "";
                this.WrhsDestTextBox.Text = "";
                this.DestSubledTextBox.Text = "";
            }
        }

        //protected void WrhsDestDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (_wrhs.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
        //    {
        //        this.DestSubledDropDownList.Enabled = false;
        //    }
        //    else
        //    {
        //        this.DestSubledDropDownList.Enabled = true;
        //        if (_wrhs.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
        //        {
        //            this.ShowCustDest();
        //        }
        //        else if (_wrhs.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
        //        {
        //            this.ShowSuppDest();
        //        }
        //    }
        //}

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            STCTransInHd _stcTransInHd = this._stcTransInBL.GetSingleSTCTransInHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcTransInHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcTransInHd.TransReff = this.SJNoDropDownList.SelectedValue;
            _stcTransInHd.WrhsSrc = _stcTransOutBL.GetWrhsSrcFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue);
            _stcTransInHd.WrhsSrcFgSubLed = _wrhsBL.GetWarehouseFgSubledByCode(_stcTransOutBL.GetWrhsSrcFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue));
            _stcTransInHd.WrhsSrcSubLed = _stcTransOutBL.GetWrhsSrcSubledFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue);
            _stcTransInHd.WrhsDest = _stcTransOutBL.GetWrhsDestFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue);
            _stcTransInHd.WrhsDestFgSubLed = _wrhsBL.GetWarehouseFgSubledByCode(_stcTransOutBL.GetWrhsDestFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue));
            _stcTransInHd.WrhsDestSubLed = _stcTransOutBL.GetWrhsDestSubledFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue);
            _stcTransInHd.Driver = this.DriverTextBox.Text;
            _stcTransInHd.CarNo = this.CarNoTextBox.Text;
            _stcTransInHd.Operator = this.OperatorTextBox.Text;
            _stcTransInHd.Remark = this.RemarkTextBox.Text;
            _stcTransInHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcTransInHd.DatePrep = DateTime.Now;

            bool _result = this._stcTransInBL.EditSTCTransInHd(_stcTransInHd);

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