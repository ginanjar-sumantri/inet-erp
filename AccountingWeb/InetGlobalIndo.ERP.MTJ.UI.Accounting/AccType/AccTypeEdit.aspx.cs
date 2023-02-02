using System;
using System.Web;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Web.UI.WebControls;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.AccType
{
    public partial class AccTypeEdit : AccTypeBase
    {
        private AccTypeBL _accTypeBL = new AccTypeBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
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

                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ShowAktivaPassiva()
        {
            this.FgSideDropDownList.Items.Clear();
            this.FgSideDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.FgSideDropDownList.Items.Insert(1, new ListItem("Aktiva", "0"));
            this.FgSideDropDownList.Items.Insert(2, new ListItem("Passiva", "1"));
        }

        protected void ShowIncomeExpense()
        {
            this.FgSideDropDownList.Items.Clear();
            this.FgSideDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.FgSideDropDownList.Items.Insert(1, new ListItem("Incomes", "0"));
            this.FgSideDropDownList.Items.Insert(2, new ListItem("Expenses", "1"));
        }

        private void ShowData()
        {
            MsAccType _msAccType = this._accTypeBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.CodeTextBox.Text = _msAccType.AccTypeCode.ToString();
            this.NameTextBox.Text = _msAccType.AccTypeName;
            this.AccTypeRBL.SelectedValue = _msAccType.FgType;
            if (this.AccTypeRBL.Items[0].Selected == true)
            {
                this.ShowAktivaPassiva();
            }
            else
            {
                this.ShowIncomeExpense();
            }
            this.FgSideDropDownList.SelectedValue = Convert.ToInt16(_msAccType.FgSide).ToString();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            MsAccType _msAccType = this._accTypeBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msAccType.AccTypeName = this.NameTextBox.Text;
            _msAccType.FgType = this.AccTypeRBL.SelectedValue;
            _msAccType.FgSide = Convert.ToBoolean(Convert.ToInt16(this.FgSideDropDownList.SelectedValue));
            _msAccType.UserID = HttpContext.Current.User.Identity.Name;
            _msAccType.UserDate = DateTime.Now;

            if (this._accTypeBL.Edit(_msAccType) == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void AccTypeRBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.AccTypeRBL.Items[0].Selected == true)
            {
                this.ShowAktivaPassiva();
            }
            else
            {
                this.ShowIncomeExpense();
            }
        }
    }
}