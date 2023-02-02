using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl
{
    public partial class ProductTypeDetailAdd : ProductTypeBase
    {
        private ProductBL _prodTypeBL = new ProductBL();
        private AccountBL _accBL = new AccountBL();
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

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowAccCOGS();
                this.ShowAccInvent();
                this.ShowAccSales();
                this.ShowAccTransitSJ();
                this.ShowAccTransitWrhs();
                this.ShowAccWIP();
                this.ShowAccSRetur();
                this.ShowAccPRetur();
                this.ShowAccTransitReject();
                this.AccExpLoss();

                this.ClearLabel();
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
            this.AccCOGSDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccCOGSDropDownList.ClientID + "," + this.AccCOGSTextBox.ClientID + ");");
            this.AccCOGSTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccCOGSDropDownList.ClientID + "," + this.AccCOGSTextBox.ClientID + ");");

            this.AccInventDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccInventDropDownList.ClientID + "," + this.AccInventTextBox.ClientID + ");");
            this.AccInventTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccInventDropDownList.ClientID + "," + this.AccInventTextBox.ClientID + ");");

            this.AccSalesDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccSalesDropDownList.ClientID + "," + this.AccSalesTextBox.ClientID + ");");
            this.AccSalesTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccSalesDropDownList.ClientID + "," + this.AccSalesTextBox.ClientID + ");");

            this.AccTransitSJDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccTransitSJDropDownList.ClientID + "," + this.AccTransitSJTextBox.ClientID + ");");
            this.AccTransitSJTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccTransitSJDropDownList.ClientID + "," + this.AccTransitSJTextBox.ClientID + ");");

            this.AccTransitWrhsDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccTransitWrhsDropDownList.ClientID + "," + this.AccTransitWrhsTextBox.ClientID + ");");
            this.AccTransitWrhsTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccTransitWrhsDropDownList.ClientID + "," + this.AccTransitWrhsTextBox.ClientID + ");");

            this.AccWIPDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccWIPDropDownList.ClientID + "," + this.AccWIPTextBox.ClientID + ");");
            this.AccWIPTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccWIPDropDownList.ClientID + "," + this.AccWIPTextBox.ClientID + ");");

            this.AccSReturDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccSReturDropDownList.ClientID + "," + this.AccSReturTextBox.ClientID + ");");
            this.AccSReturTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccSReturDropDownList.ClientID + "," + this.AccSReturTextBox.ClientID + ");");

            this.AccPReturDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccPReturDropDownList.ClientID + "," + this.AccPReturTextBox.ClientID + ");");
            this.AccPReturTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccPReturDropDownList.ClientID + "," + this.AccPReturTextBox.ClientID + ");");

            this.AccTransitRejectDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccTransitRejectDropDownList.ClientID + "," + this.AccTransitRejectTextBox.ClientID + ");");
            this.AccTransitRejectTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccTransitRejectDropDownList.ClientID + "," + this.AccTransitRejectTextBox.ClientID + ");");

            this.AccExpLossDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccExpLossDropDownList.ClientID + "," + this.AccExpLossTextBox.ClientID + ");");
            this.AccExpLossTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccExpLossDropDownList.ClientID + "," + this.AccExpLossTextBox.ClientID + ");");
        }

        private void ClearData()
        {
            this.WrhsTypeDropDownList.SelectedValue = "null";
            this.AccCOGSDropDownList.SelectedValue = "null";
            this.AccCOGSTextBox.Text = "";
            this.AccInventDropDownList.SelectedValue = "null";
            this.AccInventTextBox.Text = "";
            this.AccSalesDropDownList.SelectedValue = "null";
            this.AccSalesTextBox.Text = "";
            this.AccTransitSJDropDownList.SelectedValue = "null";
            this.AccTransitSJTextBox.Text = "";
            this.AccTransitWrhsDropDownList.SelectedValue = "null";
            this.AccTransitWrhsTextBox.Text = "";
            this.AccWIPDropDownList.SelectedValue = "null";
            this.AccWIPTextBox.Text = "";
            this.AccSReturDropDownList.SelectedValue = "null";
            this.AccSReturTextBox.Text = "";
            this.AccPReturDropDownList.SelectedValue = "null";
            this.AccPReturTextBox.Text = "";
            this.AccTransitRejectDropDownList.SelectedValue = "null";
            this.AccTransitRejectTextBox.Text = "";
            this.AccExpLossDropDownList.SelectedValue = "null";
            this.AccExpLossTextBox.Text = "";
            this.FgActiveCheckBox.Checked = false;
            this.RemarkTextBox.Text = "";
        }

        private void ShowAccSales()
        {
            this.AccSalesDropDownList.DataTextField = "AccountName";
            this.AccSalesDropDownList.DataValueField = "Account";
            this.AccSalesDropDownList.DataSource = this._accBL.GetListForDDL();
            this.AccSalesDropDownList.DataBind();
            this.AccSalesDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowAccWIP()
        {
            this.AccWIPDropDownList.DataTextField = "AccountName";
            this.AccWIPDropDownList.DataValueField = "Account";
            this.AccWIPDropDownList.DataSource = this._accBL.GetListForDDL();
            this.AccWIPDropDownList.DataBind();
            this.AccWIPDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowAccCOGS()
        {
            this.AccCOGSDropDownList.DataTextField = "AccountName";
            this.AccCOGSDropDownList.DataValueField = "Account";
            this.AccCOGSDropDownList.DataSource = this._accBL.GetListForDDL();
            this.AccCOGSDropDownList.DataBind();
            this.AccCOGSDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowAccInvent()
        {
            bool _fgStock = _prodTypeBL.GetFgStockByCode(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_fgStock == true)
            {
                this.AccInventRequiredFieldValidator.Enabled = true;
            }
            else
            {
                this.AccInventRequiredFieldValidator.Enabled = false;
            }

            this.AccInventDropDownList.DataTextField = "AccountName";
            this.AccInventDropDownList.DataValueField = "Account";
            this.AccInventDropDownList.DataSource = this._accBL.GetListForDDL();
            this.AccInventDropDownList.DataBind();
            this.AccInventDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowAccTransitSJ()
        {
            this.AccTransitSJDropDownList.DataTextField = "AccountName";
            this.AccTransitSJDropDownList.DataValueField = "Account";
            this.AccTransitSJDropDownList.DataSource = this._accBL.GetListForDDL();
            this.AccTransitSJDropDownList.DataBind();
            this.AccTransitSJDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowAccTransitWrhs()
        {
            this.AccTransitWrhsDropDownList.DataTextField = "AccountName";
            this.AccTransitWrhsDropDownList.DataValueField = "Account";
            this.AccTransitWrhsDropDownList.DataSource = this._accBL.GetListForDDL();
            this.AccTransitWrhsDropDownList.DataBind();
            this.AccTransitWrhsDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowAccSRetur()
        {
            this.AccSReturDropDownList.DataTextField = "AccountName";
            this.AccSReturDropDownList.DataValueField = "Account";
            this.AccSReturDropDownList.DataSource = this._accBL.GetListForDDL();
            this.AccSReturDropDownList.DataBind();
            this.AccSReturDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowAccPRetur()
        {
            this.AccPReturDropDownList.DataTextField = "AccountName";
            this.AccPReturDropDownList.DataValueField = "Account";
            this.AccPReturDropDownList.DataSource = this._accBL.GetListForDDL();
            this.AccPReturDropDownList.DataBind();
            this.AccPReturDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowAccTransitReject()
        {
            this.AccTransitRejectDropDownList.DataTextField = "AccountName";
            this.AccTransitRejectDropDownList.DataValueField = "Account";
            this.AccTransitRejectDropDownList.DataSource = this._accBL.GetListForDDL();
            this.AccTransitRejectDropDownList.DataBind();
            this.AccTransitRejectDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void AccExpLoss()
        {
            this.AccExpLossDropDownList.DataTextField = "AccountName";
            this.AccExpLossDropDownList.DataValueField = "Account";
            this.AccExpLossDropDownList.DataSource = this._accBL.GetListForDDL();
            this.AccExpLossDropDownList.DataBind();
            this.AccExpLossDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsProductTypeDt _msProductTypeDt = new MsProductTypeDt();

            _msProductTypeDt.ProductTypeCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            _msProductTypeDt.WrhsType = Convert.ToByte(this.WrhsTypeDropDownList.SelectedValue);
            _msProductTypeDt.AccCOGS = this.AccCOGSDropDownList.SelectedValue;
            _msProductTypeDt.AccInvent = this.AccInventDropDownList.SelectedValue;
            _msProductTypeDt.AccSales = this.AccSalesDropDownList.SelectedValue;
            _msProductTypeDt.AccTransitSJ = this.AccTransitSJDropDownList.SelectedValue;
            _msProductTypeDt.AccTransitWrhs = this.AccTransitWrhsDropDownList.SelectedValue;
            _msProductTypeDt.AccWIP = this.AccWIPDropDownList.SelectedValue;
            _msProductTypeDt.AccSRetur = this.AccSReturDropDownList.SelectedValue;
            _msProductTypeDt.AccPRetur = this.AccPReturDropDownList.SelectedValue;
            _msProductTypeDt.AccTransitReject = this.AccTransitRejectDropDownList.SelectedValue;
            _msProductTypeDt.AccExpLoss = this.AccExpLossDropDownList.SelectedValue;
            _msProductTypeDt.FgActive = (this.FgActiveCheckBox.Checked == true) ? 'Y' : 'N';
            _msProductTypeDt.Remark = this.RemarkTextBox.Text;
            _msProductTypeDt.CreatedBy = HttpContext.Current.User.Identity.Name;
            _msProductTypeDt.CreatedDate = DateTime.Now;

            bool _result = this._prodTypeBL.AddMsProductTypeDt(_msProductTypeDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }
    }
}