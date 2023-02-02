using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsWorkPlace
    {
        public MsWorkPlace(string _prmWorkPlaceCode, string _prmWorkPlaceName)
        {
            this.WorkPlaceCode = _prmWorkPlaceCode;
            this.WorkPlaceName = _prmWorkPlaceName;
        }

        public MsWorkPlace(string _prmWorkPlaceCode, string _prmWorkPlaceName, int? _prmJHTPercent, decimal? _prmUMR)
        {
            this.WorkPlaceCode = _prmWorkPlaceCode;
            this.WorkPlaceName = _prmWorkPlaceName;
            this.JHTPercent = _prmJHTPercent;
            this.UMR = _prmUMR;
        }
    }
}
