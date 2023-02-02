using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Home.Term
{
    public partial class TermView : TermBase
    {
        private TermBL _termBL = new TermBL();
        private PermissionBL _permBL = new PermissionBL();

        private int _no = 0;

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

                this.ShowDropdownlist();

                this.ShowData();
                this.ShowDetail();
            }
        }

        protected void ShowData()
        {
            MsTerm _msTerm = this._termBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TermCodeTextBox.Text = _msTerm.TermCode;
            this.TermNameTextBox.Text = _msTerm.TermName;
            this.PeriodTextBox.Text = _msTerm.XPeriod.ToString();
            this.TypeRangeDropDownList.SelectedValue = _msTerm.TypeRange;
            this.RangeTextBox.Text = _msTerm.XRange.ToString();
        }

        private void ShowDropdownlist()
        {
            this.TypeRangeDropDownList.DataSource = this._termBL.GetList();
            this.TypeRangeDropDownList.DataValueField = "TypeRange";
            this.TypeRangeDropDownList.DataTextField = "TypeRange";
            this.TypeRangeDropDownList.DataBind();
            this.TypeRangeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        public void ShowDetail()
        {
            this.TempHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.ListRepeater.DataSource = this._termBL.GetListTermDt(this.TermCodeTextBox.Text);
            this.ListRepeater.DataBind();
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                MsTermDt _termDt = (MsTermDt)e.Item.DataItem;

                string _code = _termDt.TermCode.ToString();
                string _termCode = _termDt.Period.ToString();

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _termCode;
                }
                else
                {
                    this.TempHidden.Value += "," + _termCode;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no += 1;
                _noLiteral.Text = _no.ToString();

                string _urlEditDetail = _editDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + _termCodeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_termCode, ApplicationConfig.EncryptionKey));
                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                _editButton.PostBackUrl = _urlEditDetail;
                _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";

                this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                if (this._permEdit == PermissionLevel.NoAccess)
                {
                    _editButton.Visible = false;
                }

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterListTemplate");
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

                Literal _periodLiteral = (Literal)e.Item.FindControl("PeriodLiteral");
                _periodLiteral.Text = _termDt.Period.ToString();

                Literal _typeRangeLiteral = (Literal)e.Item.FindControl("TypeRangeLiteral");
                _typeRangeLiteral.Text = HttpUtility.HtmlEncode(_termDt.TypeRange);

                Literal _rangeLiteral = (Literal)e.Item.FindControl("RangeLiteral");
                _rangeLiteral.Text = _termDt.XRange.ToString();

                Literal _percentBaseLiteral = (Literal)e.Item.FindControl("PercentBaseLiteral");
                _percentBaseLiteral.Text = _termDt.PercentBase.ToString("#,##0.##");

                Literal _percentPPnLiteral = (Literal)e.Item.FindControl("PercentPPnLiteral");
                _percentPPnLiteral.Text = _termDt.PercentPPn.ToString("#,##0.##");
            }
        }
    }
}