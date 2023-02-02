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
    public partial class GoodsInOutEdit : GoodsInOutBase
    {
        private GoodsInOutBL _GoodsInOutBL = new GoodsInOutBL();
        private MsCustomerBL _customerBL = new MsCustomerBL();
        private RackCustomerBL _rackCustomerBL = new RackCustomerBL();

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
                this.ViewDetailButton.ImageUrl = "../images/view_detail.jpg";
                this.ClearData();

                this.ShowCustomerContact();
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
            this.TransDateTextBox.Text = DateFormMapping.GetValue(_now);
            for (int i = 0; i < 24; i++)
            {
                this.HHDropDownList.Items.Add(new ListItem(i.ToString().PadLeft(2, '0')));
            }
            for (int i = 0; i < 60; i++)
            {
                this.MMDropDownList.Items.Add(new ListItem(i.ToString().PadLeft(2, '0')));
            }

            this.NumberFileTextBox.Text = "";
            this.RemarkTextBox.Text = "";
            this.CarryByTextBox.Text = "";
            this.RequestByTextBox.Text = "";
            this.ApprovedByTextBox.Text = "";
            this.TransDateTextBox.Text = DateFormMapping.GetValue(_now);

            HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
            this.PostedByLabel.Text = (cookie[ApplicationConfig.CookieName]);

        }
        public void ShowCustomerContact()
        {
            this.CustNameDropDownList.Items.Clear();
            this.CustNameDropDownList.DataTextField = "CustName";
            this.CustNameDropDownList.DataValueField = "CustCode";
            this.CustNameDropDownList.DataSource = this._customerBL.GetCustomerForDDL();
            this.CustNameDropDownList.DataBind();
            this.CustNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowRack(String _prmCustCode)
        {
            this.RackDropDownList.Items.Clear();
            this.RackDropDownList.DataTextField = "RackName";
            this.RackDropDownList.DataValueField = "RackCode";
            this.RackDropDownList.DataSource = this._rackCustomerBL.GetListRackServerForClearance(_prmCustCode);
            this.RackDropDownList.DataBind();
            //this.RackDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));  
        }
        public void ShowData()
        {
            this._GoodsInOutBL = new GoodsInOutBL();
            GoodsInOutHd _GoodsInOutHd = this._GoodsInOutBL.GetSingleHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.ShowRack(_GoodsInOutHd.CustCode);
            this.TransNumberTextBox.Text = _GoodsInOutHd.TransNmbr;
            this.NumberFileTextBox.Text = _GoodsInOutHd.FileNmbr;
            this.TransTypeDropDownList.SelectedValue = _GoodsInOutHd.TransType;
            this.CustNameDropDownList.SelectedValue = _GoodsInOutHd.CustCode;
            this.TransDateTextBox.Text = DateFormMapping.GetValue(_GoodsInOutHd.TransDate);
            this.HHDropDownList.Text = _GoodsInOutHd.TransDate.ToString("HH");
            this.MMDropDownList.Text = _GoodsInOutHd.TransDate.ToString("mm");
            this.RemarkTextBox.Text = _GoodsInOutHd.Remark;
            this.StatusLabel.Text = ClearanceCompleteDataMapping.GetStatus(_GoodsInOutHd.Status);
            this.RequestByTextBox.Text = _GoodsInOutHd.RequestedBy;
            this.CarryByTextBox.Text = _GoodsInOutHd.CarryBy;
            this.RequestByTextBox.Text = _GoodsInOutHd.RequestedBy;
            this.ApprovedByTextBox.Text = _GoodsInOutHd.ApprovedBy;
            //this.PostedByLabel.Text = _GoodsInOutHd.PostedBy;
            this.EntryDateTextBox.Text = DateFormMapping.GetValue(_GoodsInOutHd.EntryDate);
            this.HHEntryDateTextBox.Text = Convert.ToDateTime(_GoodsInOutHd.EntryDate).Hour.ToString();
            this.MMEntryDateTextBox.Text = Convert.ToDateTime(_GoodsInOutHd.EntryDate).Minute.ToString();
            this.EntryUserLabel.Text = _GoodsInOutHd.EntryUserName;
            this.RackDropDownList.SelectedValue = _GoodsInOutHd.RackCode;
        }

        protected void CustNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowRack(this.CustNameDropDownList.SelectedValue);
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
            DateTime _EntryDate = DateTime.Now;
            DateTime _dateNow = new DateTime(_EntryDate.Year, _EntryDate.Month, _EntryDate.Day, _EntryDate.Hour, _EntryDate.Minute, _EntryDate.Second);
            
            GoodsInOutHd _GoodsInOutHd = this._GoodsInOutBL.GetSingleHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            DateTime _date = DateFormMapping.GetValue(this.TransDateTextBox.Text);
            DateTime _dateTime = new DateTime(_date.Year, _date.Month, _date.Day, Convert.ToInt32(this.HHDropDownList.SelectedValue), Convert.ToInt32(this.MMDropDownList.SelectedValue), 0);

            _GoodsInOutHd.FileNmbr = this.NumberFileTextBox.Text;
            _GoodsInOutHd.TransType = this.TransTypeDropDownList.SelectedValue;
            _GoodsInOutHd.CustCode = this.CustNameDropDownList.SelectedValue;
            _GoodsInOutHd.TransDate = _dateTime;
            _GoodsInOutHd.Remark = this.RemarkTextBox.Text;
            _GoodsInOutHd.RackCode = this.RackDropDownList.SelectedValue;
            _GoodsInOutHd.CarryBy = this.CarryByTextBox.Text;
            _GoodsInOutHd.RequestedBy = this.RequestByTextBox.Text;
            _GoodsInOutHd.ApprovedBy = this.ApprovedByTextBox.Text;
            _GoodsInOutHd.PostedBy = this.PostedByLabel.Text;
            _GoodsInOutHd.EditDate = _dateNow;
            
            HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
            _GoodsInOutHd.EditUserName = (cookie[ApplicationConfig.CookieName]);
           

            bool _result = this._GoodsInOutBL.Edit(_GoodsInOutHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }
    }


}