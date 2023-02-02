using System;
using System.Web;
using System.Web.UI;
using VTSWeb.Database;
using VTSWeb.BusinessRule;
using VTSWeb.SystemConfig;
using VTSWeb.Common;
using System.Web.UI.WebControls;

namespace VTSWeb.UI
{
    public partial class RackCustomerAdd : RackCustomerBase
    {
        private RackCustomerBL _rackCustomerBL = new RackCustomerBL();
        private MsCustomerBL _customerBL = new MsCustomerBL();
        private MsRackServerBL _rackServer = new MsRackServerBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
            if (cookie == null)
            {
                Response.Redirect("..\\Login.aspx");
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.ShowCustomerDDL();
                this.ShowRackDDL();
                this.ClearLabel();
                this.ClearData();

                this.SaveButton.ImageUrl = "../images/save.jpg";
                this.ResetButton.ImageUrl = "../images/reset.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }
        public void ClearData()
        {
            this.CustomerDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.RackNameDropDownList.Items.Insert(0, new ListItem("[Choose One] ", "null"));

        }

        public void ShowCustomerDDL()
        {
            this.CustomerDropDownList.Items.Clear();
            this.CustomerDropDownList.DataTextField = "CustName";
            this.CustomerDropDownList.DataValueField = "CustCode";
            this.CustomerDropDownList.DataSource = this._customerBL.GetCustomerForDDL();
            this.CustomerDropDownList.DataBind();
        }
        //protected void CustomerDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.ShowContactNameDDL();
        //}
        //private void ShowContactNameDDL()
        //{
        //    this.ContactNameDropDownList.Items.Clear();
        //    this.ContactNameDropDownList.DataTextField = "ContactName";
        //    this.ContactNameDropDownList.DataValueField = "ItemNo";
        //    this.ContactNameDropDownList.DataSource = this._custContactBL.GetContactNameForDDL(this.CustomerDropDownList.SelectedValue);
        //    this.ContactNameDropDownList.DataBind();
        //    this.ContactNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

        //}
        private void ShowRackDDL()
        {
            this.RackNameDropDownList.Items.Clear();
            this.RackNameDropDownList.DataTextField = "RackName";
            this.RackNameDropDownList.DataValueField = "RackCode";
            this.RackNameDropDownList.DataSource = this._rackServer.GetListForDDL();
            this.RackNameDropDownList.DataBind();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsRack_Customer _msRackCustomer = new MsRack_Customer();

            _msRackCustomer.RackCustomerCode = Guid.NewGuid();
            _msRackCustomer.CustCode = this.CustomerDropDownList.SelectedValue;
            _msRackCustomer.RackCode = this.RackNameDropDownList.SelectedValue;

            bool _result = this._rackCustomerBL.AddRackCustomer(_msRackCustomer);

            if (_result == true)
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
            this.ClearLabel();
            this.ClearData();
        }
    }
}