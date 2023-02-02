using System;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PettyCash
{
    public partial class PettyCashPrintPreviewDetail : PettyCashBase
    {
        private PettyBL _pettyBL = new PettyBL();
        private PermissionBL _permBL = new PermissionBL();

        private int _no = 0;
        private int _nomor = 0;
        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private decimal _amount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permPrintPreview = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.PrintPreview);

            if (this._permPrintPreview == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;
                this.ClearLabel();
                this.ShowData();
                this.ShowDetail();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowPage()
        {
            double q = this._pettyBL.RowsCountCashDt(this.TransactionNoTextBox.Text);

            decimal min = 0, max = 0;
            q = System.Math.Ceiling(q / (double)_maxrow);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (_page - _maxlength > 0)
            {
                min = _page - _maxlength;
            }
            else
            {
                min = 0;
            }

            if (_page + _maxlength < q)
            {
                max = _page + _maxlength + 1;
            }
            else
            {
                max = Convert.ToDecimal(q);
            }

            decimal i = min;

            if (min > 0)
            {
                //sb.Append("<a href='" + this._detailPage + "?" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt((i - 1).ToString(), ApplicationConfig.EncryptionKey)) + "'>" + ("<<< ") + "</a>&nbsp;");
                sb.Append("<a href='" + this._printPreviewPageDetail + "?" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(i.ToString(), ApplicationConfig.EncryptionKey)) + "&" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransactionNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "'>" + ("<<< ") + "</a>&nbsp;");
            }

            for (; i < max; i++)
            {
                if (i == _page)
                {
                    sb.Append("[<b>" + (i + 1) + "</b>]&nbsp;");
                }
                else
                {
                    //sb.Append("<a href='" + this._detailPage + "?" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(i.ToString(), ApplicationConfig.EncryptionKey)) + "'>" + (i + 1) + "</a>&nbsp;");
                    sb.Append("<a href='" + this._printPreviewPageDetail + "?" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(i.ToString(), ApplicationConfig.EncryptionKey)) + "&" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransactionNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "'>" + (i + 1) + "</a>&nbsp;");
                }
            }

            if (max < (decimal)q)
            {
                sb.Append("<a href='" + this._printPreviewPageDetail + "?" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(i.ToString(), ApplicationConfig.EncryptionKey)) + "&" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransactionNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "'>" + (" >>>") + "</a>&nbsp;");
            }

            this.PageLabel.Text = sb.ToString();
        }

        public void ShowData()
        {
            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            _page = Convert.ToInt32((_nvcExtractor.GetValue(ApplicationConfig.RequestPageKey) == "") ? "0" : Rijndael.Decrypt(_nvcExtractor.GetValue(ApplicationConfig.RequestPageKey), ApplicationConfig.EncryptionKey));
            FINPettyHd _finPettyHd = this._pettyBL.GetSingleHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            //DateFormat _dateFormat = new DateFormat();
            MsPetty _msPetty = new MsPetty();
            _msPetty = this._pettyBL.GetSingle(_finPettyHd.Petty);

            this.TransactionNoTextBox.Text = _finPettyHd.TransNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_finPettyHd.TransDate);
            this.PayToText.Text = _finPettyHd.PayTo;
            this.RemarkTextBox.Text = _finPettyHd.Remark;
            this.PettyTextBox.Text = _msPetty.PettyName;
            this.CurrencyTextBox.Text = _finPettyHd.CurrCode;
            this.RateTextBox.Text = _finPettyHd.ForexRate.ToString("#,###.##");
            this.StatusTextBox.Text = PettyCashDataMapper.GetStatusText(_finPettyHd.Status);
            this.StatusHiddenField.Value = _finPettyHd.Status.ToString();


        }

        public void ShowDetail()
        {

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);
            _page = Convert.ToInt32((_nvcExtractor.GetValue(ApplicationConfig.RequestPageKey) == "") ? "0" : Rijndael.Decrypt(_nvcExtractor.GetValue(ApplicationConfig.RequestPageKey), ApplicationConfig.EncryptionKey));

            this.ListRepeater.DataSource = this._pettyBL.GetListDt(this.TransactionNoTextBox.Text, _page, _maxrow);
            this.ListRepeater.DataBind();

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.ShowPage();
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                FINPettyDt _temp = (FINPettyDt)e.Item.DataItem;

                string _itemNo = _temp.ItemNo.ToString();

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

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

                Literal _account = (Literal)e.Item.FindControl("AccountLiteral");
                _account.Text = HttpUtility.HtmlEncode(_temp.Account);

                Literal _fgSubled = (Literal)e.Item.FindControl("FGSubledLiteral");
                _fgSubled.Text = HttpUtility.HtmlEncode(_temp.FgSubLed.ToString());

                Literal _subLedger = (Literal)e.Item.FindControl("SubLedgerLiteral");
                _subLedger.Text = HttpUtility.HtmlEncode(_temp.SubledName);

                Literal _remark = (Literal)e.Item.FindControl("RemarkLiteral");
                _remark.Text = HttpUtility.HtmlEncode(_temp.Remark);

                Literal _forex = (Literal)e.Item.FindControl("ForexRateLiteral");
                _forex.Text = HttpUtility.HtmlEncode(_temp.AmountForex.ToString("#,###.##"));

                _amount = _amount + _temp.AmountForex;
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label _amountLabel = (Label)e.Item.FindControl("AmountLabel");
                _amountLabel.Text = HttpUtility.HtmlEncode(_amount.ToString("#,###.##"));
            }
        }
    }
}