using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

/// <summary>
/// Summary description for MemberTypeBase
/// </summary>
namespace InetGlobalIndo.ERP.MTJ.UI.POS.Member
{
    public abstract class CreditCardBase : POSBase
    {
        protected short _menuID = 2367;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";

        protected string _homePage = "CreditCard.aspx";
        protected string _addPage = "CreditCardAdd.aspx";
        protected string _editPage = "CreditCardEdit.aspx";
        protected string _viewPage = "CreditCardView.aspx";

        protected string _pageTitleLiteral = "Credit Card";

        protected NameValueCollectionExtractor _nvcExtractor;

        public CreditCardBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
