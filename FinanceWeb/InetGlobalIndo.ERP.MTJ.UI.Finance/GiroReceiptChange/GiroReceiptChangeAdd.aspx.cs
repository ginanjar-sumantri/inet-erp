using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.GiroReceiptChange
{
    public partial class GiroReceiptChangeAdd : GiroReceiptChangeBase
    {
        private FINChangeGiroInBL _finChangeGiroInBL = new FINChangeGiroInBL();
        private SupplierBL _suppBL = new SupplierBL();
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
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.ClearLabel();

            this.DateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.CodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.CodeDropDownList.SelectedValue = "null";
            this.TypeDropDownList.SelectedValue = "null";
            this.RemarkTextBox.Text = "";
        }

        private void ShowCust()
        {
            this.CodeDropDownList.Items.Clear();
            this.CodeDropDownList.DataTextField = "CustName";
            this.CodeDropDownList.DataValueField = "CustCode";
            this.CodeDropDownList.DataSource = this._custBL.GetListCustomerForDDL();
            this.CodeDropDownList.DataBind();
            this.CodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowSupp()
        {
            this.CodeDropDownList.Items.Clear();
            this.CodeDropDownList.DataTextField = "SuppName";
            this.CodeDropDownList.DataValueField = "SuppCode";
            this.CodeDropDownList.DataSource = this._suppBL.GetListDDLSupp();
            this.CodeDropDownList.DataBind();
            this.CodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            FINChangeGiroInHd _finChangeGiroInHd = new FINChangeGiroInHd();

            _finChangeGiroInHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finChangeGiroInHd.Status = GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.OnHold);

            if (this.TypeDropDownList.SelectedValue == "C")
            {
                _finChangeGiroInHd.CustCode = this.CodeDropDownList.SelectedValue;
            }
            else if (this.TypeDropDownList.SelectedValue == "S")
            {
                _finChangeGiroInHd.SuppCode = this.CodeDropDownList.SelectedValue;
            }

            _finChangeGiroInHd.Remark = this.RemarkTextBox.Text;
            _finChangeGiroInHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finChangeGiroInHd.DatePrep = DateTime.Now;

            string _result = this._finChangeGiroInBL.AddFINChangeGiroInHd(_finChangeGiroInHd);

            if (_result != "")
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
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

        protected void TypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.TypeDropDownList.SelectedValue == "C")
            {
                this.ShowCust();
            }
            else if (this.TypeDropDownList.SelectedValue == "S")
            {
                this.ShowSupp();
            }
        }
    }
}