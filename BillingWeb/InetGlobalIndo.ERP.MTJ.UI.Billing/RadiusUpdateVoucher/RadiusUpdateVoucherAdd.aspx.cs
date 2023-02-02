using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.RadiusUpdateVoucher
{
    public partial class RadiusUpdateVoucherAdd : RadiusUpdateVoucherBase
    {
        private RadiusUpdateVoucherBL _radiusUpdateVoucherBL = new RadiusUpdateVoucherBL();
        private PermissionBL _permBL = new PermissionBL();
        private RadiusBL _radiusBL = new RadiusBL();
        
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
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.btnSearchFrom.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProductSerialNumber1&configCode=product_voucherserialnumber','_popSearch','width=800,height=500,toolbar=0,location=0,status=0,scrollbars=1');return false;";
                this.btnSearchTo.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProductSerialNumber2&configCode=product_voucherserialnumber','_popSearch','width=800,height=500,toolbar=0,location=0,status=0,scrollbars=1');return false;";

                String spawnJS = "<script language='JavaScript'>\n";
                ////////////////////DECLARE FUNCTION FOR CATCHING PRODUCT SERIAL NUMBER SEARCH
                spawnJS += "function findProductSerialNumber1(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.SeriesNoFromTextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "}\n";

                spawnJS += "function findProductSerialNumber2(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.SeriesNoToTextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "}\n";

                spawnJS += "</script>\n";
                this.JSCaller.Text = spawnJS;

                this.SetAttribute();

                this.ShowRadius();
                
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.SeriesNoFromTextBox.Attributes.Add("OnKeyUp", "formatangka(" + this.SeriesNoFromTextBox.ClientID + ");");
            this.SeriesNoToTextBox.Attributes.Add("OnKeyUp", "formatangka(" + this.SeriesNoToTextBox.ClientID + ");");
            this.SeriesTextBox.Attributes.Add("OnKeyUp", "formatangka(" + this.SeriesTextBox.ClientID + ");");
            this.AssociatedServiceTextBox.Attributes.Add("OnKeyUp", "formatangka(" + this.AssociatedServiceTextBox.ClientID + ");");
            this.SellingAmountTextBox.Attributes.Add("OnKeyUp", "formatangka(" + this.SellingAmountTextBox.ClientID + ");");
            this.ExpireTimeTextBox.Attributes.Add("OnKeyUp", "formatangka(" + this.ExpireTimeTextBox.ClientID + ");");            
        }

        protected void ShowRadius()
        {
            this.RadiusDropDownList.Items.Clear();
            this.RadiusDropDownList.DataTextField = "RadiusName";
            this.RadiusDropDownList.DataValueField = "RadiusCode";
            this.RadiusDropDownList.DataSource = this._radiusBL.GetListRadiusForDDL();
            this.RadiusDropDownList.DataBind();
            this.RadiusDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearData()
        {
            this.ClearLabel();
            DateTime _now = DateTime.Now;

            this.DateTextBox.Text = DateFormMapper.GetValue(_now);
            this.RadiusDropDownList.SelectedValue = "null";
            this.ExpiredDateTextBox.Text = DateFormMapper.GetValue(_now);
            this.SeriesTextBox.Text = "";
            this.SeriesNoFromTextBox.Text = "";
            this.AssociatedServiceTextBox.Text = "";
            this.SeriesNoToTextBox.Text = "";
            this.ExpireTimeTextBox.Text = "";
            this.SellingAmountTextBox.Text = "";
            this.ExpireTimeUnitDropDownList.SelectedValue = "0";
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            BILTrRadiusUpdateVoucher _bilTrRadiusUpdateVoucher = new BILTrRadiusUpdateVoucher();

            _bilTrRadiusUpdateVoucher.Status = RadiusUpdateVoucherDataMapper.GetStatus(TransStatus.OnHold);
            _bilTrRadiusUpdateVoucher.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _bilTrRadiusUpdateVoucher.RadiusCode = this.RadiusDropDownList.SelectedValue;
            _bilTrRadiusUpdateVoucher.ExpiredDate = DateFormMapper.GetValue(this.ExpiredDateTextBox.Text);
            _bilTrRadiusUpdateVoucher.Series = this.SeriesTextBox.Text;
            _bilTrRadiusUpdateVoucher.SerialNoFrom = this.SeriesNoFromTextBox.Text;
            _bilTrRadiusUpdateVoucher.SerialNoTo = this.SeriesNoToTextBox.Text;
            _bilTrRadiusUpdateVoucher.SellingAmount = Convert.ToDecimal(this.SellingAmountTextBox.Text);
            _bilTrRadiusUpdateVoucher.AssociatedService = this.AssociatedServiceTextBox.Text;
            _bilTrRadiusUpdateVoucher.ExpireTime = Convert.ToInt32(this.ExpireTimeTextBox.Text);
            _bilTrRadiusUpdateVoucher.ExpireTimeUnit = Convert.ToInt32(this.ExpireTimeUnitDropDownList.SelectedValue);

            String _result = this._radiusUpdateVoucherBL.AddRadiusUpdateVoucher(_bilTrRadiusUpdateVoucher);

            if (_result != "")
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
            }
            else
            {
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