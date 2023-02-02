using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Reflection;
using System.Linq.Expressions;
using System.ComponentModel;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsCurrency : Base
    {
        public MsCurrency(string _prmCurrCode, string _prmCurrName, char _prmFgHome, byte _prmDecimalPlace, byte _prmDecimalPlaceReport, decimal _prmValueTolerance)
        {
            this.CurrCode = _prmCurrCode;
            this.CurrName = _prmCurrName;
            this.FgHome = _prmFgHome;
            this.DecimalPlace = _prmDecimalPlace;
            this.DecimalPlaceReport = _prmDecimalPlaceReport;
            this.ValueTolerance = _prmValueTolerance;
        }

        public MsCurrency(string _prmCurrCode, string _prmCurrName)
        {
            this.CurrCode = _prmCurrCode;
            this.CurrName = _prmCurrName;
        }
    }
}