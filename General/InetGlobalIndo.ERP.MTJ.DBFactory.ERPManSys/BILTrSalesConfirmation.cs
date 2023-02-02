using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class BILTrSalesConfirmation
    {
        private string _custName = "";
        private Boolean _fgNotify = false;
        private String _baFileNmbr = "";
        private DateTime _baTransDate = DateTime.Now;

        public BILTrSalesConfirmation(String _prmTransNmbr, String _prmFileNmbr, Byte _prmStatus, DateTime _prmTransDate, String _prmFormulirID,
            String _prmCustCode, string _prmCustName, string _prmCompanyName, string _prmCustBillAccount)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.FormulirID = _prmFormulirID;
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
            this.CompanyName = _prmCompanyName;
            this.CustBillAccount = _prmCustBillAccount;
        }

        public BILTrSalesConfirmation(String _prmTransNmbr, String _prmFileNmbr, DateTime _prmTransDate,
            String _prmCompanyName, String _prmRemark, Boolean _prmFgSoftBlock, Boolean _prmFgPending, Boolean _prmFgNotifiedBA)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.CompanyName = _prmCompanyName;
            this.Remark = _prmRemark;
            this.FgSoftBlockExec = _prmFgSoftBlock;
            this.FgPending = _prmFgPending;
            this.FgNotify = _prmFgNotifiedBA;
        }

        public BILTrSalesConfirmation(String _prmTransNmbr, String _prmFileNmbr, DateTime _prmTransDate,
            String _prmBAFileNmbr, DateTime _prmBATransDate, String _prmCompanyName, Boolean _prmFgPending, Boolean _prmFgNotifiedBA)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.BAFileNmbr = _prmBAFileNmbr;
            this.BATransDate = _prmBATransDate;
            this.CompanyName = _prmCompanyName;
            this.FgPending = _prmFgPending;
            this.FgNotify = _prmFgNotifiedBA;
        }

        public BILTrSalesConfirmation(String _prmTransNmbr, String _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }
        
        public string CustName
        {
            get
            {
                return this._custName;
            }
            set
            {
                this._custName = value;
            }
        }

        public string BAFileNmbr
        {
            get
            {
                return this._baFileNmbr;
            }
            set
            {
                this._baFileNmbr = value;
            }
        }

        public DateTime BATransDate
        {
            get
            {
                return this._baTransDate;
            }
            set
            {
                this._baTransDate = value;
            }
        }

        public Boolean FgNotify
        {
            get
            {
                return this._fgNotify;
            }
            set
            {
                this._fgNotify = value;
            }
        }
    }
}
