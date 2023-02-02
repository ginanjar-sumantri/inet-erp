using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLFAAddStockDt2
    {
        private string _faStatusName = "";
        private string _faSubGroupName = "";

        public GLFAAddStockDt2(Guid _prmGLFAAddStockDtCode, string _prmFACode, string _prmFAName, string _prmFAStatus, string _prmFAStatusName, char _prmFAOwner, string _prmFASubGroup, string _prmFASubGroupName)
        {
            this.GLFAAddStockDtCode = _prmGLFAAddStockDtCode;
            this.FACode = _prmFACode;
            this.FAName = _prmFAName;
            this.FAStatus = _prmFAStatus;
            this.FAStatusName = _prmFAStatusName;
            this.FAOwner = _prmFAOwner;
            this.FASubGroup = _prmFASubGroup;
            this.FASubGroupName = _prmFASubGroupName;
        }

        public string FAStatusName
        {
            get
            {
                return this._faStatusName;
            }
            set
            {
                this._faStatusName = value;
            }
        }

        public string FASubGroupName
        {
            get
            {
                return this._faSubGroupName;
            }
            set
            {
                this._faSubGroupName = value;
            }
        }
    }
}
