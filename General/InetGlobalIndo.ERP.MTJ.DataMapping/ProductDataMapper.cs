using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class ProductDataMapper
    {
        public static char IsActive(YesNo _prmValue)
        {
            char _result = 'N';

            switch (_prmValue)
            {
                case YesNo.Yes:
                    _result = 'Y';
                    break;
                case YesNo.No:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static YesNo IsActive(char _prmValue)
        {
            YesNo _result;

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = YesNo.Yes;
                    break;
                case "n":
                    _result = YesNo.No;
                    break;
                default:
                    _result = YesNo.No;
                    break;
            }

            return _result;
        }

        public static String ProductValTypeMapper(byte _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "FIFO";
                    break;
                case 1:
                    _result = "LIFO";
                    break;
                case 2:
                    _result = "Average";
                    break;
            }

            return _result;
        }

        public static byte ProductValTypeMapper(ProductValType _prmValue)
        {
            byte _result = 0;

            switch (_prmValue)
            {
                case ProductValType.FIFO:
                    _result = 0;
                    break;
                case ProductValType.LIFO:
                    _result = 1;
                    break;
                case ProductValType.Average:
                    _result = 2;
                    break;
            }

            return _result;
        }
    }
}