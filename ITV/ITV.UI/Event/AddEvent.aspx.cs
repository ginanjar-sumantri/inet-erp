using System;
using System.Web;
using System.Web.UI;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Configuration;
using ITV.UI.ApplicationClass;
using ITV.DataAccess.ITVDatabase;
using ITV.BusinessRule;
using ITV.DataAccess.ITVDatabase;

namespace ITV.UI.Event
{
    public partial class AddEvent : EventBase
    {
        protected ItVdB db = new ItVdB("Database=ITVDB;Data Source=192.168.200.212;User Id=root;Password=itv;DbLinqProvider=MySql;DbLinqConnectionType=MySql.Data.MySqlClient.MySqlConnection, MySql.Data");

        private EventBL _eventBL = new EventBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLIteral.Text = this._pageTitleAddLiteral;

                this.SaveImageButton.ToolTip = this._toolTipSave;
                this.ResetImageButton.ToolTip = this._toolTipReset;
                this.CancelImageButton.ToolTip = this._toolTipCancel;
                this.ClearData();
            }

        }

        protected void ClearData()
        {
            this.EventName.Text = "";
            this.DescriptionTextBox.Text = "";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._listPage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

    }
}
