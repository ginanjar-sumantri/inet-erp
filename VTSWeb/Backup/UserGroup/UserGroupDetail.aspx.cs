using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using VTSWeb.Database;
using VTSWeb.BusinessRule;
using VTSWeb.Common;
using VTSWeb.SystemConfig;

namespace VTSWeb.UI
{
    public partial class UserGroupDetail : UserGroupBase
    {
        private UserGroupBL _UserGroupBL = new UserGroupBL();
        private PermissionBL _permBL = new PermissionBL();

        private int _no = 0;
        //private int _nomor = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.BackButton.ImageUrl = "../images/back.jpg";
                this.EditButton.ImageUrl = "../images/edit2.jpg";
                this.AddButton.ImageUrl = "../images/add.jpg";
                this.DeleteButton.ImageUrl = "../images/delete.jpg";

                this.SetButtonPermission();

                this.ClearLabel();
                this.ShowData();
                this.ShowDataDetail();
            }
        }

        private void SetButtonPermission()
        {

        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            MsUserGroup _msUserGroup = this._UserGroupBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.UserGroupCodeTextBox.Text = _msUserGroup.UserGroupCode;
            this.UserGroupNameTextBox.Text = _msUserGroup.UserGroupName;
        }

        public void ShowDataDetail()
        {
            this.TempHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.ListRepeater.DataSource = this._UserGroupBL.GetListUserGroupDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            MsUserGroupDt _temp = (MsUserGroupDt)e.Item.DataItem;

            string _code = _temp.EmpNumb.ToString();

            if (this.TempHidden.Value == "")
            {
                this.TempHidden.Value = _code;
            }
            else
            {
                this.TempHidden.Value += "," + _code;
            }

            Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
            _no += 1;
            _noLiteral.Text = _no.ToString();

            CheckBox _listCheckbox;
            _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
            _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");

            //ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
            //_editButton.PostBackUrl = this._detailEditPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._itemKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
            //_editButton.ImageUrl = "../images/edit.jpg";

            HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
            _tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
            _tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

            Literal _employeeLiteral = (Literal)e.Item.FindControl("EmployeeLiteral");
            _employeeLiteral.Text = HttpUtility.HtmlEncode(_temp.EmpName);
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_detailAddPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');
            string _error = "";

            this.ClearLabel();

            bool _result = this._UserGroupBL.DeleteMultiUserGroupDt(_tempSplit, this.UserGroupCodeTextBox.Text);

            if (_result == true)
            {
                _error = "Delete Success";
            }
            else
            {
                _error = "Delete Failed";
            }

            this.CheckHidden.Value = "";
            this.AllCheckBox.Checked = false;

            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error);
        }
    }
}