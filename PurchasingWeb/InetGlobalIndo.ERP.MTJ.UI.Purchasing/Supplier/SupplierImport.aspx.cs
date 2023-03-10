using System;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.Supplier
{
    public partial class SupplierImport : SupplierBase
    {
        private PermissionBL _permBL = new PermissionBL();
        private SupplierBL _suppBL = new SupplierBL();
        private TermBL _termBL = new TermBL();
        private CityBL _cityBL = new CityBL();
        private BankBL _bankBL = new BankBL();
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
                string _conn1 = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + ApplicationConfig.PurchasingImportPath + this.FileNameHiddenField.Value + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
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
            List<MsSupplier> _msSupplierList = new List<MsSupplier>();

            string _error = "";

            String _suppCode = "";
            String _suppName = "";
            String _suppType = "";
            String _suppGroup = "";
            String _address1 = "";
            String _address2 = "";
            String _city = "";
            String _postCode = "";
            String _telephone = "";
            String _fax = "";
            String _email = "";
            String _currCode = "";
            String _term = "";
            String _bank = "";
            String _rekeningNo = "";
            String _npwp = "";
            Char _fgPPN = ' ';
            String _nppkp = "";
            String _contactPerson = "";
            String _contactTitle = "";
            String _contactHP = "";
            Char _fgActive = ' ';
            String _remark = "";
            String _userid = "";
            DateTime _userdate = DateTime.Now;

            List<String> _suppTypeError = new List<String>();
            List<String> _suppGroupError = new List<String>();
            List<String> _cityError = new List<String>();
            List<String> _currError = new List<String>();
            List<String> _termError = new List<String>();
            List<String> _bankError = new List<String>();

            try
            {
                for (int i = 0; i < _prmDSExcel.Tables[0].Rows.Count; i++)
                {
                    if (_prmDSExcel.Tables[0].Rows[i][0].ToString() != "")
                    {
                        _suppCode = _prmDSExcel.Tables[0].Rows[i][0].ToString();
                        _suppName = _prmDSExcel.Tables[0].Rows[i][1].ToString();
                        _suppType = _prmDSExcel.Tables[0].Rows[i][2].ToString();
                        _suppGroup = _prmDSExcel.Tables[0].Rows[i][3].ToString();
                        _address1 = _prmDSExcel.Tables[0].Rows[i][4].ToString();
                        _address2 = _prmDSExcel.Tables[0].Rows[i][5].ToString();
                        _city = _prmDSExcel.Tables[0].Rows[i][6].ToString();
                        _postCode = _prmDSExcel.Tables[0].Rows[i][7].ToString();
                        _telephone = _prmDSExcel.Tables[0].Rows[i][8].ToString();
                        _fax = _prmDSExcel.Tables[0].Rows[i][9].ToString();
                        _email = _prmDSExcel.Tables[0].Rows[i][10].ToString();
                        _currCode = _prmDSExcel.Tables[0].Rows[i][11].ToString();
                        _term = _prmDSExcel.Tables[0].Rows[i][12].ToString();
                        _bank = _prmDSExcel.Tables[0].Rows[i][13].ToString();
                        _rekeningNo = _prmDSExcel.Tables[0].Rows[i][14].ToString();
                        _npwp = _prmDSExcel.Tables[0].Rows[i][15].ToString();
                        _fgPPN = Convert.ToChar(_prmDSExcel.Tables[0].Rows[i][16].ToString());
                        _nppkp = _prmDSExcel.Tables[0].Rows[i][17].ToString();
                        _contactPerson = _prmDSExcel.Tables[0].Rows[i][18].ToString();
                        _contactTitle = _prmDSExcel.Tables[0].Rows[i][19].ToString();
                        _contactHP = _prmDSExcel.Tables[0].Rows[i][20].ToString();
                        _fgActive = 'Y';
                        _remark = _prmDSExcel.Tables[0].Rows[i][21].ToString();
                        _userid = HttpContext.Current.User.Identity.Name;
                        _userdate = DateTime.Now;

                        bool _suppTypeExist = _suppBL.IsSuppTypeExists(_suppType);
                        if (_suppTypeExist == false)
                        {
                            _suppTypeError.Add(_suppType);
                            _error = "Supplier Type is not Exists";
                        }

                        bool _suppGroupExist = _suppBL.IsSuppGroupExists(_suppGroup);
                        if (_suppGroupExist == false)
                        {
                            _suppGroupError.Add(_suppGroup);
                            _error = "Supplier Group is not Exists";
                        }

                        bool _cityExist = _cityBL.IsCityExists(_city);
                        if (_cityExist == false)
                        {
                            _cityError.Add(_city);
                            _error = "City is not Exists";
                        }

                        bool _currExist = _currBL.IsCurrExists(_currCode);
                        if (_currExist == false)
                        {
                            _currError.Add(_currCode);
                            _error = "Currency is not Exists";
                        }

                        bool _termExist = _termBL.IsTermExists(_term);
                        if (_termExist == false)
                        {
                            _termError.Add(_city);
                            _error = "Term is not Exists";
                        }
                        bool _bankExist = _bankBL.IsBankExists(_bank);
                        if (_bankExist == false)
                        {
                            _bankError.Add(_bank);
                            _error = "Bank is not Exists";
                        }

                        if (_error == "")
                        {
                            _msSupplierList.Add(new MsSupplier(_suppCode, _suppName, _suppType, _suppGroup, _address1, _address2, _city, _postCode, _telephone, _fax, _email, _currCode, _term, _bank, _rekeningNo, _npwp, _fgPPN, _nppkp, _contactPerson, _contactTitle, _contactHP, _fgActive, _remark, _userid, _userdate));
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                bool _result = this._suppBL.AddSuppList(_msSupplierList);

                if (_result == true)
                {
                    _error = _msSupplierList.Count + " row(s) of data has been successfully imported.<p />";
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
                FileInfo _fileInfo = new FileInfo(ApplicationConfig.PurchasingImportPath + this.FileNameHiddenField.Value);
                _fileInfo.Delete();
            }

            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.FileNameHiddenField.Value != "")
            {
                FileInfo _fileInfo = new FileInfo(ApplicationConfig.PurchasingImportPath + this.FileNameHiddenField.Value);
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

                                if (File.Exists(ApplicationConfig.PurchasingImportPath + _fileName + _fileInfo.Extension) == false)
                                {
                                    //validasi size
                                    if (Convert.ToDecimal(FileNameFileUpload.PostedFile.ContentLength) < Convert.ToInt32(ApplicationConfig.ExcelMaxSize))
                                    {
                                        this.FileNameFileUpload.PostedFile.SaveAs(ApplicationConfig.PurchasingImportPath + _fileName + _fileInfo.Extension);
                                        this.FileNameHiddenField.Value = _fileName + _fileInfo.Extension;

                                        this.PathFileLabel.Text = this.FileNameFileUpload.PostedFile.FileName;

                                        this.WorksheetDropDownList.Items.Clear();
                                        string[] _result2 = this.GetExcelSheetNames(ApplicationConfig.PurchasingImportPath + _fileName + _fileInfo.Extension);
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
