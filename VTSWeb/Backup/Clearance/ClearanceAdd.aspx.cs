using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
using System.Data.SqlClient;
using System.IO;
using System.Data.Linq;
using VTSWeb.Foto;
using VTSWeb.BusinessRule;
using VTSWeb.Database;
using VTSWeb.SystemConfig;
using VTSWeb.Common;
using VTSWeb.DataMapping;
using VTSweb.DataMapping;

namespace VTSWeb.UI
{
    public partial class ClearanceAdd : ClearanceBase
    {
        private ClearanceBL _clearanceBL = new ClearanceBL();
        private MsCustomerBL _customerBL = new MsCustomerBL();
        private MsCustContactBL _custContactBL = new MsCustContactBL();
        private MsVisitorExtensionBL _VisitorExtensionBL = new MsVisitorExtensionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
            if (cookie == null)
            {
                Response.Redirect("..\\Login.aspx");
            }
            this.Panel1.Visible = false;
            
            if (!this.Page.IsPostBack == true)
            {
                DateTime _date = DateTime.Now;
                DateTime _dateNow = new DateTime(_date.Year, _date.Month, _date.Day, _date.Hour, _date.Minute, _date.Second);

                this.NoTextBox.Text =(_date.Year.ToString() + _date.Month.ToString().PadLeft(2,'0')+ _date.Day.ToString().PadLeft(2,'0') + _date.Hour.ToString().PadLeft(2,'0') + _date.Minute.ToString().PadLeft(2,'0') + _date.Second.ToString().PadLeft(2,'0'));
                this.NoTextBox.Attributes.Add("ReadOnly", "True");

                this.PageTitleLiteral.Text = this._pageTitleLiteral;
                this.DateTextBox.Text = Convert.ToString(DateTime.Now);
                this.DateTextBox.Attributes.Add("ReadOnly", "True");
                
                this.ClearLabel();
                this.ClearData();
                this.ShowCustomerContact();
                this.ShowContactName();
                
                this.NextButton.ImageUrl = "../images/next.jpg";
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
            this.RemarkTextBox.Text = "";
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
        protected void ContactNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
                this.Panel1.Visible = true;
                this.ShowData();
        }
        public void ShowData()
        {
            MsCustContact _CustContact = this._custContactBL.GetSingleForClearance(this.CustomerDropDownList.SelectedValue, Convert.ToInt32(this.ContactNameDropDownList.SelectedValue));
            String _CustContactImage = this.CustomerDropDownList.SelectedValue+"-"+ Convert.ToInt32(this.ContactNameDropDownList.SelectedValue);     
            String _photoName = this._custContactBL.GetCustomerPhotoByCode(this.CustomerDropDownList.SelectedValue,Convert.ToInt32(this.ContactNameDropDownList.SelectedValue));

            string _strImagePath = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + ApplicationConfig.StringSeparatorPublish + "PhotoImages/" + _photoName;
            //string _strImagePath = ApplicationConfig.PhotoPictureVirDirPath + _photoName;

            this.PhotoImage.Attributes.Add("src", "" + _strImagePath + "?t=");
            this.PhotoImage.Attributes.Add("width", "115");
            this.PhotoImage.Attributes.Add("height", "160");

            if (_photoName == null)
            {
                this.PhotoImage.Visible = false;
                this.NoPhotoImage.ImageUrl = "../images/no_photo.jpg";
                this.NoPhotoImage.Attributes.Add("width", "110");
                this.NoPhotoImage.Attributes.Add("height", "130");
                this.NoPhotoLabel.Text = "No Photo";
            }
            else
            {
                this.NoPhotoImage.ImageUrl = "";
                this.NoPhotoLabel.Text = "";
                this.PhotoImage.Visible =true;

            }

            this.IDCardLabel.Text = _CustContact.CardID;
            this.PhoneLabel.Text = _CustContact.Phone;
            this.AccessLabel.Text = Convert.ToString(_CustContact.FgAccess);
            this.GoodInLabel.Text = Convert.ToString(_CustContact.FgGoodsIn);
            this.GoodOutLabel.Text = Convert.ToString(_CustContact.FgGoodsOut);
            this.AdditionalVisitorLabel.Text = Convert.ToString(_CustContact.FgAdditionalVisitor);
            this.ContactAuthorizationLabel.Text = Convert.ToString(_CustContact.FgAuthorizationContact);
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            ClearanceHd _ClearanceHd = new ClearanceHd();

            _ClearanceHd.ClearanceCode = this.NoTextBox.Text;
            _ClearanceHd.ClearanceDate = Convert.ToDateTime(this.DateTextBox.Text);
            _ClearanceHd.CustomerCode = this.CustomerDropDownList.SelectedValue;
            _ClearanceHd.VisitorCode = Convert.ToInt32(this.ContactNameDropDownList.SelectedValue);
            _ClearanceHd.Remark = this.RemarkTextBox.Text;
            
            String _ClearanceCode = _ClearanceHd.ClearanceCode;

            bool _result = this._clearanceBL.Add(_ClearanceHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_ClearanceCode, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
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
