using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockReceivingOther
{
    public partial class StockReceivingOtherEdit : StockReceivingOtherBase
    {
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private StockTypeBL _stockTypeBL = new StockTypeBL();
        private StockReceivingOtherBL _stockReceivingOtherBL = new StockReceivingOtherBL();
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
                this.SJDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.SJDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ShowWarehouse();
                this.ShowStockType();

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
            this.SJDateTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ShowWarehouse()
        {
            this.WarehouseDropDownList.Items.Clear();
            this.WarehouseDropDownList.DataTextField = "WrhsName";
            this.WarehouseDropDownList.DataValueField = "WrhsCode";
            this.WarehouseDropDownList.DataSource = this._warehouseBL.GetListForDDLNoSubled();
            this.WarehouseDropDownList.DataBind();
            this.WarehouseDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowStockType()
        {
            this.StockTypeDropDownList.Items.Clear();
            this.StockTypeDropDownList.DataTextField = "StockTypeName";
            this.StockTypeDropDownList.DataValueField = "StockTypeCode";
            this.StockTypeDropDownList.DataSource = this._stockTypeBL.GetListForDDLStockReceivingOther();
            this.StockTypeDropDownList.DataBind();
            this.StockTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowData()
        {
            STCRROtherHd _stcRROtherHd = this._stockReceivingOtherBL.GetSingleSTCRROtherHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNoTextBox.Text = _stcRROtherHd.TransNmbr;
            this.FileNoTextBox.Text = _stcRROtherHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_stcRROtherHd.TransDate);
            this.WarehouseDropDownList.SelectedValue = _stcRROtherHd.WrhsCode;
            this.StockTypeDropDownList.SelectedValue = _stcRROtherHd.StockType;
            this.ReceiveFromTextBox.Text = _stcRROtherHd.ReceiveFrom;
            this.CarNoTextBox.Text = _stcRROtherHd.CarNo;
            this.DriverTextBox.Text = _stcRROtherHd.Driver;
            this.SJNoTextBox.Text = _stcRROtherHd.SJReffNo;
            this.SJDateTextBox.Text = DateFormMapper.GetValue(_stcRROtherHd.SJReffDate);
            this.RemarkTextBox.Text = _stcRROtherHd.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCRROtherHd _stcRROtherHd = this._stockReceivingOtherBL.GetSingleSTCRROtherHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcRROtherHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcRROtherHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            _stcRROtherHd.StockType = this.StockTypeDropDownList.SelectedValue;
            _stcRROtherHd.ReceiveFrom = this.ReceiveFromTextBox.Text;
            _stcRROtherHd.CarNo = this.CarNoTextBox.Text;
            _stcRROtherHd.Driver = this.DriverTextBox.Text;
            _stcRROtherHd.SJReffNo = this.SJNoTextBox.Text;
            _stcRROtherHd.SJReffDate = DateFormMapper.GetValue(this.SJDateTextBox.Text);
            _stcRROtherHd.Remark = this.RemarkTextBox.Text;

            _stcRROtherHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcRROtherHd.DatePrep = DateTime.Now;

            bool _result = this._stockReceivingOtherBL.EditSTCRROtherHd(_stcRROtherHd);

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
            STCRROtherHd _stcRROtherHd = this._stockReceivingOtherBL.GetSingleSTCRROtherHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcRROtherHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcRROtherHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            _stcRROtherHd.StockType = this.StockTypeDropDownList.SelectedValue;
            _stcRROtherHd.ReceiveFrom = this.ReceiveFromTextBox.Text;
            _stcRROtherHd.CarNo = this.CarNoTextBox.Text;
            _stcRROtherHd.Driver = this.DriverTextBox.Text;
            _stcRROtherHd.SJReffNo = this.SJNoTextBox.Text;
            _stcRROtherHd.SJReffDate = DateFormMapper.GetValue(this.SJDateTextBox.Text);
            _stcRROtherHd.Remark = this.RemarkTextBox.Text;

            _stcRROtherHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcRROtherHd.DatePrep = DateTime.Now;

            bool _result = this._stockReceivingOtherBL.EditSTCRROtherHd(_stcRROtherHd);

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