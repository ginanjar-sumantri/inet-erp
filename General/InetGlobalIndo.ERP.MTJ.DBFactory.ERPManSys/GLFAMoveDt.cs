using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLFAMoveDt
    {
        private string _faName = "";

        public GLFAMoveDt(string _prmFACode, string _prmFAName, string _prmRemark)
        {
            this.FACode = _prmFACode;
            this.FAName = _prmFAName;
            this.Remark = _prmRemark;
        }

        public string FAName
        {
            get
            {
                return this._faName;
            }

            set
            {
                this._faName = value;
            }
        }
    }
}
