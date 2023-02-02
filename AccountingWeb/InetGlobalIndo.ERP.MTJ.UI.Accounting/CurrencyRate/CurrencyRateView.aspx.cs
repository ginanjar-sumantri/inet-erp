using System;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.CurrencyRate
{
    public partial class CurrencyRateView : CurrencyRateBase
    {
        private CurrencyRateBL _currRateBL = new CurrencyRateBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currBL = new CurrencyBL();

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _no = 0;
        private int _nomor = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.CancelButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowData();
            }
        }

        private void ShowData()
        {
            this._page = Convert.ToInt32((_nvcExtractor.GetValue(ApplicationConfig.RequestPageKey) == "") ? "0" : Rijndael.Decrypt(_nvcExtractor.GetValue(ApplicationConfig.RequestPageKey), ApplicationConfig.EncryptionKey));

            this.ListRepeater.DataSource = _currRateBL.GetListCurrRate(_page, _maxrow, Rijndael.Decrypt(_nvcExtractor.GetValue(_codeKey), ApplicationConfig.EncryptionKey));
            this.ListRepeater.DataBind();

            this.ShowPage();
        }

        private void ShowPage()
        {
            double q = this._currRateBL.RowsCountCurrRate(Rijndael.Decrypt(_nvcExtractor.GetValue(_codeKey), ApplicationConfig.EncryptionKey));

            q = System.Math.Ceiling(q / _maxrow);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < q; i++)
            {
                if (_page == i)
                {
                    sb.Append("[<b>" + (i + 1) + "</b>]&nbsp;");
                }
                else
                {
                    sb.Append("<a href='" + this._viewPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(_nvcExtractor.GetValue(_codeKey)) + "&" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(i.ToString(), ApplicationConfig.EncryptionKey)) + "'>" + (i + 1) + "</a>&nbsp;");
                }
            }

            this.PageLabel.Text = sb.ToString();
            this.PageLabel2.Text = sb.ToString();
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                MsCurrRate _temp = (MsCurrRate)e.Item.DataItem;

                byte _decimalPlace = _currBL.GetDecimalPlace(_temp.CurrCode);

                Literal _dateLiteral = (Literal)e.Item.FindControl("DateLiteral");
                _dateLiteral.Text = DateFormMapper.GetValue(_temp.CurrDate);

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                string _tempCurrCode = HttpUtility.HtmlEncode(_temp.CurrCode);
                Literal _currCode;
                _currCode = (Literal)e.Item.FindControl("CurrCodeLiteral");
                _currCode.Text = _tempCurrCode;

                Literal _currRate = (Literal)e.Item.FindControl("CurrRateLiteral");
                _currRate.Text = _temp.CurrRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

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
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_homePage);
        }
    }
}