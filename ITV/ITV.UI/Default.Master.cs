using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITV.BusinessRule;

namespace ITV.UI
{
    public partial class Default : System.Web.UI.MasterPage
    {
        private MenuBL _menu = new MenuBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.MenuLiteral = new MenuBL            
            this.MenuLiteral.Text = this._menu.GetMenu(0, "");
        }
    }
}