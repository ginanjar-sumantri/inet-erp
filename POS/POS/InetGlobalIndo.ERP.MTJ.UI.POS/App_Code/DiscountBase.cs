using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Discount
{
    public abstract class DiscountBase : POSBase
    {
        protected short _menuID = 2350;
        //protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        
        protected string _errorPermissionPage = ApplicationConfig.POSWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";

        protected string _homePage = "Discount.aspx";
        protected string _addPage = "DiscountAdd.aspx";
        protected string _editPage = "DiscountEdit.aspx";
        protected string _viewPage = "DiscountView.aspx";
        protected string _addDetailPage = "DiscountDtAdd.aspx";
        protected string _viewDetailPage = "DiscountDtView.aspx";
        protected string _editDetailPage = "DiscountDtEdit.aspx";
        protected string _addDetailPage2 = "DiscountProductAdd.aspx";
        protected string _viewDetailPage2 = "DiscountProductView.aspx";
        protected string _editDetailPage2 = "DiscountProductEdit.aspx";

        protected string _pageTitleLiteral = "Discount";

        protected NameValueCollectionExtractor _nvcExtractor;

        public DiscountBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~DiscountBase()
        {
        }
    }
}
