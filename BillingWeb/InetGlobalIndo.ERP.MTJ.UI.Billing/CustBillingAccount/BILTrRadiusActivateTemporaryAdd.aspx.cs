using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using AjaxControlToolkit;
using Microsoft.Office.Interop.Word;
using System.IO;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.CustBillingAccountBase
{
    public partial class BILTrRadiusActivateTemporaryAdd : CustBillingAccountBase
    {
        private CustBillAccountBL _custBillAccountBL = new CustBillAccountBL();
        private CustomerBL _custBL = new CustomerBL();
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
                this.PageTitleLiteral.Text = this._pageTitleRadiusTemporaryLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.UploadLabel.Text = "Images dimension : Width " + "1200" + " pixels x Height " + "1600" + " pixels, File size limit: " + ((2097152 / 1024) / 1024) + " MB";


                this.ShowCustDropdownlist();
                this.SetAttribute();
                this.ClearLabel();
            }

        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.PeriodTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.PeriodTextBox.Attributes.Add("OnBlur", " ValidatePeriod(" + this.PeriodTextBox.ClientID + ");");

            this.YearTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.YearTextBox.Attributes.Add("OnBlur", " ValidateYear(" + this.YearTextBox.ClientID + ");");

            this.PeriodTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.YearTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
        }

        protected void ShowCustDropdownlist()
        {
            this.CustNameDropDownList.Items.Clear();
            this.CustNameDropDownList.DataSource = this._custBillAccountBL.GetListDDLCustBillAccount();
            this.CustNameDropDownList.DataValueField = "CustBillCode";
            this.CustNameDropDownList.DataTextField = "CustName";
            this.CustNameDropDownList.DataBind();
            this.CustNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", ""));
        }

        protected void ClearData()
        {
            this.CustNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.CustNameDropDownList.SelectedValue = "null";

            this.PeriodTextBox.Text = "";
            this.YearTextBox.Text = "";
            this.ReasonTextBox.Text = "";
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            BILTrRadiusActivateTemporary _bilTrRadiusActivateTemporary = new BILTrRadiusActivateTemporary();

            string _custNameDDL = this.CustNameDropDownList.SelectedItem.ToString();
            string[] _custName = _custNameDDL.Split('-');

            _bilTrRadiusActivateTemporary.CustBillCode = new Guid(this.CustNameDropDownList.SelectedValue);
            _bilTrRadiusActivateTemporary.CustCode = _custName[1];
            _bilTrRadiusActivateTemporary.Period = (this.PeriodTextBox.Text == "") ? 0 : Convert.ToByte(this.PeriodTextBox.Text);
            _bilTrRadiusActivateTemporary.Year = (this.YearTextBox.Text == "") ? 0 : Convert.ToInt32(this.YearTextBox.Text);
            _bilTrRadiusActivateTemporary.Transdate = DateTime.Now;
            _bilTrRadiusActivateTemporary.Reason = this.ReasonTextBox.Text;
            _bilTrRadiusActivateTemporary.Status = BilTrRadiusActivateTempDataMapper.GetStatus(TransStatus.OnHold);

            _bilTrRadiusActivateTemporary.InsertBy = HttpContext.Current.User.Identity.Name;
            _bilTrRadiusActivateTemporary.InsertDate = DateTime.Now;
            _bilTrRadiusActivateTemporary.EditBy = HttpContext.Current.User.Identity.Name;
            _bilTrRadiusActivateTemporary.EditDate = DateTime.Now;

            String _imagepath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + @"\imagesUpload\";

            string _result = this._custBillAccountBL.AddBILTrRadiusActiveTemp(_bilTrRadiusActivateTemporary, this.FotoUpLoad, _imagepath);

            if (_result != "")
            {
                Response.Redirect(this._viewPageTrRadiusTemporary + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePageTrRadiusTemporary);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }

    }

}
