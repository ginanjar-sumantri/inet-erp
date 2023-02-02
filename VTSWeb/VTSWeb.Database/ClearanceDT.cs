using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class ClearanceDt : Base
    {
        string _areaName = "";
        string _purposeName = "";
        DateTime _dateIn = DateTime.Now.Date;
        DateTime _dateOut = DateTime.Now.Date;
        TimeSpan _timeIn = DateTime.Now.TimeOfDay;
        TimeSpan _timeOut = DateTime.Now.TimeOfDay;


        public ClearanceDt(string _prmClearanceCode, string _prmAreaCode, string _prmPurposeCode, DateTime _prmCheckIn, DateTime _prmCheckOut)
        {
            this.ClearanceCode = _prmClearanceCode;
            this.AreaCode = _prmAreaCode;
            this.PurposeCode = _prmPurposeCode;
            this.CheckIn = _prmCheckIn;
            this.CheckOut = _prmCheckOut;
        }

        public ClearanceDt(string _prmClearanceCode, string _prmAreaCode, string _prmAreaName, string _prmPurposeCode, string _prmPurposeName, DateTime _prmDateIn, TimeSpan _prmTimeIn, DateTime _prmDateOut, TimeSpan _prmTimeOut)
        {
            this.ClearanceCode = _prmClearanceCode;
            this.AreaCode = _prmAreaCode;
            this.AreaName = _prmAreaName;
            this.PurposeCode = _prmPurposeCode;
            this.PurposeName = _prmPurposeName;
            this.DateIn = _prmDateIn;
            this.TimeIn = _prmTimeIn;
            this.DateOut = _prmDateOut;
            this.TimeOut = _prmTimeOut;
        }

        public string AreaName
        {
            get
            {
                return this._areaName;
            }
            set
            {
                this._areaName = value;
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

        public DateTime DateIn
        {
            get
            {
                return this._dateIn;
            }
            set
            {
                this._dateIn = value;
            }
        }

        public TimeSpan TimeIn
        {
            get
            {
                return this._timeIn;
            }
            set
            {
                this._timeIn = value;
            }
        }

        public DateTime DateOut
        {
            get
            {
                return this._dateOut;
            }
            set
            {
                this._dateOut = value;
            }
        }

        public TimeSpan TimeOut
        {
            get
            {
                return this._timeOut;
            }
            set
            {
                this._timeOut = value;
            }
        }
    }
}