using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.DirectSales
{
    public partial class PIN : System.Web.UI.Page
    {
        private DirectSalesBL _directSalesBL = new DirectSalesBL();

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private String _codeKey = "code";
        private string _currPageKey = "CurrentPage";

        private NameValueCollectionExtractor _nvcExtractor;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.ViewState[this._currPageKey] = 0;

                this.ShowData();
            }
        }

        private void ShowData()
        {
            string _transNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            this.ListRepeater.DataSource = this._directSalesBL.GetPINForNCPSales(_transNmbr);
            this.ListRepeater.DataBind();
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                MsProduct_SerialNumber _temp = (MsProduct_SerialNumber)e.Item.DataItem;

                Literal _serialNmbrLiteral = (Literal)e.Item.FindControl("SerialNmbrLiteral");
                _serialNmbrLiteral.Text = HttpUtility.HtmlEncode(_temp.SerialNumber);

                Literal _pinLiteral = (Literal)e.Item.FindControl("PINLiteral");
                _pinLiteral.Text = HttpUtility.HtmlEncode(_temp.PIN);
            }
        }
    }
}