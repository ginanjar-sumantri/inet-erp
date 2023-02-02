using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using Microsoft.Reporting.WebForms;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.CustomControl;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.ProductAssembly
{
    public partial class ProductAssemblyView : ProductAssemblyBase
    {
        private ProductAssemblyBL _productAssemblyBL = new ProductAssemblyBL();
        private PermissionBL _permBL = new PermissionBL();
        private ProductBL _productBL = new ProductBL();
        private ReportStockControlBL _reportBL = new ReportStockControlBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private SupplierBL _supplierBL = new SupplierBL();
        private CustomerBL _customerBL = new CustomerBL();
        private UnpostingActivitiesBL _unpostingActivitiesBL = new UnpostingActivitiesBL();

        private string _reportPath1 = "ProductAssembly/ProductAssemblyPrintPreview.rdlc";

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _imgGetApproval = "get_approval.jpg";
        private string _imgApprove = "approve.jpg";
        private string _imgPosting = "posting.jpg";
        private string _imgUnposting = "unposting.jpg";
        private string _imgPreview = "preview.jpg";

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

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/approve.jpg";

                this.SetButtonPermission();
                this.ClearLabel();

                this.ShowData();
            }
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == TransactionDataMapper.GetStatusByte(TransStatus.OnHold).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;

                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == TransactionDataMapper.GetStatusByte(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == TransactionDataMapper.GetStatusByte(TransStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == TransactionDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgUnposting;

                this._permUnposting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting);

                if (this._permUnposting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
        }

        public void ShowPreviewButton()
        {
            this._permPrintPreview = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.PrintPreview);

            if (this._permPrintPreview == PermissionLevel.NoAccess)
            {
                this.PreviewButton.Visible = false;
            }
            else
            {
                if (this.StatusHiddenField.Value.Trim().ToLower() == TransactionDataMapper.GetStatusByte(TransStatus.OnHold).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == TransactionDataMapper.GetStatusByte(TransStatus.WaitingForApproval).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == TransactionDataMapper.GetStatusByte(TransStatus.Approved).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == TransactionDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
            }
        }

        public void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            STCTrProductAssembly _stcTrProductAssembly = this._productAssemblyBL.GetSingleSTCTrProductAssembly(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNoTextBox.Text = _stcTrProductAssembly.TransNmbr;
            this.FileNmbrTextBox.Text = _stcTrProductAssembly.FileNmbr;
            this.StatusHiddenField.Value = _stcTrProductAssembly.Status.ToString();
            this.StatusLabel.Text = ProductAssemblyDataMapper.GetStatusText(_stcTrProductAssembly.Status);
            this.DateTextBox.Text = DateFormMapper.GetValue(_stcTrProductAssembly.TransDate);
            this.ProductCodeTextBox.Text = _stcTrProductAssembly.ProductCode;
            this.ProductNameTextBox.Text = _productBL.GetProductNameByCode(_stcTrProductAssembly.ProductCode);
            this.ReferenceTextBox.Text = _stcTrProductAssembly.Reference;
            this.RemarkTextBox.Text = _stcTrProductAssembly.Remark;
            this.QtyTextBox.Text = _stcTrProductAssembly.Qty.ToString("#,##0.##");
            this.WarehouseCodeTextBox.Text = _warehouseBL.GetWarehouseNameByCode(_stcTrProductAssembly.WrhsCode);
            this.LocationNameTextBox.Text = _warehouseBL.GetWarehouseLocationNameByCode(_stcTrProductAssembly.WrhsLocationCode);
            if (_stcTrProductAssembly.WrhsFgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.WrhsSubledTextBox.Text = "";
            }
            else
            {
                if (_stcTrProductAssembly.WrhsFgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.WrhsSubledTextBox.Text = this._customerBL.GetNameByCode(_stcTrProductAssembly.WrhsSubled.ToString());
                }
                else if (_stcTrProductAssembly.WrhsFgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.WrhsSubledTextBox.Text = this._supplierBL.GetSuppNameByCode(_stcTrProductAssembly.WrhsSubled.ToString());
                }
            }

            this.ShowActionButton();
            this.ShowPreviewButton();

            if (Convert.ToByte(this.StatusHiddenField.Value) == TransactionDataMapper.GetStatus(TransStatus.Posted))
            {
                this.EditButton.Visible = false;
            }
            else
            {
                this.EditButton.Visible = true;
            }

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.Panel1.Visible = true;
            this.Panel2.Visible = false;
        }

        protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = true;

            this.ReportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

            ReportDataSource _reportDataSource1 = this._reportBL.ProductAssemblyPrintPreview(this.TransNoTextBox.Text);

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
            this.ReportViewer1.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[2];
            _reportParam[0] = new ReportParameter("Nmbr", this.TransNoTextBox.Text, true);
            _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ActionButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();

            if (this.StatusHiddenField.Value.Trim().ToLower() == TransactionDataMapper.GetStatusByte(TransStatus.OnHold).ToString().ToLower())
            {
                string _result = this._productAssemblyBL.GetAppr(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == TransactionDataMapper.GetStatusByte(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                string _result = this._productAssemblyBL.Approve(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == TransactionDataMapper.GetStatusByte(TransStatus.Approved).ToString().ToLower())
            {
                string _result = this._productAssemblyBL.Posting(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == TransactionDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
            {
                this.ReasonPanel.Visible = true;
                //string _result = this._productAssemblyBL.Unposting(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

                //this.WarningLabel.Text = _result;
            }

            this.ShowData();
        }
        protected void YesButton_OnClick(object sender, EventArgs e)
        {
            DateTime _transdate = DateFormMapper.GetValue(this.DateTextBox.Text);
            int _year = _transdate.Year;
            int _period = _transdate.Month;

            string _result = this._productAssemblyBL.Unposting(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

            //this.Label.Text = _result;

            if (_result == "Unposting Success")
            {
                bool _result1 = _unpostingActivitiesBL.InsertUnpostingActivities(AppModule.GetValue(TransactionType.ProductAssembly), this.TransNoTextBox.Text, this.FileNmbrTextBox.Text, HttpContext.Current.User.Identity.Name, this.ReasonTextBox.Text, Convert.ToByte(ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting)));
                if (_result1 == true)
                this.WarningLabel.Text = _result;
                this.ReasonTextBox.Text = "";
                this.ReasonPanel.Visible = false;
            }
            else
            {
                this.ReasonTextBox.Text = "";
                this.ReasonPanel.Visible = false;
                this.WarningLabel.Text = _result;
            }

            this.ShowData();
        }

        protected void NoButton_OnClick(object sender, EventArgs e)
        {
            this.ReasonPanel.Visible = false;
        }
    }
}