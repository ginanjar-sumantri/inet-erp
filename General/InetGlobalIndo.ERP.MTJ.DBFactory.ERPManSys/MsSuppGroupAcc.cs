using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsSuppGroupAcc
    {
        private string _accAPName = "";
        private string _accAPPendingName = "";
        private string _accDebitAPName = "";
        private string _accDPName = "";

        public MsSuppGroupAcc(string _prmCurrCode, string _prmAccAPName, string _prmAccAPPendingName, string _prmAccDebitAPName, string _prmAccDPName)
        {
            this.CurrCode = _prmCurrCode;
            this.AccAPName = _prmAccAPName;
            this.AccAPPendingName = _prmAccAPPendingName;
            this.AccDebitAPName = _prmAccDebitAPName;
            this.AccDPName = _prmAccDPName;
        }

        public string AccDPName
        {
            get
            {
                return this._accDPName;
            }

            set
            {
                this._accDPName = value;
            }
        }

        public string AccDebitAPName
        {
            get
            {
                return this._accDebitAPName;
            }

            set
            {
                this._accDebitAPName = value;
            }
        }

        public string AccAPPendingName
        {
            get
            {
                return this._accAPPendingName;
            }

            set
            {
                this._accAPPendingName = value;
            }
        }

        public string AccAPName
        {
            get
            {
                return this._accAPName;
            }

            set
            {
                this._accAPName = value;
            }
        }
    }
}
