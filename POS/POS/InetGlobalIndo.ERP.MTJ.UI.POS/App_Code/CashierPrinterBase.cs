using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.CashierPrinter
{
    public abstract class CashierPrinterBase : POSBase
    {
        protected short _menuID = 2558;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.POSWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";

        protected string _homePage = "CashierPrinter.aspx";
        protected string _addPage = "CashierPrinterAdd.aspx";
        protected string _editPage = "CashierPrinterEdit.aspx";
        protected string _viewPage = "CashierPrinterView.aspx";
        
        protected string _pageTitleLiteral = "Cashier Printer";
        
        protected NameValueCollectionExtractor _nvcExtractor;

        public CashierPrinterBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~CashierPrinterBase()
        {
        }
    }
}
