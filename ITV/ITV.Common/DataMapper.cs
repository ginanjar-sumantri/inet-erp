using System;
using ITV.Common.Enum;

namespace ITV.Common
{
    public abstract class DataMapper
    {
        public static String GetYesNo(Boolean _value)
        {
            switch (_value)
            {
                case true:
                    return "Yes";
                case false:
                    return "No";
                default:
                    return "No";
            }
        }
        public static Boolean GetYesNo(String _value)
        {
            switch (_value)
            {
                case "Yes":
                    return true;
                case "No":
                    return false;
                default:
                    return false;
            }
        }
        public static Boolean GetYesNo(YesNo _value)
        {
            switch (_value)
            {
                case YesNo.Yes:
                    return true;
                case YesNo.No:
                    return false;
                default:
                    return false;
            }
        }
    }
}
