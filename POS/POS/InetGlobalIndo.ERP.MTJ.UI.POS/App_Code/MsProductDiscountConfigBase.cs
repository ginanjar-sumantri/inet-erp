using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.MsProductDiscount
{
    public abstract class MsProductDiscountBase : POSBase
    {
        protected short _menuID = 2352;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.POSWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";

        protected string _homePage = "MsProductDiscountConfig.aspx";
        protected string _addPage = "MsProductDiscountConfigAdd.aspx";
        protected string _editPage = "MsProductDiscountConfigEdit.aspx";
        protected string _viewPage = "MsProductDiscountConfigView.aspx";

        protected string _pageTitleLiteral = "Product Discount";

        protected NameValueCollectionExtractor _nvcExtractor;

        public MsProductDiscountBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~MsProductDiscountBase()
        {
        }
    }
}
