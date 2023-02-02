using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FixedAssetMoving
{
    public partial class FixedAssetMovingAdd : FixedAssetMovingBase
    {
        private FixedAssetsBL _fixedAssetBL = new FixedAssetsBL();
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

                this.ClearLabel();
                this.SetAttribute();
                this.ClearData();
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

        public void ClearDropDown()
        {
            this.SourceDropDownList.Items.Clear();
            this.SourceDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

            this.DestinationDropDownList.Items.Clear();
            this.DestinationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ClearData()
        {
            this.ClearDropDown();

            DateTime now = DateTime.Now;

            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.SourceTypeDropDownList.SelectedValue = "null";
            this.SourceDropDownList.SelectedValue = "null";
            this.DestinationTypeDropDownList.SelectedValue = "null";
            this.DestinationDropDownList.SelectedValue = "null";
            this.OperatorTextBox.Text = "";
            this.RemarkTextBox.Text = "";
        }

        protected void SourceTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SourceDropDownList.Items.Clear();
            this.SourceDropDownList.DataTextField = "Name";
            this.SourceDropDownList.DataValueField = "Code";
            this.SourceDropDownList.DataSource = this._fixedAssetBL.GetListDDLFixedAssetLocation(FixedAssetsDataMapper.GetValueFALocation(this.SourceTypeDropDownList.SelectedValue));
            this.SourceDropDownList.DataBind();
            this.SourceDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void DestinationTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DestinationDropDownList.Items.Clear();
            this.DestinationDropDownList.DataTextField = "Name";
            this.DestinationDropDownList.DataValueField = "Code";
            this.DestinationDropDownList.DataSource = this._fixedAssetBL.GetListDDLFixedAssetLocation(FixedAssetsDataMapper.GetValueFALocation(this.DestinationTypeDropDownList.SelectedValue));
            this.DestinationDropDownList.DataBind();
            this.DestinationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            GLFAMoveHd _glFAMoveHd = new GLFAMoveHd();

            _glFAMoveHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _glFAMoveHd.Status = FixedAssetMovingStatus.GetStatus(TransStatus.OnHold);
            _glFAMoveHd.FALocationTypeSrc = this.SourceTypeDropDownList.SelectedValue;
            _glFAMoveHd.FALocationCodeSrc = this.SourceDropDownList.SelectedValue;
            _glFAMoveHd.FALocationTypeDest = this.DestinationTypeDropDownList.SelectedValue;
            _glFAMoveHd.FALocationCodeDest = this.DestinationDropDownList.SelectedValue;
            _glFAMoveHd.Operator = this.OperatorTextBox.Text;
            _glFAMoveHd.Remark = this.RemarkTextBox.Text;

            _glFAMoveHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _glFAMoveHd.DatePrep = DateTime.Now;

            string _result = this._fixedAssetBL.AddFixedAssetMoving(_glFAMoveHd);

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