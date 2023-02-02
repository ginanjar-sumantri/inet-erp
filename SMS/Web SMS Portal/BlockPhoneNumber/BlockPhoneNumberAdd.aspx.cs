using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.SMSWeb.BlockPhoneNumber
{
    public partial class BlockPhoneNumberAdd : BlockPhoneNumberBase
    {
        protected BlockPhoneNumberBL _blockPhoneNumber = new BlockPhoneNumberBL();
        private LoginBL _loginBL = new LoginBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.CekSession();

            if (Session["FgWebAdmin"] != null)
                if (Session["FgWebAdmin"].ToString() == "False" && _loginBL.getPackageName(Session["Organization"].ToString(), Session["UserID"].ToString()) == "PERSONAL") Response.Redirect("../Message/Compose.aspx");
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.BlockPhoneNumberTextBox.Text != "")
            {
                MsBlockPhoneBook _newData = new MsBlockPhoneBook();
                _newData.OrganizationID = Convert.ToInt32( Session["Organization"]);
                _newData.UserID = Session["UserID"].ToString();
                _newData.phoneNumber = this.BlockPhoneNumberTextBox.Text ;
                if (_blockPhoneNumber.AddBlockPhoneNumber(_newData))
                    Response.Redirect(this._homePage + "?result=Success Insert Data.");
                else
                    this.WarningLabel.Text = "Failed to insert data.";
            }
            else this.WarningLabel.Text = "Block Phone Number must be filled.";            
        }
        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }
    }
}