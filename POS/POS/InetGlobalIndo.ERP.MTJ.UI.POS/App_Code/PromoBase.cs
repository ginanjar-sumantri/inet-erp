using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Promo
{
    public abstract class PromoBase : POSBase
    {
        protected short _menuID = 2541;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting;
        
        protected string _errorPermissionPage = ApplicationConfig.POSWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";

        protected string _homePage = "Promo.aspx";
        protected string _addPage = "PromoAdd.aspx";
        protected string _editPage = "PromoEdit.aspx";
        protected string _viewPage = "PromoView.aspx";
        protected string _addDetailPage = "PromoMemberAdd.aspx";
        protected string _addDetailPage2 = "PromoProductAdd.aspx";
        protected string _addDetailPage3 = "PromoFreeProductAdd.aspx";
        protected string _viewDetailPage = "PromoMemberView.aspx";
        protected string _viewDetailPage2 = "PromoProductView.aspx";
        protected string _viewDetailPage3 = "PromoFreeProductView.aspx";
        protected string _editDetailPage = "PromoMemberEdit.aspx";
        protected string _editDetailPage2 = "PromoProductEdit.aspx";
        protected string _editDetailPage3 = "PromoFreeProductEdit.aspx";

        protected string _pageTitleLiteral = "Promo";
        protected string _pageTitleDetailLiteral = "Promo Member";
        protected string _pageTitleDetail2Literal = "Promo Product";

        protected NameValueCollectionExtractor _nvcExtractor;

        public PromoBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~PromoBase()
        {
        }
    }
}
