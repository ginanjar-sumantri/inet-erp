using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Home.Term
{
    public partial class TermAdd : TermBase
    {
        private TermBL _termBL = new TermBL();
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

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.PeriodTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.RangeTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
        }

        public void ClearData()
        {
            this.ClearLabel();

            this.TermCodeTextBox.Text = "";
            this.TermNameTextBox.Text = "";
            this.PeriodTextBox.Text = "";
            this.RangeTextBox.Text = "";
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            MsTerm _msTerm = new MsTerm();



            _msTerm.TermCode = this.TermCodeTextBox.Text;
            _msTerm.TermName = this.TermNameTextBox.Text;
            _msTerm.XPeriod = Convert.ToInt32(this.PeriodTextBox.Text);
            _msTerm.TypeRange = this.TypeRangeDropDownList.SelectedValue;
            _msTerm.XRange = Convert.ToInt32(this.RangeTextBox.Text);
            _msTerm.IsValid = TermDataMapper.IsValid(false);
            _msTerm.UserID = HttpContext.Current.User.Identity.Name;
            _msTerm.UserDate = DateTime.Now;

            decimal _percentBase = 0;
            decimal _percentPPn = 0;

            List<MsTermDt> _listTermDt = new List<MsTermDt>();

            for (int i = 1; i <= Convert.ToInt32(this.PeriodTextBox.Text); i++)
            {
                MsTermDt _msTermDt = new MsTermDt();

                _msTermDt.TermCode = this.TermCodeTextBox.Text;
                _msTermDt.Period = i;
                _msTermDt.TypeRange = this.TypeRangeDropDownList.SelectedValue;

                if (i == Convert.ToInt32(this.PeriodTextBox.Text))
                {
                    _msTermDt.PercentBase = 100 - _percentBase;
                    _msTermDt.PercentPPn = 100 - _percentPPn;
                }
                else if (i != Convert.ToInt32(this.PeriodTextBox.Text))
                {
                    _msTermDt.PercentBase = Convert.ToDecimal(100 / Convert.ToInt32(this.PeriodTextBox.Text));
                    _msTermDt.PercentPPn = Convert.ToDecimal(100 / Convert.ToInt32(this.PeriodTextBox.Text));
                }
                if (i >= Convert.ToInt32(this.RangeTextBox.Text))
                {
                    _msTermDt.XRange = Convert.ToInt32(this.RangeTextBox.Text);
                }
                else if (i < Convert.ToInt32(this.RangeTextBox.Text))
                {
                    _msTermDt.XRange = i;
                }

                _percentBase = _percentBase + _msTermDt.PercentBase;
                _percentPPn = _percentPPn + _msTermDt.PercentPPn;

                _listTermDt.Add(_msTermDt);
            }

            bool _result = this._termBL.Add(_msTerm);

            this._termBL.AddMulti(_listTermDt);

            _msTerm.IsValid = TermDataMapper.IsValid(true);

            this._termBL.Edit(_msTerm);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TermCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Add Data";
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