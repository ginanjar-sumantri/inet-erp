using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.Customer
{
    public partial class CustomerAdd : CustomerBase
    {
        private CustomerBL _customerBL = new CustomerBL();
        private TermBL _termBL = new TermBL();
        private CurrencyBL _currBL = new CurrencyBL();
        private CityBL _cityBL = new CityBL();
        private RegionalBL _regionalBL = new RegionalBL();
        private EmployeeBL _empBL = new EmployeeBL();
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
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowCityDropdownlist();
                this.ShowTermDropdownlist();
                this.ShowCustTypeDropdownlist();
                this.ShowCustGroupDropdownlist();
                this.ShowCurrDropdownlist();
                this.ShowCustomerDropdownlist();
                this.ShowSalesPersonDropdownlist();

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
            this.NPWPTextBox.Attributes.Add("ReadOnly", "True");
            this.NPPKPTextBox.Attributes.Add("ReadOnly", "True");
            this.NPWPAddressTextBox.Attributes.Add("ReadOnly", "True");

            this.CustLimitTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.UseLimitTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.GracePeriodTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.DueDateCycleTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");

            this.CustLimitTextBox.Attributes.Add("OnBlur", "ChangeFormat(" + this.CustLimitTextBox.ClientID + ");");
            this.UseLimitTextBox.Attributes.Add("OnBlur", "ChangeFormat(" + this.UseLimitTextBox.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearData()
        {
            this.ClearLabel();

            this.Addr1TextBox.Text = "";
            this.Addr2TextBox.Text = "";
            this.BillToDropDownList.SelectedValue = "null";
            this.CityDropDownList.SelectedValue = "null";
            this.ContactEmailTextBox.Text = "";
            this.ContactHPTextBox.Text = "";
            this.ContactNameTextBox.Text = "";
            this.ContactTitleTextBox.Text = "";
            this.CurrDropDownList.SelectedValue = "null";
            this.CustCodeTextBox.Text = "";
            this.CustGroupDropDownList.SelectedValue = "null";
            this.CustLimitTextBox.Text = "0";
            this.CustNameTextBox.Text = "";
            this.CustTypeDropDownList.SelectedValue = "null";
            this.DueDateCycleTextBox.Text = "1";
            this.EmailTextBox.Text = "";
            this.FaxTextBox.Text = "";
            this.FgLimitCheckBox.Checked = false;
            this.FgPPNCheckBox.Checked = false;
            this.GracePeriodTextBox.Text = "";
            this.IsActiveCheckBox.Checked = true;
            this.NPWPTextBox.Text = "";
            this.NPPKPTextBox.Text = "";
            this.PhoneTextBox.Text = "";
            this.PrintFPCheckBox.Checked = false;
            this.StampCheckBox.Checked = false;
            this.SalesPersonDropDownList.SelectedValue = "null";
            this.TermDropDownList.SelectedValue = "null";
            this.UseLimitTextBox.Text = "0";
            this.ZipCodeTextBox.Text = "";
            this.RemarkTextBox.Text = "";
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
            this.BillToDropDownList.DataSource = this._customerBL.GetListCustomerForDDL();
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
            this.CustGroupDropDownList.DataSource = this._customerBL.GetListCustGroupForDDL();
            this.CustGroupDropDownList.DataValueField = "CustGroupCode";
            this.CustGroupDropDownList.DataTextField = "CustGroupName";
            this.CustGroupDropDownList.DataBind();
            this.CustGroupDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowCustTypeDropdownlist()
        {
            this.CustTypeDropDownList.Items.Clear();
            this.CustTypeDropDownList.DataSource = this._customerBL.GetListCustTypeForDDL();
            this.CustTypeDropDownList.DataValueField = "CustTypeCode";
            this.CustTypeDropDownList.DataTextField = "CustTypeName";
            this.CustTypeDropDownList.DataBind();
            this.CustTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowCurrDropdownlist()
        {
            this.BillToDropDownList.Items.Clear();
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

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsCustomer _msCustomer = new MsCustomer();

            _msCustomer.CustCode = this.CustCodeTextBox.Text;
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
            _msCustomer.CustVerificationCode = (new Random().Next(000000, 999999)).ToString().PadLeft(6,'0');

            if (this.CustLimitTextBox.Text != "")
            {
                _msCustomer.CustLimit = Convert.ToDecimal(this.CustLimitTextBox.Text);
            }
            if (this.UseLimitTextBox.Text != "")
            {
                _msCustomer.UseLimit = Convert.ToDecimal(this.UseLimitTextBox.Text);
            }
            if (this.GracePeriodTextBox.Text != "")
            {
                _msCustomer.GracePeriod = Convert.ToInt32(this.GracePeriodTextBox.Text);
            }
            _msCustomer.FgPPN = CustomerDataMapper.IsPPN(this.FgPPNCheckBox.Checked);
            _msCustomer.PrintFP = this.PrintFPCheckBox.Checked;
            _msCustomer.FgStamp = this.StampCheckBox.Checked;
            _msCustomer.Remark = this.RemarkTextBox.Text;
            _msCustomer.UserID = HttpContext.Current.User.Identity.Name;
            _msCustomer.UserDate = DateTime.Now;
            _msCustomer.DueDateCycle = (this.DueDateCycleTextBox.Text == "") ? Convert.ToByte(1) : Convert.ToByte(this.DueDateCycleTextBox.Text);

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
            if (this._customerBL.AddCust(_msCustomer, _msCustCollect, _msCustExtension) == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data";
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
    }
}