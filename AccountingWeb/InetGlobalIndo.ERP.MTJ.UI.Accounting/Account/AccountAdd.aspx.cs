using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.Account
{
    public partial class AccountAdd : AccountBase
    {
        private CurrencyBL _currencyBL = new CurrencyBL();
        private AccClassBL _accClassBL = new AccClassBL();
        private AccountBL _accountBL = new AccountBL();
        private SubledBL _subledBL = new SubledBL();
        private PermissionBL _permBL = new PermissionBL();

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

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowBranchAccDropDownList();
                this.ShowAccClassDropDownList();
                this.ShowCurrCodeDropDownList();
                this.ShowSubledRadioButtonList();

                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void ShowBranchAccDropDownList()
        {
            this.BranchAccDropDownList.Items.Clear();
            this.BranchAccDropDownList.DataSource = this._accountBL.GetListBranchAccountForDDL();
            this.BranchAccDropDownList.DataValueField = "BranchAccCode";
            this.BranchAccDropDownList.DataTextField = "BranchAccName";
            this.BranchAccDropDownList.DataBind();
            this.BranchAccDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowAccClassDropDownList()
        {
            this.AccClassDropDownList.Items.Clear();
            this.AccClassDropDownList.DataSource = this._accClassBL.GetListAccClass();
            this.AccClassDropDownList.DataValueField = "AccClassCode";
            this.AccClassDropDownList.DataTextField = "AccClassName";
            this.AccClassDropDownList.DataBind();
            this.AccClassDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCurrCodeDropDownList()
        {
            this.CurrCodeDropDownList.Items.Clear();
            this.CurrCodeDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrCodeDropDownList.DataValueField = "CurrCode";
            this.CurrCodeDropDownList.DataTextField = "CurrCode";
            this.CurrCodeDropDownList.DataBind();
            this.CurrCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowSubledRadioButtonList()
        {
            this.SubledRBL.Items.Clear();
            this.SubledRBL.DataSource = _subledBL.GetList();
            this.SubledRBL.DataTextField = "SubledName";
            this.SubledRBL.DataValueField = "SubledCode";
            this.SubledRBL.DataBind();
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.DetailTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
            this.AccClassDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccClassDropDownList.ClientID + "," + this.CodeTextBox.ClientID + ");");
            this.CodeTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccClassDropDownList.ClientID + "," + this.CodeTextBox.ClientID + ");");
        }

        protected void ClearData()
        {
            this.ClearLabel();

            this.CodeTextBox.Text = "";
            this.DetailTextBox.Text = "";
            this.DescTextBox.Text = "";
            this.SaldoNormalRBL.SelectedIndex = 0;
            this.SubledRBL.SelectedIndex = 0;
            this.CurrCodeDropDownList.SelectedValue = "null";
            this.AccClassDropDownList.SelectedValue = "null";
            this.BranchAccDropDownList.SelectedValue = "null";
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            MsAccount _account = new MsAccount();

            _account.Account = _accountBL.GetBranchAccountIDByCode(new Guid(this.BranchAccDropDownList.SelectedValue)) + this.CodeTextBox.Text + this.DetailTextBox.Text;
            _account.AccountName = this.DescTextBox.Text;
            _account.BranchAccCode = new Guid(this.BranchAccDropDownList.SelectedValue);
            _account.AccClass = this.AccClassDropDownList.SelectedValue;
            _account.Detail = this.DetailTextBox.Text;
            _account.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _account.FgActive = AccountMapper.GetYesNo(YesNo.Yes);
            _account.FgNormal = Convert.ToChar(this.SaldoNormalRBL.SelectedValue);
            _account.FgSubLed = Convert.ToChar(this.SubledRBL.SelectedValue);
            _account.UserID = HttpContext.Current.User.Identity.Name;
            _account.UserDate = DateTime.Now;

            bool _result = this._accountBL.AddAccount(_account);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Account";
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        protected void AccClassDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            String _lastCode = _accountBL.GetLastAccount(this.AccClassDropDownList.SelectedValue);
            if (_lastCode != null)
            {
                this.LastCodeLabel.Text = " Last Code " + _lastCode;
            }
            else
            {
                this.LastCodeLabel.Text = " Last Code " + this.AccClassDropDownList.SelectedValue;
            }
        }
    }
}
