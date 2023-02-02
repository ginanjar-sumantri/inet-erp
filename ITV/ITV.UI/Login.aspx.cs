using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITV.BusinessRule;

namespace ITV.UI
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            UserBL _userBL = new UserBL();
            Boolean _result = _userBL.ValidateUser(this.TextBox1.Text, this.TextBox2.Text);
            if (_result)
            {
                this.Label.Text = "User Exist";
            }
            else
            {
                this.Label.Text = "User Not Valid";
            }
        }
    }
}