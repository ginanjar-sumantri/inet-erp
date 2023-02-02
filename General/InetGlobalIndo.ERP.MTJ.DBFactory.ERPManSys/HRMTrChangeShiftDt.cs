using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrChangeShiftDt
    {
        private string _fromEmpName = "";
        private string _fromShiftName = "";
        private string _toEmpName = "";
        private string _toShiftName = "";

        public HRMTrChangeShiftDt(String _prmTransNmbr, String _prmFromEmpNumb, String _prmFromEmpName, DateTime _prmFromDate,
            String _prmFromShift, String _prmFromShiftName, String _prmToEmpNumb, String _prmToEmpName, DateTime _prmToDate,
            String _prmToShift, String _prmToShiftName, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FromEmpNumb = _prmFromEmpNumb;
            this.FromEmpName = _prmFromEmpName;
            this.FromDate = _prmFromDate;
            this.FromShift = _prmFromShift;
            this.FromShiftName = _prmFromShiftName;
            this.ToEmpNumb = _prmToEmpNumb;
            this.ToEmpName = _prmToEmpName;
            this.ToDate = _prmToDate;
            this.ToShift = _prmToShift;
            this.ToShiftName = _prmToShiftName;
            this.Remark = _prmRemark;
        }

        public string FromEmpName
        {
            get
            {
                return this._fromEmpName;
            }
            set
            {
                this._fromEmpName = value;
            }
        }

        public string FromShiftName
        {
            get
            {
                return this._fromShiftName;
            }
            set
            {
                this._fromShiftName = value;
            }
        }

        public string ToEmpName
        {
            get
            {
                return this._toEmpName;
            }
            set
            {
                this._toEmpName = value;
            }
        }

        public string ToShiftName
        {
            get
            {
                return this._toShiftName;
            }
            set
            {
                this._toShiftName = value;
            }
        }
    }
}
