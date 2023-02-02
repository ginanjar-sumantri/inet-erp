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
    public partial class VisitorExtensionEdit : VisitorExtensionBase
    {
        private MsVisitorExtensionBL _VisitorExtensionBL = new MsVisitorExtensionBL();
        private MsCustomerBL _customerBL = new MsCustomerBL();
        private MsCustContactBL _custContactBL = new MsCustContactBL();
        private FotoAdd _fotoAdd = new FotoAdd();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);
            HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
            if (cookie == null)
            {
                Response.Redirect("..\\Login.aspx");
            }

            if (!this.Page.IsPostBack == true)
            {
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = this._pageTitleLiteral;
                this.ShowData();

                this.SaveButton.ImageUrl = "../images/save.jpg";
                this.ResetButton.ImageUrl = "../images/reset.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }
        public void ShowData()
        {
            master_CustContactExtension _msCustContactExtension = this._VisitorExtensionBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            String _CustContactExtension = _msCustContactExtension.CustCode.ToString() + "-" + _msCustContactExtension.ItemNo.ToString();

            this.CustNameTextBox.Text = (this._customerBL.GetSingle(_msCustContactExtension.CustCode)).CustName;
            this.ContactNameTextBox.Text = (this._custContactBL.GetSingleContactName(_CustContactExtension)).ContactName;
            this.UploadLabel.Text = "Photo dimension : Width " + ApplicationConfig.ImageWidth + " pixels x Height " + ApplicationConfig.ImageHeight + " pixels, File size limit: " + (Convert.ToDecimal(ApplicationConfig.ImageMaxSize) / 1024).ToString() + " KBytes";

            string _strImagePath = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + ApplicationConfig.StringSeparatorPublish + "PhotoImages/" + _msCustContactExtension.CustomerPhoto;
            //string _strImagePath = ApplicationConfig.PhotoPictureVirDirPath + _msCustContactExtension.CustomerPhoto;
            this.PictureImage.Attributes.Add("src", "" + _strImagePath + "?t=");
            this.PictureImage.Attributes.Add("width", "115");
            this.PictureImage.Attributes.Add("height", "160");


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
          master_CustContactExtension _msCustContactExtension = this._VisitorExtensionBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

          String _imagepath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + @"\PhotoImages\";
          String _result = this._fotoAdd.Edit(_msCustContactExtension, this.FotoUpLoad, _imagepath);

          if (_result == "")
          {
              Response.Redirect(this._homePage);
          }
          else
          {
              this.WarningLabel.Text = _result;
          }

        }
    }


}