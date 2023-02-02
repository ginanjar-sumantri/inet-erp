using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.CustomControl;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPCustomerList
{
    public partial class DPCustomerListView : DPCustomerListBase
    {
        private FINDPCustomerListBL _finDPCustListBL = new FINDPCustomerListBL();
        private CustomerBL _cust = new CustomerBL();
        private SalesOrderBL _salesOrderBL = new SalesOrderBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private UnpostingActivitiesBL _unpostingActivitiesBL = new UnpostingActivitiesBL();

        private string _imgGetApproval = "get_approval.jpg";
        private string _imgApprove = "approve.jpg";
        private string _imgPosting = "posting.jpg";
        private string _imgUnposting = "unposting.jpg";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ClearLabel();
                this.ShowData();

                this.SetButtonPermission();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        public void ShowData()
        {
            _finDPCustListBL = new FINDPCustomerListBL();
            FINDPCustList _finDPCustomerList = this._finDPCustListBL.GetSingleFINDPCustList(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finDPCustomerList.CurrCode);

            this.DPCustListCodeTextBox.Text = _finDPCustomerList.TransNmbr;
            this.FileNmbrTextBox.Text = _finDPCustomerList.FileNmbr;
            this.DPCustListDateTextBox.Text = DateFormMapper.GetValue(_finDPCustomerList.TransDate);
            this.StatusLabel.Text = DPCustListDataMapper.GetStatusText(_finDPCustomerList.Status);
            this.StatusHiddenField.Value = _finDPCustomerList.Status.ToString();
            this.CustTextBox.Text = _cust.GetNameByCode(_finDPCustomerList.CustCode);
            this.CurrCodeTextBox.Text = _finDPCustomerList.CurrCode;
            this.ForexRateTextBox.Text = (_finDPCustomerList.ForexRate == 0) ? "0" : _finDPCustomerList.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AttnTextBox.Text = _finDPCustomerList.Attn;
            this.SONoTextBox.Text = this._salesOrderBL.GetFileNmbrMKTSOHd(_finDPCustomerList.SONo);
            this.BaseForexTextBox.Text = (_finDPCustomerList.BaseForex == 0) ? "0" : _finDPCustomerList.BaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNTextBox.Text = (_finDPCustomerList.PPn == 0) ? "0" : _finDPCustomerList.PPn.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNForexTextBox.Text = (_finDPCustomerList.PPNForex == 0) ? "0" : _finDPCustomerList.PPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalForexTextBox.Text = (_finDPCustomerList.TotalForex == 0) ? "0" : _finDPCustomerList.TotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _finDPCustomerList.Remark;

            this.ShowActionButton();
        }

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == DPCustListDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;

                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == DPCustListDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == DPCustListDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == DPCustListDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgUnposting;

                this._permUnposting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting);

                if (this._permUnposting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }

            FINDPCustList _finDPCustomerList = this._finDPCustListBL.GetSingleFINDPCustList(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            if (_finDPCustomerList.Status != DPCustListDataMapper.GetStatus(TransStatus.Posted))
            {
                this.EditButton.Visible = true;
                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
            }
            else
            {
                this.EditButton.Visible = false;
            }
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ActionButton_Click(object sender, ImageClickEventArgs e)
        {
            //string[] _date = this.DPCustListDateTextBox.Text.Split('-');
            //int _year = Convert.ToInt32(_date[0]);
            //int _period = Convert.ToInt32(_date[1]);
            DateTime _transdate = DateFormMapper.GetValue(this.DPCustListDateTextBox.Text);
            int _year = _transdate.Year;
            int _period = _transdate.Month;

            this.ClearLabel();

            if (this.StatusHiddenField.Value.Trim().ToLower() == DPCustListDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                String _result2 = this._finDPCustListBL.GetAppr(this.DPCustListCodeTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result2;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == DPCustListDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                String _result2 = this._finDPCustListBL.Approve(this.DPCustListCodeTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result2;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == DPCustListDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                string _result2 = this._finDPCustListBL.Posting(this.DPCustListCodeTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result2;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == DPCustListDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.ReasonPanel.Visible = true;
                //string _result2 = this._finDPCustListBL.Unposting(this.DPCustListCodeTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                //this.WarningLabel.Text = _result2;
            }

            this.ShowData();
        }

        protected void YesButton_OnClick(object sender, EventArgs e)
        {
            DateTime _transdate = DateFormMapper.GetValue(this.DPCustListDateTextBox.Text);
            int _year = _transdate.Year;
            int _period = _transdate.Month;

            string _result2 = this._finDPCustListBL.Unposting(this.DPCustListCodeTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

            //this.Label.Text = _result;

            if (_result2 == "Unposting Success")
            {
                bool _result1 = _unpostingActivitiesBL.InsertUnpostingActivities(AppModule.GetValue(TransactionType.FINDPCustRetur), this.DPCustListCodeTextBox.Text, this.FileNmbrTextBox.Text, HttpContext.Current.User.Identity.Name, this.ReasonTextBox.Text, Convert.ToByte(ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting)));
                if (_result1 == true)
                    this.WarningLabel.Text = _result2;
                this.ReasonTextBox.Text = "";
                this.ReasonPanel.Visible = false;
            }
            else
            {
                this.ReasonTextBox.Text = "";
                this.ReasonPanel.Visible = false;
                this.WarningLabel.Text = _result2;
            }

            this.ShowData();
        }

        protected void NoButton_OnClick(object sender, EventArgs e)
        {
            this.ReasonPanel.Visible = false;
        }
    }
}