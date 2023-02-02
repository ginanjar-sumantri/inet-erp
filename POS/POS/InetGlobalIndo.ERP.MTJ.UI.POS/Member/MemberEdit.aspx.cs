using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using BusinessRule.POS;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Member
{
    public partial class MemberEdit : MemberBase
    {
        private MemberBL _memberBL = new MemberBL();
        private CityBL _cityBL = new CityBL();
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

                this.SetAttribute();
                this.ShowMemberType();
                this.ShowCity();
                this.ShowReligion();

                this.ShowJobTitle();
                this.ShowJobLevel();
                this.ShowEducation();

                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void SetAttribute()
        {
            this.MemberCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.DateOfBirthTextBox.Attributes.Add("ReadOnly", "True");
            this.ReferenceTextBox.Attributes.Add("ReadOnly", "True");
            this.BarcodeTextBox.Attributes.Add("ReadOnly", "True");

            this.IdentityNumberTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.IdentityNumberTextBox.ClientID + ")");
            this.Telephone1TextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.Telephone1TextBox.ClientID + ")");
            this.Telephone2TextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.Telephone2TextBox.ClientID + ")");
            this.HandPhone1TextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.HandPhone1TextBox.ClientID + ")");
            this.HandPhone2TextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.HandPhone2TextBox.ClientID + ")");
            this.ReferenceTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.ReferenceTextBox.ClientID + ")");
            this.ZipCodeTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.ZipCodeTextBox.ClientID + ")");
        }

        protected void ShowEducation()
        {
            this.EducationDropDownList.Items.Clear();
            this.EducationDropDownList.DataTextField = "EducationName";
            this.EducationDropDownList.DataValueField = "EducationCode";
            this.EducationDropDownList.DataSource = this._employeeBL.GetListEducationForDDL();
            this.EducationDropDownList.DataBind();
            this.EducationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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

        protected void ShowMemberType()
        {
            this.MemberTypeDropDownList.Items.Clear();
            this.MemberTypeDropDownList.DataTextField = "MemberTypeName";
            this.MemberTypeDropDownList.DataValueField = "MemberTypeCode";
            this.MemberTypeDropDownList.DataSource = this._membertypeBL.GetList();
            this.MemberTypeDropDownList.DataBind();
            this.MemberTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCity()
        {
            this.CityDropDownList.Items.Clear();
            this.CityDropDownList.DataTextField = "CityName";
            this.CityDropDownList.DataValueField = "CityCode";
            this.CityDropDownList.DataSource = this._cityBL.GetListCityForDDL();
            this.CityDropDownList.DataBind();
            this.CityDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowReligion()
        {
            this.ReligionDDL.Items.Clear();
            this.ReligionDDL.DataTextField = "ReligionName";
            this.ReligionDDL.DataValueField = "ReligionCode";
            this.ReligionDDL.DataSource = this._religionBL.GetList();
            this.ReligionDDL.DataBind();
            this.ReligionDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            MsMember _msMember = this._memberBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.MemberTypeDropDownList.SelectedValue = _msMember.MemberTypeCode;
            this.MemberTitleRadioButtonList.SelectedValue = _msMember.MemberTitle.ToString();
            this.MemberCodeTextBox.Text = _msMember.MemberCode;
            this.BarcodeTextBox.Text = _msMember.Barcode;
            this.GenderRadioButtonList.SelectedValue = GenderDataMapper.GetGender(_msMember.Gender);
            this.MemberNameTextBox.Text = _msMember.MemberName;
            this.ReligionDDL.SelectedValue = _msMember.ReligionCode;
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
            this.CityDropDownList.SelectedValue = _msMember.CityCode;
            this.ZipCodeTextBox.Text = _msMember.ZipCode;
            this.EmailTextBox.Text = _msMember.Email;
            this.CompanyTextBox.Text = _msMember.Company;
            this.HobbyTextBox.Text = _msMember.Hobby;
            this.JobTitleListBox.SelectedValue = _msMember.JobTtlCode;
            this.JobLevelListBox.SelectedValue = _msMember.JobLvlCode;
            this.SalaryListBox.SelectedValue = _msMember.Salary.ToString();
            if (_msMember.EducationCode == null)
            {
                this.EducationDropDownList.SelectedValue = "null";
            }
            else
            {
                this.EducationDropDownList.SelectedValue = _msMember.EducationCode.ToString();
            }
            this.SourceInfoTextBox.Text = _msMember.SourceInfo;
            this.StatusCheckBox.Checked = MemberDataMapper.GetMemberStatus(_msMember.Status);
            this.FgActiveCheckBox.Checked = (_msMember.FgActive == 'Y') ? true : false;
            this.RemarkTextBox.Text = _msMember.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsMember _msMember = this._memberBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msMember.MemberTypeCode = this.MemberTypeDropDownList.SelectedValue;
            _msMember.MemberTitle = Convert.ToByte(this.MemberTitleRadioButtonList.SelectedValue);
            //_msMember.MemberCode = this.MemberCodeTextBox.Text;
            _msMember.Gender = GenderDataMapper.GetGender(this.GenderRadioButtonList.SelectedValue);
            _msMember.MemberName = this.MemberNameTextBox.Text;
            _msMember.ReligionCode = this.ReligionDDL.SelectedValue;
            _msMember.IdentityType = Convert.ToInt32(this.IdentityRadioButtonList.SelectedValue);
            _msMember.IdentityNumber = this.IdentityNumberTextBox.Text;
            _msMember.DateOfBirth = DateFormMapper.GetValue(this.DateOfBirthTextBox.Text);
            _msMember.PlaceOfBirth = this.PlaceOfBirthTextBox.Text;
            _msMember.ReferenceCode = this.ReferenceTextBox.Text;
            _msMember.Telephone1 = this.Telephone1TextBox.Text;
            _msMember.Telephone2 = this.Telephone2TextBox.Text;
            _msMember.HandPhone1 = this.HandPhone1TextBox.Text;
            _msMember.HandPhone2 = this.HandPhone2TextBox.Text;
            _msMember.Address = this.AddressTextBox.Text;
            _msMember.CityCode = this.CityDropDownList.SelectedValue;
            _msMember.ZipCode = this.ZipCodeTextBox.Text;
            _msMember.Email = this.EmailTextBox.Text;
            _msMember.Company = this.CompanyTextBox.Text;
            _msMember.Hobby = this.HobbyTextBox.Text;
            _msMember.JobTtlCode = this.JobTitleListBox.SelectedValue;
            _msMember.JobLvlCode = this.JobLevelListBox.SelectedValue;
            _msMember.Salary = Convert.ToByte(this.SalaryListBox.SelectedValue);
            if (this.EducationDropDownList.SelectedValue != "null")
            {
                _msMember.EducationCode = new Guid(this.EducationDropDownList.SelectedValue);
            }
            _msMember.SourceInfo = this.SourceInfoTextBox.Text;
            _msMember.ExpiredDate = DateTime.Now.AddYears(999);
            _msMember.ActivationDate = DateTime.Now;
            _msMember.Status = MemberDataMapper.GetMemberStatus(true);
            _msMember.FgActive = (this.FgActiveCheckBox.Checked == true) ? 'Y' : 'N';
            _msMember.Remark = this.RemarkTextBox.Text;
            _msMember.ModifiedBy = HttpContext.Current.User.Identity.Name;
            _msMember.ModifiedDate = DateTime.Now;

            bool _result = this._memberBL.Edit(_msMember);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Edit Data";
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
    }
}