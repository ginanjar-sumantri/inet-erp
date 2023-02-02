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

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FixedAsset
{
    public partial class FixedAssetImport : FixedAssetBase
    {
        private PermissionBL _permBL = new PermissionBL();
        private FixedAssetsBL _faBL = new FixedAssetsBL();
        private CurrencyBL _currBL = new CurrencyBL();

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
                string _conn1 = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + ApplicationConfig.AccountImportPath + this.FileNameHiddenField.Value + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
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
            List<MsFixedAsset> _msFixedAssetList = new List<MsFixedAsset>();

            string _error = "";

            String _faCode = "";
            String _faName = "";
            String _faStatus = "";
            char _faOwner = ' ';
            String _faSubGroup = "";
            DateTime _buyingDate = DateTime.Now;
            String _faLocationType = "";
            String _faLocationCode = "";
            String _curr = "";
            Decimal _forexRate = 0;
            Decimal _amountForex = 0;
            Decimal _amountHome = 0;
            int _totalLifeMonth = 0;
            int _lifeDepr = 0;
            int _lifeProcess = 0;
            int _totalLifeDepr = 0;
            Decimal _amountDepr = 0;
            Decimal _amountProcess = 0;
            Decimal _totalAmountDepr = 0;
            Decimal _amountCurrent = 0;
            char _fgActive = ' ';
            char _fgSold = ' ';
            char _fgProcess = ' ';
            String _specification = "";
            char _createdFrom = ' ';
            Boolean _createJournal = false;
            String _photo = "";

            List<String> _FAError = new List<String>();
            List<String> _currError = new List<String>();
            List<String> _faGroupSubError = new List<String>();
            List<String> _faStatusError = new List<String>();
            
            try
            {
                for (int i = 0; i < _prmDSExcel.Tables[0].Rows.Count; i++)
                {
                    if (_prmDSExcel.Tables[0].Rows[i][0].ToString() != "")
                    {
                        _faCode = _prmDSExcel.Tables[0].Rows[i][0].ToString();
                        _faName = _prmDSExcel.Tables[0].Rows[i][1].ToString();
                        _faStatus = _prmDSExcel.Tables[0].Rows[i][2].ToString();
                        _faOwner = Convert.ToChar(_prmDSExcel.Tables[0].Rows[i][3].ToString());
                        _faSubGroup = _prmDSExcel.Tables[0].Rows[i][4].ToString();
                        _buyingDate = Convert.ToDateTime(_prmDSExcel.Tables[0].Rows[i][5].ToString());
                        _faLocationType = _prmDSExcel.Tables[0].Rows[i][6].ToString();
                        _faLocationCode = _prmDSExcel.Tables[0].Rows[i][7].ToString();
                        _curr = _prmDSExcel.Tables[0].Rows[i][8].ToString();
                        _forexRate = Convert.ToDecimal(_prmDSExcel.Tables[0].Rows[i][9].ToString());
                        _amountForex = Convert.ToDecimal(_prmDSExcel.Tables[0].Rows[i][10].ToString());
                        _amountHome = Convert.ToDecimal(_prmDSExcel.Tables[0].Rows[i][11].ToString());
                        _totalLifeMonth = Convert.ToInt32(_prmDSExcel.Tables[0].Rows[i][12].ToString());
                        _lifeDepr = Convert.ToInt32(_prmDSExcel.Tables[0].Rows[i][13].ToString());
                        _lifeProcess = Convert.ToInt32(_prmDSExcel.Tables[0].Rows[i][14].ToString());
                        _totalLifeDepr = Convert.ToInt32(_prmDSExcel.Tables[0].Rows[i][15].ToString());
                        _amountDepr = Convert.ToDecimal(_prmDSExcel.Tables[0].Rows[i][16].ToString());
                        _amountProcess = 0;
                        _totalAmountDepr = _amountDepr;
                        _amountCurrent = _amountHome - _amountDepr;
                        _fgActive = 'Y';
                        _fgSold = 'N';
                        _fgProcess = Convert.ToChar(_prmDSExcel.Tables[0].Rows[i][17].ToString());
                        _specification = _prmDSExcel.Tables[0].Rows[i][18].ToString();
                        _createdFrom = 'M';
                        _createJournal = false;
                        _photo = "product_no_photo.jpg";

                        bool _faStatusExist = _faBL.IsFAStatusExist(_faStatus);
                        if (_faStatusExist == false)
                        {
                            _faStatusError.Add(_faStatus);
                            _error = "Fixed Asset Status is not Exists";
                        }

                        bool _faGroupSubExist = _faBL.IsFAGroupSubExist(_faSubGroup);
                        if (_faGroupSubExist == false)
                        {
                            _faStatusError.Add(_faSubGroup);
                            _error = "Fixed Asset Sub Group is not Exists";
                        }

                        bool _currExist = _currBL.IsCurrExists(_curr);
                        if (_currExist == false)
                        {
                            _currError.Add(_curr);
                            _error = "Currency is not Exists";
                        }

                        foreach (MsFixedAsset _row in this._faBL.GetListFixedAsset())
                        {
                            if (_row.FACode == _faCode)
                            {
                                _FAError.Add(_faCode);
                                _error = "Fixed Asset is Exists";
                            }
                        }

                        if (_error == "")
                        {
                            _msFixedAssetList.Add(new MsFixedAsset(_faCode, _faName, _faStatus, _faOwner, _faSubGroup, _buyingDate, _faLocationType, _faLocationCode, _curr, _forexRate, _amountForex, _amountHome, _totalLifeMonth, _lifeDepr, _lifeProcess, _totalLifeDepr, _amountDepr, _amountProcess, _totalAmountDepr, _amountCurrent, _fgActive, _fgSold, _fgProcess, _createdFrom, _createJournal, _photo));
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                bool _result = this._faBL.AddFixedAssetList(_msFixedAssetList);

                if (_result == true)
                {
                    _error = _msFixedAssetList.Count + " row(s) of data has been successfully imported.<p />";
                }
                
                //foreach (var _currency in _currError)
                //{
                //    _currAll += _currency + ", ";
                //}

                //foreach (var _fixedAssetSubGroup in _faGroupSubError)
                //{
                //    _faGroupSubAll += _fixedAssetSubGroup + ", ";
                //}

                //foreach (var _fixedAssetStatus in _faStatusError)
                //{
                //    _faStatusAll += _fixedAssetStatus + ", ";
                //}

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
                FileInfo _fileInfo = new FileInfo(ApplicationConfig.AccountImportPath + this.FileNameHiddenField.Value);
                _fileInfo.Delete();
            }

            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.FileNameHiddenField.Value != "")
            {
                FileInfo _fileInfo = new FileInfo(ApplicationConfig.AccountImportPath + this.FileNameHiddenField.Value);
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
                                String _fileName = "COA - " + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

                                if (File.Exists(ApplicationConfig.AccountImportPath + _fileName + _fileInfo.Extension) == false)
                                {
                                    //validasi size
                                    if (Convert.ToDecimal(FileNameFileUpload.PostedFile.ContentLength) < Convert.ToInt32(ApplicationConfig.ExcelMaxSize))
                                    {
                                        this.FileNameFileUpload.PostedFile.SaveAs(ApplicationConfig.AccountImportPath + _fileName + _fileInfo.Extension);
                                        this.FileNameHiddenField.Value = _fileName + _fileInfo.Extension;

                                        this.PathFileLabel.Text = this.FileNameFileUpload.PostedFile.FileName;

                                        this.WorksheetDropDownList.Items.Clear();
                                        string[] _result2 = this.GetExcelSheetNames(ApplicationConfig.AccountImportPath + _fileName + _fileInfo.Extension);
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
