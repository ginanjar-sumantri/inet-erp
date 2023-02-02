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
namespace InetGlobalIndo.ERP.MTJ.UI.Tour.Ticketing
{
    public class TicketingBase : TourBase
    {
        protected short _menuID = 2457;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permRevised;
        protected string _errorPermissionPage = ApplicationConfig.TourWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "Ticketing.aspx";
        protected string _addPage = "TicketingAdd.aspx";
        protected string _editPage = "TicketingEdit.aspx";
        protected string _detailPage = "TicketingDetail.aspx";
        protected string _addDetailPage = "TicketingDetailAdd.aspx";        
        protected string _editDetailPage = "TicketingDetailEdit.aspx";
        protected string _viewDetailPage = "TicketingDetailView.aspx";
        protected string _revisedDetailPage = "TicketingDetailRevised.aspx";
        

        protected string _codeKey = "code";
        protected string _itemCode = "ItemNo";
  

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Ticketing";

        public TicketingBase()
        {
           
        }

        ~TicketingBase() 
        { 

        }
    }
}
