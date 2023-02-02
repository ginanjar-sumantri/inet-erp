using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;

/// <summary>
/// Summary description for TicketingBase
/// </summary>
namespace InetGlobalIndo.ERP.MTJ.UI.Tour.Hotel
{
    public class HotelBase : TourBase
    {
        protected short _menuID = 2474;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "Hotel.aspx";
        protected string _addPage = "HotelAdd.aspx";
        protected string _editPage = "HotelEdit.aspx";
        protected string _detailPage = "HotelView.aspx";

        protected string _codeKey = "code";


        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Hotel";

        public HotelBase()
        {

        }

        ~HotelBase()
        {

        }
    }
}
