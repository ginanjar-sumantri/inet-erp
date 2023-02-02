using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCRROtherHd
    {
        private string _wrhsName = "";
        private string _stockTypeName = "";

        public STCRROtherHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, char _prmStatus, string _prmWrhsCode, string _prmWrhsName, char _prmWrhsFgSubLed, string _prmWrhsSubLed, string _prmStockType, string _prmStockTypeName, string _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.WrhsCode = _prmWrhsCode;
            this.WrhsName = _prmWrhsName;
            this.WrhsFgSubLed = _prmWrhsFgSubLed;
            this.WrhsSubLed = _prmWrhsSubLed;
            this.StockType = _prmStockType;
            this.StockTypeName = _prmStockTypeName;
            this.Remark = _prmRemark;
        }

        public string WrhsName
        {
            get
            {
                return this._wrhsName;
            }
            set
            {
                this._wrhsName = value;
            }
        }

        public string StockTypeName
        {
            get
            {
                return this._stockTypeName;
            }
            set
            {
                this._stockTypeName = value;
            }
        }
    }
}
