using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAAddStock
{
    public partial class FAAddStockAdd : FAAddStockBase
    {
        private StockChangeToFABL _faAddStockBL = new StockChangeToFABL();
        private StockIssueFixedAssetBL _stcIssueFABL = new StockIssueFixedAssetBL();
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
                this.TransDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.TransDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowWISNoDropdownlist();

                this.ClearLabel();
                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowWISNoDropdownlist()
        {
            this.WISNoDropDownList.DataSource = this._stcIssueFABL.GetListDDLSTCIssueToFAHd();
            this.WISNoDropDownList.DataValueField = "TransNmbr";
            this.WISNoDropDownList.DataTextField = "FileNmbr";
            this.WISNoDropDownList.DataBind();
            this.WISNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void SetAttribute()
        {
            this.TransDateTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ClearData()
        {
            this.TransDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.WISNoDropDownList.SelectedValue = "null";
            this.ReceivedByTextBox.Text = "";
            this.RemarkTextBox.Text = "";
        }
        
        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            GLFAAddStockHd _glFAAddStockHd = new GLFAAddStockHd();

            _glFAAddStockHd.TransDate = new DateTime(DateFormMapper.GetValue(this.TransDateTextBox.Text).Year, DateFormMapper.GetValue(this.TransDateTextBox.Text).Month, DateFormMapper.GetValue(this.TransDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _glFAAddStockHd.Status = FAAddStockDataMapper.GetStatus(TransStatus.OnHold);
            _glFAAddStockHd.WISNo = this.WISNoDropDownList.SelectedValue;
            _glFAAddStockHd.ReceiveBy = this.ReceivedByTextBox.Text;
            _glFAAddStockHd.Remark = this.RemarkTextBox.Text;

            _glFAAddStockHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _glFAAddStockHd.DatePrep = DateTime.Now;

            string _result = this._faAddStockBL.AddFAAddStockHd(_glFAAddStockHd);

            if (_result != "")
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }
    }
}