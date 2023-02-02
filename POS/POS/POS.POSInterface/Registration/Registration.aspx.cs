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

namespace POS.POSInterface.Registration
{
    public partial class Registration : System.Web.UI.Page
    {
        private MemberBL _memberBL = new MemberBL();
        private CityBL _cityBL = new CityBL();
        private MemberTypeBL _membertypeBL = new MemberTypeBL();
        private ReligionBL _religionBL = new ReligionBL();
        private EmployeeBL _employeeBL = new EmployeeBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack == true)
            {
                this.JavaScript();
                this.ShowMemberType();
                this.ShowCity();
                this.ShowReligion();

                this.ShowJobTitle();
                this.ShowJobLevel();
                this.ShowEducation();
                this.ShowRefName();

                this.ClearData();
                this.ClearLabel();
                this.SetAttribute();
                //this.BackButton.OnClientClick = "window.close()";
            }
        }

        protected void JavaScript()
        {
            this.BirthDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.DateOfBirthTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

            String spawnJS = "<script language='JavaScript'>\n";
            spawnJS += "function returnValue (val) {\n";
            spawnJS += "window.opener." + Request.QueryString["valueCatcher"] + "(val);\n";
            spawnJS += "window.close();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard MemberCode
            //spawnJS += "function MemberCodeKeyBoard(x) {\n";
            //spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findMemberCode&titleinput=Member Code&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            //spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON MemberCode
            spawnJS += "function findMemberCode(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.MemberCodeTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard Barcode
            spawnJS += "function BarcodeKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findBarcode&titleinput=Barcode&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON Barcode
            spawnJS += "function findBarcode(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.BarcodeTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard MemberName
            spawnJS += "function MemberNameKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findMemberName&titleinput=Member Name&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON MemberName
            spawnJS += "function findMemberName(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.MemberNameTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard IdentityNumber
            spawnJS += "function IdentityNumberKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findIdentityNumber&titleinput=Identity Number&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON IdentityNumber
            spawnJS += "function findIdentityNumber(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.IdentityNumberTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard Telephone1
            spawnJS += "function Telephone1KeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findTelephone1&titleinput=Phone&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON Telephone1
            spawnJS += "function findTelephone1(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.Telephone1TextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard HandPhone1
            spawnJS += "function HandPhone1KeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findHandPhone1&titleinput=Phone&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON HandPhone1
            spawnJS += "function findHandPhone1(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.HandPhone1TextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard Address
            spawnJS += "function AddressKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findAddress&titleinput=Address&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON Address
            spawnJS += "function findAddress(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.AddressTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard Company
            spawnJS += "function CompanyKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findCompany&titleinput=Company&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON Company
            spawnJS += "function findCompany(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.CompanyTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard PlaceOfBirth
            spawnJS += "function PlaceOfBirthKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findPlaceOfBirth&titleinput=Place Of Birth&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON PlaceOfBirth
            spawnJS += "function findPlaceOfBirth(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.PlaceOfBirthTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard Reference
            spawnJS += "function ReferenceKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findReference&titleinput=Reference&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON Reference
            spawnJS += "function findReference(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.ReferenceTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard Telephone2
            spawnJS += "function Telephone2KeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findTelephone2&titleinput=Phone&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON Telephone2
            spawnJS += "function findTelephone2(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.Telephone2TextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard HandPhone2
            spawnJS += "function HandPhone2KeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findHandPhone2&titleinput=Phone&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON HandPhone2
            spawnJS += "function findHandPhone2(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.HandPhone2TextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard ZipCode
            spawnJS += "function ZipCodeKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findZipCode&titleinput=ZipCode&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON ZipCode
            spawnJS += "function findZipCode(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.ZipCodeTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard Email
            spawnJS += "function EmailKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findEmail&titleinput=Email&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON Email
            spawnJS += "function findEmail(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.EmailTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard Hobby
            spawnJS += "function HobbyKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findHobby&titleinput=Hobby&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON Hobby
            spawnJS += "function findHobby(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.HobbyTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard SourceInfo
            spawnJS += "function SourceInfoKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findSourceInfo&titleinput=SourceInfo&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON SourceInfo
            spawnJS += "function findSourceInfo(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.SourceInfoTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            spawnJS += "</script>";
            this.javaScriptDeclaration.Text = spawnJS;
            //this.BackButton.OnClientClick = "window.close()";
        }

        protected void SetAttribute()
        {
            this.DateOfBirthTextBox.Attributes.Add("ReadOnly", "True");

            this.BarcodeTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.BarcodeTextBox.ClientID + "," + this.BarcodeTextBox.ClientID + ",500" + ")");
            this.IdentityNumberTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.IdentityNumberTextBox.ClientID + "," + this.IdentityNumberTextBox.ClientID + ",500" + ")");
            this.Telephone1TextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.Telephone1TextBox.ClientID + "," + this.Telephone1TextBox.ClientID + ",500" + ");");
            this.Telephone2TextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.Telephone2TextBox.ClientID + "," + this.Telephone2TextBox.ClientID + ",500" + ")");
            this.HandPhone1TextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.HandPhone1TextBox.ClientID + "," + this.HandPhone1TextBox.ClientID + ",500" + ")");
            this.HandPhone2TextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.HandPhone2TextBox.ClientID + "," + this.HandPhone2TextBox.ClientID + ",500" + ")");
            this.ReferenceTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.ReferenceTextBox.ClientID + "," + this.ReferenceTextBox.ClientID + ",500" + ")");
            this.ZipCodeTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.ZipCodeTextBox.ClientID + "," + this.ZipCodeTextBox.ClientID + ",500" + ")");

            //this.MemberCodeTextBox.Attributes["onclick"] = "MemberCodeKeyBoard(this.id)";
            this.BarcodeTextBox.Attributes["onclick"] = "BarcodeKeyBoard(this.id)";
            this.MemberNameTextBox.Attributes["onclick"] = "MemberNameKeyBoard(this.id)";
            this.IdentityNumberTextBox.Attributes["onclick"] = "IdentityNumberKeyBoard(this.id)";
            this.Telephone1TextBox.Attributes["onclick"] = "Telephone1KeyBoard(this.id)";
            this.HandPhone1TextBox.Attributes["onclick"] = "HandPhone1KeyBoard(this.id)";
            this.AddressTextBox.Attributes["onclick"] = "AddressKeyBoard(this.id)";
            this.CompanyTextBox.Attributes["onclick"] = "CompanyKeyBoard(this.id)";
            this.PlaceOfBirthTextBox.Attributes["onclick"] = "PlaceOfBirthKeyBoard(this.id)";
            this.ReferenceTextBox.Attributes["onclick"] = "ReferenceKeyBoard(this.id)";
            this.Telephone2TextBox.Attributes["onclick"] = "Telephone2KeyBoard(this.id)";
            this.HandPhone2TextBox.Attributes["onclick"] = "HandPhone2KeyBoard(this.id)";
            this.ZipCodeTextBox.Attributes["onclick"] = "ZipCodeKeyBoard(this.id)";
            this.EmailTextBox.Attributes["onclick"] = "EmailKeyBoard(this.id)";
            this.HobbyTextBox.Attributes["onclick"] = "HobbyKeyBoard(this.id)";
            this.SourceInfoTextBox.Attributes["onclick"] = "SourceInfoKeyBoard(this.id)";
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
            //if (this.JobTitleListBox.DataBinding != null)
            //{
            //    this.JobTitleListBox.Items[0].Selected = true;
            //}
        }

        protected void ShowJobLevel()
        {
            this.JobLevelListBox.Items.Clear();
            this.JobLevelListBox.DataTextField = "JobLvlName";
            this.JobLevelListBox.DataValueField = "JobLvlCode";
            this.JobLevelListBox.DataSource = this._employeeBL.GetListJobLevelForDDL();
            this.JobLevelListBox.DataBind();
            //if (this.JobLevelListBox.DataSource != null)
            //{
            //    this.JobLevelListBox.Items[0].Selected = true;
            //}
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
            this.ReferenceNameLabel.Text = "";
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
            this.ReferenceCodeHidden.Value = "";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            String _setValue = this._memberBL.GetSetValueByCode("POSMember");
            int _maxMemberCode = this._memberBL.GetMaxMemberCode();
            String _memberCode = _setValue + (_maxMemberCode + 1).ToString().PadLeft(6, '0');

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
                _msMember.MemberCode = _memberCode;
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

                bool _result = this._memberBL.Add(_msMember);

                if (_result == true)
                {
                    this.WarningLabel.Text = "You Successful Add New Member";
                    this.ClearData();
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "You Failed Add Data";
                }

            }
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
            this.ClearLabel();
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../Login.aspx");
        }
}


}