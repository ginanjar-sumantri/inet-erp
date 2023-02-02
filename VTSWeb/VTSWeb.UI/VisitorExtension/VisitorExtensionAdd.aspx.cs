using System;
using System.Web;
using System.Web.UI;
using VTSWeb.Database;
using VTSWeb.BusinessRule;
using VTSWeb.SystemConfig;
using VTSWeb.Common;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data.SqlClient;
using System.IO;
using System.Data.Linq;
using VTSWeb.Foto;

namespace VTSWeb.UI
{
    public partial class VisitorExtensionAdd : VisitorExtensionBase
    {
        private MsVisitorExtensionBL _VisitorExtensionBL = new MsVisitorExtensionBL();
        private MsCustomerBL _customerBL = new MsCustomerBL();
        private MsCustContactBL _custContactBL = new MsCustContactBL();
        private FotoAdd _fotoAdd = new FotoAdd();

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
            this.UploadLabel.Text = "Photo dimension : Width " + ApplicationConfig.ImageWidth + " pixels x Height " + ApplicationConfig.ImageHeight + " pixels, File size limit: " + (Convert.ToDecimal(ApplicationConfig.ImageMaxSize) / 1024).ToString() + " KBytes";

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

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            master_CustContactExtension _msCustContactExtension = new master_CustContactExtension();

            _msCustContactExtension.CustCode = this.CustomerDropDownList.SelectedValue;
            _msCustContactExtension.ItemNo = Convert.ToInt32(this.ContactNameDropDownList.SelectedValue);

            String _imagepath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + @"\PhotoImages\";
            String _result = this._fotoAdd.Add(_msCustContactExtension, this.FotoUpLoad, _imagepath);

            if (_result =="")
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = _result;
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