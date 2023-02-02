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

namespace VTSWeb.UI
{
    public partial class ClearanceDetail : ClearanceBase
    {
        private ClearanceBL _ClearanceBL = new ClearanceBL();
        private MsCustomerBL _customerBL = new MsCustomerBL();
        private MsCustContactBL _custContactBL = new MsCustContactBL();
        private MsCustContact_AreaBL _custContactAreaBL = new MsCustContact_AreaBL();
        private MsPurposeBL _purposeBL = new MsPurposeBL();
        private MsAreaBL _areaBL = new MsAreaBL();
        private MsRackServerBL _rackServerBL = new MsRackServerBL();
        private RackCustomerBL _rackCustomerBL = new RackCustomerBL();

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";
        private int?[] _navMark = { null, null, null, null };
        private string _currPageKey = "CurrentPage";


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

                ClearanceHd _ClearanceHd = this._ClearanceBL.GetSingleClearance(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.ViewState[this._currPageKey] = 0;

                this.AddButton.ImageUrl = "../images/add.jpg";
                this.EditButton.ImageUrl = "../images/edit2.jpg";
                this.DeleteButton.ImageUrl = "../images/delete.jpg";
                this.BackButton.ImageUrl = "../images/back.jpg";
                this.CompleteButton.ImageUrl = "../images/Complete.jpg";
                this.SaveButton.ImageUrl = "../images/Save.jpg";
                this.ResetButton.ImageUrl = "../images/Reset.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";

                this.ClearData();
                this.ClearLabel();
                this.ShowDataHd(0);
                this.Panel2.Visible = false;

                if (this.StatusHiddenField.Value == "1")
                {
                    this.AddButton.Visible = false;
                    this.EditButton.Visible = false;
                    this.CompleteButton.Visible = false;
                    this.DeleteButton.Visible = false;
                    //this.Panel2.Visible = false;
                }
                else
                {
                    this.AddButton.Visible = true;
                    //this.Panel2.Visible = false;
                }
            }
        }
        //    string _code = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
        //    string _area = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._areaKey), ApplicationConfig.EncryptionKey));
        //    string _purpose = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._purposeKey), ApplicationConfig.EncryptionKey));

        //    if (_area != "" && _purpose != "")
        //    {
        //        this.ShowDataDt(0);
        //    }
        //    else
        //    {
        //        this.ClearData();
        //        this.ClearLabel();
        //        this.ShowDataHd(0);
        //    }
        //}
        
        private Boolean IsCheckedAll()
        {
            Boolean _result = true;

            foreach (RepeaterItem _row in this.ListRepeater.Items)
            {
                CheckBox _chk = (CheckBox)_row.FindControl("ListCheckBox");

                if (_chk.Checked == false)
                {
                    _result = false;
                    break;
                }
            }

            if (this.ListRepeater.Items.Count == 0)
            {
                _result = false;
            }

            return _result;
        }

        public void ClearLabel()
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
                this.HHInDropDownList.Items.Add(new ListItem(i.ToString().PadLeft(2, '0')));
                this.HHOutDropDownList.Items.Add(new ListItem(i.ToString().PadLeft(2, '0')));
            }
            for (int i = 0; i < 60; i++)
            {
                this.MMInDropDownList.Items.Add(new ListItem(i.ToString().PadLeft(2, '0')));
                this.MMOutDropDownList.Items.Add(new ListItem(i.ToString().PadLeft(2, '0')));
            }
            this.ShowPurpose();
            this.ShowAreaDDL();
        }

        public void ShowPurpose()
        {
            this.PurposeDropDownList.Items.Clear();
            this.PurposeDropDownList.DataTextField = "PurposeName";
            this.PurposeDropDownList.DataValueField = "PurposeCode";
            this.PurposeDropDownList.DataSource = this._purposeBL.GetPurposeForClearance();
            this.PurposeDropDownList.DataBind();
            this.PurposeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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

        public void ShowDataHd(Int32 _prmCurrentPage)
        {
            this._ClearanceBL = new ClearanceBL();
            ClearanceHd _ClearanceHd = this._ClearanceBL.GetSingleClearance(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            String _Clearance = _ClearanceHd.CustomerCode.ToString() + "-" + _ClearanceHd.VisitorCode.ToString();           

            //HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];

            this.NoTextBox.Text = _ClearanceHd.ClearanceCode;
            this.DateTextBox.Text = Convert.ToString(_ClearanceHd.ClearanceDate);
            this.CustomerNameTextBox.Text = (this._customerBL.GetSingle(_ClearanceHd.CustomerCode)).CustName;
            this.CustomerHiddenField.Value = _ClearanceHd.CustomerCode;
            this.ContactNameTextBox.Text = (this._custContactBL.GetSingleContactName(_Clearance)).ContactName;
            this.ContactNameHiddenField.Value = Convert.ToString(_ClearanceHd.VisitorCode);
            this.RemarkTextBox.Text = _ClearanceHd.Remark;
            this.StatusLabel.Text = ClearanceCompleteDataMapping.GetStatus(_ClearanceHd.CompleteStatus);
            this.StatusHiddenField.Value = Convert.ToString(_ClearanceHd.CompleteStatus);

            this.ShowRack(_ClearanceHd.CustomerCode);

            //this.GradeTextBox.Text = _AssignmentHd.Grade.ToString() + " - " + _gradeBL.GetGradeNameByCode((_AssignmentHd.Grade == null) ? ' ' : Convert.ToChar(_AssignmentHd.Grade));

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.TempHidden.Value = "";

            this._page = _prmCurrentPage;

            this.ListRepeater.DataSource = this._ClearanceBL.GetListDt(this.NoTextBox.Text);
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.AllCheckBox.Checked = this.IsCheckedAll();

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");
            this.ShowAreaDDL();
            //this.Panel2.Visible = false;
        }

        private void ShowAreaDDL()
        {
            this.AreaDropDownList.Items.Clear();
            this.AreaDropDownList.DataTextField = "AreaName";
            this.AreaDropDownList.DataValueField = "AreaCode";
            this.AreaDropDownList.DataSource = this._custContactAreaBL.GetAreaDDLForClearance(this.CustomerHiddenField.Value, this.ContactNameHiddenField.Value);
            this.AreaDropDownList.DataBind();
            this.AreaDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        //public void ShowDataDt(Int32 _prmCurrentPage)
        //{
        //    this._ClearanceBL = new ClearanceBL();
        //    ClearanceDt _ClearanceDt = this._ClearanceBL.GetSingleDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._areaKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._purposeKey), ApplicationConfig.EncryptionKey));

        //    this.AreaTextBox.Visible = true;
        //    this.AreaDropDownList.Visible = false;
        //    this.AreaTextBox.Text = _ClearanceDt.AreaCode;
        //    //this.AreaDropDownList.Attributes.Add("ReadOnly", "True");

        //    this.PurposeDropDownList.Text = Convert.ToString(_ClearanceDt.PurposeCode);
        //    this.PurposeDropDownList.Attributes.Add("ReadOnly", "True");
        //    this.DateInTextBox.Text = _ClearanceDt.DateIn.ToString("dd/MM/yyyy");
        //    this.HHInDropDownList.SelectedValue = _ClearanceDt.DateIn.ToString("HH");
        //    this.MMInDropDownList.SelectedValue = _ClearanceDt.DateIn.ToString("mm");
        //    this.DateOutTextBox.Text = _ClearanceDt.DateOut.ToString("dd/MM/yyyy");
        //    this.HHInDropDownList.SelectedValue = _ClearanceDt.DateOut.ToString("HH");
        //    this.MMInDropDownList.SelectedValue = _ClearanceDt.DateOut.ToString("mm");

        //    NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

        //    if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
        //    {
        //        this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
        //    }

        //    this.TempHidden.Value = "";

        //    this._page = _prmCurrentPage;

        //    this.ListRepeater.DataSource = this._ClearanceBL.GetListDt(this.NoTextBox.Text);
        //    this.ListRepeater.DataBind();

        //    this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

        //    this.AllCheckBox.Checked = this.IsCheckedAll();

        //    this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

        //}

        private Boolean IsChecked(String _prmValue)
        {
            Boolean _result = false;
            String[] _value = this.CheckHidden.Value.Split(',');

            for (int i = 0; i < _value.Length; i++)
            {
                if (_prmValue == _value[i])
                {
                    _result = true;
                    break;
                }
            }

            return _result;
        }
        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ClearanceDt _temp = (ClearanceDt)e.Item.DataItem;
                string _code = _temp.ClearanceCode.ToString();
                string _areaCode = _temp.AreaCode.ToString();
                string _purposeCode = _temp.PurposeCode.ToString();
                string _chekIn = _temp.CheckIn.ToString();
                string _all = _temp.ClearanceCode.ToString() + "-" + _temp.AreaCode.ToString() + "-" + _temp.PurposeCode.ToString()+ "-" +_temp.CheckIn.ToString();


                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _all;
                }
                else
                {
                    this.TempHidden.Value += "," + _all;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _all + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");
                _listCheckbox.Checked = this.IsChecked(_all);

                ImageButton _editButton2 = (ImageButton)e.Item.FindControl("EditButton2");
                _editButton2.PostBackUrl = this._detailEditPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._areaKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_areaCode, ApplicationConfig.EncryptionKey)) + "&" + this._purposeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_purposeCode, ApplicationConfig.EncryptionKey)) + "&" + this._checkinKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_chekIn, ApplicationConfig.EncryptionKey));
                _editButton2.ImageUrl = "../images/edit.jpg";
                //_editButton2.CommandArgument = _temp.AreaCode + "-" + _temp.PurposeCode;
                //_editButton2.CommandName = "EditDetailButton";

                if (this.StatusHiddenField.Value == "1")
                {
                    _editButton2.Visible = false;
                }
                else
                {
                    _editButton2.Visible = true;
                }

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
                _tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                _tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

                Literal _areaNameLiteral = (Literal)e.Item.FindControl("AreaNameLiteral");
                _areaNameLiteral.Text = HttpUtility.HtmlEncode(this._areaBL.GetAreaNameByCode(_temp.AreaCode));

                Literal _purposeNameLiteral = (Literal)e.Item.FindControl("PurposeNameLiteral");
                _purposeNameLiteral.Text = HttpUtility.HtmlEncode(this._purposeBL.GetPurposeNameByCode(_temp.PurposeCode));

                Literal _dateInLiteral = (Literal)e.Item.FindControl("DateInLiteral");
                _dateInLiteral.Text = HttpUtility.HtmlEncode(_temp.CheckIn.ToString());

                Literal _dateOutLiteral = (Literal)e.Item.FindControl("DateOutLiteral");
                _dateOutLiteral.Text = HttpUtility.HtmlEncode(_temp.CheckOut.ToString());
            }
        }
        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Panel2.Visible = true;
            //Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CompleteButton_Click(object sender, ImageClickEventArgs e)
        {
            ClearanceHd _ClearanceHd = this._ClearanceBL.GetSingleClearance(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            string _statusLabel = this.StatusLabel.Text = Convert.ToString(1);
            _ClearanceHd.CompleteStatus = Convert.ToByte(_statusLabel);

            bool _result = this._ClearanceBL.Edit(_ClearanceHd);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');

            bool _result = this._ClearanceBL.DeleteMultiDt(_tempSplit);

            this.ClearLabel();

            if (_result == true)
            {
                this.WarningDeleteLabel.Text = "Delete Success";
            }
            else
            {
                this.WarningDeleteLabel.Text = "Delete Failed";
            }

            this.CheckHidden.Value = "";
            this.AllCheckBox.Checked = false;

            this.ViewState[this._currPageKey] = 0;

            this.ShowDataHd(0);
        }
        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {

            ClearanceDt _ClearanceDt = new ClearanceDt();

            DateTime _dateIn = DateFormMapping.GetValue(this.DateInTextBox.Text);
            DateTime _dateTimeIn = new DateTime(_dateIn.Year, _dateIn.Month, _dateIn.Day, Convert.ToInt32(this.HHInDropDownList.SelectedValue), Convert.ToInt32(this.MMInDropDownList.SelectedValue), 0);
            DateTime _dateOut = DateFormMapping.GetValue(this.DateOutTextBox.Text);
            DateTime _dateTimeOut = new DateTime(_dateOut.Year, _dateOut.Month, _dateOut.Day, Convert.ToInt32(this.HHOutDropDownList.SelectedValue), Convert.ToInt32(this.MMInDropDownList.SelectedValue), 0);

            _ClearanceDt.ClearanceCode = this.NoTextBox.Text;
            _ClearanceDt.AreaCode = this.AreaDropDownList.SelectedValue;
            _ClearanceDt.PurposeCode = this.PurposeDropDownList.SelectedValue;
            _ClearanceDt.CheckIn = _dateTimeIn;
            _ClearanceDt.CheckOut = _dateTimeOut;
            _ClearanceDt.RackCode = this.RackDropDownList.SelectedValue;


            //if (_dateTimeOut <= _dateTimeIn)
            //{
            //    this.WarningDetailLabel.Text = "You Failed Add Data, Time in should be greater  than the time out !!";
            //}
            //else
            //{
                bool _result = this._ClearanceBL.AddDt(_ClearanceDt);

                if (_result == true)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.WarningDetailLabel.Text = "You Failed Add Data";
                }
            //}
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
            
        }
        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));

        }

        //protected void ListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    if (e.CommandName == "EditDetailButton")
        //    {
        //        this.ShowDetail(e.CommandArgument.ToString());
        //    }
        //}

        //private void ShowDetail(String _prmCode)
        //{
        //    String[] _code = _prmCode.Split('-');

        //    ClearanceDt _ClearanceDt = this._ClearanceBL.GetSingleDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), _code[0], _code[1]);
        //    this.AreaDropDownList.SelectedValue = _ClearanceDt.AreaCode;
        //    this.PurposeDropDownList.SelectedValue = _ClearanceDt.PurposeCode;
        //    this.DateInTextBox.Text = DateFormMapping.GetValue(_ClearanceDt.DateIn);
        //    this.HHInDropDownList.SelectedValue = _ClearanceDt.DateIn.Hour.ToString();
        //    this.MMInDropDownList.SelectedValue = _ClearanceDt.DateIn.Minute.ToString();            
        //    this.DateOutTextBox.Text = DateFormMapping.GetValue(_ClearanceDt.DateOut);
        //    this.HHOutDropDownList.SelectedValue = _ClearanceDt.DateOut.Hour.ToString();
        //    this.MMOutDropDownList.SelectedValue = _ClearanceDt.DateOut.Minute.ToString();            
        //}
    }
}