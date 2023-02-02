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

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.CompConfigReportParameter
{
    public partial class CompConfigReportParameter : CompanyConfigReportParameterBase
    {
        private CompanyConfigReportParameterBL _compConfigBL = new CompanyConfigReportParameterBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.ClearLabel();
                this.ShowReportNameDDL();
                this.ShowData();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowReportNameDDL()
        {
            this.ReportNameDropDownList.Items.Clear();
            this.ReportNameDropDownList.DataSource = this._compConfigBL.GetListCompanyConfigParamForDDL();
            this.ReportNameDropDownList.DataTextField = "ReportName";
            this.ReportNameDropDownList.DataValueField = "ReportID";
            this.ReportNameDropDownList.DataBind();
        }

        public void ShowData()
        {
            this.ListRepeater.DataSource = this._compConfigBL.GetListCompanyConfigParamByReportID(this.ReportNameDropDownList.SelectedValue);
            this.ListRepeater.DataBind();
        }

        protected void ReportNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void ListRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CompanyConfigReportParameter _temp = (CompanyConfigReportParameter)e.Item.DataItem;
                string _code = _temp.ReportID.ToString();

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

                Literal _paramLiteral = (Literal)e.Item.FindControl("ParameterLiteral");
                _paramLiteral.Text = HttpUtility.HtmlEncode(_temp.ReportParameter);

                Literal _remarkLiteral = (Literal)e.Item.FindControl("RemarkLiteral");
                _remarkLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);

                TextBox _valueTextBox = (TextBox)e.Item.FindControl("ValueTextBox");
                _valueTextBox.Text = HttpUtility.HtmlEncode(_temp.Value);

                ImageButton _updateButton = (ImageButton)e.Item.FindControl("UpdateButton");
                _updateButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                _updateButton.CommandName = "UpdateConfig";
                _updateButton.CommandArgument = _temp.ReportID + "," + _temp.ReportParameter;
            }
        }

        protected void ListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            String _value = "";
            String _errMessage = "";
            if (e.CommandName == "UpdateConfig")
            {
                _value = ((TextBox)e.Item.FindControl("ValueTextBox")).Text;

                CompanyConfigReportParameter _compConfig = this._compConfigBL.GetSingleCompanyConfigParam(e.CommandArgument.ToString(), ref _errMessage);
                _compConfig.Value = _value;
                _compConfig.ModifiedBy = HttpContext.Current.User.Identity.Name;
                _compConfig.ModifiedDate = DateTime.Now;
                Boolean _result = this._compConfigBL.EditCompanyConfigReportParameter(_compConfig, ref _errMessage);
                if (_result)
                {
                    this.ClearLabel();
                    String[] _key = e.CommandArgument.ToString().Split(',');
                    this.WarningLabel.Text = _key[1].ToString() + " successfully edited";
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "Edit failed, " + _errMessage;
                }
            }
        }
    }
}
