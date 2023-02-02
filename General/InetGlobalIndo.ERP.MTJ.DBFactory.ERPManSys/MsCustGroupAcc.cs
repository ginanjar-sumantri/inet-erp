using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsCustGroupAcc
    {
        private string _accARName = "";
        private string _accDiscName = "";
        private string _accDPName = "";
        private string _accCreditName = "";
        private string _accPPnName = "";
        private string _accOtherName = "";
    
        public MsCustGroupAcc(string _prmCustGroup, string _prmCurrCode, string _prmAccARName, string _prmAccDiscName, string _prmAccDPName, string _prmAccCreditName, string _prmAccPPnName, string _prmAccOtherName)
        {
            this.CustGroup = _prmCustGroup;
            this.CurrCode = _prmCurrCode;
            this.AccARName = _prmAccARName;
            this.AccDiscName = _prmAccDiscName;
            this.AccDPName = _prmAccDPName;
            this.AccCreditName = _prmAccCreditName;
            this.AccPPnName = _prmAccPPnName;
            this.AccOtherName = _prmAccOtherName;
        }

        public string AccARName
        {
            get
            {
                return this._accARName;
            }

            set
            {
                this._accARName = value;
            }
        }

        public string AccDiscName
        {
            get
            {
                return this._accDiscName;
            }

            set
            {
                this._accDiscName = value;
            }
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

        public string AccCreditName
        {
            get
            {
                return this._accCreditName;
            }

            set
            {
                this._accCreditName = value;
            }
        }

        public string AccPPnName
        {
            get
            {
                return this._accPPnName;
            }

            set
            {
                this._accPPnName = value;
            }
        }

        public string AccOtherName
        {
            get
            {
                return this._accOtherName;
            }

            set
            {
                this._accOtherName = value;
            }
        }
    }
}
