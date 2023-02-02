using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_WorkingHourList
    {
        private string _workingHourName = "";

        public HRM_WorkingHourList(Guid _prmWorkingHourListCode, Guid _prmWorkingHourCode, string _prmWorkingHourName, byte _prmWorkDay, byte _prmBeginHour, byte _prmBeginMinute, byte _prmFinishHour, byte _prmFinishMinute, Boolean _prmIsNextDay, string _prmRemark)
        {
            this.WorkingHourListCode = _prmWorkingHourListCode;
            this.WorkingHourCode = _prmWorkingHourCode;
            this.WorkingHourName = _prmWorkingHourName;
            this.WorkDay = _prmWorkDay;
            this.BeginHour = _prmBeginHour;
            this.BeginMinute = _prmBeginMinute;
            this.FinishHour = _prmFinishHour;
            this.FinishMinute = _prmFinishMinute;
            this.IsNextDay = _prmIsNextDay;
            this.Remark = _prmRemark;
        }

        public string WorkingHourName
        {
            get
            {
                return this._workingHourName;
            }
            set
            {
                this._workingHourName = value;
            }
        }
    }
}
