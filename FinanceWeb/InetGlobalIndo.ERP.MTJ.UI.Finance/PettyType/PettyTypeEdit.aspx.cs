using System;
using System.Web;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PettyType
{
    public partial class PettyTypeEdit : PettyTypeBase
    {
        private PettyBL _petty = new PettyBL();
        private AccountBL _account = new AccountBL();
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
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.PettyDDL.Attributes.Add("OnChange", "SelectedAccount(" + this.PettyDDL.ClientID + "," + this.AccountCodeTextBox.ClientID + ");");
                this.AccountCodeTextBox.Attributes.Add("OnBlur", "BlurAccountCode(" + this.PettyDDL.ClientID + "," + this.AccountCodeTextBox.ClientID + ");");

                this.ShowData();
            }
        }

        private void ShowDropdownlist()
        {
            var hasil = this._account.GetListForDDL();

            this.PettyDDL.DataSource = hasil;
            this.PettyDDL.DataValueField = "Account";
            this.PettyDDL.DataTextField = "AccountName";
            this.PettyDDL.DataBind();
            this.PettyDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowData()
        {
            MsPetty _petty = this._petty.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.CodeTextBox.Text = _petty.PettyCode;
            this.NameTextBox.Text = _petty.PettyName;
            this.AccountCodeTextBox.Text = _petty.Account;

            //if(_petty.FgReport=='Y'){
            //    this.CheckBox1.Checked = true;
            //}else{
            //    this.CheckBox1.Checked = false;
            //}                

            this.ShowDropdownlist();

            this.PettyDDL.SelectedValue = _petty.Account;
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            //MsAccClass _msAccClass = this._accClass.GetSingle(this._nvcExtractor.Value);
            MsPetty _msPetty = this._petty.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msPetty.PettyCode = this.CodeTextBox.Text;
            _msPetty.PettyName = this.NameTextBox.Text;
            //_msPetty.FgType = Convert.ToByte(this.TypeDropDownList.SelectedValue);
            _msPetty.Account = this.PettyDDL.SelectedValue;
            //if (this.CheckBox1.Checked == true)
            //{
            //    _msPetty.FgReport = 'Y';
            //}
            //else
            //{
            //    _msPetty.FgReport = 'N';
            //}
            _msPetty.UserID = HttpContext.Current.User.Identity.Name;
            _msPetty.UserDate = DateTime.Now;

            if (this._petty.Edit(_msPetty) == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_homePage);
        }
    }
}