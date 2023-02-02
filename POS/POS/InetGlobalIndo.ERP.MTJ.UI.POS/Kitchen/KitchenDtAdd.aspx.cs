using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using System.Web.UI.WebControls;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Kitchen
{
    public partial class KitchenDtAdd : KitchenBase
    {
        private KitchenBL _kitchenBL = new KitchenBL();
        private PermissionBL _permBL = new PermissionBL();
        private ProductBL _productBL = new ProductBL();

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
                this.PageTitleLiteral.Text = this._pageTitleDetailLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ShowProductType();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.ClearLabel();

            String _code = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string[] _split = _code.Split('|');

            this.KitchenCodeTextBox.Text = _split[0];
            this.ProductTypeDDL.SelectedIndex = -1;
            this.FgActiveCheckBox.Checked = false;
            this.RemarkTextBox.Text = "";
        }

        protected void ShowProductType()
        {
            this.ProductTypeDDL.Items.Clear();
            this.ProductTypeDDL.DataTextField = "ProductTypeName";
            this.ProductTypeDDL.DataValueField = "ProductTypeCode";
            this.ProductTypeDDL.DataSource = this._productBL.GetListProductType(0, 1000, "", "");
            this.ProductTypeDDL.DataBind();
            this.ProductTypeDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void CheckValidData()
        {
            if (this.ProductTypeDDL.SelectedIndex == -1)
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Choose One Of Product Type. ";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                POSMsKitchenDt _posMsKitchenDt = new POSMsKitchenDt();

                _posMsKitchenDt.KitchenCode = this.KitchenCodeTextBox.Text;
                _posMsKitchenDt.ProductTypeCode = this.ProductTypeDDL.SelectedValue;
                _posMsKitchenDt.FgActive = (this.FgActiveCheckBox.Checked == true) ? 'Y' : 'N';
                _posMsKitchenDt.Remark = this.RemarkTextBox.Text;
                _posMsKitchenDt.CreatedBy = HttpContext.Current.User.Identity.Name;
                _posMsKitchenDt.CreatedDate = DateTime.Now;

                Boolean _result = this._kitchenBL.AddDt(_posMsKitchenDt);

                if (_result == true)
                {
                    Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "You Failed Add Data" + _result;
                }
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}