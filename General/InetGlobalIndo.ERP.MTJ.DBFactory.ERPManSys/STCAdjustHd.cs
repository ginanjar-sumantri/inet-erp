using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCAdjustHd
    {
        private string _wrhsName = "";
        private string _stockTypeName = "";
        private string _fileNo = "";

        public STCAdjustHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, char _prmStatus, string _prmOpnameNo, string _prmFileNo, string _prmWrhsCode, string _prmWrhsName, string _prmStockType, string _prmStockTypeName)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.OpnameNo = _prmOpnameNo;
            this.FileNo = _prmFileNo;
            this.WrhsCode = _prmWrhsCode;
            this.WrhsName = _prmWrhsName;
            this.StockType = _prmStockType;
            this.StockTypeName = _prmStockTypeName;
        }

        public STCAdjustHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, char _prmStatus, string _prmWrhsCode, string _prmWrhsName, string _prmStockType, string _prmStockTypeName)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.WrhsCode = _prmWrhsCode;
            this.WrhsName = _prmWrhsName;
            this.StockType = _prmStockType;
            this.StockTypeName = _prmStockTypeName;
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

        public string FileNo
        {
            get
            {
                return this._fileNo;
            }
            set
            {
                this._fileNo = value;
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
