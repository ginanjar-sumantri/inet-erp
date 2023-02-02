using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrAppraisalHd
    {
        private string _empName = "";
        private string _purposeName = "";
        private string _empApprByName = "";

        public HRMTrAppraisalHd(string _prmTransNmbr, String _prmFileNmbr, char _prmStatus, DateTime _prmTransDate,
            String _prmPurposeCode, String _prmPurposeName, String _prmEmpNumb, String _prmEmpName, string _prmEmpApprBy, String _prmEmpApprByName,
            String _prmRecomendation, decimal _prmAverage)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.PurposeCode = _prmPurposeCode;
            this.PurposeName = _prmPurposeName;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.EmpApprBy = _prmEmpApprBy;
            this.EmpApprByName = _prmEmpApprByName;
            this.Recomendation = _prmRecomendation;
            this.Average = _prmAverage;
        }

        public HRMTrAppraisalHd(string _prmTransNmbr, String _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }

        public string EmpName
        {
            get
            {
                return this._empName;
            }
            set
            {
                this._empName = value;
            }
        }

        public string PurposeName
        {
            get
            {
                return this._purposeName;
            }
            set
            {
                this._purposeName = value;
            }
        }

        public string EmpApprByName
        {
            get
            {
                return this._empApprByName;
            }
            set
            {
                this._empApprByName = value;
            }
        }
    }
}
