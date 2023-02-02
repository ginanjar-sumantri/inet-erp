using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class CurrencyDataMapper
    {
        //public static char GetYesNo(YesNo _prmYesNo)
        //{
        //    char _result;

        //    switch (_prmYesNo)
        //    {
        //        case YesNo.Yes:
        //            _result = 'Y';
        //            break;
        //        case YesNo.No:
        //            _result = 'N';
        //            break;
        //        default:
        //            _result = 'N';
        //            break;
        //    }

        //    return _result;
        //}

        public static string GetDecimal(byte _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "1";
                    break;
                case 1:
                    _result = "10";
                    break;
                case 2:
                    _result = "100";
                    break;
                case 3:
                    _result = "1000";
                    break;
                case 4:
                    _result = "10000";
                    break;
                case 5:
                    _result = "100000";
                    break;
                case 6:
                    _result = "1000000";
                    break;
                case 7:
                    _result = "10000000";
                    break;
                case 8:
                    _result = "100000000";
                    break;
                default:
                    _result = "1";
                    break;
            }
            return _result;
        }

        public static string GetFormatDecimal(byte _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "#,##0";
                    break;
                case 1:
                    _result = "#,##0.#";
                    break;
                case 2:
                    _result = "#,##0.##";
                    break;
                case 3:
                    _result = "#,##0.###";
                    break;
                case 4:
                    _result = "#,##0.####";
                    break;
                case 5:
                    _result = "#,##0.#####";
                    break;
                case 6:
                    _result = "#,##0######";
                    break;
                case 7:
                    _result = "#,##0.#######";
                    break;
                case 8:
                    _result = "#,##0.########";
                    break;
                default:
                    _result = "#,##0";
                    break;
            }
            return _result;
        }
    }
}