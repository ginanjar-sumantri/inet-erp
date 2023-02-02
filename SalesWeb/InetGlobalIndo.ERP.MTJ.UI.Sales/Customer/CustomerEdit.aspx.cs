using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.Customer
{
    public partial class CustomerEdit : CustomerBase
    {
        private CustomerBL _custBL = new CustomerBL();
        private TermBL _termBL = new TermBL();
        private CurrencyBL _currBL = new CurrencyBL();
        private CityBL _cityBL = new CityBL();
        private RegionalBL _regionalBL = new RegionalBL();
        private EmployeeBL _empBL = new EmployeeBL();
        private FINReceiptTradeBL _finReceiptTradeBL = new FINReceiptTradeBL();
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

                this.ShowCityDropdownlist();
                this.ShowTermDropdownlist();
                this.ShowCustTypeDropdownlist();
                this.ShowCustGroupDropdownlist();
                this.ShowCurrDropdownlist();
                this.ShowSalesPersonDropdownlist();

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.CustLimitTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.UseLimitTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.GracePeriodTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.DueDateCycleTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");

            this.CustLimitTextBox.Attributes.Add("OnBlur", "ChangeFormat(" + this.CustLimitTextBox.ClientID + ");");
            this.UseLimitTextBox.Attributes.Add("OnBlur", "ChangeFormat(" + this.UseLimitTextBox.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ShowCityDropdownlist()
        {
            this.CityDropDownList.Items.Clear();
            this.CityDropDownList.DataSource = this._cityBL.GetListCityForDDL();
            this.CityDropDownList.DataValueField = "CityCode";
            this.CityDropDownList.DataTextField = "CityName";
            this.CityDropDownList.DataBind();
            this.CityDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowCustomerDropdownlist()
        {
            this.BillToDropDownList.Items.Clear();
            this.BillToDropDownList.DataSource = this._custBL.GetListCustomerForDDL();
            this.BillToDropDownList.DataValueField = "CustCode";
            this.BillToDropDownList.DataTextField = "CustName";
            this.BillToDropDownList.DataBind();
            this.BillToDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowTermDropdownlist()
        {
            this.TermDropDownList.Items.Clear();
            this.TermDropDownList.DataSource = this._termBL.GetListTermForDDL();
            this.TermDropDownList.DataValueField = "TermCode";
            this.TermDropDownList.DataTextField = "TermName";
            this.TermDropDownList.DataBind();
            this.TermDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowCustGroupDropdownlist()
        {
            this.CustGroupDropDownList.Items.Clear();
            this.CustGroupDropDownList.DataSource = this._custBL.GetListCustGroupForDDL();
            this.CustGroupDropDownList.DataValueField = "CustGroupCode";
            this.CustGroupDropDownList.DataTextField = "CustGroupName";
            this.CustGroupDropDownList.DataBind();
            this.CustGroupDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowCustTypeDropdownlist()
        {
            this.CustTypeDropDownList.Items.Clear();
            this.CustTypeDropDownList.DataSource = this._custBL.GetListCustTypeForDDL();
            this.CustTypeDropDownList.DataValueField = "CustTypeCode";
            this.CustTypeDropDownList.DataTextField = "CustTypeName";
            this.CustTypeDropDownList.DataBind();
            this.CustTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowCurrDropdownlist()
        {
            this.CurrDropDownList.Items.Clear();
            this.CurrDropDownList.DataSource = this._currBL.GetListAll();
            this.CurrDropDownList.DataValueField = "CurrCode";
            this.CurrDropDownList.DataTextField = "CurrCode";
            this.CurrDropDownList.DataBind();
            this.CurrDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowSalesPersonDropdownlist()
        {
            this.SalesPersonDropDownList.Items.Clear();
            this.SalesPersonDropDownList.DataSource = this._empBL.GetListEmpSalesForDDL();
            this.SalesPersonDropDownList.DataValueField = "EmpNumb";
            this.SalesPersonDropDownList.DataTextField = "EmpName";
            this.SalesPersonDropDownList.DataBind();
            this.SalesPersonDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowData()
        {
            MsCustomer _msCustomer = this._custBL.GetSingleCust(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            MsCustCollect _msCustCollect = this._custBL.GetSingleCustCollect(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            Master_CustomerExtension _msCustExtension = this._custBL.GetSingleCustExtension(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.CustCodeTextBox.Text = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            this.CustNameTextBox.Text = _msCustomer.CustName;
            this.CustGroupDropDownList.SelectedValue = _msCustomer.CustGroup;
            this.CustTypeDropDownList.SelectedValue = _msCustomer.CustType;
            this.Addr1TextBox.Text = _msCustomer.Address1;
            this.Addr2TextBox.Text = _msCustomer.Address2;
            this.CityDropDownList.SelectedValue = _msCustomer.City;
            this.ZipCodeTextBox.Text = _msCustomer.ZipCode;
            this.PhoneTextBox.Text = _msCustomer.Phone;
            this.FaxTextBox.Text = _msCustomer.Fax;
            this.EmailTextBox.Text = _msCustomer.Email;
            this.CurrDropDownList.SelectedValue = _msCustomer.CurrCode;
            this.TermDropDownList.SelectedValue = _msCustomer.Term;
            this.NPWPTextBox.Text = _msCustomer.NPWP;
            this.NPPKPTextBox.Text = _msCustomer.NPPKP;
            this.NPWPAddressTextBox.Text = _msCustomer.NPWPAddress;
            this.IsActiveCheckBox.Checked = CustomerDataMapper.IsActive(_msCustomer.FgActive);
            this.ContactNameTextBox.Text = _msCustomer.ContactName;
            this.ContactTitleTextBox.Text = _msCustomer.ContactTitle;
            this.ContactEmailTextBox.Text = _msCustomer.ContactEmail;
            this.ContactHPTextBox.Text = _msCustomer.ContactHP;
            this.FgLimitCheckBox.Checked = CustomerDataMapper.IsLimit(_msCustomer.FgLimit);
            this.CustLimitTextBox.Text = (_msCustomer.CustLimit == 0) ? "0" : Convert.ToDecimal(_msCustomer.CustLimit).ToString("#,###.##");
            this.UseLimitTextBox.Text = (_msCustomer.UseLimit == 0) ? "0" : Convert.ToDecimal(_msCustomer.UseLimit).ToString("#,###.##");
            this.GracePeriodTextBox.Text = Convert.ToString(_msCustomer.GracePeriod);
            this.PrintFPCheckBox.Checked = Convert.ToBoolean(_msCustomer.PrintFP);
            this.StampCheckBox.Checked = Convert.ToBoolean(_msCustomer.FgStamp);
            this.FgPPNCheckBox.Checked = CustomerDataMapper.IsPPN(_msCustomer.FgPPN);
            this.DueDateCycleTextBox.Text = _msCustomer.DueDateCycle.ToString();
            this.RemarkTextBox.Text = _msCustomer.Remark;
            if (this.FgPPNCheckBox.Checked == false)
            {
                this.NPWPTextBox.Attributes.Add("ReadOnly", "True");
                this.NPWPTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.NPPKPTextBox.Attributes.Add("ReadOnly", "True");
                this.NPPKPTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.NPWPAddressTextBox.Attributes.Add("ReadOnly", "True");
                this.NPWPAddressTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            }
            else
            {
                this.NPWPTextBox.Attributes.Remove("ReadOnly");
                this.NPWPTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                this.NPPKPTextBox.Attributes.Remove("ReadOnly");
                this.NPPKPTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                this.NPWPAddressTextBox.Attributes.Remove("ReadOnly");
                this.NPWPAddressTextBox.Attributes.Add("style", "background-color:#FFFFFF");

            }

            if (CustomerDataMapper.IsPPN(_msCustomer.FgPPN) == true)
            {
                this.PrintFPCheckBox.Enabled = true;
            }
            else
            {
                this.PrintFPCheckBox.Enabled = false;
            }

            if (_msCustCollect != null)
            {
                this.ShowCustomerDropdownlist();
                this.BillToDropDownList.SelectedValue = _msCustCollect.CustCollect;
            }
            else
            {
                this.ShowCustomerDropdownlist();
                this.BillToDropDownList.SelectedValue = "null";
            }

            if (_msCustExtension != null)
            {
                this.SalesPersonDropDownList.SelectedValue = _msCustExtension.EmpNumb;
            }
            else
            {
                this.SalesPersonDropDownList.SelectedValue = "null";
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsCustomer _msCustomer = this._custBL.GetSingleCust(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msCustomer.CustName = this.CustNameTextBox.Text;
            _msCustomer.CustGroup = this.CustGroupDropDownList.SelectedValue;
            _msCustomer.CustType = this.CustTypeDropDownList.SelectedValue;
            _msCustomer.Address1 = this.Addr1TextBox.Text;
            _msCustomer.Address2 = this.Addr2TextBox.Text;
            _msCustomer.City = this.CityDropDownList.SelectedValue;
            _msCustomer.ZipCode = this.ZipCodeTextBox.Text;
            _msCustomer.Phone = this.PhoneTextBox.Text;
            _msCustomer.Fax = this.FaxTextBox.Text;
            _msCustomer.Email = this.EmailTextBox.Text;
            _msCustomer.CurrCode = this.CurrDropDownList.SelectedValue;
            _msCustomer.Term = this.TermDropDownList.SelectedValue;
            _msCustomer.NPWP = this.NPWPTextBox.Text;
            _msCustomer.NPPKP = this.NPPKPTextBox.Text;
            _msCustomer.NPWPAddress = this.NPWPAddressTextBox.Text;
            _msCustomer.FgActive = CustomerDataMapper.IsActive(this.IsActiveCheckBox.Checked);
            _msCustomer.ContactName = this.ContactNameTextBox.Text;
            _msCustomer.ContactTitle = this.ContactTitleTextBox.Text;
            _msCustomer.ContactEmail = this.ContactEmailTextBox.Text;
            _msCustomer.ContactHP = this.ContactHPTextBox.Text;
            _msCustomer.FgLimit = CustomerDataMapper.IsLimit(this.FgLimitCheckBox.Checked);
            _msCustomer.PrintFP = this.PrintFPCheckBox.Checked;
            _msCustomer.FgStamp = this.StampCheckBox.Checked;
            _msCustomer.FgPPN = CustomerDataMapper.IsPPN(this.FgPPNCheckBox.Checked);
            _msCustomer.Remark = this.RemarkTextBox.Text;
            if (this.CustLimitTextBox.Text != "")
            {
                _msCustomer.CustLimit = Convert.ToDecimal(this.CustLimitTextBox.Text);
            }
            if (this.UseLimitTextBox.Text != "")
            {
                _msCustomer.UseLimit = Convert.ToDecimal(this.UseLimitTextBox.Text);
            }
            _msCustomer.GracePeriod = (this.GracePeriodTextBox.Text != "") ? Convert.ToInt32(this.GracePeriodTextBox.Text) : 0;

            _msCustomer.FgPPN = CustomerDataMapper.IsPPN(this.FgPPNCheckBox.Checked);
            _msCustomer.PrintFP = this.PrintFPCheckBox.Checked;
            _msCustomer.DueDateCycle = (this.DueDateCycleTextBox.Text == "") ? Convert.ToByte(1) : Convert.ToByte(this.DueDateCycleTextBox.Text);
            _msCustomer.UserID = HttpContext.Current.User.Identity.Name;
            _msCustomer.UserDate = DateTime.Now;

            MsCustCollect _msCustCollect = null;

            if (this.BillToDropDownList.SelectedValue != "null")
            {
                _msCustCollect = new MsCustCollect();

                _msCustCollect.CustCode = this.CustCodeTextBox.Text;
                _msCustCollect.CustCollect = this.BillToDropDownList.SelectedValue;
                _msCustCollect.UserID = HttpContext.Current.User.Identity.Name;
                _msCustCollect.UserDate = DateTime.Now;
            }

            Master_CustomerExtension _msCustExtension = null;

            if (this.SalesPersonDropDownList.SelectedValue != "null")
            {
                _msCustExtension = new Master_CustomerExtension();

                _msCustExtension.CustCode = this.CustCodeTextBox.Text;
                _msCustExtension.EmpNumb = this.SalesPersonDropDownList.SelectedValue;
                _msCustExtension.InsertBy = HttpContext.Current.User.Identity.Name;
                _msCustExtension.InsertDate = DateTime.Now;
                _msCustExtension.EditBy = HttpContext.Current.User.Identity.Name;
                _msCustExtension.EditDate = DateTime.Now;
            }

            if (this._custBL.EditCust(_msCustomer, _msCustCollect, _msCustExtension) == true)
            {
                Response.Redirect(this._homePage + "?" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(ApplicationConfig.RequestPageKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }

        protected void FgPPNCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.FgPPNCheckBox.Checked == true)
            {
                this.PrintFPCheckBox.Enabled = true;
                this.NPWPTextBox.Attributes.Remove("ReadOnly");
                this.NPWPTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                this.NPPKPTextBox.Attributes.Remove("ReadOnly");
                this.NPPKPTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                this.NPWPAddressTextBox.Attributes.Remove("ReadOnly");
                this.NPWPAddressTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            }
            else
            {
                this.PrintFPCheckBox.Checked = false;
                this.PrintFPCheckBox.Enabled = false;
                this.NPWPTextBox.Text = "";
                this.NPWPTextBox.Attributes.Add("ReadOnly", "True");
                this.NPWPTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.NPPKPTextBox.Text = "";
                this.NPPKPTextBox.Attributes.Add("ReadOnly", "True");
                this.NPPKPTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.NPWPAddressTextBox.Text = "";
                this.NPWPAddressTextBox.Attributes.Add("ReadOnly", "True");
                this.NPWPAddressTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            }
        }

        protected void PrintFPCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.PrintFPCheckBox.Checked == true)
            {
                this.NPWPRequiredFieldValidator.Enabled = true;
                this.NPPKPRequiredFieldValidator.Enabled = true;
                this.NPWPAddressRequiredFieldValidator.Enabled = true;
            }
            else
            {
                this.NPWPRequiredFieldValidator.Enabled = false;
                this.NPPKPRequiredFieldValidator.Enabled = false;
                this.NPWPAddressRequiredFieldValidator.Enabled = false;
            }
        }

        protected void CustGroupDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            MsCustomer _msCustomer = this._custBL.GetSingleCust(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_finReceiptTradeBL.IsCustCodeExists(this.CustCodeTextBox.Text) == true)
            {
                this.CustGroupDropDownList.Enabled = false;
                this.WarningLabel.Text = "Can't Change Customer Group Because Customer Is Already in ARPosting";
                this.CustGroupDropDownList.SelectedValue = _msCustomer.CustGroup;
            }
            else
            {
                this.CustGroupDropDownList.Enabled = true;
            }
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.Page.IsValid == true)
            {
                MsCustomer _msCustomer = this._custBL.GetSingleCust(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _msCustomer.CustName = this.CustNameTextBox.Text;
                _msCustomer.CustGroup = this.CustGroupDropDownList.SelectedValue;
                _msCustomer.CustType = this.CustTypeDropDownList.SelectedValue;
                _msCustomer.Address1 = this.Addr1TextBox.Text;
                _msCustomer.Address2 = this.Addr2TextBox.Text;
                _msCustomer.City = this.CityDropDownList.SelectedValue;
                _msCustomer.ZipCode = this.ZipCodeTextBox.Text;
                _msCustomer.Phone = this.PhoneTextBox.Text;
                _msCustomer.Fax = this.FaxTextBox.Text;
                _msCustomer.Email = this.EmailTextBox.Text;
                _msCustomer.CurrCode = this.CurrDropDownList.SelectedValue;
                _msCustomer.Term = this.TermDropDownList.SelectedValue;
                _msCustomer.NPWP = this.NPWPTextBox.Text;
                _msCustomer.NPPKP = this.NPPKPTextBox.Text;
                _msCustomer.NPWPAddress = this.NPWPAddressTextBox.Text;
                _msCustomer.FgActive = CustomerDataMapper.IsActive(this.IsActiveCheckBox.Checked);
                _msCustomer.ContactName = this.ContactNameTextBox.Text;
                _msCustomer.ContactTitle = this.ContactTitleTextBox.Text;
                _msCustomer.ContactEmail = this.ContactEmailTextBox.Text;
                _msCustomer.ContactHP = this.ContactHPTextBox.Text;
                _msCustomer.FgLimit = CustomerDataMapper.IsLimit(this.FgLimitCheckBox.Checked);
                _msCustomer.PrintFP = this.PrintFPCheckBox.Checked;
                _msCustomer.FgStamp = this.StampCheckBox.Checked;
                _msCustomer.FgPPN = CustomerDataMapper.IsPPN(this.FgPPNCheckBox.Checked);
                _msCustomer.Remark = this.RemarkTextBox.Text;

                if (this.CustLimitTextBox.Text != "")
                {
                    _msCustomer.CustLimit = Convert.ToDecimal(this.CustLimitTextBox.Text);
                }
                if (this.UseLimitTextBox.Text != "")
                {
                    _msCustomer.UseLimit = Convert.ToDecimal(this.UseLimitTextBox.Text);
                }
                _msCustomer.GracePeriod = (this.GracePeriodTextBox.Text != "") ? Convert.ToInt32(this.GracePeriodTextBox.Text) : 0;

                _msCustomer.FgPPN = CustomerDataMapper.IsPPN(this.FgPPNCheckBox.Checked);
                _msCustomer.PrintFP = this.PrintFPCheckBox.Checked;
                _msCustomer.DueDateCycle = (this.DueDateCycleTextBox.Text == "") ? Convert.ToByte(1) : Convert.ToByte(this.DueDateCycleTextBox.Text);
                _msCustomer.UserID = HttpContext.Current.User.Identity.Name;
                _msCustomer.UserDate = DateTime.Now;

                MsCustCollect _msCustCollect = null;

                if (this.BillToDropDownList.SelectedValue != "null")
                {
                    _msCustCollect = new MsCustCollect();

                    _msCustCollect.CustCode = this.CustCodeTextBox.Text;
                    _msCustCollect.CustCollect = this.BillToDropDownList.SelectedValue;
                    _msCustCollect.UserID = HttpContext.Current.User.Identity.Name;
                    _msCustCollect.UserDate = DateTime.Now;
                }

                Master_CustomerExtension _msCustExtension = null;

                if (this.SalesPersonDropDownList.SelectedValue != "null")
                {
                    _msCustExtension = new Master_CustomerExtension();

                    _msCustExtension.CustCode = this.CustCodeTextBox.Text;
                    _msCustExtension.EmpNumb = this.SalesPersonDropDownList.SelectedValue;
                    _msCustExtension.InsertBy = HttpContext.Current.User.Identity.Name;
                    _msCustExtension.InsertDate = DateTime.Now;
                    _msCustExtension.EditBy = HttpContext.Current.User.Identity.Name;
                    _msCustExtension.EditDate = DateTime.Now;
                }

                if (this._custBL.EditCust(_msCustomer, _msCustCollect, _msCustExtension) == true)
                {
                    Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.WarningLabel.Text = "Your Failed Edit Data";
                }
            }
        }
    }
}