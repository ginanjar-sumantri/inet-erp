using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PRCPODt2
    {
        private string _productName = "";
        private string _unitName = "";
        private string _requestNoFileNmbr = "";

        //public PRCPODt2(string _prmTransNmbr, int _prmRevisi, string _prmProductCode, string _prmProductName, string _prmRequestNo, string _prmRequestNoFileNmbr, decimal _prmQty, string _prmUnitName, string _prmCreatedBy)
        //{
        //    this.TransNmbr = _prmTransNmbr;
        //    this.Revisi = _prmRevisi;
        //    this.ProductCode = _prmProductCode;
        //    this.ProductName = _prmProductName;
        //    this.RequestNo = _prmRequestNo;
        //    this.RequestNoFileNmbr = _prmRequestNoFileNmbr;
        //    this.Qty = _prmQty;
        //    this.UnitName = _prmUnitName;
        //    this.CreatedBy = _prmCreatedBy;
        //}

        public PRCPODt2(string _prmTransNmbr, int _prmRevisi, string _prmRequestNo, string _prmRequestNoFileNmbr, string _prmCreatedBy)
        {
            this.TransNmbr = _prmTransNmbr;
            this.Revisi = _prmRevisi;
            this.RequestNo = _prmRequestNo;
            this.RequestNoFileNmbr = _prmRequestNoFileNmbr;
            this.CreatedBy = _prmCreatedBy;
        }

        public string ProductName
        {
            get
            {
                return this._productName;
            }
            set
            {
                this._productName = value;
            }
        }

        public string UnitName
        {
            get
            {
                return this._unitName;
            }
            set
            {
                this._unitName = value;
            }
        }

        public string RequestNoFileNmbr
        {
            get
            {
                return this._requestNoFileNmbr;
            }
            set
            {
                this._requestNoFileNmbr = value;
            }
        }
    }
}
