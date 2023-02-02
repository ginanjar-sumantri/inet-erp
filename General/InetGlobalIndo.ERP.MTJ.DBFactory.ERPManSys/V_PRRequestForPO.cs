using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class V_PRRequestForPO
    {
        private string _transNmbr = "";
        private int _revisi = 0;
        private string _unitName = "";

        public V_PRRequestForPO(string _prmPR_No, string _prmFileNmbr, int _prmQty, String _prmUnit, String _prmUnitName, String _prmRequestBy)
        {
            this.PR_No = _prmPR_No;
            this.FileNmbr = _prmFileNmbr;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.UnitName = _prmUnitName;
            this.RequestBy = _prmRequestBy;
        }

        public V_PRRequestForPO(string _prmTransNmbr, int _prmRevisi, string _prmPR_No, string _prmFileNmbr, String _prmRequestBy)
        {
            this.TransNmbr = _prmTransNmbr;
            this.Revisi = _prmRevisi;
            this.PR_No = _prmPR_No;
            this.FileNmbr = _prmFileNmbr;
            this.RequestBy = _prmRequestBy;
        }

        public string TransNmbr
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

        public int Revisi
        {
            get
            {
                return this._revisi;
            }
            set
            {
                this._revisi = value;
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
    }
}
