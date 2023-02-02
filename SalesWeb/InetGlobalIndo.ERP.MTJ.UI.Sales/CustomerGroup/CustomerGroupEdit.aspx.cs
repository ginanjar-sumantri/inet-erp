using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.CustomerGroup
{
    public partial class CustomerGroupEdit : CustomerGroupBase
    {
        private CustomerBL _customerBL = new CustomerBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

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

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.FgPPhCheckBox.Attributes.Add("OnClick", "DoEnableOrDisable(" + this.FgPPhCheckBox.ClientID + ", " + this.PPhTextBox.ClientID + ");");
            this.PPhTextBox.Attributes.Add("OnBlur", "ChangeFormat(" + this.PPhTextBox.ClientID + ");");
            //this.ViewDetailButton.Attributes.Add("OnClick", "return AskYouFirstToSave(" + this.CheckHidden.ClientID + ");");
        }

        public void ShowData()
        {
            MsCustGroup _msCustGroup = this._customerBL.GetSingleCustGroup(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.CustGroupCodeTextBox.Text = _msCustGroup.CustGroupCode;
            this.CustGroupNameTextBox.Text = _msCustGroup.CustGroupName;
            this.TypeDropDownList.SelectedValue = _msCustGroup.CustGroupType;
            this.FgPKPCheckBox.Checked = CustomerDataMapper.IsChecked(_msCustGroup.FgPKP);
            this.FgPPhCheckBox.Checked = CustomerDataMapper.IsChecked(_msCustGroup.FgPPh);
            if (CustomerDataMapper.IsChecked(_msCustGroup.FgPPh) == true)
            {
                this.PPhTextBox.Attributes.Remove("ReadOnly");
                this.PPhTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
            }
            else
            {
                this.PPhTextBox.Attributes.Add("ReadOnly", "True");
                this.PPhTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
            }
            this.PPhTextBox.Text = (_msCustGroup.PPH == 0) ? "0" : _msCustGroup.PPH.ToString("#,###.##");
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsCustGroup _msCustGroup = this._customerBL.GetSingleCustGroup(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msCustGroup.CustGroupName = this.CustGroupNameTextBox.Text;
            _msCustGroup.CustGroupType = this.TypeDropDownList.SelectedValue;
            _msCustGroup.FgPKP = CustomerDataMapper.IsChecked(this.FgPKPCheckBox.Checked);
            _msCustGroup.FgPPh = CustomerDataMapper.IsChecked(this.FgPPhCheckBox.Checked);
            _msCustGroup.PPH = Convert.ToDecimal(this.PPhTextBox.Text);
            _msCustGroup.UserID = HttpContext.Current.User.Identity.Name;
            _msCustGroup.UserDate = DateTime.Now;

            bool _result = this._customerBL.EditCustGroup(_msCustGroup);

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
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {

            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.Page.IsValid == true)
            {
                MsCustGroup _msCustGroup = this._customerBL.GetSingleCustGroup(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _msCustGroup.CustGroupName = this.CustGroupNameTextBox.Text;
                _msCustGroup.CustGroupType = this.TypeDropDownList.SelectedValue;
                _msCustGroup.FgPKP = CustomerDataMapper.IsChecked(this.FgPKPCheckBox.Checked);
                _msCustGroup.FgPPh = CustomerDataMapper.IsChecked(this.FgPPhCheckBox.Checked);
                _msCustGroup.PPH = Convert.ToDecimal(this.PPhTextBox.Text);
                _msCustGroup.UserID = HttpContext.Current.User.Identity.Name;
                _msCustGroup.UserDate = DateTime.Now;

                bool _result = this._customerBL.EditCustGroup(_msCustGroup);

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
}