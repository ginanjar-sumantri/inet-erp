using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class SubledDataMapper
    {
        public static SubledStatus GetSubled(char _prmStatus)
        {
            SubledStatus _result;

            switch (_prmStatus)
            {
                case 'N':
                    _result = SubledStatus.NoSubled;
                    break;
                case 'C':
                    _result = SubledStatus.Customer;
                    break;
                case 'S':
                    _result = SubledStatus.Supplier;
                    break;
                case 'E':
                    _result = SubledStatus.Employee;
                    break;
                case 'F':
                    _result = SubledStatus.FixedAsset;
                    break;
                case 'P':
                    _result = SubledStatus.Product;
                    break;
                default:
                    _result = SubledStatus.NoSubled;
                    break;
            }

            return _result;
        }

        public static char GetSubled(SubledStatus _prmStatus)
        {
            char _result;

            switch (_prmStatus)
            {
                case SubledStatus.NoSubled:
                    _result = 'N';
                    break;
                case SubledStatus.Customer:
                    _result = 'C';
                    break;
                case SubledStatus.Supplier:
                    _result = 'S';
                    break;
                case SubledStatus.Employee:
                    _result = 'E';
                    break;
                case SubledStatus.FixedAsset:
                    _result = 'F';
                    break;
                case SubledStatus.Product:
                    _result = 'P';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static string GetSubledName(char _prmStatus)
        {
            string _result;

            switch (_prmStatus)
            {
                case 'N':
                    _result = "No Subled";
                    break;
                case 'C':
                    _result = "Customer";
                    break;
                case 'S':
                    _result = "Supplier";
                    break;
                case 'E':
                    _result = "Employee";
                    break;
                case 'F':
                    _result = "FixedAsset";
                    break;
                case 'P':
                    _result = "Product";
                    break;
                default:
                    _result = "No Subled";
                    break;
            }

            return _result;
        }
    }
}