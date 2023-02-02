using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.CompConfig
{
    public partial class CompConfigCompact : CompConfigBase
    {
        private CompanyConfig _compConfigBL = new CompanyConfig();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.ClearLabel();
                this.ShowGroupSettingDDL();
                this.ShowData();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowGroupSettingDDL()
        {
            this.GroupSettingDropDownList.Items.Clear();
            this.GroupSettingDropDownList.DataSource = _compConfigBL.GetListCompanyConfigGroup();
            this.GroupSettingDropDownList.DataBind();
        }

        public void ShowData()
        {
            this.ListRepeater.DataSource = this._compConfigBL.GetListCompanyConfig(this.GroupSettingDropDownList.SelectedValue);
            this.ListRepeater.DataBind();
        }

        protected void GroupSettingDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void ListRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CompanyConfiguration _temp = (CompanyConfiguration)e.Item.DataItem;
                string _code = _temp.ConfigCode.ToString();

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
                _tableRow.Attributes.Add("OnMouseOver", "this.style.backgroundColor='" + this._rowColorHover + "';");
                if (e.Item.ItemType == ListItemType.Item)
                {
                    _tableRow.Attributes.Add("style", "background-color:" + this._rowColor);
                    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColor + "';");
                }
                if (e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    _tableRow.Attributes.Add("style", "background-color:" + this._rowColorAlternate);
                    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColorAlternate + "';");
                }

                Literal _configCodeLiteral = (Literal)e.Item.FindControl("ConfigCodeLiteral");
                _configCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ConfigCode);

                Literal _descLiteral = (Literal)e.Item.FindControl("DescriptionLiteral");
                _descLiteral.Text = HttpUtility.HtmlEncode(_temp.ConfigDescription);

                TextBox _valueTextBox = (TextBox)e.Item.FindControl("ValueTextBox");
                DropDownList _valueDropDownList = (DropDownList)e.Item.FindControl("ValueDropDownList");
                ImageButton _updateButton = (ImageButton)e.Item.FindControl("UpdateButton");
                _updateButton.CommandArgument = _temp.ConfigCode;

                CompConfigDataType _switch = CompConfigDataTypeMapper.GetCompConfigDataType(_temp.ValueType);
                switch (_switch)
                {
                    case CompConfigDataType.String:
                        _valueTextBox.Visible = true;
                        _valueDropDownList.Visible = false;
                        _updateButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";

                        _valueTextBox.Text = HttpUtility.HtmlEncode(_temp.SetValue);
                        _updateButton.CommandName = CompConfigDataTypeMapper.GetCompConfigDataType(CompConfigDataType.String);
                        break;
                    case CompConfigDataType.Multiline:
                        _valueTextBox.Visible = true;
                        _valueDropDownList.Visible = false;
                        _updateButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";

                        _valueTextBox.TextMode = TextBoxMode.MultiLine;
                        _valueTextBox.Height = 80;
                        _valueTextBox.Text = HttpUtility.HtmlEncode(_temp.SetValue);
                        _updateButton.CommandName = CompConfigDataTypeMapper.GetCompConfigDataType(CompConfigDataType.String);
                        break;
                    case CompConfigDataType.Enum:
                        _valueTextBox.Visible = false;
                        _valueDropDownList.Visible = true;
                        _updateButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";

                        String[] _value = _temp.SQLExpr.Split(',');
                        foreach (var _item in _value)
                        {
                            _valueDropDownList.Items.Add(new ListItem(_item.ToString()));
                        }
                        _valueDropDownList.SelectedValue = _temp.SetValue;

                        _updateButton.CommandName = CompConfigDataTypeMapper.GetCompConfigDataType(CompConfigDataType.Enum);
                        break;
                    case CompConfigDataType.Decimal:
                        _valueTextBox.Visible = true;
                        _valueDropDownList.Visible = false;
                        _updateButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";

                        _valueTextBox.Attributes.Add("OnBlur", "if (isNaN(this.value)) x.value = '';");
                        _valueTextBox.Text = HttpUtility.HtmlEncode(_temp.SetValue);
                        _updateButton.CommandName = CompConfigDataTypeMapper.GetCompConfigDataType(CompConfigDataType.Decimal);
                        break;
                    case CompConfigDataType.Int:
                        _valueTextBox.Visible = true;
                        _valueDropDownList.Visible = false;
                        _updateButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";

                        _valueTextBox.Attributes.Add("OnBlur", "if (isNaN(this.value)) x.value = '';");
                        _valueTextBox.Text = HttpUtility.HtmlEncode(_temp.SetValue);
                        _updateButton.CommandName = CompConfigDataTypeMapper.GetCompConfigDataType(CompConfigDataType.Int);
                        break;
                    case CompConfigDataType.SQLQuery:
                        _valueTextBox.Visible = false;
                        _valueDropDownList.Visible = true;
                        _updateButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";

                        String[] _valueStr = _temp.SQLExpr.Split(',');
                        foreach (var _item in _valueStr)
                        {
                            String[] _itemStr = _item.Split('|');
                            _valueDropDownList.Items.Add(new ListItem(_itemStr[1], _itemStr[0]));
                        }
                        _valueDropDownList.SelectedValue = _temp.SetValue;

                        _updateButton.CommandName = CompConfigDataTypeMapper.GetCompConfigDataType(CompConfigDataType.SQLQuery);
                        break;
                }

                if (_temp.AlowEdit == YesNoDataMapper.GetYesNo(YesNo.No))
                {
                    _updateButton.Visible = false;
                }
            }
        }

        protected void ListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            String _code = "";
            String _value = "";
            if (e.CommandName == CompConfigDataTypeMapper.GetCompConfigDataType(CompConfigDataType.String))
            {
                _code = e.CommandArgument.ToString();
                _value = ((TextBox)e.Item.FindControl("ValueTextBox")).Text;
            }
            else if (e.CommandName == CompConfigDataTypeMapper.GetCompConfigDataType(CompConfigDataType.Multiline))
            {
                _code = e.CommandArgument.ToString();
                _value = ((DropDownList)e.Item.FindControl("ValueDropDownList")).Text;
            }
            else if (e.CommandName == CompConfigDataTypeMapper.GetCompConfigDataType(CompConfigDataType.Enum))
            {
                _code = e.CommandArgument.ToString();
                _value = ((DropDownList)e.Item.FindControl("ValueDropDownList")).Text;
            }
            else if (e.CommandName == CompConfigDataTypeMapper.GetCompConfigDataType(CompConfigDataType.Decimal))
            {
                _code = e.CommandArgument.ToString();
                _value = ((TextBox)e.Item.FindControl("ValueTextBox")).Text;
            }
            else if (e.CommandName == CompConfigDataTypeMapper.GetCompConfigDataType(CompConfigDataType.Int))
            {
                _code = e.CommandArgument.ToString();
                _value = ((TextBox)e.Item.FindControl("ValueTextBox")).Text;
            }
            else if (e.CommandName == CompConfigDataTypeMapper.GetCompConfigDataType(CompConfigDataType.SQLQuery))
            {
                _code = e.CommandArgument.ToString();
                _value = ((DropDownList)e.Item.FindControl("ValueDropDownList")).Text;
            }

            CompanyConfiguration _compConfig = this._compConfigBL.GetSingle(_code);
            _compConfig.SetValue = _value;
            Boolean _result = this._compConfigBL.Edit(_compConfig);
            if (_result)
            {
                this.ClearLabel();
                this.WarningLabel.Text = _code + " successfully edited";
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Edit failed";
            }
        }
    }
}
