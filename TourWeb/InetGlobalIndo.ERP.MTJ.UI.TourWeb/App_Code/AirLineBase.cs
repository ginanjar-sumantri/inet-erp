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
namespace InetGlobalIndo.ERP.MTJ.UI.Tour.AirLine
{
    public class AirLineBase : TourBase
    {
        protected short _menuID = 2459;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.TourWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "AirLine.aspx";
        protected string _addPage = "AirLineAdd.aspx";
        protected string _editPage = "AirLineEdit.aspx";
        protected string _detailPage = "AirLineView.aspx";        

        protected string _codeKey = "code";
  

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Air Line";

        public AirLineBase()
        {
           
        }

        ~AirLineBase() 
        { 

        }
    }
}
