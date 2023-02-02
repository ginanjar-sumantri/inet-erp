using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Conversion
{
    public partial class ConvertionAdd : ConversionBase
    {
        private UnitBL _convBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowUnit();
                this.ShowUnitConvertion();

                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void SetAttribute()
        {
            this.RateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.ClearLabel();

            this.UnitCodeDropDownList.SelectedValue = "null";
            this.UnitConvertDropDownList.SelectedValue = "null";
            this.RateTextBox.Text = "";
        }

        private void ShowUnit()
        {
            this.UnitCodeDropDownList.DataTextField = "UnitName";
            this.UnitCodeDropDownList.DataValueField = "UnitCode";
            this.UnitCodeDropDownList.DataSource = this._convBL.GetListForDDL();
            this.UnitCodeDropDownList.DataBind();
            this.UnitCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowUnitConvertion()
        {
            this.UnitConvertDropDownList.DataTextField = "UnitName";
            this.UnitConvertDropDownList.DataValueField = "UnitCode";
            this.UnitConvertDropDownList.DataSource = this._convBL.GetListForDDL();
            this.UnitConvertDropDownList.DataBind();
            this.UnitConvertDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsConvertion _msConvertion = new MsConvertion();

            _msConvertion.UnitCode = this.UnitCodeDropDownList.SelectedValue;
            _msConvertion.UnitConvert = this.UnitConvertDropDownList.SelectedValue;
            _msConvertion.Rate = Convert.ToDecimal(this.RateTextBox.Text);
            _msConvertion.UserID = HttpContext.Current.User.Identity.Name;
            _msConvertion.UserDate = DateTime.Now;

            bool _result = this._convBL.AddConvertion(_msConvertion);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}