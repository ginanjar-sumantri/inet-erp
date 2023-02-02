using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace InetGlobalIndo.ERP.MTJ.Common
{
    public static class DocumentHandler
    {
        public static bool IsDocumentFile(String _prmPath, String _prmFileExtAllowed)
        {
            Regex _regex = new Regex(@"\" + _prmFileExtAllowed + "$", RegexOptions.IgnoreCase);

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
