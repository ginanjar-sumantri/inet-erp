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
    public partial class OpenShift : System.Web.UI.Page
    {
        private User_EmployeeBL _userEmployeeBL = new User_EmployeeBL();
        private EmployeeBL _employeeBL = new EmployeeBL();
        private CashierAccountBL _cashierAccountBL = new CashierAccountBL();
        private AccountBL _accountBL = new AccountBL();
        private CloseShiftBL _closeShiftBL = new CloseShiftBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack == true)
            {
                this.ClearData();
            }
            this.CashTextBoxt.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
            this.WarningLabel.Text = "";
        }

        protected void ClearData()
        {
            this.CashTextBoxt.Text = "";
        }

        protected void OkButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                POSTrShiftLog _posTrShiftLog = new POSTrShiftLog();

                _posTrShiftLog.CashierEmpNmbr = this._userEmployeeBL.GetEmployeeIDByUserName(HttpContext.Current.User.Identity.Name);
                _posTrShiftLog.UserId = this._userEmployeeBL.GetUserIdEmpId(_posTrShiftLog.CashierEmpNmbr);
                _posTrShiftLog.OpenShift = DateTime.Now;
                _posTrShiftLog.BeginningCash = this.CashTextBoxt.Text == "" ? 0 : Convert.ToDecimal(this.CashTextBoxt.Text);
                _posTrShiftLog.Account = this._cashierAccountBL.GetMemberNameByCode(_posTrShiftLog.CashierEmpNmbr);

                bool _result = this._closeShiftBL.AddShiftLog(_posTrShiftLog);
                if (_result == true)
                {
                    //this.WarningLabel.Text = "Your Success Open Shift";
                    //this.ClearData();
                    Response.Redirect("../Login.aspx");
                }
                else
                {
                    this.WarningLabel.Text = "Your Failed Open Shift";
                    this.ClearData();
                }
            }
            catch (ThreadAbortException) { throw; }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void errorhandler(Exception ex)
        {
            //ErrorHandler.Record(ex, EventLogEntryType.Error);
            String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "OPENSHIFT");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
        }
    }
}