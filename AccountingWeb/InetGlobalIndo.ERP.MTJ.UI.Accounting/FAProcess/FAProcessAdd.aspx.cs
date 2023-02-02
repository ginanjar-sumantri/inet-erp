using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAProcess
{
    public partial class FAProcessAdd : FAProcessBase
    {
        private FixedAssetsBL _faProcessBL = new FixedAssetsBL();
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
                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.YearDropdownlist();
                this.PeriodDropdownlist();

                this.SetAttribute();
                this.ClearLabel();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearData()
        {
            this.YearDDL.SelectedValue = DateTime.Now.Year.ToString();
            this.PeriodDDL.SelectedValue = DateTime.Now.Month.ToString();
            this.RemarkTextBox.Text = "";
        }

        protected void YearDropdownlist()
        {
            int _year = DateTime.Now.Year;
            for (int i = 9; i >= 0; i--)
            {
                this.YearDDL.Items.Add((_year - i).ToString());
            }
        }

        protected void PeriodDropdownlist()
        {
            this.PeriodDDL.DataSource = this._faProcessBL.GetPeriod();
            this.PeriodDDL.DataValueField = "MonthCode";
            this.PeriodDDL.DataTextField = "MonthName";
            this.PeriodDDL.DataBind();
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            if (_faProcessBL.IsDataProcessExist(Convert.ToInt32(this.YearDDL.SelectedValue), Convert.ToInt32(this.PeriodDDL.SelectedValue)) == true)
            {
                if (_faProcessBL.PostedPrevPeriodExist(Convert.ToInt32(this.YearDDL.SelectedValue), Convert.ToInt32(this.PeriodDDL.SelectedValue)) == true)
                {
                    if (_faProcessBL.PostedNextPeriodExist(Convert.ToInt32(this.YearDDL.SelectedValue), Convert.ToInt32(this.PeriodDDL.SelectedValue)) == false)
                    {
                        GLFAProcessHd _glFAProcessHd = new GLFAProcessHd();

                        _glFAProcessHd.Year = Convert.ToInt32(this.YearDDL.SelectedValue);
                        _glFAProcessHd.Period = Convert.ToInt32(this.PeriodDDL.SelectedValue);
                        _glFAProcessHd.Status = Convert.ToChar(FAProcessDataMapper.GetStatus(TransStatus.OnHold));
                        _glFAProcessHd.Remark = this.RemarkTextBox.Text;

                        _glFAProcessHd.UserPrep = HttpContext.Current.User.Identity.Name;
                        _glFAProcessHd.DatePrep = DateTime.Now;

                        bool _result = this._faProcessBL.AddFAProcessHd(_glFAProcessHd);

                        if (_result == true)
                        {
                            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_glFAProcessHd.Year.ToString(), ApplicationConfig.EncryptionKey)) + "&" + this._codePeriod + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_glFAProcessHd.Period.ToString(), ApplicationConfig.EncryptionKey)));
                        }
                        else
                        {
                            this.WarningLabel.Text = "You Failed Add Data";
                        }
                    }
                    else
                    {
                        this.WarningLabel.Text = "You Failed Add Data, Next Posted Period already exist";
                    }
                }
                else
                {
                    this.WarningLabel.Text = "You Failed Add Data, Previous period does not posted yet";
                }
            }
            else
            {
                GLFAProcessHd _glFAProcessHd = new GLFAProcessHd();

                _glFAProcessHd.Year = Convert.ToInt32(this.YearDDL.SelectedValue);
                _glFAProcessHd.Period = Convert.ToInt32(this.PeriodDDL.SelectedValue);
                _glFAProcessHd.Status = Convert.ToChar(FAProcessDataMapper.GetStatus(TransStatus.OnHold));
                _glFAProcessHd.Remark = this.RemarkTextBox.Text;

                _glFAProcessHd.UserPrep = HttpContext.Current.User.Identity.Name;
                _glFAProcessHd.DatePrep = DateTime.Now;

                bool _result = this._faProcessBL.AddFAProcessHd(_glFAProcessHd);

                if (_result == true)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_glFAProcessHd.Year.ToString(), ApplicationConfig.EncryptionKey)) + "&" + this._codePeriod + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_glFAProcessHd.Period.ToString(), ApplicationConfig.EncryptionKey)));
                }
                else
                {
                    this.WarningLabel.Text = "You Failed Add Data";
                }
            }
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }
    }
}