using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using BusinessRule.POSInterface;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Threading;
using BusinessRule.POS;

public partial class General_FormCloseShift : System.Web.UI.Page
{
    private User_EmployeeBL _userEmployeeBL = new User_EmployeeBL();
    private CloseShiftBL _closeShiftBL = new CloseShiftBL();
    private POSConfigurationBL _pOSConfigurationBL = new POSConfigurationBL();
    private UserBL _userBL = new UserBL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack == true)
        {
            this.ClearLabel();
            this.ClearData();
            this.ShowData();
            this.JavaScript();
            this.SetAttributes();
            this.ChangeVisiblePanel(0);
        }
        this.ClearLabel();
        this.Calculate();
    }

    protected void ClearLabel()
    {
        this.WarningLabel.Text = "";
    }

    protected void ClearData()
    {
        this.CashInHandTextBox.Text = "0.00";
        this.RemarkTextBox.Text = "";
    }

    protected void ShowData()
    {
        try
        {
            this.TotalCashTextBox.Text = "0.00";
            this.TotalDebitTextBox.Text = "0.00";
            this.TotalCreditTextBox.Text = "0.00";
            this.TotalVoucherTextBox.Text = "0.00";
            this.BeginningCashTextBox.Text = "0.00";
            this.BalanceTextBox.Text = "0.00";
            String _employeeId = _userEmployeeBL.GetEmployeeIDByUserName(HttpContext.Current.User.Identity.Name);
            Guid _userId = _userEmployeeBL.GetUserIdEmpId(_employeeId);
            POSTrShiftLog _posTrShiftLog = _closeShiftBL.GetSinglePOSTrShiftLog(_employeeId, _userId);

            this.BeginningCashTextBox.Text = Convert.ToDecimal(_posTrShiftLog.BeginningCash).ToString("#,#.00");

            List<POSTrSettlementDtPaymentType> _posTrSettlementDtPaymentType = _closeShiftBL.GetPayment(HttpContext.Current.User.Identity.Name, Convert.ToDateTime(_posTrShiftLog.OpenShift), DateTime.Now);

            foreach (var _row in _posTrSettlementDtPaymentType)
            {
                if (_row.PaymentType == POSPaymentTypeMapper.GetStatusText(POSPaymentType.Cash))
                {
                    this.TotalCashTextBox.Text = Convert.ToDecimal(Convert.ToDecimal(_row.PaymentAmount) + Convert.ToDecimal(this.BeginningCashTextBox.Text)).ToString("#,#.00");
                }
                else if (_row.PaymentType == POSPaymentTypeMapper.GetStatusText(POSPaymentType.Debit))
                {
                    this.TotalDebitTextBox.Text = Convert.ToDecimal(_row.PaymentAmount).ToString("#,#.00");
                }
                else if (_row.PaymentType == POSPaymentTypeMapper.GetStatusText(POSPaymentType.Kredit))
                {
                    this.TotalCreditTextBox.Text = Convert.ToDecimal(_row.PaymentAmount).ToString("#,#.00");
                }
                else if (_row.PaymentType == POSPaymentTypeMapper.GetStatusText(POSPaymentType.Voucher))
                {
                    this.TotalVoucherTextBox.Text = Convert.ToDecimal(_row.PaymentAmount).ToString("#,#.00");
                }
            }
            this.TotalSalesTextBox.Text = Convert.ToDecimal(Convert.ToDecimal(this.TotalCashTextBox.Text) + Convert.ToDecimal(this.TotalDebitTextBox.Text) + Convert.ToDecimal(this.TotalCreditTextBox.Text) + Convert.ToDecimal(this.TotalVoucherTextBox.Text)).ToString("#,#.00");
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void Calculate()
    {
        this.BalanceTextBox.Text = Convert.ToDecimal(Convert.ToDecimal(this.BeginningCashTextBox.Text) + Convert.ToDecimal(this.TotalCashTextBox.Text) - Convert.ToDecimal(this.CashInHandTextBox.Text)).ToString("#,#.00");
    }

    protected void CancelButton_Click(object sender, ImageClickEventArgs e)
    {
        this.ClearData();
    }

    protected void CheckValidData()
    {
        if (Convert.ToDecimal(this.BalanceTextBox.Text) != 0)
        {
            //this.WarningLabel.Text = "Balance Must Have Value 0.";
        }
    }

    protected void ApproveButton_Click(object sender, ImageClickEventArgs e)
    {
        this.CheckValidData();
        if (this.WarningLabel.Text == "")
        {
            this.ChangeVisiblePanel(1);
        }
    }

    protected void errorhandler(Exception ex)
    {
        //ErrorHandler.Record(ex, EventLogEntryType.Error);
        String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "CLOSESHIFT");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
    }

    protected void JavaScript()
    {
        String spawnJS = "<script type='text/javascript' language='JavaScript'>\n";
        //DECLARE FUNCTION FOR Calling KeyBoard CashInHand
        spawnJS += "function CashInHandKeyBoard(x) {\n";
        spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findCashInHand&titleinput=Cash In Hand&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
        spawnJS += "}\n";

        //DECLARE FUNCTION FOR CATCHING ON CashInHand
        spawnJS += "function findCashInHand(x) {\n";
        spawnJS += "dataArray = x.split ('|') ;\n";
        spawnJS += "document.getElementById('" + this.CashInHandTextBox.ClientID + "').value = dataArray [0];\n";
        spawnJS += "document.forms[0].submit();\n";
        spawnJS += "}\n";

        //DECLARE FUNCTION FOR Calling KeyBoard Remark
        spawnJS += "function RemarkKeyBoard(x) {\n";
        spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findRemark&titleinput=Remark&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
        spawnJS += "}\n";

        //DECLARE FUNCTION FOR CATCHING ON Remark
        spawnJS += "function findRemark(x) {\n";
        spawnJS += "dataArray = x.split ('|') ;\n";
        spawnJS += "document.getElementById('" + this.RemarkTextBox.ClientID + "').value = dataArray [0];\n";
        spawnJS += "document.forms[0].submit();\n";
        spawnJS += "}\n";

        spawnJS += "</script>\n";
        this.javascriptReceiver.Text = spawnJS;
    }

    protected void SetAttributes()
    {
        this.CashInHandTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.CashInHandTextBox.ClientID + ")");

        this.CashInHandTextBox.Attributes["onclick"] = "CashInHandKeyBoard(this.id)";
        this.RemarkTextBox.Attributes["onclick"] = "RemarkKeyBoard(this.id)";

        this.TotalSalesTextBox.Attributes.Add("ReadOnly", "True");
        this.TotalDebitTextBox.Attributes.Add("ReadOnly", "True");
        this.TotalCreditTextBox.Attributes.Add("ReadOnly", "True");
        this.TotalVoucherTextBox.Attributes.Add("ReadOnly", "True");
        this.TotalCashTextBox.Attributes.Add("ReadOnly", "True");
        this.BeginningCashTextBox.Attributes.Add("ReadOnly", "True");
        this.BalanceTextBox.Attributes.Add("ReadOnly", "True");
    }

    protected void ChangeVisiblePanel(Byte _prmValue)
    {
        if (_prmValue == 0)
        {
            this.FormPanel.Visible = true;
            this.PasswordPanel.Visible = false;
        }
        else if (_prmValue == 1)
        {
            this.FormPanel.Visible = true;
            this.PasswordPanel.Visible = true;
        }
    }

    protected void OKButton_Click(object sender, EventArgs e)
    {
        try
        {
            //String _employeeId = _userEmployeeBL.GetEmployeeIDByUserName(this.UserApproveTextBox.Text);
            //Guid _userId = _userEmployeeBL.GetUserIdEmpId(_employeeId);
            //Boolean _checkUserApprove = this._closeShiftBL.CheckUserApprove(_employeeId, _userId);

            String _password = this._pOSConfigurationBL.GetSingle("POSCancelPassword").SetValue;
            //if (_password.ToLower() == this.PasswordTextBox.Text.ToLower() & _checkUserApprove == true)
            if (_password.ToLower() == this.PasswordTextBox.Text.ToLower())
            {
                String _employeeId = this._userEmployeeBL.GetEmployeeIDByUserName(HttpContext.Current.User.Identity.Name);
                //Guid _userId = this._userEmployeeBL.GetUserIdEmpId(_employeeId);
                Guid _userId = this._userBL.GetUserIDByName(HttpContext.Current.User.Identity.Name);

                POSTrShiftLog _posTrShiftLog = _closeShiftBL.GetSinglePOSTrShiftLog(_employeeId, _userId);
                _posTrShiftLog.TotalSales = Convert.ToDecimal(this.TotalSalesTextBox.Text);
                _posTrShiftLog.TotalDebit = Convert.ToDecimal(this.TotalDebitTextBox.Text);
                _posTrShiftLog.TotalCredit = Convert.ToDecimal(this.TotalCreditTextBox.Text);
                _posTrShiftLog.TotalVoucher = Convert.ToDecimal(this.TotalVoucherTextBox.Text);
                _posTrShiftLog.TotalCash = Convert.ToDecimal(this.TotalCashTextBox.Text);
                _posTrShiftLog.CashOnHand = Convert.ToDecimal(this.CashInHandTextBox.Text);
                _posTrShiftLog.Balance = Convert.ToDecimal(this.BalanceTextBox.Text);
                _posTrShiftLog.Remark = this.RemarkTextBox.Text;
                //_posTrShiftLog.CloseShift = DateTime.Now;
                //_posTrShiftLog.ApprovedBy = this.UserApproveTextBox.Text;
                bool _result = this._closeShiftBL.EditShiftLog(_posTrShiftLog);
                if (_result == true)
                {
                    //_result = _closeShiftBL.UpDateCloseShift(_posTrShiftLog.ShiftLogCode,this.UserApproveTextBox.Text);
                    _result = _closeShiftBL.UpDateCloseShift(_posTrShiftLog.ShiftLogCode, HttpContext.Current.User.Identity.Name);
                    if (_result == true)
                        Response.Redirect("../Login.aspx");
                }
                else
                    this.WarningLabel.Text = "Close Shift Process is Failed.";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Incorrect User Approve or Password.');", true);
            }
            this.PasswordTextBox.Text = "";
        }
        catch (ThreadAbortException) { throw; }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }
}
