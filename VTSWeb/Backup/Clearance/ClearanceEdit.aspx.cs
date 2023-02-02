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
using VTSWeb.DataMapping;
using VTSweb.DataMapping;

namespace VTSWeb.UI
{
    public partial class ClearanceEdit : ClearanceBase
    {
        private ClearanceBL _ClearanceBL = new ClearanceBL();
        private MsCustomerBL _customerBL = new MsCustomerBL();
        private MsCustContactBL _custContactBL = new MsCustContactBL();


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
                this.SaveButton.ImageUrl = "../images/Save.jpg";
                this.CancelButton.ImageUrl = "../images/Cancel.jpg";
                this.ResetButton.ImageUrl = "../images/Reset.jpg";
                this.ViewDetailButton.ImageUrl = "../images/view_detail.jpg";
                
                this.ShowCustomerContact();
                this.ShowContactName();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }
        public void ClearData()
        {
            this.RemarkTextBox.Text="";
        }
        public void ShowCustomerContact()
        {
            this.CustomerDropDownList.Items.Clear();
            this.CustomerDropDownList.DataTextField = "CustName";
            this.CustomerDropDownList.DataValueField = "CustCode";
            this.CustomerDropDownList.DataSource = this._customerBL.GetCustomerForDDL();
            this.CustomerDropDownList.DataBind();
            this.CustomerDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }
        protected void CustomerDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowContactName();
        }
        public void ShowContactName()
        {
            this.ContactNameDropDownList.Items.Clear();
            this.ContactNameDropDownList.DataTextField = "ContactName";
            this.ContactNameDropDownList.DataValueField = "ItemNo";
            this.ContactNameDropDownList.DataSource = this._custContactBL.GetContactNameForClearance(this.CustomerDropDownList.SelectedValue);
            this.ContactNameDropDownList.DataBind();
            this.ContactNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            this._ClearanceBL = new ClearanceBL();
            ClearanceHd _ClearanceHd = this._ClearanceBL.GetSingleClearance(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            String _Clearance = _ClearanceHd.CustomerCode.ToString() + "-" + _ClearanceHd.VisitorCode.ToString();  

            this.NoTextBox.Text = _ClearanceHd.ClearanceCode;
            this.DateTextBox.Text = Convert.ToString(_ClearanceHd.ClearanceDate);
            this.CustomerDropDownList.SelectedValue = _ClearanceHd.CustomerCode;
            this.VistorTextBox.Text = (this._custContactBL.GetSingleContactName(_Clearance)).ContactName;
            this.RemarkTextBox.Text = _ClearanceHd.Remark;
            this.StatusLabel.Text = ClearanceCompleteDataMapping.GetStatus(_ClearanceHd.CompleteStatus);

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
        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            ClearanceHd _ClearanceHd = this._ClearanceBL.GetSingleClearance(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _ClearanceHd.CustomerCode = this.CustomerDropDownList.SelectedValue;
            _ClearanceHd.VisitorCode = Convert.ToInt32(this.ContactNameDropDownList.SelectedValue);
            _ClearanceHd.Remark = this.RemarkTextBox.Text;

            bool _result = this._ClearanceBL.Edit(_ClearanceHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }

        }
    }


}