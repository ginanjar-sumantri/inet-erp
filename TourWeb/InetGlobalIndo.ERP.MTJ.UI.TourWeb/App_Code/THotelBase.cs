using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;

/// <summary>
/// Summary description for THotelBase
/// </summary>
namespace InetGlobalIndo.ERP.MTJ.UI.Tour.THotel
{
    public class THotelBase : TourBase
    {
        protected short _menuID = 2468;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permRevised;
        protected string _errorPermissionPage = ApplicationConfig.TourWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "Hotel.aspx";
        protected string _addPage = "HotelAdd.aspx";
        protected string _editPage = "HotelEdit.aspx";
        protected string _detailPage = "HotelDetail.aspx";
        protected string _addDetailPage = "HotelDetailAdd.aspx";
        protected string _editDetailPage = "HotelDetailEdit.aspx";
        protected string _viewDetailPage = "HotelDetailView.aspx";
        protected string _revisedDetailPage = "HotelDetailRevised.aspx";

        protected string _codeKey = "code";
        protected string _itemCode = "ItemNo";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Voucher Hotel";

        public THotelBase()
        {

        }

        ~THotelBase()
        {

        }
    }
}
