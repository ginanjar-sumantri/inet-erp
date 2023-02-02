using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{

    public partial class MsStockTypeDt
    {
        private string _transTypeName = "";
        private char? _fgAcount = ' ';

        public MsStockTypeDt(string _prmStockType, string _prmTransType, string _prmCurentUser, DateTime? _prmDateTime)
        {
            this.StockType = _prmStockType;
            this.TransType = _prmTransType;
            this.UserId = _prmCurentUser;
            this.UserDate = _prmDateTime;
        }

        public MsStockTypeDt(string _prmStockType, string _prmTransType, string _prmCurentUser, DateTime? _prmDateTime, string _prmTransTypeName, char? _prmFgAccount)
        {
            this.StockType = _prmStockType;
            this.TransType = _prmTransType;
            this.UserId = _prmCurentUser;
            this.UserDate = _prmDateTime;
            this.TransTypeName = _prmTransTypeName;
            this.FgAccount = _prmFgAccount;
        }

        public string TransTypeName
        {
            get
            {
                return this._transTypeName;
            }
            set
            {
                this._transTypeName = value;
            }
        }

        public char? FgAccount
        {
            get
            {
                return this._fgAcount;
            }
            set
            {
                this._fgAcount = value;
            }
        }
    }


}
