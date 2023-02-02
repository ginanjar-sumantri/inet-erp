using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.CustomControl;
using System.Collections;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Web;

public partial class Search_Search : System.Web.UI.Page
{
    /// <summary>
    /// to call this [search] Pop Up window you need to post some querystrings parameter
    /// 1. the function value cathcer from window opener that will process the value returned -> [valueCatcher]
    /// 2. configuration code, refering to table Search Config -> [configCode]
    /// 3. optional parameter -> [paramWhere] contain generated condition string based on UI input
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 

    private SearchBL _searchBL = new SearchBL();
    private SortedList _stringChoice = new SortedList();
    private SortedList _numericChoice = new SortedList();
    private SortedList _dateChoice = new SortedList();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                String spawnJS = "<script language='JavaScript'>\n";
                spawnJS += "function returnValue (val) {\n";
                spawnJS += "window.opener." + Request.QueryString["valueCatcher"] + "(val);\n";
                spawnJS += "window.close();\n";
                spawnJS += "}\n";
                spawnJS += "</script>";
                this.javaScriptDeclaration.Text = spawnJS;

                this.BackButton.OnClientClick = "window.close()";

                this.headerTabel.DataSource = this._searchBL.getFieldList(Request.QueryString["configCode"], true);
                this.headerTabel.DataBind();

                //this.btnFilter.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/searchBtn.jpg";
                //this.btnShowAll.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/showallBtn.jpg";
                //this.CloseButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/close2.jpg";
                //this.CloseButton.OnClientClick = "window.close()";]

                SortedList _whereFieldDataSource = new SortedList();
                List<SearchField> _searchFields = this._searchBL.getFieldCondition(Request.QueryString["configCode"]);
                foreach (SearchField _rs in _searchFields)
                {
                    _whereFieldDataSource.Add(_rs.FieldLabel, _rs.FieldName + "|" + _rs.FieldType);
                }
                this.whereField.DataSource = _whereFieldDataSource;
                this.whereField.DataValueField = "Value";
                this.whereField.DataTextField = "Key";
                this.whereField.DataBind();
            }

            this._stringChoice.Add("1%val%", "Contains");
            this._stringChoice.Add("2val%", "Starts With");
            this._stringChoice.Add("3%val", "Ends With");
            this._numericChoice.Add("<", "Less Than");
            this._numericChoice.Add("=", "Equal");
            this._numericChoice.Add(">", "Greater Than");
            this._dateChoice.Add("<", "Before");
            this._dateChoice.Add("=", "At");
            this._dateChoice.Add(">", "After");
            this.whereField_SelectedIndexChanged(new Object(), new EventArgs());
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void headerTabel_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton _headerText = (LinkButton)e.Item.FindControl("headerText");
        SearchField _temp = (SearchField)e.Item.DataItem;
        if (_temp.FieldLabel != null)
            _headerText.Text = _temp.FieldLabel.ToString();
        else
            _headerText.Text = _temp.FieldName.ToString();

        _headerText.CommandName = "sort";
        _headerText.CommandArgument = _temp.FieldName.ToString();
    }

    protected void hasilPencarian_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        String _itemData = (String)e.Item.DataItem;
        Literal _rowContent = (Literal)e.Item.FindControl("rowContent");
        _rowContent.Text = "<td>" + _itemData.Replace("|", "</td><td>") + "</td>";
        Button _btnPilih = (Button)e.Item.FindControl("btnPilih");
        _btnPilih.Attributes.Add("onclick", "returnValue('" + _itemData.Replace("'", "\\'") + "')");
    }

    protected void headerTabel_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "sort")
            {
                if (this.sortField.Value == e.CommandArgument.ToString())
                {
                    if (this.ascDesc.Value == "false")
                    {
                        this.ascDesc.Value = "true";
                    }
                    else
                    {
                        this.ascDesc.Value = "false";
                    }
                }
                else
                {
                    this.sortField.Value = e.CommandArgument.ToString();
                    this.ascDesc.Value = "false";
                }
                String _whereString = "";
                if (this.whereCon.Value != "")
                    _whereString = this.whereCon.Value.ToString();
                Boolean _descending = false;
                if (this.ascDesc.Value.ToString() == "true")
                    _descending = true;

                if (Request.QueryString["paramWhere"] != "")
                {

                }

                this.hasilPencarian.DataSource = this._searchBL.getSearchDataResult(Request.QueryString["configCode"], _whereString, this.sortField.Value.ToString(), _descending, Request.QueryString["paramWhere"] == null ? "" : (Request.QueryString["paramWhere"].Replace("spasi", " ").Replace("samadengan", " = ").Replace("petik", "'")));
                this.hasilPencarian.DataBind();
            }
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void whereField_SelectedIndexChanged(object sender, EventArgs e)
    {
        String[] _type = this.whereField.SelectedValue.Split('|');
        if (_type[1] == "N")
        {
            this.whereType.DataSource = this._numericChoice;
        }
        else if (_type[1] == "D")
        {
            this.whereType.DataSource = this._dateChoice;
        }
        else
        {
            this.whereType.DataSource = this._stringChoice;
        }
        this.whereType.DataValueField = "Key";
        this.whereType.DataTextField = "Value";
        this.whereType.DataBind();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.whereValue.Text != "")
            {
                String _whereString = " WHERE ";
                String[] _type = this.whereField.SelectedValue.Split('|');
                if (_type[1] == "S")
                {
                    _whereString += _type[0] + " ";
                    _whereString += "like '";
                    _whereString += Request.Form[this.whereType.ClientID].Substring(1).Replace("val", this.whereValue.Text) + "'";
                }
                else if (_type[1] == "D")
                {
                    _whereString += " CONVERT ( DateTime , cast ( " + _type[0].Trim() + " as varchar(12) ) , 101 ) ";
                    _whereString += Request.Form[this.whereType.ClientID] + " '" + this.whereValue.Text + "'";
                }
                else
                {
                    _whereString += _type[0] + " ";
                    _whereString += Request.Form[this.whereType.ClientID] + " " + this.whereValue.Text;
                }

                this.hasilPencarian.DataSource = this._searchBL.getSearchDataResult(Request.QueryString["configCode"], _whereString, "", false, Request.QueryString["paramWhere"] == null ? "" : (Request.QueryString["paramWhere"].Replace("spasi", " ").Replace("samadengan", " = ").Replace("petik", "'")));
                this.hasilPencarian.DataBind();
                this.whereCon.Value = _whereString;
                this.whereType.SelectedValue = Request.Form[this.whereType.ClientID];
            }
            else
            {
                this.hasilPencarian.DataSource = this._searchBL.getSearchDataResult(Request.QueryString["configCode"], "", "", false, Request.QueryString["paramWhere"] == null ? "" : (Request.QueryString["paramWhere"].Replace("spasi", " ").Replace("samadengan", " = ").Replace("petik", "'")));
                this.hasilPencarian.DataBind();
            }
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void btnShowAll_Click(object sender, EventArgs e)
    {
        try
        {
            this.hasilPencarian.DataSource = this._searchBL.getSearchDataResult(Request.QueryString["configCode"], "", "", false, Request.QueryString["paramWhere"] == null ? "" : (Request.QueryString["paramWhere"].Replace("spasi", " ").Replace("samadengan", " = ").Replace("petik", "'")));
            this.hasilPencarian.DataBind();
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void ResetButton_Click(object sender, ImageClickEventArgs e)
    {
        this.whereField.SelectedIndex = 0;
        this.whereType.SelectedIndex = 0;
        this.whereValue.Text = "";

        this.hasilPencarian.DataSource = null;
        this.hasilPencarian.DataBind();
    }

    protected void errorhandler(Exception ex)
    {
        //ErrorHandler.Record(ex, EventLogEntryType.Error);
        String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "SEARCH");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
    }
}

