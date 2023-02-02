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
    public partial class GoodsInOutDetailEdit : GoodsInOutBase
    {
        private GoodsInOutBL _GoodsInOutBL = new GoodsInOutBL();
        private MsCustContactBL _custContactBL = new MsCustContactBL();

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
                this.SaveButton.ImageUrl = "../images/Save.jpg";
                this.CancelButton.ImageUrl = "../images/Cancel.jpg";
                this.ResetButton.ImageUrl = "../images/Reset.jpg";

                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            this._GoodsInOutBL = new GoodsInOutBL();
            GoodsInOutDt _GoodsInOut = this._GoodsInOutBL.GetSingleDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNumbTextBox.Text = _GoodsInOut.TransNmbr;
            this.ItemNoTextBox.Text = Convert.ToString(_GoodsInOut.ItemNo);
            this.ItemCodeTextBox.Text = _GoodsInOut.ItemCode;
            this.ProductNameTextBox.Text = _GoodsInOut.ProductName;
            this.SerialNumberTextBox.Text = _GoodsInOut.SerialNumber;
            this.RemarkTextBox.Text = _GoodsInOut.Remark;
            this.ElectriCityTextBox.Text = _GoodsInOut.ElectriCityNumerik;
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNumbTextBox.Text, ApplicationConfig.EncryptionKey)));
        }
        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();

        }
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            GoodsInOutDt _GoodsInOut = this._GoodsInOutBL.GetSingleDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));


            _GoodsInOut.ProductName = this.ProductNameTextBox.Text;
            _GoodsInOut.SerialNumber = this.SerialNumberTextBox.Text;
            _GoodsInOut.Remark = this.RemarkTextBox.Text;
            _GoodsInOut.ElectriCityNumerik = this.ElectriCityTextBox.Text;


            bool _result = this._GoodsInOutBL.EditDt(_GoodsInOut);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNumbTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }
    }

}