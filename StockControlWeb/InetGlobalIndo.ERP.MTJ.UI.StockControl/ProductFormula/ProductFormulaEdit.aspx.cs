using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.ProductFormula
{
    public partial class ProductFormulaEdit : ProductFormulaBase
    {
        private ProductBL _productBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();

        private DataTable dt2 = new DataTable();

        private int _no = 0;
        private int _nomor = 0;


        private DataTable dt = null;

        private List<STCMsProductFormula> _listSTCMsProductFormula = null;

        protected void Page_Load(object sender, EventArgs e)
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

            if (!this.Page.IsPostBack == true)
            {
                this.StyleSheetLiteral.Text = "<link type=\"text/css\" href=\"" + ApplicationConfig.HomeWebAppURL + "StyleSheet.css\" rel=\"Stylesheet\" />";

                String spawnJS = "<script language='JavaScript'>\n";
                //////////////////DECLARE FUNCTION FOR CATCHING PRODUCT SEARCH
                spawnJS += "function findProduct(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.ProductCodeTextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.getElementById('" + this.ProductNameHiddenField.ClientID + "').value = dataArray [1];\n";
                spawnJS += "document.getElementById('" + this.UnitHiddenField.ClientID + "').value = dataArray [3];\n";
                spawnJS += "document.getElementById('" + this.ProductNameTextBox.ClientID + "').value = dataArray [1];\n";
                spawnJS += "document.getElementById('" + this.UnitTextBox.ClientID + "').value = dataArray [3];\n";
                spawnJS += "document.getElementById('" + this.QtyTextBox.ClientID + "').focus();\n";
                spawnJS += "}\n";

                /////////////////FUNCTION FOR KEYPRESS ENTER ON PRODUCTCODE
                spawnJS += "function enterProductCode() {\n";
                spawnJS += "document.getElementById('" + this.QtyTextBox.ClientID + "').focus();\n";
                spawnJS += "return false;\n";
                spawnJS += "}\n";

                /////////////////FUNCTION FOR KEYPRESS ENTER ON QTY
                spawnJS += "function enterQty() {\n";
                spawnJS += "document.getElementById('" + this.AddLineButton.ClientID + "').focus();\n";
                spawnJS += "return false;\n";
                spawnJS += "}\n";

                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

                this.UnitTextBox.Attributes.Add("ReadOnly", "True");
                this.QtyTextBox.Attributes.Add("onkeyup", "numericInput(this);");
                this.QtyTextBox.Attributes.Add("onchange", "numericInput(this);");
                this.ProductCodeTextBox.Attributes.Add("onkeydown", "if(event.keyCode==13 && this.value != ''){return enterProductCode();}");
                this.QtyTextBox.Attributes.Add("onkeydown", "if(event.keyCode==13){return enterQty();}");

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";
                //this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";

                this.ClearLabel();
                this.ShowData();                
            }

            this.SearchProductCodeButton.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProduct&configCode=productAddFurmulaDt','_popSearch','width=550,height=800px,toolbar=0,location=0,status=0,scrollbars=1')";

            this.ProductCodeTextBox.Focus();            
        }

        protected void ClearLabel()
        {
            this.WarningLabel0.Text = "";
        }

        protected void SetAttribute()
        {
            this.ProductCodeHdTextBox.Attributes.Add("ReadOnly", "True");
            this.ProductNameHdTextBox.Attributes.Add("ReadOnly", "True");
        }


        private void ShowData()
        {
            String _product = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            if (_product != "")
            {
                this.ProductCodeHdTextBox.Text = _product;
                this.ProductNameHdTextBox.Text = _productBL.GetProductNameByCode(_product);

                this.ListRepeater.DataSource = _productBL.GetListProductFormulaDtByProductAssembly(_product);
                this.ListRepeater.DataBind();
            }
        }        

        protected void ProductCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            String[] _productData = _productBL.GetProductData(this.ProductCodeTextBox.Text).Split('|');

            if (_productData.Length > 0 && _productData[0] != "")
            {
                this.ProductNameTextBox.Text = _productData[0];
                this.ProductNameHiddenField.Value = _productData[0];
                this.UnitTextBox.Text = _productData[1];
                this.UnitHiddenField.Value = _productData[1];
                this.QtyTextBox.Focus();
            }
        }

        protected void AddLineButton_Click(object sender, EventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');
            int i = 0;
            foreach (var _item in _tempSplit)
            {
                i++;
            }

            if (i <= 1)
            {
                Boolean _check = false;
                List<STCMsProductFormula> _listSTCMsProductFormula = this._productBL.GetListProductFormulaDtByProductAssembly(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                if (_listSTCMsProductFormula.Count > 0)
                {
                    foreach (var _item in _listSTCMsProductFormula)
                    {
                        if (_item.fgMainProduct == true && this.FgMainProductCheckBox.Checked == true)
                        {
                            _check = false;
                            break;
                        }
                        else
                        {
                            _check = true;
                        }
                    }
                }
                else
                {
                    _check = true;
                }


                if (_check == true)
                {
                    if (this.FgMainProductCheckBox.Checked == true)
                    {
                        if (this.QtyTextBox.Text == "1")
                        {
                            DateTime _now = DateTime.Now;
                            STCMsProductFormula _stcMsProductFormula = new STCMsProductFormula();

                            _stcMsProductFormula.ProductCodeAssembly = this.ProductCodeHdTextBox.Text;
                            //String[] _product = this.ProductNameTextBox.Text.Split('-');
                            _stcMsProductFormula.ProductCode = this.ProductCodeTextBox.Text;
                            _stcMsProductFormula.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
                            _stcMsProductFormula.Unit = this.UnitTextBox.Text;
                            _stcMsProductFormula.fgMainProduct = this.FgMainProductCheckBox.Checked;
                            _stcMsProductFormula.CreatedBy = HttpContext.Current.User.Identity.Name;
                            _stcMsProductFormula.CreatedDate = _now;

                            bool result = this._productBL.AddProductFormulaDetail(_stcMsProductFormula);

                            this.ProductCodeTextBox.Text = "";
                            this.ProductCodeTextBox.Focus();
                            this.ProductNameHiddenField.Value = "";
                            this.ProductNameTextBox.Text = "";
                            this.QtyTextBox.Text = "";
                            this.UnitHiddenField.Value = "";
                            this.UnitTextBox.Text = "";

                            this.ClearLabel();
                            this.ShowData();
                        }
                        else
                        {
                            this.WarningLabel0.Text = "Main Product is Checked, The Product must be Qty = 1";
                        }
                    }
                    else
                    {
                        DateTime _now = DateTime.Now;
                        STCMsProductFormula _stcMsProductFormula = new STCMsProductFormula();

                        _stcMsProductFormula.ProductCodeAssembly = this.ProductCodeHdTextBox.Text;
                        //String[] _product = this.ProductNameTextBox.Text.Split('-');
                        _stcMsProductFormula.ProductCode = this.ProductCodeTextBox.Text;
                        _stcMsProductFormula.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
                        _stcMsProductFormula.Unit = this.UnitTextBox.Text;
                        _stcMsProductFormula.fgMainProduct = this.FgMainProductCheckBox.Checked;
                        _stcMsProductFormula.CreatedBy = HttpContext.Current.User.Identity.Name;
                        _stcMsProductFormula.CreatedDate = _now;

                        bool result = this._productBL.AddProductFormulaDetail(_stcMsProductFormula);

                        this.ProductCodeTextBox.Text = "";
                        this.ProductCodeTextBox.Focus();
                        this.ProductNameHiddenField.Value = "";
                        this.ProductNameTextBox.Text = "";
                        this.QtyTextBox.Text = "";
                        this.UnitHiddenField.Value = "";
                        this.UnitTextBox.Text = "";

                        this.ClearLabel();
                        this.ShowData();
                    }
                }
                else
                {
                    this.WarningLabel0.Text = "Main product has been checked by other products.";
                }
            }
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Boolean _check = false;
            List<STCMsProductFormula> _listSTCMsProductFormula = this._productBL.GetListProductFormulaDtByProductAssembly(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            if (_listSTCMsProductFormula.Count > 0)
            {
                foreach (var _item in _listSTCMsProductFormula)
                {
                    if (_item.fgMainProduct == true)
                    {
                        _check = true;
                        break;
                    }
                    else
                    {
                        _check = false;
                    }
                }
            }
            else
            {
                _check = true;
            }

            if(_check == true)
            {
                Response.Redirect(_homePage);
            }
            else
            {
                this.WarningLabel0.Text = "Main product from one of the products must be checked.";
            }
            
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                STCMsProductFormula _temp = (STCMsProductFormula)e.Item.DataItem;
                string _code = _temp.ProductCodeAssembly.ToString() + "-" + _temp.ProductCode.ToString();

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _code;
                }
                else
                {
                    this.TempHidden.Value += "," + _code;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                //_nomor += 1;

                HiddenField _hiddenField = (HiddenField)e.Item.FindControl("CodeHiddenField");
                _hiddenField.Value = _code;                
                
                ImageButton _CheckedButton = (ImageButton)e.Item.FindControl("CheckedImageButton");
                //_editButton.PostBackUrl = this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                _CheckedButton.CommandName = "UnCheck";
                _CheckedButton.CommandArgument = _code;
                _CheckedButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/checked.png";

                ImageButton _unCheckedButton = (ImageButton)e.Item.FindControl("UnCheckImageButton");
                //_editButton.PostBackUrl = this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                _unCheckedButton.CommandName = "Checked";
                _unCheckedButton.CommandArgument = _temp.ProductCodeAssembly.ToString() + "-" + _temp.ProductCode.ToString() + "-" + _temp.fgMainProduct;
                _unCheckedButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/uncheck.png";

                if (_temp.fgMainProduct == true)
                {
                    _CheckedButton.Visible = true;
                    _unCheckedButton.Visible = false;
                }
                else
                {
                    _CheckedButton.Visible = false;
                    _unCheckedButton.Visible = true;
                }

                ImageButton _deleteButton = (ImageButton)e.Item.FindControl("DeleteImageButton");
                //_editButton.PostBackUrl = this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                _deleteButton.CommandName = "Delete";
                _deleteButton.CommandArgument = _code;
                _deleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";


                Literal _productBLCode = (Literal)e.Item.FindControl("ProductLiteral");
                _productBLCode.Text = HttpUtility.HtmlEncode(_temp.ProductCode + "-" + _productBL.GetProductNameByCode(_temp.ProductCode));

                Literal _productBLName = (Literal)e.Item.FindControl("QtyLiteral");
                _productBLName.Text = HttpUtility.HtmlEncode(_temp.Qty.ToString("#,#0"));

                Literal _productBLSubGroup = (Literal)e.Item.FindControl("UnitLiteral");
                _productBLSubGroup.Text = HttpUtility.HtmlEncode(_temp.Unit);
            }
        }

        protected void ListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Checked")
            {
                String[] _ex = e.CommandArgument.ToString().Split('-');

                Boolean _check = false;
                List<STCMsProductFormula> _listSTCMsProductFormula = this._productBL.GetListProductFormulaDtByProductAssembly(this.ProductCodeHdTextBox.Text);
                if (_listSTCMsProductFormula.Count > 0)
                {
                    foreach (var _item in _listSTCMsProductFormula)
                    {
                        if (_item.fgMainProduct == true)
                        {
                            _check = false;
                            break;
                        }
                        else
                        {
                            _check = true;
                        }
                    }
                }
                else
                {
                    _check = true;
                }
                if (_check == true)
                {
                    //List<STCMsProductFormula> _listSTCMsProductFormula = this._productBL.GetListProductFormulaDtByProductAssembly(this.ProductPicker2.ProductCode);
                    
                    STCMsProductFormula _stcMsProductFormula = this._productBL.GetSingleProductFormulaDt(_ex[0], _ex[1]);

                    _stcMsProductFormula.fgMainProduct = true;

                    bool _result = this._productBL.EditProductFormulaDt(_stcMsProductFormula);

                    this.ClearLabel();
                    this.ShowData();
                }
                else
                {
                    this.WarningLabel0.Text = "Main product has been checked by other products.";
                }
            }

            if (e.CommandName == "UnCheck")
            {
                //List<STCMsProductFormula> _listSTCMsProductFormula = this._productBL.GetListProductFormulaDtByProductAssembly(this.ProductPicker2.ProductCode);
                String[] _ex = e.CommandArgument.ToString().Split('-');
                STCMsProductFormula _stcMsProductFormula = this._productBL.GetSingleProductFormulaDt(_ex[0], _ex[1]);

                _stcMsProductFormula.fgMainProduct = false;

                bool _result = this._productBL.EditProductFormulaDt(_stcMsProductFormula);

                this.ClearLabel();
                this.ShowData();
            }

            if (e.CommandName == "Delete")
            {
                String[] _ex = e.CommandArgument.ToString().Split('-');
                bool _result = this._productBL.DeleteMultiProductFormulaDt(_ex[1], _ex[0]);
                if (_result)
                {
                    this.ClearLabel();
                    this.ShowData();
                }
                //this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);
                //this.ShowData(Convert.ToInt32(e.CommandArgument));
            }
        }

        private void _listCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            RepeaterItem item = cb.NamingContainer as RepeaterItem;
            HiddenField _hiddenCode = item.FindControl("CodeHiddenField") as HiddenField;

            if (cb.Checked)
            {
                this.WarningLabel0.Text = "Tes";
            }
            
        }

        private Boolean IsChecked(String _prmValue)
        {
            Boolean _result = false;
                        
            //Boolean _result = false;
            //String[] _value = this.CheckHidden.Value.Split(',');

            //for (int i = 0; i < _value.Length; i++)
            //{
            //    if (_prmValue == _value[i])
            //    {
            //        _result = true;
            //        break;
            //    }
            //}

            return _result;
        }
    }
}