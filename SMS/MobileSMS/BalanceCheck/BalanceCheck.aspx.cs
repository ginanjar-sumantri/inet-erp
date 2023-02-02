using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

public partial class BalanceCheck_BalanceCheck : System.Web.UI.Page
{
    BalanceCheckBL _balanceCheckBL = new BalanceCheckBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !Page.IsPostBack ) {
            _balanceCheckBL.BeginGetwayStatusCheck(Session["Organization"].ToString());
            this.AvailableStatus.Visible = false;
            this.balanceCheckCodeTextBox.Text = _balanceCheckBL.BalanceCheckCode(Session["Organization"].ToString());
        }
    }
    protected void btnRequestBalanceInfo_Click(object sender, EventArgs e)
    {
        if (this.AvailableStatus.Visible)
        {
            this.BalanceInfoResponse.Text = "<img src='../Images/waiting.gif'>";
            _balanceCheckBL.RequestBalanceInfo(Session["Organization"].ToString(), this.balanceCheckCodeTextBox.Text);
            this.TimerRequestBalanceInfo.Enabled = true;
            this.AvailabilityTimer.Enabled = false;
        }
        else { this.BalanceInfoResponse.Text = "Gateway status currently unavailable."; }
    }
    protected void TimerRequestBalanceInfo_Tick(object sender, EventArgs e)
    {
        String _balanceInfoResponse = _balanceCheckBL.ReadBalanceResponse(Session["Organization"].ToString());
        if (_balanceInfoResponse != "")
        {
            this.BalanceInfoResponse.Text = _balanceInfoResponse;
            this.TimerRequestBalanceInfo.Enabled = false;
            this.AvailabilityTimer.Enabled = true;
        }
    }
    protected void AvailabilityTimer_Tick(object sender, EventArgs e)
    {
        Boolean _availabilityGateway = _balanceCheckBL.GatewayStatusCheck (Session["Organization"].ToString()) ;
        this.AvailableStatus.Visible = _availabilityGateway;
        this.NotAvailableStatus.Visible = !_availabilityGateway;
    }
}
