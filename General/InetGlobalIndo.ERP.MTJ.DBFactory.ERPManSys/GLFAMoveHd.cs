using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLFAMoveHd
    {
        private string _faLocationNameSrc = "";
        private string _faLocationNameDest = "";

        public GLFAMoveHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, char _prmStatus, string _prmFALocationTypeSrc, string _prmFALocationCodeSrc, string _prmFALocationNameSrc, string _prmFALocationTypeDest, string _prmFALocationCodeDest, string _prmFALocationNameDest)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.FALocationTypeSrc = _prmFALocationTypeSrc;
            this.FALocationCodeSrc = _prmFALocationCodeSrc;
            this.FALocationNameSrc = _prmFALocationNameSrc;
            this.FALocationTypeDest = _prmFALocationTypeDest;
            this.FALocationCodeDest = _prmFALocationCodeDest;
            this.FALocationNameDest = _prmFALocationNameDest;
        }

        public string FALocationNameDest
        {
            get
            {
                return this._faLocationNameDest;
            }

            set
            {
                this._faLocationNameDest = value;
            }
        }

        public string FALocationNameSrc
        {
            get
            {
                return this._faLocationNameSrc;
            }

            set
            {
                this._faLocationNameSrc = value;
            }
        }
    }
}
