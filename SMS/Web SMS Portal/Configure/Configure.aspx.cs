using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.SMSWeb.Configure
{
    public partial class Configure : SMSWebBase
    {
        protected ConfigureBL _configureBL = new ConfigureBL();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CekSession();

            //if (Session["Organization"] != null) {
            //    this._orgID = Session["Organization"].ToString();
            //    this._userID = Session["UserID"].ToString();
            //    this._fgAdmin = Session["FgWebAdmin"].ToString();
            //}
            this.UpdateButton.ImageUrl = "../images/edit.jpg";
            
            if (!Page.IsPostBack)
            {   
                this.AdminPhoneNumberTextBox.Text = _configureBL.getAdminPhoneNumber(_orgID);
                this.AdminEmailTextBox.Text = _configureBL.getAdminEmail(_orgID);
                this.GlobalAutoReplyTextBox.Text = _configureBL.getGlobalAutoReply(_orgID);
                this.FooterAdditionalMessageTextBox.Text = _configureBL.getFooterAdditionalMessage(_orgID);
            }
        }
        protected void UpdateButton_Click(object sender, ImageClickEventArgs e)
        {
            MsOrganization _updateData = _configureBL.getSingleOrganization(_orgID);
            _updateData.GatewayStatusNoticeNumber = this.AdminPhoneNumberTextBox.Text;
            _updateData.Email = this.AdminEmailTextBox.Text;
            _updateData.GlobalReplyMessage = this.GlobalAutoReplyTextBox.Text;
            _updateData.FooterAdditionalMessage = this.FooterAdditionalMessageTextBox.Text;
            this._configureBL.EditSubmit();
        }
    }
}