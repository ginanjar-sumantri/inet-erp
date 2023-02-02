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
    public partial class VisitorAdd : VisitorBase
    {
        private MsCustContact_AreaBL _custContactAreaBL = new MsCustContact_AreaBL();
        private MsCustomerBL _customerBL = new MsCustomerBL();
        private MsCustContactBL _custContactBL = new MsCustContactBL();
        private MsAreaBL _areaBL = new MsAreaBL();

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
                this.ShowContactNameDDL();
                this.ShowAreaDDL();
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
            this.AreaNameDropDownList.Items.Insert(0, new ListItem("[Choose One] ", "null"));
            
        }

        public void ShowCustomerDDL()
        {
            this.CustomerDropDownList.Items.Clear();
            this.CustomerDropDownList.DataTextField = "CustName";
            this.CustomerDropDownList.DataValueField = "CustCode";
            this.CustomerDropDownList.DataSource = this._customerBL.GetCustomerForDDL();
            this.CustomerDropDownList.DataBind();
        }
        protected void CustomerDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowContactNameDDL();
        }
        private void ShowContactNameDDL()
        {
            this.ContactNameDropDownList.Items.Clear();
            this.ContactNameDropDownList.DataTextField = "ContactName";
            this.ContactNameDropDownList.DataValueField = "ItemNo";
            this.ContactNameDropDownList.DataSource = this._custContactBL.GetContactNameForDDL(this.CustomerDropDownList.SelectedValue);
            this.ContactNameDropDownList.DataBind();
            this.ContactNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            
        }
        private void ShowAreaDDL()
        {
            this.AreaNameDropDownList.Items.Clear();
            this.AreaNameDropDownList.DataTextField = "AreaName";
            this.AreaNameDropDownList.DataValueField = "AreaCode";
            this.AreaNameDropDownList.DataSource = this._areaBL.GetAreaForDDL();
            this.AreaNameDropDownList.DataBind();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsCustContact_MsArea _msCustContactArea = new MsCustContact_MsArea();

            _msCustContactArea.VisitorAreaCode = Guid.NewGuid();
            _msCustContactArea.CustCode = this.CustomerDropDownList.SelectedValue;
            _msCustContactArea.ItemNo = Convert.ToInt32(this.ContactNameDropDownList.SelectedValue);
            _msCustContactArea.AreaCode = this.AreaNameDropDownList.SelectedValue;

            bool _result = this._custContactAreaBL.AddCustContactArea(_msCustContactArea);

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