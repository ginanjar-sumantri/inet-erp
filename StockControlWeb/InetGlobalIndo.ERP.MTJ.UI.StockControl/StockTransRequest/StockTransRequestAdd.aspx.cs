using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockTransRequest
{
    public partial class StockTransRequestAdd : StockTransRequestBase
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

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowWarehouse();

                this.SetAttribute();
                this.ClearData();
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

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();

            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.WrhsSrcDropDownList.SelectedValue = "null";
            this.WrhsDestDropDownList.Items.Clear();
            this.WrhsDestDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.WrhsDestDropDownList.SelectedValue = "null";
            this.WrhsDestDropDownList.Enabled = false;
            this.RequestByTextBox.Text = "";
            this.RemarkTextBox.Text = "";
        }

        protected void WrhsSrcDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.WrhsSrcDropDownList.SelectedValue != "null")
            {
                this.ShowWarehouseDest();
                this.WrhsDestDropDownList.Enabled = true;
            }
            else
            {
                this.WrhsDestDropDownList.Items.Clear();
                this.WrhsDestDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.WrhsDestDropDownList.SelectedValue = "null";
                this.WrhsDestDropDownList.Enabled = false;
            }
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            STCTransReqHd _stcTransReqHd = new STCTransReqHd();

            _stcTransReqHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcTransReqHd.Status = StockTransRequestDataMapper.GetStatus(TransStatus.OnHold);
            _stcTransReqHd.WrhsAreaSrc = this.WrhsSrcDropDownList.SelectedValue;
            _stcTransReqHd.WrhsAreaDest = this.WrhsDestDropDownList.SelectedValue;
            _stcTransReqHd.RequestBy = this.RequestByTextBox.Text;
            _stcTransReqHd.Remark = this.RemarkTextBox.Text;
            _stcTransReqHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcTransReqHd.DatePrep = DateTime.Now;

            string _result = this._stockTransReqBL.AddSTCTransReqHd(_stcTransReqHd);

            if (_result != "")
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}