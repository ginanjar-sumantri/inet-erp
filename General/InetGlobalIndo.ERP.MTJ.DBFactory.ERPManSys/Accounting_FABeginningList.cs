using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Accounting_FABeginningList
    {
        private string _FAName = "";
        private int _totalLife = 0;
        private int _totalDepr = 0;
        private decimal _amountHome = 0;
        private decimal _amountDepr = 0;
        private decimal _amountCurrent = 0;

        public Accounting_FABeginningList(Guid _prmFABeginningListCode, Guid _prmFABeginningCode,
            string _prmFACode, string _prmFAName, int _prmTotalLife, int _prmTotalDepr,
            decimal _prmAmountHome, decimal _prmAmountDepr, decimal _prmAmountCurrent,
            string _prmInsertBy, DateTime _prmInsertDate)
        {
            this.FABeginningListCode = _prmFABeginningListCode;
            this.FABeginningCode = _prmFABeginningCode;
            this.FACode = _prmFACode;
            this.FAName = _prmFAName;
            this.TotalLife = _prmTotalLife;
            this.TotalDepr = _prmTotalDepr;
            this.AmountHome = _prmAmountHome;
            this.AmountDepr = _prmAmountDepr;
            this.AmountCurrent = _prmAmountCurrent;
            this.InsertBy = _prmInsertBy;
            this.InsertDate = _prmInsertDate;
        }

        public Accounting_FABeginningList(string _prmFACode, string _prmFAName, int _prmTotalLife, 
            int _prmTotalDepr, decimal _prmAmountHome, decimal _prmAmountDepr, decimal _prmAmountCurrent)
        {
            this.FACode = _prmFACode;
            this.FAName = _prmFAName;
            this.TotalLife = _prmTotalLife;
            this.TotalDepr = _prmTotalDepr;
            this.AmountHome = _prmAmountHome;
            this.AmountDepr = _prmAmountDepr;
            this.AmountCurrent = _prmAmountCurrent;
        }

        public Accounting_FABeginningList(string _prmFACode)
        {
            this.FACode = _prmFACode;
        }

        public string FAName
        {
            get
            {
                return this._FAName;
            }
            set
            {
                this._FAName = value;
            }
        }

        public int TotalLife
        {
            get
            {
                return this._totalLife;
            }
            set
            {
                this._totalLife = value;
            }
        }

        public int TotalDepr
        {
            get
            {
                return this._totalDepr;
            }
            set
            {
                this._totalDepr = value;
            }
        }

        public decimal AmountHome
        {
            get
            {
                return this._amountHome;
            }
            set
            {
                this._amountHome = value;
            }
        }

        public decimal AmountDepr
        {
            get
            {
                return this._amountDepr;
            }
            set
            {
                this._amountDepr = value;
            }
        }

        public decimal AmountCurrent
        {
            get
            {
                return this._amountCurrent;
            }
            set
            {
                this._amountCurrent = value;
            }
        }
    }
}
