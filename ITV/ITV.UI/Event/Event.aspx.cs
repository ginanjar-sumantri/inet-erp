using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITV.UI.MsEvent
{
	public partial class Event : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");
            this.DataPagerBottomButton.Attributes.Add("Style", "visibility: hidden;");


		}

        protected void Showdata()
        {

        }
	}
}