using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.ProductDefaultValType
{
    public partial class ProductDefaultValTypeEdit : ProductDefaultValTypeBase
    {
        private ProductBL _productBL = new ProductBL();
        private PermissionBL _permBL = new PermissionBL();

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

                this.ClearLabel();
                this.ShowProductValType();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            Master_DefaultProductValType _productDefaultvalue = this._productBL.GetSingleProductDefaultVal(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.ProductDefaultValDropDownList.SelectedValue = _productDefaultvalue.ProductValType.ToString();
        }

        private void ShowProductValType()
        {
            this.ProductDefaultValDropDownList.Items.Insert(0, new ListItem(ProductDataMapper.ProductValTypeMapper(ProductDataMapper.ProductValTypeMapper(ProductValType.FIFO)), ProductDataMapper.ProductValTypeMapper(ProductValType.FIFO).ToString()));
            this.ProductDefaultValDropDownList.Items.Insert(1, new ListItem(ProductDataMapper.ProductValTypeMapper(ProductDataMapper.ProductValTypeMapper(ProductValType.LIFO)), ProductDataMapper.ProductValTypeMapper(ProductValType.LIFO).ToString()));
            this.ProductDefaultValDropDownList.Items.Insert(2, new ListItem(ProductDataMapper.ProductValTypeMapper(ProductDataMapper.ProductValTypeMapper(ProductValType.Average)), ProductDataMapper.ProductValTypeMapper(ProductValType.Average).ToString()));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            Master_DefaultProductValType _productDefaultvalue = this._productBL.GetSingleProductDefaultVal(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _productDefaultvalue.ProductValType = Convert.ToByte(this.ProductDefaultValDropDownList.SelectedValue);
            _productDefaultvalue.EditBy = HttpContext.Current.User.Identity.Name;
            _productDefaultvalue.EditDate = DateTime.Now;

            bool _result = this._productBL.EditDefaultProductVal(_productDefaultvalue);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}