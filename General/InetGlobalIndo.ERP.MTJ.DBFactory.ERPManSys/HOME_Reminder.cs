using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HOME_Reminder
    {
        private Int16 _menuID = 0;

        private String _transType = "";
        private String _transNmbr = "";
        private String _fileNmbr = "";
        private String _status = "";
        private String _reminderPath = "";

        private String _reminderName = "";
        private int _count = 0;
        private String _reminderCode = "";
        
        public HOME_Reminder() { }

        public HOME_Reminder(String _prmTransType, String _prmTransNmbr ,
            String _prmFileNmbr, String _prmStatus, String _prmReminderPath
            )
        {
            this.TransType = _prmTransType;
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.ReminderPath = _prmReminderPath;
        }

        public HOME_Reminder(String _prmReminderName, int _prmCount, String _prmReminderCode)
        {
            this.ReminderName = _prmReminderName;
            this.Count = _prmCount;
            this.ReminderCode = _prmReminderCode;
        }

        public Int16 MenuID
        {
            get
            {
                return this._menuID;
            }
            set
            {
                this._menuID = value;
            }
        }

        public String TransType
        {
            get
            {
                return this._transType;
            }
            set
            {
                this._transType = value;
            }
        }
        public String TransNmbr
        {
            get
            {
                return this._transNmbr;
            }
            set
            {
                this._transNmbr = value;
            }
        }
        public String FileNmbr
        {
            get
            {
                return this._fileNmbr;
            }
            set
            {
                this._fileNmbr = value;
            }
        }
        public String Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }
        public String ReminderPath
        {
            get
            {
                return this._reminderPath;
            }
            set
            {
                this._reminderPath = value;
            }
        }

        public String ReminderName
        {
            get
            {
                return this._reminderName;
            }
            set
            {
                this._reminderName = value;
            }
        }
        public int Count
        {
            get
            {
                return this._count;
            }
            set
            {
                this._count = value;
            }
        }
        public String ReminderCode
        {
            get
            {
                return this._reminderCode;
            }
            set
            {
                this._reminderCode = value;
            }
        }

        ~HOME_Reminder() { }
    }
}
