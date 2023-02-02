using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.Report
{
    public partial class FixedAssetList : ReportBase
    {
        private FixedAssetsBL _fixedAssetBL = new FixedAssetsBL();
        private ReportBL _reportBL = new ReportBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath = "";
        private string _reportPath0 = "Report/FixedAssetList2.rdlc";
        private string _reportPath1 = "Report/FixedAssetList2new.rdlc";
        private string _reportPath2 = "Report/FixedAssetListNonValue.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDFixedAssetList, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer1.Attributes.Add("style", "margin-bottom: 30px;");

            if (!this.Page.IsPostBack == true)
            {
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = "Fixed Asset List";

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer1.Visible = false;

                this.ShowFAGroup();
                this.ShowFASubGroup();
                this.ShowFAStatus();
                this.ClearData();
                this.ShowLocation();
            }
        }

        public void ShowFAGroup()
        {
            this.FAGroupDropDownList.DataTextField = "FAGroupName";
            this.FAGroupDropDownList.DataValueField = "FAGroupCode";
            this.FAGroupDropDownList.DataSource = this._fixedAssetBL.GetListFAGroup();
            if (this.FASubGroupDropDownList.SelectedValue != "")
            {
                this.FAGroupDropDownList.SelectedValue = this._fixedAssetBL.GetFAGroupByFASubGrpCode(this.FASubGroupDropDownList.SelectedValue);
            }
            this.FAGroupDropDownList.DataBind();
            this.FAGroupDropDownList.Items.Insert(0, new ListItem("[Choose One]", ""));
        }

        public void ShowFASubGroup()
        {
            this.FASubGroupDropDownList.DataTextField = "FASubGrpName";
            this.FASubGroupDropDownList.DataValueField = "FASubGrpCode";
            if (this.FAGroupDropDownList.SelectedValue != "")
            {
                this.FASubGroupDropDownList.DataSource = this._fixedAssetBL.GetListFAGroupSubForDDLByFAGroupCode(this.FAGroupDropDownList.SelectedValue);
            }
            else
            {
                this.FASubGroupDropDownList.DataSource = this._fixedAssetBL.GetListFAGroupSub();
            }
            this.FASubGroupDropDownList.DataBind();
            this.FASubGroupDropDownList.Items.Insert(0, new ListItem("[Choose One]", ""));
        }

        public void ShowFAStatus()
        {
            this.StatusDropDownList.DataTextField = "FAStatusName";
            this.StatusDropDownList.DataValueField = "FAStatusCode";
            this.StatusDropDownList.DataSource = this._fixedAssetBL.GetListFAStatus();
            this.StatusDropDownList.DataBind();
            this.StatusDropDownList.Items.Insert(0, new ListItem("[Choose One]", ""));
        }

        public void ClearData()
        {
            this.LocationTypeDropDownList.SelectedValue = "GENERAL";
            this.FgActiveDropDownList.SelectedValue = "";
            this.FASubGroupDropDownList.SelectedValue = "";
            this.FAGroupDropDownList.SelectedValue = "";
            this.StatusDropDownList.SelectedValue = "";
            this.ClearDropDown();
        }

        public void ClearDropDown()
        {
            this.LocationDropDownList.Items.Clear();
            this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", ""));
        }

        public void ShowLocation()
        {
            this.LocationDropDownList.Items.Clear();
            this.LocationDropDownList.DataTextField = "Name";
            this.LocationDropDownList.DataValueField = "Code";
            this.LocationDropDownList.DataSource = this._fixedAssetBL.GetListDDLFixedAssetLocation(FixedAssetsDataMapper.GetValueFALocation(this.LocationTypeDropDownList.SelectedValue));
            this.LocationDropDownList.DataBind();
            this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", ""));
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer1.Visible = true;

            if (this.TypeRadioButtonList.SelectedValue == "0")
            {
                _reportPath = _reportPath1;
            }
            else if (this.TypeRadioButtonList.SelectedValue == "1")
            {
                _reportPath = _reportPath2;
            }
            else if (this.TypeRadioButtonList.SelectedValue == "2")
            {
                _reportPath = _reportPath0;
            }

            string _locationType = this.LocationTypeDropDownList.SelectedValue;
            string _locationCode = this.LocationDropDownList.SelectedValue;
            string _status = this.StatusDropDownList.SelectedValue;
            string _active = this.FgActiveDropDownList.SelectedValue;
            string _fgSold = this.FgSoldDropDownList.SelectedValue;
            string _type = this.TypeRadioButtonList.SelectedValue;
            string _group = this.FAGroupDropDownList.SelectedValue;
            string _subGroup = this.FASubGroupDropDownList.SelectedValue;


            string _textFgActive = "";
            string _textFgSold = "";
            string _textType = "";

            if (this.FgActiveDropDownList.SelectedValue == "Y") _textFgActive = "Active";
            else if (this.FgActiveDropDownList.SelectedValue == "N") _textFgActive = "Non Active";
            else _textFgActive = "All";

            if (this.FgSoldDropDownList.SelectedValue == "Y") _textFgSold = "Sold";
            else if (this.FgSoldDropDownList.SelectedValue == "N") _textFgSold = "Non Sold";
            else _textFgSold = "All";

            if (this.TypeRadioButtonList.SelectedValue == "0") _textType = "Value (Portrait)";
            else if (this.TypeRadioButtonList.SelectedValue == "1") _textType = "Non Value";
            else if (this.TypeRadioButtonList.SelectedValue == "2") _textType = "Value (Landscape)";

            ReportDataSource _reportDataSource1 = this._reportBL.FixedAssetList(_active, _status, _locationCode, _locationType, Convert.ToInt32(_type), _fgSold, _group, _subGroup);

            this.ReportViewer1.LocalReport.EnableExternalImages = true;
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath;
            this.ReportViewer1.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[18];
            _reportParam[0] = new ReportParameter("FAStatus", _status, true);
            _reportParam[1] = new ReportParameter("FALocationCode", _locationCode, true);
            _reportParam[2] = new ReportParameter("FALocationType", _locationType, true);
            _reportParam[3] = new ReportParameter("FgActive", _active, true);
            _reportParam[4] = new ReportParameter("FgSold", _fgSold, true);
            _reportParam[5] = new ReportParameter("Type", _type, true);
            _reportParam[6] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            _reportParam[7] = new ReportParameter("FAGroup", _group, true);
            _reportParam[8] = new ReportParameter("FASubGroup", _subGroup, true);
            _reportParam[9] = new ReportParameter("Image", ApplicationConfig.HomeWebAppURL + "images/" + new UserBL().CompanyLogo(HttpContext.Current.User.Identity.Name), false);

            _reportParam[10] = new ReportParameter("TextFAGroup", (this.FAGroupDropDownList.SelectedValue == "") ? "" : _fixedAssetBL.GetFAGroupNameByCode(this.FAGroupDropDownList.SelectedValue), true);
            _reportParam[11] = new ReportParameter("TextFASubGroup", (this.FASubGroupDropDownList.SelectedValue == "") ? "" : _fixedAssetBL.GetFAGroupSubNameByCode(this.FASubGroupDropDownList.SelectedValue), true);
            _reportParam[12] = new ReportParameter("TextFAStatus", (this.StatusDropDownList.SelectedValue == "") ? "" : _fixedAssetBL.GetFAStatusSubNameByCode(this.StatusDropDownList.SelectedValue), true);
            _reportParam[13] = new ReportParameter("TextFALocationType", this.LocationTypeDropDownList.Text, true);
            _reportParam[14] = new ReportParameter("TextFALocationCode", (this.LocationDropDownList.SelectedValue == "") ? "" : _fixedAssetBL.GetFALocationNameByCode( this.LocationDropDownList.SelectedValue), true);
            _reportParam[15] = new ReportParameter("TextFgActive", _textFgActive, true);
            _reportParam[16] = new ReportParameter("TextFgSold", _textFgSold, true);
            _reportParam[17] = new ReportParameter("TextType", _textType, true);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        protected void LocationTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowLocation();
        }

        protected void FAGroupDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowFASubGroup();
        }

        protected void FASubGroupDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowFAGroup();
        }
    }
}