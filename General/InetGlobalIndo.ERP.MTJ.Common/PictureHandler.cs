using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace InetGlobalIndo.ERP.MTJ.Common
{
    public static class PictureHandler
    {
        public static bool IsPictureFile(String _prmPath, String _prmPictureExtAllowed)
        {
            Regex _regex = new Regex(@"\" + _prmPictureExtAllowed + "$", RegexOptions.IgnoreCase);

            if (_regex.IsMatch(_prmPath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}