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

namespace VTSWeb.UI
{
    public partial class ClearanceDetailEdit : ClearanceBase
    {
        private ClearanceBL _clearanceBL = new ClearanceBL();
        private MsPurposeBL _purposeBL = new MsPurposeBL();
        private MsAreaBL _areaBL = new MsAreaBL();



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

                this.ClearData();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }
        public void ClearData()
        {
            DateTime _now = DateTime.Now;

            this.DateInTextBox.Text = DateFormMapping.GetValue(_now);
            this.DateOutTextBox.Text = DateFormMapping.GetValue(_now);
            for (int i = 0; i < 24; i++)
            {
                //this.HHInDropDownList.Items.Add(new ListItem(i.ToString().PadLeft(2, '0')));
                this.HHOutDropDownList.Items.Add(new ListItem(i.ToString().PadLeft(2, '0')));
            }
            for (int i = 0; i < 60; i++)
            {
                //this.MMInDropDownList.Items.Add(new ListItem(i.ToString().PadLeft(2, '0')));
                this.MMOutDropDownList.Items.Add(new ListItem(i.ToString().PadLeft(2, '0')));
            }
        }

        public void ShowData()
        {
            this._clearanceBL = new ClearanceBL();
            ClearanceDt _ClearanceDt = _clearanceBL.GetSingleDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._areaKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._purposeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._checkinKey), ApplicationConfig.EncryptionKey));

            this.AreaTextBox.Text = (this._areaBL.GetAreaNameByCode(_ClearanceDt.AreaCode));
            this.PurposeTextBox.Text =(this._purposeBL.GetPurposeNameByCode(_ClearanceDt.PurposeCode));
            this.DateInTextBox.Text = DateFormMapping.GetValue(_ClearanceDt.CheckIn);
            this.HHInTextBox.Text = _ClearanceDt.CheckIn.ToString("HH");
            this.MMInTextBox.Text = _ClearanceDt.CheckIn.ToString("mm");
            this.DateOutTextBox.Text = _ClearanceDt.CheckOut.ToString("yyyy-MM-dd");
            this.HHOutDropDownList.SelectedValue = _ClearanceDt.CheckOut.ToString("HH");
            this.MMOutDropDownList.SelectedValue = _ClearanceDt.CheckOut.ToString("mm");

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();

        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            ClearanceDt _ClearanceDt = _clearanceBL.GetSingleDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._areaKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._purposeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._checkinKey), ApplicationConfig.EncryptionKey));

            //DateTime _dateIn = DateFormMapping.GetValue(this.DateInTextBox.Text);
            //DateTime _dateTimeIn = new DateTime(_dateIn.Year, _dateIn.Month, _dateIn.Day, Convert.ToInt32(this.HHInDropDownList.SelectedValue), Convert.ToInt32(this.MMInDropDownList.SelectedValue), 0);
            DateTime _dateOut =Convert.ToDateTime( this.DateOutTextBox.Text);
            DateTime _dateTimeOut = new DateTime(_dateOut.Year, _dateOut.Month, _dateOut.Day, Convert.ToInt32(this.HHOutDropDownList.SelectedValue), Convert.ToInt32(this.MMOutDropDownList.SelectedValue), 0);

            //_ClearanceDt.CheckIn = _dateTimeIn;
            _ClearanceDt.CheckOut = _dateTimeOut;

            if (_dateTimeOut <= Convert.ToDateTime(this.DateInTextBox.Text))
            {
                this.WarningLabel.Text = "You Failed Edit Data, Time in should be greater  than the time out !!";
            }
            else
            {
                bool _result = this._clearanceBL.EditDt(_ClearanceDt);

                if (_result == true)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.WarningLabel.Text = "You Failed Edit Data";
                }
            }
        }
    }


}