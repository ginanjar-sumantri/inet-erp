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

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.Account
{
    public partial class AccountImport : AccountBase
    {
        private PermissionBL _permBL = new PermissionBL();
        private AccountBL _accountBL = new AccountBL();
        private AccClassBL _accClassBL = new AccClassBL();
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
            List<MsAccount> _msAccountList = new List<MsAccount>();

            string _error = "";
            string _account = "";
            string _accountName = "";
            Guid _branchAccCode = new Guid();
            string _accClassCode = "";
            string _detail = "";
            string _currCode = "";
            char _fgSubled = ' ';
            char _fgNormal = ' ';
            char _fgActive = ' ';
            string _userID = HttpContext.Current.User.Identity.Name;
            DateTime _userDate = DateTime.Now;
            string _userClose = HttpContext.Current.User.Identity.Name;
            DateTime _closeDate = DateTime.Now;
            List<String> _accError = new List<String>();
            List<String> _accClassError = new List<String>();
            List<String> _branchAccountError = new List<String>();
            List<String> _currError = new List<String>();
            string _accAll = "";
            string _accClassAll = "";
            string _branchAccAll = "";
            string _currAll = "";

            try
            {
                for (int i = 0; i < _prmDSExcel.Tables[0].Rows.Count; i++)
                {
                    if (_prmDSExcel.Tables[0].Rows[i][0].ToString() != "")
                    {
                        _account = _prmDSExcel.Tables[0].Rows[i][0].ToString();
                        _accountName = _prmDSExcel.Tables[0].Rows[i][1].ToString();
                        _accClassCode = _prmDSExcel.Tables[0].Rows[i][3].ToString();
                        _detail = _prmDSExcel.Tables[0].Rows[i][4].ToString();
                        _fgSubled = Convert.ToChar(_prmDSExcel.Tables[0].Rows[i][5].ToString());
                        _fgNormal = Convert.ToChar(_prmDSExcel.Tables[0].Rows[i][6].ToString());
                        _currCode = _prmDSExcel.Tables[0].Rows[i][7].ToString();
                        _fgActive = Convert.ToChar(_prmDSExcel.Tables[0].Rows[i][8].ToString());

                        bool _accClassExist = _accClassBL.IsAccountClassExists(_accClassCode);
                        if (_accClassExist == false)
                        {
                            _accClassError.Add(_accClassCode);
                            _error = "Account Class is not Exists";
                        }

                        bool _currExist = _currBL.IsCurrExists(_currCode);
                        if (_currExist == false)
                        {
                            _currError.Add(_currCode);
                            _error = "Currency is not Exists";
                        }

                        bool _branchAccountExist = _accountBL.IsBranchAccountExists(_prmDSExcel.Tables[0].Rows[i][2].ToString());
                        if (_branchAccountExist == false)
                        {
                            _branchAccountError.Add(_prmDSExcel.Tables[0].Rows[i][2].ToString());
                            _error = "Branch Account is not Exists";
                        }
                        else
                        {
                            _branchAccCode = this._accountBL.GetBranchAccountCodeByID(_prmDSExcel.Tables[0].Rows[i][2].ToString());
                        }

                        bool _accountExist = _accountBL.IsExist(_account.Trim());
                        if (_accountExist == true)
                        {
                            _accError.Add(_account);
                            _error = "Account is Exists";
                        }
                        //foreach (MsAccount _row in this._accountBL.GetListAccount())
                        //{
                        //    if (_row.Account == _account)
                        //    {
                        //        _accError.Add(_accClassCode);
                        //        _error = "Account is Exists";
                        //    }
                        //}

                        if (_error == "")
                        {
                            _msAccountList.Add(new MsAccount(_account, _accountName, _branchAccCode, _accClassCode, _detail, _fgSubled, _fgNormal, _currCode, _fgActive, _userID, _userDate, _userClose, _closeDate));
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

                bool _result = this._accountBL.AddAccountList(_msAccountList);

                if (_result == true)
                {
                    _error = _msAccountList.Count + " row(s) of data has been successfully imported.<p />";
                }

                foreach (var _accClass in _accClassError)
                {
                    _accClassAll += _accClass + ", ";
                }

                foreach (var _curr in _currError)
                {
                    _currAll += _curr + ", ";
                }

                foreach (var _branchAccount in _branchAccountError)
                {
                    _branchAccAll += _branchAccount + ", ";
                }

                foreach (var _acc in _accError)
                {
                    _accAll += _acc + ", ";
                }

                if (_accAll != "")
                {
                    _error += "Duplicate Account : <br />" + _accAll + "<br />";
                }

                if (_accClassAll != "")
                {
                    _error += "Account Class Code is not in Master Account Class : <br />" + _accClassAll + "<br />";
                }

                if (_currAll != "")
                {
                    _error += "Currency Code is not in Master Currency : <br />" + _currAll + "<br />";
                }

                if (_branchAccAll != "")
                {
                    _error += "Branch Account Code is not in Master Branch Account : <br />" + _branchAccAll + "<br />";
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
