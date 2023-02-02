using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.BackEndSMSPortal
{
    public partial class GatewayAvailabilityAlert : GatewayAvailabilityAlertBase
    {
        protected OrganizationSettingBL _orgBL = new OrganizationSettingBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);
                this.RequestParam();
                
                this._orgBL.Leverage(this.Leverage);
                this.OrganizationRepeater.DataSource = _orgBL.getListOrganization();
                this.OrganizationRepeater.DataBind();
            }
        }
        protected void RequestParam() { this.Leverage = this._nvcExtractor.GetValue(this._codeKey); }

        protected void OrganizationRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            MsOrganization _temp = (MsOrganization)e.Item.DataItem;
            Label _OrganizationNameLiteral = (Label)e.Item.FindControl("OrganizationNameLiteral");
            _OrganizationNameLiteral.Text = _temp.OrganizationName;
            Label _HostedLiteral = (Label)e.Item.FindControl("HostedLiteral");
            _HostedLiteral.Text = Convert.ToBoolean(_temp.FgHosted) ? "Yes" : "No";
            _HostedLiteral.Style.Add("color", Convert.ToBoolean(_temp.FgHosted) ? "Yellow" : "LightGreen");
            Label _GatewayAvailableLiteral = (Label)e.Item.FindControl("GatewayAvailableLiteral");
            _GatewayAvailableLiteral.Text = Convert.ToBoolean(_temp.GatewayStatus) ? "Available" : "Not Available";
            _GatewayAvailableLiteral.Style.Add("background-color", Convert.ToBoolean(_temp.GatewayStatus) ? "Green" : "Red");
            _GatewayAvailableLiteral.Style.Add("color", Convert.ToBoolean(_temp.GatewayStatus) ? "LightGreen" : "White");
        }
        protected void TimerAttention_Tick(object sender, EventArgs e)
        {
            this.AttentionCounter.Value = (Convert.ToInt16(this.AttentionCounter.Value) + 1).ToString();
            this.OrganizationRepeater.DataSource = _orgBL.getListOrganization();
            this.AttentionPanel.Visible = _orgBL.NeedAttention();
            this.OrganizationRepeater.DataBind();
            if (Convert.ToInt16(this.AttentionCounter.Value) >= 12)
            {
                //_orgBL.SweepGateway();
                this.AttentionCounter.Value = "0";
            }
        }
    }
}