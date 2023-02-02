using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.DeliveryOrder
{
    public partial class DeliveryOrderEdit : DeliveryOrderBase
    {
        private CustomerBL _cust = new CustomerBL();
        private DeliveryOrderBL _deliveryOrder = new DeliveryOrderBL();
        private SalesOrderBL _salesOrder = new SalesOrderBL();
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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.ShowCust();

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
            this.DateTextBox.Attributes.Add("ReadOnly","True");
            this.POCustNoTextBox.Attributes.Add("ReadOnly", "True");
            this.DeliveryToTextBox.Attributes.Add("ReadOnly", "True");
            this.DeliveryDateTextBox.Attributes.Add("ReadOnly", "True");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowCust()
        {
            this.CustDropDownList.Items.Clear();
            this.CustDropDownList.DataTextField = "CustName";
            this.CustDropDownList.DataValueField = "CustCode";
            this.CustDropDownList.DataSource = this._cust.GetListCustomerForDDL();
            this.CustDropDownList.DataBind();
            this.CustDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowSONo()
        {
            this.SONoDropDownList.Items.Clear();
            this.SONoDropDownList.DataTextField = "FileNmbr";
            this.SONoDropDownList.DataValueField = "TransNmbr";
            this.SONoDropDownList.DataSource = this._salesOrder.GetSONoFromVMKSOForDO(this.CustDropDownList.SelectedValue);
            this.SONoDropDownList.DataBind();
            this.SONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            MKTDOHd _mktDOHd = this._deliveryOrder.GetSingleMKTDOHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNoTextBox.Text = _mktDOHd.TransNmbr;
            this.FileNmbrTextBox.Text = _mktDOHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_mktDOHd.TransDate);
            this.CustDropDownList.SelectedValue = _mktDOHd.CustCode;
            //this.StatusLabel.Text = DeliveryOrderDataMapper.GetStatusText(_mktDOHd.Status);
            this.ShowSONo();
            this.SONoDropDownList.SelectedValue = _mktDOHd.SONo;
            this.DeliveryToTextBox.Text = _cust.GetCustAddressNameByCode(_mktDOHd.CustCode, _mktDOHd.DeliveryTo);
            this.DeliveryDateTextBox.Text = DateFormMapper.GetValue(_mktDOHd.DeliveryDate);
            this.RemarkTextBox.Text = _mktDOHd.Remark;
            this.POCustNoTextBox.Text = _mktDOHd.POCustNo;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MKTDOHd _mktDOHd = this._deliveryOrder.GetSingleMKTDOHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _mktDOHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _mktDOHd.CustCode = this.CustDropDownList.SelectedValue;
            _mktDOHd.SONo = this.SONoDropDownList.SelectedValue;
            _mktDOHd.POCustNo = this.POCustNoTextBox.Text;
            _mktDOHd.Remark = this.RemarkTextBox.Text;
            _mktDOHd.DeliveryTo = _salesOrder.GetDeliveryToFromVMKSOForDO(this.SONoDropDownList.SelectedValue);
            _mktDOHd.DeliveryDate = DateFormMapper.GetValue(this.DeliveryDateTextBox.Text);
            _mktDOHd.EditBy = HttpContext.Current.User.Identity.Name;
            _mktDOHd.EditDate = DateTime.Now;

            bool _result = this._deliveryOrder.EditMKTDOHd(_mktDOHd);

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
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }

        protected void SONoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SONoDropDownList.SelectedValue != "null")
            {
                this.POCustNoTextBox.Text = _salesOrder.GetPOCustNoFromVMKSOForDO(this.SONoDropDownList.SelectedValue);
                this.DeliveryToTextBox.Text = _cust.GetCustAddressNameByCode(this.CustDropDownList.SelectedValue, _salesOrder.GetDeliveryToFromVMKSOForDO(this.SONoDropDownList.SelectedValue));
                this.DeliveryDateTextBox.Text = _salesOrder.GetDeliveryDateFromVMKSOForDO(this.SONoDropDownList.SelectedValue);
            }
            else
            {
                this.POCustNoTextBox.Text = "";
                this.DeliveryToTextBox.Text = "";
                this.DeliveryDateTextBox.Text = "";
            }
        }

        protected void CustDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CustDropDownList.SelectedValue != "null")
            {
                this.ShowSONo();
            }
            else
            {
                this.SONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.SONoDropDownList.SelectedValue = "null";
            }
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {

            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));

        }
        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.Page.IsValid == true)
            {
                MKTDOHd _mktDOHd = this._deliveryOrder.GetSingleMKTDOHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _mktDOHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                _mktDOHd.CustCode = this.CustDropDownList.SelectedValue;
                _mktDOHd.SONo = this.SONoDropDownList.SelectedValue;
                _mktDOHd.POCustNo = this.POCustNoTextBox.Text;
                _mktDOHd.Remark = this.RemarkTextBox.Text;
                _mktDOHd.DeliveryTo = _salesOrder.GetDeliveryToFromVMKSOForDO(this.SONoDropDownList.SelectedValue);
                _mktDOHd.DeliveryDate = DateFormMapper.GetValue(this.DeliveryDateTextBox.Text);
                _mktDOHd.EditBy = HttpContext.Current.User.Identity.Name;
                _mktDOHd.EditDate = DateTime.Now;

                bool _result = this._deliveryOrder.EditMKTDOHd(_mktDOHd);

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
}