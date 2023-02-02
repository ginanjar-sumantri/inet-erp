using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{

    public partial class GLJournalHd : Base
    {

        public GLJournalHd(string _prmReference, string _prmTransClass, string _prmFileNmbr, DateTime _prmTransDate, char _prmStatus, string _prmRemark, int? _prmYear, int? _prmPeriod)
        {
            this.Reference = _prmReference;
            this.TransClass = _prmTransClass;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.Remark = _prmRemark;
            this.Year = _prmYear;
            this.Period = _prmPeriod;
        }

        public GLJournalHd(string _prmTransClass)
        {
            this.TransClass = _prmTransClass;
        }

        public GLJournalHd(string _prmTransClass, String _prmTransTypeName)
        {
            this.TransClass = _prmTransClass;
            this.TransTypeName = _prmTransTypeName;
        }

        public String TransTypeName { get; set; }
    }
}
