﻿using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using System.Web;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.GenerateBillingInvoice
{
    public partial class GenerateBillingInvoice : GenerateBillingInvoiceBase 
    {
        private BillingInvoiceBL _invoiceServiceBL = new BillingInvoiceBL();
        private CustomerBL _customerBL = new CustomerBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _confirmationMessage = "Are you sure want to generate billing invoice?";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }
            
            this._permGenerate = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Generate);

            if (this._permGenerate == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.GenerateButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/generate.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ClearLabel();
                this.SetAttribute();

                this.ShowCustomerGroup();
                this.ShowCustomerType();

                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.PeriodTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.PeriodTextBox.Attributes.Add("OnBlur", " ValidatePeriod(" + this.PeriodTextBox.ClientID + ");");

            this.YearTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.YearTextBox.Attributes.Add("OnBlur", " ValidateYear(" + this.YearTextBox.ClientID + ");");

            this.GenerateButton.Attributes.Add("OnClick", "return Confirmation('" + _confirmationMessage + "');");
        }


        protected void ShowCustomerGroup()
        {
            this.CustomerGroupDropDownList.Items.Clear();
            this.CustomerGroupDropDownList.DataSource = this._customerBL.GetListCustGroupForDDL();
            this.CustomerGroupDropDownList.DataValueField = "CustGroupCode";
            this.CustomerGroupDropDownList.DataTextField = "CustGroupName";
            this.CustomerGroupDropDownList.DataBind();
            this.CustomerGroupDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCustomerType()
        {
            this.CustomerTypeDropDownList.Items.Clear();
            this.CustomerTypeDropDownList.DataSource = this._customerBL.GetListCustTypeForDDL();
            this.CustomerTypeDropDownList.DataValueField = "CustTypeCode";
            this.CustomerTypeDropDownList.DataTextField = "CustTypeName";
            this.CustomerTypeDropDownList.DataBind();
            this.CustomerTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearData()
        {
            DateTime _now = DateTime.Now;

            this.PeriodTextBox.Text = _now.Month.ToString();
            this.YearTextBox.Text = _now.Year.ToString();
            this.CustomerGroupDropDownList.SelectedValue = "null";
            this.CustomerTypeDropDownList.SelectedValue = "null";

            this.CustomerDropDownList.Items.Clear();
            this.CustomerDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCustomer()
        {
            string _tempCustomerGroup = (this.CustomerGroupDropDownList.SelectedValue == "null") ? "" : this.CustomerGroupDropDownList.SelectedValue;
            string _tempCustomerType = (this.CustomerTypeDropDownList.SelectedValue == "null") ? "" : this.CustomerTypeDropDownList.SelectedValue;

            this.CustomerDropDownList.Items.Clear();
            this.CustomerDropDownList.DataSource = this._customerBL.GetListDDLCustomer(_tempCustomerGroup, _tempCustomerType);
            this.CustomerDropDownList.DataValueField = "CustCode";
            this.CustomerDropDownList.DataTextField = "CustName";
            this.CustomerDropDownList.DataBind();
            this.CustomerDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void CustomerGroupDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowCustomer();
        }

        protected void CustomerTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowCustomer();
        }

        protected void GenerateButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.PeriodTextBox.Text != "" && this.YearTextBox.Text != "")
            {
                string _tempCustomerGroup = (this.CustomerGroupDropDownList.SelectedValue == "null") ? "" : this.CustomerGroupDropDownList.SelectedValue;
                string _tempCustomerType = (this.CustomerTypeDropDownList.SelectedValue == "null") ? "" : this.CustomerTypeDropDownList.SelectedValue;
                string _tempCustomer = (this.CustomerDropDownList.SelectedValue == "null") ? "" : this.CustomerDropDownList.SelectedValue;

                string _result = this._invoiceServiceBL.Generate(Convert.ToInt32(this.PeriodTextBox.Text), Convert.ToInt32(this.YearTextBox.Text), _tempCustomerGroup, _tempCustomerType, _tempCustomer);

                if (_result == "")
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "You Success Generate Billing Invoice";
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = _result;
                }
            }
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }


    }
}