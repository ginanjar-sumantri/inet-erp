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
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.DirectBillOfLading
{
    public partial class DirectBillOfLadingEdit : DirectBillOfLadingBase
    {
        private DirectBillOfLadingBL _directBillOfLadingBL = new DirectBillOfLadingBL();
        private CustomerBL _customerBL = new CustomerBL();
        private PermissionBL _permBL = new PermissionBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();

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
                this.TransDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.TransDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

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
            this.TransDateTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void WarehouseCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            char _fgSubled = _warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseCodeDropDownList.SelectedValue);

            if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.WrhsSubledDropDownList.Enabled = false;
                this.WrhsSubledDropDownList.SelectedValue = "null";
            }
            else
            {
                this.WrhsSubledDropDownList.Enabled = true;
                if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    //this.ShowCust();
                }
                else if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    //this.ShowSupp();
                }
            }
        }

        private void ShowData()
        {
            STCTrDirectSJHd _STCTrDirectSJHd = this._directBillOfLadingBL.GetSingleSTCTrDirectSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNmbrTextBox.Text = _STCTrDirectSJHd.TransNmbr;
            this.FileNmbrTextBox.Text = _STCTrDirectSJHd.FileNmbr;
            this.TransDateTextBox.Text = DateFormMapper.GetValue(_STCTrDirectSJHd.TransDate);
            this.CustCodeTextBox.Text = _STCTrDirectSJHd.CustCode;
            this.CustomerNameTextBox.Text = _customerBL.GetNameByCode(_STCTrDirectSJHd.CustCode);            
            this.RemarkTextBox.Text = _STCTrDirectSJHd.Remark;
            this.CounterTextBox.Text = (500 - this.RemarkTextBox.Text.Length).ToString();

            this.WarehouseCodeDropDownList.SelectedValue = _STCTrDirectSJHd.WrhsCode;
            char _fgSubled = _warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseCodeDropDownList.SelectedValue);
            if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.WrhsSubledDropDownList.Enabled = false;
                this.WrhsSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.WrhsSubledDropDownList.SelectedValue = "null";
            }
            else
            {
                this.WrhsSubledDropDownList.Enabled = true;
                if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    //this.ShowCust();
                }
                else if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    //this.ShowSupp();
                }
                this.WrhsSubledDropDownList.SelectedValue = _STCTrDirectSJHd.WrhsSubLed;
            }
        }

         protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCTrDirectSJHd _STCTrDirectSJHd = this._directBillOfLadingBL.GetSingleSTCTrDirectSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _STCTrDirectSJHd.TransDate = new DateTime(DateFormMapper.GetValue(this.TransDateTextBox.Text).Year, DateFormMapper.GetValue(this.TransDateTextBox.Text).Month, DateFormMapper.GetValue(this.TransDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _STCTrDirectSJHd.CustCode = this.CustCodeTextBox.Text;
            _STCTrDirectSJHd.Remark = this.RemarkTextBox.Text;
            _STCTrDirectSJHd.EditBy = HttpContext.Current.User.Identity.Name;
            _STCTrDirectSJHd.EditDate = DateTime.Now;

            _STCTrDirectSJHd.WrhsCode = this.WarehouseCodeDropDownList.SelectedValue;
            if (this.WrhsSubledDropDownList.SelectedValue != "null")
            {
                _STCTrDirectSJHd.WrhsSubLed = this.WrhsSubledDropDownList.SelectedValue;
            }
            else
            {
                _STCTrDirectSJHd.WrhsSubLed = "";
            }

            bool _result = this._directBillOfLadingBL.EditSTCTrDirectSJHd();

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
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            STCTrDirectSJHd _STCTrDirectSJHd = this._directBillOfLadingBL.GetSingleSTCTrDirectSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _STCTrDirectSJHd.TransDate = new DateTime(DateFormMapper.GetValue(this.TransDateTextBox.Text).Year, DateFormMapper.GetValue(this.TransDateTextBox.Text).Month, DateFormMapper.GetValue(this.TransDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _STCTrDirectSJHd.CustCode = this.CustCodeTextBox.Text;
            _STCTrDirectSJHd.Remark = this.RemarkTextBox.Text;
            _STCTrDirectSJHd.EditBy = HttpContext.Current.User.Identity.Name;
            _STCTrDirectSJHd.EditDate = DateTime.Now;

            bool _result = this._directBillOfLadingBL.EditSTCTrDirectSJHd();

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }
    }
}