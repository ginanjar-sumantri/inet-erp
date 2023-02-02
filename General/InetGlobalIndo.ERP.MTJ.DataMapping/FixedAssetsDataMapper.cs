using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class FixedAssetsDataMapper 
    {
        public static bool IsChecked(char _prmValue)
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

        public static char IsChecked(bool _prmValue)
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

        public static char IsAllowAddValue(bool _prmValue)
        {
            char _result = 'N';

            switch (_prmValue)
            {
                case true:
                    _result = 'Y';
                    break;
                case false:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static bool IsFGAddValue(char? _prmValue)
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

        public static char GetValueFALocation(string _prmFALocation)
        {
            char _result = ' ';

            switch (_prmFALocation.ToLower())
            {
                case "general":
                    _result = 'F';
                    break;
                case "employee":
                    _result = 'E';
                    break;
                case "customer":
                    _result = 'C';
                    break;
                case "supplier":
                    _result = 'S';
                    break;
                default:
                    _result = 'F';
                    break;
            }
            return _result;
        }

        public static char CreatedFrom(FixedAssetCreatedFrom _prmFACreatedFrom)
        {
            char _result;

            switch (_prmFACreatedFrom)
            {
                case FixedAssetCreatedFrom.FAAddStock:
                    _result = 'S';
                    break;
                case FixedAssetCreatedFrom.FAPurchase:
                    _result = 'P';
                    break;
                case FixedAssetCreatedFrom.Manual:
                    _result = 'M';
                    break;
                default:
                    _result = 'M';
                    break;
            }

            return _result;
        }

        public static string CreatedFromText(char _prmFACreatedFrom)
        {
            string _result;

            switch (_prmFACreatedFrom)
            {
                case 'S':
                    _result = "FA Add Stock";
                    break;
                case 'P':
                    _result = "FA Purchase";
                    break;
                case 'M':
                    _result = "Manual";
                    break;
                default:
                    _result = "Manual";
                    break;
            }

            return _result;
        }

        public static bool CreateJournal(YesNo _prmCreateJournal)
        {
            bool _result;

            switch (_prmCreateJournal)
            {
                case YesNo.Yes:
                    _result = true;
                    break;
                case YesNo.No:
                    _result = false;
                    break;
                default:
                    _result = true;
                    break;
            }

            return _result;
        }

        public static string CreateJournal(bool _prmCreateJournal)
        {
            string _result;

            switch (_prmCreateJournal)
            {
                case true:
                    _result = "Yes";
                    break;
                case false:
                    _result = "No";
                    break;
                default:
                    _result = "Yes";
                    break;
            }

            return _result;
        }
    }
}