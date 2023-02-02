using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using BusinessRule.POSInterface;
using System.Web.UI.HtmlControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

public partial class General_Member : System.Web.UI.Page
{
    private POSRetailBL _retailBL = new POSRetailBL();

    private int _page;
    private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
    private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
    private int _no = 0;
    private int _nomor = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack == true)
        {
            String spawnJS = "<script language='JavaScript'>\n";
            spawnJS += "function returnValue (val) {\n";
            spawnJS += "window.opener." + Request.QueryString["valueCatcher"] + "(val);\n";
            spawnJS += "window.close();\n";
            spawnJS += "}\n";
            spawnJS += "</script>";
            this.javaScriptDeclaration.Text = spawnJS;

            //this.SearchButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/searchBtn.jpg";
            //this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
            //this.CloseButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/close2.jpg";
            this.CloseButton.OnClientClick = "window.close()";

            this.ClearLabel();
            this.ClearData();
            this.ShowData();
        }
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        this.ShowData();
    }

    protected void ResetButton_Click(object sender, EventArgs e)
    {
        this.ClearLabel();
        this.ClearData();
        this.ShowData();
    }

    public void ClearData()
    {
        this.CustIDTextBox.Text = "";
        this.SearchFieldTextBox.Text = "";
    }

    public void ClearLabel()
    {
    }

    public void ShowData()
    {
        try
        {
            this.ListRepeater.DataSource = this._retailBL.GetListRetailOnHold(this.CustIDTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
            this.ListRepeater.DataBind();
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.ShowData();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        this.CustIDTextBox.Text = "";
        this.SearchFieldTextBox.Text = "";
        this.ShowData();
    }

    protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            POSTrRetailHd _temp = (POSTrRetailHd)e.Item.DataItem;

            string _code = _temp.TransNmbr.ToString();

            Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
            _no = _page * _maxrow;
            _no += 1;
            _no = _nomor + _no;
            _noLiteral.Text = _no.ToString();
            _nomor += 1;

            Literal _jobCodeLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
            _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

            Literal _refNoLiteral = (Literal)e.Item.FindControl("RefNoLiteral");
            _refNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

            Literal _custNameLiteral = (Literal)e.Item.FindControl("CustomerNameLiteral");
            _custNameLiteral.Text = HttpUtility.HtmlEncode(_temp.CustName);

            Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
            DateTime _date = Convert.ToDateTime(_temp.TransDate);
            _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

            ImageButton _cancelOrderButton = (ImageButton)e.Item.FindControl("CancelOrder");
            //_cancelOrderButton.PostBackUrl = this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
            //_cancelOrderButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
            _cancelOrderButton.Attributes.Add("OnClick", "return CancelOrder();");
            _cancelOrderButton.CommandName = "CancelOrderButton";
            _cancelOrderButton.CommandArgument = _temp.TransNmbr;

            ImageButton _openHoldButton = (ImageButton)e.Item.FindControl("OpenHold");
            _openHoldButton.Attributes.Add("onclick", "returnValue('" + _temp.TransNmbr.Replace("'", "\\'") + "')");
        }
    }

    protected void ListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "CancelOrderButton")
            {
                Boolean _result = _retailBL.SetVOID(e.CommandArgument.ToString(), true);

                if (_result == true)
                {
                    this.ClearLabel();
                    this.ClearData();
                    this.ShowData();
                }
            }
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void errorhandler(Exception ex)
    {
        //ErrorHandler.Record(ex, EventLogEntryType.Error);
        String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "OPENHOLD");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
    }
}
