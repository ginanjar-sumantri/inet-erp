using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class POSTrRetailDataMapper
    {
        public static POSTrRetailStatus GetStatus(Byte _prmStatus)
        {
            POSTrRetailStatus _result;

            switch (_prmStatus)
            {
                case 0:
                    _result = POSTrRetailStatus.NewEntry;
                    break;
                case 1:
                    _result = POSTrRetailStatus.OnHold;
                    break;
                case 2:
                    _result = POSTrRetailStatus.SendToCashier;
                    break;
                case 3:
                    _result = POSTrRetailStatus.Posted;
                    break;
                default:
                    _result = POSTrRetailStatus.NewEntry;
                    break;
            }

            return _result;
        }

        public static Byte GetStatus(POSTrRetailStatus _prmStatus)
        {
            Byte _result = 0;

            switch (_prmStatus)
            {
                case POSTrRetailStatus.NewEntry:
                    _result = 0;
                    break;
                case POSTrRetailStatus.OnHold:
                    _result = 1;
                    break;
                case POSTrRetailStatus.SendToCashier:
                    _result = 2;
                    break;
                case POSTrRetailStatus.Posted:
                    _result = 3;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static string GetStatusText(int _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case 0:
                    _result = "New Entry";
                    break;
                case 1:
                    _result = "On Hold";
                    break;
                case 2:
                    _result = "Send To Cashier";
                    break;
                case 3:
                    _result = "Posted";
                    break;
                default:
                    _result = "New Entry";
                    break;
            }

            return _result;
        }

        public static string GetStatusText(POSTrRetailStatus _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case POSTrRetailStatus.NewEntry:
                    _result = "0";
                    break;
                case POSTrRetailStatus.OnHold:
                    _result = "1";
                    break;
                case POSTrRetailStatus.SendToCashier:
                    _result = "2";
                    break;
                case POSTrRetailStatus.Posted:
                    _result = "3";
                    break;
                default:
                    _result = "0";
                    break;
            }

            return _result;
        }
    }
}