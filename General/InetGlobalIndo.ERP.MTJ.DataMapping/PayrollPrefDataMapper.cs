using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class PayrollPrefDataMapper
    {
        public static String GetPayrollPrefText(PayrollPref _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case PayrollPref.Benefit:
                    _result = "Benefit";
                    break;
                case PayrollPref.Deduction:
                    _result = "Deduction";
                    break;
                case PayrollPref.Payroll:
                    _result = "Payroll";
                    break;
                case PayrollPref.Terminate:
                    _result = "Terminate";
                    break;
            }

            return _result;
        }

        public static String GetPayrollPrefText(Char _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case 'B':
                    _result = "Benefit";
                    break;
                case 'D':
                    _result = "Deduction";
                    break;
                case 'P':
                    _result = "Payroll";
                    break;
                case 'T':
                    _result = "Terminate";
                    break;
            }

            return _result;
        }

        public static Char GetPayrollPrefValue(PayrollPref _prmValue)
        {
            Char _result = ' ';

            switch (_prmValue)
            {
                case PayrollPref.Benefit:
                    _result = 'B';
                    break;
                case PayrollPref.Deduction:
                    _result = 'D';
                    break;
                case PayrollPref.Payroll:
                    _result = 'P';
                    break;
                case PayrollPref.Terminate:
                    _result = 'T';
                    break;
            }

            return _result;
        }
    }
}