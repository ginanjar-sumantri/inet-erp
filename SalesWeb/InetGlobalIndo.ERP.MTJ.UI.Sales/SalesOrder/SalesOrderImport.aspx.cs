using System;
using System.Web.UI;
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
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.SalesOrder
{
    public partial class SalesOrderImport : SalesOrderBase
    {
        private PermissionBL _permBL = new PermissionBL();
        private SalesOrderBL _salesOrderBL = new SalesOrderBL();
        private ProductBL _productBL = new ProductBL();
        private PriceGroupBL _priceGroupBL = new PriceGroupBL();
        private CompanyConfig _companyConfigBL = new CompanyConfig();

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
                string _conn1 = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + ApplicationConfig.SalesImportPath + this.FileNameHiddenField.Value + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
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
            List<MKTSODt> _mktSODtList = new List<MKTSODt>();

            String _error = "";
            String _error1 = "";
            String _transNmbr = "";
            int _revisi = 0;
            String _productCode = "";
            String _specification = "";
            Decimal _qtyOrder = 0;
            String _unitOrder = "";
            Decimal? _qty = 0;
            String _unit = "";
            Decimal _price = 0;
            Decimal _amount = 0;
            Decimal _qtyDO = 0;
            DateTime _requireDate = new DateTime();
            String _remark = "";
            Char? _doneClosing = ' ';
            List<String> _productError = new List<String>();
            List<String> _productError2 = new List<String>();
            string _productAll = "";
            string _productAll2 = "";

            try
            {
                for (int i = 0; i < _prmDSExcel.Tables[0].Rows.Count; i++)
                {
                    _error = "";
                    _error1 = "";

                    if (_prmDSExcel.Tables[0].Rows[i][0].ToString() != "")
                    {
                        _transNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                        _revisi = Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisiKey), ApplicationConfig.EncryptionKey));
                        _productCode = _prmDSExcel.Tables[0].Rows[i][0].ToString();
                        _qtyOrder = Convert.ToDecimal(_prmDSExcel.Tables[0].Rows[i][1].ToString());
                        _unitOrder = this._productBL.GetUnitCodeByCode(_productCode);
                        _qty = _qtyOrder;
                        _unit = _unitOrder;
                        _remark = _prmDSExcel.Tables[0].Rows[i][3].ToString();
                        _doneClosing = SalesOrderDataMapper.GetStatusDetail(SalesOrderStatusDt.Open);
                        _qtyDO = 0;

                        bool _productExist = _productBL.IsProductCodeExists(_productCode);
                        if (_productExist == false)
                        {
                            _productError.Add(_productCode);
                            _error1 = "Product is not Exists";
                        }
                        else
                        {
                            Boolean _isUsingPG = _productBL.GetSingleProductType(_productBL.GetSingleProduct(_productCode).ProductType).IsUsingPG;
                            if (_isUsingPG == true)
                            {
                                Int32 _activeYear = Convert.ToInt32(_companyConfigBL.GetSingle(CompanyConfigure.ActivePGYear).SetValue);
                                _price = _priceGroupBL.GetSingle(_productBL.GetSingleProduct(_productCode).PriceGroupCode, _activeYear).AmountForex;
                            }
                            else
                            {
                                _price = Convert.ToDecimal(_prmDSExcel.Tables[0].Rows[i][2].ToString());
                            }
                        }

                        _amount = _qtyOrder * _price;

                        foreach (String _row in this._salesOrderBL.GetListProductMKTSODt(_transNmbr, _revisi))
                        {
                            if (_row == _productCode)
                            {
                                _productError2.Add(_productCode);
                                _error1 = "Product is Exists in Detail Sales Order";
                            }
                        }
                        if (_error1 == "")
                        {
                            _mktSODtList.Add(new MKTSODt(_transNmbr, _revisi, _productCode, _qtyOrder, _unitOrder, _qty, _unit, _price, _amount, _remark, _doneClosing, _qtyDO));
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                bool _result = this._salesOrderBL.AddMKTSODtList(_mktSODtList);

                if (_result == true)
                {
                    _error = _mktSODtList.Count + " row(s) of data has been successfully imported.<p />";
                }

                foreach (var _prod in _productError)
                {
                    _productAll += _prod + "<br />";
                }
                foreach (var _prod2 in _productError2)
                {
                    _productAll2 += _prod2 + "<br />";
                }

                if (_productAll != "")
                {
                    _error += "Product Code is not in Master Product : <br />" + _productAll;
                }
                if (_productAll2 != "")
                {
                    _error += "Product Code is already in Detail Purchase Request : <br />" + _productAll2;
                }

                this.LogPanel.Visible = true;
                this.ErrorLogLabel.Text = _error;
            }
            catch (Exception ex)
            {
                this.WarningLabel.Text = "You Failed Add Data, Please Correct Your File Excel Template";

                FileInfo _fileInfo = new FileInfo(ApplicationConfig.SalesImportPath + this.FileNameHiddenField.Value);
                _fileInfo.Delete();
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
            //if (this.FileNameHiddenField.Value != "")
            //{
            //    FileInfo _fileInfo = new FileInfo(ApplicationConfig.SalesImportPath + this.FileNameHiddenField.Value);
            //    _fileInfo.Delete();
            //}

            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisiKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeRevisiKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.FileNameHiddenField.Value != "")
            {
                FileInfo _fileInfo = new FileInfo(ApplicationConfig.SalesImportPath + this.FileNameHiddenField.Value);
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
                                String _fileName = "SO - " + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

                                if (File.Exists(ApplicationConfig.SalesImportPath + _fileName + _fileInfo.Extension) == false)
                                {
                                    //validasi size
                                    if (Convert.ToDecimal(FileNameFileUpload.PostedFile.ContentLength) < Convert.ToInt32(ApplicationConfig.ExcelMaxSize))
                                    {
                                        this.FileNameFileUpload.PostedFile.SaveAs(ApplicationConfig.SalesImportPath + _fileName + _fileInfo.Extension);
                                        this.FileNameHiddenField.Value = _fileName + _fileInfo.Extension;

                                        this.PathFileLabel.Text = this.FileNameFileUpload.PostedFile.FileName;

                                        this.WorksheetDropDownList.Items.Clear();
                                        string[] _result2 = this.GetExcelSheetNames(ApplicationConfig.SalesImportPath + _fileName + _fileInfo.Extension);
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
