using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class POSTransTypeDataMapper
    {
        public static String GetTransType(POSTransType _prmStatus)
        {
            String _result = "";

            switch (_prmStatus)
            {
                case POSTransType.Retail:
                    _result = "RETAIL";
                    break;
                case POSTransType.Internet:
                    _result = "INTERNET";
                    break;                
                case POSTransType.Ticketing:
                    _result = "RETAIL";
                    break;
                case POSTransType.Printing:
                    _result = "PRINTING";
                    break;
                case POSTransType.Photocopy:
                    _result = "PHOTOCOPY";
                    break;
                case POSTransType.Shipping:
                    _result = "SHIPPING";
                    break;
                case POSTransType.Graphic:
                    _result = "GRAPHIC";
                    break;
                case POSTransType.EVoucher:
                    _result = "VOUCHER";
                    break;
                case POSTransType.Cafe:
                    _result = "CAFE";
                    break;
                default:
                    _result = "RETAIL";
                    break;
            }

            return _result;
        }

        public static POSTransType GetTransType(String _prmStatus)
        {
            POSTransType _result;

            switch (_prmStatus)
            {
                case "RETAIL":
                    _result = POSTransType.Retail;
                    break;
                case "INTERNET":
                    _result = POSTransType.Internet;
                    break;
                case "TICKETING":
                    _result = POSTransType.Ticketing;
                    break;
                case "PRINTING":
                    _result = POSTransType.Printing;
                    break;
                case "PHOTOCOPY":
                    _result = POSTransType.Photocopy;
                    break;
                case "SHIPPING":
                    _result = POSTransType.Shipping;
                    break;
                case "GRAPHIC":
                    _result = POSTransType.Graphic;
                    break;
                case "VOUCHER":
                    _result = POSTransType.EVoucher;
                    break;
                case "CAFE":
                    _result = POSTransType.Cafe;
                    break;
                default:
                    _result = POSTransType.Retail;
                    break;
            }

            return _result;
        }        
    }
}