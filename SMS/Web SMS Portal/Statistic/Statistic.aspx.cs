using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.SMSWeb.Statistic
{
    public partial class Statistic_Statistic : StatisticBase
    {
        private LoginBL _loginBL = new LoginBL();
        protected StatisticBL _statisticBL = new StatisticBL();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (HttpContext.Current.Session["UserID"] != null && HttpContext.Current.Session["Organization"] != null && HttpContext.Current.Session["FgWebAdmin"] != null)
            //{
            //    if ((HttpContext.Current.Session["UserID"].ToString() == "") || (HttpContext.Current.Session["Organization"].ToString() == "") || (HttpContext.Current.Session["FgWebAdmin"].ToString() == ""))
            //        Response.Redirect("../Login/Login.aspx");
            //}
            //else Response.Redirect("../Login/Login.aspx");
            this.CekSession();

            if (Session["FgWebAdmin"] != null)
            {
                if (Session["FgWebAdmin"].ToString() == "False" && _loginBL.getPackageName(Session["Organization"].ToString(), Session["UserID"].ToString()) == "PERSONAL") Response.Redirect("../Message/Compose.aspx");
                _userID = Session["UserID"].ToString();
                _orgID = Session["Organization"].ToString();
                _fgAdmin = Session["FgWebAdmin"].ToString();
            }

            if (Convert.ToBoolean(_fgAdmin))
            {
                this.AdminPanel.Visible = true;
                for (int i = 1; i <= 12; i++)
                    this.UserMonthDropDownList.Items.Add(new ListItem(i.ToString()));
                for (int i = 2010; i <= 2050; i++)
                {
                    this.UserYearDropDownList.Items.Add(new ListItem(i.ToString()));
                    this.OutBalanceYearDropDownList.Items.Add(new ListItem(i.ToString()));
                }
            }
            else {
                this.AdminPanel.Visible = false;
            }

            for (int i = 1; i <= 12; i++)
                this.CategoryMonthDropDownList.Items.Add(new ListItem(i.ToString()));
            for (int i = 2010; i <= 2050; i++)
                this.CategoryYearDropDownList.Items.Add(new ListItem(i.ToString()));

            if (!Page.IsPostBack) {
                this.CategoryMonthDropDownList.SelectedValue = this.UserMonthDropDownList.SelectedValue = DateTime.Now.Month.ToString();
                this.OutBalanceYearDropDownList.SelectedValue = this.CategoryYearDropDownList.SelectedValue = this.UserYearDropDownList.SelectedValue = DateTime.Now.Year.ToString();
            }

            ShowData();
        }

        protected void ShowData() {
            this.TodaySentSMSLiteral.Text = _statisticBL.GetTodaySentSMS(_orgID, _userID).ToString();
            this.TodayIncomingSMSLiteral.Text = _statisticBL.GetTodayIncomingSMS(_orgID, _userID).ToString();
            this.TodayJunkLiteral.Text = _statisticBL.GetTodayJunkSMS(_orgID, _userID).ToString();

            this.ThisMonthSentSMSLiteral.Text = _statisticBL.GetThisMonthSentSMS(_orgID, _userID).ToString();
            this.ThisMonthIncomingSMSLiteral.Text = _statisticBL.GetThisMonthIncomingSMS(_orgID, _userID).ToString();

            this.BalanceIncomingLiteral.Text = _statisticBL.GetIncomingBalanceTotal(_orgID).ToString("#,##0.00");

            this.SentSMSByCategoryRepeater.DataSource = _statisticBL.GetListSentSMSByCategory(this.CategoryMonthDropDownList.SelectedValue, this.CategoryYearDropDownList.SelectedValue, _orgID);
            this.SentSMSByCategoryRepeater.DataBind();


            if (Convert.ToBoolean(_fgAdmin))
            {
                this.SentSMSPerUserRepeater.DataSource = _statisticBL.GetListSentSMSByUser(this.UserMonthDropDownList.SelectedValue, this.UserYearDropDownList.SelectedValue, _orgID);
                this.SentSMSPerUserRepeater.DataBind();

                this.BalanceIncomingRepeater.DataSource = _statisticBL.GetListTransactionIncoming(_orgID);
                this.BalanceIncomingRepeater.DataBind();

                this.BalanceOutGoingRepeater.DataSource = _statisticBL.GetListOutgoingMasking(_orgID, this.OutBalanceYearDropDownList.SelectedValue);
                this.BalanceOutGoingRepeater.DataBind();
            }
        }
        protected void SentSMSByCategoryRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            String _temp = (String)e.Item.DataItem;
            String [] _data = _temp.Split('|');
            Literal _CategoryLiteral = (Literal)e.Item.FindControl("CategoryLiteral");
            _CategoryLiteral.Text = _data[0];
            Literal _NumberOfSMSLiteral = (Literal)e.Item.FindControl("NumberOfSMSLiteral");
            _NumberOfSMSLiteral.Text = _data[1];
        }
        protected void SentSMSByUserRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            String _temp = (String)e.Item.DataItem;
            String[] _data = _temp.Split('|');
            Literal _UserNameLiteral = (Literal)e.Item.FindControl("UserNameLiteral");
            _UserNameLiteral.Text = _data[0];
            Literal _NumberOfSMSPerUserLiteral = (Literal)e.Item.FindControl("NumberOfSMSPerUserLiteral");
            _NumberOfSMSPerUserLiteral.Text = _data[1];
        }
        protected void BalanceIncomingRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            String _temp = (String)e.Item.DataItem;
            String[] _data = _temp.Split('|');
            Literal _DateIncomeBalance = (Literal)e.Item.FindControl("DateIncomeBalance");
            _DateIncomeBalance.Text = _data[0];
            Literal _IncomeAmountLiteral = (Literal)e.Item.FindControl("IncomeAmountLiteral");
            _IncomeAmountLiteral.Text = _data[1];
        }
        protected void BalanceOutGoingRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            String _temp = (String)e.Item.DataItem;
            String[] _data = _temp.Split('|');
            Literal _MonthOutgoingBalance = (Literal)e.Item.FindControl("MonthOutgoingBalance");
            _MonthOutgoingBalance.Text = _data[0];
            Literal _OutgoingAmountLiteral = (Literal)e.Item.FindControl("OutgoingAmountLiteral");
            _OutgoingAmountLiteral.Text = _data[1];
        }

        protected void CategoryMonthDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SentSMSByCategoryRepeater.DataSource = _statisticBL.GetListSentSMSByCategory(this.CategoryMonthDropDownList.SelectedValue, this.CategoryYearDropDownList.SelectedValue, _orgID);
            this.SentSMSByCategoryRepeater.DataBind();
        }
        protected void CategoryYearDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SentSMSByCategoryRepeater.DataSource = _statisticBL.GetListSentSMSByCategory(this.CategoryMonthDropDownList.SelectedValue, this.CategoryYearDropDownList.SelectedValue, _orgID);
            this.SentSMSByCategoryRepeater.DataBind();
        }
        protected void UserMonthDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(_fgAdmin))
            {
                this.SentSMSPerUserRepeater.DataSource = _statisticBL.GetListSentSMSByUser(this.UserMonthDropDownList.SelectedValue, this.UserYearDropDownList.SelectedValue, _orgID);
                this.SentSMSPerUserRepeater.DataBind();
            }
        }
        protected void UserYearDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(_fgAdmin))
            {
                this.SentSMSPerUserRepeater.DataSource = _statisticBL.GetListSentSMSByUser(this.UserMonthDropDownList.SelectedValue, this.UserYearDropDownList.SelectedValue, _orgID);
                this.SentSMSPerUserRepeater.DataBind();
            }
        }
        protected void OutBalanceYearDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(_fgAdmin))
            {
                this.BalanceOutGoingRepeater.DataSource = _statisticBL.GetListOutgoingMasking(_orgID, this.OutBalanceYearDropDownList.SelectedValue);
                this.BalanceOutGoingRepeater.DataBind();
            }
        }
        protected void ViewTransactionButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(this._viewDetailTransactionPage);
        }
}
}