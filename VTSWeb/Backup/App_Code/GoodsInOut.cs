using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public abstract class GoodsInOutBase : System.Web.UI.Page
    {
        protected String _homePage = "GoodsInOut.aspx";
        protected String _addPage = "GoodsInOutAdd.aspx";
        protected String _editPage = "GoodsInOutEdit.aspx";
        protected String _detailPage = "GoodsInOutDetail.aspx";
        protected String _detailEditPage = "GoodsInOutDetailEdit.aspx";


        protected String _codeKey = "code";
        protected String _itemKey = "item";
        //protected String _areaKey = "area";
        //protected String _purposeKey = "purpose";
        //protected String _checkinKey = "check in";

        protected String _pageTitleLiteral = "Goods In Out";

        protected NameValueCollectionExtractor _nvcExtractor;

        public GoodsInOutBase()
        {
        }

        ~GoodsInOutBase()
        {
        }
    }
}
