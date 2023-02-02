﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class WorkingHourDataMapper
    {
        public static String IsActive(Boolean _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case true:
                    _result = "Active";
                    break;
                case false:
                    _result = "Inactive";
                    break;
            }

            return _result;
        }

        public static Boolean IsActive(String _prmValue)
        {
            Boolean _result = false;

            switch (_prmValue)
            {
                case "Active":
                    _result = true;
                    break;
                case "Inactive":
                    _result = false;
                    break;
            }

            return _result;
        }

        public static String IsNextDay(Boolean _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case true:
                    _result = "Yes";
                    break;
                case false:
                    _result = "No";
                    break;
            }

            return _result;
        }

        public static Boolean IsNextDay(String _prmValue)
        {
            Boolean _result = false;

            switch (_prmValue)
            {
                case "Yes":
                    _result = true;
                    break;
                case "No":
                    _result = false;
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
    }
}