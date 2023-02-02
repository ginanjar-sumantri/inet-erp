using System;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Web;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.Data.OleDb;
using System.Data;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Collections.Generic;
using System.IO;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Product
{
    public partial class ProductImport : ProductBase
    {
        private PermissionBL _permBL = new PermissionBL();
        private ProductBL _productBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private PriceGroupBL _priceGroupBL = new PriceGroupBL();

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

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ClearData();
            }
        }

        protected void ClearData()
        {
            this.WorksheetDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.WorksheetDropDownList.SelectedValue = "null";
            this.PathFileLabel.Text = "";
            this.LogPanel.Visible = false;
            this.Panel1.Visible = true;
        }

        public void GetData()
        {
            //Microsoft.ACE.OLEDB.12.0
            //Microsoft.Jet.OLEDB.4.0
            try
            {
                string _conn1 = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + ApplicationConfig.StockControlImportPath + this.FileNameHiddenField.Value + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
                OleDbConnection _conn2 = new OleDbConnection(_conn1);

                string _query = "SELECT * FROM [" + this.WorksheetDropDownList.SelectedValue + "]";

                OleDbDataAdapter _da = new OleDbDataAdapter(_query, _conn2);

                DataSet _dsExcel = new DataSet();
                _dsExcel.Clear();
                _da.Fill(_dsExcel);

                _conn2.Dispose();

                this.ImportData(_dsExcel);
            }
            catch (Exception ex)
            {
                this.WarningLabel.Text = "Invalid File, Try Import Again";
            }
        }

        public void ImportData(DataSet _prmDSExcel)
        {
            List<MsProduct> _msProductList = new List<MsProduct>();

            string _error = "";
            string _productCode = "";
            string _productName = "";
            string _productSubGroup = "";
            string _productType = "";
            string _specification1 = "";
            string _specification2 = "";
            string _specification3 = "";
            string _specification4 = "";
            decimal _minQty = 0;
            decimal _maxQty = 0;
            string _unit = "";
            string _unitOrder = "";
            decimal _length = 0;
            decimal _width = 0;
            decimal _height = 0;
            decimal _volume = 0;
            decimal _weight = 0;
            string _purchaseCurr = "";            
            String _priceGroupCode = "";
            decimal _buyingPrice = 0;
            decimal _sellingPrice = 0;
            decimal _cogsPriceForex = 0;
            decimal _forexRate = 0;
            string _barcode = "";
            char _fgActive = ' ';
            bool _fgConsignment = false;
            string _ConsignmentSuppCode = "";
            string _userID = HttpContext.Current.User.Identity.Name;
            DateTime _userDate = DateTime.Now;
            List<String> _unitError = new List<String>();
            List<String> _unitOrderError = new List<String>();
            List<String> _productSubGroupError = new List<String>();
            List<String> _productTypeError = new List<String>();
            List<String> _purchaseCurrError = new List<String>();
            List<String> _priceGroupError = new List<String>();
            List<String> _productCodeError = new List<String>();
            string _unitAll = "";
            string _unitOrderAll = "";
            string _productSubGroupAll = "";
            string _productTypeAll = "";
            string _purchaseCurrAll = "";
            string _priceGroupAll = "";
            string _productCodeAll = "";

            try
            {
                for (int i = 0; i < _prmDSExcel.Tables[0].Rows.Count; i++)
                {
                    _error = "";

                    if (_prmDSExcel.Tables[0].Rows[i][0].ToString() != "")
                    {
                        _productCode = _prmDSExcel.Tables[0].Rows[i][0].ToString().Trim();
                        _productName = _prmDSExcel.Tables[0].Rows[i][1].ToString().Trim();
                        _productSubGroup = _prmDSExcel.Tables[0].Rows[i][2].ToString().Trim();
                        _productType = _prmDSExcel.Tables[0].Rows[i][3].ToString().Trim();
                        _specification1 = _prmDSExcel.Tables[0].Rows[i][4].ToString().Trim();
                        _specification2 = _prmDSExcel.Tables[0].Rows[i][5].ToString().Trim();
                        _specification3 = _prmDSExcel.Tables[0].Rows[i][6].ToString().Trim();
                        _specification4 = _prmDSExcel.Tables[0].Rows[i][7].ToString().Trim();
                        _minQty = Convert.ToDecimal((_prmDSExcel.Tables[0].Rows[i][8]).ToString().Trim());
                        _maxQty = Convert.ToDecimal((_prmDSExcel.Tables[0].Rows[i][9]).ToString().Trim());
                        _unit = _prmDSExcel.Tables[0].Rows[i][10].ToString().Trim();
                        _unitOrder = _prmDSExcel.Tables[0].Rows[i][11].ToString().Trim();
                        _length = Convert.ToDecimal((_prmDSExcel.Tables[0].Rows[i][12]).ToString().Trim());
                        _width = Convert.ToDecimal((_prmDSExcel.Tables[0].Rows[i][13]).ToString().Trim());
                        _height = Convert.ToDecimal((_prmDSExcel.Tables[0].Rows[i][14]).ToString().Trim());
                        _volume = Convert.ToDecimal((_prmDSExcel.Tables[0].Rows[i][15]).ToString().Trim());
                        _weight = Convert.ToDecimal((_prmDSExcel.Tables[0].Rows[i][16]).ToString().Trim());
                        _purchaseCurr = _prmDSExcel.Tables[0].Rows[i][17].ToString().Trim();
                        _cogsPriceForex = Convert.ToDecimal((_prmDSExcel.Tables[0].Rows[i][18]).ToString().Trim());
                        _forexRate = Convert.ToDecimal((_prmDSExcel.Tables[0].Rows[i][19]).ToString().Trim());
                        _priceGroupCode = _prmDSExcel.Tables[0].Rows[i][20].ToString().Trim();
                        _buyingPrice = Convert.ToDecimal((_prmDSExcel.Tables[0].Rows[i][21]).ToString().Trim());
                        _sellingPrice = Convert.ToDecimal((_prmDSExcel.Tables[0].Rows[i][22]).ToString().Trim());
                        _fgActive = Convert.ToChar((_prmDSExcel.Tables[0].Rows[i][23]).ToString().Trim());
                        _barcode = _prmDSExcel.Tables[0].Rows[i][24].ToString().Trim();
                        _fgConsignment = Convert.ToBoolean(_prmDSExcel.Tables[0].Rows[i][26].ToString().Trim());
                        _ConsignmentSuppCode = (_prmDSExcel.Tables[0].Rows[i][27].ToString().Trim());

                        bool _unitExist = _unitBL.IsUnitExists(_unit);
                        if (_unitExist == false)
                        {
                            _unitError.Add(_unit);
                            _error = "Unit is not Exists";
                        }

                        bool _unitOrderExist = _unitBL.IsUnitExists(_unitOrder);
                        if (_unitOrderExist == false)
                        {
                            _unitOrderError.Add(_unitOrder);
                            _error = "Unit Order is not Exists";
                        }

                        bool _productSubGroupExist = _productBL.IsProductSubGroupExists(_productSubGroup);
                        if (_productSubGroupExist == false)
                        {
                            _productSubGroupError.Add(_productSubGroup);
                            _error = "Product Sub Group is not Exists";
                        }

                        bool _productTypeExist = _productBL.IsProductTypeExists(_productType);
                        if (_productTypeExist == false)
                        {
                            _productTypeError.Add(_productType);
                            _error = "Product Type is not Exists";
                        }

                        bool _purchaseCurrExist = _currencyBL.IsCurrExists(_purchaseCurr);
                        if (_purchaseCurrExist == false)
                        {
                            _purchaseCurrError.Add(_purchaseCurr);
                            _error = "Purchase Currency is not Exists";
                        }

                        if (_priceGroupCode != "")
                        {
                            bool _priceGroupExist = _priceGroupBL.IsPriceGroupExists(_priceGroupCode);
                            if (_priceGroupExist == false)
                            {
                                _priceGroupError.Add(_priceGroupCode);
                                _error = "Price Group is not Exists";
                            }
                        }
                        else
                        {
                            _priceGroupCode = null;
                        }

                        foreach (MsProduct _row in this._productBL.GetListForDDLProduct())
                        {
                            if (_row.ProductCode == _productCode)
                            {
                                _productCodeError.Add(_productCode);
                                _error = "Product Code is Exists";
                            }
                        }

                        if (_error == "")
                        {
                            _msProductList.Add(new MsProduct(_productCode, _productName, _productSubGroup, 
                                _productType, _specification1, _specification2, _specification3, _specification4, 
                                _minQty, _maxQty, _unit, _unitOrder, _length, _width, _height, _volume, 
                                _weight, _purchaseCurr, _priceGroupCode, _buyingPrice, _sellingPrice, 
                                _fgActive, _userID, _userDate, _barcode, _fgConsignment, _ConsignmentSuppCode));
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                bool _result = this._productBL.AddProductList(_msProductList);

                if (_result == true)
                {
                    _error = _msProductList.Count + " row(s) of data has been successfully imported.<p />";
                }

                foreach (var _unitCode in _unitError)
                {
                    _unitAll += _unitCode + "<br />";
                }

                foreach (var _unitOrderCode in _unitOrderError)
                {
                    _unitOrderAll += _unitOrderCode + "<br />";
                }

                foreach (var _productSubGroupCode in _productSubGroupError)
                {
                    _productSubGroupAll += _productSubGroupCode + "<br />";
                }

                foreach (var _productTypeCode in _productTypeError)
                {
                    _productTypeAll += _productTypeCode + "<br />";
                }

                foreach (var _purchaseCurrCode in _purchaseCurrError)
                {
                    _purchaseCurrAll += _purchaseCurrCode + "<br />";
                }

                foreach (var _priceGroup in _priceGroupError)
                {
                    _priceGroupAll += _priceGroup + "<br />";
                }

                foreach (var _prod in _productCodeError)
                {
                    _productCodeAll += _prod + "<br />";
                }

                if (_unitAll != "")
                {
                    _error += "Unit is not in Master Unit : <br />" + _unitAll;
                }
                if (_unitOrderAll != "")
                {
                    _error += "Unit Order is not in Master Unit : <br />" + _unitOrderAll;
                }
                if (_productSubGroupAll != "")
                {
                    _error += "Product Sub Group is not in Master Product Sub Group : <br />" + _productSubGroupAll;
                }
                if (_productTypeAll != "")
                {
                    _error += "Product Type is not in Master Product Type : <br />" + _productTypeAll;
                }
                if (_purchaseCurrAll != "")
                {
                    _error += "Purchase Currency is not in Master Currency : <br />" + _purchaseCurrAll;
                }
                if (_priceGroupAll != "")
                {
                    _error += "Price Group is not in Master Price Group : <br />" + _priceGroupAll;
                }
                if (_productCodeAll != "")
                {
                    _error += "Duplicate Product Code : <br />" + _productCodeAll;
                }

                this.LogPanel.Visible = true;
                this.ErrorLogLabel.Text = _error;
            }
            catch (Exception ex)
            {
                this.WarningLabel.Text = "You Failed Add Data, Please Correct Your File Excel Template";
            }
        }

        private String[] GetExcelSheetNames(string _prmfileName)
        {
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;

            try
            {
                String connString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + _prmfileName + ";Extended Properties=Excel 8.0;";

                objConn = new OleDbConnection(connString);
                objConn.Open();

                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    return null;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }

                // Loop through all of the sheets if you want too...
                for (int j = 0; j < excelSheets.Length; j++)
                {
                    // Query each excel sheet.
                }

                return excelSheets;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.GetData();
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.FileNameHiddenField.Value != "")
            {
                FileInfo _fileInfo = new FileInfo(ApplicationConfig.StockControlImportPath + this.FileNameHiddenField.Value);
                _fileInfo.Delete();
            }

            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.FileNameHiddenField.Value != "")
            {
                FileInfo _fileInfo = new FileInfo(ApplicationConfig.StockControlImportPath + this.FileNameHiddenField.Value);
                _fileInfo.Delete();
            }

            this.ClearData();
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            this.WarningLabel.Text = "";
            this.PathFileLabel.Text = "";

            if (this.FileNameFileUpload.PostedFile.FileName.Trim() != "")
            {
                FileInfo _fileInfo = new FileInfo(this.FileNameFileUpload.PostedFile.FileName);

                if (_fileInfo.Name != "")
                {
                    string[] _validExtension = ApplicationConfig.ExcelFileExtension.Split(',');

                    foreach (string _temp in _validExtension)
                    {
                        if (_fileInfo.Extension == _temp)
                        {
                            while (true)
                            {
                                String _fileName = "Product - " + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

                                if (File.Exists(ApplicationConfig.StockControlImportPath + _fileName + _fileInfo.Extension) == false)
                                {
                                    //validasi size
                                    if (Convert.ToDecimal(FileNameFileUpload.PostedFile.ContentLength) < Convert.ToInt32(ApplicationConfig.ExcelMaxSize))
                                    {
                                        this.FileNameFileUpload.PostedFile.SaveAs(ApplicationConfig.StockControlImportPath + _fileName + _fileInfo.Extension);
                                        this.FileNameHiddenField.Value = _fileName + _fileInfo.Extension;

                                        this.PathFileLabel.Text = this.FileNameFileUpload.PostedFile.FileName;

                                        this.WorksheetDropDownList.Items.Clear();
                                        string[] _result2 = this.GetExcelSheetNames(ApplicationConfig.StockControlImportPath + _fileName + _fileInfo.Extension);
                                        foreach (string _sheetName in _result2)
                                        {
                                            this.WorksheetDropDownList.Items.Add(_sheetName);
                                        }
                                        this.WorksheetDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

                                    }
                                    else
                                    {
                                        this.WarningLabel.Text = "File Only Allow Only 10 MegaBytes";
                                    }

                                    break;
                                }
                            }

                            break;
                        }
                        else
                        {
                            this.WarningLabel.Text = "File Must Be Excel Extension";
                        }
                    }
                }
                else
                {
                    this.WarningLabel.Text = "File Name Not Valid";
                }
            }
            else
            {
                this.WarningLabel.Text = "File Name Must be Filled";
            }
        }
    }
}
