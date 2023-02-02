using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITV.Common;

namespace ITV.UI.ApplicationClass
{
    public abstract class HotelOutletBase : Base
    {
        protected String _pageTitleListLiteral = "Hotel Outlet";
        protected String _pageTitleViewLiteral = "View Hotel Outlet";
        protected String _pageTitleAddLiteral = "Add Hotel Outlet";
        protected String _pageTitleEditLiteral = "Edit Hotel Outlet";

        //--LInk Page--
        protected String _listPage = "HotelOutlet.aspx";
        protected String _addPage = "AddHotelOutlet.aspx";
        protected String _editPage = "EditHotelOutlet.aspx";
        protected String _viewPage = "ViewHotelOutlet.aspx";


        // --ToolTip button--
        protected string _toolTipView = "View";
        protected string _toolTipSave = "Save";
        protected string _toolTipCancel = "Cancel";
        protected string _toolTipDrop = "Drop";
        protected string _toolTipReset = "Reset";

        protected String _codeKey = "code";
        protected NameValueCollectionExtractor _nvcExtractor;


        public HotelOutletBase()
        {
        }

        ~HotelOutletBase()
        {
        }
    }
}