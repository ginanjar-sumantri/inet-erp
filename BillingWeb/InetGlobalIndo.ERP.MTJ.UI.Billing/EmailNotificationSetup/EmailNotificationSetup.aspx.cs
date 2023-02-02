using System;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.EmailNotificationSetup
{
    public partial class EmailNotificationSetup : EmailNotificationSetupBase
    {
        private EmailNotificationSetupBL _emailSetupBL = new EmailNotificationSetupBL();

        private int _no = 0;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowData()
        {
            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.ListRepeater.DataSource = this._emailSetupBL.GetList(this.TypeDropDownList.SelectedValue);
            this.ListRepeater.DataBind();

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                BILMsEmailNotificationSetup _temp = (BILMsEmailNotificationSetup)e.Item.DataItem;
                string _code = _temp.ID.ToString();

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no += 1;
                _noLiteral.Text = _no.ToString();

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

                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewImageButton");
                _viewButton.PostBackUrl = this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditImageButton");
                _editButton.PostBackUrl = this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";

                Literal _typeLiteral = (Literal)e.Item.FindControl("TypeLiteral");
                _typeLiteral.Text = HttpUtility.HtmlEncode(EmailNotificationDataMapper.GetTypeText(_temp.NotificationType));

                Literal _subTypeLiteral = (Literal)e.Item.FindControl("SubTypeLiteral");
                _subTypeLiteral.Text = HttpUtility.HtmlEncode(EmailNotificationDataMapper.GetIDText(_temp.ID));

                Literal _emailFromLiteral = (Literal)e.Item.FindControl("EmailFromLiteral");
                _emailFromLiteral.Text = HttpUtility.HtmlEncode(_temp.EmailFrom);

                Literal _emailToLiteral = (Literal)e.Item.FindControl("EmailToLiteral");
                _emailToLiteral.Text = HttpUtility.HtmlEncode(_temp.EmailTo);

                Literal _subjectLiteral = (Literal)e.Item.FindControl("SubjectLiteral");
                _subjectLiteral.Text = HttpUtility.HtmlEncode(_temp.Subject);
            }
        }

        protected void TypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowData();
        }
    }
}