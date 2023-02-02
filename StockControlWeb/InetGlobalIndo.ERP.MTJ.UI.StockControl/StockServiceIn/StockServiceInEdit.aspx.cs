using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockServiceIn
{
    public partial class StockServiceInEdit : StockServiceInBase
    {
        private StockServiceInBL _stockServiceInBL = new StockServiceInBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private CustomerBL _customerBL = new CustomerBL();
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
                //this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ShowWarehouse();
                this.ShowCustomer2();

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
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ShowWarehouse()
        {
            this.WarehouseDropDownList.Items.Clear();
            this.WarehouseDropDownList.DataTextField = "WrhsName";
            this.WarehouseDropDownList.DataValueField = "WrhsCode";
            this.WarehouseDropDownList.DataSource = this._warehouseBL.GetListForDDLCustomerSubled();
            this.WarehouseDropDownList.DataBind();
            this.WarehouseDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCustomer()
        {
            this.WarehouseSubledDropDownList.Items.Clear();
            this.WarehouseSubledDropDownList.DataTextField = "CustName";
            this.WarehouseSubledDropDownList.DataValueField = "CustCode";
            this.WarehouseSubledDropDownList.DataSource = this._customerBL.GetListCustomerForDDL();
            this.WarehouseSubledDropDownList.DataBind();
            this.WarehouseSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCustomer2()
        {
            this.CustomerDropDownList.Items.Clear();
            this.CustomerDropDownList.DataTextField = "CustName";
            this.CustomerDropDownList.DataValueField = "CustCode";
            this.CustomerDropDownList.DataSource = this._customerBL.GetListCustomerForDDL();
            this.CustomerDropDownList.DataBind();
            this.CustomerDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void WarehouseDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.WarehouseDropDownList.SelectedValue != "null")
            {
                char _tempFgSubled = this._warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue);

                this.WarehouseFgSubledHiddenField.Value = _tempFgSubled.ToString();

                if (_tempFgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.ShowCustomer();

                    this.WarehouseSubledDropDownList.Enabled = true;
                }
                else if (_tempFgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
                {
                    this.WarehouseSubledDropDownList.Items.Clear();
                    this.WarehouseSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                    this.WarehouseSubledDropDownList.Enabled = false;
                }
                else
                {
                    this.WarehouseSubledDropDownList.Items.Clear();
                    this.WarehouseSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                    this.WarehouseSubledDropDownList.Enabled = false;
                }

            }
            else
            {
                this.WarehouseFgSubledHiddenField.Value = "";

                this.WarehouseSubledDropDownList.Items.Clear();
                this.WarehouseSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.WarehouseSubledDropDownList.Enabled = false;
            }
        }

        protected void ShowData()
        {
            STCServiceInHd _stcServiceInHd = this._stockServiceInBL.GetSingleSTCServiceInHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNoTextBox.Text = _stcServiceInHd.TransNmbr;
            this.FileNoTextBox.Text = _stcServiceInHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_stcServiceInHd.TransDate);
            if (_stcServiceInHd.CustCode != null)
            {
                this.CustomerDropDownList.SelectedValue = _stcServiceInHd.CustCode;
            }
            this.WarehouseDropDownList.SelectedValue = _stcServiceInHd.WrhsCode;
            this.WarehouseFgSubledHiddenField.Value = _stcServiceInHd.WrhsFgSubLed.ToString();

            if (_stcServiceInHd.WrhsFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                this.ShowCustomer();
                this.WarehouseSubledDropDownList.Enabled = true;
                this.WarehouseSubledDropDownList.SelectedValue = _stcServiceInHd.WrhsSubLed;
            }
            else if (_stcServiceInHd.WrhsFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.WarehouseSubledDropDownList.Items.Clear();
                this.WarehouseSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.WarehouseSubledDropDownList.Enabled = false;
            }
            else
            {
                this.WarehouseSubledDropDownList.Items.Clear();
                this.WarehouseSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.WarehouseSubledDropDownList.Enabled = false;
            }

            this.ReceivedByTextBox.Text = _stcServiceInHd.ReceivedBy;
            this.RemarkTextBox.Text = _stcServiceInHd.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCServiceInHd _stcServiceInHd = this._stockServiceInBL.GetSingleSTCServiceInHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcServiceInHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            if (this.CustomerDropDownList.SelectedValue != "null")
            {
                _stcServiceInHd.CustCode = this.CustomerDropDownList.SelectedValue;
            }
            else
            {
                _stcServiceInHd.CustCode = null;
            }
            _stcServiceInHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            _stcServiceInHd.WrhsFgSubLed = Convert.ToChar(this.WarehouseFgSubledHiddenField.Value);
            if (this.WarehouseSubledDropDownList.SelectedValue != "null")
            {
                _stcServiceInHd.WrhsSubLed = this.WarehouseSubledDropDownList.SelectedValue;
            }
            else
            {
                _stcServiceInHd.WrhsSubLed = "";
            }

            _stcServiceInHd.ReceivedBy = this.ReceivedByTextBox.Text;
            _stcServiceInHd.Remark = this.RemarkTextBox.Text;

            _stcServiceInHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcServiceInHd.DatePrep = DateTime.Now;

            bool _result = this._stockServiceInBL.EditSTCServiceInHd(_stcServiceInHd);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
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
            STCServiceInHd _stcServiceInHd = this._stockServiceInBL.GetSingleSTCServiceInHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcServiceInHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            if (this.CustomerDropDownList.SelectedValue != "null")
            {
                _stcServiceInHd.CustCode = this.CustomerDropDownList.SelectedValue;
            }
            else
            {
                _stcServiceInHd.CustCode = null;
            }
            _stcServiceInHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            _stcServiceInHd.WrhsFgSubLed = Convert.ToChar(this.WarehouseFgSubledHiddenField.Value);
            if (this.WarehouseSubledDropDownList.SelectedValue != "null")
            {
                _stcServiceInHd.WrhsSubLed = this.WarehouseSubledDropDownList.SelectedValue;
            }
            else
            {
                _stcServiceInHd.WrhsSubLed = "";
            }

            _stcServiceInHd.ReceivedBy = this.ReceivedByTextBox.Text;
            _stcServiceInHd.Remark = this.RemarkTextBox.Text;

            _stcServiceInHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcServiceInHd.DatePrep = DateTime.Now;

            bool _result = this._stockServiceInBL.EditSTCServiceInHd(_stcServiceInHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }
    }
}