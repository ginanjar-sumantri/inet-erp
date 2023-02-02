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
using InetGlobalIndo.ERP.MTJ.Common;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.RadiusUpdateVoucher
{
    public partial class RadiusUpdateVoucherEdit : RadiusUpdateVoucherBase
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

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.btnSearchFrom.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProductSerialNumber1&configCode=product_voucherserialnumber','_popSearch','width=500,height=300,toolbar=0,location=0,status=0,scrollbars=1');return false;";
                this.btnSearchTo.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProductSerialNumber2&configCode=product_voucherserialnumber','_popSearch','width=500,height=300,toolbar=0,location=0,status=0,scrollbars=1');return false;";

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
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.TransNoTextBox.Attributes.Add("ReadOnly","True");
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

        protected void ShowData()
        {
            this.ClearLabel();

            BILTrRadiusUpdateVoucher _bilTrRadiusUpdateVoucher = this._radiusUpdateVoucherBL.GetSingleRadiusUpdateVoucher(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNoTextBox.Text = _bilTrRadiusUpdateVoucher.TransNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_bilTrRadiusUpdateVoucher.TransDate);
            this.RadiusDropDownList.SelectedValue = _bilTrRadiusUpdateVoucher.RadiusCode;
            this.ExpiredDateTextBox.Text = DateFormMapper.GetValue(_bilTrRadiusUpdateVoucher.ExpiredDate);
            this.SeriesTextBox.Text = _bilTrRadiusUpdateVoucher.Series;
            this.SeriesNoFromTextBox.Text = _bilTrRadiusUpdateVoucher.SerialNoFrom;
            this.SeriesNoToTextBox.Text = _bilTrRadiusUpdateVoucher.SerialNoTo;
            this.AssociatedServiceTextBox.Text = _bilTrRadiusUpdateVoucher.AssociatedService;
            int _expireTime = (_bilTrRadiusUpdateVoucher.ExpireTime == null) ? 0 : Convert.ToInt32(_bilTrRadiusUpdateVoucher.ExpireTime);
            this.ExpireTimeTextBox.Text = _expireTime.ToString("#,##0");
            decimal _sellamount = (_bilTrRadiusUpdateVoucher.SellingAmount == null) ? 0 : Convert.ToDecimal(_bilTrRadiusUpdateVoucher.SellingAmount);
            this.SellingAmountTextBox.Text = _sellamount.ToString("#,##0");
            this.ExpireTimeUnitDropDownList.SelectedValue = _bilTrRadiusUpdateVoucher.ExpireTimeUnit.ToString();

            this.StatusLabel.Text = RadiusUpdateVoucherDataMapper.GetStatusText(_bilTrRadiusUpdateVoucher.Status);
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            BILTrRadiusUpdateVoucher _bilTrRadiusUpdateVoucher = this._radiusUpdateVoucherBL.GetSingleRadiusUpdateVoucher(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

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

            Boolean _result = this._radiusUpdateVoucherBL.EditRadiusUpdateVoucher(_bilTrRadiusUpdateVoucher);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.Page.IsValid == true)
            {
                BILTrRadiusUpdateVoucher _bilTrRadiusUpdateVoucher = this._radiusUpdateVoucherBL.GetSingleRadiusUpdateVoucher(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

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

                Boolean _result = this._radiusUpdateVoucherBL.EditRadiusUpdateVoucher(_bilTrRadiusUpdateVoucher);

                if (_result == true)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "You Failed Edit Data";
                }
            }
        }
    }
}