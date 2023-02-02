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
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.PriceGroup
{
    public partial class PriceGroupImport : PriceGroupBase
    {
        private PermissionBL _permBL = new PermissionBL();
        private PriceGroupBL _priceGroupBL = new PriceGroupBL();
        private CurrencyBL _currencyBL = new CurrencyBL();

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
            this.Panel1.Visible = true;
            this.LogPanel.Visible = false;
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
            List<Master_PriceGroup> _msPriceGroupList = new List<Master_PriceGroup>();

            string _error = "";
            string _priceGroupCode = "";
            int _year = 0;
            string _currCode = "";
            decimal _amountForex = 0;
            Boolean _fgActive = false;
            string _pgDesc = "";
            DateTime _startDate = new DateTime();
            DateTime _endDate = new DateTime();
            string _insertBy = HttpContext.Current.User.Identity.Name;
            DateTime _insertDate = DateTime.Now;
            string _editBy = HttpContext.Current.User.Identity.Name;
            DateTime _editDate = DateTime.Now;
            List<String> _currCodeError = new List<String>();
            List<String> _priceGroupError = new List<String>();
            string _priceGroupAll = "";
            string _currCodeAll = "";

            try
            {
                for (int i = 0; i < _prmDSExcel.Tables[0].Rows.Count; i++)
                {
                    _error = "";

                    if (_prmDSExcel.Tables[0].Rows[i][0].ToString() != "")
                    {
                        _priceGroupCode = _prmDSExcel.Tables[0].Rows[i][0].ToString();
                        _year = Convert.ToInt32(_prmDSExcel.Tables[0].Rows[i][1]);
                        _currCode = _prmDSExcel.Tables[0].Rows[i][2].ToString();
                        _amountForex = Convert.ToDecimal(_prmDSExcel.Tables[0].Rows[i][3]);
                        _fgActive = Convert.ToBoolean(_prmDSExcel.Tables[0].Rows[i][4]);
                        _pgDesc = _prmDSExcel.Tables[0].Rows[i][5].ToString();
                        _startDate = Convert.ToDateTime(_prmDSExcel.Tables[0].Rows[i][6]);
                        _endDate = Convert.ToDateTime(_prmDSExcel.Tables[0].Rows[i][7]);

                        bool _currExist = _currencyBL.IsCurrExists(_currCode);
                        if (_currExist == false)
                        {
                            _currCodeError.Add(_currCode);
                            _error = "Currency is not Exists";
                        }

                        foreach (Master_PriceGroup _row in this._priceGroupBL.GetList())
                        {
                            if (_row.PriceGroupCode == _priceGroupCode && _row.Year == _year)
                            {
                                _priceGroupError.Add(_priceGroupCode);
                                _error = "Price Group Code and Year is Exists";
                            }
                        }

                        if (_error == "")
                        {
                            _msPriceGroupList.Add(new Master_PriceGroup(_priceGroupCode, _year, _currCode, _amountForex, _fgActive, _pgDesc, _startDate, _endDate, _insertBy, _insertDate, _editBy, _editDate));
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                bool _result = this._priceGroupBL.AddPriceGroupList(_msPriceGroupList);

                if (_result == true)
                {
                    _error = _msPriceGroupList.Count + " row(s) of data has been successfully imported.<p />";
                }

                foreach (var _curr in _currCodeError)
                {
                    _currCodeAll += _curr + "<br />";
                }

                foreach (var _priceGroup in _priceGroupError)
                {
                    _priceGroupAll += _priceGroup + "<br />";
                }

                if (_currCodeAll != "")
                {
                    _error += "Currency is not in Master Currency : <br />" + _currCodeAll;
                }

                if (_priceGroupAll != "")
                {
                    _error += "Duplicate Price Group and Year : <br />" + _priceGroupAll;
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
                                String _fileName = "Price Group - " + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

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
