using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockBeginning
{
    public partial class StockBeginningImport : StockBeginningBase
    {
        private PermissionBL _permBL = new PermissionBL();
        private ProductBL _productBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private StockBeginningBL _stockBeginningBL = new StockBeginningBL();
        
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
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";
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
            List<STCAdjustDt> _stcAdjustDt = new List<STCAdjustDt>();

            string _error = "";
            string _productCode = "";
            string _locationCode = "";
            decimal _qty = 0;
            decimal _priceCost = 0;
            string _remark = "";

            string _transNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _unit = "";
            Char _fgAdjust = '+';
            decimal _totalCost = 0;

            List<String> _productCodeError = new List<String>();
            List<String> _locationError = new List<String>();
            List<String> _unitError = new List<String>();

            string _productCodeAll = "";
            string _locationAll = "";
            string _unitAll = "";
            
            try
            {
                for (int i = 0; i < _prmDSExcel.Tables[0].Rows.Count; i++)
                {
                    _error = "";

                    if (_prmDSExcel.Tables[0].Rows[i][0].ToString() != "")
                    {
                        //_transNmbr
                        _productCode = _prmDSExcel.Tables[0].Rows[i][0].ToString().Trim();
                        _locationCode = _prmDSExcel.Tables[0].Rows[i][1].ToString().Trim();
                        _qty = Convert.ToDecimal(_prmDSExcel.Tables[0].Rows[i][2].ToString().Trim());
                        String _unitProduct = this._productBL.GetSingleProduct(_productCode).Unit;
                        _unit = _unitProduct;
                        //_fgAdjust
                        _priceCost = Convert.ToDecimal(_prmDSExcel.Tables[0].Rows[i][3].ToString().Trim());
                        _totalCost = _qty * _priceCost;
                        _remark = _prmDSExcel.Tables[0].Rows[i][4].ToString().Trim();
                        
                        bool _unitExist = this._unitBL.IsUnitExists(_unit);
                        if (_unitExist == false)
                        {
                            _unitError.Add(_unit);
                            _error = "Unit is not Exists";
                        }

                        STCAdjustHd _stcAdjust = this._stockBeginningBL.GetSingleSTCAdjustHd(_transNmbr);

                        bool _locationExist = this._warehouseBL.IsLocationExistInWarehouse(_stcAdjust.WrhsCode, _locationCode);
                        if (_locationExist == false)
                        {
                            _locationError.Add(_locationCode);
                            _error = "Location is not Exists in assigned warehouse";
                        }

                        bool _productExist = this._productBL.IsProductCodeExists(_productCode);
                        if (_productExist == false)
                        {
                            _productCodeError.Add(_unit);
                            _error = "Product is not Exists";
                        }

                        if (_error == "")
                        {
                            _stcAdjustDt.Add(new STCAdjustDt(_transNmbr, _productCode, _locationCode, _qty, _unit, _fgAdjust, _remark, _priceCost, _totalCost));
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                bool _result = this._stockBeginningBL.AddSTCAdjustDtList(_stcAdjustDt);

                if (_result == true)
                {
                    _error = _stcAdjustDt.Count + " row(s) of data has been successfully imported.<p />";
                }

                foreach (var _unitCode in _unitError)
                {
                    _unitAll += _unitCode + "<br />";
                }

                foreach (var _locCode in _locationError)
                {
                    _locationAll += _locCode + "<br />";
                }
                
                foreach (var _prod in _productCodeError)
                {
                    _productCodeAll += _prod + "<br />";
                }

                if (_unitAll != "")
                {
                    _error += "Unit is not in Master Unit : <br />" + _unitAll;
                }
                if (_locationAll != "")
                {
                    _error += "Location is not in assigned in Master Warehouse : <br />" + _locationAll;
                }
                if (_productCodeAll != "")
                {
                    _error += "Product Code is not in Master Product : <br />" + _productCodeAll;
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

            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
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
