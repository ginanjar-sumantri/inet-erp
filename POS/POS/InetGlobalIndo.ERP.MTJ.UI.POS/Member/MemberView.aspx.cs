using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Member
{
    public partial class MemberView : MemberBase
    {
        private MemberBL _memberBL = new MemberBL();
        private MemberTypeBL _membertypeBL = new MemberTypeBL();
        private ReligionBL _religionBL = new ReligionBL();
        private EmployeeBL _employeeBL = new EmployeeBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.SetButtonPermission();

                this.ShowJobTitle();
                this.ShowJobLevel();

                this.ShowData();
            }
        }

        protected void ShowJobTitle()
        {
            this.JobTitleListBox.Items.Clear();
            this.JobTitleListBox.DataTextField = "JobTtlName";
            this.JobTitleListBox.DataValueField = "JobTtlCode";
            this.JobTitleListBox.DataSource = this._employeeBL.GetListJobTitleForDDL();
            this.JobTitleListBox.DataBind();
            if (this.JobTitleListBox.DataSource != null)
            {
                this.JobTitleListBox.Items[0].Selected = true;
            }
        }

        protected void ShowJobLevel()
        {
            this.JobLevelListBox.Items.Clear();
            this.JobLevelListBox.DataTextField = "JobLvlName";
            this.JobLevelListBox.DataValueField = "JobLvlCode";
            this.JobLevelListBox.DataSource = this._employeeBL.GetListJobLevelForDDL();
            this.JobLevelListBox.DataBind();
            if (this.JobLevelListBox.DataSource != null)
            {
                this.JobLevelListBox.Items[0].Selected = true;
            }
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        public void ShowData()
        {
            MsMember _msMember = this._memberBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.MemberTypeTextBox.Text = new MemberTypeBL().GetMemberNameByCode(_msMember.MemberTypeCode);
            this.MemberTitleRadioButtonList.SelectedValue = _msMember.MemberTitle.ToString();
            this.MemberCodeTextBox.Text = _msMember.MemberCode;
            this.BarcodeTextBox.Text = _msMember.Barcode;
            this.GenderRadioButtonList.SelectedValue = GenderDataMapper.GetGender(_msMember.Gender);
            this.MemberNameTextBox.Text = _msMember.MemberName;
            this.ReligionTextBox.Text = new ReligionBL().GetReligionNameByCode(_msMember.ReligionCode);
            this.IdentityRadioButtonList.SelectedValue = _msMember.IdentityType.ToString();
            this.IdentityNumberTextBox.Text = _msMember.IdentityNumber;
            this.DateOfBirthTextBox.Text = DateFormMapper.GetValue(_msMember.DateOfBirth);
            this.PlaceOfBirthTextBox.Text = _msMember.PlaceOfBirth;
            this.ReferenceTextBox.Text = _msMember.ReferenceCode;
            this.Telephone1TextBox.Text = _msMember.Telephone1;
            this.Telephone1TextBox.Text = _msMember.Telephone1;
            this.Telephone2TextBox.Text = _msMember.Telephone2;
            this.HandPhone1TextBox.Text = _msMember.HandPhone1;
            this.HandPhone2TextBox.Text = _msMember.HandPhone2;
            this.AddressTextBox.Text = _msMember.Address;
            this.CityTextBox.Text = new CityBL().GetCityNameByCode(_msMember.CityCode);
            this.ZipCodeTextBox.Text = _msMember.ZipCode;
            this.EmailTextBox.Text = _msMember.Email;
            this.CompanyTextBox.Text = _msMember.Company;
            this.HobbyTextBox.Text = _msMember.Hobby;
            this.JobTitleListBox.SelectedValue = _msMember.JobTtlCode;
            this.JobLevelListBox.SelectedValue = _msMember.JobLvlCode;
            this.SalaryListBox.SelectedValue = _msMember.Salary.ToString();
            if (_msMember.EducationCode == null)
            {
                this.EducationTextBox.Text = "";
            }
            else
            {
                this.EducationTextBox.Text = this._employeeBL.GetEducationNameByCode((Guid)_msMember.EducationCode);
            }
            this.SourceInfoTextBox.Text = _msMember.SourceInfo;
            this.StatusCheckBox.Checked = MemberDataMapper.GetMemberStatus(_msMember.Status);
            this.FgActiveCheckBox.Checked = (_msMember.FgActive == 'Y') ? true : false;
            this.RemarkTextBox.Text = _msMember.Remark;
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }
    }
}