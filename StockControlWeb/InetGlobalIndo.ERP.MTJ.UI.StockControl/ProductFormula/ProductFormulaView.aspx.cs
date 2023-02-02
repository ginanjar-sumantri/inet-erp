using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Data;
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.ProductFormula
{
    public partial class ProductFormulaView : ProductFormulaBase
    {
        private ProductBL _productBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();

        private DataTable dt2 = new DataTable();

        private int _no = 0;

        private DataTable dt = null;

        private List<STCMsProductFormula> _listSTCMsProductFormula = null;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!IsPostBack)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";
                this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";

                this.ClearLabel();

                this.ShowData();
            }
        }

        public void ClearLabel()
        {
            this.Label1.Text = "";
            this.WarningLabel0.Text = "";
        }

        public void ShowData()
        {
            String _product = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            if (_product != "")
            {
                this.ProductCodeTextBox.Text = _product;
                this.ProductNameTextBox.Text = _productBL.GetProductNameByCode(_product);

                this.ListRepeater.DataSource = _productBL.GetListProductFormulaDt(_product);
                this.ListRepeater.DataBind();
            }
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ProductCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
        }

        protected void ListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (e.CommandName == "UpdateDatabase")
            {
                Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductLiteral");
                
                String _productCode = _productCodeLiteral.Text.Split('-')[0].Trim();

                Boolean _result = this._productBL.DeleteMultiProductFormulaDt(_productCode, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                if (_result == true)
                {
                    Response.Redirect(_detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ProductCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
                }
                else
                {
                    this.Label1.Text = "Delete data failed";
                }
            }
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                STCMsProductFormula _temp = (STCMsProductFormula)e.Item.DataItem;

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no += 1;
                _noLiteral.Text = _no.ToString();

                Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductLiteral");
                Literal _qty = (Literal)e.Item.FindControl("QtyLiteral");
                Literal _unit = (Literal)e.Item.FindControl("UnitLiteral");
                CheckBox _mainProduct = (CheckBox)e.Item.FindControl("MainProductCheckBox");

                _productCodeLiteral.Text = _temp.ProductCode + " - " + _temp.ProductName;
                _qty.Text = _temp.Qty.ToString("#,##0.##");
                _unit.Text = _temp.UnitName;
                _mainProduct.Checked = Convert.ToBoolean(_temp.fgMainProduct);                
            }
        }
    }
}