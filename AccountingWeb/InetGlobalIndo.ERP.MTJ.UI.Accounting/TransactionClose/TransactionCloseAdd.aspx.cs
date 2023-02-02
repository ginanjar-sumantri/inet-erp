using System;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.TransactionClose
{
    public partial class TransactionCloseAdd : TransactionCloseBase
    {
        private TransactionCloseBL _transCloseBL = new TransactionCloseBL();
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

                this.ClearData();
                this.SetAttribute();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.YearTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
            this.YearEndTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
            this.PeriodTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
            this.PeriodEndTextBox.Attributes.Add("OnKeyDown", "return Numeric();");

            this.DescTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.DescTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ClearData()
        {
            this.ClearLabel();

            DateTime now = DateTime.Now;
            this.YearTextBox.Text = DateTime.Now.Year.ToString();
            this.YearEndTextBox.Text = DateTime.Now.Year.ToString();
            this.PeriodTextBox.Text = "";
            this.PeriodEndTextBox.Text = "";

            this.DescTextBox.Text = "";
            this.StatusCheckBox.Checked = false;
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            Master_TransactionClose _msTransClose = new Master_TransactionClose();

            DateTime _startDateTime = new DateTime(Convert.ToInt32( this.YearTextBox.Text), Convert.ToInt32( this.PeriodTextBox.Text), 1, 0, 0, 0);
            DateTime _endDateTime = new DateTime(Convert.ToInt32(this.YearEndTextBox.Text), Convert.ToInt32(this.PeriodEndTextBox.Text), DateTime.DaysInMonth(Convert.ToInt32(this.YearEndTextBox.Text), Convert.ToInt32(this.PeriodEndTextBox.Text)), 23, 59, 59);

            _msTransClose.TransCloseCode = Guid.NewGuid();
            _msTransClose.StartDate = _startDateTime;
            _msTransClose.EndDate = _endDateTime;
            _msTransClose.Description = this.DescTextBox.Text;
            _msTransClose.Status = TransactionCloseDataMapper.GetTransCloseStatus(this.StatusCheckBox.Checked);
            _msTransClose.InsertBy = HttpContext.Current.User.Identity.Name;
            _msTransClose.InsertDate = DateTime.Now;
            _msTransClose.EditBy = HttpContext.Current.User.Identity.Name;
            _msTransClose.EditDate = DateTime.Now;

            string _result = this._transCloseBL.Add(_msTransClose);

            if (_result == "")
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = _result;
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ClearData();
        }

    }
}