using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class SMSCategoryDataMapper 
    {
        public static String SMSCategoryText(SMSCategoryEnum _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case SMSCategoryEnum.Other:
                    _result = "Other";
                    break;
                case SMSCategoryEnum.InetReg:
                    _result = "Registration";
                    break;
                case SMSCategoryEnum.Konf:
                    _result = "Confirmation";
                    break;
                default:
                    _result = "Other";
                    break;
            }

            return _result;
        }

        public static String SMSCategoryText(String _prmValue)
        {
            String _result = "";

            switch (_prmValue.ToLower())
            {
                case "other":
                    _result = "Other";
                    break;
                case "inetreg":
                    _result = "Registration";
                    break;
                case "konf":
                    _result = "Confirmation";
                    break;
                default:
                    _result = "Other";
                    break;
            }

            return _result;
        }

        public static String SMSCategory(SMSCategoryEnum _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case SMSCategoryEnum.Other:
                    _result = "Other";
                    break;
                case SMSCategoryEnum.InetReg:
                    _result = "InetReg";
                    break;
                case SMSCategoryEnum.Konf:
                    _result = "Konf";
                    break;
                default:
                    _result = "Other";
                    break;
            }

            return _result;
        }

        public static SMSCategoryEnum SMSCategory(String _prmValue)
        {
            SMSCategoryEnum _result;

            switch (_prmValue.ToLower())
            {
                case "other":
                    _result = SMSCategoryEnum.Other;
                    break;
                case "inetreg":
                    _result = SMSCategoryEnum.InetReg;
                    break;
                case "konf":
                    _result = SMSCategoryEnum.Konf;
                    break;
                default:
                    _result = SMSCategoryEnum.Other;
                    break;
            }

            return _result;
        }
    }
}