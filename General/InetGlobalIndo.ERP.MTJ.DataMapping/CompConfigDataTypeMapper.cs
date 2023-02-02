using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class CompConfigDataTypeMapper
    {
        public static String GetCompConfigDataType(CompConfigDataType _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case CompConfigDataType.String:
                    _result = "S";
                    break;
                case CompConfigDataType.Multiline:
                    _result = "MTL";
                    break;
                case CompConfigDataType.Decimal:
                    _result = "DEC";
                    break;
                case CompConfigDataType.Int:
                    _result = "INT";
                    break;
                case CompConfigDataType.Enum:
                    _result = "ENM";
                    break;
                case CompConfigDataType.SQLQuery:
                    _result = "SQL";
                    break;
            }

            return _result;
        }

        public static CompConfigDataType GetCompConfigDataType(String _prmValue)
        {
            CompConfigDataType _result = CompConfigDataType.String;

            switch (_prmValue)
            {
                case "S":
                    _result = CompConfigDataType.String;
                    break;
                case "MTL":
                    _result = CompConfigDataType.Multiline;
                    break;
                case "DEC":
                    _result = CompConfigDataType.Decimal;
                    break;
                case "INT":
                    _result = CompConfigDataType.Int;
                    break;
                case "ENM":
                    _result = CompConfigDataType.Enum;
                    break;
                case "SQL":
                    _result = CompConfigDataType.SQLQuery;
                    break;
            }

            return _result;
        }
    }
}