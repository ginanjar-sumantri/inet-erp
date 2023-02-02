using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using VTSWeb.BusinessRule;
using VTSWeb.Database;
using VTSWeb.SystemConfig;
using VTSWeb.Common;
using VTSWeb.DataMapping;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;



namespace VTSWeb.UI
{
    public partial class GoodsInOutDetail : GoodsInOutBase
    {
        private GoodsInOutBL _GoodsInOutBL = new GoodsInOutBL();
        private MsCustomerBL _customerBL = new MsCustomerBL();
        private TrGoodListBL _TrGoodListBL = new TrGoodListBL();
        private PrintPreviewBL _printPreviewBL = new PrintPreviewBL();
        private MsRackServerBL _rackServerBL = new MsRackServerBL();

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";
        private int?[] _navMark = { null, null, null, null };
        private string _currPageKey = "CurrentPage";

        private String _reportPath0 = "ReportGoodsInOut/PreviewGoodsIn.rdlc";


        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
            if (cookie == null)
            {
                Response.Redirect("..\\Login.aspx");
            }

            if (!this.Page.IsPostBack == true)
            {

                //GoodsInOutHd _GoodsInOutHd = this._GoodsInOutBL.GetSingleGoodsInOut(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.ViewState[this._currPageKey] = 0;

                //this.AddButton.ImageUrl = "../images/add.jpg";
                this.EditButton.ImageUrl = "../images/edit2.jpg";
                this.DeleteButton.ImageUrl = "../images/delete.jpg";
                this.BackButton.ImageUrl = "../images/back.jpg";
                this.CompleteButton.ImageUrl = "../images/Complete.jpg";
                this.SaveButton.ImageUrl = "../images/Save.jpg";
                this.ResetButton.ImageUrl = "../images/Reset.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";

                this.SaveButton2.ImageUrl = "../images/Save.jpg";
                this.DeleteButton2.ImageUrl = "../images/delete.jpg";
                this.CompleteButton2.ImageUrl = "../images/Complete.jpg";

                this.ClearData();
                this.ClearLabel();
                this.ShowDataHd(0);
                this.ShowDataTrGood(0);
                this.ShowDataPreview(0);
                this.Panel1.Visible = false;
                this.PanelPrintIn.Visible = false;

                if (this.TransactionTypeTextBox.Text == "In")
                {
                    this.Panel1.Visible = true;
                    this.Panel2.Visible = false;
                    this.CompleteButton.Visible = true;
                    this.CompleteButton2.Visible = false;

                }
                if (this.TransactionTypeTextBox.Text == "Out")
                {
                    this.CompleteButton.Visible = false;

                }

                if (this.StatusHiddenField.Value == "1")
                {
                    this.EditButton.Visible = false;
                    this.CompleteButton.Visible = false;
                    this.CompleteButton2.Visible = false;
                    this.DeleteButton.Visible = false;
                    this.Panel1.Visible = false;
                    this.Panel2.Visible = false;
                }
                else
                {
                    this.EditButton.Visible = true;
                    //this.CompleteButton.Visible = true;
                }
            }
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

        public void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {

            this.ProductNameTextBox.Text = "";
            this.SerialNumberTextBox.Text = "";
            this.RemarkTextBox.Text = "";
            this.ElectriCityTextBox.Text = "";
        }

        public void ShowDataHd(Int32 _prmCurrentPage)
        {
            this._GoodsInOutBL = new GoodsInOutBL();
            GoodsInOutHd _GoodsInOutHd = this._GoodsInOutBL.GetSingleHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.NumberTransTextBox.Text = _GoodsInOutHd.TransNmbr;
            this.StatusLabel.Text = ClearanceCompleteDataMapping.GetStatus(_GoodsInOutHd.Status);
            this.StatusHiddenField.Value = Convert.ToString(_GoodsInOutHd.Status);
            this.NumberFileTextBox.Text = _GoodsInOutHd.FileNmbr;
            this.TransactionTypeTextBox.Text = _GoodsInOutHd.TransType;
            this.CompanyNameTextBox.Text = (_customerBL.GetSingle(_GoodsInOutHd.CustCode).CustName);
            this.CustCodeHiddenField.Value = _GoodsInOutHd.CustCode;
            this.TransactionDateTextBox.Text = DateFormMapping.GetValue(_GoodsInOutHd.TransDate);
            this.HHTrTextBox.Text = Convert.ToDateTime(_GoodsInOutHd.TransDate).Hour.ToString();
            this.MMTrTextBox.Text = Convert.ToDateTime(_GoodsInOutHd.TransDate).Minute.ToString();
            this.CarryByTextBox.Text = _GoodsInOutHd.CarryBy;
            this.RequestedByTextBox.Text = _GoodsInOutHd.RequestedBy;
            this.ApprovedByTextBox.Text = _GoodsInOutHd.ApprovedBy;
            this.PostedByLabel.Text = _GoodsInOutHd.PostedBy;
            this.EntryDateTextBox.Text = DateFormMapping.GetValue(_GoodsInOutHd.EntryDate);
            this.RackName.Text = this._rackServerBL.GetRackNameByCode(_GoodsInOutHd.RackCode);
            this.HHEntryDateTextBox.Text = Convert.ToDateTime(_GoodsInOutHd.EntryDate).Hour.ToString();
            this.MMEntryDateTextBox.Text = Convert.ToDateTime(_GoodsInOutHd.EntryDate).Minute.ToString();
            this.EntryUserNameLabel.Text = _GoodsInOutHd.EntryUserName;

            this.EditDateTextBox.Text = DateFormMapping.GetValue(_GoodsInOutHd.EditDate);
            this.HHEditDateTextBox.Text = Convert.ToDateTime(_GoodsInOutHd.EditDate).Hour.ToString();
            this.MMEditDateTextBox.Text = Convert.ToDateTime(_GoodsInOutHd.EditDate).Minute.ToString();
            this.EditUserNameLabel.Text = _GoodsInOutHd.EditUserName;

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.TempHidden.Value = "";

            this._page = _prmCurrentPage;

            this.ListRepeater.DataSource = this._GoodsInOutBL.GetListDt(this.NumberTransTextBox.Text);
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.AllCheckBox.Checked = this.IsCheckedAll();

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");
            //this.Panel2.Visible = false;
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
        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                GoodsInOutDt _temp = (GoodsInOutDt)e.Item.DataItem;
                string _code = _temp.TransNmbr.ToString();
                string _ItemNo = _temp.ItemNo.ToString();
                string _all = _temp.TransNmbr.ToString() + "-" + _temp.ItemNo.ToString();

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _all;
                }
                else
                {
                    this.TempHidden.Value += "," + _all;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _all + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");
                _listCheckbox.Checked = this.IsChecked(_all);

                ImageButton _editButton2 = (ImageButton)e.Item.FindControl("EditButton2");
                _editButton2.PostBackUrl = this._detailEditPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_all, ApplicationConfig.EncryptionKey));
                _editButton2.ImageUrl = "../images/edit.jpg";
                //_editButton2.CommandArgument = _temp.AreaCode + "-" + _temp.PurposeCode;
                //_editButton2.CommandName = "EditDetailButton";

                if (this.StatusHiddenField.Value == "1")
                {
                    _editButton2.Visible = false;
                }
                if (this.TransactionTypeTextBox.Text == "Out")
                {
                    _editButton2.Visible = false;
                }

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
                _tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                _tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");


                Literal _ItemCodeLiteral = (Literal)e.Item.FindControl("ItemCodeLiteral");
                _ItemCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ItemCode);

                Literal _ProductNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                _ProductNameLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductName);

                Literal _SerialNumberLiteral = (Literal)e.Item.FindControl("SerialNumberLiteral");
                _SerialNumberLiteral.Text = HttpUtility.HtmlEncode(_temp.SerialNumber);

                Literal _RemarkLiteral = (Literal)e.Item.FindControl("RemarkLiteral");
                _RemarkLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);

                Literal _ElectriCityLiteral = (Literal)e.Item.FindControl("ElectriCityLiteral");
                _ElectriCityLiteral.Text = HttpUtility.HtmlEncode(_temp.ElectriCityNumerik);
            }

        }


        private Boolean IsCheckedAllTrGood()
        {
            Boolean _result = true;

            foreach (RepeaterItem _row in this.ListRepeaterTrGood.Items)
            {
                CheckBox _chk = (CheckBox)_row.FindControl("ListCheckBox");

                if (_chk.Checked == false)
                {
                    _result = false;
                    break;
                }
            }

            if (this.ListRepeaterTrGood.Items.Count == 0)
            {
                _result = false;
            }

            return _result;
        }

        private void ItemCode()
        {
            this.ItemCodeDDL.Items.Clear();
            this.ItemCodeDDL.DataTextField = "ItemCode";
            this.ItemCodeDDL.DataValueField = "ItemCode";
            this.ItemCodeDDL.DataSource = this._TrGoodListBL.GetItemCodeDDLForGoodsInOut(this.CustCodeHiddenField.Value);
            this.ItemCodeDDL.DataBind();
            this.ItemCodeDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowDataTrGood(Int32 _prmCurrentPage)
        {
            this.TempHiddenTrGood.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = _prmCurrentPage;

            this.ListRepeaterTrGood.DataSource = this._TrGoodListBL.GetList(this.CustCodeHiddenField.Value, this.ItemCodeDDL.SelectedValue);
            this.ListRepeaterTrGood.DataBind();

            this.AllCheckBoxTrGood.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHiddenTrGood.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.AllCheckBoxTrGood.Checked = this.IsCheckedAllTrGood();

            this.DeleteButton2.Attributes.Add("OnClick", "return AskYouFirst();");
            this.ItemCode();
        }
        private Boolean IsCheckedTrGood(String _prmValue)
        {
            Boolean _result = false;
            String[] _value = this.CheckHiddenTrGood.Value.Split(',');

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

        protected void ListRepeater_ItemDataBoundTrGood(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TrGoodList _temp = (TrGoodList)e.Item.DataItem;
                string _code = _temp.CustCode.ToString() + "-" + _temp.ItemCode.ToString();

                if (this.TempHiddenTrGood.Value == "")
                {
                    this.TempHiddenTrGood.Value = _code;
                }
                else
                {
                    this.TempHiddenTrGood.Value += "," + _code;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHiddenTrGood.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");
                _listCheckbox.Checked = this.IsCheckedTrGood(_code);

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
                _tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                _tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

                Literal _ItemCodeLiteral = (Literal)e.Item.FindControl("ItemCodeLiteral");
                _ItemCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ItemCode);

                Literal _ProductNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                _ProductNameLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductName);

                Literal _TransTypeLiteral = (Literal)e.Item.FindControl("SerialNumberLiteral");
                _TransTypeLiteral.Text = HttpUtility.HtmlEncode(_temp.SerialNumber);

                Literal _RemarkLiteral = (Literal)e.Item.FindControl("RemarkLiteral");
                _RemarkLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);

                Literal _ElectriCityLiteral = (Literal)e.Item.FindControl("ElectriCityLiteral");
                _ElectriCityLiteral.Text = HttpUtility.HtmlEncode(_temp.ElectriCityNumerik);


            }
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        //protected void AddButton_Click(object sender, ImageClickEventArgs e)
        //{
        //    this.Panel2.Visible = true;
        //    //Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        //}

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');

            bool _result = this._GoodsInOutBL.DeleteMultiDt(_tempSplit);

            this.ClearLabel();

            if (_result == true)
            {
                this.WarningDeleteLabel.Text = "Delete Success";
            }
            else
            {
                this.WarningDeleteLabel.Text = "Delete Failed";
            }

            this.CheckHidden.Value = "";
            this.AllCheckBox.Checked = false;

            this.ViewState[this._currPageKey] = 0;

            this.ShowDataHd(0);
        }
        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {

            GoodsInOutDt _GoodsInOutDt = new GoodsInOutDt();

            _GoodsInOutDt.TransNmbr = this.NumberTransTextBox.Text;
            _GoodsInOutDt.ItemNo = this._GoodsInOutBL.GetMaxItemNoByCode(this.NumberTransTextBox.Text) + 1;
            _GoodsInOutDt.CustCode = this.CustCodeHiddenField.Value;
            _GoodsInOutDt.ProductName = this.ProductNameTextBox.Text;
            _GoodsInOutDt.SerialNumber = this.SerialNumberTextBox.Text;
            _GoodsInOutDt.Remark = this.RemarkTextBox.Text;
            _GoodsInOutDt.ElectriCityNumerik = this.ElectriCityTextBox.Text;
            _GoodsInOutDt.ItemCode = this.NumberTransTextBox.Text + "." + _GoodsInOutDt.ItemNo;

            bool _result = this._GoodsInOutBL.AddDt(_GoodsInOutDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }

        }

        protected void ShowDataPreview(Int32 _prmCurrentPage)
        {
            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = _prmCurrentPage;

        }
        protected void CompleteButton_Click(object sender, ImageClickEventArgs e)
        {
            GoodsInOutHd _GoodsInOutHd = this._GoodsInOutBL.GetSingleHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            string _statusLabel = this.StatusLabel.Text = Convert.ToString(1);
            _GoodsInOutHd.Status = Convert.ToByte(_statusLabel);


            List<TrGoodList> _TrGoodList = new List<TrGoodList>();

            foreach (RepeaterItem _row in this.ListRepeater.Items)
            {
                Literal _ItemCodeLiteral = (Literal)_row.FindControl("ItemCodeLiteral");
                Literal _ProductNameLiteral = (Literal)_row.FindControl("ProductNameLiteral");
                Literal _SerialNumberLiteral = (Literal)_row.FindControl("SerialNumberLiteral");
                Literal _RemarkLiteral = (Literal)_row.FindControl("RemarkLiteral");
                Literal _ElectriCityLiteral = (Literal)_row.FindControl("ElectriCityLiteral");

                _TrGoodList.Add(new TrGoodList(this.CustCodeHiddenField.Value, _ItemCodeLiteral.Text, _ProductNameLiteral.Text,
                _SerialNumberLiteral.Text, _RemarkLiteral.Text, _ElectriCityLiteral.Text));
            }

            bool _result = this._GoodsInOutBL.Edit(_GoodsInOutHd);
            bool _resultTrGood = this._TrGoodListBL.Add(_TrGoodList);

            if (_result == true && _resultTrGood == true)
            {
                this.PanelHeader.Visible = false;
                this.PanelDetail.Visible = false;
                this.Panel1.Visible = false;
                this.Panel2.Visible = false;
                this.PanelPrintIn.Visible = true;
                this.ReportViewer1.Visible = true;

                ReportDataSource _reportDataSource1 = this._printPreviewBL.PrintPreviewGoodsIn(this.NumberTransTextBox.Text);
                //ReportDataSource _reportDataSource1 = this._reportDowntimeBL.ReportDowntimePerformanceDetailbyCircuit(this.CustomerDDL.SelectedValue, ((this.CircuitSelectRBL.SelectedValue == "All") ? "" : this.CircuitSelectDDL.SelectedValue), Convert.ToDateTime(this.DateFrom1TextBox.Text), Convert.ToDateTime(this.DateFrom2TextBox.Text));
                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;


                ReportParameter[] _reportParam = new ReportParameter[4];
                _reportParam[0] = new ReportParameter("TransNumb", this.NumberTransTextBox.Text, true);
                _reportParam[1] = new ReportParameter("ParamForm", "FORM GOODS IN/OUT ", true);
                _reportParam[2] = new ReportParameter("ParamCDC", "CYBER DATA CENTER", true);
                _reportParam[3] = new ReportParameter("ParamType", "Goods IN", true);

                this.ReportViewer1.LocalReport.SetParameters(_reportParam);
                this.ReportViewer1.LocalReport.Refresh();

            }
            else if (_result == true && _resultTrGood == false)
            {
                this.WarningLabel.Text = "You Failed Add Data, data already stored";
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }


        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();

        }
        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));

        }

        protected void ItemCodeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearLabel();
            this.ShowDataTrGood(0);
        }

        protected void SaveButton2_Click(object sender, ImageClickEventArgs e)
        {
            GoodsInOutDt _GoodsInOutDt = new GoodsInOutDt();

            _GoodsInOutDt.TransNmbr = this.NumberTransTextBox.Text;
            _GoodsInOutDt.ItemNo = this._GoodsInOutBL.GetMaxItemNoByCode(this.NumberTransTextBox.Text) + 1;
            _GoodsInOutDt.CustCode = this.CustCodeHiddenField.Value;


            foreach (RepeaterItem _row in this.ListRepeaterTrGood.Items)
            {
                Literal _ItemCodeLiteral = (Literal)_row.FindControl("ItemCodeLiteral");
                Literal _ProductNameLiteral = (Literal)_row.FindControl("ProductNameLiteral");
                Literal _SerialNumberLiteral = (Literal)_row.FindControl("SerialNumberLiteral");
                Literal _RemarkLiteral = (Literal)_row.FindControl("RemarkLiteral");
                Literal _ElectriCityLiteral = (Literal)_row.FindControl("ElectriCityLiteral");

                _GoodsInOutDt.ItemCode = _ItemCodeLiteral.Text;
                _GoodsInOutDt.ProductName = _ProductNameLiteral.Text; //this.ProductNameLiteral.;
                _GoodsInOutDt.SerialNumber = _SerialNumberLiteral.Text;//this.SerialNumberLiteral.Text;
                _GoodsInOutDt.Remark = _RemarkLiteral.Text;
                _GoodsInOutDt.ElectriCityNumerik = _ElectriCityLiteral.Text;
            }

            bool _result = this._GoodsInOutBL.AddDt(_GoodsInOutDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }

        }
        protected void DeleteButton2_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHiddenTrGood.Value;
            string[] _tempSplit = _temp.Split(',');

            bool _result = this._TrGoodListBL.DeleteMulti(_tempSplit);

            this.ClearLabel();

            if (_result == true)
            {
                this.WarningLabelTrGood.Text = "Delete Success";
            }
            else
            {
                this.WarningLabelTrGood.Text = "Delete Failed";
            }

            this.CheckHiddenTrGood.Value = "";
            this.AllCheckBoxTrGood.Checked = false;

            this.ViewState[this._currPageKey] = 0;

            this.ShowDataTrGood(0);
        }


        protected void CompleteButton2_Click(object sender, ImageClickEventArgs e)
        {
            GoodsInOutHd _GoodsInOutHd = this._GoodsInOutBL.GetSingleHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            string _statusLabel = this.StatusLabel.Text = Convert.ToString(1);
            _GoodsInOutHd.Status = Convert.ToByte(_statusLabel);

            bool _result = this._GoodsInOutBL.Edit(_GoodsInOutHd);

            if (_result == true)
            {
                this.PanelHeader.Visible = false;
                this.PanelDetail.Visible = false;
                this.Panel1.Visible = false;
                this.Panel2.Visible = false;
                this.PanelPrintIn.Visible = true;
                this.ReportViewer1.Visible = true;

                ReportDataSource _reportDataSource1 = this._printPreviewBL.PrintPreviewGoodsIn(this.NumberTransTextBox.Text);
                //ReportDataSource _reportDataSource1 = this._reportDowntimeBL.ReportDowntimePerformanceDetailbyCircuit(this.CustomerDDL.SelectedValue, ((this.CircuitSelectRBL.SelectedValue == "All") ? "" : this.CircuitSelectDDL.SelectedValue), Convert.ToDateTime(this.DateFrom1TextBox.Text), Convert.ToDateTime(this.DateFrom2TextBox.Text));
                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;

                ReportParameter[] _reportParam = new ReportParameter[4];
                _reportParam[0] = new ReportParameter("TransNumb", this.NumberTransTextBox.Text, true);
                _reportParam[1] = new ReportParameter("ParamForm", "FORM GOODS IN/OUT ", true);
                _reportParam[2] = new ReportParameter("ParamCDC", "CYBER DATA CENTER", true);
                _reportParam[3] = new ReportParameter("ParamType", "Goods Out", true);

                this.ReportViewer1.LocalReport.SetParameters(_reportParam);
                this.ReportViewer1.LocalReport.Refresh();

            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

    }
}