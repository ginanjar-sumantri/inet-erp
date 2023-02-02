using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class ReceiptDataMapper
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

        public static char GetModeReceiptType(ModeReceiptType _prmModeReceiptType)
        {
            char _result;

            switch (_prmModeReceiptType)
            {
                case ModeReceiptType.Bank:
                    _result = 'B';
                    break;
                case ModeReceiptType.Giro:
                    _result = 'G';
                    break;
                case ModeReceiptType.Kas:
                    _result = 'K';
                    break;
                case ModeReceiptType.Other:
                    _result = 'O';
                    break;
                default:
                    _result = 'O';
                    break;
            }

            return _result;
        }

        public static char GetTypeReceipt(TypeReceipt _prmTypeReceipt)
        {
            char _result;

            switch (_prmTypeReceipt)
            {
                case TypeReceipt.All:
                    _result = 'A';
                    break;
                case TypeReceipt.Payment:
                    _result = 'P';
                    break;
                case TypeReceipt.Receipt:
                    _result = 'R';
                    break;
                default:
                    _result = 'A';
                    break;
            }

            return _result;
        }

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
    }
}