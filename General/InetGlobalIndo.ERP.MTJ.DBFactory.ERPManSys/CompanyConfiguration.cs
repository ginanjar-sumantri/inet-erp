using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class CompanyConfiguration
    {
        public CompanyConfiguration(String _prmConfigCode, String _prmSetValue, String _prmConfigDescription,
            String _prmCreateBy, String _prmGroupConfig, char? _prmAlowEdit,  byte? _prmSortNo, bool? _prmVisible, 
            String _prmValueType, String _prmSQLExpr)
        {
            this.ConfigCode = _prmConfigCode;
            this.SetValue = _prmSetValue;
            this.ConfigDescription = _prmConfigDescription;
            this.CreateBy = _prmCreateBy;
            this.GroupConfig = _prmGroupConfig;
            this.AlowEdit = _prmAlowEdit;
            this.SortNo = _prmSortNo;
            this.Visible = _prmVisible;
            this.ValueType = _prmValueType;
            this.SQLExpr = _prmSQLExpr;
        }
    }
}
