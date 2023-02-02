using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class PayrollTypeDataMapper
    {
        public static String GetPayrollTypeText(PayrollType _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case PayrollType.POT:
                    _result = "Potongan";
                    break;
                case PayrollType.GP:
                    _result = "Gaji Pokok";
                    break;
                case PayrollType.PPH:
                    _result = "PPH";
                    break;
                case PayrollType.TTT:
                    _result = "Tunjangan Aktivitas";
                    break;
                case PayrollType.T3:
                    _result = "Tunjangan Tidak Tetap";
                    break;
                case PayrollType.TT:
                    _result = "Tunjangan Tetap";
                    break;
                case PayrollType.RAPEL:
                    _result = "RAPEL";
                    break;
                case PayrollType.PESANGON:
                    _result = "Pesangon";
                    break;
            }

            return _result;
        }

        public static String GetPayrollTypeText(String _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case "POT":
                    _result = "Potongan";
                    break;
                case "GP":
                    _result = "Gaji Pokok";
                    break;
                case "PPH":
                    _result = "PPH";
                    break;
                case "TTT":
                    _result = "Tunjangan Aktivitas";
                    break;
                case "T3":
                    _result = "Tunjangan Tidak Tetap";
                    break;
                case "TT":
                    _result = "Tunjangan Tetap";
                    break;
                case "RAPEL":
                    _result = "RAPEL";
                    break;
                case "PESANGON":
                    _result = "Pesangon";
                    break;
            }

            return _result;
        }

        public static String GetPayrollTypeValue(PayrollType _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case PayrollType.POT:
                    _result = "POT";
                    break;
                case PayrollType.GP:
                    _result = "GP";
                    break;
                case PayrollType.PPH:
                    _result = "PPH";
                    break;
                case PayrollType.TTT:
                    _result = "TTT";
                    break;
                case PayrollType.TT:
                    _result = "TT";
                    break;
                case PayrollType.T3:
                    _result = "T3";
                    break;
                case PayrollType.RAPEL:
                    _result = "RAPEL";
                    break;
                case PayrollType.PESANGON:
                    _result = "PESANGON";
                    break;
            }

            return _result;
        }
    }
}