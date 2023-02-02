using System;
using System.Web.UI;
using System.Data.SqlClient;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Web;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.Data.OleDb;
using System.Data;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.BusinessRule.NCC;
using System.IO;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockBeginning
{
    public partial class NCPImport : StockBeginningBase
    {
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private PermissionBL _permBL = new PermissionBL();
        private NCPBL _ncpBL = new NCPBL();
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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetAttribute();

                this.ShowCurrency();

                this.ClearData();
            }
        }

        private void SetAttribute()
        {
            this.ForexRateTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.ForexRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        public void ShowCurrency()
        {
            this.CurrDropDownList.DataTextField = "CurrCode";
            this.CurrDropDownList.DataValueField = "CurrCode";
            this.CurrDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrDropDownList.DataBind();
            this.CurrDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearData()
        {
            this.WorksheetDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.WorksheetDropDownList.SelectedValue = "null";
            this.CurrDropDownList.SelectedValue = "null";
            this.ForexRateTextBox.Text = "0";
            this.PathFileLabel.Text = "";
            this.LogPanel.Visible = false;
            this.Panel1.Visible = true;

            Transaction_NCPImport _transImport = _ncpBL.GetSingleTransactionNCPImport(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            string _currCodeHome = _currencyBL.GetCurrDefault();
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrDropDownList.SelectedValue);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            if (_transImport != null)
            {
                this.CurrDropDownList.SelectedValue = _transImport.Currency;
                this.ForexRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }

            if (this.CurrDropDownList.SelectedValue == _currCodeHome)
            {
                this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                this.ForexRateTextBox.Text = "1";
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#cccccc");
            }
            else
            {
                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
        }

        public void GetData()
        {
            //Microsoft.ACE.OLEDB.12.0
            //Microsoft.Jet.OLEDB.4.0
            try
            {
                string _conn1 = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + ApplicationConfig.NCPImportPath + this.FileNameHiddenField.Value + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
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
            List<MsProduct_SerialNumber> _msProduct_SerialNumber = new List<MsProduct_SerialNumber>();

            string _error = "";
            string _serialNumber = "";
            string _pin = "";
            string _expirationDate = "";
            string _country = "";
            char _bulkMode = ' ';
            string _productCode = "";
            string _transNmbr = "";
            string _manufactureID = "";
            bool _isSold = false;
            List<String> _snError = new List<String>();
            List<String> _productError = new List<String>();
            string _SNAll = "";
            string _productAll = "";

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _product = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _location = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

            Transaction_NCPImport _transactionNCPImport = null;

            try
            {
                _transactionNCPImport = _ncpBL.GetSingleTransactionNCPImport(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                if (_transactionNCPImport == null)
                {
                    _transactionNCPImport = new Transaction_NCPImport();

                    _transactionNCPImport.TransNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                    _transactionNCPImport.TransDate = DateTime.Now;
                    _transactionNCPImport.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
                    _transactionNCPImport.Currency = this.CurrDropDownList.SelectedValue;
                }

                for (int i = 0; i < _prmDSExcel.Tables[0].Rows.Count; i++)
                {
                    _error = "";

                    if (_prmDSExcel.Tables[0].Rows[i][0].ToString() != "")
                    {
                        _transNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                        _serialNumber = _prmDSExcel.Tables[0].Rows[i][0].ToString();
                        _pin = _prmDSExcel.Tables[0].Rows[i][1].ToString();
                        _expirationDate = _prmDSExcel.Tables[0].Rows[i][2].ToString();
                        //_country = _prmDSExcel.Tables[0].Rows[i][3].ToString();
                        //_bulkMode = Convert.ToChar(_prmDSExcel.Tables[0].Rows[i][4]);
                        _manufactureID = _prmDSExcel.Tables[0].Rows[i][3].ToString();
                        _isSold = false;
                        _productCode = _prmDSExcel.Tables[0].Rows[i][0].ToString().Substring(0, 4);

                        bool _productExist = _productBL.IsProductCodeExists(_prmDSExcel.Tables[0].Rows[i][0].ToString().Substring(0, 4));
                        if (_productExist == false)
                        {
                            _productError.Add(_productCode);
                            _error = "Product is not Exists";
                        }

                        if (_product != _productCode)
                        {
                            _productError.Add(_productCode);
                            _error = "Product is different with detail";
                        }

                        foreach (String _row in this._ncpBL.GetListSerialNumber())
                        {
                            if (_row == _serialNumber)
                            {
                                _snError.Add(_serialNumber);
                                _error = "Serial Number is Exists";
                            }
                        }

                        if (_error == "")
                        {
                            _msProduct_SerialNumber.Add(new MsProduct_SerialNumber(_serialNumber, _pin, _expirationDate, _isSold, _productCode, _transNmbr, _manufactureID, 0));
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                bool _result = this._ncpBL.Add(_transNo, _transactionNCPImport, _msProduct_SerialNumber);

                if (_result == true)
                {
                    _error = _msProduct_SerialNumber.Count + " row(s) of data has been successfully imported.<p />";
                }

                foreach (var _serial in _snError)
                {
                    _SNAll += _serial + "<br />";
                }

                foreach (var _prod in _productError)
                {
                    _productAll += _prod + "<br />";
                }

                if (_SNAll != "")
                {
                    _error += "Duplicate Serial Number : <br />" + _SNAll;
                }
                if (_productAll != "")
                {
                    _error += "Product Code is not in Master Product : <br />" + _productAll;
                }

                this.LogPanel.Visible = true;
                this.ErrorLogLabel.Text = _error;
            }
            catch (Exception ex)
            {
                _error = "You Failed Add Data, Please Correct Your File Excel Template";
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
                FileInfo _fileInfo = new FileInfo(ApplicationConfig.NCPImportPath + this.FileNameHiddenField.Value);
                _fileInfo.Delete();
            }

            Response.Redirect(this._viewDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._productKey)) + "&" + this._locationKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._locationKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.FileNameHiddenField.Value != "")
            {
                FileInfo _fileInfo = new FileInfo(ApplicationConfig.NCPImportPath + this.FileNameHiddenField.Value);
                _fileInfo.Delete();
            }

            this.ClearData();
        }

        protected void CurrDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DecimalPlaceHiddenField.Value = "";

            if (this.CurrDropDownList.SelectedValue != "null")
            {
                string _currCodeHome = _currencyBL.GetCurrDefault();
                byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrDropDownList.SelectedValue);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                this.ForexRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                if (this.CurrDropDownList.SelectedValue == _currCodeHome)
                {
                    this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.ForexRateTextBox.Text = "1";
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#cccccc");
                }
                else
                {
                    this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#ffffff");
                }
            }
            else
            {
                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
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
                                String _fileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

                                if (File.Exists(ApplicationConfig.NCPImportPath + _fileName + _fileInfo.Extension) == false)
                                {
                                    //validasi size
                                    if (Convert.ToDecimal(FileNameFileUpload.PostedFile.ContentLength) < Convert.ToInt32(ApplicationConfig.ExcelMaxSize))
                                    {
                                        this.FileNameFileUpload.PostedFile.SaveAs(ApplicationConfig.NCPImportPath + _fileName + _fileInfo.Extension);
                                        this.FileNameHiddenField.Value = _fileName + _fileInfo.Extension;

                                        this.PathFileLabel.Text = this.FileNameFileUpload.PostedFile.FileName;

                                        this.WorksheetDropDownList.Items.Clear();
                                        string[] _result2 = this.GetExcelSheetNames(ApplicationConfig.NCPImportPath + _fileName + _fileInfo.Extension);
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
