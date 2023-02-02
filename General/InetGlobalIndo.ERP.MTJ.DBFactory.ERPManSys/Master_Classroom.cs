using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_Classroom
    {
        private string _classGrpName = "";
        private string _courseLocationName = "";

        public Master_Classroom(Guid _prmClassCode, string _prmClassName, string _prmClassGroupName, string _prmLocationName, int _prmMaxCapacity)
        {
            this.ClassroomCode = _prmClassCode;
            this.ClassroomName = _prmClassName;
            this.ClassroomGroupName = _prmClassGroupName;
            this.CourseLocationName = _prmLocationName;
            this.MaxCapacity = _prmMaxCapacity;
        }

        public Master_Classroom(Guid _prmClassCode, string _prmClassName)
        {
            this.ClassroomCode = _prmClassCode;
            this.ClassroomName = _prmClassName;
        }


        public string ClassroomGroupName
        {
            get
            {
                return this._classGrpName;
            }
            set
            {
                this._classGrpName = value;
            }
        }

        public string CourseLocationName
        {
            get
            {
                return this._courseLocationName;
            }
            set
            {
                this._courseLocationName = value;
            }
        }
    }
}
