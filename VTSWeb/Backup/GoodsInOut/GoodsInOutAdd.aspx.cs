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
using VTSWeb.BusinessRule;
using VTSWeb.Database;
using VTSWeb.SystemConfig;
using VTSWeb.Common;
using VTSWeb.DataMapping;
using VTSweb.DataMapping;

namespace VTSWeb.UI
{
    public partial class GoodsInOutAdd : GoodsInOutBase
    {
        private GoodsInOutBL _GoodsInOutBL = new GoodsInOutBL();
        private MsCustomerBL _customerBL = new MsCustomerBL();
        private MsCustContactBL _custContactBL = new MsCustContactBL();
        private RackCustomerBL _rackCustomerBL = new RackCustomerBL();

        private int _page;
        private int _no = 0;
        private int _nomor = 0;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);


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

                this.ClearLabel();
                this.ClearData();
                this.ShowCustomerContact();

                this.NextButton.ImageUrl = "../images/next.jpg";
                this.ResetButton.ImageUrl = "../images/reset.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";

                this.Panel1.Visible = false;

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
        protected void TypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Panel1.Visible = true;
            this.ShowData(0);
        }
        protected void CustNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Panel1.Visible = true;
            this.ShowData(0);
            this.ShowRack(this.CustNameDropDownList.SelectedValue);
        }
        private void ShowData(Int32 _prmCurrentPage)
        {
            if (this.TypeDropDownList.SelectedValue == "In")
            {
                this.TypeInHiddenField.Value = "Y";
            }
            else
            {
                this.TypeInHiddenField.Value = "";
            }

            if (this.TypeDropDownList.SelectedValue == "Out")
            {
                this.TypeOutHiddenField.Value = "Y";
            }
            else
            {
                this.TypeOutHiddenField.Value = "";
            }

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = _prmCurrentPage;

            this.ListRepeater.DataSource = this._custContactBL.GetListForTrGoods(this.CustNameDropDownList.SelectedValue, this.TypeInHiddenField.Value, this.TypeOutHiddenField.Value);
            this.ListRepeater.DataBind();
        }
        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                MsCustContact _temp = (MsCustContact)e.Item.DataItem;
                //string _code = _temp.AreaCode.ToString();

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
                _tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                _tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

                Literal _ContactNameLiteral = (Literal)e.Item.FindControl("ContactNameLiteral");
                _ContactNameLiteral.Text = HttpUtility.HtmlEncode(_temp.ContactName);

                Literal _IDCardLiteral = (Literal)e.Item.FindControl("IDCardLiteral");
                _IDCardLiteral.Text = HttpUtility.HtmlEncode(_temp.CardID);

                //Literal _InLiteral = (Literal)e.Item.FindControl("InLiteral");
                //_InLiteral.Text = HttpUtility.HtmlEncode(_temp.FgGoodsIn.ToString());

                //Literal _OutLiteral = (Literal)e.Item.FindControl("OutLiteral");
                //_OutLiteral.Text = HttpUtility.HtmlEncode(_temp.FgGoodsOut.ToString());
            }
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            DateTime _EntryDate = DateTime.Now;
            DateTime _dateNow = new DateTime(_EntryDate.Year, _EntryDate.Month, _EntryDate.Day, _EntryDate.Hour, _EntryDate.Minute, _EntryDate.Second);

            GoodsInOutHd _GoodsInOutHd = new GoodsInOutHd();
            DateTime _date = DateFormMapping.GetValue(this.TransDateTextBox.Text);
            DateTime _dateTime = new DateTime(_date.Year, _date.Month, _date.Day, Convert.ToInt32(this.HHDropDownList.SelectedValue), Convert.ToInt32(this.MMDropDownList.SelectedValue), 0);

            _GoodsInOutHd.FileNmbr = this.NumberFileTextBox.Text;
            _GoodsInOutHd.TransType = this.TypeDropDownList.SelectedValue;
            _GoodsInOutHd.CustCode = this.CustNameDropDownList.SelectedValue;
            _GoodsInOutHd.TransDate = _dateTime;
            _GoodsInOutHd.Remark = this.RemarkTextBox.Text;
            _GoodsInOutHd.CarryBy = this.CarryByTextBox.Text;
            _GoodsInOutHd.RequestedBy = this.RequestByTextBox.Text;
            _GoodsInOutHd.ApprovedBy = this.ApprovedByTextBox.Text;
            _GoodsInOutHd.PostedBy = this.PostedByLabel.Text;
            _GoodsInOutHd.EntryDate = _dateNow; 
            _GoodsInOutHd.EntryUserName = this.PostedByLabel.Text;
            _GoodsInOutHd.RackCode = this.RackDropDownList.SelectedValue;


            bool _result = this._GoodsInOutBL.Add(_GoodsInOutHd);
            String _TransNumber = _GoodsInOutHd.TransNmbr;

            if (_result == true)
            {

                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_TransNumber.ToString(), ApplicationConfig.EncryptionKey)));
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
