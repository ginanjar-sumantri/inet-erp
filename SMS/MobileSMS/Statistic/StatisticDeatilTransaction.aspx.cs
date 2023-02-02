using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.SMSWeb.Statistic
{
    public partial class StatisticDeatilTransaction : StatisticBase
    {
        private LoginBL _loginBL = new LoginBL();
        protected StatisticBL _statisticBL = new StatisticBL();
        protected String _userID, _organization, _fgAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["userID"] != null && HttpContext.Current.Session["Organization"] != null && HttpContext.Current.Session["FgWebAdmin"] != null)
            {
                if ((HttpContext.Current.Session["userID"].ToString() == "") || (HttpContext.Current.Session["Organization"].ToString() == "") || (HttpContext.Current.Session["FgWebAdmin"].ToString() == ""))
                    Response.Redirect("../Login/Login.aspx");
            }
            else Response.Redirect("../Login/Login.aspx");

            if (Session["FgAdmin"] != null)
            {
                if (Session["FgAdmin"].ToString() == "False" && _loginBL.getPackageName(Session["Organization"].ToString(), Session["UserID"].ToString()) == "PERSONAL") Response.Redirect("../Message/Compose.aspx");
                _userID = Session["UserID"].ToString();
                _organization = Session["Organization"].ToString();
                _fgAdmin = Session["FgAdmin"].ToString();
            }
            ShowData();
        }

        protected void ShowData() {
            this.DetailTransRepeater.DataSource = _statisticBL.GetListAllTransaction(_organization);
            this.DetailTransRepeater.DataBind();
        }
        protected void DetailTransRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            TrBalance _temp = (TrBalance)e.Item.DataItem;

            Literal _TransDateLiteral = (Literal)e.Item.FindControl("TransDateLiteral");
            _TransDateLiteral.Text = Convert.ToDateTime( _temp.TransDate).ToString("dd MMM yyyy");

            Literal _DescriptionLiteral = (Literal)e.Item.FindControl("DescriptionLiteral");
            _DescriptionLiteral.Text =_temp.Description ;

            Literal _AmountLiteral = (Literal)e.Item.FindControl("AmountLiteral");
            _AmountLiteral.Text = Convert.ToDecimal (_temp.Amount).ToString("#,##0.00");

            Literal _IncDecLiteral = (Literal)e.Item.FindControl("IncDecLiteral");
            _IncDecLiteral.Text = Convert.ToBoolean(_temp.fgIncrease)?"Increase":"Decrease";
        }
        protected void BackButton_Click1(object sender, EventArgs e)
        {
            Response.Redirect(this._homePage);
        }
}
}