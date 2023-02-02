using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PettyType
{
    public partial class PettyTypeAdd : PettyTypeBase
    {
        private PettyBL _pettyBL = new PettyBL();
        private AccountBL _accountBL = new AccountBL();
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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.PettyDDL.Attributes.Add("OnChange", "SelectedAccount(" + this.PettyDDL.ClientID + "," + this.AccountCodeTextBox.ClientID + ");");
                this.AccountCodeTextBox.Attributes.Add("OnBlur", "BlurAccountCode(" + this.PettyDDL.ClientID + "," + this.AccountCodeTextBox.ClientID + ");");

                this.ShowDropdownlist();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowDropdownlist()
        {
            this.PettyDDL.Items.Clear();
            this.PettyDDL.DataSource = this._accountBL.GetListForDDL();
            this.PettyDDL.DataValueField = "Account";
            this.PettyDDL.DataTextField = "AccountName";
            this.PettyDDL.DataBind();
            this.PettyDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearData()
        {
            this.ClearLabel();

            this.CodeTextBox.Text = "";
            this.NameTextBox.Text = "";
            this.AccountCodeTextBox.Text = "";
            this.PettyDDL.SelectedValue = "null";

        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsPetty _msPetty = new MsPetty();

            _msPetty.PettyCode = this.CodeTextBox.Text;
            _msPetty.PettyName = this.NameTextBox.Text;
            //_msPetty.FgType = Convert.ToByte(this.TypeDropDownList.SelectedValue);
            _msPetty.Account = this.PettyDDL.SelectedValue;
            //_msPetty.FgReport = 
            //if(this.CheckBox1.Checked==true){
            //    _msPetty.FgReport = 'Y';
            //}else{
            //    _msPetty.FgReport = 'N';
            //}        
            _msPetty.UserID = HttpContext.Current.User.Identity.Name;
            _msPetty.UserDate = DateTime.Now;

            bool _result = this._pettyBL.Add(_msPetty);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ClearData();
        }

        //protected void PettyDDL_SelectedIndexChanged1(object sender, EventArgs e)
        //{
        //    this.AccountTextBox.Text = PettyDDL.SelectedValue;
        //}
        //protected void AccountTextBox_TextChanged1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.PettyDDL.SelectedValue = this.AccountTextBox.Text;
        //    }
        //    catch (Exception ex)
        //    {

        //        this.AccountTextBox.Text = "";

        //    }

        //}

    }
}