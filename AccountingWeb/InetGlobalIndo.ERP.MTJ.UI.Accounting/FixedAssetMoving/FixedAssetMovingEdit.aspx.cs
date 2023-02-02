using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FixedAssetMoving
{
    public partial class FixedAssetMovingEdit : FixedAssetMovingBase
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

                this.ClearLabel();
                this.SetAttribute();
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

        public void ShowSourceDropDown()
        {
            this.SourceDropDownList.Items.Clear();
            this.SourceDropDownList.DataTextField = "Name";
            this.SourceDropDownList.DataValueField = "Code";
            this.SourceDropDownList.DataSource = this._fixedAssetBL.GetListDDLFixedAssetLocation(FixedAssetsDataMapper.GetValueFALocation(this.SourceTypeDropDownList.SelectedValue));
            this.SourceDropDownList.DataBind();
            this.SourceDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowDestinationDropDown()
        {
            this.DestinationDropDownList.Items.Clear();
            this.DestinationDropDownList.DataTextField = "Name";
            this.DestinationDropDownList.DataValueField = "Code";
            this.DestinationDropDownList.DataSource = this._fixedAssetBL.GetListDDLFixedAssetLocation(FixedAssetsDataMapper.GetValueFALocation(this.DestinationTypeDropDownList.SelectedValue));
            this.DestinationDropDownList.DataBind();
            this.DestinationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            GLFAMoveHd _glFAMoveHd = this._fixedAssetBL.GetSingleFixedAssetMoving(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.NoTextBox.Text = _glFAMoveHd.TransNmbr;
            this.FileNmbrTextBox.Text = _glFAMoveHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_glFAMoveHd.TransDate);
            this.SourceTypeDropDownList.SelectedValue = _glFAMoveHd.FALocationTypeSrc;
            this.ShowSourceDropDown();
            this.SourceDropDownList.SelectedValue = _glFAMoveHd.FALocationCodeSrc;

            this.DestinationTypeDropDownList.SelectedValue = _glFAMoveHd.FALocationTypeDest;
            this.ShowDestinationDropDown();
            this.DestinationDropDownList.SelectedValue = _glFAMoveHd.FALocationCodeDest;

            this.OperatorTextBox.Text = _glFAMoveHd.Operator;
            this.RemarkTextBox.Text = _glFAMoveHd.Remark;
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

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            GLFAMoveHd _glFAMoveHd = this._fixedAssetBL.GetSingleFixedAssetMoving(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _glFAMoveHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _glFAMoveHd.FALocationTypeSrc = this.SourceTypeDropDownList.SelectedValue;
            _glFAMoveHd.FALocationCodeSrc = this.SourceDropDownList.SelectedValue;
            _glFAMoveHd.FALocationTypeDest = this.DestinationTypeDropDownList.SelectedValue;
            _glFAMoveHd.FALocationCodeDest = this.DestinationDropDownList.SelectedValue;
            _glFAMoveHd.Operator = this.OperatorTextBox.Text;
            _glFAMoveHd.Remark = this.RemarkTextBox.Text;

            _glFAMoveHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _glFAMoveHd.DatePrep = DateTime.Now;

            bool _result = this._fixedAssetBL.EditFixedAssetMoving(_glFAMoveHd);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            GLFAMoveHd _glFAMoveHd = this._fixedAssetBL.GetSingleFixedAssetMoving(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _glFAMoveHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _glFAMoveHd.FALocationTypeSrc = this.SourceTypeDropDownList.SelectedValue;
            _glFAMoveHd.FALocationCodeSrc = this.SourceDropDownList.SelectedValue;
            _glFAMoveHd.FALocationTypeDest = this.DestinationTypeDropDownList.SelectedValue;
            _glFAMoveHd.FALocationCodeDest = this.DestinationDropDownList.SelectedValue;
            _glFAMoveHd.Operator = this.OperatorTextBox.Text;
            _glFAMoveHd.Remark = this.RemarkTextBox.Text;

            _glFAMoveHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _glFAMoveHd.DatePrep = DateTime.Now;

            bool _result = this._fixedAssetBL.EditFixedAssetMoving(_glFAMoveHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }
    }
}