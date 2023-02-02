using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYTrTHRHd
    {
        private string _orgUnitName = "";

        public PAYTrTHRHd(string _prmTransNmbr, String _prmFileNmbr, char _prmStatus, DateTime _prmTransDate,
            DateTime _prmTHRDate, decimal _prmPercentPaid, char? _prmFgAllReligion, string _prmReligion1,
            string _prmReligion2, string _prmReligion3, string _prmReligion4, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.THRDate = _prmTHRDate;
            this.PercentPaid = _prmPercentPaid;
            this.FgAllReligion = _prmFgAllReligion;
            this.Religion1 = _prmReligion1;
            this.Religion2 = _prmReligion2;
            this.Religion3 = _prmReligion3;
            this.Religion4 = _prmReligion4;
            this.Remark = _prmRemark;
        }

        public PAYTrTHRHd(string _prmTransNmbr, String _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }
    }
}
