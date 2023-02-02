using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class ClearanceHd
    {
        private string _ContactName = "";
        private string _CustName = "";

        public ClearanceHd(string _prmClearanceCode, DateTime _prmClearanceDate, string _prmCustomerCode, int _prmVisitorCode, string _prmRemark, byte _prmCompleteStatus)
        {
            this.ClearanceCode = _prmClearanceCode;
            this.ClearanceDate = _prmClearanceDate;
            this.CustomerCode = _prmCustomerCode;
            this.VisitorCode = _prmVisitorCode;
            this.Remark = _prmRemark;
            this.CompleteStatus = _prmCompleteStatus;
        }

        public ClearanceHd(string _prmClearanceCode, DateTime _prmClearanceDate, string _prmCustomerCode, string _prmCustName, int _prmVisitorCode, string _prmContactName, string _prmRemark, byte _prmCompleteStatus)
        {
            this.ClearanceCode = _prmClearanceCode;
            this.ClearanceDate = _prmClearanceDate;
            this.CustomerCode = _prmCustomerCode;
            this.CustName = _prmCustName;
            this.VisitorCode = _prmVisitorCode;
            this.ContactName = _prmContactName;
            this.Remark = _prmRemark;
            this.CompleteStatus = _prmCompleteStatus;
        }

        public string ContactName
        {
            get
            {
                return this._ContactName;
            }
            set
            {
                this._ContactName = value;
            }
        }

        public string CustName
        {
            get
            {
                return this._CustName;
            }
            set
            {
                this._CustName = value;
            }
        }
    }
}