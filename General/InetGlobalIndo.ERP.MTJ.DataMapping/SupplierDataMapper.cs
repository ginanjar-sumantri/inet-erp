using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class SupplierDataMapper 
    {
        public static char SuppStatus(SupplierStatus _prmSupplierStatus)
        {
            char _result = ' ';

            switch(_prmSupplierStatus)
            {
                case SupplierStatus.Active:
                    _result = 'Y';
                    break;
                case SupplierStatus.NotActive:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static string SuppStatusText(char _prmSupplierStatus)
        {
            string _result = "";

            switch (_prmSupplierStatus)
            {
                case 'Y':
                    _result = "Active";
                    break;
                case 'N':
                    _result = "Inactive";
                    break;
                default:
                    _result = "Inactive";
                    break;
            }

            return _result;
        }

        public static bool IsActive(char _prmValue)
        {
            bool _result;

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = true;
                    break;
                case "n":
                    _result = false;
                    break;
                default:
                    _result = false;
                    break;
            }

            return _result;
        }

        public static char IsActive(bool _prmValue)
        {
            char _result;

            switch (_prmValue)
            {
                case true:
                    _result = 'Y';
                    break;
                case false:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static bool IsPPN(char _prmValue)
        {
            bool _result;

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = true;
                    break;
                case "n":
                    _result = false;
                    break;
                default:
                    _result = false;
                    break;
            }

            return _result;
        }

        public static char IsPPN(bool _prmValue)
        {
            char _result;

            switch (_prmValue)
            {
                case true:
                    _result = 'Y';
                    break;
                case false:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }
    }
}