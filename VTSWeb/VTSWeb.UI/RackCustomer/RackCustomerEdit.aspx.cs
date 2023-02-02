using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VTSWeb.Database;
using VTSWeb.SystemConfig;
using VTSWeb.BusinessRule;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public partial class RackCustomerEdit : RackCustomerBase
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
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = this._pageTitleLiteral;
                this.CompanyTextBox.Attributes.Add("ReadOnly", "True");
                this.ShowData();
                this.ShowRackDDL();
                this.SaveButton.ImageUrl = "../images/save.jpg";
                this.ResetButton.ImageUrl = "../images/reset.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }
        private void ShowRackDDL()
        {
            this.RackNameDropDownList.Items.Clear();
            this.RackNameDropDownList.DataTextField = "RackName";
            this.RackNameDropDownList.DataValueField = "RackCode";
            this.RackNameDropDownList.DataSource = this._rackServer.GetListForDDL();
            this.RackNameDropDownList.DataBind();
        }
        public void ShowData()
        {
            MsRack_Customer _msRackCustomer = this._rackCustomerBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.CompanyTextBox.Text = _customerBL.GetCustomerNameByCode(_msRackCustomer.CustCode);
            this.RackNameDropDownList.SelectedValue = _msRackCustomer.RackCode;
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
        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsRack_Customer _msRackCustomer = this._rackCustomerBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msRackCustomer.RackCode = this.RackNameDropDownList.SelectedValue;

            bool _result = this._rackCustomerBL.Edit(_msRackCustomer);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }

        }
    }


}