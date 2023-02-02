using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockIssueRequest
{
    public partial class StockIssueRequestEdit : StockIssueRequestBase
    {
        private OrganizationStructureBL _orgBL = new OrganizationStructureBL();
        private StockIssueRequestBL _stockIssueBL = new StockIssueRequestBL();
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
                
                this.PageTitleLiteral.Text = "Stock - Issue Request";

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ShowDept();

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

        public void ShowDept()
        {
            this.OrgUnitDDL.Items.Clear();
            this.OrgUnitDDL.DataTextField = "Description";
            this.OrgUnitDDL.DataValueField = "OrgUnit";
            this.OrgUnitDDL.DataSource = this._orgBL.GetListOrganizationUnitForDDL();
            this.OrgUnitDDL.DataBind();
            this.OrgUnitDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            STCRequestHd _stcRequestHd = this._stockIssueBL.GetSingleSTCRequestHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNoTextBox.Text = _stcRequestHd.TransNmbr;
            this.FileNoTextBox.Text = _stcRequestHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_stcRequestHd.TransDate);
            //this.StatusLabel.Text = StockIssueRequestDataMapper.GetStatusText(_stcRequestHd.Status);
            this.OrgUnitDDL.SelectedValue = _stcRequestHd.OrgUnit;
            this.RequestByTextBox.Text = _stcRequestHd.RequestBy;
            this.RemarkTextBox.Text = _stcRequestHd.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCRequestHd _stcRequestHd = this._stockIssueBL.GetSingleSTCRequestHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcRequestHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcRequestHd.OrgUnit = this.OrgUnitDDL.SelectedValue;
            _stcRequestHd.RequestBy = this.RequestByTextBox.Text;
            _stcRequestHd.Remark = this.RemarkTextBox.Text;
            _stcRequestHd.EditBy = HttpContext.Current.User.Identity.Name;
            _stcRequestHd.EditDate = DateTime.Now;

            bool _result = this._stockIssueBL.EditSTCRequestHd(_stcRequestHd);

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
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            STCRequestHd _stcRequestHd = this._stockIssueBL.GetSingleSTCRequestHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcRequestHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcRequestHd.OrgUnit = this.OrgUnitDDL.SelectedValue;
            _stcRequestHd.RequestBy = this.RequestByTextBox.Text;
            _stcRequestHd.Remark = this.RemarkTextBox.Text;
            _stcRequestHd.EditBy = HttpContext.Current.User.Identity.Name;
            _stcRequestHd.EditDate = DateTime.Now;

            bool _result = this._stockIssueBL.EditSTCRequestHd(_stcRequestHd);

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