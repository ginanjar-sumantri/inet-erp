using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Member
{
    public partial class MemberAdd : MemberBase
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

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

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

                this.ClearData();
            }
        }

        protected void SetAttribute()
        {
            this.DateOfBirthTextBox.Attributes.Add("ReadOnly", "True");
            //this.MemberCodeTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.MemberCodeTextBox.ClientID + ")");
            this.BarcodeTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.BarcodeTextBox.ClientID + ")");
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

        protected void ReferenceTextBox_TextChanged(object sender, EventArgs e)
        {
            this.ShowRefName();
        }
        public void ShowRefName()
        {
            MsMember _msMember = this._memberBL.GetSingleByBarcode(this.ReferenceTextBox.Text);
            if (_msMember == null)
            {
                this.WarningLabel.Text = "Reference ID Not Found in Database Member";
                this.ReferenceNameLabel.Text = "";
                this.ReferenceTextBox.Text = "";
            }
            else
            {
                this.ReferenceCodeHidden.Value = _msMember.MemberCode;
                this.ReferenceNameLabel.Text = _msMember.MemberName;
                this.WarningLabel.Text = "";
            }

        }
        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            DateTime _now = DateTime.Now;

            this.ClearLabel();

            this.MemberTypeDropDownList.SelectedValue = "null";
            this.MemberTitleRadioButtonList.SelectedIndex = 0;
            this.MemberCodeTextBox.Text = "";
            this.BarcodeTextBox.Text = "";
            this.GenderRadioButtonList.SelectedIndex = 0;
            this.MemberNameTextBox.Text = "";
            this.ReligionDDL.SelectedValue = "null";
            this.IdentityRadioButtonList.SelectedIndex = 0;
            this.IdentityNumberTextBox.Text = "";
            this.DateOfBirthTextBox.Text = DateFormMapper.GetValue(_now);
            this.PlaceOfBirthTextBox.Text = "";
            this.ReferenceTextBox.Text = "";
            this.ReferenceCodeHidden.Value = "";
            this.Telephone1TextBox.Text = "";
            this.Telephone2TextBox.Text = "";
            this.HandPhone1TextBox.Text = "";
            this.HandPhone2TextBox.Text = "";
            this.AddressTextBox.Text = "";
            this.CityDropDownList.SelectedValue = "null";
            this.ZipCodeTextBox.Text = "";
            this.EmailTextBox.Text = "";
            this.CompanyTextBox.Text = "";
            this.HobbyTextBox.Text = "";
            this.JobTitleListBox.SelectedIndex = 0;
            this.JobLevelListBox.SelectedIndex = 0;
            this.SalaryListBox.SelectedIndex = 0;
            this.EducationDropDownList.SelectedValue = "null";
            this.SourceInfoTextBox.Text = "";
            this.FgActiveCheckBox.Checked = false;
            this.RemarkTextBox.Text = "";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            //if (this.MemberTypeDropDownList.SelectedValue == "null")
            //{
            //    this.ClearLabel();
            //    this.WarningLabel.Text = "Member Type Must Be Filled";
            //}
            //else if (this.MemberCodeTextBox.Text.Trim() == "")
            //{
            //    this.ClearLabel();
            //    this.WarningLabel.Text = "Member Code Must Be Filled";
            //}
            //else if (this.MemberNameTextBox.Text.Trim() == "")
            //{
            //    this.ClearLabel();
            //    this.WarningLabel.Text = "Member Name Must Be Filled";
            //}

            //else if (this.ReligionDDL.SelectedValue == "null")
            //{
            //    this.ClearLabel();
            //    this.WarningLabel.Text = "Religion Must Be Filled";
            //}
            //else
            //{
             if (this.BarcodeTextBox.Text.Trim() == this.ReferenceTextBox.Text.Trim())
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Member Barcode and Reference cannot same";
            }
            else
            {
                MsMember _msMember = new MsMember();

                _msMember.MemberTypeCode = this.MemberTypeDropDownList.SelectedValue;
                _msMember.MemberTitle = Convert.ToByte(this.MemberTitleRadioButtonList.SelectedValue);
                _msMember.MemberCode = this.MemberCodeTextBox.Text;
                _msMember.Barcode = this.BarcodeTextBox.Text;
                _msMember.Gender = GenderDataMapper.GetGender(this.GenderRadioButtonList.SelectedValue);
                _msMember.MemberName = this.MemberNameTextBox.Text;
                _msMember.ReligionCode = this.ReligionDDL.SelectedValue;
                _msMember.IdentityType = Convert.ToInt32(this.IdentityRadioButtonList.SelectedValue);
                _msMember.IdentityNumber = this.IdentityNumberTextBox.Text;
                _msMember.DateOfBirth = DateFormMapper.GetValue(this.DateOfBirthTextBox.Text);
                _msMember.PlaceOfBirth = this.PlaceOfBirthTextBox.Text;
                _msMember.ReferenceCode = this.ReferenceCodeHidden.Value;
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
                _msMember.CreatedBy = HttpContext.Current.User.Identity.Name;
                _msMember.CreatedDate = DateTime.Now;

                bool _result = this._memberBL.Add(_msMember);

                if (_result == true)
                {
                    Response.Redirect(this._homePage);
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "You Failed Add Data";
                }
                //}
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