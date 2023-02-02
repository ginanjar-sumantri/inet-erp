using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class OrganizationUnitDataMapper
    {
        public static byte ActiveStatus(OrganizationUnitActiveStatus _prmValue)
        {
            byte _result = 0;

            switch (_prmValue)
            {
                case OrganizationUnitActiveStatus.NotActive:
                    _result = 0;
                    break;
                case OrganizationUnitActiveStatus.Active:
                    _result = 1;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static byte ActiveStatus(bool _prmValue)
        {
            byte _result = 0;

            switch (_prmValue)
            {
                case false:
                    _result = 0;
                    break;
                case true:
                    _result = 1;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static bool ActiveStatus(byte _prmValue)
        {
            bool _result = false;

            switch (_prmValue)
            {
                case 0:
                    _result = false;
                    break;
                case 1:
                    _result = true;
                    break;
                default:
                    _result = false;
                    break;
            }

            return _result;
        }

        //public static OrganizationUnitActiveStatus ActiveStatus(byte _prmValue)
        //{
        //    OrganizationUnitActiveStatus _result;

        //    switch (_prmValue)
        //    {
        //        case 0:
        //            _result = OrganizationUnitActiveStatus.NotActive;
        //            break;
        //        case 1:
        //            _result = OrganizationUnitActiveStatus.Active;
        //            break;
        //        default:
        //            _result = OrganizationUnitActiveStatus.NotActive;
        //            break;
        //    }

        //    return _result;
        //}

        public static string ActiveStatusText(byte _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "InActive";
                    break;
                case 1:
                    _result = "Active";
                    break;
                default:
                    _result = "InActive";
                    break;
            }

            return _result;
        }
    }
}