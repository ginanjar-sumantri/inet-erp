using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class ReminderDataModel1
    {
        private String _transName, _transNmbr, _fileNmbr, _status, _reminderPath;
        [Column(Storage = "_TransName", DbType = "varchar(50)")]
        public String TransName
        {
            get { return this._transName; }
            set { this._transName = value; }
        }
        [Column(Storage = "_TransNmbr", DbType = "varchar(50)")]
        public String TransNmbr
        {
            get { return this._transNmbr; }
            set { this._transNmbr = value; }
        }
        [Column(Storage = "_FileNmbr", DbType = "varchar(50)")]
        public String FileNmbr
        {
            get { return this._fileNmbr; }
            set { this._fileNmbr = value; }
        }
        [Column(Storage = "_Status", DbType = "varchar(50)")]
        public String Status
        {
            get { return this._status; }
            set { this._status = value; }
        }
        [Column(Storage = "_ReminderPath", DbType = "varchar(800)")]
        public String ReminderPath
        {
            get { return this._reminderPath; }
            set { this._reminderPath = value; }
        }
    }


    // harusnya return ISingleResult<dynamicSearchResult>, vs menghasilkan int

    public partial class dynamicSearchResult
    {
        private String _resultRow;

        [Column(Storage = "_resultRow", DbType = "varchar(8000)")]
        public String ResultRow
        {
            get
            {
                return this._resultRow;
            }
            set
            {
                if ((this._resultRow != value))
                {
                    this._resultRow = value;
                }
            }
        }
    }

    public partial class spHOME_ReminderResult
    {
        private int _reminderCount;

        [Column(Storage = "_reminderCount", DbType = "Int")]
        public int ReminderCount
        {
            get
            {
                return this._reminderCount;
            }
            set
            {
                if ((this._reminderCount != value))
                {
                    this._reminderCount = value;
                }
            }
        }
    }

    public partial class spHOME_ReminderResultTransactionApproval
    {
        private String _transNmbr, _fileNmbr, _status;

        [Column(Storage = "_transNmbr", DbType = "varchar(8000)")]
        public string TransNmbr
        {
            get
            {
                return this._transNmbr;
            }
            set
            {
                if ((this._transNmbr != value))
                {
                    this._transNmbr = value;
                }
            }
        }

        [Column(Storage = "_fileNmbr", DbType = "varchar(8000)")]
        public string FileNmbr
        {
            get
            {
                return this._fileNmbr;
            }
            set
            {
                if ((this._fileNmbr != value))
                {
                    this._fileNmbr = value;
                }
            }
        }

        [Column(Storage = "_status", DbType = "varchar(8000)")]
        public string Status
        {
            get
            {
                return this._status;
            }
            set
            {
                if ((this._status != value))
                {
                    this._status = value;
                }
            }
        }

    }

    public partial class S_GLFAProcessFormulaResult
    {

        private string _FACode;

        private string _FAName;

        private decimal _BalanceAmount;

        private System.Nullable<int> _BalanceLife;

        private System.Nullable<decimal> _Amount;

        public S_GLFAProcessFormulaResult()
        {
        }

        [Column(Storage = "_FACode", DbType = "VarChar(20) NOT NULL", CanBeNull = false)]
        public string FACode
        {
            get
            {
                return this._FACode;
            }
            set
            {
                if ((this._FACode != value))
                {
                    this._FACode = value;
                }
            }
        }

        [Column(Storage = "_FAName", DbType = "VarChar(80) NOT NULL", CanBeNull = false)]
        public string FAName
        {
            get
            {
                return this._FAName;
            }
            set
            {
                if ((this._FAName != value))
                {
                    this._FAName = value;
                }
            }
        }

        [Column(Storage = "_BalanceAmount", DbType = "Decimal(0,0) NOT NULL")]
        public decimal BalanceAmount
        {
            get
            {
                return this._BalanceAmount;
            }
            set
            {
                if ((this._BalanceAmount != value))
                {
                    this._BalanceAmount = value;
                }
            }
        }

        [Column(Storage = "_BalanceLife", DbType = "Int")]
        public System.Nullable<int> BalanceLife
        {
            get
            {
                return this._BalanceLife;
            }
            set
            {
                if ((this._BalanceLife != value))
                {
                    this._BalanceLife = value;
                }
            }
        }

        [Column(Storage = "_Amount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> Amount
        {
            get
            {
                return this._Amount;
            }
            set
            {
                if ((this._Amount != value))
                {
                    this._Amount = value;
                }
            }
        }
    }

    public partial class spPAY_KlikBCAFileResult
    {
        private String _data;

        [Column(Storage = "_data", DbType = "varchar(8000)")]
        public String Data
        {
            get
            {
                return this._data;
            }
            set
            {
                if ((this._data != value))
                {
                    this._data = value;
                }
            }
        }
    }

    public partial class spPAY_THRGetDtResult
    {
        private string _EmpNumb;

        private string _EmpName;

        private string _Currency;

        private System.Nullable<DateTime> _HireDate;

        private string _SMasaKerja;

        private string _SBulan;

        private System.Nullable<Decimal> _MasaKerja;

        private string _TransNmbr;

        private string _Method;

        private System.Nullable<int> _xPeriod;

        private string _EmpStatus;

        private System.Nullable<char> _FgPermanent;

        private System.Nullable<Decimal> _TotalGP;

        private System.Nullable<Decimal> _TotalTT;

        private System.Nullable<Decimal> _Formula;

        private System.Nullable<Decimal> _TotalTHR;

        private System.Nullable<Decimal> _TotalPaid;

        private string _JobLevel;

        private string _JobTitle;

        public spPAY_THRGetDtResult()
        {
        }

        [Column(Storage = "_EmpNumb", DbType = "VarChar(15)")]
        public string EmpNumb
        {
            get
            {
                return this._EmpNumb;
            }
            set
            {
                if ((this._EmpNumb != value))
                {
                    this._EmpNumb = value;
                }
            }
        }

        [Column(Storage = "_EmpName", DbType = "VarChar(60)")]
        public string EmpName
        {
            get
            {
                return this._EmpName;
            }
            set
            {
                if ((this._EmpName != value))
                {
                    this._EmpName = value;
                }
            }
        }

        [Column(Storage = "_Currency", DbType = "VarChar(5)")]
        public string Currency
        {
            get
            {
                return this._Currency;
            }
            set
            {
                if ((this._Currency != value))
                {
                    this._Currency = value;
                }
            }
        }

        [Column(Storage = "_HireDate", DbType = "Datetime")]
        public System.Nullable<DateTime> HireDate
        {
            get
            {
                return this._HireDate;
            }
            set
            {
                if ((this._HireDate != value))
                {
                    this._HireDate = value;
                }
            }
        }

        [Column(Storage = "_SMasaKerja", DbType = "VarChar(30)")]
        public string SMasaKerja
        {
            get
            {
                return this._SMasaKerja;
            }
            set
            {
                if ((this._SMasaKerja != value))
                {
                    this._SMasaKerja = value;
                }
            }
        }

        [Column(Storage = "_SBulan", DbType = "VarChar(20)")]
        public string SBulan
        {
            get
            {
                return this._SBulan;
            }
            set
            {
                if ((this._SBulan != value))
                {
                    this._SBulan = value;
                }
            }
        }

        [Column(Storage = "_MasaKerja", DbType = "Decimal(18,2)")]
        public System.Nullable<Decimal> MasaKerja
        {
            get
            {
                return this._MasaKerja;
            }
            set
            {
                if ((this._MasaKerja != value))
                {
                    this._MasaKerja = value;
                }
            }
        }

        [Column(Storage = "_TransNmbr", DbType = "Varchar(20)")]
        public string TransNmbr
        {
            get
            {
                return this._TransNmbr;
            }
            set
            {
                if ((this._TransNmbr != value))
                {
                    this._TransNmbr = value;
                }
            }
        }

        [Column(Storage = "_Method", DbType = "Varchar(5)")]
        public string Method
        {
            get
            {
                return this._Method;
            }
            set
            {
                if ((this._Method != value))
                {
                    this._Method = value;
                }
            }
        }

        [Column(Storage = "_xPeriod", DbType = "INT")]
        public System.Nullable<int> xPeriod
        {
            get
            {
                return this._xPeriod;
            }
            set
            {
                if ((this._xPeriod != value))
                {
                    this._xPeriod = value;
                }
            }
        }

        [Column(Storage = "_EmpStatus", DbType = "Varchar(10)")]
        public string EmpStatus
        {
            get
            {
                return this._EmpStatus;
            }
            set
            {
                if ((this._EmpStatus != value))
                {
                    this._EmpStatus = value;
                }
            }
        }

        [Column(Storage = "_FgPermanent", DbType = "Varchar(1)")]
        public System.Nullable<char> FgPermanent
        {
            get
            {
                return this._FgPermanent;
            }
            set
            {
                if ((this._FgPermanent != value))
                {
                    this._FgPermanent = value;
                }
            }
        }

        [Column(Storage = "_TotalGP", DbType = "Decimal(18,4)")]
        public System.Nullable<Decimal> TotalGP
        {
            get
            {
                return this._TotalGP;
            }
            set
            {
                if ((this._TotalGP != value))
                {
                    this._TotalGP = value;
                }
            }
        }

        [Column(Storage = "_TotalTT", DbType = "Decimal(18,4)")]
        public System.Nullable<Decimal> TotalTT
        {
            get
            {
                return this._TotalTT;
            }
            set
            {
                if ((this._TotalTT != value))
                {
                    this._TotalTT = value;
                }
            }
        }

        [Column(Storage = "_Formula", DbType = "Decimal(18,6)")]
        public System.Nullable<Decimal> Formula
        {
            get
            {
                return this._Formula;
            }
            set
            {
                if ((this._Formula != value))
                {
                    this._Formula = value;
                }
            }
        }

        [Column(Storage = "_TotalTHR", DbType = "Decimal(18,4)")]
        public System.Nullable<Decimal> TotalTHR
        {
            get
            {
                return this._TotalTHR;
            }
            set
            {
                if ((this._TotalTHR != value))
                {
                    this._TotalTHR = value;
                }
            }
        }

        [Column(Storage = "_TotalPaid", DbType = "Decimal(18,4)")]
        public System.Nullable<Decimal> TotalPaid
        {
            get
            {
                return this._TotalPaid;
            }
            set
            {
                if ((this._TotalPaid != value))
                {
                    this._TotalPaid = value;
                }
            }
        }

        [Column(Storage = "_JobLevel", DbType = "Varchar(10)")]
        public string JobLevel
        {
            get
            {
                return this._JobLevel;
            }
            set
            {
                if ((this._JobLevel != value))
                {
                    this._JobLevel = value;
                }
            }
        }

        [Column(Storage = "_JobTitle", DbType = "Varchar(40)")]
        public string JobTitle
        {
            get
            {
                return this._JobTitle;
            }
            set
            {
                if ((this._JobTitle != value))
                {
                    this._JobTitle = value;
                }
            }
        }
    }

    public partial class spHRM_TrLeaveProcessGetDtResult
    {
        private string _EmpNumb;

        private string _EmpName;

        private System.Nullable<DateTime> _StartDate;

        private System.Nullable<DateTime> _StartEffective;

        private System.Nullable<DateTime> _EndEffective;

        private System.Nullable<int> _DefaultTotal;

        private System.Nullable<int> _Deduction;

        private System.Nullable<int> _Total;

        public spHRM_TrLeaveProcessGetDtResult()
        {
        }

        [Column(Storage = "_EmpNumb", DbType = "VarChar(20)")]
        public string EmpNumb
        {
            get
            {
                return this._EmpNumb;
            }
            set
            {
                if ((this._EmpNumb != value))
                {
                    this._EmpNumb = value;
                }
            }
        }

        [Column(Storage = "_EmpName", DbType = "VarChar(60)")]
        public string EmpName
        {
            get
            {
                return this._EmpName;
            }
            set
            {
                if ((this._EmpName != value))
                {
                    this._EmpName = value;
                }
            }
        }

        [Column(Storage = "_StartDate", DbType = "Datetime")]
        public System.Nullable<DateTime> StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                if ((this._StartDate != value))
                {
                    this._StartDate = value;
                }
            }
        }

        [Column(Storage = "_StartEffective", DbType = "Datetime")]
        public System.Nullable<DateTime> StartEffective
        {
            get
            {
                return this._StartEffective;
            }
            set
            {
                if ((this._StartEffective != value))
                {
                    this._StartEffective = value;
                }
            }
        }

        [Column(Storage = "_EndEffective", DbType = "Datetime")]
        public System.Nullable<DateTime> EndEffective
        {
            get
            {
                return this._EndEffective;
            }
            set
            {
                if ((this._EndEffective != value))
                {
                    this._EndEffective = value;
                }
            }
        }

        [Column(Storage = "_DefaultTotal", DbType = "INT")]
        public System.Nullable<int> DefaultTotal
        {
            get
            {
                return this._DefaultTotal;
            }
            set
            {
                if ((this._DefaultTotal != value))
                {
                    this._DefaultTotal = value;
                }
            }
        }

        [Column(Storage = "_Deduction", DbType = "INT")]
        public System.Nullable<int> Deduction
        {
            get
            {
                return this._Deduction;
            }
            set
            {
                if ((this._Deduction != value))
                {
                    this._Deduction = value;
                }
            }
        }

        [Column(Storage = "_Total", DbType = "INT")]
        public System.Nullable<int> Total
        {
            get
            {
                return this._Total;
            }
            set
            {
                if ((this._Total != value))
                {
                    this._Total = value;
                }
            }
        }
    }

    public partial class spHRM_ValidateSalaryAbsResult
    {
        private String _EmpNumb;
        private String _EmpName;
        private System.Nullable<DateTime> _TransDate;
        private String _EmpGroup;
        private String _Shift;
        private System.Nullable<char> _FgSchedule;
        private System.Nullable<char> _FgAbsence;

        [Column(Storage = "_EmpNumb", DbType = "varchar(20)")]
        public String EmpNumb
        {
            get
            {
                return this._EmpNumb;
            }
            set
            {
                if ((this._EmpNumb != value))
                {
                    this._EmpNumb = value;
                }
            }
        }

        [Column(Storage = "_EmpName", DbType = "varchar(60)")]
        public String EmpName
        {
            get
            {
                return this._EmpName;
            }
            set
            {
                if ((this._EmpName != value))
                {
                    this._EmpName = value;
                }
            }
        }

        [Column(Storage = "_TransDate", DbType = "Datetime")]
        public System.Nullable<DateTime> TransDate
        {
            get
            {
                return this._TransDate;
            }
            set
            {
                if ((this._TransDate != value))
                {
                    this._TransDate = value;
                }
            }
        }

        [Column(Storage = "_EmpGroup", DbType = "varchar(50)")]
        public String EmpGroup
        {
            get
            {
                return this._EmpGroup;
            }
            set
            {
                if ((this._EmpGroup != value))
                {
                    this._EmpGroup = value;
                }
            }
        }

        [Column(Storage = "_Shift", DbType = "varchar(50)")]
        public String Shift
        {
            get
            {
                return this._Shift;
            }
            set
            {
                if ((this._Shift != value))
                {
                    this._Shift = value;
                }
            }
        }

        [Column(Storage = "_FgSchedule", DbType = "Varchar(1)")]
        public System.Nullable<char> FgSchedule
        {
            get
            {
                return this._FgSchedule;
            }
            set
            {
                if ((this._FgSchedule != value))
                {
                    this._FgSchedule = value;
                }
            }
        }

        [Column(Storage = "_FgAbsence", DbType = "Varchar(1)")]
        public System.Nullable<char> FgAbsence
        {
            get
            {
                return this._FgAbsence;
            }
            set
            {
                if ((this._FgAbsence != value))
                {
                    this._FgAbsence = value;
                }
            }
        }
    }

    public partial class spHRM_ValidateSalaryEmpResult
    {
        private String _EmpNumb;
        private String _EmpName;
        private System.Nullable<DateTime> _StartDate;
        private String _JobTitle;
        private String _JobLevel;
        private String _EmpGroup;

        [Column(Storage = "_EmpNumb", DbType = "varchar(12)")]
        public String EmpNumb
        {
            get
            {
                return this._EmpNumb;
            }
            set
            {
                if ((this._EmpNumb != value))
                {
                    this._EmpNumb = value;
                }
            }
        }

        [Column(Storage = "_EmpName", DbType = "varchar(60)")]
        public String EmpName
        {
            get
            {
                return this._EmpName;
            }
            set
            {
                if ((this._EmpName != value))
                {
                    this._EmpName = value;
                }
            }
        }

        [Column(Storage = "_StartDate", DbType = "Datetime")]
        public System.Nullable<DateTime> StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                if ((this._StartDate != value))
                {
                    this._StartDate = value;
                }
            }
        }

        [Column(Storage = "_JobTitle", DbType = "varchar(60)")]
        public String JobTitle
        {
            get
            {
                return this._JobTitle;
            }
            set
            {
                if ((this._JobTitle != value))
                {
                    this._JobTitle = value;
                }
            }
        }

        [Column(Storage = "_JobLevel", DbType = "varchar(60)")]
        public String JobLevel
        {
            get
            {
                return this._JobLevel;
            }
            set
            {
                if ((this._JobLevel != value))
                {
                    this._JobLevel = value;
                }
            }
        }

        [Column(Storage = "_EmpGroup", DbType = "varchar(50)")]
        public String EmpGroup
        {
            get
            {
                return this._EmpGroup;
            }
            set
            {
                if ((this._EmpGroup != value))
                {
                    this._EmpGroup = value;
                }
            }
        }
    }

    public partial class spHRM_ValidateSalaryOvtResult
    {
        private String _TransNmbr;
        private System.Nullable<DateTime> _TransDate;
        private System.Nullable<char> _Status;
        private String _DayType;
        private String _EmpNumb;
        private String _EmpName;
        private String _StartHours;
        private String _EndHours;

        [Column(Storage = "_TransNmbr", DbType = "varchar(20)")]
        public String TransNmbr
        {
            get
            {
                return this._TransNmbr;
            }
            set
            {
                if ((this._TransNmbr != value))
                {
                    this._TransNmbr = value;
                }
            }
        }

        [Column(Storage = "_TransDate", DbType = "Datetime")]
        public System.Nullable<DateTime> TransDate
        {
            get
            {
                return this._TransDate;
            }
            set
            {
                if ((this._TransDate != value))
                {
                    this._TransDate = value;
                }
            }
        }

        [Column(Storage = "_Status", DbType = "Varchar(1)")]
        public System.Nullable<char> Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                if ((this._Status != value))
                {
                    this._Status = value;
                }
            }
        }

        [Column(Storage = "_DayType", DbType = "varchar(15)")]
        public String DayType
        {
            get
            {
                return this._DayType;
            }
            set
            {
                if ((this._DayType != value))
                {
                    this._DayType = value;
                }
            }
        }

        [Column(Storage = "_EmpNumb", DbType = "varchar(20)")]
        public String EmpNumb
        {
            get
            {
                return this._EmpNumb;
            }
            set
            {
                if ((this._EmpNumb != value))
                {
                    this._EmpNumb = value;
                }
            }
        }

        [Column(Storage = "_EmpName", DbType = "varchar(60)")]
        public String EmpName
        {
            get
            {
                return this._EmpName;
            }
            set
            {
                if ((this._EmpName != value))
                {
                    this._EmpName = value;
                }
            }
        }


        [Column(Storage = "_StartHours", DbType = "varchar(5)")]
        public String StartHours
        {
            get
            {
                return this._StartHours;
            }
            set
            {
                if ((this._StartHours != value))
                {
                    this._StartHours = value;
                }
            }
        }

        [Column(Storage = "_EndHours", DbType = "varchar(5)")]
        public String EndHours
        {
            get
            {
                return this._EndHours;
            }
            set
            {
                if ((this._EndHours != value))
                {
                    this._EndHours = value;
                }
            }
        }
    }

    public partial class spHRM_ValidateSalaryScheduleResult
    {
        private String _EmpNumb;
        private String _EmpName;
        private System.Nullable<DateTime> _StartDate;
        private String _EmpGroup;

        [Column(Storage = "_EmpNumb", DbType = "varchar(20)")]
        public String EmpNumb
        {
            get
            {
                return this._EmpNumb;
            }
            set
            {
                if ((this._EmpNumb != value))
                {
                    this._EmpNumb = value;
                }
            }
        }

        [Column(Storage = "_EmpName", DbType = "varchar(60)")]
        public String EmpName
        {
            get
            {
                return this._EmpName;
            }
            set
            {
                if ((this._EmpName != value))
                {
                    this._EmpName = value;
                }
            }
        }

        [Column(Storage = "_StartDate", DbType = "Datetime")]
        public System.Nullable<DateTime> StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                if ((this._StartDate != value))
                {
                    this._StartDate = value;
                }
            }
        }

        [Column(Storage = "_EmpGroup", DbType = "varchar(50)")]
        public String EmpGroup
        {
            get
            {
                return this._EmpGroup;
            }
            set
            {
                if ((this._EmpGroup != value))
                {
                    this._EmpGroup = value;
                }
            }
        }
    }

    public partial class spHRM_ValidateSalarySlipResult
    {
        private String _EmpNumb;
        private String _EmpName;
        private System.Nullable<DateTime> _StartDate;
        private String _JobTitle;
        private String _SKNo;

        [Column(Storage = "_EmpNumb", DbType = "varchar(12)")]
        public String EmpNumb
        {
            get
            {
                return this._EmpNumb;
            }
            set
            {
                if ((this._EmpNumb != value))
                {
                    this._EmpNumb = value;
                }
            }
        }

        [Column(Storage = "_EmpName", DbType = "varchar(60)")]
        public String EmpName
        {
            get
            {
                return this._EmpName;
            }
            set
            {
                if ((this._EmpName != value))
                {
                    this._EmpName = value;
                }
            }
        }

        [Column(Storage = "_StartDate", DbType = "Datetime")]
        public System.Nullable<DateTime> StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                if ((this._StartDate != value))
                {
                    this._StartDate = value;
                }
            }
        }

        [Column(Storage = "_JobTitle", DbType = "varchar(60)")]
        public String JobTitle
        {
            get
            {
                return this._JobTitle;
            }
            set
            {
                if ((this._JobTitle != value))
                {
                    this._JobTitle = value;
                }
            }
        }

        [Column(Storage = "_SKNo", DbType = "varchar(20)")]
        public String SKNo
        {
            get
            {
                return this._SKNo;
            }
            set
            {
                if ((this._SKNo != value))
                {
                    this._SKNo = value;
                }
            }
        }
    }

    //public partial class S_GLFADevaluationGetDtResult
    //{
    //    private string _FA_Code, _FA_Name, _FA_Sub_Group;
    //    private System.Nullable<int> _LifeCurrent;
    //    private System.Nullable<Decimal> _AmountCurrent;

    //    [Column(Storage = "_FA_Code", DbType = "VarChar(20)")]
    //    public string FA_Code
    //    {
    //        get
    //        {
    //            return this._FA_Code;
    //        }
    //        set
    //        {
    //            if ((this._FA_Code != value))
    //            {
    //                this._FA_Code = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_FA_Name", DbType = "VarChar(80)")]
    //    public string FA_Name
    //    {
    //        get
    //        {
    //            return this._FA_Name;
    //        }
    //        set
    //        {
    //            if ((this._FA_Name != value))
    //            {
    //                this._FA_Name = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_LifeCurrent", DbType = "INT")]
    //    public System.Nullable<int> LifeCurrent
    //    {
    //        get
    //        {
    //            return this._LifeCurrent;
    //        }
    //        set
    //        {
    //            if ((this._LifeCurrent != value))
    //            {
    //                this._LifeCurrent = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_AmountCurrent", DbType = "Decimal(18,2)")]
    //    public System.Nullable<Decimal> AmountCurrent
    //    {
    //        get
    //        {
    //            return this._AmountCurrent;
    //        }
    //        set
    //        {
    //            if ((this._AmountCurrent != value))
    //            {
    //                this._AmountCurrent = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_FA_Sub_Group", DbType = "VarChar(50)")]
    //    public string FA_Sub_Group
    //    {
    //        get
    //        {
    //            return this._FA_Sub_Group;
    //        }
    //        set
    //        {
    //            if ((this._FA_Sub_Group != value))
    //            {
    //                this._FA_Sub_Group = value;
    //            }
    //        }
    //    }
    //}
    
    //pos
    //public partial class spPOS_TrAllResult
    //{
    //    private String _TransType;
    //    private String _TransNmbr;
    //    private String _ProductCode;
    //    private String _ProductName;
    //    private int _Qty;

    //    [Column(Storage = "_TransType", DbType = "varchar(50)")]
    //    public String TransType
    //    {
    //        get
    //        {
    //            return this._TransType;
    //        }
    //        set
    //        {
    //            if ((this._TransType != value))
    //            {
    //                this._TransType = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_TransNmbr", DbType = "varchar(50)")]
    //    public String TransNmbr
    //    {
    //        get
    //        {
    //            return this._TransNmbr;
    //        }
    //        set
    //        {
    //            if ((this._TransNmbr != value))
    //            {
    //                this._TransNmbr = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_ProductCode", DbType = "varchar(50)")]
    //    public String ProductCode
    //    {
    //        get
    //        {
    //            return this._ProductCode;
    //        }
    //        set
    //        {
    //            if ((this._ProductCode != value))
    //            {
    //                this._ProductCode = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_ProductName", DbType = "varchar(50)")]
    //    public String ProductName
    //    {
    //        get
    //        {
    //            return this._ProductName;
    //        }
    //        set
    //        {
    //            if ((this._ProductName != value))
    //            {
    //                this._ProductName = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Qty", DbType = "Int")]
    //    public int Qty
    //    {
    //        get
    //        {
    //            return this._Qty;
    //        }
    //        set
    //        {
    //            if ((this._Qty != value))
    //            {
    //                this._Qty = value;
    //            }
    //        }
    //    }
    //}

    //public partial class spPOS_GetAllDiscountNonMemberResult
    //{
    //    private String _TransNmbr;
    //    private System.Nullable<DateTime> _TransDate;
    //    private String _TransType;
    //    private String _ProductCode;
    //    private Decimal _TotalDisconItem;
    //    private Decimal _TotalDisconSubtotal;
    //    private String _TypeDiscon;

    //    [Column(Storage = "_TransNmbr", DbType = "varchar(50)")]
    //    public String TransNmbr
    //    {
    //        get
    //        {
    //            return this._TransNmbr;
    //        }
    //        set
    //        {
    //            if ((this._TransNmbr != value))
    //            {
    //                this._TransNmbr = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_TransDate", DbType = "Datetime")]
    //    public System.Nullable<DateTime> TransDate
    //    {
    //        get
    //        {
    //            return this._TransDate;
    //        }
    //        set
    //        {
    //            if ((this._TransDate != value))
    //            {
    //                this._TransDate = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_TransType", DbType = "varchar(50)")]
    //    public String TransType
    //    {
    //        get
    //        {
    //            return this._TransType;
    //        }
    //        set
    //        {
    //            if ((this._TransType != value))
    //            {
    //                this._TransType = value;
    //            }
    //        }
    //    }


    //    [Column(Storage = "_ProductCode", DbType = "varchar(50)")]
    //    public String ProductCode
    //    {
    //        get
    //        {
    //            return this._ProductCode;
    //        }
    //        set
    //        {
    //            if ((this._ProductCode != value))
    //            {
    //                this._ProductCode = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_TotalDisconItem", DbType = "Money")]
    //    public Decimal TotalDisconItem
    //    {
    //        get
    //        {
    //            return this._TotalDisconItem;
    //        }
    //        set
    //        {
    //            if ((this._TotalDisconItem != value))
    //            {
    //                this._TotalDisconItem = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_TotalDisconSubtotal", DbType = "Money")]
    //    public Decimal TotalDisconSubtotal
    //    {
    //        get
    //        {
    //            return this._TotalDisconSubtotal;
    //        }
    //        set
    //        {
    //            if ((this._TotalDisconSubtotal != value))
    //            {
    //                this._TotalDisconSubtotal = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_TypeDiscon", DbType = "varchar(50)")]
    //    public String TypeDiscon
    //    {
    //        get
    //        {
    //            return this._TypeDiscon;
    //        }
    //        set
    //        {
    //            if ((this._TypeDiscon != value))
    //            {
    //                this._TypeDiscon = value;
    //            }
    //        }
    //    }
    //}

    //public partial class spPOS_GetAllDiscountMemberResult
    //{
    //    private String _TransNmbr;
    //    private System.Nullable<DateTime> _TransDate;
    //    private String _TransType;
    //    private String _ProductCode;
    //    private Decimal _TotalDisconItem;
    //    private Decimal _TotalDisconSubtotal;
    //    private String _TypeDiscon;

    //    [Column(Storage = "_TransNmbr", DbType = "varchar(50)")]
    //    public String TransNmbr
    //    {
    //        get
    //        {
    //            return this._TransNmbr;
    //        }
    //        set
    //        {
    //            if ((this._TransNmbr != value))
    //            {
    //                this._TransNmbr = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_TransDate", DbType = "Datetime")]
    //    public System.Nullable<DateTime> TransDate
    //    {
    //        get
    //        {
    //            return this._TransDate;
    //        }
    //        set
    //        {
    //            if ((this._TransDate != value))
    //            {
    //                this._TransDate = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_TransType", DbType = "varchar(50)")]
    //    public String TransType
    //    {
    //        get
    //        {
    //            return this._TransType;
    //        }
    //        set
    //        {
    //            if ((this._TransType != value))
    //            {
    //                this._TransType = value;
    //            }
    //        }
    //    }


    //    [Column(Storage = "_ProductCode", DbType = "varchar(50)")]
    //    public String ProductCode
    //    {
    //        get
    //        {
    //            return this._ProductCode;
    //        }
    //        set
    //        {
    //            if ((this._ProductCode != value))
    //            {
    //                this._ProductCode = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_TotalDisconItem", DbType = "Money")]
    //    public Decimal TotalDisconItem
    //    {
    //        get
    //        {
    //            return this._TotalDisconItem;
    //        }
    //        set
    //        {
    //            if ((this._TotalDisconItem != value))
    //            {
    //                this._TotalDisconItem = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_TotalDisconSubtotal", DbType = "Money")]
    //    public Decimal TotalDisconSubtotal
    //    {
    //        get
    //        {
    //            return this._TotalDisconSubtotal;
    //        }
    //        set
    //        {
    //            if ((this._TotalDisconSubtotal != value))
    //            {
    //                this._TotalDisconSubtotal = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_TypeDiscon", DbType = "varchar(50)")]
    //    public String TypeDiscon
    //    {
    //        get
    //        {
    //            return this._TypeDiscon;
    //        }
    //        set
    //        {
    //            if ((this._TypeDiscon != value))
    //            {
    //                this._TypeDiscon = value;
    //            }
    //        }
    //    }
    //}

    //public partial class spPOS_MsShippingResult
    //{
    //    private String _VendorCode;
    //    private String _ShippingTypeCode;
    //    private String _ProductShape;
    //    private String _CityCode;
    //    private String _VendorName;
    //    private String _ShippingTypeName;
    //    private String _CityName;
    //    private Decimal _Percentage;
    //    private Decimal _Price1;
    //    private Decimal _Price2;
    //    private String _EstimationTime;
    //    private String _UnitCode;

    //    [Column(Storage = "_VendorCode", DbType = "varchar(12)")]
    //    public String VendorCode
    //    {
    //        get
    //        {
    //            return this._VendorCode;
    //        }
    //        set
    //        {
    //            if ((this._VendorCode != value))
    //            {
    //                this._VendorCode = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_ShippingTypeCode", DbType = "varchar(12)")]
    //    public String ShippingTypeCode
    //    {
    //        get
    //        {
    //            return this._ShippingTypeCode;
    //        }
    //        set
    //        {
    //            if ((this._ShippingTypeCode != value))
    //            {
    //                this._ShippingTypeCode = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_ProductShape", DbType = "char(1)")]
    //    public String ProductShape
    //    {
    //        get
    //        {
    //            return this._ProductShape;
    //        }
    //        set
    //        {
    //            if ((this._ProductShape != value))
    //            {
    //                this._ProductShape = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CityCode", DbType = "varchar(12)")]
    //    public String CityCode
    //    {
    //        get
    //        {
    //            return this._CityCode;
    //        }
    //        set
    //        {
    //            if ((this._CityCode != value))
    //            {
    //                this._CityCode = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_VendorName", DbType = "varchar(60)")]
    //    public String VendorName
    //    {
    //        get
    //        {
    //            return this._VendorName;
    //        }
    //        set
    //        {
    //            if ((this._VendorName != value))
    //            {
    //                this._VendorName = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_ShippingTypeName", DbType = "varchar(60)")]
    //    public String ShippingTypeName
    //    {
    //        get
    //        {
    //            return this._ShippingTypeName;
    //        }
    //        set
    //        {
    //            if ((this._ShippingTypeName != value))
    //            {
    //                this._ShippingTypeName = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CityName", DbType = "varchar(60)")]
    //    public String CityName
    //    {
    //        get
    //        {
    //            return this._CityName;
    //        }
    //        set
    //        {
    //            if ((this._CityName != value))
    //            {
    //                this._CityName = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Percentage", DbType = "Numeric")]
    //    public Decimal Percentage
    //    {
    //        get
    //        {
    //            return this._Percentage;
    //        }
    //        set
    //        {
    //            if ((this._Percentage != value))
    //            {
    //                this._Percentage = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Price1", DbType = "Numeric")]
    //    public Decimal Price1
    //    {
    //        get
    //        {
    //            return this._Price1;
    //        }
    //        set
    //        {
    //            if ((this._Price1 != value))
    //            {
    //                this._Price1 = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Price2", DbType = "Numeric")]
    //    public Decimal Price2
    //    {
    //        get
    //        {
    //            return this._Price2;
    //        }
    //        set
    //        {
    //            if ((this._Price2 != value))
    //            {
    //                this._Price2 = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_EstimationTime", DbType = "varchar(5)")]
    //    public String EstimationTime
    //    {
    //        get
    //        {
    //            return this._EstimationTime;
    //        }
    //        set
    //        {
    //            if ((this._EstimationTime != value))
    //            {
    //                this._EstimationTime = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_UnitCode", DbType = "varchar(12)")]
    //    public String UnitCode
    //    {
    //        get
    //        {
    //            return this._UnitCode;
    //        }
    //        set
    //        {
    //            if ((this._UnitCode != value))
    //            {
    //                this._UnitCode = value;
    //            }
    //        }
    //    }

    //}
}
//rem by boby
//harusnya mengembalikan integer tapi hasil oleh vs isingle result <sedang dicoba, jd tdk perlu diubah SpSAL_DirectSalesUnPost>
