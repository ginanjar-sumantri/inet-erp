using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_Absence
    {
        string _absenceGroupName = "";

        public Master_Absence(Guid _prmAbsenceCode, Guid _prmAbsenceGroupCode, string _prmAbsenceGroupName, string _prmAbsenceName, string _prmAbsenceDescription)
        {
            this.AbsenceCode = _prmAbsenceCode;
            this.AbsenceGroupCode = _prmAbsenceGroupCode;
            this.AbsenceGroupName = _prmAbsenceGroupName;
            this.AbsenceName = _prmAbsenceName;
            this.AbsenceDescription = _prmAbsenceDescription;
        }

        public Master_Absence(Guid _prmAbsenceCode, string _prmAbsenceName)
        {
            this.AbsenceCode = _prmAbsenceCode;
            this.AbsenceName = _prmAbsenceName;
        }

        public string AbsenceGroupName
        {
            get
            {
                return this._absenceGroupName;
            }
            set
            {
                this._absenceGroupName = value;
            }
        }
    }
}
