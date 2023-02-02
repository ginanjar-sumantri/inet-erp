using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLFAAddStockDt
    {
        private string _productName = "";
        private string _locationName = "";
        private string _faCode = "";
        private string _faName = "";
        private string _faStatus = "";
        private string _faStatusName = "";
        private char _faOwner = ' ';
        private string _faSubGroup = "";
        private string _faSubGroupName = "";

        public GLFAAddStockDt(Guid _prmGLFAAddStockDtCode, string _prmTransNmbr, string _prmProductCode, string _prmProductName, string _prmLocationCode, string _prmLocationName, string _prmFACode, string _prmFAName, string _prmFAStatus, string _prmFAStatusName, char _prmFAOwner, string _prmFASubGroup, string _prmFASubGroupName)
        {
            this.GLFAAddStockDtCode = _prmGLFAAddStockDtCode;
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.LocationCode = _prmLocationCode;
            this.LocationName = _prmLocationName;
            this.FACode = _prmFACode;
            this.FAName = _prmFAName;
            this.FAStatus = _prmFAStatus;
            this.FAStatusName = _prmFAStatusName;
            this.FAOwner = _prmFAOwner;
            this.FASubGroup = _prmFASubGroup;
            this.FASubGroupName = _prmFASubGroupName;
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

        public string LocationName
        {
            get
            {
                return this._locationName;
            }
            set
            {
                this._locationName = value;
            }
        }

        public string FACode
        {
            get
            {
                return this._faCode;
            }
            set
            {
                this._faCode = value;
            }
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

        public string FAStatus
        {
            get
            {
                return this._faStatus;
            }
            set
            {
                this._faStatus = value;
            }
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

        public char FAOwner
        {
            get
            {
                return this._faOwner;
            }
            set
            {
                this._faOwner = value;
            }
        }

        public string FASubGroup
        {
            get
            {
                return this._faSubGroup;
            }
            set
            {
                this._faSubGroup = value;
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
