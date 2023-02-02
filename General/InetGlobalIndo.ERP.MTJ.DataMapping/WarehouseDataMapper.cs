using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class WarehouseDataMapper 
    {
        public static string GetStatus(char _prmValue)
        {
            string _result = "";

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = "Active";
                    break;
                case "n":
                    _result = "Not Active";
                    break;
            }
            return _result;
        }

        public static Boolean IsActive(char _prmValue)
        {
            Boolean _result = false;

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = true;
                    break;
                case "n":
                    _result = false;
                    break;
            }
            return _result;
        }

        public static char IsActive(Boolean _prmValue)
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

        public static char GetYesNo(YesNo _prmYesNo)
        {
            char _result;

            switch (_prmYesNo)
            {
                case YesNo.Yes:
                    _result = 'Y';
                    break;
                case YesNo.No:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static char GetSubledStatus(WarehouseSubledStatus _prmStatus)
        {
            char _result;

            switch (_prmStatus)
            {
                case WarehouseSubledStatus.Customer:
                    _result = 'C';
                    break;
                case WarehouseSubledStatus.Supplier:
                    _result = 'S';
                    break;
                case WarehouseSubledStatus.NoSubled:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static byte GetWrhsType(string _prmWrhsType)
        {
            byte _result;

            switch (_prmWrhsType)
            {
                case "Owner" :
                    _result = 0;
                    break;
                case "Deposit In" :
                    _result = 1;
                    break;
                case "Deposit Out" :
                    _result = 2;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static string GetWrhsType(byte _prmWrhsType)
        {
            string _result;

            switch (_prmWrhsType)
            {
                case 0:
                    _result = "Owner";
                    break;
                case 1:
                    _result = "Deposit In";
                    break;
                case 2:
                    _result = "Deposit Out";
                    break;
                default:
                    _result = "Owner";
                    break;
            }

            return _result;
        }
    }
}