using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Product
{
    public partial class ProductChangePhoto : ProductBase
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

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                Response.AddCacheItemDependency("Pages");

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.UploadButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/upload.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";

                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
            this.UploadLabel.Text = "Photo dimension : Width " + ApplicationConfig.ImageHeight + " pixels x Height " + ApplicationConfig.ImageHeight + " pixels, File size limit: " + Convert.ToInt32(ApplicationConfig.ImageMaxSize) / 1024 + " KBytes";
        }

        protected void ShowData()
        {
            MsProduct _msProduct = this._productBL.GetSingleProduct(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            string _strImagePath = ApplicationConfig.ProductPhotoVirDirPath + _msProduct.Photo;
            this.ProductCodeTextBox.Text = _msProduct.ProductCode;
            this.ProductNameTextBox.Text = _msProduct.ProductName;
            //this.EmployeePhoto.ImageUrl = _strImagePath;
            this.ProductPhoto.Attributes.Add("src", "" + _strImagePath + "?t=" + System.DateTime.Now.ToString());
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            this.ClearLabel();

            String _result = _productBL.ChangeProductPicture(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), this.PhotoFileUpload);

            this.WarningLabel.Text = _result;

            this.ShowData();
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }
    }
}