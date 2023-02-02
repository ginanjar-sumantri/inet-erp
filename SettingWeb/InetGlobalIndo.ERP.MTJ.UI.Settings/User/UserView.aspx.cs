using System;
using System.Web;
using System.Web.UI;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Authentication;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Payroll;
using InetGlobalIndo.ERP.MTJ.DataMapping;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.User
{
    public partial class UserView : UserBase
    {
        private UserBL _userBL = new UserBL();
        private EmployeeBL _empBL = new EmployeeBL();
        private User_EmployeeBL _userEmpBL = new User_EmployeeBL();
        private RoleBL _roleBL = new RoleBL();
        private MembershipService _serviceBL = new MembershipService();
        private PermissionBL _permBL = new PermissionBL();
        private MethodSalaryBL _methodSalaryBL = new MethodSalaryBL();

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private string _currPageKey = "CurrentPage";

        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.ReActivateImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reActivateButton.jpg";
                this.ReActivateImageButton.Visible = false;

                this.ShowData(0);

                this.ShowRoleCBList();
                // load CBlist data
                this.GetRoleData();

                this.SetButtonPermission();
                this.ClearLabel();
            }
        }

        private void ClearLabel()
        {
            this.Label1.Text = "";
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                this.AddButton.Visible = false;
            }

            this._permDelete = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete);

            if (this._permDelete == PermissionLevel.NoAccess)
            {
                this.DeleteButton.Visible = false;
            }
        }

        public void ShowData(Int32 _prmCurrentPage)
        {
            aspnet_User _aspuser = this._userBL.GetSingleForEdit(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.UserNameTextBox.Text = _aspuser.UserName;
            //this.PwdQTextBox.Text = _aspuser.PasswordQuestion;
            //this.PwdATextBox.Text = _aspuser.PasswordAnswer;
            this.EmailTextBox.Text = _aspuser.Email;
            string _temp = _userEmpBL.GetEmployeeIdByCode(_aspuser.UserId.ToString()); //ambil EmployeeId
            this.QuestionTextBox.Text = this._userBL.GetQuestionByUserId(_aspuser.UserId.ToString());
            this.EmployeeTextBox.Text = _empBL.GetEmpNameByCode(_temp);
            this.IsLockedOutLiteral.Text = YesNoDataMapper.GetYesNo(_aspuser.IsLockedOut);

            if (_aspuser.IsLockedOut) this.ReActivateImageButton.Visible = true;
            else this.ReActivateImageButton.Visible = false;

            this.TempHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");
            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

            this.ListRepeater.DataSource = this._methodSalaryBL.GetListMethodUserPermission(_prmCurrentPage, _maxrow, this.UserNameTextBox.Text);
            this.ListRepeater.DataBind();

            this.AllCheckBox.Checked = this.IsCheckedAll();

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.Label1.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.ShowPage(_prmCurrentPage);
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._methodSalaryBL.RowsCountMethodUserPermission(this.UserNameTextBox.Text);
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
        }

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

        private void ShowPage(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCount();

            if (_prmCurrentPage - _maxlength > 0)
            {
                min = _prmCurrentPage - _maxlength;
            }
            else
            {
                min = 0;
            }

            if (_prmCurrentPage + _maxlength < q)
            {
                max = _prmCurrentPage + _maxlength + 1;
            }
            else
            {
                max = Convert.ToDecimal(q);
            }

            if (_prmCurrentPage > 0)
                _addElement += 2;

            if (_prmCurrentPage < q - 1)
                _addElement += 2;

            i = Convert.ToInt32(min);
            _pageNumber = new Int32[Convert.ToInt32(max - min) + _addElement];
            if (_pageNumber.Length != 0)
            {
                //NB: Prev Or First
                if (_prmCurrentPage > 0)
                {
                    this._navMark[0] = 0;

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[0]);
                    _pageNumberElement++;

                    this._navMark[1] = Convert.ToInt32(Math.Abs(_prmCurrentPage - 1));

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[1]);
                    _pageNumberElement++;
                }

                for (; i < max; i++)
                {
                    _pageNumber[_pageNumberElement] = i;
                    _pageNumberElement++;
                }

                if (_prmCurrentPage < q - 1)
                {
                    this._navMark[2] = Convert.ToInt32(_prmCurrentPage + 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[2]);
                    _pageNumberElement++;

                    if (_pageNumber[_pageNumberElement - 2] > _pageNumber[_pageNumberElement - 1])
                    {
                        this._flag = true;
                    }

                    this._navMark[3] = Convert.ToInt32(q - 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[3]);
                    _pageNumberElement++;
                }

                int?[] _navMarkBackup = new int?[4];
                this._navMark.CopyTo(_navMarkBackup, 0);
                this.DataPagerTopRepeater.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTopRepeater.DataBind();

                _flag = true;
                _nextFlag = false;
                _lastFlag = false;
                _navMark = _navMarkBackup;
            }
            else
            {
                this.DataPagerTopRepeater.DataSource = null;
                this.DataPagerTopRepeater.DataBind();

            }
        }

        public void ShowRoleCBList()
        {
            this.RoleCheckBoxList.DataTextField = "RoleName";
            this.RoleCheckBoxList.DataValueField = "RoleName";
            this.RoleCheckBoxList.DataSource = this._roleBL.GetListRoleName();
            this.RoleCheckBoxList.DataBind();
        }

        public void GetRoleData()
        {
            string[] _tempRole;
            _tempRole = _serviceBL.GetRolesForUser(this.UserNameTextBox.Text);

            for (int i = 0; i < this.RoleCheckBoxList.Items.Count; i++)
            {
                for (int j = 0; j < _tempRole.Length; j++)
                {
                    if (this.RoleCheckBoxList.Items[i].Value == _tempRole[j])
                    {
                        this.RoleCheckBoxList.Items[i].Selected = true;
                    }
                }
            }
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');

            this.ClearLabel();

            bool _result = this._methodSalaryBL.DeleteMultiMethodUserPermission(_tempSplit);

            if (_result == true)
            {
                this.Label1.Text = "Delete Success";
            }
            else
            {
                this.Label1.Text = "Delete Failed";
            }

            this.CheckHidden.Value = "";
            this.AllCheckBox.Checked = false;

            this.ViewState[this._currPageKey] = 0;

            this.ShowData(0);
        }

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
                PAYMsMethodUserPermission _temp = (PAYMsMethodUserPermission)e.Item.DataItem;

                string _code = _temp.MethodCode;
                string _code2 = _temp.Username;
                string _all = _code + "=" + _code2;

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _all;
                }
                else
                {
                    this.TempHidden.Value += "," + _all;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no += 1;
                _noLiteral.Text = _no.ToString();

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _all + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");
                _listCheckbox.Checked = this.IsChecked(_code);

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
                _tableRow.Attributes.Add("OnMouseOver", "this.style.backgroundColor='" + this._rowColorHover + "';");
                if (e.Item.ItemType == ListItemType.Item)
                {
                    _tableRow.Attributes.Add("style", "background-color:" + this._rowColor);
                    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColor + "';");
                }
                if (e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    _tableRow.Attributes.Add("style", "background-color:" + this._rowColorAlternate);
                    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColorAlternate + "';");
                }

                Literal _methodName = (Literal)e.Item.FindControl("SalaryMethodLiteral");
                _methodName.Text = HttpUtility.HtmlEncode(_temp.MethodName);
            }
        }

        protected void DataPagerTopRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowData(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void DataPagerTopRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

                if (Convert.ToInt32(this.ViewState[this._currPageKey]) == _pageNumber)
                {
                    TextBox _pageNumberTextbox = (TextBox)e.Item.FindControl("PageNumberTextBox");

                    _pageNumberTextbox.Text = (_pageNumber + 1).ToString();
                    _pageNumberTextbox.MaxLength = 9;
                    _pageNumberTextbox.Visible = true;

                    _pageNumberTextbox.Attributes.Add("style", "text-align: center;");
                    _pageNumberTextbox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                }
                else
                {
                    LinkButton _pageNumberLinkButton = (LinkButton)e.Item.FindControl("PageNumberLinkButton");
                    _pageNumberLinkButton.CommandName = "DataPager";
                    _pageNumberLinkButton.CommandArgument = _pageNumber.ToString();

                    if (_pageNumber == this._navMark[0])
                    {
                        _pageNumberLinkButton.Text = "First";
                        this._navMark[0] = null;
                    }
                    else if (_pageNumber == this._navMark[1])
                    {
                        _pageNumberLinkButton.Text = "Prev";
                        this._navMark[1] = null;
                    }
                    else if (_pageNumber == this._navMark[2] && this._flag == false)
                    {
                        _pageNumberLinkButton.Text = "Next";
                        this._navMark[2] = null;
                        this._nextFlag = true;
                        this._flag = true;
                    }
                    else if (_pageNumber == this._navMark[3] && this._flag == true && this._nextFlag == true)
                    {
                        _pageNumberLinkButton.Text = "Last";
                        this._navMark[3] = null;
                        this._lastFlag = true;
                    }
                    else
                    {
                        if (this._lastFlag == false)
                        {
                            _pageNumberLinkButton.Text = (_pageNumber + 1).ToString();
                        }

                        if (_pageNumber == this._navMark[2] && this._flag == true)
                            this._flag = false;
                    }
                }
            }
        }

        protected void DataPagerButton_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTopRepeater.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCount())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCount().ToString();
                        _reqPage = Convert.ToInt32(this.RowCount()) - 1;
                        break;
                    }
                    else if (_reqPage < 0)
                    {
                        ((TextBox)_item.Controls[3]).Text = "1";
                        _reqPage = 0;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            this.ViewState[this._currPageKey] = _reqPage;

            this.ShowData(_reqPage);
        }

        protected void ReActivateImageButton_Click(object sender, ImageClickEventArgs e)
        {
            aspnet_User _aspuser = this._userBL.GetSingleForEdit(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            if (this._userBL.ReActivateUser(_aspuser.UserName))
            {
                this.WarningLabel.Text = "User Re-Activated";

                this.ShowData(Convert.ToInt32(this.ViewState[this._currPageKey]));                
            }
            else
            {
                this.WarningLabel.Text = "Reactivate User Failed, Please contact your administrator";
            }
        }
    }
}