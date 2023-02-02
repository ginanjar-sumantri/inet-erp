using System;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Web.UI.HtmlControls;
using InetGlobalIndo.ERP.MTJ.DataMapping;

namespace InetGlobalIndo.ERP.MTJ.UI.Home
{
    public partial class _Default : HomeBase
    {
        
        private Int16 _ctr = 1;
        private ReminderBL _reminderBL = new ReminderBL();
        private UserBL _userBL = new UserBL();
        private PermissionBL _permBL = new PermissionBL();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._moduleID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageReminderTitleLiteral;

                this.ListRepeater.DataSource = _reminderBL.getReminderFrontList ( _reminderBL.getComposedRoleIdList ( _userBL.GetUserIDByName( HttpContext.Current.User.Identity.Name ).ToString() ) ) ;
                this.ListRepeater.DataBind();
            }
        }


        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HOME_Reminder _temp = (HOME_Reminder)e.Item.DataItem;

            Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
            _noLiteral.Text = _ctr.ToString();
            _ctr++;

            ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
            _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";
            //link nya dibelokin langsung ke halaman detail remindernya.
            //_viewButton.PostBackUrl = "Reminder/ReminderDetail.aspx?reminderCode=" + _temp.ReminderCode ;
            String _menuNya = _reminderBL.getMenuId(_temp.ReminderCode);
            String _linkNya = "";
            switch (AppModule.GetValue(_menuNya))
            {
                case ERPModule.Home:
                    _linkNya = ApplicationConfig.HomeWebAppURL + _reminderBL.GetReminderPath( _temp.ReminderCode );
                    //_btnView.PostBackUrl = ApplicationConfig.HomeWebAppURL + _temp.ReminderPath + "?" + "code=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_temp.TransNmbr, ApplicationConfig.EncryptionKey));
                    break;
                case ERPModule.Accounting:
                    _linkNya = ApplicationConfig.AccountingWebAppURL + _reminderBL.GetReminderPath(_temp.ReminderCode);
                    //_btnView.PostBackUrl = ApplicationConfig.AccountingWebAppURL + _temp.ReminderPath + "?" + "code=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_temp.TransNmbr, ApplicationConfig.EncryptionKey));
                    break;
                case ERPModule.ExecutiveInformation:
                    _linkNya = ApplicationConfig.ExecutiveInformationWebAppURL + _reminderBL.GetReminderPath(_temp.ReminderCode);
                    //_btnView.PostBackUrl = ApplicationConfig.ExecutiveInformationWebAppURL + _temp.ReminderPath + "?" + "code=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_temp.TransNmbr, ApplicationConfig.EncryptionKey));
                    break;
                case ERPModule.Finance:
                    _linkNya = ApplicationConfig.FinanceWebAppURL + _reminderBL.GetReminderPath(_temp.ReminderCode);
                    //_btnView.PostBackUrl = ApplicationConfig.FinanceWebAppURL + _temp.ReminderPath + "?" + "code=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_temp.TransNmbr, ApplicationConfig.EncryptionKey));
                    break;
                case ERPModule.HumanResource:
                    _linkNya = ApplicationConfig.HumanResourceWebAppURL + _reminderBL.GetReminderPath(_temp.ReminderCode);
                    //_btnView.PostBackUrl = ApplicationConfig.HumanResourceWebAppURL + _temp.ReminderPath + "?" + "code=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_temp.TransNmbr, ApplicationConfig.EncryptionKey));
                    break;
                case ERPModule.Payroll:
                    _linkNya = ApplicationConfig.PayrollWebAppURL + _reminderBL.GetReminderPath(_temp.ReminderCode);
                    //_btnView.PostBackUrl = ApplicationConfig.PayrollWebAppURL + _temp.ReminderPath + "?" + "code=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_temp.TransNmbr, ApplicationConfig.EncryptionKey));
                    break;
                case ERPModule.Production:
                    _linkNya = ApplicationConfig.ProductionWebAppURL + _reminderBL.GetReminderPath(_temp.ReminderCode);
                    //_btnView.PostBackUrl = ApplicationConfig.ProductionWebAppURL + _temp.ReminderPath + "?" + "code=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_temp.TransNmbr, ApplicationConfig.EncryptionKey));
                    break;
                case ERPModule.Purchasing:
                    _linkNya = ApplicationConfig.PurchasingWebAppURL + _reminderBL.GetReminderPath(_temp.ReminderCode);
                    //_btnView.PostBackUrl = ApplicationConfig.PurchasingWebAppURL + _temp.ReminderPath + "?" + "code=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_temp.TransNmbr, ApplicationConfig.EncryptionKey));
                    break;
                case ERPModule.Sales:
                    _linkNya = ApplicationConfig.SalesWebAppURL + _reminderBL.GetReminderPath(_temp.ReminderCode);
                    //_btnView.PostBackUrl = ApplicationConfig.SalesWebAppURL + _temp.ReminderPath + "?" + "code=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_temp.TransNmbr, ApplicationConfig.EncryptionKey));
                    break;
                case ERPModule.Settings:
                    _linkNya = ApplicationConfig.SettingsWebAppURL + _reminderBL.GetReminderPath(_temp.ReminderCode);
                    //_btnView.PostBackUrl = ApplicationConfig.SettingsWebAppURL + _temp.ReminderPath + "?" + "code=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_temp.TransNmbr, ApplicationConfig.EncryptionKey));
                    break;
                case ERPModule.ShipPorting:
                    _linkNya = ApplicationConfig.PortWebAppURL + _reminderBL.GetReminderPath(_temp.ReminderCode);
                    //_btnView.PostBackUrl = ApplicationConfig.PortWebAppURL + _temp.ReminderPath + "?" + "code=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_temp.TransNmbr, ApplicationConfig.EncryptionKey));
                    break;
                case ERPModule.StockControl:
                    _linkNya = ApplicationConfig.StockControlWebAppURL + _reminderBL.GetReminderPath(_temp.ReminderCode);
                    //_btnView.PostBackUrl = ApplicationConfig.StockControlWebAppURL + _temp.ReminderPath + "?" + "code=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_temp.TransNmbr, ApplicationConfig.EncryptionKey));
                    break;
                case ERPModule.Billing:
                    _linkNya = ApplicationConfig.BillingWebAppURL + _reminderBL.GetReminderPath(_temp.ReminderCode);
                    //_btnView.PostBackUrl = ApplicationConfig.BillingWebAppURL + _temp.ReminderPath + "?" + "code=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_temp.TransNmbr, ApplicationConfig.EncryptionKey));
                    break;
                case ERPModule.NCC:
                    _linkNya = ApplicationConfig.NCCWebAppURL + _reminderBL.GetReminderPath(_temp.ReminderCode);
                    //_btnView.PostBackUrl = ApplicationConfig.NCCWebAppURL + _temp.ReminderPath + "?" + "code=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_temp.TransNmbr, ApplicationConfig.EncryptionKey));
                    break;
            }
            _viewButton.OnClientClick = "parent.location='" + _linkNya + "';";


            Literal _reminderNameLiteral = (Literal)e.Item.FindControl("ReminderNameLiteral");
            _reminderNameLiteral.Text = _temp.ReminderName;

            Literal _countLiteral = (Literal)e.Item.FindControl("CountLiteral");
            _countLiteral.Text = _temp.Count.ToString() ;

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
}