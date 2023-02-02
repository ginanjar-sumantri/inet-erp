using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.CashFlowGroup
{
    public partial class CashFlowGroupAdd : CashFlowGroupBase
    {
        private CashFlowGroupBL _cashFlowGroupBL = new CashFlowGroupBL();
        private PermissionBL _permBL = new PermissionBL();

        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;
        private decimal _min = 0;
        private decimal _max = 0;

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private string _currPageKey = "CurrentPage";

        string _transTypeCode = "";

        private string awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string akhir = "_ListCheckBox";
        private string cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00_DefaultBodyContentPlaceHolder_TempHidden";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.ViewState[this._currPageKey] = 0;

                this.PageTitleLiteral.Text = this._pageTitleLiteral;
                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ClearLabel();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.CashFlowGroupCodeTextBox.Text = "";
            this.CashFlowGroupNameTextBox.Text = "";
        }

        protected void NextButton_Click(object sender, EventArgs e)
        {
            FINMsCashFlowGroup _finMsCashFlowGroup = new FINMsCashFlowGroup();

            _finMsCashFlowGroup.CashFlowGroupCode = this.CashFlowGroupCodeTextBox.Text;
            _finMsCashFlowGroup.CashFlowGroupName = this.CashFlowGroupNameTextBox.Text;
            _finMsCashFlowGroup.InsertBy = HttpContext.Current.User.Identity.Name;
            _finMsCashFlowGroup.InsertDate = DateTime.Now;
            _finMsCashFlowGroup.EditBy = HttpContext.Current.User.Identity.Name;
            _finMsCashFlowGroup.EditDate = DateTime.Now;

            Boolean _result = _cashFlowGroupBL.AddFINMsCashFlowGroup(_finMsCashFlowGroup);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.CashFlowGroupCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }
    }
}