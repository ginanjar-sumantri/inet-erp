using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class MsMenu
    {
        public MsMenu(short _prmMenuID, String _prmText)
        {
            this.MenuID = _prmMenuID;
            this.Text = _prmText;
        }
    }
}
