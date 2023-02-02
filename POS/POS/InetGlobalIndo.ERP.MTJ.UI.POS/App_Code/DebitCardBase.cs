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
    public abstract class DebitCardBase : POSBase
    {
        protected short _menuID = 2375;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";

        protected string _homePage = "DebitCard.aspx";
        protected string _addPage = "DebitCardAdd.aspx";
        protected string _editPage = "DebitCardEdit.aspx";
        protected string _viewPage = "DebitCardView.aspx";

        protected string _pageTitleLiteral = "Debit Card";

        protected NameValueCollectionExtractor _nvcExtractor;

        public DebitCardBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
