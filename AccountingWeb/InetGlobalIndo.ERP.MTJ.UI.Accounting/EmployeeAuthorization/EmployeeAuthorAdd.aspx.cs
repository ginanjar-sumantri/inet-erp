using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.EmployeeAuthor
{
    public partial class EmployeeAuthorAdd : EmployeeAuthorBase
    {
        private AccountBL _accountBL = new AccountBL();
        private EmployeeBL _empBL = new EmployeeBL();
        private TransTypeBL _transTypeBL = new TransTypeBL();
        private PermissionBL _permBL = new PermissionBL();

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;

        private string awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string akhir = "_ListCheckBox";
        private string cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _temphidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowEmployeeDropdownlist();
                this.ShowTransTypeDropdownlist();

                this.ClearData();
                this.ShowAccount();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ShowEmployeeDropdownlist()
        {
            this.EmployeeDropDownList.Items.Clear();
            this.EmployeeDropDownList.DataSource = this._empBL.GetListEmpForDDL();
            this.EmployeeDropDownList.DataValueField = "EmpNumb";
            this.EmployeeDropDownList.DataTextField = "EmpName";
            this.EmployeeDropDownList.DataBind();
            this.EmployeeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowTransTypeDropdownlist()
        {
            this.TransTypeDropDownList.Items.Clear();
            this.TransTypeDropDownList.DataSource = this._transTypeBL.GetList();
            this.TransTypeDropDownList.DataValueField = "TransTypeCode";
            this.TransTypeDropDownList.DataTextField = "TransTypeName";
            this.TransTypeDropDownList.DataBind();
            this.TransTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", ""));
        }

        protected void ClearData()
        {
            this.ClearLabel();

            this.EmployeeDropDownList.SelectedValue = "null";
            this.TransTypeDropDownList.SelectedValue = "";
        }

        protected void ShowAccount()
        {
            this.TempHidden.Value = "";

            this._page = Convert.ToInt32((_nvcExtractor.GetValue(ApplicationConfig.RequestPageKey) == "") ? "0" : Rijndael.Decrypt(_nvcExtractor.GetValue(ApplicationConfig.RequestPageKey), ApplicationConfig.EncryptionKey));

            this.ListRepeater.DataSource = this._transTypeBL.GetListTransTypeAccount(this._page, _maxrow, this.TransTypeDropDownList.SelectedValue);
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + awal + "', '" + akhir + "', '" + _temphidden + "' );");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.ShowPage();
        }

        private void ShowPage()
        {
            double q = this._transTypeBL.RowsCountTransTypeAccount(this.TransTypeDropDownList.SelectedValue);

            decimal min = 0, max = 0;
            q = System.Math.Ceiling(q / (double)_maxrow);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (_page - _maxlength > 0)
            {
                min = _page - _maxlength;
            }
            else
            {
                min = 0;
            }

            if (_page + _maxlength < q)
            {
                max = _page + _maxlength + 1;
            }
            else
            {
                max = Convert.ToDecimal(q);
            }

            decimal i = min;

            if (min > 0)
            {
                sb.Append("<a href='" + this._addPage + "?" + this._transTypeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransTypeDropDownList.SelectedValue, ApplicationConfig.EncryptionKey)) + "&" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt((i - 1).ToString(), ApplicationConfig.EncryptionKey)) + "'>" + ("<<< ") + "</a>&nbsp;");
            }

            for (; i < max; i++)
            {
                if (i == _page)
                {
                    sb.Append("[<b>" + (i + 1) + "</b>]&nbsp;");
                }
                else
                {
                    sb.Append("<a href='" + this._addPage + "?" + this._transTypeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransTypeDropDownList.SelectedValue, ApplicationConfig.EncryptionKey)) + "&" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(i.ToString(), ApplicationConfig.EncryptionKey)) + "'>" + (i + 1) + "</a>&nbsp;");
                }
            }

            if (max < (decimal)q)
            {
                sb.Append("<a href='" + this._addPage + "?" + this._transTypeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransTypeDropDownList.SelectedValue, ApplicationConfig.EncryptionKey)) + "&" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(i.ToString(), ApplicationConfig.EncryptionKey)) + "'>" + (" >>>") + "</a>&nbsp;");
            }

            this.PageLabel.Text = sb.ToString();
            this.PageLabel2.Text = sb.ToString();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden.Value;
            string[] _tempsplit = _temp.Split(',');
            bool _result = false;

            if (this.GrabAllCheckBox.Checked == true)
            {
                List<MsTransType_MsAccount> _list = this._transTypeBL.GetListTransTypeAccount();

                _result = this._accountBL.AddList(_list, this.EmployeeDropDownList.SelectedValue);
            }
            else
            {
                _result = this._accountBL.AddEmployeeAuthorization(this.EmployeeDropDownList.SelectedValue, _tempsplit);
            }

            if (_result == true)
            {
                Response.Redirect(this._homePage);
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
            this.ClearData();
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                MsTransType_MsAccount _temp = (MsTransType_MsAccount)e.Item.DataItem;

                string _code = _temp.TransTypeCode + "-" + _temp.Account;

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _code;
                }
                else
                {
                    this.TempHidden.Value += "," + _code;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                CheckBox _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + awal + "', '" + akhir + "', '" + cbox + "')");

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterListTemplate");
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

                Literal _transTypeLiteral = (Literal)e.Item.FindControl("TransTypeLiteral");
                _transTypeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransTypeCode);

                Literal _transTypeNameLiteral = (Literal)e.Item.FindControl("TransTypeNameLiteral");
                _transTypeNameLiteral.Text = HttpUtility.HtmlEncode(_temp.TransTypeName);

                Literal _accountLiteral = (Literal)e.Item.FindControl("AccountLiteral");
                _accountLiteral.Text = HttpUtility.HtmlEncode(_temp.Account);

                Literal _accountNameLiteral = (Literal)e.Item.FindControl("AccountNameLiteral");
                _accountNameLiteral.Text = HttpUtility.HtmlEncode(_temp.AccountName);
            }
        }

        protected void TransTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowAccount();
        }
    }
}