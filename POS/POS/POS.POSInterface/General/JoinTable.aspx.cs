using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule.POSInterface;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

public partial class General_JoinTable : System.Web.UI.Page
{
    private POSRetailBL _retailBL = new POSRetailBL();
    private POSInternetBL _internetBL = new POSInternetBL();
    private POSReasonBL _reasonBL = new POSReasonBL();

    private int _page;
    private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
    private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
    private int _no = 0;
    private int _nomor = 0;

    private int _page2;
    private int _maxrow2 = Convert.ToInt32(ApplicationConfig.ListPageSize);
    private int _maxlength2 = Convert.ToInt32(ApplicationConfig.DataPagerRange);
    private int _no2 = 0;
    private int _nomor2 = 0;

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

            this.CloseButton.OnClientClick = "window.close()";

            this.ClearLabel();
            this.ClearData();
            this.ChangeVisiblePanel(0);
            this.ShowData();
        }
    }

    public void ClearLabel()
    {
    }

    public void ClearData()
    {
        this.CustIDTextBox.Text = "";
        this.SearchFieldDDL.SelectedIndex = 0;
        this.SearchFieldTextBox.Text = "";

        this.TransNmbrHiddenField.Value = "";

        this.DetailListRepeater.DataSource = null;
        this.DetailListRepeater.DataBind();
        this.ReasonListRepeater.DataSource = null;
        this.ReasonListRepeater.DataBind();
    }

    protected void ChangeVisiblePanel(Byte _prmValue)
    {
        if (_prmValue == 0)
        {
            this.DetailListPanel.Visible = true;
            this.ReasonListPanel.Visible = false;
        }
        else
        {
            this.DetailListPanel.Visible = false;
            this.ReasonListPanel.Visible = true;
        }
    }

    public void ShowData()
    {
        try
        {
            if (Request.QueryString["pos"].ToString() == "retail")
            {
                this.JobOrderListRepeater.DataSource = this._retailBL.GetListRetailSendToCashier(this.CustIDTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
                this.JobOrderListRepeater.DataBind();
            }
            else if (Request.QueryString["pos"].ToString() == "internet")
            {
                this.JobOrderListRepeater.DataSource = this._internetBL.GetListInternetSendToCashier(this.CustIDTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
                this.JobOrderListRepeater.DataBind();
            }
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

    protected void JobOrderListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (Request.QueryString["pos"].ToString() == "retail")
            {
                POSTrRetailHd _temp = (POSTrRetailHd)e.Item.DataItem;

                string _code = _temp.TransNmbr.ToString();

                //Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //_no = _page * _maxrow;
                //_no += 1;
                //_no = _nomor + _no;
                //_noLiteral.Text = _no.ToString();
                //_nomor += 1;

                Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                Literal _jobCodeLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
                _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
                DateTime _date = Convert.ToDateTime(_temp.TransDate);
                _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                ImageButton _cancelPaidButton = (ImageButton)e.Item.FindControl("CancelPaidButton");
                _cancelPaidButton.CommandName = "CancelPaid";
                _cancelPaidButton.CommandArgument = _temp.TransNmbr;

                ImageButton _viewJobOrderButton = (ImageButton)e.Item.FindControl("ViewJobOrderButton");
                _viewJobOrderButton.CommandName = "ViewDetail";
                _viewJobOrderButton.CommandArgument = _temp.TransNmbr;

                ImageButton _joinjobButton = (ImageButton)e.Item.FindControl("JoinJobOrderButton");
                _joinjobButton.Attributes.Add("onclick", "returnValue('" + _temp.TransNmbr.Replace("'", "\\'") + "')");
            }
            else if (Request.QueryString["pos"].ToString() == "internet")
            {
                POSTrInternetHd _temp = (POSTrInternetHd)e.Item.DataItem;

                string _code = _temp.TransNmbr.ToString();

                //Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //_no = _page * _maxrow;
                //_no += 1;
                //_no = _nomor + _no;
                //_noLiteral.Text = _no.ToString();
                //_nomor += 1;

                Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                Literal _jobCodeLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
                _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
                DateTime _date = Convert.ToDateTime(_temp.TransDate);
                _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                ImageButton _cancelPaidButton = (ImageButton)e.Item.FindControl("CancelPaidButton");
                _cancelPaidButton.CommandName = "CancelPaid";
                _cancelPaidButton.CommandArgument = _temp.TransNmbr;

                ImageButton _viewJobOrderButton = (ImageButton)e.Item.FindControl("ViewJobOrderButton");
                _viewJobOrderButton.CommandName = "ViewDetail";
                _viewJobOrderButton.CommandArgument = _temp.TransNmbr;

                ImageButton _joinjobButton = (ImageButton)e.Item.FindControl("JoinJobOrderButton");
                _joinjobButton.Attributes.Add("onclick", "returnValue('" + _temp.TransNmbr.Replace("'", "\\'") + "')");
            }
        }
    }

    protected void JobOrderListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "CancelPaid")
            {
                this.ChangeVisiblePanel(1);

                this.TransNmbrHiddenField.Value = e.CommandArgument.ToString();
                this.ReasonListRepeater.DataSource = this._reasonBL.GetList();
                this.ReasonListRepeater.DataBind();

            }
            else if (e.CommandName == "ViewDetail")
            {
                this.ChangeVisiblePanel(0);
                if (Request.QueryString["pos"].ToString() == "retail")
                {
                    this.DetailListRepeater.DataSource = this._retailBL.GetListRetailDtByTransNmbr(e.CommandArgument.ToString());
                    this.DetailListRepeater.DataBind();
                }
                else if (Request.QueryString["pos"].ToString() == "internet")
                {
                    this.DetailListRepeater.DataSource = this._internetBL.GetListInternetDtByTransNmbr(e.CommandArgument.ToString());
                    this.DetailListRepeater.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void DetailListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (Request.QueryString["pos"].ToString() == "retail")
            {
                POSTrRetailDt _temp = (POSTrRetailDt)e.Item.DataItem;

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no2 = _page2 * _maxrow2;
                _no2 += 1;
                _no2 = _nomor2 + _no2;
                _noLiteral.Text = _no2.ToString();
                _nomor2 += 1;

                Literal _productNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                _productNameLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);

                Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                _qtyLiteral.Text = HttpUtility.HtmlEncode(_temp.Qty.ToString());
            }
            else if (Request.QueryString["pos"].ToString() == "internet")
            {
                POSTrInternetDt _temp = (POSTrInternetDt)e.Item.DataItem;

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no2 = _page2 * _maxrow2;
                _no2 += 1;
                _no2 = _nomor2 + _no2;
                _noLiteral.Text = _no2.ToString();
                _nomor2 += 1;

                Literal _productNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                _productNameLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);

                Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                _qtyLiteral.Text = HttpUtility.HtmlEncode(_temp.Qty.ToString());
            }
        }
    }

    protected void ReasonListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            POSMsReason _temp = (POSMsReason)e.Item.DataItem;

            String _reasonCode = _temp.ReasonCode.ToString();

            ImageButton _pickReasonButton = (ImageButton)e.Item.FindControl("PickReasonImageButton");
            _pickReasonButton.Attributes.Add("OnClick", "return CancelPaid();");
            _pickReasonButton.CommandName = "PickButton";
            _pickReasonButton.CommandArgument = _reasonCode;

            Literal _reasonLiteral = (Literal)e.Item.FindControl("ReasonLiteral");
            _reasonLiteral.Text = HttpUtility.HtmlEncode(_temp.ReasonName);
        }
    }

    protected void ReasonListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "PickButton")
            {
                Boolean _result = false;
                if (Request.QueryString["pos"].ToString() == "retail")
                {
                    _result = this._retailBL.SetVOID(this.TransNmbrHiddenField.Value, e.CommandArgument.ToString(), true);
                }
                else if (Request.QueryString["pos"].ToString() == "internet")
                {
                    _result = this._internetBL.SetVOID(this.TransNmbrHiddenField.Value, e.CommandArgument.ToString(), true);
                }

                if (_result == true)
                {
                    this.ClearLabel();
                    this.ClearData();
                    this.ShowData();

                    this.ChangeVisiblePanel(0);
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
        String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "JOINTABLE");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
    }
}
