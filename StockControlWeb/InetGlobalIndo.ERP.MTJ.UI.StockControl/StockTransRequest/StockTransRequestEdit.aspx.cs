using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockTransRequest
{
    public partial class StockTransRequestEdit : StockTransRequestBase
    {
        private WarehouseBL _wrhsBL = new WarehouseBL();
        private StockTransRequestBL _stockTransReqBL = new StockTransRequestBL();
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

                this.ShowWarehouse();

                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowWarehouse()
        {
            this.WrhsSrcDropDownList.Items.Clear();
            this.WrhsSrcDropDownList.DataTextField = "WrhsAreaName";
            this.WrhsSrcDropDownList.DataValueField = "WrhsAreaCode";
            this.WrhsSrcDropDownList.DataSource = this._wrhsBL.GetListWrhsAreaForDDL();
            this.WrhsSrcDropDownList.DataBind();
            this.WrhsSrcDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowWarehouseDest()
        {
            this.WrhsDestDropDownList.Items.Clear();
            this.WrhsDestDropDownList.DataTextField = "WrhsAreaName";
            this.WrhsDestDropDownList.DataValueField = "WrhsAreaCode";
            this.WrhsDestDropDownList.DataSource = this._wrhsBL.GetListWrhsAreaDestForDDL(this.WrhsSrcDropDownList.SelectedValue);
            this.WrhsDestDropDownList.DataBind();
            this.WrhsDestDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            STCTransReqHd _stcTransReqHd = this._stockTransReqBL.GetSingleSTCTransReqHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNoTextBox.Text = _stcTransReqHd.TransNmbr;
            this.FileNoTextBox.Text = _stcTransReqHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_stcTransReqHd.TransDate);
            this.WrhsSrcDropDownList.SelectedValue = _stcTransReqHd.WrhsAreaSrc;
            if (this.WrhsSrcDropDownList.SelectedValue == "null")
            {
                this.WrhsDestDropDownList.Enabled = false;
                this.WrhsDestDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            else
            {
                this.ShowWarehouseDest();
            }
            this.WrhsDestDropDownList.SelectedValue = _stcTransReqHd.WrhsAreaDest;
            this.RequestByTextBox.Text = _stcTransReqHd.RequestBy;
            this.RemarkTextBox.Text = _stcTransReqHd.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCTransReqHd _stcTransReqHd = this._stockTransReqBL.GetSingleSTCTransReqHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcTransReqHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcTransReqHd.WrhsAreaSrc = this.WrhsSrcDropDownList.SelectedValue;
            _stcTransReqHd.WrhsAreaDest = this.WrhsDestDropDownList.SelectedValue;
            _stcTransReqHd.RequestBy = this.RequestByTextBox.Text;
            _stcTransReqHd.Remark = this.RemarkTextBox.Text;
            _stcTransReqHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcTransReqHd.DatePrep = DateTime.Now;

            bool _result = this._stockTransReqBL.EditSTCTransReqHd(_stcTransReqHd);

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
            STCTransReqHd _stcTransReqHd = this._stockTransReqBL.GetSingleSTCTransReqHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcTransReqHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcTransReqHd.WrhsAreaSrc = this.WrhsSrcDropDownList.SelectedValue;
            _stcTransReqHd.WrhsAreaDest = this.WrhsDestDropDownList.SelectedValue;
            _stcTransReqHd.RequestBy = this.RequestByTextBox.Text;
            _stcTransReqHd.Remark = this.RemarkTextBox.Text;
            _stcTransReqHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcTransReqHd.DatePrep = DateTime.Now;

            bool _result = this._stockTransReqBL.EditSTCTransReqHd(_stcTransReqHd);

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

        protected void WrhsSrcDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowWarehouseDest();

            if (this.WrhsSrcDropDownList.SelectedValue != "null")
            {
                this.WrhsDestDropDownList.Enabled = true;
            }
            else
            {
                this.WrhsDestDropDownList.Enabled = false;
            }
            this.WrhsDestDropDownList.SelectedValue = "null";
        }
    }
}