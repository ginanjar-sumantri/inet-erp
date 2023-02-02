using System;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.Company
{
    public partial class CompanyView : CompanyBase
    {
        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;
        private int?[] _navMark2 = { null, null, null, null };
        private bool _flag2 = true;
        private bool _nextFlag2 = false;
        private bool _lastFlag2 = false;
        private int?[] _navMark3 = { null, null, null, null };
        private bool _flag3 = true;
        private bool _nextFlag3 = false;
        private bool _lastFlag3 = false;
        private int?[] _navMark4 = { null, null, null, null };
        private bool _flag4 = true;
        private bool _nextFlag4 = false;
        private bool _lastFlag4 = false;
        private int?[] _navMark5 = { null, null, null, null };
        private bool _flag5 = true;
        private bool _nextFlag5 = false;
        private bool _lastFlag5 = false;

        private CompanyBL _companyBL = new CompanyBL();

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;

        private int _page2;
        private int _maxrow2 = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength2 = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no2 = 0;
        private int _nomor2 = 0;

        private int _page3;
        private int _maxrow3 = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength3 = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no3 = 0;
        private int _nomor3 = 0;

        private int _page4;
        private int _maxrow4 = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength4 = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no4 = 0;
        private int _nomor4 = 0;

        private int _page5;
        private int _maxrow5 = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength5 = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no5 = 0;
        private int _nomor5 = 0;

        private string _currPageKey = "CurrentPage";
        private string _currPageKey2 = "CurrentPage";
        private string _currPageKey3 = "CurrentPage";
        private string _currPageKey4 = "CurrentPage";
        private string _currPageKey5 = "CurrentPage";

        private string _app = "Aplikasi";

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _awal2 = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater2_ctl";
        private string _akhir2 = "_ListCheckBox2";
        private string _cbox2 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox2";
        private string _tempHidden2 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden2";

        private string _awal3 = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater3_ctl";
        private string _akhir3 = "_ListCheckBox3";
        private string _cbox3 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox3";
        private string _tempHidden3 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden3";

        private string _awal4 = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater4_ctl";
        private string _akhir4 = "_ListCheckBox4";
        private string _cbox4 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox4";
        private string _tempHidden4 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden4";

        private string _awal5 = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater5_ctl";
        private string _akhir5 = "_ListCheckBox5";
        private string _cbox5 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox5";
        private string _tempHidden5 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden5";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");
            this.DataPagerButton2.Attributes.Add("Style", "visibility: hidden;");
            this.DataPagerButton3.Attributes.Add("Style", "visibility: hidden;");
            this.DataPagerButton4.Attributes.Add("Style", "visibility: hidden;");
            this.DataPagerButton5.Attributes.Add("Style", "visibility: hidden;");

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.ViewState[this._currPageKey] = 0;
                this.ViewState[this._currPageKey2] = 0;
                this.ViewState[this._currPageKey3] = 0;
                this.ViewState[this._currPageKey4] = 0;
                this.ViewState[this._currPageKey5] = 0;

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditMenuButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2-1.jpg";
                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.UpdateReportTemplateButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/updateReportTemplate.jpg";
                this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.AddButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.AddButton3.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton3.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.GoImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/go.jpg";

                this.ClearLabel();
                this.ShowData(0);
                this.ShowData2(0);
                this.ShowData3(0);
                this.ShowData4(0);
                this.ShowData5(0);
            }
        }

        protected void ClearLabel()
        {
            this.Label1.Text = "";
            this.Label2.Text = "";
            this.Label3.Text = "";
            this.Label4.Text = "";
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._companyBL.RowsCountCompanyDatabase(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey)));
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
        }

        private double RowCount2()
        {
            double _result = 0;

            _result = this._companyBL.RowsCountCompanyUser(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey)));
            _result = System.Math.Ceiling(_result / (double)_maxrow2);

            return _result;
        }

        private double RowCount3()
        {
            double _result = 0;

            _result = this._companyBL.RowsCountCompanyRole(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey)));
            _result = System.Math.Ceiling(_result / (double)_maxrow3);

            return _result;
        }

        private double RowCount4()
        {
            double _result = 0;

            _result = this._companyBL.RowsCountReportList(this.CategoryDropDownList.SelectedValue, this.KeywordTextBox.Text, new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey)));
            _result = System.Math.Ceiling(_result / (double)_maxrow3);

            return _result;
        }

        private double RowCount5()
        {
            double _result = 0;

            _result = this._companyBL.RowsCountPrintPreview(this.CategoryDropDownList2.SelectedValue, this.KeywordTextBox2.Text, new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey)));
            _result = System.Math.Ceiling(_result / (double)_maxrow3);

            return _result;
        }

        private Boolean IsCheckedAll()
        {
            Boolean _result = true;

            foreach (RepeaterItem _row in this.ListRepeater.Items)
            {
                CheckBox _chk = (CheckBox)_row.FindControl("ListCheckBox");

                if (_chk.Checked == false)
                {
                    _result = false;
                    break;
                }
            }

            if (this.ListRepeater.Items.Count == 0)
            {
                _result = false;
            }

            return _result;
        }

        private Boolean IsCheckedAll2()
        {
            Boolean _result = true;

            foreach (RepeaterItem _row in this.ListRepeater2.Items)
            {
                CheckBox _chk = (CheckBox)_row.FindControl("ListCheckBox2");

                if (_chk.Checked == false)
                {
                    _result = false;
                    break;
                }
            }

            if (this.ListRepeater2.Items.Count == 0)
            {
                _result = false;
            }

            return _result;
        }

        private Boolean IsCheckedAll3()
        {
            Boolean _result = true;

            foreach (RepeaterItem _row in this.ListRepeater3.Items)
            {
                CheckBox _chk = (CheckBox)_row.FindControl("ListCheckBox3");

                if (_chk.Checked == false)
                {
                    _result = false;
                    break;
                }
            }

            if (this.ListRepeater3.Items.Count == 0)
            {
                _result = false;
            }

            return _result;
        }

        private void ShowPage(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCount();

            if (_prmCurrentPage - _maxlength > 0)
            {
                min = _prmCurrentPage - _maxlength;
            }
            else
            {
                min = 0;
            }

            if (_prmCurrentPage + _maxlength < q)
            {
                max = _prmCurrentPage + _maxlength + 1;
            }
            else
            {
                max = Convert.ToDecimal(q);
            }

            if (_prmCurrentPage > 0)
                _addElement += 2;

            if (_prmCurrentPage < q - 1)
                _addElement += 2;

            i = Convert.ToInt32(min);
            _pageNumber = new Int32[Convert.ToInt32(max - min) + _addElement];
            if (_pageNumber.Length != 0)
            {
                //NB: Prev Or First
                if (_prmCurrentPage > 0)
                {
                    this._navMark[0] = 0;

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[0]);
                    _pageNumberElement++;

                    this._navMark[1] = Convert.ToInt32(Math.Abs(_prmCurrentPage - 1));

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[1]);
                    _pageNumberElement++;
                }

                for (; i < max; i++)
                {
                    _pageNumber[_pageNumberElement] = i;
                    _pageNumberElement++;
                }

                if (_prmCurrentPage < q - 1)
                {
                    this._navMark[2] = Convert.ToInt32(_prmCurrentPage + 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[2]);
                    _pageNumberElement++;

                    if (_pageNumber[_pageNumberElement - 2] > _pageNumber[_pageNumberElement - 1])
                    {
                        this._flag = true;
                    }

                    this._navMark[3] = Convert.ToInt32(q - 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[3]);
                    _pageNumberElement++;
                }

                int?[] _navMarkBackup = new int?[4];
                this._navMark.CopyTo(_navMarkBackup, 0);
                this.DataPagerTopRepeater.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTopRepeater.DataBind();

                _flag = true;
                _nextFlag = false;
                _lastFlag = false;
                _navMark = _navMarkBackup;

                //this.DataPagerBottomRepeater.DataSource = from _query in _pageNumber
                //                                          select _query;
                //this.DataPagerBottomRepeater.DataBind();
            }
            else
            {
                this.DataPagerTopRepeater.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTopRepeater.DataBind();
            }
        }

        private void ShowPage2(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCount2();

            if (_prmCurrentPage - _maxlength2 > 0)
            {
                min = _prmCurrentPage - _maxlength2;
            }
            else
            {
                min = 0;
            }

            if (_prmCurrentPage + _maxlength2 < q)
            {
                max = _prmCurrentPage + _maxlength2 + 1;
            }
            else
            {
                max = Convert.ToDecimal(q);
            }

            if (_prmCurrentPage > 0)
                _addElement += 2;

            if (_prmCurrentPage < q - 1)
                _addElement += 2;

            i = Convert.ToInt32(min);
            _pageNumber = new Int32[Convert.ToInt32(max - min) + _addElement];
            if (_pageNumber.Length != 0)
            {
                //NB: Prev Or First
                if (_prmCurrentPage > 0)
                {
                    this._navMark2[0] = 0;

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[0]);
                    _pageNumberElement++;

                    this._navMark2[1] = Convert.ToInt32(Math.Abs(_prmCurrentPage - 1));

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[1]);
                    _pageNumberElement++;
                }

                for (; i < max; i++)
                {
                    _pageNumber[_pageNumberElement] = i;
                    _pageNumberElement++;
                }

                if (_prmCurrentPage < q - 1)
                {
                    this._navMark2[2] = Convert.ToInt32(_prmCurrentPage + 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[2]);
                    _pageNumberElement++;

                    if (_pageNumber[_pageNumberElement - 2] > _pageNumber[_pageNumberElement - 1])
                    {
                        this._flag2 = true;
                    }

                    this._navMark2[3] = Convert.ToInt32(q - 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[3]);
                    _pageNumberElement++;
                }

                int?[] _navMarkBackup = new int?[4];
                this._navMark2.CopyTo(_navMarkBackup, 0);
                this.DataPagerTopRepeater2.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater2.DataBind();

                _flag2 = true;
                _nextFlag2 = false;
                _lastFlag2 = false;
                _navMark2 = _navMarkBackup;

                //this.DataPagerBottomRepeater.DataSource = from _query in _pageNumber
                //                                          select _query;
                //this.DataPagerBottomRepeater.DataBind();
            }
            else
            {
                this.DataPagerTopRepeater2.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater2.DataBind();
            }
        }

        private void ShowPage3(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCount3();

            if (_prmCurrentPage - _maxlength3 > 0)
            {
                min = _prmCurrentPage - _maxlength3;
            }
            else
            {
                min = 0;
            }

            if (_prmCurrentPage + _maxlength3 < q)
            {
                max = _prmCurrentPage + _maxlength3 + 1;
            }
            else
            {
                max = Convert.ToDecimal(q);
            }

            if (_prmCurrentPage > 0)
                _addElement += 2;

            if (_prmCurrentPage < q - 1)
                _addElement += 2;

            i = Convert.ToInt32(min);
            _pageNumber = new Int32[Convert.ToInt32(max - min) + _addElement];
            if (_pageNumber.Length != 0)
            {
                //NB: Prev Or First
                if (_prmCurrentPage > 0)
                {
                    this._navMark3[0] = 0;

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark3[0]);
                    _pageNumberElement++;

                    this._navMark3[1] = Convert.ToInt32(Math.Abs(_prmCurrentPage - 1));

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark3[1]);
                    _pageNumberElement++;
                }

                for (; i < max; i++)
                {
                    _pageNumber[_pageNumberElement] = i;
                    _pageNumberElement++;
                }

                if (_prmCurrentPage < q - 1)
                {
                    this._navMark3[2] = Convert.ToInt32(_prmCurrentPage + 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark3[2]);
                    _pageNumberElement++;

                    if (_pageNumber[_pageNumberElement - 2] > _pageNumber[_pageNumberElement - 1])
                    {
                        this._flag3 = true;
                    }

                    this._navMark3[3] = Convert.ToInt32(q - 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark3[3]);
                    _pageNumberElement++;
                }

                int?[] _navMarkBackup = new int?[4];
                this._navMark3.CopyTo(_navMarkBackup, 0);
                this.DataPagerTopRepeater3.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater3.DataBind();

                _flag3 = true;
                _nextFlag3 = false;
                _lastFlag3 = false;
                _navMark3 = _navMarkBackup;

                //this.DataPagerBottomRepeater.DataSource = from _query in _pageNumber
                //                                          select _query;
                //this.DataPagerBottomRepeater.DataBind();
            }
            else
            {
                this.DataPagerTopRepeater3.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater3.DataBind();
            }
        }

        private void ShowPage4(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCount4();

            if (_prmCurrentPage - _maxlength4 > 0)
            {
                min = _prmCurrentPage - _maxlength4;
            }
            else
            {
                min = 0;
            }

            if (_prmCurrentPage + _maxlength4 < q)
            {
                max = _prmCurrentPage + _maxlength4 + 1;
            }
            else
            {
                max = Convert.ToDecimal(q);
            }

            if (_prmCurrentPage > 0)
                _addElement += 2;

            if (_prmCurrentPage < q - 1)
                _addElement += 2;

            i = Convert.ToInt32(min);
            _pageNumber = new Int32[Convert.ToInt32(max - min) + _addElement];
            if (_pageNumber.Length != 0)
            {
                //NB: Prev Or First
                if (_prmCurrentPage > 0)
                {
                    this._navMark4[0] = 0;

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark4[0]);
                    _pageNumberElement++;

                    this._navMark4[1] = Convert.ToInt32(Math.Abs(_prmCurrentPage - 1));

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark4[1]);
                    _pageNumberElement++;
                }

                for (; i < max; i++)
                {
                    _pageNumber[_pageNumberElement] = i;
                    _pageNumberElement++;
                }

                if (_prmCurrentPage < q - 1)
                {
                    this._navMark4[2] = Convert.ToInt32(_prmCurrentPage + 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark4[2]);
                    _pageNumberElement++;

                    if (_pageNumber[_pageNumberElement - 2] > _pageNumber[_pageNumberElement - 1])
                    {
                        this._flag4 = true;
                    }

                    this._navMark4[3] = Convert.ToInt32(q - 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark4[3]);
                    _pageNumberElement++;
                }

                int?[] _navMarkBackup = new int?[4];
                this._navMark4.CopyTo(_navMarkBackup, 0);
                this.DataPagerTopRepeater4.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater4.DataBind();

                _flag4 = true;
                _nextFlag4 = false;
                _lastFlag4 = false;
                _navMark4 = _navMarkBackup;

                //this.DataPagerBottomRepeater.DataSource = from _query in _pageNumber
                //                                          select _query;
                //this.DataPagerBottomRepeater.DataBind();
            }
            else
            {
                this.DataPagerTopRepeater4.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater4.DataBind();
            }
        }

        private void ShowPage5(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCount5();

            if (_prmCurrentPage - _maxlength5 > 0)
            {
                min = _prmCurrentPage - _maxlength5;
            }
            else
            {
                min = 0;
            }

            if (_prmCurrentPage + _maxlength5 < q)
            {
                max = _prmCurrentPage + _maxlength5 + 1;
            }
            else
            {
                max = Convert.ToDecimal(q);
            }

            if (_prmCurrentPage > 0)
                _addElement += 2;

            if (_prmCurrentPage < q - 1)
                _addElement += 2;

            i = Convert.ToInt32(min);
            _pageNumber = new Int32[Convert.ToInt32(max - min) + _addElement];
            if (_pageNumber.Length != 0)
            {
                //NB: Prev Or First
                if (_prmCurrentPage > 0)
                {
                    this._navMark5[0] = 0;

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark5[0]);
                    _pageNumberElement++;

                    this._navMark5[1] = Convert.ToInt32(Math.Abs(_prmCurrentPage - 1));

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark5[1]);
                    _pageNumberElement++;
                }

                for (; i < max; i++)
                {
                    _pageNumber[_pageNumberElement] = i;
                    _pageNumberElement++;
                }

                if (_prmCurrentPage < q - 1)
                {
                    this._navMark5[2] = Convert.ToInt32(_prmCurrentPage + 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark5[2]);
                    _pageNumberElement++;

                    if (_pageNumber[_pageNumberElement - 2] > _pageNumber[_pageNumberElement - 1])
                    {
                        this._flag5 = true;
                    }

                    this._navMark5[3] = Convert.ToInt32(q - 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark5[3]);
                    _pageNumberElement++;
                }

                int?[] _navMarkBackup = new int?[4];
                this._navMark5.CopyTo(_navMarkBackup, 0);
                this.DataPagerTopRepeater5.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater5.DataBind();

                _flag5 = true;
                _nextFlag5 = false;
                _lastFlag5 = false;
                _navMark5 = _navMarkBackup;

                //this.DataPagerBottomRepeater.DataSource = from _query in _pageNumber
                //                                          select _query;
                //this.DataPagerBottomRepeater.DataBind();
            }
            else
            {
                this.DataPagerTopRepeater5.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater5.DataBind();
            }
        }

        public void ShowData(Int32 _prmCurrentPage)
        {
            master_Company _master_Company = this._companyBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey));

            this.NameTextBox.Text = _master_Company.Name;
            this.LogoTextBox.Text = _master_Company.Logo;
            this.AddressTextBox.Text = _master_Company.PrimaryAddress;
            this.CompanyTagTextBox.Text = _master_Company.CompanyTag;
            this.TaxBranchNoTextBox.Text = _master_Company.TaxBranchNo;
            this.DefaultCheckBox.Checked = _master_Company.@default == null ? false : Convert.ToBoolean(_master_Company.@default);

            this.TempHidden.Value = "";

            this._page = _prmCurrentPage;

            this.ListRepeater.DataSource = this._companyBL.GetListCompanyDatabase(_prmCurrentPage, _maxrow, new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey)));
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.AllCheckBox.Checked = this.IsCheckedAll();

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "" && _nvcExtractor.GetValue(_app) == "Database")
            {
                this.Label1.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.ShowPage(_prmCurrentPage);
        }

        public void ShowData2(Int32 _prmCurrentPage)
        {
            this.TempHidden2.Value = "";

            this._page2 = _prmCurrentPage;

            this.ListRepeater2.DataSource = this._companyBL.GetListCompanyUser(_prmCurrentPage, _maxrow2, new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey)));
            this.ListRepeater2.DataBind();

            this.AllCheckBox2.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox2.ClientID + ", " + this.CheckHidden2.ClientID + ", '" + _awal2 + "', '" + _akhir2 + "', '" + _tempHidden2 + "' );");

            this.AllCheckBox2.Checked = this.IsCheckedAll2();

            this.DeleteButton2.Attributes.Add("OnClick", "return AskYouFirst();");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "" && _nvcExtractor.GetValue(_app) == "User")
            {
                this.Label2.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.ShowPage2(_prmCurrentPage);
        }

        public void ShowData3(Int32 _prmCurrentPage)
        {
            this.TempHidden3.Value = "";

            this._page3 = _prmCurrentPage;

            this.ListRepeater3.DataSource = this._companyBL.GetListCompanyRole(_prmCurrentPage, _maxrow3, new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey)));
            this.ListRepeater3.DataBind();

            this.AllCheckBox3.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox3.ClientID + ", " + this.CheckHidden3.ClientID + ", '" + _awal3 + "', '" + _akhir3 + "', '" + _tempHidden3 + "' );");

            this.AllCheckBox3.Checked = this.IsCheckedAll3();

            this.DeleteButton3.Attributes.Add("OnClick", "return AskYouFirst();");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "" && _nvcExtractor.GetValue(_app) == "Role")
            {
                this.Label3.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.ShowPage3(_prmCurrentPage);
        }

        public void ShowData4(Int32 _prmCurrentPage)
        {
            this.TempHidden4.Value = "";

            this._page4 = _prmCurrentPage;

            this.ListRepeater4.DataSource = this._companyBL.GetListReportList(_prmCurrentPage, _maxrow3, this.CategoryDropDownList.SelectedValue, this.KeywordTextBox.Text, new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey)));
            this.ListRepeater4.DataBind();

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "" && _nvcExtractor.GetValue(_app) == "ReportList")
            {
                this.Label4.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.ShowPage4(_prmCurrentPage);
        }

        public void ShowData5(Int32 _prmCurrentPage)
        {
            this.TempHidden5.Value = "";

            this._page5 = _prmCurrentPage;

            this.ListRepeater5.DataSource = this._companyBL.GetListPrintPreview(_prmCurrentPage, _maxrow3, this.CategoryDropDownList2.SelectedValue, this.KeywordTextBox2.Text, new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey)));
            this.ListRepeater5.DataBind();

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "" && _nvcExtractor.GetValue(_app) == "ReportList")
            {
                this.Label5.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.ShowPage5(_prmCurrentPage);
        }

        private Boolean IsChecked(String _prmValue)
        {
            Boolean _result = false;
            String[] _value = this.CheckHidden.Value.Split(',');

            for (int i = 0; i < _value.Length; i++)
            {
                if (_prmValue == _value[i])
                {
                    _result = true;
                    break;
                }
            }

            return _result;
        }

        private Boolean IsChecked2(String _prmValue)
        {
            Boolean _result = false;
            String[] _value = this.CheckHidden2.Value.Split(',');

            for (int i = 0; i < _value.Length; i++)
            {
                if (_prmValue == _value[i])
                {
                    _result = true;
                    break;
                }
            }

            return _result;
        }

        private Boolean IsChecked3(String _prmValue)
        {
            Boolean _result = false;
            String[] _value = this.CheckHidden3.Value.Split(',');

            for (int i = 0; i < _value.Length; i++)
            {
                if (_prmValue == _value[i])
                {
                    _result = true;
                    break;
                }
            }

            return _result;
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _compKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._compKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                master_Company_master_Database _temp = (master_Company_master_Database)e.Item.DataItem;
                string _code = _temp.DatabaseID.ToString();

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _code;
                }
                else
                {
                    this.TempHidden.Value += "," + _code;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");
                _listCheckbox.Checked = this.IsChecked(_code);

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
                _tableRow.Attributes.Add("OnMouseOver", "this.style.backgroundColor='" + this._rowColorHover + "';");
                if (e.Item.ItemType == ListItemType.Item)
                {
                    _tableRow.Attributes.Add("style", "background-color:" + this._rowColor);
                    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColor + "';");
                }
                if (e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    _tableRow.Attributes.Add("style", "background-color:" + this._rowColorAlternate);
                    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColorAlternate + "';");
                }

                Literal _name = (Literal)e.Item.FindControl("DatabaseLiteral");
                _name.Text = HttpUtility.HtmlEncode(_temp.Name);

                Literal _status = (Literal)e.Item.FindControl("StatusLiteral");
                _status.Text = ConnectionModeMapper.GetLabel(_temp.Status);
            }
        }

        protected void ListRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                master_Company_aspnet_User _temp = (master_Company_aspnet_User)e.Item.DataItem;
                string _code = _temp.UserID.ToString();
                string _code2 = _temp.CompanyID.ToString();

                if (this.TempHidden2.Value == "")
                {
                    this.TempHidden2.Value = _code;
                }
                else
                {
                    this.TempHidden2.Value += "," + _code;
                }

                Literal _noLiteral2 = (Literal)e.Item.FindControl("NoLiteral2");
                _no2 = _page2 * _maxrow2;
                _no2 += 1;
                _no2 = _nomor2 + _no2;
                _noLiteral2.Text = _no2.ToString();
                _nomor2 += 1;

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox2");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden2.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal2 + "', '" + _akhir2 + "', '" + _cbox2 + "')");
                _listCheckbox.Checked = this.IsChecked2(_code);

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate2");
                _tableRow.Attributes.Add("OnMouseOver", "this.style.backgroundColor='" + this._rowColorHover + "';");
                if (e.Item.ItemType == ListItemType.Item)
                {
                    _tableRow.Attributes.Add("style", "background-color:" + this._rowColor);
                    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColor + "';");
                }
                if (e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    _tableRow.Attributes.Add("style", "background-color:" + this._rowColorAlternate);
                    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColorAlternate + "';");
                }

                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                _viewButton.PostBackUrl = this._viewDetailPage2 + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._compKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code2, ApplicationConfig.EncryptionKey));
                _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                _editButton.PostBackUrl = this._editDetailPage2 + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._compKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code2, ApplicationConfig.EncryptionKey));
                _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";

                Literal _name = (Literal)e.Item.FindControl("UserLiteral");
                _name.Text = HttpUtility.HtmlEncode(_temp.Name);
            }
        }

        protected void ListRepeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                master_Company_aspnet_Role _temp = (master_Company_aspnet_Role)e.Item.DataItem;
                string _code = _temp.RoleId.ToString();
                string _code2 = _temp.CompanyID.ToString();

                if (this.TempHidden3.Value == "")
                {
                    this.TempHidden3.Value = _code;
                }
                else
                {
                    this.TempHidden3.Value += "," + _code;
                }

                Literal _noLiteral3 = (Literal)e.Item.FindControl("NoLiteral3");
                _no3 = _page3 * _maxrow3;
                _no3 += 1;
                _no3 = _nomor3 + _no3;
                _noLiteral3.Text = _no3.ToString();
                _nomor3 += 1;

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox3");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden3.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal3 + "', '" + _akhir3 + "', '" + _cbox3 + "')");
                _listCheckbox.Checked = this.IsChecked3(_code);

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate3");
                _tableRow.Attributes.Add("OnMouseOver", "this.style.backgroundColor='" + this._rowColorHover + "';");
                if (e.Item.ItemType == ListItemType.Item)
                {
                    _tableRow.Attributes.Add("style", "background-color:" + this._rowColor);
                    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColor + "';");
                }
                if (e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    _tableRow.Attributes.Add("style", "background-color:" + this._rowColorAlternate);
                    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColorAlternate + "';");
                }

                Literal _name = (Literal)e.Item.FindControl("RoleNameLiteral");
                _name.Text = HttpUtility.HtmlEncode(_temp.Name);
            }
        }

        protected void ListRepeater4_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            master_ReportList _temp = (master_ReportList)e.Item.DataItem;

            Literal _noLiteral4 = (Literal)e.Item.FindControl("NoLiteral4");
            _no4 = _page4 * _maxrow4;
            _no4 += 1;
            _no4 = _nomor4 + _no4;
            _noLiteral4.Text = _no4.ToString();
            _nomor4 += 1;

            Button _reportListBtn = (Button)e.Item.FindControl("ReportListButton");
            _reportListBtn.Text = _temp.fgActive == true ? "Deactivate" : "Activate";
            _reportListBtn.Width = 120;
            _reportListBtn.CommandName = "activateReportList";
            _reportListBtn.CommandArgument = _temp.ReportGroupID + ":" + _temp.ReportName;

            Literal _reportGroupLiteral = (Literal)e.Item.FindControl("ReportGroupLiteral");
            _reportGroupLiteral.Text = _temp.ReportGroupID;

            Literal _reportNameLiteral = (Literal)e.Item.FindControl("ReportNameLiteral");
            _reportNameLiteral.Text = _temp.ReportName;

        }

        protected void ListRepeater5_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            String _temp = (String)e.Item.DataItem;
            ReportListBL _reportListBL = new ReportListBL();

            Literal _noLiteral5 = (Literal)e.Item.FindControl("NoLiteral5");
            _no5 = _page5 * _maxrow5;
            _no5 += 1;
            _no5 = _nomor5 + _no5;
            _noLiteral5.Text = _no5.ToString();
            _nomor5 += 1;

            Literal _reportGroupIDLiteral = (Literal)e.Item.FindControl("ReportGroupIDLiteral");
            _reportGroupIDLiteral.Text = _temp;

            DropDownList _printPreviewSelection = (DropDownList)e.Item.FindControl("PrintPreviewSelection");
            _printPreviewSelection.DataSource = _reportListBL.GetListPrintPreviewSelection(_temp, new UserBL().ConnectionCompanyID(HttpContext.Current.User.Identity.Name));
            _printPreviewSelection.DataValueField = "ReportName";
            _printPreviewSelection.DataTextField = "ReportName";
            _printPreviewSelection.DataBind();
            
            _printPreviewSelection.SelectedValue = _reportListBL.GetActivePrintPreview(_temp, new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey)));

            Button _btnSave = (Button)e.Item.FindControl("BtnSave");
            _btnSave.CommandName = "save";
            _btnSave.Text = "Save";
            _btnSave.CommandArgument = _temp + ":" + _printPreviewSelection.SelectedValue;

        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addDetailPage + "?" + _compKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._compKey)));
        }

        protected void AddButton2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addDetailPage2 + "?" + _compKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._compKey)));
        }

        protected void AddButton3_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addDetailPage3 + "?" + _compKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._compKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');
            string _page = "0";

            this.ClearLabel();

            bool _result = this._companyBL.DeleteMultiCompanyDatabase(_tempSplit, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey));

            if (_result == true)
            {
                _error = "Delete Success";
            }
            else
            {
                _error = "Delete Failed";
            }

            this.CheckHidden.Value = "";
            this.AllCheckBox.Checked = false;

            Response.Redirect(this._viewPage + "?" + this._compKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._compKey)) + "&" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error + "&" + _app + "=" + "Database");
        }

        protected void DeleteButton2_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden2.Value;
            string[] _tempSplit = _temp.Split(',');
            string _page = "0";

            this.ClearLabel();

            bool _result = this._companyBL.DeleteMultiCompanyUser(_tempSplit, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey));

            if (_result == true)
            {
                _error = "Delete Success";
            }
            else
            {
                _error = "Delete Failed";
            }

            this.CheckHidden.Value = "";
            this.AllCheckBox2.Checked = false;

            Response.Redirect(this._viewPage + "?" + this._compKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._compKey)) + "&" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error + "&" + _app + "=" + "User");
        }

        protected void DeleteButton3_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden3.Value;
            string[] _tempSplit = _temp.Split(',');
            string _page = "0";

            this.ClearLabel();

            bool _result = this._companyBL.DeleteMultiCompanyRole(_tempSplit, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey));

            if (_result == true)
            {
                _error = "Delete Success";
            }
            else
            {
                _error = "Delete Failed";
            }

            this.CheckHidden.Value = "";
            this.AllCheckBox3.Checked = false;

            Response.Redirect(this._viewPage + "?" + this._compKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._compKey)) + "&" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error + "&" + _app + "=" + "Role");
        }

        protected void DataPagerTopRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowData(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void DataPagerTopRepeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey2] = Convert.ToInt32(e.CommandArgument);

                this.ShowData2(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void DataPagerTopRepeater3_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey3] = Convert.ToInt32(e.CommandArgument);

                this.ShowData3(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void DataPagerTopRepeater4_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey4] = Convert.ToInt32(e.CommandArgument);

                this.ShowData4(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void DataPagerTopRepeater5_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey5] = Convert.ToInt32(e.CommandArgument);

                this.ShowData5(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void DataPagerTopRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

                if (Convert.ToInt32(this.ViewState[this._currPageKey]) == _pageNumber)
                {
                    TextBox _pageNumberTextbox = (TextBox)e.Item.FindControl("PageNumberTextBox");

                    _pageNumberTextbox.Text = (_pageNumber + 1).ToString();
                    _pageNumberTextbox.MaxLength = 9;
                    _pageNumberTextbox.Visible = true;

                    _pageNumberTextbox.Attributes.Add("style", "text-align: center;");
                    _pageNumberTextbox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                }
                else
                {
                    LinkButton _pageNumberLinkButton = (LinkButton)e.Item.FindControl("PageNumberLinkButton");
                    _pageNumberLinkButton.CommandName = "DataPager";
                    _pageNumberLinkButton.CommandArgument = _pageNumber.ToString();

                    if (_pageNumber == this._navMark[0])
                    {
                        _pageNumberLinkButton.Text = "First";
                        this._navMark[0] = null;
                    }
                    else if (_pageNumber == this._navMark[1])
                    {
                        _pageNumberLinkButton.Text = "Prev";
                        this._navMark[1] = null;
                    }
                    else if (_pageNumber == this._navMark[2] && this._flag == false)
                    {
                        _pageNumberLinkButton.Text = "Next";
                        this._navMark[2] = null;
                        this._nextFlag = true;
                        this._flag = true;
                    }
                    else if (_pageNumber == this._navMark[3] && this._flag == true && this._nextFlag == true)
                    {
                        _pageNumberLinkButton.Text = "Last";
                        this._navMark[3] = null;
                        this._lastFlag = true;
                    }
                    else
                    {
                        if (this._lastFlag == false)
                        {
                            _pageNumberLinkButton.Text = (_pageNumber + 1).ToString();
                        }

                        if (_pageNumber == this._navMark[2] && this._flag == true)
                            this._flag = false;
                    }
                }
            }
        }

        protected void DataPagerTopRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

                if (Convert.ToInt32(this.ViewState[this._currPageKey2]) == _pageNumber)
                {
                    TextBox _pageNumberTextbox = (TextBox)e.Item.FindControl("PageNumberTextBox2");

                    _pageNumberTextbox.Text = (_pageNumber + 1).ToString();
                    _pageNumberTextbox.MaxLength = 9;
                    _pageNumberTextbox.Visible = true;

                    _pageNumberTextbox.Attributes.Add("style", "text-align: center;");
                    _pageNumberTextbox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                }
                else
                {
                    LinkButton _pageNumberLinkButton = (LinkButton)e.Item.FindControl("PageNumberLinkButton2");
                    _pageNumberLinkButton.CommandName = "DataPager";
                    _pageNumberLinkButton.CommandArgument = _pageNumber.ToString();

                    if (_pageNumber == this._navMark2[0])
                    {
                        _pageNumberLinkButton.Text = "First";
                        this._navMark2[0] = null;
                    }
                    else if (_pageNumber == this._navMark2[1])
                    {
                        _pageNumberLinkButton.Text = "Prev";
                        this._navMark2[1] = null;
                    }
                    else if (_pageNumber == this._navMark2[2] && this._flag2 == false)
                    {
                        _pageNumberLinkButton.Text = "Next";
                        this._navMark2[2] = null;
                        this._nextFlag2 = true;
                        this._flag2 = true;
                    }
                    else if (_pageNumber == this._navMark2[3] && this._flag2 == true && this._nextFlag2 == true)
                    {
                        _pageNumberLinkButton.Text = "Last";
                        this._navMark2[3] = null;
                        this._lastFlag2 = true;
                    }
                    else
                    {
                        if (this._lastFlag2 == false)
                        {
                            _pageNumberLinkButton.Text = (_pageNumber + 1).ToString();
                        }

                        if (_pageNumber == this._navMark2[2] && this._flag2 == true)
                            this._flag2 = false;
                    }
                }
            }
        }

        protected void DataPagerTopRepeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

                if (Convert.ToInt32(this.ViewState[this._currPageKey3]) == _pageNumber)
                {
                    TextBox _pageNumberTextbox = (TextBox)e.Item.FindControl("PageNumberTextBox3");

                    _pageNumberTextbox.Text = (_pageNumber + 1).ToString();
                    _pageNumberTextbox.MaxLength = 9;
                    _pageNumberTextbox.Visible = true;

                    _pageNumberTextbox.Attributes.Add("style", "text-align: center;");
                    _pageNumberTextbox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                }
                else
                {
                    LinkButton _pageNumberLinkButton = (LinkButton)e.Item.FindControl("PageNumberLinkButton3");
                    _pageNumberLinkButton.CommandName = "DataPager";
                    _pageNumberLinkButton.CommandArgument = _pageNumber.ToString();

                    if (_pageNumber == this._navMark3[0])
                    {
                        _pageNumberLinkButton.Text = "First";
                        this._navMark3[0] = null;
                    }
                    else if (_pageNumber == this._navMark3[1])
                    {
                        _pageNumberLinkButton.Text = "Prev";
                        this._navMark3[1] = null;
                    }
                    else if (_pageNumber == this._navMark3[2] && this._flag3 == false)
                    {
                        _pageNumberLinkButton.Text = "Next";
                        this._navMark3[2] = null;
                        this._nextFlag3 = true;
                        this._flag3 = true;
                    }
                    else if (_pageNumber == this._navMark3[3] && this._flag3 == true && this._nextFlag3 == true)
                    {
                        _pageNumberLinkButton.Text = "Last";
                        this._navMark3[3] = null;
                        this._lastFlag3 = true;
                    }
                    else
                    {
                        if (this._lastFlag3 == false)
                        {
                            _pageNumberLinkButton.Text = (_pageNumber + 1).ToString();
                        }

                        if (_pageNumber == this._navMark3[2] && this._flag3 == true)
                            this._flag3 = false;
                    }
                }
            }
        }

        protected void DataPagerTopRepeater4_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

                if (Convert.ToInt32(this.ViewState[this._currPageKey4]) == _pageNumber)
                {
                    TextBox _pageNumberTextbox = (TextBox)e.Item.FindControl("PageNumberTextBox4");

                    _pageNumberTextbox.Text = (_pageNumber + 1).ToString();
                    _pageNumberTextbox.MaxLength = 9;
                    _pageNumberTextbox.Visible = true;

                    _pageNumberTextbox.Attributes.Add("style", "text-align: center;");
                    _pageNumberTextbox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                }
                else
                {
                    LinkButton _pageNumberLinkButton = (LinkButton)e.Item.FindControl("PageNumberLinkButton4");
                    _pageNumberLinkButton.CommandName = "DataPager";
                    _pageNumberLinkButton.CommandArgument = _pageNumber.ToString();

                    if (_pageNumber == this._navMark4[0])
                    {
                        _pageNumberLinkButton.Text = "First";
                        this._navMark4[0] = null;
                    }
                    else if (_pageNumber == this._navMark4[1])
                    {
                        _pageNumberLinkButton.Text = "Prev";
                        this._navMark4[1] = null;
                    }
                    else if (_pageNumber == this._navMark4[2] && this._flag4 == false)
                    {
                        _pageNumberLinkButton.Text = "Next";
                        this._navMark4[2] = null;
                        this._nextFlag4 = true;
                        this._flag4 = true;
                    }
                    else if (_pageNumber == this._navMark4[3] && this._flag4 == true && this._nextFlag4 == true)
                    {
                        _pageNumberLinkButton.Text = "Last";
                        this._navMark4[3] = null;
                        this._lastFlag4 = true;
                    }
                    else
                    {
                        if (this._lastFlag4 == false)
                        {
                            _pageNumberLinkButton.Text = (_pageNumber + 1).ToString();
                        }

                        if (_pageNumber == this._navMark4[2] && this._flag4 == true)
                            this._flag4 = false;
                    }
                }
            }
        }

        protected void DataPagerTopRepeater5_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

                if (Convert.ToInt32(this.ViewState[this._currPageKey5]) == _pageNumber)
                {
                    TextBox _pageNumberTextbox = (TextBox)e.Item.FindControl("PageNumberTextBox5");

                    _pageNumberTextbox.Text = (_pageNumber + 1).ToString();
                    _pageNumberTextbox.MaxLength = 9;
                    _pageNumberTextbox.Visible = true;

                    _pageNumberTextbox.Attributes.Add("style", "text-align: center;");
                    _pageNumberTextbox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                }
                else
                {
                    LinkButton _pageNumberLinkButton = (LinkButton)e.Item.FindControl("PageNumberLinkButton5");
                    _pageNumberLinkButton.CommandName = "DataPager";
                    _pageNumberLinkButton.CommandArgument = _pageNumber.ToString();

                    if (_pageNumber == this._navMark5[0])
                    {
                        _pageNumberLinkButton.Text = "First";
                        this._navMark5[0] = null;
                    }
                    else if (_pageNumber == this._navMark5[1])
                    {
                        _pageNumberLinkButton.Text = "Prev";
                        this._navMark5[1] = null;
                    }
                    else if (_pageNumber == this._navMark5[2] && this._flag5 == false)
                    {
                        _pageNumberLinkButton.Text = "Next";
                        this._navMark5[2] = null;
                        this._nextFlag5 = true;
                        this._flag5 = true;
                    }
                    else if (_pageNumber == this._navMark5[3] && this._flag5 == true && this._nextFlag5 == true)
                    {
                        _pageNumberLinkButton.Text = "Last";
                        this._navMark5[3] = null;
                        this._lastFlag5 = true;
                    }
                    else
                    {
                        if (this._lastFlag5 == false)
                        {
                            _pageNumberLinkButton.Text = (_pageNumber + 1).ToString();
                        }

                        if (_pageNumber == this._navMark5[2] && this._flag5 == true)
                            this._flag5 = false;
                    }
                }
            }
        }

        protected void DataPagerButton_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTopRepeater.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCount())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCount().ToString();
                        _reqPage = Convert.ToInt32(this.RowCount()) - 1;
                        break;
                    }
                    else if (_reqPage < 0)
                    {
                        ((TextBox)_item.Controls[3]).Text = "1";
                        _reqPage = 0;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            this.ViewState[this._currPageKey] = _reqPage;

            this.ShowData(_reqPage);
        }

        protected void DataPagerButton2_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTopRepeater2.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCount2())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCount2().ToString();
                        _reqPage = Convert.ToInt32(this.RowCount2()) - 1;
                        break;
                    }
                    else if (_reqPage < 0)
                    {
                        ((TextBox)_item.Controls[3]).Text = "1";
                        _reqPage = 0;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            this.ViewState[this._currPageKey2] = _reqPage;

            this.ShowData2(_reqPage);
        }

        protected void DataPagerButton3_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTopRepeater3.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCount3())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCount3().ToString();
                        _reqPage = Convert.ToInt32(this.RowCount3()) - 1;
                        break;
                    }
                    else if (_reqPage < 0)
                    {
                        ((TextBox)_item.Controls[3]).Text = "1";
                        _reqPage = 0;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            this.ViewState[this._currPageKey3] = _reqPage;

            this.ShowData3(_reqPage);
        }

        protected void DataPagerButton4_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTopRepeater4.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCount4())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCount4().ToString();
                        _reqPage = Convert.ToInt32(this.RowCount4()) - 1;
                        break;
                    }
                    else if (_reqPage < 0)
                    {
                        ((TextBox)_item.Controls[3]).Text = "1";
                        _reqPage = 0;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            this.ViewState[this._currPageKey4] = _reqPage;

            this.ShowData4(_reqPage);
        }

        protected void DataPagerButton5_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTopRepeater5.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCount5())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCount5().ToString();
                        _reqPage = Convert.ToInt32(this.RowCount5()) - 1;
                        break;
                    }
                    else if (_reqPage < 0)
                    {
                        ((TextBox)_item.Controls[3]).Text = "1";
                        _reqPage = 0;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            this.ViewState[this._currPageKey5] = _reqPage;

            this.ShowData5(_reqPage);
        }

        protected void ListRepeater4_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "activateReportList")
            {
                ReportListBL _reportListBL = new ReportListBL();
                String[] _paramReportList = e.CommandArgument.ToString().Split(':');
                _reportListBL.ActivateReportList(_paramReportList[0], _paramReportList[1], new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey)));
                this.ShowData4((int)this.ViewState[this._currPageKey4]);
            }
        }

        protected void ListRepeater5_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "save")
            {
                DropDownList _printPreviewSelection = (DropDownList)e.Item.FindControl("PrintPreviewSelection");
                ReportListBL _reportListBL = new ReportListBL();
                String[] _paramReportList = e.CommandArgument.ToString().Split(':');
                _reportListBL.UpdateActivePrintPreview(_paramReportList[0], _printPreviewSelection.SelectedValue, new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey)));
                this.ShowData5((int)this.ViewState[this._currPageKey5]);
            }
        }

        protected void UpdateReportTemplateButton_Click(object sender, ImageClickEventArgs e)
        {
            _companyBL.UpdateReportTemplate(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey)));
            this.ShowData4(0);
            this.ShowData5(0);
        }

        protected void EditMenuButton_Click1(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("CompanyEditMenu.aspx" + "?" + _compKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._compKey)));
        }

        protected void GoImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.ClearLabel();
            this.ShowData4(0);
        }
        protected void GoImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.ClearLabel();
            this.ShowData5(0);
        }
}
}