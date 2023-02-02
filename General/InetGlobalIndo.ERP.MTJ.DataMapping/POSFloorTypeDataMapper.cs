using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class POSFloorTypeDataMapper
    {
        public static POSFloorType GetFloorType(String _prmValue)
        {
            POSFloorType _result;

            switch (_prmValue)
            {
                case "Internet":
                    _result = POSFloorType.Internet;
                    break;
                case "Cafe":
                    _result = POSFloorType.Cafe;
                    break;
                default:
                    _result = POSFloorType.Internet;
                    break;
            }

            return _result;
        }

        public static String GetFloorType(POSFloorType _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case POSFloorType.Internet:
                    _result = "Internet";
                    break;
                case POSFloorType.Cafe:
                    _result = "Cafe";
                    break;
                default:
                    _result = "Internet";
                    break;
            }

            return _result;
        }
    }
}