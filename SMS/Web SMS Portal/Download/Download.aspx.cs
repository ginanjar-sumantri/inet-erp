using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMS.SMSWeb.Download
{
    public partial class Download : SMSWebBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CekSession();
        }
    }
}