using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITV.UI.ApplicationClass
{
    public abstract class EventBase : Base
    {
        protected String _pageTitleListLiteral = "Event";
        protected String _pageTitleViewLiteral = "View Event";
        protected String _pageTitleAddLiteral = "Add Event";
        protected String _pageTitleEditLiteral = "Edit Event";

        //--LInk Page--
        protected String _listPage = "Event.aspx";
        protected String _addPage = "AddEvent.aspx";
        protected String _editPage = "EditEvent.aspx";
        protected String _viewPage = "ViewEvent.aspx";


        // --ToolTip button--
        protected string _toolTipView = "View";
        protected string _toolTipSave = "Save";
        protected string _toolTipCancel = "Cancel";
        protected string _toolTipDrop = "Drop";
        protected string _toolTipReset = "Reset";

        protected String _codeKey = "code";


        public EventBase()
        {
        }

        ~EventBase()
        {
        }
    }
}