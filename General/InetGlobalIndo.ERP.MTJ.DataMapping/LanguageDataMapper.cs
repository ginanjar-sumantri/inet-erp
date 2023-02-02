using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class LanguageDataMapper 
    {
        public static bool IsLanguage(Language _prmValue)
        {
            bool _result;

            switch (_prmValue)
            {
                case Language.Indonesia:
                    _result = true;
                    break;
                case Language.English:
                    _result = false;
                    break;
                default:
                    _result = false;
                    break;
            }

            return _result;
        }

        public static Language IsLanguage(bool _prmValue)
        {
            Language _result;

            switch (_prmValue)
            {
                case true:
                    _result = Language.Indonesia;
                    break;
                case false:
                    _result = Language.English;
                    break;
                default:
                    _result = Language.English;
                    break;
            }

            return _result;
        }
    }
}