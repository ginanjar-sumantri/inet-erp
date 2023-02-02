using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using BusinessRule.POSInterface;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Threading;

namespace POS.POSInterface.General
{
    public partial class CloseShift : CloseShiftBase
    {
        private User_EmployeeBL _userEmployeeBL = new User_EmployeeBL();
        private EmployeeBL _employeeBL = new EmployeeBL();
        private CashierAccountBL _cashierAccountBL = new CashierAccountBL();
        private AccountBL _accountBL = new AccountBL();
        private CloseShiftBL _closeShiftBL = new CloseShiftBL();

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack == true)
            {
                this.ClearData();
                this.ShowData();
                this.Menu.Visible = true;
                //this.ReportViewer1.Visible = false;
                this.ReportPanel.Visible = false;
                //this.CloseButton.Attributes.Add("Onclick", "return OpenBooked();");
            }
        }

        private void ClearData()
        {
            this.CashierIDLabel.Text = "";
            this.CashierEmployeeLabel.Text = "";
            this.CashierAccountLabel.Text = "";
            //this.TotalTextBox.Text = "";
            this.CashLiteral.Text = "";
            this.DebitLiteral.Text = "";
            this.KreditLiteral.Text = "";
            this.VoucherLiteral.Text = "";
        }

        private void ClearListDetail()
        {
            this.ListRepeater.DataSource = null;
            this.ListRepeater.DataBind();
        }

        private void ShowData()
        {
            try
            {
                String _cashierEmployee = this._userEmployeeBL.GetEmployeeIDByUserName(HttpContext.Current.User.Identity.Name);
                String _employeeName = this._employeeBL.GetEmpNameByCode(_cashierEmployee);
                if (_employeeName != "")
                {
                    String _cashierAccountID = this._cashierAccountBL.GetMemberNameByCode(_cashierEmployee);
                    String _accountName = this._accountBL.GetAccountNameByCode(_cashierAccountID);
                    this.CashierAccountLabel.Text = _accountName;
                }

                if (_employeeName == "")
                {
                    this.PrintButton.Enabled = false;
                }

                this.CashierIDLabel.Text = HttpContext.Current.User.Identity.Name;
                this.CashierEmployeeLabel.Text = _employeeName;
                this.CashHiddenField.Value = Convert.ToString(0);
                this.DebitHiddenField.Value = Convert.ToString(0);
                this.KreditHiddenField.Value = Convert.ToString(0);
                this.VoucherHiddenField.Value = Convert.ToString(0);

                Guid _userId = _userEmployeeBL.GetUserIdEmpId(_cashierEmployee);
                POSTrShiftLog _posTrShiftLog = _closeShiftBL.GetSinglePOSTrShiftLog(_cashierEmployee, _userId);

                List<POSTrSettlementDtPaymentType> _posTrSettlementDtPaymentType = _closeShiftBL.GetPayment(HttpContext.Current.User.Identity.Name, Convert.ToDateTime(_posTrShiftLog.OpenShift), DateTime.Now);

                foreach (var _row in _posTrSettlementDtPaymentType)
                {
                    if (_row.PaymentType == POSPaymentTypeMapper.GetStatusText(POSPaymentType.Cash))
                    {
                        this.CashLiteral.Text = Convert.ToDecimal(_row.PaymentAmount).ToString("#,##0.00");
                        this.CashHiddenField.Value = _row.PaymentAmount.ToString();
                    }
                    else if (_row.PaymentType == POSPaymentTypeMapper.GetStatusText(POSPaymentType.Debit))
                    {
                        this.DebitLiteral.Text = Convert.ToDecimal(_row.PaymentAmount).ToString("#,##0.00");
                        this.DebitHiddenField.Value = _row.PaymentAmount.ToString();
                    }
                    else if (_row.PaymentType == POSPaymentTypeMapper.GetStatusText(POSPaymentType.Kredit))
                    {
                        this.KreditLiteral.Text = Convert.ToDecimal(_row.PaymentAmount).ToString("#,##0.00");
                        this.KreditHiddenField.Value = _row.PaymentAmount.ToString();
                    }
                    else if (_row.PaymentType == POSPaymentTypeMapper.GetStatusText(POSPaymentType.Voucher))
                    {
                        this.VoucherLiteral.Text = Convert.ToDecimal(_row.PaymentAmount).ToString("#,##0.00");
                        this.VoucherHiddenField.Value = _row.PaymentAmount.ToString();

                    }
                    else
                    {
                        this.CashLiteral.Text = (this.CashLiteral.Text == "" ? "0" : this.CashLiteral.Text);
                        this.DebitLiteral.Text = (this.DebitLiteral.Text == "" ? "0" : this.DebitLiteral.Text);
                        this.KreditLiteral.Text = (this.KreditLiteral.Text == "" ? "0" : this.KreditLiteral.Text);
                        this.VoucherLiteral.Text = (this.VoucherLiteral.Text == "" ? "0" : this.VoucherLiteral.Text);
                    }
                }

                this.CashLiteral.Text = (this.CashLiteral.Text == "" ? "0" : this.CashLiteral.Text);
                this.DebitLiteral.Text = (this.DebitLiteral.Text == "" ? "0" : this.DebitLiteral.Text);
                this.KreditLiteral.Text = (this.KreditLiteral.Text == "" ? "0" : this.KreditLiteral.Text);
                this.VoucherLiteral.Text = (this.VoucherLiteral.Text == "" ? "0" : this.VoucherLiteral.Text);

                this.ListRepeater.DataSource = _closeShiftBL.GetListCloseShift(HttpContext.Current.User.Identity.Name, Convert.ToDateTime(_posTrShiftLog.OpenShift), DateTime.Now);
                this.ListRepeater.DataBind();

                this.TotalLiteral.Text = ((this.CashLiteral.Text == "" ? 0 : Convert.ToDecimal(this.CashLiteral.Text)) + (this.DebitLiteral.Text == "" ? 0 : Convert.ToDecimal(this.DebitLiteral.Text)) + (this.KreditLiteral.Text == "" ? 0 : Convert.ToDecimal(this.KreditLiteral.Text)) + (this.VoucherLiteral.Text == "" ? 0 : Convert.ToDecimal(this.VoucherLiteral.Text))).ToString("#,##0.00");//total
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                POSTrSettlementDtPaymentType _temp = (POSTrSettlementDtPaymentType)e.Item.DataItem;

                Literal _noLiteral = (Literal)e.Item.FindControl("Number");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                Literal _transNo = (Literal)e.Item.FindControl("TransactionNoLiteral");
                _transNo.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _transDate = (Literal)e.Item.FindControl("TransactionDateLiteral");
                _transDate.Text = DateFormMapper.GetValue(_temp.TransDate);

                //Literal _referenceNo = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                //_referenceNo.Text = HttpUtility.HtmlEncode(_temp.Reference);

                Literal _paymentType = (Literal)e.Item.FindControl("PaymentTypeLiteral");
                _paymentType.Text = HttpUtility.HtmlEncode(_temp.PaymentType);

                //Literal _divisi = (Literal)e.Item.FindControl("DivisiLiteral");
                //_divisi.Text = HttpUtility.HtmlEncode(_temp.Divisi);

                Literal _amount = (Literal)e.Item.FindControl("AmountTransactionLiteral");
                _amount.Text = HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.PaymentAmount).ToString("#,##0.00"));

            }
        }

        //protected void CloseButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        bool _close = _closeShiftBL.UpDateCloseShift(HttpContext.Current.User.Identity.Name);

        //        if (_close)
        //            Response.Redirect(this._cashierPage);
        //    }
        //    catch (ThreadAbortException) { throw; }
        //    catch (Exception ex)
        //    {
        //        this.errorhandler(ex);
        //    }
        //}

        protected void PrintButton_Click(object sender, EventArgs e)
        {
            try
            {
                //this.Menu.Visible = false;
                //this.ReportViewer1.Visible = true;
                this.FormPanel.Visible = false;
                this.ReportPanel.Visible = true;

                this.ReportViewer1.Reset();
                ReportDataSource _reportDataSource1 = this._closeShiftBL.ReportCloseShift(this.CashierEmployeeLabel.Text, this.CashierAccountLabel.Text, this.CashHiddenField.Value, this.VoucherHiddenField.Value, this.KreditHiddenField.Value, this.DebitHiddenField.Value);

                this.ReportViewer1.LocalReport.EnableExternalImages = true;
                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "General/RptCloseShift.rdlc";
                this.ReportViewer1.DataBind();

                ReportParameter[] _reportParam = new ReportParameter[12];
                _reportParam[0] = new ReportParameter("CashierID", HttpContext.Current.User.Identity.Name, true);
                _reportParam[1] = new ReportParameter("TransType", POSTransTypeDataMapper.GetTransType(POSTransType.Retail), true);
                _reportParam[2] = new ReportParameter("Status", POSTrSettlementDataMapper.GetStatus(POSTrSettlementStatus.Posted).ToString(), true);
                _reportParam[3] = new ReportParameter("FgClose", true.ToString(), true);
                _reportParam[4] = new ReportParameter("CashierEmployee", this.CashierEmployeeLabel.Text, false);
                _reportParam[5] = new ReportParameter("CashierAccount", this.CashierAccountLabel.Text, false);
                _reportParam[6] = new ReportParameter("PaymentTypeCredit", POSPaymentTypeMapper.GetStatusText(POSPaymentType.Kredit), false);
                _reportParam[7] = new ReportParameter("PaymentTypeDebit", POSPaymentTypeMapper.GetStatusText(POSPaymentType.Debit), false);
                _reportParam[8] = new ReportParameter("Cash", this.CashHiddenField.Value);
                _reportParam[9] = new ReportParameter("Voucher", this.VoucherHiddenField.Value);
                _reportParam[10] = new ReportParameter("Credit", this.KreditHiddenField.Value);
                _reportParam[11] = new ReportParameter("Debit", this.DebitHiddenField.Value);

                this.ReportViewer1.LocalReport.SetParameters(_reportParam);
                this.ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void errorhandler(Exception ex)
        {
            //ErrorHandler.Record(ex, EventLogEntryType.Error);
            String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "CLOSESHIFT");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
        }
    }
}
