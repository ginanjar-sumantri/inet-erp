using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class POSMsInternetTable
    {
        public POSMsInternetTable(string _prmFloorType, int _prmFloorNmbr, int _prmTableIDPerRoom)
        {
            this.FloorType = _prmFloorType;
            this.FloorNmbr = _prmFloorNmbr;
            this.TableIDPerRoom = _prmTableIDPerRoom;
        }

        public POSMsInternetTable(string _prmFloorType, int _prmFloorNmbr, int _prmTableIDPerRoom,string _prmTableNum)
        {
            this.FloorType = _prmFloorType;
            this.FloorNmbr = _prmFloorNmbr;
            this.TableIDPerRoom = _prmTableIDPerRoom;
            this.TableNmbr = _prmTableNum;
        }
    }
}
