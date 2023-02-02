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

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockTransferExternalSJ
{
    public partial class StockTransferExternalSJEdit : StockTransferExternalSJBase
    {
        private WarehouseBL _wrhs = new WarehouseBL();
        private StockTransferExternalSJBL _stcTransOutBL = new StockTransferExternalSJBL();
        private StockTransRequestBL _stcTransReqBL = new StockTransRequestBL();
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

                this.ShowDDL();

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowDDL()
        {
            this.ShowRequestNo();
            this.ShowWarehouseSrc();
            this.ShowWarehouseDest();

            //this.SrcSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            //this.SrcSubledDropDownList.SelectedValue = "null";
            //this.DestSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            //this.DestSubledDropDownList.SelectedValue = "null";

            //this.ShowCustSrc();
            //this.ShowCustDest();
            //this.ShowSuppSrc();
            //this.ShowSuppDest();
        }

        public void SetAttribute()
        {
            this.WrhsAreaSrcTextBox.Attributes.Add("ReadOnly", "True");
            this.WrhsAreaDestTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowRequestNo()
        {
            this.RequestNoDropDownList.Items.Clear();
            this.RequestNoDropDownList.DataTextField = "FileNmbr";
            this.RequestNoDropDownList.DataValueField = "TransNmbr";
            this.RequestNoDropDownList.DataSource = this._stcTransReqBL.GetRequestNoFromVSTTransferReqForSJ();
            this.RequestNoDropDownList.DataBind();
            this.RequestNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowWarehouseDest()
        {
            this.WrhsDestDropDownList.Items.Clear();
            this.WrhsDestDropDownList.DataTextField = "WrhsName";
            this.WrhsDestDropDownList.DataValueField = "WrhsCode";
            this.WrhsDestDropDownList.DataSource = this._wrhs.GetWarehouseActiveAndWrhsArea(_stcTransReqBL.GetWrhsAreaDestFromVSTTransferReqForSJ(this.RequestNoDropDownList.SelectedValue));
            this.WrhsDestDropDownList.DataBind();
            this.WrhsDestDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowWarehouseSrc()
        {
            this.WrhsSrcDropDownList.Items.Clear();
            this.WrhsSrcDropDownList.DataTextField = "WrhsName";
            this.WrhsSrcDropDownList.DataValueField = "WrhsCode";
            this.WrhsSrcDropDownList.DataSource = this._wrhs.GetWarehouseActiveAndWrhsArea(_stcTransReqBL.GetWrhsAreaSrcFromVSTTransferReqForSJ(this.RequestNoDropDownList.SelectedValue));
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
            STCTransOutHd _stcTransOutHd = this._stcTransOutBL.GetSingleSTCTransOutHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNoTextBox.Text = _stcTransOutHd.TransNmbr;
            this.FileNoTextBox.Text = _stcTransOutHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_stcTransOutHd.TransDate);
            this.RequestNoDropDownList.SelectedValue = _stcTransOutHd.RequestNo;
            if (this.RequestNoDropDownList.SelectedValue != "null")
            {
                this.ShowWarehouseDest();
                this.ShowWarehouseSrc();
                this.WrhsAreaSrcTextBox.Text = _wrhs.GetWrhsAreaNameByCode(_stcTransReqBL.GetWrhsAreaSrcFromVSTTransferReqForSJ(this.RequestNoDropDownList.SelectedValue));
                this.WrhsAreaDestTextBox.Text = _wrhs.GetWrhsAreaNameByCode(_stcTransReqBL.GetWrhsAreaDestFromVSTTransferReqForSJ(this.RequestNoDropDownList.SelectedValue));
            }
            this.WrhsSrcDropDownList.SelectedValue = _stcTransOutHd.WrhsSrc;
            this.WrhsDestDropDownList.SelectedValue = _stcTransOutHd.WrhsDest;
            this.OperatorTextBox.Text = _stcTransOutHd.Operator;
            this.DriverTextBox.Text = _stcTransOutHd.Driver;
            this.CarNoTextBox.Text = _stcTransOutHd.CarNo;
            this.RemarkTextBox.Text = _stcTransOutHd.Remark;
            this.WrhsAreaSrcTextBox.Text = _wrhs.GetWrhsAreaNameByCode(_stcTransOutHd.WrhsAreaSrc);
            this.WrhsAreaDestTextBox.Text = _wrhs.GetWrhsAreaNameByCode(_stcTransOutHd.WrhsAreaDest);
            if (_wrhs.GetWarehouseFgSubledByCode(this.WrhsSrcDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.SrcSubledDropDownList.Enabled = false;
                this.SrcSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.SrcSubledDropDownList.SelectedValue = "null";
            }
            else
            {
                this.SrcSubledDropDownList.Enabled = true;
                if (_wrhs.GetWarehouseFgSubledByCode(this.WrhsSrcDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.ShowCustSrc();
                }
                else if (_wrhs.GetWarehouseFgSubledByCode(this.WrhsSrcDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.ShowSuppSrc();
                }
                this.SrcSubledDropDownList.SelectedValue = _stcTransOutHd.WrhsSrcSubLed;
            }
            if (_wrhs.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.DestSubledDropDownList.Enabled = false;
                this.DestSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.DestSubledDropDownList.SelectedValue = "null";
            }
            else
            {
                this.DestSubledDropDownList.Enabled = true;
                if (_wrhs.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.ShowCustDest();
                }
                else if (_wrhs.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.ShowSuppDest();
                }
                this.DestSubledDropDownList.SelectedValue = _stcTransOutHd.WrhsDestSubLed;
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCTransOutHd _stcTransOutHd = this._stcTransOutBL.GetSingleSTCTransOutHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcTransOutHd.RequestNo = this.RequestNoDropDownList.SelectedValue;
            _stcTransOutHd.WrhsAreaSrc = _stcTransReqBL.GetWrhsAreaSrcFromVSTTransferReqForSJ(this.RequestNoDropDownList.SelectedValue);
            _stcTransOutHd.WrhsSrc = this.WrhsSrcDropDownList.SelectedValue;
            _stcTransOutHd.WrhsSrcFgSubLed = _wrhs.GetWarehouseFgSubledByCode(this.WrhsSrcDropDownList.SelectedValue);
            if (this.SrcSubledDropDownList.SelectedValue != "null")
            {
                _stcTransOutHd.WrhsSrcSubLed = this.SrcSubledDropDownList.SelectedValue;
            }
            else
            {
                _stcTransOutHd.WrhsSrcSubLed = "";
            }
            _stcTransOutHd.WrhsAreaDest = _stcTransReqBL.GetWrhsAreaDestFromVSTTransferReqForSJ(this.RequestNoDropDownList.SelectedValue);
            _stcTransOutHd.WrhsDest = this.WrhsDestDropDownList.SelectedValue;
            _stcTransOutHd.WrhsDestFgSubLed = _wrhs.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue);
            if (this.DestSubledDropDownList.SelectedValue != "null")
            {
                _stcTransOutHd.WrhsDestSubLed = this.DestSubledDropDownList.SelectedValue;
            }
            else
            {
                _stcTransOutHd.WrhsDestSubLed = "";
            }
            _stcTransOutHd.Operator = this.OperatorTextBox.Text;
            _stcTransOutHd.Driver = this.DriverTextBox.Text;
            _stcTransOutHd.CarNo = this.CarNoTextBox.Text;
            _stcTransOutHd.Remark = this.RemarkTextBox.Text;
            _stcTransOutHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcTransOutHd.DatePrep = DateTime.Now;

            bool _result = this._stcTransOutBL.EditSTCTransOutHd(_stcTransOutHd);

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

        protected void RequestNoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.RequestNoDropDownList.SelectedValue != "null")
            {
                this.ShowWarehouseDest();
                this.ShowWarehouseSrc();
                this.WrhsAreaSrcTextBox.Text = _wrhs.GetWrhsAreaNameByCode(_stcTransReqBL.GetWrhsAreaSrcFromVSTTransferReqForSJ(this.RequestNoDropDownList.SelectedValue));
                this.WrhsAreaDestTextBox.Text = _wrhs.GetWrhsAreaNameByCode(_stcTransReqBL.GetWrhsAreaDestFromVSTTransferReqForSJ(this.RequestNoDropDownList.SelectedValue));
            }
            else
            {
                this.WrhsAreaDestTextBox.Text = "";
                this.WrhsAreaSrcTextBox.Text = "";
                this.WrhsSrcDropDownList.SelectedValue = "null";
                this.WrhsDestDropDownList.SelectedValue = "null";
            }
        }

        protected void WrhsSrcDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_wrhs.GetWarehouseFgSubledByCode(this.WrhsSrcDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.SrcSubledDropDownList.Enabled = false;
                this.SrcSubledDropDownList.SelectedValue = "null";
            }
            else
            {
                this.SrcSubledDropDownList.Enabled = true;
                if (_wrhs.GetWarehouseFgSubledByCode(this.WrhsSrcDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.ShowCustSrc();
                }
                else if (_wrhs.GetWarehouseFgSubledByCode(this.WrhsSrcDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.ShowSuppSrc();
                }
            }
        }

        protected void WrhsDestDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_wrhs.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.DestSubledDropDownList.Enabled = false;
                this.DestSubledDropDownList.SelectedValue = "null";
            }
            else
            {
                this.DestSubledDropDownList.Enabled = true;
                if (_wrhs.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.ShowCustDest();
                }
                else if (_wrhs.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
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
            STCTransOutHd _stcTransOutHd = this._stcTransOutBL.GetSingleSTCTransOutHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcTransOutHd.RequestNo = this.RequestNoDropDownList.SelectedValue;
            _stcTransOutHd.WrhsAreaSrc = _stcTransReqBL.GetWrhsAreaSrcFromVSTTransferReqForSJ(this.RequestNoDropDownList.SelectedValue);
            _stcTransOutHd.WrhsSrc = this.WrhsSrcDropDownList.SelectedValue;
            _stcTransOutHd.WrhsSrcFgSubLed = _wrhs.GetWarehouseFgSubledByCode(this.WrhsSrcDropDownList.SelectedValue);
            if (this.SrcSubledDropDownList.SelectedValue != "null")
            {
                _stcTransOutHd.WrhsSrcSubLed = this.SrcSubledDropDownList.SelectedValue;
            }
            else
            {
                _stcTransOutHd.WrhsSrcSubLed = "";
            }
            _stcTransOutHd.WrhsDest = this.WrhsDestDropDownList.SelectedValue;
            _stcTransOutHd.WrhsDestFgSubLed = _wrhs.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue);
            if (this.DestSubledDropDownList.SelectedValue != "null")
            {
                _stcTransOutHd.WrhsDestSubLed = this.DestSubledDropDownList.SelectedValue;
            }
            else
            {
                _stcTransOutHd.WrhsDestSubLed = "";
            }
            _stcTransOutHd.Operator = this.OperatorTextBox.Text;
            _stcTransOutHd.Driver = this.DriverTextBox.Text;
            _stcTransOutHd.CarNo = this.CarNoTextBox.Text;
            _stcTransOutHd.Remark = this.RemarkTextBox.Text;
            _stcTransOutHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcTransOutHd.DatePrep = DateTime.Now;

            bool _result = this._stcTransOutBL.EditSTCTransOutHd(_stcTransOutHd);

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