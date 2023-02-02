using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public abstract class ClearanceBase : System.Web.UI.Page
    {
        protected String _homePage = "Clearance.aspx";
        protected String _addPage = "ClearanceAdd.aspx";
        protected String _editPage = "ClearanceEdit.aspx";
        protected String _detailPage = "ClearanceDetail.aspx";
        protected String _detailEditPage = "ClearanceDetailEdit.aspx";


        protected String _codeKey = "code";
        protected String _areaKey = "area";
        protected String _purposeKey = "purpose";
        protected String _checkinKey = "check in";

        protected String _pageTitleLiteral = "Clearance";

        protected NameValueCollectionExtractor _nvcExtractor;

        public ClearanceBase()
        {
        }

        ~ClearanceBase()
        {
        }
    }
}
